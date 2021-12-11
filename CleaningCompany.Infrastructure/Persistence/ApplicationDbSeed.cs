using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Persistence
{
    public static class ApplicationDbSeed
    {
        public static async void InitializeProducts(ApplicationContext context)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(GetProducts());
                context.SaveChanges();
            }
        }

        public static async void InitializeOrderRequests(ApplicationContext context)
        {
            if (!context.OrderRequests.Any())
            {
                await context.OrderRequests.AddRangeAsync(GetOrderRequests());
                context.SaveChanges();
            }
        }

        public static async void InitializeOrders(ApplicationContext context)
        {
            if (!context.Orders.Any())
            {
                var employees = context.Employees.Take(2).ToList();

                await context.Orders.AddRangeAsync(GetOrders(employees));
                context.SaveChanges();
            }
        }


        public static async Task InitializeUser(ApplicationContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateUserRoles(roleManager);

            userManager.PasswordValidators.Clear();
            var users = GetUsers();
            foreach (var user in users)
            {
                if (userManager.Users.All(u => u.UserName != user.UserName))
                {
                    var (role, password) = GetUserCredentionals(user);

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, role);
                }
            }

        }

        private static async Task CreateUserRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetValues<UserRoles>()
                .Select(ur => ur.ToString());

            foreach (var role in roles)
            {
                var newRole = new IdentityRole(role);
                if (roleManager.Roles.All(r => r.Name != newRole.Name))
                {
                    await roleManager.CreateAsync(newRole);
                }
            }
        }

        private static (string role, string password) GetUserCredentionals(User user)
        {
            if (user is Client)
            {
                return (UserRoles.User.ToString(), "User123");
            }

            if (user is Employee)
            {
                return (UserRoles.Employee.ToString(), "emp123");
            }

            return (UserRoles.Admin.ToString(), "Admin123");
        }

        private static List<User> GetUsers()
        {
            return new List<User>()
            {
                new Client()
                {
                    Email = "testUser@gmail.com",
                    UserName = "testUser@gmail.com",
                    FirstName = "artsem",
                    LastName = "stoka",
                    Birthday = new DateTime(2021, 01, 31),
                    Discount = Discount.Premium,
                    Gender = Gender.Male,
                },
                new Client()
                {
                    Email = "testUser2@gmail.com",
                    UserName = "testUser2@gmail.com",
                    FirstName = "Adam",
                    LastName = "Linkoln",
                    Birthday = new DateTime(1987, 04, 21),
                    Discount = Discount.Regular,
                    Gender = Gender.Male
                },
                new Client()
                {
                    Email = "testUser3@gmail.com",
                    UserName = "testUser3@gmail.com",
                    FirstName = "Heyly",
                    LastName = "Jenkins",
                    Birthday = new DateTime(1997, 01, 31),
                    Discount = Discount.VIP,
                    Gender = Gender.Female
                },
                new Employee()
                {
                    Email = "testEmployeeNumber1@gmail.com",
                    UserName = "testEmployeeNumber1@gmail.com",
                    FirstName = "Employee",
                    LastName = "Number 1",
                    Birthday = new DateTime(2000, 06, 2),
                    Gender = Domain.Entities.Enums.Gender.Male,
                    HireDate = new DateTime(2010, 06, 2),
                    PhoneNumber = "+375296603042"
                },
                new Employee()
                {
                    Email = "testEmployeeNumber2@gmail.com",
                    UserName = "testEmployeeNumber2@gmail.com",
                    FirstName = "Employee",
                    LastName = "Number 2",
                    Birthday = new DateTime(1997, 06, 2),
                    Gender = Gender.Male,
                    HireDate = new DateTime(2011, 06, 2),
                    PhoneNumber = "+375296603042"
                },
                new User()
                {
                    Email = "Admin@gmail.com",
                    UserName = "Admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Birthday = new DateTime(1997, 06, 2),
                    Gender = Gender.NotSpecified,
                    PhoneNumber = "+375296603042"
                }
            };
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Cleaning of cottages",
                    Description = "When you live in an apartment of 40-50 sq.m.," +
                    "you can try to put things in order at least once a week. " +
                    "But if you live in your house, you will have to spend all your weekends putting things in order. " +
                    "Is it convenient? It is much easier when there is a hostess in the house who works according to a certain " +
                    "schedule (1 time a week, 2 times a week, 2 times a month). You can create such a schedule yourself, " +
                    "or you can entrust the drawing up to an experienced head of the regular cleaning department.In addition " +
                    "to the usual maintenance of order, the windows are also washed seasonally, the territory is put in order," +
                    " garages and baths are cleaned. If you are waiting for guests before the holiday, then most likely you will" +
                    " need a general cleaning. Cleaners from the special team will be able to bring global order to the house in 8-10 hours!",
                    BasePrice = 400
                },
                new Product()
                {
                    Name = "Cleaning of premises after renovation",
                    Description = "When the renovation is nearing completion," +
                    " you just want to finish faster and move. " +
                    "But how to do this if there is dust and dirt everywhere? " +
                    "The best solution, so as not to waste the last effort on cleaning, is to order cleaning." +
                    " Cleaners from the special team are trained to do difficult cleaning after renovation or construction." +
                    "The correct sequence of cleaning and knowledge of the subtle points are important here: " +
                    "safe dust removal of painted walls, safe cleaning of tiles in bathrooms without damaging chrome surfaces, " +
                    "the correct selection of special equipment for different types of floors, and much more. " +
                    "The contract must always be drawn up at the request of the customer before starting work. " +
                    "If you are refused to draw up a contract, it is likely that the cleaning representatives " +
                    "are not confident in their abilities and do not want to be held responsible for the work performed.",
                    BasePrice = 270
                },
                new Product()
                {
                    Name = "Cleaning of office premises",
                    BasePrice = 270
                },
                new Product()
                {
                    Name = "Cleaning of a 1-room apartment",
                    Description = "Do-it-yourself apartment cleaning often takes a lot of time. " +
                    "Cleaning specialists cope with this task much faster, as they work according to a well-established method. " +
                    "The necessary set of special means and equipment makes it possible to complete the work not only quickly, " +
                    "but also as efficiently as possible.Each client is interested not only in keeping the apartment clean," +
                    " but also in the fact that the employees do not spoil anything. Therefore, it is necessary that the entire responsibility " +
                    "of the cleaning company be spelled out in the contract. You are for? We, too! In fact, you get an apartment in which you will enjoy cleanliness and order," +
                    " without thinking about the safety of your property.",
                    BasePrice = 120.90m
                },
                new Product()
                {
                    Name = "Cleaning of a 2-room apartment",
                    Description = "Do-it-yourself apartment cleaning often takes a lot of time. " +
                    "Cleaning specialists cope with this task much faster, as they work according to a well-established method. " +
                    "The necessary set of special means and equipment makes it possible to complete the work not only quickly," +
                    " but also as efficiently as possible. Each client is interested not only in keeping the apartment clean, " +
                    "but also in the fact that the employees do not spoil anything. Therefore, it is necessary that the entire responsibility " +
                    "of the cleaning company be spelled out in the contract. You are for? We, too! In fact, you get an apartment in which you will enjoy cleanliness and order, " +
                    "without thinking about the safety of your property.",
                    BasePrice = 180.60m
                },
                new Product()
                {
                    Name = "Cleaning of a 3-room apartment",
                    Description = "Do-it-yourself apartment cleaning often takes a lot of time. " +
                    "Cleaning specialists cope with this task much faster, as they work according to a well-established method. " +
                    "The necessary set of special means and equipment makes it possible to complete the work not only quickly, " +
                    "but also as efficiently as possible. Each client is interested not only in keeping the apartment clean, " +
                    "but also in the fact that the employees do not spoil anything. " +
                    "Therefore, it is necessary that the entire responsibility of the cleaning company be spelled out in the contract. " +
                    "You are for? We, too! In fact, you get an apartment in which you will enjoy cleanliness and order, without thinking about the safety of your property.",
                    BasePrice = 270.50m
                },
                new Product()
                {
                    Name = "Kitchen cleaning",
                    Description = "Cleaning the entire kitchen is a full 5-6 hour cleaning of everything in the kitchen." +
                    " All equipment is washed inside and out. Wipe down all the furniture, put things in order, throw out the trash and replace the trash bags.",
                    BasePrice = 69.90m
                }
            };
        }

        private static List<OrderRequest> GetOrderRequests()
        {
            return new List<OrderRequest>()
            {
                new OrderRequest()
                {
                    Email = "testUser2@gmail.com",
                    Address = "Lesi Ukrainki 4/1, 96",
                    FIO = "Test User 2",
                    Products = GetProducts().TakeLast(3).ToList(),
                    RequestedDate = DateTime.Now
                },
                new OrderRequest()
                {
                    Email = "testUser2@gmail.com",
                    Address = "Lesi Ukrainki 4/1, 96",
                    FIO = "Test User 2",
                    Products = GetProducts().Take(3).ToList(),
                    RequestedDate = DateTime.Now
                }
            };
        }

        private static List<Order> GetOrders(List<Employee> employees)
        {
            return new List<Order>()
            {
                new Order()
                {
                    ClientId = "55c3e6d8-90a4-4b13-8f33-cdebd0c8404b",
                    OrderDate = DateTime.Now,
                    OrderRequestId = GetOrderRequests().FirstOrDefault().Id,
                    TotalPrice = 200,
                    Status = Status.Pending,
                    Products = GetProducts().TakeLast(3).ToList(),
                    ResponsibleEmployees = employees,
                    RenderedDate = DateTime.Now
                }
            };
        }
    }
}
