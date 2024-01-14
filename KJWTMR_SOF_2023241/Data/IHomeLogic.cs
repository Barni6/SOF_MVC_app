using KJWTMR_SOF_2023241.Models;
using System.Security.Claims;

namespace KJWTMR_SOF_2023241.Data
{
    public interface IHomeLogic
    {
        public void AddAlcohol(Alcohol alcohol, ClaimsPrincipal user);
        void AdminDeleteAlcohol(string uid);
        Task DelegateAdmin(ClaimsPrincipal user);
        void DeleteAlcohol(string uid, ClaimsPrincipal user);
        Task DoSomethingWithUser(ClaimsPrincipal user);
        IEnumerable<Alcohol> GetAlcohols();
        IEnumerable<SiteUser> GetUsers();
        Task GrantAdmin(string uid);
        Task RemoveAdmin(string uid);
    }
}