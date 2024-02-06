using System;
using System.Linq;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            //Oracle,Sql,MongoDb
            _products = new List<Product> {
                new Product{ProductID=1 , CategoryId=1,ProductName ="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductID=2 , CategoryId=2,ProductName ="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductID=3 , CategoryId=3,ProductName ="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductID=4 , CategoryId=4,ProductName ="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductID=5 , CategoryId=5,ProductName ="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //LINQ 
            //Lambda
            Product productToDelete = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            _products.Remove(productToDelete);

        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllCategory(int categoryId)
        {
           return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün ,d'sine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;


        }
    }
}

