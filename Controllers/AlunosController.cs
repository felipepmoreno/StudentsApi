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

        [HttpGet]
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
                //return BadRequest("Request Inválido"); / pode ser uma alternativa, assim como a de baixo
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }
    }
}
