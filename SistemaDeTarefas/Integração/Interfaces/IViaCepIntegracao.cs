using SistemaDeTarefas.Integração.Response;

namespace SistemaDeTarefas.Integração.Interfaces;

public interface IViaCepIntegracao
{
    Task<ViaCepResponse> ObterDadosViaCep(string cep);
}