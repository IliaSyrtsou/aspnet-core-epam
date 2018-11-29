using Northwind.Repository;
using Northwind.Entities;
using Northwind.Services.Interfaces;
using System.Linq;
using System.IO;
using Northwind.Common.Helpers;
using System;

namespace Northwind.Services
{
    public class CategoryService: BaseService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repo, IUnitOfWork uow)
            : base(repo, uow) {}

        public void UpdateCategoryImage(int categoryId, byte[] image) {
            var category = GetAll().FirstOrDefault(x => x.CategoryId.Equals(categoryId));

            var randomBytes = RandomizeHelper.GenerateBufferFromSeed(78);
            var pictureBytes = new byte[randomBytes.Length + image.Length];
            Buffer.BlockCopy(randomBytes, 0, pictureBytes, 0, randomBytes.Length);
            Buffer.BlockCopy(image, 0, pictureBytes, randomBytes.Length, image.Length);
            category.Picture = pictureBytes;
            
            Update(category);
        }


    }
}