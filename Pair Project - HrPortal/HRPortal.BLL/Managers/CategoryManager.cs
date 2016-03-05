using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.Contracts.Manager;
using HRPortal.Contracts.Repository;
using HRPortal.Data;
using HRPortal.Models;

namespace HRPortal.BLL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IPolicyRepository _polRepo;

        public CategoryManager()
        {
            _catRepo = RepositoryFactory.GetCategoryRepository();
            _polRepo = RepositoryFactory.GetPolicyRepository();
        }

        public CategoryManager(bool nUnitTest)
        {
            _catRepo = RepositoryFactory.GetCategoryRepository(nUnitTest);
            _polRepo = RepositoryFactory.GetPolicyRepository(nUnitTest);
        }

        public Response<Category> Add(Category categoryToAdd)
        {
            var response = new Response<Category>();
            try
            {
                response.Success = true;
                response.Message = "Added category.";
                response.Data = _catRepo.Add(categoryToAdd);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to add category.";
            }
            return response;
        }

        public Response<Category> Edit(int categoryId, Category categoryToEdit)
        {
            var response = new Response<Category>();
            try
            {
                response.Success = true;
                response.Message = "Edited category.";
                response.Data = _catRepo.Edit(categoryId, categoryToEdit);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to edit category.";
            }
            return response;
        }

        public Response<int> Remove(int categoryId)
        {
            var response = new Response<int>();
            try
            {
                if (_polRepo.LoadAll().Any(p => p.CategoryId == categoryId))
                {
                    _polRepo.LoadAll()
                        .Where(p => p.CategoryId == categoryId).ToList()
                        .ForEach(p =>
                        {
                            p.CategoryId = 0;
                            _polRepo.Edit(p.PolicyId, p);
                        });
                }
                response.Success = true;
                response.Message = "Removed category.";
                response.Data = _catRepo.Remove(categoryId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to remove category.";
            }
            return response;
        }

        public Response<Category> Load(int categoryId)
        {
            var response = new Response<Category>();
            try
            {
                response.Success = true;
                response.Message = "Loaded category.";
                response.Data = _catRepo.Load(categoryId);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load category.";
            }
            return response;
        }

        public Response<List<Category>> LoadAll()
        {
            var response = new Response<List<Category>>();
            try
            {
                response.Success = true;
                response.Message = "Loaded all categories.";
                response.Data = _catRepo.LoadAll().OrderBy(c => c.CategoryId).ToList();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to load all categories.";
            }
            return response;
        }
    }
}