using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;
using ModeMan.Ecommerce.Services.Common;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(ModeManDbContext context) : base(context)
        {
        }
    }
}
