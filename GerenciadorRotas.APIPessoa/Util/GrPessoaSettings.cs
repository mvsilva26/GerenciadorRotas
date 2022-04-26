using GerenciadorRotas.APIPessoa.Util;

namespace GerenciadorRotas.APIPessoa.Service
{
    public class GrPessoaSettings : IGrPessoaSettings
    {

        public string GrPessoaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }



    }
}
