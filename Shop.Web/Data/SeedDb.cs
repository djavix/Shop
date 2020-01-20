﻿using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper userHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this._context = context;
            this.userHelper = userHelper;
            this._random = new Random();
        }

        public async Task SeedAsync()
        {
            await this._context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this._context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Caracas" });
                cities.Add(new City { Name = "Pto. La Cruz" });
                cities.Add(new City { Name = "Barquisimeto" });

                this._context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Venezuela"
                });

                await this._context.SaveChangesAsync();
            }


            var user = await this.userHelper.GetUserByEmailAsync("djavix.17@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Javier",
                    LastName = "Velasquez",
                    Email = "djavix.17@gmail.com",
                    UserName = "djavix.17@gmail.com",
                    PhoneNumber = "+584242851004",
                    Address = "El Paraiso.",
                    CityId = this._context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this._context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };
                var result = await this.userHelper.AddUserAsync(user, "Abc123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!this._context.Produtcs.Any())
            {
                this.AddProduct("Motorola G8 Plus", user);
                this.AddProduct("Kit Bartender Profesional", user);
                this.AddProduct("Gin Gordon", user);
                await this._context.SaveChangesAsync();
            }


        }

        private void AddProduct(string name, User user)
        {
            this._context.Produtcs.Add(new Product
            {
                Name = name,
                Price = this._random.Next(100),
                IsAvailabe = true,
                Stock = this._random.Next(100),
                User = user
            });
        }
    }
}
