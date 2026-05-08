using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sellius.API.Migrations
{
    /// <inheritdoc />
    public partial class Sellius : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado = table.Column<string>(type: "text", nullable: false),
                    sigla = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "licencas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    licenca = table.Column<Guid>(type: "uuid", nullable: false),
                    dth_vencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_inicio_lincenca = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_mensal = table.Column<decimal>(type: "numeric", nullable: false),
                    usuairos_incluir_free = table.Column<int>(type: "integer", nullable: false),
                    usuarios_incluidos = table.Column<int>(type: "integer", nullable: false),
                    valor_por_usuario = table.Column<decimal>(type: "numeric", nullable: false),
                    tipo_lincenca = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_licencas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    messagem = table.Column<string>(type: "text", nullable: false),
                    inner_execption = table.Column<string>(type: "text", nullable: false),
                    dth_erro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    de_menu = table.Column<string>(type: "text", nullable: false),
                    url_menu = table.Column<string>(type: "text", nullable: false),
                    icone = table.Column<string>(type: "text", nullable: false),
                    id_menu_pai = table.Column<int>(type: "integer", nullable: false),
                    f_menu_exclusivo = table.Column<bool>(type: "boolean", nullable: false),
                    id_empresa = table.Column<int>(type: "integer", nullable: true),
                    f_ativo = table.Column<bool>(type: "boolean", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cidades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    estado_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cidades", x => x.id);
                    table.ForeignKey(
                        name: "fk_cidades_estados_estado_id",
                        column: x => x.estado_id,
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    licenca_id = table.Column<int>(type: "integer", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empresas", x => x.id);
                    table.ForeignKey(
                        name: "fk_empresas_cidades_cidade_id",
                        column: x => x.cidade_id,
                        principalTable: "cidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_empresas_licencas_licenca_id",
                        column: x => x.licenca_id,
                        principalTable: "licencas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cliente_Grupos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Grupo = table.Column<string>(type: "text", nullable: false),
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cliente_grupos", x => x.id);
                    table.ForeignKey(
                        name: "fk_cliente_grupos_empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "empresas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    complemento = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fornecedores", x => x.id);
                    table.ForeignKey(
                        name: "fk_fornecedores_cidades_cidade_id",
                        column: x => x.cidade_id,
                        principalTable: "cidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fornecedores_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segmentacaoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    segmento = table.Column<string>(type: "text", nullable: false),
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_segmentacaoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_segmentacaoes_empresas_id_empresa",
                        column: x => x.id_empresa,
                        principalTable: "empresas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tp_produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false),
                    empresaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tp_produtos", x => x.id);
                    table.ForeignKey(
                        name: "fk_tp_produtos_empresas_empresaid",
                        column: x => x.empresaid,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    documento = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_usuario = table.Column<int>(type: "integer", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuarios_cidades_cidade_id",
                        column: x => x.cidade_id,
                        principalTable: "cidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuarios_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    documento = table.Column<string>(type: "text", nullable: false),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    bairro = table.Column<string>(type: "text", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_segmentacao = table.Column<int>(type: "integer", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false),
                    id_grupo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clientes", x => x.id);
                    table.ForeignKey(
                        name: "fk_clientes_cidades_cidade_id",
                        column: x => x.cidade_id,
                        principalTable: "cidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_cliente_grupos_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "Cliente_Grupos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clientes_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_segmentacaoes_id_segmentacao",
                        column: x => x.id_segmentacao,
                        principalTable: "Segmentacaoes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    tipo_produto_id = table.Column<int>(type: "integer", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    f_ativo = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: true),
                    empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produtos", x => x.id);
                    table.ForeignKey(
                        name: "fk_produtos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_produtos_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_produtos_tp_produtos_tipo_produto_id",
                        column: x => x.tipo_produto_id,
                        principalTable: "tp_produtos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    f_email_confirmado = table.Column<bool>(type: "boolean", nullable: false),
                    tipo_usuario = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: true),
                    empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logins", x => x.id);
                    table.ForeignKey(
                        name: "fk_logins_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_logins_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    cliente_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    finalizado = table.Column<short>(type: "smallint", nullable: false),
                    dth_pedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedidos_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedidos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedidos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido_x_produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_pedido = table.Column<int>(type: "integer", nullable: false),
                    id_produto = table.Column<int>(type: "integer", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    valor_venda = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedido_x_produtos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedido_x_produtos_pedidos_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedido_x_produtos_produtos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cidades_estado_id",
                table: "cidades",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "ix_cliente_grupos_id_empresa",
                table: "Cliente_Grupos",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_cidade_id",
                table: "clientes",
                column: "cidade_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresa_id",
                table: "clientes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_id_grupo",
                table: "clientes",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_id_segmentacao",
                table: "clientes",
                column: "id_segmentacao");

            migrationBuilder.CreateIndex(
                name: "ix_empresas_cidade_id",
                table: "empresas",
                column: "cidade_id");

            migrationBuilder.CreateIndex(
                name: "ix_empresas_licenca_id",
                table: "empresas",
                column: "licenca_id");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedores_cidade_id",
                table: "fornecedores",
                column: "cidade_id");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedores_empresa_id",
                table: "fornecedores",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_logins_empresa_id",
                table: "logins",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_logins_usuario_id",
                table: "logins",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_x_produtos_id_pedido",
                table: "pedido_x_produtos",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_x_produtos_id_produto",
                table: "pedido_x_produtos",
                column: "id_produto");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_cliente_id",
                table: "pedidos",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_empresa_id",
                table: "pedidos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_usuario_id",
                table: "pedidos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_produtos_empresa_id",
                table: "produtos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_produtos_fornecedor_id",
                table: "produtos",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "ix_produtos_tipo_produto_id",
                table: "produtos",
                column: "tipo_produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_segmentacaoes_id_empresa",
                table: "Segmentacaoes",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "ix_tp_produtos_empresaid",
                table: "tp_produtos",
                column: "empresaid");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_cidade_id",
                table: "usuarios",
                column: "cidade_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_empresa_id",
                table: "usuarios",
                column: "empresa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logins");

            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "pedido_x_produtos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "tp_produtos");

            migrationBuilder.DropTable(
                name: "Cliente_Grupos");

            migrationBuilder.DropTable(
                name: "Segmentacaoes");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "cidades");

            migrationBuilder.DropTable(
                name: "licencas");

            migrationBuilder.DropTable(
                name: "estados");
        }
    }
}
