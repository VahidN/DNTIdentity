using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities;

namespace ASPNETCoreIdentitySample.Services.Contracts
{
    public interface ICategoryService
    {
        void AddNewCategory(Category category);
        IList<Category> GetAllCategories();
    }
}