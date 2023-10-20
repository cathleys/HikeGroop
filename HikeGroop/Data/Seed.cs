using HikeGroop.Data.Enums;
using HikeGroop.Models;
using Microsoft.AspNetCore.Identity;

namespace HikeGroop.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<DataContext>();

            context.Database.EnsureCreated();

            if (!context.Destinations.Any())
            {
                context.Destinations.AddRange(new List<Destination>()
                    {
                       new Destination
                       {
                        Title = "Rizal Treasure Mountain Day Pass with Breakfast, Obstacle Course, Giant Seesaw & Bosay Falls Trek",
                        Description="Visit Treasure Mountain, one of the most stunning spots in Rizal, with this day tour.",
                        Image ="https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        HikingCategory = HikingCategory.DayHike,
                        HikingTour = HikingTour.PrivatePackage,
                        TrailClass = TrailClass.TrailClass1,

                        Itinerary = new Itinerary
                        {
                            MeetUp = "Mcdo's Greenfield District at Shaw Boulevard",
                            JumpOffPoint="Treasure Mountain",
                            Location ="Treasure Mountain, Tanay Rizal",
                            Elevation="505 MASL",
                            DaysRequired=1,
                            HoursToSummit=2,
                        }
                       },
                       new Destination
                       {
                        Title = "Nueva Ecija Mt. 387 & Aloha Falls Minor Day Hike with Transfers from Manila | Beginner-Friendly",
                        Description="Immerse in the natural beauty of Nueva Ecija with this day hike to Mt. 387's summit (also called Mt Batong Amat) and a visit to the Aloha Falls.",
                        Image ="https://gttp.imgix.net/364012/x/0/nueva-ecija-mt-3…in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        HikingCategory = HikingCategory.MultiDayHike,
                        HikingTour = HikingTour.PrivatePackage,
                        TrailClass = TrailClass.TrailClass4,

                        Itinerary = new Itinerary
                        {
                            MeetUp = "Your chosen pick-up point within Metro Manila or Cavite ",
                            JumpOffPoint="Mt. 387",
                            Location ="Nueva Ecija",
                            Elevation="1,390+ MASL",
                            DaysRequired=2,
                            HoursToSummit=3,
                        }
                       },

                       new Destination
                       {
                        Title = "Bulacan Mt. Manalmon & Matinik Twin Hike with Transfers from Manila | Beginner-Friendly",
                        Description="Set out on an outdoor adventure to the summits of Mt. Manalmon and Mt. Matinik in Bulacan through this twin hike tour.",
                        Image ="https://gttp.imgix.net/358293/x/0/have-fun-and-enj%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        HikingCategory = HikingCategory.DayHike ,
                        HikingTour = HikingTour.SharedTours,
                        TrailClass = TrailClass.TrailClass2,

                        Itinerary = new Itinerary
                        {
                            MeetUp = "chosen pick-up point in Metro Manila",
                            JumpOffPoint="Bulacan",
                            Location =" Mt.Manalmon and Mt.Matinik in Bulacan",
                            Elevation="192 MASL",
                            DaysRequired=1,
                            HoursToSummit=2,
                        }
                       },
                    });
                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(new List<Group>()
                {
                    new Group
                    {
                        Name = "Mountain Rangers",
                        Description="No Mountain we cannot move",
                        Image ="https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        Address  =new Address
                        {
                            City = "Pasig City"
                        },
                    },
                    new Group
                    {
                        Name = "Snooze and Shoes",
                        Description="We are on firw climbing top, sleepy when hiking down",
                        Image ="https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        Address  =new Address
                        {
                            City = "Rizal City"
                        },
                    },
                    new Group
                    {
                        Name = "Move Hikers!",
                        Description="We leave no trace but we leave legacy",
                        Image ="https://gttp.imgix.net/368267/x/0/take-photos-at-t%E2%80%A6in-h=210&ixlib=react-9.4.0&q=35&w=920&h=359&dpr=1",
                        Address  =new Address
                        {
                            City = "Quezon City"
                        },
                    },
                });
                context.SaveChanges();
            }


        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Member))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Member));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "cathdev@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser
                {
                    UserName = "cathdev",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    Address = new Address
                    {
                        City = "Pasig City",
                    }
                };
                await userManager.CreateAsync(newAdminUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            string appUserEmail = "user@etickets.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new AppUser
                {
                    UserName = "member",
                    Email = appUserEmail,
                    EmailConfirmed = true,
                    Address = new Address
                    {
                        City = "Pasig City",
                    }
                };
                await userManager.CreateAsync(newAppUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.Member);
            }
        }
    }

}
