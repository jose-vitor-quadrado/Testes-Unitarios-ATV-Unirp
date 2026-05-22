using System;

namespace SistemaNotificacao.Core;

public class LogNotificacao
{
    public int Id { get; set; }
    public string Telefone { get; set; }
    public string Mensagem { get; set; }
    public DateTime DataEnvio { get; set; }
    public bool StatusSucesso { get; set; }
}