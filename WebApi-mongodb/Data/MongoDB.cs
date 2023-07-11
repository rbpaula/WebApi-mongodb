using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WebApiMongodb.Data.Collections;

namespace WebApiMongodb.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB  { get; }
        public MongoDB(IConfiguration configuration)
        {
			try
			{
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["nomeBanco"]);
                MapClasses();
			}
			catch (Exception ex)
			{

				throw new MongoException("Não foi possível conectar ao MongoBD", ex);
			}
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
