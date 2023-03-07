using Mc_Task.DTO;
using Mc_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Mc_Task.Services
{
    public class ProductService : IProductService
    {
        ApplicationDbContext context;
        public ProductService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public List<Product> GetAll()
        {
            return context.products.ToList();
        }
        public Product GetById(int id)
        {
            return context.products.FirstOrDefault(p => p.ID == id);
        }
        public async Task<IEnumerable<Product>> Search(SearchRequest request)
        {
            IEnumerable<Product> query =await context.products.ToListAsync();

            if (request.id.HasValue)
            {
                query = query.Where(e => e.ID == request.id);
            }
            if (!string.IsNullOrEmpty(request.name))
            {
                query = query.Where(e => e.Name.Contains(request.name));
            }

            if (request.price.HasValue)
            {
                query = query.Where(e => e.Price == request.price);
            }
            if (request.quantity.HasValue)
            {
                query = query.Where(e => e.Quantity == request.quantity);
            }

            return query;
        }
        public int Insert(ProductViewModel product)
        {
            context.products.Add(new Product
            {
                ID = product.ProductId,
                Name = product.ProductName,
                Price = product.ProductPrice,
                Quantity = product.ProductQuantity,
                Description = product.Description,
                Image = product.Photo
            });
            return context.SaveChanges();
        }

        public int Update(int id, ProductViewModel product)
        {
            Product oldproduct = GetById(id);
            if (oldproduct != null)
            {
                oldproduct.Name = product.ProductName;
                oldproduct.Price = product.ProductPrice;
                oldproduct.Quantity = product.ProductQuantity;
                oldproduct.Description = product.Description;
                oldproduct.Image = product.Photo;

                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Product oldproduct = GetById(id);
            context.products.Remove(oldproduct);
            return context.SaveChanges();
        }


    }
}
