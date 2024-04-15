using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PontoEletronico.Domain.Entities;
using PontoEletronico.Domain.Enums;
using PontoEletronico.Infra.Data.Context;
using PontoEletronico.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PontoEletronico.Test
{
    public class FuncionarioRepositoryTests
    {        
        [Fact]
        public async Task GetByIdAsync_ShouldReturnFuncionario_WhenIdExists()
        {
            using (var context = BuildContext())
            {
                var expectedFuncionario = new Funcionario { Id = 1, Nome = "Teste" };
                context.Funcionarios.Add(expectedFuncionario);
                context.SaveChanges();
            }

            Funcionario actualFuncionario;
            using (var context = BuildContext())
            {
                var repository = new FuncionarioRepository(context);

                // Act
                actualFuncionario = await repository.GetByIdAsync(1);
            }

            // Assert
            actualFuncionario.Should().NotBeNull();
            actualFuncionario.Nome.Should().Be("Teste");
        }

        [Fact]
        public async Task CreateAsync_ShouldAddFuncionarioToDatabase()
        {
            Funcionario newFuncionario = new Funcionario { Id = 2, Nome = "Novo Funcionário" };

            // Act
            using (var context = BuildContext())
            {
                var repository = new FuncionarioRepository(context);
                await repository.CreateAsync(newFuncionario);
            }

            // Assert
            using (var context = BuildContext())
            {
                var createdFuncionario = await context.Funcionarios.FindAsync(2);
                createdFuncionario.Should().NotBeNull();
                createdFuncionario.Nome.Should().Be("Novo Funcionário");
                // Cleanup
                context.Funcionarios.Remove(createdFuncionario);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetFuncionariosAsync_ShouldReturnAllFuncionariosOrderedByIdDescending()
        {
            // Arrange
            var expectedFuncionarios = new[]
            {
                new Funcionario { Id = 3, Nome = "Funcionario1" },
                new Funcionario { Id = 4, Nome = "Funcionario2" },
                new Funcionario { Id = 5, Nome = "Funcionario3" }
            };

            using (var context = BuildContext())
            {
                context.Funcionarios.AddRange(expectedFuncionarios);
                context.SaveChanges();
            }

            IEnumerable<Funcionario> actualFuncionarios;
            using (var context = BuildContext())
            {
                var repository = new FuncionarioRepository(context);

                // Act
                actualFuncionarios = await repository.GetFuncionariosAsync();
            }

            // Assert
            actualFuncionarios.Should().NotBeNull();
            actualFuncionarios.Should().NotBeEmpty();

            // Cleanup
            using (var context = BuildContext())
            {
                foreach (var actualFuncionario in actualFuncionarios)
                {
                    context.Funcionarios.Remove(actualFuncionario);
                }
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnFuncionario_WhenUserIdExists()
        {
            // Arrange
            var userId = "user123";
            var expectedFuncionario = new Funcionario { Id = 6, Nome = "Teste", UserId = userId };

            using (var context = BuildContext())
            {
                context.Funcionarios.Add(expectedFuncionario);
                context.SaveChanges();
            }

            Funcionario actualFuncionario;
            using (var context = BuildContext())
            {
                var repository = new FuncionarioRepository(context);

                // Act
                actualFuncionario = await repository.GetByUserIdAsync(userId);
            }

            // Assert
            actualFuncionario.Should().NotBeNull();
            actualFuncionario.Nome.Should().Be("Teste");
            actualFuncionario.UserId.Should().Be(userId);
            // Cleanup
            using (var context = BuildContext())
            {
                context.Funcionarios.Remove(actualFuncionario);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateFuncionarioInDatabase()
        {
            // Arrange
            var updatedFuncionario = new Funcionario { Id = 7, Nome = "Funcionario Atualizado" };

            using (var context = BuildContext())
            {
                context.Funcionarios.Add(updatedFuncionario);
                context.SaveChanges();
            }

            // Act
            using (var context = BuildContext())
            {
                var repository = new FuncionarioRepository(context);
                updatedFuncionario.Nome = "Novo Nome";
                await repository.UpdateAsync(updatedFuncionario);
            }

            // Assert
            using (var context = BuildContext())
            {
                var funcionarioFromDb = await context.Funcionarios.FindAsync(7);
                funcionarioFromDb.Should().NotBeNull();
                funcionarioFromDb.Nome.Should().Be("Novo Nome");
                // Cleanup
                context.Funcionarios.Remove(funcionarioFromDb);
                context.SaveChanges();
            }
        }

        private ApplicationDbContext BuildContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;
            return new ApplicationDbContext(options);
        }
    }
}
