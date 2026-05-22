using System;
using Xunit;
using FluentAssertions;
using Bogus;
using SistemaNotificacao.Core;

namespace SistemaNotificacao.Tests;

public class GerenciadorEnvioTests
{
    private readonly Faker _faker = new Faker("pt_BR");

    [Fact]
    public void ProcessarNotificacaoUrgente_DadosValidos_DeveRetornarSucessoEManterFormato()
    {
        // Arrange
        string telefoneFake = _faker.Phone.PhoneNumber("119########");
        string mensagemFake = _faker.Lorem.Sentence();

        var fakeService = new FakeNotificacaoService { RetornoSimulado = true };
        var gerenciador = new GerenciadorEnvio(fakeService);

        // Act
        string resultado = gerenciador.ProcessarNotificacaoUrgente(telefoneFake, mensagemFake);

        // Assert
        resultado.Should().Be("Processado com sucesso");

        fakeService.MensagemRecebida.Should().StartWith("[URGENTE]");
        fakeService.TelefoneRecebido.Should().Be(telefoneFake);
    }

    [Fact]
    public void ProcessarNotificacaoUrgente_TelefoneInvalido_DeveLancarExcecao()
    {
        // Arrange
        string telefoneInvalido = _faker.Phone.PhoneNumber(_faker.Random.AlphaNumeric(8));
        string mensagemFake = _faker.Lorem.Sentence();

        var fakeService = new FakeNotificacaoService();
        var gerenciador = new GerenciadorEnvio(fakeService);

        // Act
        Action acao = () => gerenciador.ProcessarNotificacaoUrgente(telefoneInvalido, mensagemFake);

        // Assert
        acao.Should().Throw<ArgumentException>().WithMessage("Telefone inválido.");
    }

    [Theory]
    [InlineData("", "Mensagem válida")]
    [InlineData("11999999999", "")]
    [InlineData(null, null)]
    public void ProcessarNotificacaoUrgente_DadosVazios_DeveLancarExcecao(string telefone, string mensagem)
    {
        // Arrange
        var fakeService = new FakeNotificacaoService();
        var gerenciador = new GerenciadorEnvio(fakeService);

        // Act
        Action acao = () => gerenciador.ProcessarNotificacaoUrgente(telefone, mensagem);

        // Assert
        acao.Should().Throw<ArgumentException>().WithMessage("Telefone e mensagem são obrigatórios.");
    }
}