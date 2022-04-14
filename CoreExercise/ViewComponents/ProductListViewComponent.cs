using CoreExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoreExercise.ViewComponents
{
    // using Microsoft.AspNetCore.Mvc; => ViewComponent
    /// <summary>
    /// ProductList檢視元件(以ViewComponent結尾並繼承)
    /// </summary>
    public class ProductListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProductListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        //透過EF Core讀取資料庫, TopPricing參數是指價格前幾名
        public async Task<IViewComponentResult> InvokeAsync(int TopPricing)
        {
            var products = await _context.Products
                .OrderByDescending(p => p.Price)
                .Take(TopPricing)
                .ToListAsync();

            return View("MyProduct", products);
        }
    }
}