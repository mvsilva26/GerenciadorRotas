using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string LoginUser { get; set; }
        public string Senha { get; set; }
        public string NomeCompleto { get; set; }

        public string Role { get; set; } = "Usuario";


    }
}
