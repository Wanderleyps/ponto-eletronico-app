using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEletronico.Domain.Entities;

public class RegistroPontoConfiguration : IEntityTypeConfiguration<RegistroPonto>
{
    public void Configure(EntityTypeBuilder<RegistroPonto> builder)
    {
        builder.HasKey(rp => rp.Id);
        builder.Property(rp => rp.Data).IsRequired();
        builder.Property(rp => rp.Hora).IsRequired();

        // Configuração do relacionamento com Funcionario
        builder.HasOne(rp => rp.Funcionario)
               .WithMany(f => f.RegistroPontos)
               .HasForeignKey(rp => rp.FuncionarioId);
    }
}
