namespace GerenciadorRotasFrontEnd.Models
{
    public class Usuario
    {

        public string Id { get; set; }
        public string LoginUser { get; set; }
        public string Senha { get; set; }
        public string NomeCompleto { get; set; }

        public string Role { get; set; } = "Usuario";




    }
}
