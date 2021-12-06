using AlunosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public interface IAlunoService //a classe abstrata herda as funções 8
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAluno(int id);
        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome);
        Task CreateAluno(Aluno aluno);
        Task UpdateAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
    }
}
