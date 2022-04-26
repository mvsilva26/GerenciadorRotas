namespace GerenciadorRotas.APICidade.Util
{
    public interface IGrCidadeSettings
    {

        public string GrCidadeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
