using Sellius.API.Models;
using Microsoft.EntityFrameworkCore;
using Sellius.API.Models.Cliente;

namespace Sellius.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CidadeModel> Cidades { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<EstadoModel> Estados { get; set; }
        public DbSet<FornecedoresModel> Fornecedores { get; set; }
        public DbSet<LicencaModel> Licencas { get; set; }
        public DbSet<LoginModel> Logins { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<PedidoXProduto> PedidoXProdutos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<TipoProdutoModel> TpProdutos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<SegmentacaoModel> Segmentacaos { get; set; }
        public DbSet<GrupoClienteModel> GrupoClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relações de tabela
            #region PedidoxProduto
            modelBuilder.Entity<PedidoXProduto>()
    .HasOne(p => p.Pedido)
    .WithMany(p => p.Produto)
    .HasForeignKey(p => p.idPedido);
            modelBuilder.Entity<PedidoXProduto>()
                .HasOne(p => p.Produto)
                .WithMany(p => p.pedidos)
                .HasForeignKey(p => p.idProduto);
            #endregion

            #region Pedido
            modelBuilder.Entity<PedidoModel>().HasOne(c => c.Cliente).WithMany(c => c.Pedidos).HasForeignKey(p => p.ClienteId);
            modelBuilder.Entity<PedidoModel>().HasOne(u => u.Usuario).WithMany(u => u.Pedidos).HasForeignKey(p => p.UsuarioId);
            modelBuilder.Entity<PedidoModel>().HasOne(e => e.Empresa).WithMany();
            #endregion

            #region Usuario
            modelBuilder.Entity<UsuarioModel>().HasOne(c => c.Cidade).WithMany().HasForeignKey(c => c.CidadeId);
            modelBuilder.Entity<UsuarioModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(u => u.EmpresaId);
            #endregion

            #region Produto
            modelBuilder.Entity<ProdutoModel>().HasOne(tp => tp.tipoProduto).WithMany(tp => tp.Produtos).HasForeignKey(p => p.TipoProdutoId);
            modelBuilder.Entity<ProdutoModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(p => p.EmpresaId);
            modelBuilder.Entity<ProdutoModel>().HasOne(f => f.Fornecedor);
            modelBuilder.Entity<TipoProdutoModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(e => e.Empresaid);
            #endregion

            #region Cliente
            modelBuilder.Entity<ClienteModel>().HasOne(c => c.Cidade).WithMany().HasForeignKey(c => c.CidadeId);
            modelBuilder.Entity<ClienteModel>().HasOne(p => p.Empresa).WithMany().HasForeignKey(c => c.EmpresaId);
            modelBuilder.Entity<ClienteModel>().HasOne(g=> g.Grupo).WithMany().HasForeignKey(g => g.idGrupo);
            modelBuilder.Entity<ClienteModel>().HasOne(s => s.segmentacao).WithMany().HasForeignKey(s => s.idSegmentacao);
            modelBuilder.Entity<GrupoClienteModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(e => e.idEmpresa).IsRequired(false);
            modelBuilder.Entity<SegmentacaoModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(e => e.idEmpresa).IsRequired(false);
            #endregion

            #region Login
            modelBuilder.Entity<LoginModel>()
                .HasOne(l => l.Usuario)
                .WithMany() // <- não define navegação reversa
                .HasForeignKey(l => l.usuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Fornecedor
            modelBuilder.Entity<FornecedoresModel>().HasOne(e => e.Empresa).WithMany().HasForeignKey(f => f.EmpresaId);
            modelBuilder.Entity<FornecedoresModel>().HasOne(e => e.Cidade).WithMany().HasForeignKey(f => f.CidadeId);
            #endregion

            modelBuilder.Entity<CidadeModel>()
                .HasOne(c => c.Estado).WithMany(e => e.Cidade)
                .HasForeignKey(c => c.EstadoId);

            #endregion
        }
    }
}
