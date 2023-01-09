using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Repositories;
using EcommerceApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(EcommerceAppDbContext ecommerceAppDbContext) : base(ecommerceAppDbContext)
        {
        }
    }
}
