using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
