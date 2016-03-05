using System.Collections.Generic;
using HRPortal.Models;

namespace HRPortal.Contracts.Manager
{
    public interface ICategoryManager
    {
        Response<Category> Add(Category categoryToAdd);
        Response<Category> Edit(int categoryId, Category categoryToEdit);
        Response<int> Remove(int categoryId);
        Response<Category> Load(int categoryId);
        Response<List<Category>> LoadAll();
    }
}