using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sellius.API.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_logins_empresas_empresa_id",
                table: "logins");

            migrationBuilder.DropForeignKey(
                name: "fk_logins_usuarios_usuario_id",
                table: "logins");

            migrationBuilder.DropTable(
                name: "logs");

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

            migrationBuilder.DropIndex(
                name: "ix_logins_empresa_id",
                table: "logins");

            migrationBuilder.DropIndex(
                name: "ix_logins_usuario_id",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "dt_atualizacao",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "dt_cadastro",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "f_ativo",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "f_menu_exclusivo",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "id_empresa",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "id_menu_pai",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "empresa_id",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "f_email_confirmado",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "tipo_usuario",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                table: "logins");

            migrationBuilder.EnsureSchema(
                name: "Sellius");

            migrationBuilder.RenameTable(
                name: "menus",
                newName: "Menus",
                newSchema: "Sellius");

            migrationBuilder.RenameTable(
                name: "logins",
                newName: "Logins",
                newSchema: "Sellius");

            migrationBuilder.RenameColumn(
                name: "icone",
                schema: "Sellius",
                table: "Menus",
                newName: "icon");

            migrationBuilder.RenameColumn(
                name: "de_menu",
                schema: "Sellius",
                table: "Menus",
                newName: "desc_menu");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "Sellius",
                table: "Menus",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<short>(
                name: "active",
                schema: "Sellius",
                table: "Menus",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "altered_date",
                schema: "Sellius",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                schema: "Sellius",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "enterprise_id",
                schema: "Sellius",
                table: "Menus",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "menu_father_id",
                schema: "Sellius",
                table: "Menus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "hash",
                schema: "Sellius",
                table: "Logins",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                schema: "Sellius",
                table: "Logins",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "enterprise_id",
                schema: "Sellius",
                table: "Logins",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "Sellius",
                table: "Logins",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "license",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_license_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    monthly_price = table.Column<decimal>(type: "numeric", nullable: false),
                    qtde_user_free = table.Column<int>(type: "integer", nullable: false),
                    price_for_user = table.Column<decimal>(type: "numeric", nullable: false),
                    type_license = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_license", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    acronym = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    state_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_state_state_id",
                        column: x => x.state_id,
                        principalSchema: "Sellius",
                        principalTable: "States",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enterprise",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    licenca_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    license_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_enterprise", x => x.id);
                    table.ForeignKey(
                        name: "fk_enterprise_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_enterprise_license_license_id",
                        column: x => x.license_id,
                        principalTable: "license",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "GroupCustomers",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_customers", x => x.id);
                    table.ForeignKey(
                        name: "fk_group_customers_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segmentations",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_segmentations", x => x.id);
                    table.ForeignKey(
                        name: "fk_segmentations_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    neighborhood = table.Column<string>(type: "text", nullable: false),
                    complement = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suppliers", x => x.id);
                    table.ForeignKey(
                        name: "fk_suppliers_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_suppliers_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeProducts",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_type_products_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeUsers",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_type = table.Column<string>(type: "text", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_type_users_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    neighborhood = table.Column<string>(type: "text", nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    segmentation_id = table.Column<long>(type: "bigint", nullable: true),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    group_id = table.Column<long>(type: "bigint", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                    table.ForeignKey(
                        name: "fk_customers_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customers_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customers_group_customer_group_id",
                        column: x => x.group_id,
                        principalSchema: "Sellius",
                        principalTable: "GroupCustomers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_customers_segmentation_segmentation_id",
                        column: x => x.segmentation_id,
                        principalSchema: "Sellius",
                        principalTable: "Segmentations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PriceTables",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    desc_price_table = table.Column<string>(type: "text", nullable: false),
                    initial_validate_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    final_validate_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price_tables", x => x.id);
                    table.ForeignKey(
                        name: "fk_price_tables_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_price_tables_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "Sellius",
                        principalTable: "Suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    type_product_id = table.Column<long>(type: "bigint", nullable: true),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "Sellius",
                        principalTable: "Suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_type_product_type_product_id",
                        column: x => x.type_product_id,
                        principalSchema: "Sellius",
                        principalTable: "TypeProducts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TypeUserXMenus",
                schema: "Sellius",
                columns: table => new
                {
                    type_user_id = table.Column<long>(type: "bigint", nullable: false),
                    menu_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_user_x_menus", x => new { x.type_user_id, x.menu_id });
                    table.ForeignKey(
                        name: "fk_type_user_x_menus_menus_menu_id",
                        column: x => x.menu_id,
                        principalSchema: "Sellius",
                        principalTable: "Menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_user_x_menus_type_users_type_user_id",
                        column: x => x.type_user_id,
                        principalSchema: "Sellius",
                        principalTable: "TypeUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    zip_code = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    altered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tp_usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_type_users_tp_usuario_id",
                        column: x => x.tp_usuario_id,
                        principalSchema: "Sellius",
                        principalTable: "TypeUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConfigurations",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_user_id = table.Column<long>(type: "bigint", nullable: false),
                    permission_create = table.Column<bool>(type: "boolean", nullable: false),
                    permission_delete = table.Column<bool>(type: "boolean", nullable: false),
                    permission_edit = table.Column<bool>(type: "boolean", nullable: false),
                    permission_inactivate = table.Column<bool>(type: "boolean", nullable: false),
                    permission_approve = table.Column<bool>(type: "boolean", nullable: false),
                    permission_export = table.Column<bool>(type: "boolean", nullable: false),
                    permission_control_user = table.Column<bool>(type: "boolean", nullable: false),
                    type_user_id1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_configurations", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_configurations_type_users_type_user_id",
                        column: x => x.type_user_id,
                        principalSchema: "Sellius",
                        principalTable: "TypeUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_configurations_type_users_type_user_id1",
                        column: x => x.type_user_id1,
                        principalSchema: "Sellius",
                        principalTable: "TypeUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierXCustomers",
                schema: "Sellius",
                columns: table => new
                {
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_x_customers", x => new { x.customer_id, x.supplier_id });
                    table.ForeignKey(
                        name: "fk_supplier_x_customers_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "Sellius",
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_supplier_x_customers_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "Sellius",
                        principalTable: "Suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceTableXProducts",
                schema: "Sellius",
                columns: table => new
                {
                    price_table_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_price_table_x_products", x => new { x.price_table_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_price_table_x_products_price_tables_price_table_id",
                        column: x => x.price_table_id,
                        principalSchema: "Sellius",
                        principalTable: "PriceTables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_price_table_x_products_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "Sellius",
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                schema: "Sellius",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    finished = table.Column<short>(type: "smallint", nullable: false),
                    order_create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    enterprise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sale_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_sale_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "Sellius",
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_orders_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalSchema: "Sellius",
                        principalTable: "Enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_orders_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "Sellius",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderXProduct",
                schema: "Sellius",
                columns: table => new
                {
                    sale_order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    prive_seller = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sale_order_x_product", x => new { x.sale_order_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_sale_order_x_product_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "Sellius",
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_order_x_product_sale_order_sale_order_id",
                        column: x => x.sale_order_id,
                        principalSchema: "Sellius",
                        principalTable: "SaleOrders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_logins_enterprise_id",
                schema: "Sellius",
                table: "Logins",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_logins_user_id",
                schema: "Sellius",
                table: "Logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_state_id",
                table: "city",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_city_id",
                schema: "Sellius",
                table: "Customers",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_enterprise_id",
                schema: "Sellius",
                table: "Customers",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_group_id",
                schema: "Sellius",
                table: "Customers",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_customers_segmentation_id",
                schema: "Sellius",
                table: "Customers",
                column: "segmentation_id");

            migrationBuilder.CreateIndex(
                name: "ix_enterprise_city_id",
                schema: "Sellius",
                table: "Enterprise",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_enterprise_license_id",
                schema: "Sellius",
                table: "Enterprise",
                column: "license_id");

            migrationBuilder.CreateIndex(
                name: "ix_group_customers_enterprise_id",
                schema: "Sellius",
                table: "GroupCustomers",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_price_tables_enterprise_id",
                schema: "Sellius",
                table: "PriceTables",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_price_tables_supplier_id",
                schema: "Sellius",
                table: "PriceTables",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_price_table_x_products_product_id",
                schema: "Sellius",
                table: "PriceTableXProducts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_enterprise_id",
                schema: "Sellius",
                table: "Products",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_supplier_id",
                schema: "Sellius",
                table: "Products",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_type_product_id",
                schema: "Sellius",
                table: "Products",
                column: "type_product_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_orders_customer_id",
                schema: "Sellius",
                table: "SaleOrders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_orders_enterprise_id",
                schema: "Sellius",
                table: "SaleOrders",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_orders_user_id",
                schema: "Sellius",
                table: "SaleOrders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_order_x_product_product_id",
                schema: "Sellius",
                table: "SaleOrderXProduct",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_segmentations_enterprise_id",
                schema: "Sellius",
                table: "Segmentations",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_city_id",
                schema: "Sellius",
                table: "Suppliers",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_enterprise_id",
                schema: "Sellius",
                table: "Suppliers",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_supplier_x_customers_supplier_id",
                schema: "Sellius",
                table: "SupplierXCustomers",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_products_enterprise_id",
                schema: "Sellius",
                table: "TypeProducts",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_users_enterprise_id",
                schema: "Sellius",
                table: "TypeUsers",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_user_x_menus_menu_id",
                schema: "Sellius",
                table: "TypeUserXMenus",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_city_id",
                schema: "Sellius",
                table: "User",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_enterprise_id",
                schema: "Sellius",
                table: "User",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_tp_usuario_id",
                schema: "Sellius",
                table: "User",
                column: "tp_usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_configurations_type_user_id",
                schema: "Sellius",
                table: "UserConfigurations",
                column: "type_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_configurations_type_user_id1",
                schema: "Sellius",
                table: "UserConfigurations",
                column: "type_user_id1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_logins_enterprise_enterprise_id",
                schema: "Sellius",
                table: "Logins",
                column: "enterprise_id",
                principalSchema: "Sellius",
                principalTable: "Enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_logins_user_user_id",
                schema: "Sellius",
                table: "Logins",
                column: "user_id",
                principalSchema: "Sellius",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_logins_enterprise_enterprise_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "fk_logins_user_user_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.DropTable(
                name: "PriceTableXProducts",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "SaleOrderXProduct",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "SupplierXCustomers",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "TypeUserXMenus",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "UserConfigurations",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "PriceTables",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "SaleOrders",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "TypeProducts",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "GroupCustomers",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "Segmentations",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "TypeUsers",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "Enterprise",
                schema: "Sellius");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "license");

            migrationBuilder.DropTable(
                name: "States",
                schema: "Sellius");

            migrationBuilder.DropIndex(
                name: "ix_logins_enterprise_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "ix_logins_user_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "active",
                schema: "Sellius",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "altered_date",
                schema: "Sellius",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "create_date",
                schema: "Sellius",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "enterprise_id",
                schema: "Sellius",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "menu_father_id",
                schema: "Sellius",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "enterprise_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "Sellius",
                table: "Logins");

            migrationBuilder.RenameTable(
                name: "Menus",
                schema: "Sellius",
                newName: "menus");

            migrationBuilder.RenameTable(
                name: "Logins",
                schema: "Sellius",
                newName: "logins");

            migrationBuilder.RenameColumn(
                name: "icon",
                table: "menus",
                newName: "icone");

            migrationBuilder.RenameColumn(
                name: "desc_menu",
                table: "menus",
                newName: "de_menu");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "menus",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "dt_atualizacao",
                table: "menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dt_cadastro",
                table: "menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "f_ativo",
                table: "menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "f_menu_exclusivo",
                table: "menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "id_empresa",
                table: "menus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_menu_pai",
                table: "menus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "hash",
                table: "logins",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "logins",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "empresa_id",
                table: "logins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "f_email_confirmado",
                table: "logins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "salt",
                table: "logins",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "tipo_usuario",
                table: "logins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_id",
                table: "logins",
                type: "integer",
                nullable: true);

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
                    tipo_lincenca = table.Column<int>(type: "integer", nullable: false),
                    usuairos_incluir_free = table.Column<int>(type: "integer", nullable: false),
                    usuarios_incluidos = table.Column<int>(type: "integer", nullable: false),
                    valor_mensal = table.Column<decimal>(type: "numeric", nullable: false),
                    valor_por_usuario = table.Column<decimal>(type: "numeric", nullable: false),
                    dth_inicio_lincenca = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_vencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    inner_execption = table.Column<string>(type: "text", nullable: false),
                    messagem = table.Column<string>(type: "text", nullable: false),
                    dth_erro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cidades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado_id = table.Column<int>(type: "integer", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false)
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
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    licenca_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false),
                    Grupo = table.Column<string>(type: "text", nullable: false)
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
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    complemento = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
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
                    id_empresa = table.Column<int>(type: "integer", nullable: false),
                    segmento = table.Column<string>(type: "text", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    empresaid = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
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
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    documento = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    tipo_usuario = table.Column<int>(type: "integer", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    id_grupo = table.Column<int>(type: "integer", nullable: true),
                    id_segmentacao = table.Column<int>(type: "integer", nullable: true),
                    bairro = table.Column<string>(type: "text", nullable: false),
                    cep = table.Column<string>(type: "text", nullable: false),
                    documento = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    rua = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dth_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<short>(type: "smallint", nullable: false)
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
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: true),
                    tipo_produto_id = table.Column<int>(type: "integer", nullable: true),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    dth_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dth_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    f_ativo = table.Column<int>(type: "integer", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false)
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
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cliente_id = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    finalizado = table.Column<short>(type: "smallint", nullable: false),
                    dth_pedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false)
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
                    valor_venda = table.Column<float>(type: "real", nullable: false),
                    qtd = table.Column<int>(type: "integer", nullable: false)
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
                name: "ix_logins_empresa_id",
                table: "logins",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_logins_usuario_id",
                table: "logins",
                column: "usuario_id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_logins_empresas_empresa_id",
                table: "logins",
                column: "empresa_id",
                principalTable: "empresas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_logins_usuarios_usuario_id",
                table: "logins",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
