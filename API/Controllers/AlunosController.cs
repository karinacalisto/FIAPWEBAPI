using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AlunosController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public List<Aluno> Get()
        {
            return context.Alunos.OrderByDescending(c => c.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = context.Alunos.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult CreateAluno(AlunoDto alunoDto)
        {

            var novoAluno = context.Alunos.FirstOrDefault(c => c.Usuario == alunoDto.Usuario);
            if (novoAluno != null)
            {
                ModelState.AddModelError("Usuario", "Usuário já cadastrado");
                var validation = new BadRequestObjectResult(ModelState);
                return BadRequest(validation);
            }

            var aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Usuario = alunoDto.Usuario,
                Senha = alunoDto.Senha
            };
            
            context.Alunos.Add(aluno); 
            context.SaveChanges(); 

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAluno(int id, AlunoDto alunoDto)
        {
            var alunoExistente = context.Alunos.FirstOrDefault(c => c.Id != id && c.Usuario == alunoDto.Usuario);
            if (alunoExistente != null)
            {
                ModelState.AddModelError("Usuario", "Usuário já cadastrado");
                var validation = new BadRequestObjectResult(ModelState);
                return BadRequest(validation);
            }

            var aluno = context.Alunos.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.Nome = alunoDto.Nome;
            aluno.Usuario = alunoDto.Usuario;
            aluno.Senha = alunoDto.Senha;

            context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletAluno(int id)
        {

            var aluno = context.Alunos.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }

            context.Alunos.Remove(aluno);
            context.SaveChanges();
            return Ok();
        }
    }
}
