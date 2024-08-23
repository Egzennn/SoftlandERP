using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Mvc;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Core.Helpers
{
    public static class ExcelExporter
    {
        public static IActionResult Export<T>(IEnumerable<T> items, string moduleName)
        {
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.AddWorksheet(moduleName?.Replace("Słownik - ", string.Empty, StringComparison.InvariantCulture));

            using var usersTable = ExcelExporter.ToDataTable(items);

            var table = worksheet.Cell("B2").InsertTable(usersTable, moduleName, true);
            table.Theme = ClosedXML.Excel.XLTableTheme.TableStyleMedium1;
            table.SortColumns.Clear();

            worksheet.Columns().AdjustToContents();

            return workbook.Deliver(moduleName + ".xlsx");
        }

        private static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new (typeof(T).Name);

            using var numberColumn = new DataColumn("#", typeof(string));

            dataTable.Columns?.Add(numberColumn);

            List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();

            var propertyToDelete = properties.Where(x => x.Name == "Password" || x.Name == "Id");

            foreach (var property in propertyToDelete.ToList())
            {
                if (property != null)
                {
                    properties.Remove(property);
                }
            }

            if (typeof(T) == typeof(string))
            {
                dataTable.Columns?.Add("Value");
            }
            else
            {
                foreach (var property in properties)
                {
                    if (typeof(T).GetProperty(property.Name)?.GetCustomAttributes(typeof(DisplayAttribute)).Any() == true)
                    {
                        if (property.Name != "Password")
                        {
                            dataTable.Columns?.Add(typeof(T).GetProperty(property.Name)?.GetCustomAttributes(typeof(DisplayAttribute)).Cast<DisplayAttribute>().Single().Name);
                        }
                    }

                    if (typeof(T).GetProperty(property.Name)?.GetCustomAttributes(typeof(DisplayNameAttribute)).Any() == true)
                    {
                        if (property.Name != "Password")
                        {
                            dataTable.Columns?.Add(typeof(T).GetProperty(property.Name)?.GetCustomAttributes(typeof(DisplayNameAttribute)).Cast<DisplayNameAttribute>().Single().DisplayName);
                        }
                    }
                }
            }

            if (items != null)
            {
                int count = 1;

                foreach (T item in items)
                {
                    if (item?.GetType() == typeof(string))
                    {
                        var values = new object[2];
                        values[0] = count;
                        values[1] = item;
                        dataTable.Rows.Add(values);
                    }
                    else
                    {
                        var values = new object[properties.Count + 1];

                        values[0] = count;

                        for (int i = 0; i < properties.Count; i++)
                        {
                            if (properties[i].GetValue(item)?.GetType() == typeof(UserAddress))
                            {
                                values[i + 1] = properties[i].GetValue(item)?.ToString() ?? string.Empty;
                            }
                            else
                            {
                                values[i + 1] = properties[i].GetValue(item) ?? string.Empty;
                            }
                        }

                        dataTable.Rows.Add(values);
                    }

                    count++;
                }
            }

            return dataTable;
        }
    }
}