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
        fakeService.TelefoneRecebido.Should().Be(telefoneFake);
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