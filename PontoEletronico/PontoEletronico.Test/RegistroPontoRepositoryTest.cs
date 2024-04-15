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
    public class RegistroPontoRepositoryTest
    {
        [Fact]
        public async Task CreateAsync_ShouldAddRegistroPontoToDatabase()
        {
            // Arrange
            var registroPonto = new RegistroPonto
            {
                Id = 1,
                Tipo = TipoRegistro.Entrada,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                FuncionarioId = 1
            };

            using (var context = BuildContext())
            {
                var repository = new RegistroPontoRepository(context);

                // Act
                await repository.CreateAsync(registroPonto);
            }

            // Assert
            using (var context = BuildContext())
            {
                var createdRegistroPonto = await context.RegistroPontos.FindAsync(1);
                createdRegistroPonto.Should().NotBeNull();
                createdRegistroPonto.Tipo.Should().Be(TipoRegistro.Entrada);
                createdRegistroPonto.Data.Should().Be(DateTime.Now.Date);                
                createdRegistroPonto.FuncionarioId.Should().Be(1);
                // Cleanup            
                context.RegistroPontos.Remove(createdRegistroPonto);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRegistroPonto_WhenIdExists()
        {
            // Arrange
            var registroPonto = new RegistroPonto 
            { 
                Id = 2,
                Tipo = TipoRegistro.Entrada, 
                Data = DateTime.Now.Date, 
                Hora = DateTime.Now.TimeOfDay, 
                FuncionarioId = 1 
            };
            using (var context = BuildContext())
            {
                context.RegistroPontos.Add(registroPonto);
                context.SaveChanges();
            }

            RegistroPonto actualRegistroPonto;
            using (var context = BuildContext())
            {
                var repository = new RegistroPontoRepository(context);

                // Act
                actualRegistroPonto = await repository.GetByIdAsync(2);
            }

            // Assert
            actualRegistroPonto.Should().NotBeNull();
            actualRegistroPonto.Id.Should().Be(2);
            actualRegistroPonto.Tipo.Should().Be(TipoRegistro.Entrada);
            actualRegistroPonto.Data.Should().Be(DateTime.Now.Date);
            actualRegistroPonto.FuncionarioId.Should().Be(1);

            // Cleanup
            using (var context = BuildContext())
            {                
                context.RegistroPontos.Remove(actualRegistroPonto);                
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetByFuncionarioIdAsync_ShouldReturnRegistroPontos_WhenFuncionarioIdExists()
        {
            // Arrange
            var funcionarioId = 1;            
            var registroPonto1 = new RegistroPonto { Id = 1, Tipo = TipoRegistro.Entrada, Data = DateTime.Now.Date, Hora = DateTime.Now.TimeOfDay, FuncionarioId = funcionarioId };
            var registroPonto2 = new RegistroPonto { Id = 2, Tipo = TipoRegistro.Saida, Data = DateTime.Now.Date, Hora = DateTime.Now.TimeOfDay, FuncionarioId = funcionarioId };
            var registroPonto3 = new RegistroPonto { Id = 3, Tipo = TipoRegistro.Entrada, Data = DateTime.Now.Date.AddDays(-1), Hora = DateTime.Now.TimeOfDay, FuncionarioId = funcionarioId };
            using (var context = BuildContext())
            {
                context.RegistroPontos.AddRange(registroPonto1, registroPonto2, registroPonto3);
                context.SaveChanges();
            }

            IEnumerable<RegistroPonto> actualRegistroPontos;
            using (var context = BuildContext())
            {
                var repository = new RegistroPontoRepository(context);

                // Act
                actualRegistroPontos = await repository.GetByFuncionarioIdAsync(funcionarioId);
            }

            // Assert
            actualRegistroPontos.Should().NotBeNull();
            actualRegistroPontos.Should().HaveCount(3); // Apenas os registros do funcionário 1 que são de hoje devem ser retornados
            actualRegistroPontos.Should().OnlyContain(rp => rp.FuncionarioId == funcionarioId);

            // Cleanup
            using (var context = BuildContext())
            {
                foreach (var registroPonto in actualRegistroPontos)
                {
                    context.RegistroPontos.Remove(registroPonto);
                }
                context.SaveChanges();
            }
        }


        [Fact]
        public async Task UpdateAsync_ShouldUpdateRegistroPonto()
        {
            // Arrange
            var registroPonto = new RegistroPonto
            {
                Id = 1,
                Tipo = TipoRegistro.Entrada,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                FuncionarioId = 1
            };
            using (var context = BuildContext())
            {
                context.RegistroPontos.Add(registroPonto);
                context.SaveChanges();
            }

            using (var context = BuildContext())
            {
                var repository = new RegistroPontoRepository(context);
                registroPonto.Hora = new TimeSpan(10, 30, 0); // Atualizar o horário do registro de ponto

                // Act
                var updatedRegistroPonto = await repository.UpdateAsync(registroPonto);

                // Assert
                updatedRegistroPonto.Should().NotBeNull();
                updatedRegistroPonto.Hora.Should().Be(new TimeSpan(10, 30, 0)); // Verifica se o horário foi atualizado corretamente
                // Cleanup            
                context.RegistroPontos.Remove(updatedRegistroPonto);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetByFuncionarioIdDataAsync_ShouldReturnRegistroPontosForGivenFuncionarioIdAndData()
        {
            // Arrange
            var funcionarioId = 1;
            var data = DateTime.Now.Date;

            var registroPontos = new List<RegistroPonto>
            {
                new RegistroPonto { Id = 1, Tipo = TipoRegistro.Entrada, Data = data, Hora = new TimeSpan(8, 0, 0), FuncionarioId = funcionarioId },
                new RegistroPonto { Id = 2, Tipo = TipoRegistro.Saida, Data = data, Hora = new TimeSpan(12, 0, 0), FuncionarioId = funcionarioId },
                new RegistroPonto { Id = 3, Tipo = TipoRegistro.Entrada, Data = data.AddDays(-1), Hora = new TimeSpan(8, 0, 0), FuncionarioId = funcionarioId }
            };

            using (var context = BuildContext())
            {
                await context.RegistroPontos.AddRangeAsync(registroPontos);
                await context.SaveChangesAsync();
            }

            IEnumerable<RegistroPonto> actualRegistroPontos;

            // Act
            using (var context = BuildContext())
            {
                var repository = new RegistroPontoRepository(context);
                actualRegistroPontos = await repository.GetByFuncionarioIdDataAsync(funcionarioId, data);
            }

            // Assert
            actualRegistroPontos.Should().NotBeNull();
            actualRegistroPontos.Should().HaveCount(2);
            actualRegistroPontos.All(rp => rp.Data.Date == data).Should().BeTrue();

            // Cleanup
            using (var context = BuildContext())
            {
                foreach (var registroPonto in actualRegistroPontos)
                {
                    context.RegistroPontos.Remove(registroPonto);
                }
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
