namespace GerenciadorRotas.APIUsuario.Util
{
    public interface IGrUsuarioSettings
    {

        public string GrUsuarioCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }


    }
}
