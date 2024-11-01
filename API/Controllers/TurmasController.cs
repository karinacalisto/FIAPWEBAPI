using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public TurmasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Turma> Get()
        {
            return context.Turmas.OrderByDescending(t => t.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var turma = context.Turmas.Find(id);
            if (turma == null)
            {
                return NotFound();
            }
            return Ok(turma);
        }

        [HttpPost]
        public IActionResult CreateTurma(TurmaDto turmaDto)
        {
            var novaTurma = context.Turmas.FirstOrDefault(t => t.NomeDaTurma == turmaDto.NomeDaTurma);
            if (novaTurma != null)
            {
                ModelState.AddModelError("NomeDaTurma", "Nome da turma já cadastrado");
                var validation = new BadRequestObjectResult(ModelState);
                return BadRequest(validation);
            }

            var turma = new Turma
            {
                CursoId = turmaDto.CursoId,
                NomeDaTurma = turmaDto.NomeDaTurma,
                Ano = turmaDto.Ano
            };

            context.Turmas.Add(turma);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTurma(int id, TurmaDto turmaDto)
        {
            var turmaExistente = context.Turmas.FirstOrDefault(t => t.Id != id && t.NomeDaTurma == turmaDto.NomeDaTurma);
            if (turmaExistente != null)
            {
                ModelState.AddModelError("NomeDaTurma", "Nome da turma já cadastrado");
                var validation = new BadRequestObjectResult(ModelState);
                return BadRequest(validation);
            }

            var turma = context.Turmas.Find(id);
            if (turma == null)
            {
                return NotFound();
            }

            turma.CursoId = turmaDto.CursoId;
            turma.NomeDaTurma = turmaDto.NomeDaTurma;
            turma.Ano = turmaDto.Ano;

            context.SaveChanges();
            return Ok(turma);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurma(int id)
        {
            var turma = context.Turmas.Find(id);
            if (turma == null)
            {
                return NotFound();
            }

            context.Turmas.Remove(turma);
            context.SaveChanges();
            return Ok();
        }
    }
}
