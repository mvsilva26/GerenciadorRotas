using GerenciadorRotas.APIUsuario.Util;
using Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GerenciadorRotas.APIUsuario.Service
{
    public class UsuarioService
    {

        private readonly IMongoCollection<Usuario> _usuario;
        public UsuarioService(IGrUsuarioSettings settings)
        {
            var usuario = new MongoClient(settings.ConnectionString);
            var database = usuario.GetDatabase(settings.DatabaseName);
            _usuario = database.GetCollection<Usuario>(settings.GrUsuarioCollectionName);
        }

        public List<Usuario> Get()
        {
            List<Usuario> usuario = new();
            usuario = _usuario.Find(person => true).ToList();
            return usuario;
        }

        public Usuario Get(string id) =>
          _usuario.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();


        public Usuario GetUserName(string loginUser) =>
          _usuario.Find<Usuario>(usuario => usuario.LoginUser == loginUser).FirstOrDefault();


        public void Remove(string id) =>
           _usuario.DeleteOne(usuario => usuario.Id == id);

        public Usuario Create(Usuario usuario)
        {
            _usuario.InsertOne(usuario);
            return usuario;

        }

        public void Update(string id, Usuario usuario) =>
            _usuario.ReplaceOne(usuario => usuario.Id == id, usuario);




    }
}
