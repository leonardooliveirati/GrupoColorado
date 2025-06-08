namespace GrupoColorado.Application.DTOs
{
    public class TelefoneDto
    {
        public string NumeroTelefone { get; set; }
        public int CodigoTipoTelefone { get; set; }
        public string Operadora { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInsercao { get; set; }
        public string UsuarioInsercao { get; set; }
    }
}
