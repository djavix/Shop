using Shop.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
	public class Repository : IRepository
	{
		private readonly DataContext context;

		public Repository(DataContext context)
		{
			this.context = context;
		}

		public IEnumerable<Product> GetProducts()
		{
			return this.context.Produtcs.OrderBy(p => p.Name);
		}

		public Product GetProduct(int id)
		{
			return this.context.Produtcs.Find(id);
		}

		public void AddProduct(Product product)
		{
			this.context.Produtcs.Add(product);
		}

		public void UpdateProduct(Product product)
		{
			this.context.Update(product);
		}

		public void RemoveProduct(Product product)
		{
			this.context.Produtcs.Remove(product);
		}

		public async Task<bool> SaveAllAsync()
		{
			return await this.context.SaveChangesAsync() > 0;
		}

		public bool ProductExists(int id)
		{
			return this.context.Produtcs.Any(p => p.Id == id);
		}

	}
}
