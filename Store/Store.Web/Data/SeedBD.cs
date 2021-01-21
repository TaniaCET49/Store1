using Store.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Data
{
	public class SeedBD
	{
		private readonly DataContext context;

		private Random random;

		public SeedBD(DataContext context)
		{
			this.context = context;

			this.random = new Random();
		}

		public async Task SeedAsync()
		{
			await this.context.Database.EnsureCreatedAsync();

			if (!this.context.Products.Any())
			{

				this.AddProduct("Equipamento Oficial SLB");
				this.AddProduct("Chuteiras oficiais");
				this.AddProduct("Águia pequena");
				await this.context.SaveChangesAsync();
			}
		}

		private void AddProduct(string name)
		{
			this.context.Products.Add(new Product
			{

				Name = name,
				Price = this.random.Next(200),
				IsAvailable = true,
				Stock = this.random.Next(100)
			});
		}
	}
}