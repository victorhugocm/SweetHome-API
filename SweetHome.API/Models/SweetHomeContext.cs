using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SweetHome.API.Models
{
    public partial class SweetHomeContext : DbContext
    {
        public SweetHomeContext()
        {
        }

        public SweetHomeContext(DbContextOptions<SweetHomeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cor> Cor { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Tamanho> Tamanho { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<VendaProduto> VendaProduto { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=sweethome;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cor>(entity =>
            {
                entity.ToTable("cor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("produto");

                entity.HasIndex(e => new { e.Descricao, e.TamanhoId, e.CorId })
                    .HasName("UK_descricao_tamanho_cor")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorId).HasColumnName("cor_id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Preco)
                    .HasColumnName("preco")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.TamanhoId).HasColumnName("tamanho_id");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.CorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cor_produto");

                entity.HasOne(d => d.Tamanho)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.TamanhoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tamanho_produto");
            });

            modelBuilder.Entity<Tamanho>(entity =>
            {
                entity.ToTable("tamanho");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.ToTable("venda");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataVenda)
                    .HasColumnName("data_venda")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ValorVenda)
                    .HasColumnName("valor_venda")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.VendedorId).HasColumnName("vendedor_id");

                entity.HasOne(d => d.Vendedor)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.VendedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_venda_vendedor");
            });

            modelBuilder.Entity<VendaProduto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("venda_produto");

                entity.HasIndex(e => new { e.ProdutoId, e.VendaId })
                    .HasName("UK_venda_produto")
                    .IsUnique();

                entity.Property(e => e.PrecoProduto)
                    .HasColumnName("preco_produto")
                    .HasColumnType("decimal(9, 2)");

                entity.Property(e => e.ProdutoId).HasColumnName("produto_id");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.VendaId).HasColumnName("venda_id");

                entity.HasOne(d => d.Produto)
                    .WithMany()
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_produto");

                entity.HasOne(d => d.Venda)
                    .WithMany()
                    .HasForeignKey(d => d.VendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_venda");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("vendedor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comissao)
                    .HasColumnName("comissao")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
