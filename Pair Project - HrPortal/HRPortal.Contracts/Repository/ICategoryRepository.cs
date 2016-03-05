using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Repository
{
    public interface ICategoryRepository
    {
        Category Add(Category categoryToAdd);
        Category Edit(int categoryId, Category categoryToEdit);
        int Remove(int categoryId);
        Category Load(int categoryId);
        List<Category> LoadAll();
    }
}