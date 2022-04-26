using GerenciadorRotas.APIEquipe.Util;
using GerenciadorRotasFrontEnd.Service;
using Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIEquipe.Service
{
    public class EquipeService
    {

        private readonly IMongoCollection<Equipe> _equipe;
        public EquipeService(IGrEquipeSettings settings)
        {
            var equipe = new MongoClient(settings.ConnectionString);
            var database = equipe.GetDatabase(settings.DatabaseName);
            _equipe = database.GetCollection<Equipe>(settings.GrEquipeCollectionName);
        }

        public List<Equipe> Get()
        {
            List<Equipe> equipe = new();
            equipe = _equipe.Find(person => true).ToList();
            return equipe;  
        }


        public Equipe Get(string id) =>
          _equipe.Find<Equipe>(equipe => equipe.Id == id).FirstOrDefault();

        public async Task<Equipe> Remove(string id)
        {
           

            var equipe = Get(id);
            foreach(var pessoa in equipe.Pessoa)
            {
                await PessoaAPIService.UpdateStatus(pessoa.Id);
            }

            _equipe.DeleteOne(equipe => equipe.Id == id);

            return equipe;

        }

        public List<Equipe> GetEquipesCidades(string id) =>
            _equipe.Find(equipe => equipe.Cidade.Id == id).ToList();

        public Equipe Create(Equipe equipe)
        {
            _equipe.InsertOne(equipe);
            return equipe;

        }

        public void Update(string id, Equipe equipe) =>
            _equipe.ReplaceOne(equipe => equipe.Id == id, equipe);


        public async Task<GerenciadorRotasFrontEnd.Models.Equipe> UpdateInsert(string id, GerenciadorRotasFrontEnd.Models.Pessoa updatePerson)
        {
            var seachTeam = await FrontEquipeService.GetIdEquipes(id);

            var seachPerson = await FrontPessoaService.GetIdPessoas(updatePerson.Id);

            if (seachTeam == null)
                return null;

            seachPerson.Disponivel = false;

            var filter = Builders<Equipe>.Filter.Where(team => team.Id == id);
            var update = Builders<Equipe>.Update.Push("Pessoa", seachPerson);

            await FrontPessoaService.GetIdAlterarStatus(seachPerson.Id);

            await _equipe.UpdateOneAsync(filter, update);

            return seachTeam;
        }



    }
}
