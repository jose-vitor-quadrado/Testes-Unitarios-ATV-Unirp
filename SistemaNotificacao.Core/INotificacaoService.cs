namespace SistemaNotificacao.Core;

public interface INotificacaoService
{
    bool EnviarSms(string telefone, string mensagem);
}