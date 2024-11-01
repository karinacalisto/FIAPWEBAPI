using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionamentoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public RelacionamentoController(ApplicationDbContext context)
        {
            this.context = context;
        }

       
        [HttpPost("turma/{Id}/aluno/{alunoId}")]
        public IActionResult AddAlunoToTurma(int Id, int alunoId)
        {
            var turma = context.Turmas.Find(Id);
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            var aluno = context.Alunos.Find(alunoId);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            var relacionamento = new Relacionamento
            {
                Aluno_Id = alunoId,
                NomeAluno = aluno.Nome, 
                Turma_Id = Id,
                CursoId = turma.CursoId, 
                NomeDaTurma = turma.NomeDaTurma 
            };

            context.Relacionamentos.Add(relacionamento);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("turma/{Id}/aluno/{alunoId}")]
        public IActionResult UpdateAlunoTurma(int Id, int alunoId)
        {
            var turma = context.Turmas.Find(Id);
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            var aluno = context.Alunos.Find(alunoId);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            var relacionamento = new Relacionamento
            {
                Aluno_Id = alunoId,
                NomeAluno = aluno.Nome,
                Turma_Id = Id,
                CursoId = turma.CursoId,
                NomeDaTurma = turma.NomeDaTurma
            };

            context.Relacionamentos.Add(relacionamento);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("turma/{Id}/aluno/{alunoId}")]
        public IActionResult RemoveAlunoFromTurma(int Id, int alunoId)
        {
            var turma = context.Turmas.Find(Id);
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            var aluno = context.Alunos.Find(alunoId);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            var relacionamento = context.Relacionamentos.FirstOrDefault(r => r.Aluno_Id == alunoId && r.Turma_Id == Id);
            if (relacionamento == null)
            {
                return NotFound("Relacionamento não encontrado");
            }

            context.Relacionamentos.Remove(relacionamento);
            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllRelacionamentos()
        {
            var relacionamentos = context.Relacionamentos.ToList();
            return Ok(relacionamentos);
        }
    }
}
