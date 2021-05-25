using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>>  GetAll()
        {
            //İş kodları.
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll()) ;
        }
        //SELECT * FROM Categories WHERE CategoryId = 3
        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c=>c.CategoryId == categoryId));
        }
    } 
}