using Microsoft.AspNetCore.Identity;
using Store.Data.Enity.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "ElsayedAhmed",
                    Email = "Sayed@gmail.com",
                    UserName = "Sayed",
                    Address = new Address
                    {
                        FirstName = "sayed",
                        LastName = "ahmed",
                        City = "Mansoura",
                        State = "Aga",
                        Street = "15",
                        PostalCode = "12345"

                    }
                };

            }
        }
    }
}