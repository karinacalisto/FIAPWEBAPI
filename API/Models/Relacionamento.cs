namespace API.Models
{
    public class Relacionamento
    {
        public int Aluno_Id { get; set; }
        public string NomeAluno { get; set; }
        public int Turma_Id { get; set; }
        public int CursoId { get; set; }
        public string NomeDaTurma { get; set; }
    }
}
