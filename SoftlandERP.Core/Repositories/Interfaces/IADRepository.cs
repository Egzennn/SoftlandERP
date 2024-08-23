using SoftlandERP.Data.Entities.Staff.AD;

namespace SoftlandERP.Core.Repositories.Interfaces
{
    public interface IADRepository
    {
        IEnumerable<ADUser>? GetAllADUsers();

        IEnumerable<string>? GetAllADUserLogins();

        IEnumerable<string>? GetAllADUserAcronyms();

        IEnumerable<ADGroup>? GetAllADGroups();

        ADUser? GetADUsersById(Guid? id);

        bool CreateUser(ADUser? adUser);

        bool UpdateUser(ADUser? adUser);

        IEnumerable<string>? GetAllADGroupsName();

        ADGroup? GetADGroupById(Guid? id);

        List<string> GetAllADGroupsByUser(string? login);

        bool CheckGroup(string? login, List<string> groups);

        bool CheckMembership(string? login, string? applicationModule);

        bool CheckLogin(string? login);

        public bool CheckSAM(string? login, string? sam);

        bool ResetPassword(ADUser? adUser);

        bool ChangeStatus(ADUser? adUser);

        int GetUsersCount();

        int GetComputersCount();

        string GetUserAcronym(string username);

        string GetUserFirstLastName(string username);
    }
}