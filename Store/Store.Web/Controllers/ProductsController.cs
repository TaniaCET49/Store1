using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Web.Data;
using Store.Web.Data.Entities;

namespace Store.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
		{
            this.productRepository = productRepository;
        }

		// GET: Produtos
		public IActionResult Index()
		{
			return View(this.productRepository.GetAll()/*.OrderBy(p => p.Name)*/);
		}

		// GET: Produtos/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Produtos/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Produtos/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageURL,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
		{
			if (ModelState.IsValid)
			{
				await this.productRepository.CreateAsync(product);
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Produtos/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: Produtos/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageURL,LastPurchase,LastSale,IsAvailable,Stock")] Product product)
		{

			if (ModelState.IsValid)
			{
				try
				{
					await this.productRepository.UpdateAsync(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await this.productRepository.ExistsAsync(product.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Produtos/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await this.productRepository.GetByIdAsync(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Produtos/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await this.productRepository.GetByIdAsync(id);
			await this.productRepository.DeleteAsync(product);
			return RedirectToAction(nameof(Index));
		}

	}
}
