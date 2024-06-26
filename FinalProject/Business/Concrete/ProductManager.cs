﻿using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("admin")]

        [ValidationAspect(typeof(ProductValidator))]

        [CacheRemoveAspect("IProductService.Get")]

        [TransactionScopeAspect]

        public IResult Add(Product product)
        {
            //Business codes
            //Aynı isimde ürün eklenemez
            //max 10 bir kategoride

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryID)
                );

            if (result!=null)
            {
               return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==9)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);

            }
            //İş Kodları
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p=>p.CategoryID==id));
        }

        [CacheAspect]

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p=>p.UnitPrice >= min && p.UnitPrice <= max ));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos());
        }

        [ValidationAspect(typeof(ProductValidator))]

        [CacheRemoveAspect("IProductService.Get")]

        public IResult Update(Product product)
        {
           
            throw new NotImplementedException();
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 100)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p=>p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

       
    }
}

