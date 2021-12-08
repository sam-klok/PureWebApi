using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using PureWebApiCore.Data;
using PureWebApi.Data.Entities;

namespace PureWebApi.Data
{
    public class CampSeeder
    {
        private readonly CampContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public CampSeeder(
            CampContext ctx,
            IHostingEnvironment hosting,
            UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            // create DB is not already there
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("shawn@dutchtreat.com");

            if (user == null)
            {
                user = new StoreUser
                {
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    UserName = "shawn@dutchtreat.com",
                    Email = "shawn@dutchtreat.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!"); // default rules must be satisfied
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }

            user = await _userManager.FindByEmailAsync("sklok11@dutchtreat.com");

            if (user == null)
            {
                user = new StoreUser
                {
                    FirstName = "Sam",
                    LastName = "Klokov",
                    UserName = "sklok11@dutchtreat.com",
                    Email = "sklok11@dutchtreat.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!"); // default rules must be satisfied
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }

            if (!_ctx.Products.Any())
            {
                // need to add sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, @"Data\art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();

                _ctx.Products.AddRange(products);

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}
