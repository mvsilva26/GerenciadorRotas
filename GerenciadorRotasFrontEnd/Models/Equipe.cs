using System.Collections.Generic;

namespace GerenciadorRotasFrontEnd.Models
{
    public class Equipe
    {

        public string Id { get; set; }
        public string Nome { get; set; }
        public Cidade Cidade { get; set; }
        public List<Pessoa> Pessoa { get; set; }




    }
}
