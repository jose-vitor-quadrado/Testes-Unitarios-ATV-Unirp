using SistemaNotificacao.Core;

namespace SistemaNotificacao.Tests;

public class FakeNotificacaoService : INotificacaoService
{
    public bool RetornoSimulado { get; set; } = true;
    public string TelefoneRecebido { get; private set; }
    public string MensagemRecebida { get; private set; }

    public bool EnviarSms(string telefone, string mensagem)
    {
        TelefoneRecebido = telefone;
        MensagemRecebida = mensagem;
        return RetornoSimulado;
    }
}