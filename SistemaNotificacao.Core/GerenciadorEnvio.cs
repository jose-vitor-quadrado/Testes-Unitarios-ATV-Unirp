using System;

namespace SistemaNotificacao.Core;

public class GerenciadorEnvio
{
    private readonly INotificacaoService _notificacaoService;

    public GerenciadorEnvio(INotificacaoService notificacaoService)
    {
        _notificacaoService = notificacaoService;
    }

    public string ProcessarNotificacaoUrgente(string telefone, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(telefone) || string.IsNullOrWhiteSpace(mensagem))
        {
            throw new ArgumentException("Telefone e mensagem são obrigatórios");
        } 
        if (telefone.Length < 10)
        {
            throw new ArgumentException("Telefone inválido.");
        }

        string mensagemFormatada = $"[URGENTE] {mensagem}";
        bool enviado = _notificacaoService.EnviarSms(telefone, mensagemFormatada);

        return enviado ? "Processado com sucesso" : "Falha no envio";
    }
}