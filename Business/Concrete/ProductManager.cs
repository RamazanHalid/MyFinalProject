using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.VisualBasic;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal iProductDal, ICategoryService categoryService)
        {
            _productDal = iProductDal; 
            _categoryService = categoryService;
        }

        //[CacheAspect]// key, value
         public IDataResult<List<Product>> GetAll()
        {
            //İş kodları buraya yaszılır (if-else)
            if (DateTime.Now.Hour == 10)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);

        }

         public IDataResult<List<Product>>  GetAllByCategory(int id)
         {
             return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
         }

         
        [CacheAspect]
         public IDataResult<Product> GetBytId(int productId)
         {
             return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId)) ;
         }

         public IDataResult<List<Product>>  GetByUnitPrice(decimal min, decimal max)
         {
             return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max)) ;
             
         }

         public IDataResult<List<ProductDetailDto>>  GetProductDetails()
         {
             return  new SuccessDataResult<List<ProductDetailDto>>( _productDal.GetProductDetails());
         }
         
        
        
        
        
          [SecuredOperation("product.add")]
          [ValidationAspect(typeof(ProductValidator))]
          [CacheRemoveAspect("IProductService.Get")]
         public IResult Add(Product product)
         {
           var result =  BusinessRules.Run(ChechIfProductNameExists(product.ProductName),
                 ChechIfProductCountCategoryCorrect(product.CategoryId),
                 CheckIfCategoryLimitedExceded());

             if (result != null)
             {
                 return result;
             }
             _productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);

         }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
         public IResult Update(Product product)
         {
             throw new NotImplementedException();
         }
        [TransactionScopeAspect]
         public IResult AddTransactionalTest(Product product)
         {
             using (TransactionScope scope = new TransactionScope())
             {
                 try
                 {
                     Add(product);
                     if (product.UnitPrice < 10)
                     {
                         throw new Exception("");
                     }
                     Add(product);    
                     scope.Complete();
                 }
                 catch (Exception e)
                 {
                     scope.Dispose();
                 }
                
             
                           
             }
             
             return null;
         }

         //İş kodları parçacığı

         private IResult ChechIfProductCountCategoryCorrect(int categoryId)
         {
             var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
             if (result >= 10)
             {
                 return new ErrorResult(Messages.ProductCountOfCategoryError);
             }

             return new SuccessResult();
         }
         private IResult ChechIfProductNameExists(string productName)
         {
             var result = _productDal.GetAll(p => p.ProductName == productName).Any();
             if (result)
             {
                 return new ErrorResult(Messages.ProductNameAlreadyExists);
             }

             return new SuccessResult();
         }

         private IResult CheckIfCategoryLimitedExceded()
         {
             var result = _categoryService.GetAll();
             if (result.Data.Count>15)
             {
                 return new ErrorResult(Messages.CategoryLimitedExceded);
             }

             return new SuccessResult();
         }
    }
}