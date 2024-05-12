namespace BeautySoftAPI.DTOs
{
    public class UsuarioDto
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string EnderecoEmail { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
