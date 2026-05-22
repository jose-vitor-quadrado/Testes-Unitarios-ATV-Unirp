using System;

namespace SistemaNotificacao.Core;

public class SmsService : INotificacaoService
{
    public bool EnviarSms(string telefone, string mensagem)
    {
        Console.WriteLine($"[Rede Operadora] Transmitindo para {telefone}..."); 

        return true;
    }
}