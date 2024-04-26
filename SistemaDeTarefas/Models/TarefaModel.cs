using SistemaDeTarefas.Enums;

namespace SistemaDeTarefas.Models;

public class TarefaModel
{
    public int Id  { get; set; }
    public string? Name { get; set; }
    public string? Descricao { get; set; }
    public StatusTarefas Status { get; set; }
    
    public int? UsuarioID { get; set; }
    
    public virtual UsuarioModel? Usuario { get; set; }
}