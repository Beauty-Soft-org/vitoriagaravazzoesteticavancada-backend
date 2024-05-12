namespace BeautySoftAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string EnderecoEmail { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string ConfirmSenhaHash { get; set; } = string.Empty;

    }
}
