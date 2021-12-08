using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 Enquanto a pasta model possui as classes de modelo de objetos, ou seja, os atributos de um aluno (e que também
mostra as colunas que teremos nas tabelas), a pasta Services possui os métodos para que um aluno seja criado,
deletado, atualizado, buscado e etc. Já na pasta Controller, teremos as intâncias de endpoints para que
possamos utilizar os Services através dos verbos Http (GET, PUT, POST e DELETE).
 */

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet("Alunos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }
        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);

                if (alunos.Count() == 0)
                    return NotFound($"Não existem alunos com o critério {nome}");

                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }

        }

        [HttpGet("{id:int}", Name="GetAlunoId")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Não existem alunos com id {id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        //um método post deve retornar 201 (CreateAtRoute) para informar que um recurso foi criado
        [HttpPost]
        public async Task<ActionResult<Aluno>> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, [FromBody]Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return NoContent();
                }
                else
                {
                    return BadRequest("Dados de aluno Inválidos");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
    }
}
