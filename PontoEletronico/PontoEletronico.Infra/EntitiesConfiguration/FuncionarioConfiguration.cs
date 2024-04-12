using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoEletronico.Domain.Entities;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Matricula).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Cargo).HasMaxLength(100).IsRequired();
        builder.Property(p => p.TipoJornada).IsRequired();

        // Relacionamento um para muitos entre Funcionario e RegistroPonto
        builder.HasMany(f => f.RegistroPontos)
            .WithOne(rp => rp.Funcionario)
            .HasForeignKey(rp => rp.FuncionarioId);
    }
}
