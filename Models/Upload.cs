using System.ComponentModel.DataAnnotations.Schema;

namespace Beautysoft.Models
{
    public class Upload
    {

        [NotMapped]
        public required IFormFile InserirArquivo { get; set; }
    }
}
