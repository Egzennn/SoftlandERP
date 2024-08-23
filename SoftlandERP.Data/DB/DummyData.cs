using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SoftlandERP.Data.Entities.Staff.AD;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Data.DB
{
    public static class DummyData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var list = new List<HelperText>();

            list.AddRange(GetNames(typeof(ADUser)));
            list.AddRange(GetNames(typeof(ApplicationPermissionsRule)));
            list.AddRange(GetNames(typeof(UserAddress)));
            list.AddRange(GetNames(typeof(DownloadableForm)));

            modelBuilder?.Entity<HelperText>().HasData(list);
        }

        private static List<HelperText> GetNames(Type type)
        {
            var list = new List<HelperText>();

            var names = Array.ConvertAll(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance), field => field.Name.Replace("<", string.Empty, StringComparison.InvariantCulture).Replace(">k__BackingField", string.Empty, StringComparison.InvariantCulture));

            foreach (var name in names)
            {
                if (type.GetProperty(name)?.GetCustomAttributes(typeof(DisplayAttribute)).Any() == true || type.GetProperty(name)?.GetCustomAttributes(typeof(DisplayNameAttribute)).Any() == true)
                {
                    list.Add(new HelperText { Entity = type.Name, FieldName = name, FieldDisplayName = type.GetProperty(name)?.GetCustomAttributes(typeof(DisplayAttribute)).Cast<DisplayAttribute>().Single().Name });
                }
            }

            return list;
        }
    }
}