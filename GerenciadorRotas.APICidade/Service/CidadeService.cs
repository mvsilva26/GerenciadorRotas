using GerenciadorRotas.APICidade.Util;
using Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GerenciadorRotas.APICidade.Service
{
    public class CidadeService
    {

        private readonly IMongoCollection<Cidade> _cidade;
        public CidadeService(IGrCidadeSettings settings)
        {
            var cidade = new MongoClient(settings.ConnectionString);
            var database = cidade.GetDatabase(settings.DatabaseName);
            _cidade = database.GetCollection<Cidade>(settings.GrCidadeCollectionName);
        }

        public List<Cidade> Get()
        {
            List<Cidade> cidade = new();
            cidade = _cidade.Find(person => true).ToList();
            return cidade;
        }

        public Cidade Get(string id) =>
          _cidade.Find<Cidade>(cidade => cidade.Id == id).FirstOrDefault();

        public void Remove(string id) =>
           _cidade.DeleteOne(cidade => cidade.Id == id);

        public Cidade Create(Cidade cidade)
        {
            _cidade.InsertOne(cidade);
            return cidade;

        }

        public void Update(string id, Cidade cidade) =>
            _cidade.ReplaceOne(cidade => cidade.Id == id, cidade);






    }
}
