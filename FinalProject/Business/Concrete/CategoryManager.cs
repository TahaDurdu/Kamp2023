﻿using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IDataResult<List<Category>> GetAll()
        {
            _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>();
        }

        public IDataResult<Category> GetById(int categoryId)
        {
             _categoryDal.Get(c => c.CategoryID == categoryId);
            return new SuccessDataResult<Category> ();
        }
    }
}

