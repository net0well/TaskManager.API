using Refit;
using SistemaDeTarefas.Integração.Response;

namespace SistemaDeTarefas.Integração.Refit;

public interface IViaCepIntegracaoRefit
{
    [Get("/ws/{cep}/json")]
    Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
}