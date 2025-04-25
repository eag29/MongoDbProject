using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbProject.Services
{
    public class MongoDbConnection
    {
        private IMongoDatabase _database;

        public MongoDbConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017"); // MongoDB sunucusuna bağlanmak için bir istemci (client) oluşturur.
            _database = client.GetDatabase("ProductVt"); // "ProductVt" adında bir veritabanı seçilir. Eğer böyle bir veritabanı yoksa, MongoDB bunu ilk veri ekleme işleminde otomatik olarak oluşturur.
        }

        public IMongoCollection<BsonDocument> GetProductsCollection()
        {
            // GetProductsCollection(): "Products" adındaki koleksiyona (collection) erişmek için bir metot.
            return _database.GetCollection<BsonDocument>("Products"); //"Products" adındaki koleksiyona erişim sağlar.
            //IMongoCollection<BsonDocument>: MongoDB koleksiyonunu temsil eder. Burada her belge (document) BsonDocument tipinde, yani MongoDB'nin ham veri biçiminde.
        }
    }
}
