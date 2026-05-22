using System;
using System.Linq;

namespace SistemaNotificacao.Core;

public class Program
{
    public static void Main(string[] args)
    {
        using var db = new AppDbContext();
        var smsService = new SmsService();
        var gerenciador = new GerenciadorEnvio(smsService);

        Console.WriteLine("--- Sistema de Disparo de Notificações ---"); 
        Console.Write("Digite o número do telefone (mínimo 10 dígitos): ");
        string telefone = Console.ReadLine();

        Console.Write("Digite o texto da mensagem: "); 
        string mensagem = Console.ReadLine();

        try
        {
            string resultado = gerenciador.ProcessarNotificacaoUrgente(telefone, mensagem);
            Console.WriteLine($"Status da Operação: {resultado}");

            var log = new LogNotificacao
            {
                Telefone = telefone,
                Mensagem = mensagem,
                DataEnvio = DateTime.Now,
                StatusSucesso = resultado == "Processado com sucesso"
            };

            db.LogsNotificacoes.Add(log);
            db.SaveChanges();
            Console.WriteLine("Histórico persistido com sucesso no banco de dados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro de Validação: {ex.Message}");
        }

        Console.WriteLine("\n--- Últimos Logs Registrados no Banco ---");

        var logs = db.LogsNotificacoes.OrderByDescending(l => l.DataEnvio).Take(5).ToList();
        foreach(var item in logs)
        {
            Console.WriteLine($"[{item.DataEnvio}] Para: {item.Telefone} | Status: {item.StatusSucesso}");
        }
    }
}