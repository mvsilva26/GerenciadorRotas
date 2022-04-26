using System.ComponentModel.DataAnnotations;

namespace GerenciadorRotasFrontEnd.Models
{
    public class Cidade
    {

        public string Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Estado: ")]
        public string Estado { get; set; }

    }
}
