using GerenciadorRotas.APIPessoa.Util;
using Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIPessoa.Service
{
    public class PessoaService
    {

        private readonly IMongoCollection<Pessoa> _pessoa;
        public PessoaService(IGrPessoaSettings settings)
        {
            var pessoa = new MongoClient(settings.ConnectionString);
            var database = pessoa.GetDatabase(settings.DatabaseName);
            _pessoa = database.GetCollection<Pessoa>(settings.GrPessoaCollectionName);
        }

        public List<Pessoa> Get()
        {
            List<Pessoa> pessoa = new();
            pessoa = _pessoa.Find(person => true).ToList();
            return pessoa;
        }

        public async Task<Pessoa> AtualizaStatus(string id)
        {
            var pessoa = Get(id);
            if(pessoa == null)
            {
                return null;
            }
            pessoa.Disponivel = !pessoa.Disponivel;

            _pessoa.ReplaceOne(p => p.Id == id, pessoa);

            return pessoa;
        }

        public Pessoa Get(string id) =>
          _pessoa.Find<Pessoa>(pessoa => pessoa.Id == id).FirstOrDefault();

        public void Remove(string id) =>
           _pessoa.DeleteOne(pessoa => pessoa.Id == id);

        public Pessoa Create(Pessoa pessoa)
        {
            _pessoa.InsertOne(pessoa);
            return pessoa;

        }

        public void Update(string id, Pessoa pessoa) =>
            _pessoa.ReplaceOne(pessoa => pessoa.Id == id, pessoa);



    }
}
