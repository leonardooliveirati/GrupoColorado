namespace GrupoColorado.Domain.Entities
{
    public class Telefone
    {
        public int CodigoCliente { get; set; }
        public string NumeroTelefone { get; set; }
        public int CodigoTipoTelefone { get; set; }
        public string Operadora { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInsercao { get; set; }
        public string UsuarioInsercao { get; set; }

        public TipoTelefone TipoTelefone { get; set; }
    }
}
