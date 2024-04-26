using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorio.Interfaces;

namespace SistemaDeTarefas.Repositorio;

public class TarefaRepositorio : ITarefaRepositorio
{
    private readonly SistemaDeTarefasDBContext _dbContext;
    
    public TarefaRepositorio(SistemaDeTarefasDBContext sistemaDeTarefasDbContext)
    {
        _dbContext = sistemaDeTarefasDbContext;
    }
    public async Task<List<TarefaModel>> BuscarTodasTarefas()
    {
        return await _dbContext.Tarefas.Include(x => x.Usuario).ToListAsync();
    }

    public async Task<TarefaModel> BuscarPorId(int id)
    {
        return await _dbContext.Tarefas.Include(x => x.Usuario).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
    {
        await _dbContext.Tarefas.AddAsync(tarefa);
        await _dbContext.SaveChangesAsync();

        return tarefa;
    }

    public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
    {
        TarefaModel tarefaPorID = await BuscarPorId(id);

        if (tarefaPorID == null)
        {
            throw new Exception($"Usuario para o ID:{id} não foi encontrado.");
        }

        tarefaPorID.Name = tarefa.Name;
        tarefaPorID.Descricao = tarefa.Descricao;
        tarefaPorID.Status = tarefa.Status;
        tarefaPorID.UsuarioID = tarefa.UsuarioID;

        _dbContext.Tarefas.Update(tarefaPorID);
        await _dbContext.SaveChangesAsync();

        return tarefaPorID;
    }

    public async Task<bool> Apagar(int id)
    {
        TarefaModel tarefaPorID = await BuscarPorId(id);

        if (tarefaPorID == null)
        {
            throw new Exception($"A tarefa para o ID:{id} não foi encontrado.");
        }

        _dbContext.Tarefas.Remove(tarefaPorID);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}