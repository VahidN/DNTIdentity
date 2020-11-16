using System;
using System.Collections.Generic;
using System.Linq;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIdentitySample.Services
{
    public class EfCategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Category> _categories;

        public EfCategoryService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));

            _categories = _uow.Set<Category>();
        }

        public void AddNewCategory(Category category)
        {
            _categories.Add(category);
        }

        public IList<Category> GetAllCategories()
        {
            return _categories.ToList();
        }
    }
}