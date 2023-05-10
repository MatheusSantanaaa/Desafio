﻿using ControleDeDividas.Domain.Models;
using ControleDeDividas.UnitTests.Domain.Fixtures;
using ControleDeDividas.Api.Applications.Commands.Models;
using Xunit;

namespace ControleDeDividas.UnitTests.Application.Dividas.Models
{
    public class AtualizarDividaCommandTest
    {
        private readonly DividaTestFixture _dividaTestFixture;

        public AtualizarDividaCommandTest()
        {
            _dividaTestFixture = new DividaTestFixture();
        }

        [Fact(DisplayName = "Atualizar Divida Valido")]
        [Trait("Categoria", "Application - Command - Atualizar Divida")]
        public void AtualizarDividaCommand_ComandoDeveEstaValido_Sucesso()
        {
            //Arrange
            var divida = _dividaTestFixture.GerarDividaValido();
            var parcelas = _dividaTestFixture.ObterListaParcela(divida.Id);

            var dividaCommand = new AtualizarDividaCommand(
                divida.Id,
                divida.NumeroDoTitulo,
                divida.NomeDoDevedor,
                divida.CpfDoDevedor,
                divida.Juros,
                divida.Multa,
                parcelas);

            //Act
            var result = dividaCommand.EhValido();

            //Assert
            Assert.NotNull(dividaCommand.ValidationResult);
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Divida Inválido")]
        [Trait("Categoria", "Application - Command - Atualizar Divida")]
        public void AtualizarDividaCommand_ComandoDeveEstaInvalido_NãoDevePassarNaValidacao()
        {
            //Arrange
            var divida = Divida.Factory.CriarDivida(string.Empty, string.Empty, string.Empty, 0, 0);

            var dividaCommand = new AtualizarDividaCommand(
              Guid.Empty,
              divida.NumeroDoTitulo,
              divida.NomeDoDevedor,
              divida.CpfDoDevedor,
              divida.Juros,
              divida.Multa,
              divida.Parcelas);

            //Act
            var result = dividaCommand.EhValido();


            //Assert
            Assert.False(result);
            Assert.Contains("O número do titulo deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O nome do devedor deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("A dívida deve ter pelo menos uma parcela.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O CPF do devedor deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("A multa da divida deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O juros da divida deve ser informado.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("O Id é obrigatório.", dividaCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
