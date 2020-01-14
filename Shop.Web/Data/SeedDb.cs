using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            this._context = context;
            this._random = new Random();
        }

        public async Task SeedAsync()
        {
            await this._context.Database.EnsureCreatedAsync();
            if (!this._context.Produtcs.Any())
            {
                this.AddProduct("First Product");
                this.AddProduct("Second Product");
                this.AddProduct("Third Product");
                await this._context.SaveChangesAsync();
            }


        }

        private void AddProduct(string name)
        {
            this._context.Produtcs.Add(new Product
            {
                Name = name,
                Price = this._random.Next(100),
                IsAvailabe = true,
                Stock = this._random.Next(100)
            });
        }
    }
}
