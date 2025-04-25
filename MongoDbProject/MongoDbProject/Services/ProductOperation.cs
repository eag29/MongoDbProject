using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbProject.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbProject.Services
{
    public class ProductOperation
    {
        public List<Product> GetAllProductList()
        {
            var connection = new MongoDbConnection(); // MongoDB'ye bağlantı sağlanılır.
            var ProductCollection = connection.GetProductsCollection(); // "Products" koleksiyonuna erişilir.

            var documents = ProductCollection.Find(new BsonDocument()).ToList();
            // Find(new BsonDocument()): Tüm dokümanları (verileri) getirir.

            List<Product> products = new List<Product>();

            foreach (var doc in documents)
            {
                var product = new Product()
                {
                    ProductID = doc["_id"].ToString(),
                    ProductName = doc["ProductName"].AsString,
                    ProductPrice = doc["ProductPrice"].AsInt32,
                    ProductStock = doc["ProductStock"].AsInt32,
                    ProductStatus = doc["ProductStatus"].AsBoolean
                };
                products.Add(product);
            }

            return products;
        }
        public Product GetProductById(string ProductId)
        {
            var connection = new MongoDbConnection();
            var productCollection = connection.GetProductsCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(ProductId));
            var result = productCollection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                return new Product
                {
                    ProductID = result["_id"].ToString(),
                    ProductName = result["ProductName"].ToString(),
                    ProductPrice = result["ProductPrice"].ToInt32(),
                    ProductStock = result["ProductStock"].ToInt32(),
                    ProductStatus = result["ProductStatus"].ToBoolean()
                };
            }
            else
            {
                return null;
            }

        }
        public void AddProduct(Product product)
        {
            var connection = new MongoDbConnection();
            var productCollection = connection.GetProductsCollection();

            var document = new BsonDocument
           {
               {"ProductName", product.ProductName },
               {"ProductPrice", product.ProductPrice },
               {"ProductStock", product.ProductStock },
               {"ProductStatus", product.ProductStatus }
           };

            productCollection.InsertOne(document);
        }
        public void UpdateProduct(Product product)
        {
            var connection = new MongoDbConnection();
            var productCollection = connection.GetProductsCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(product.ProductID));
            var updatedValue = Builders<BsonDocument>
                .Update.Set("ProductName", product.ProductName).Set("ProductPrice", product.ProductPrice).Set("ProductStock", product.ProductStock).Set("ProductStatus", product.ProductStatus);
            productCollection.UpdateOne(filter, updatedValue);
        }
        public void DeleteProduct(string ProductId)
        {
            var connection = new MongoDbConnection();
            var productCollection = connection.GetProductsCollection();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(ProductId));
            productCollection.DeleteOne(filter);
        }
    }
}
