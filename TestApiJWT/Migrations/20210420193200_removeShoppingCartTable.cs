using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApiJWT.Migrations
{
    public partial class removeShoppingCartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_shoppingCartId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "shoppingCartId",
                table: "ShoppingCartProducts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartProducts_shoppingCartId",
                table: "ShoppingCartProducts",
                newName: "IX_ShoppingCartProducts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_AspNetUsers_UserId",
                table: "ShoppingCartProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_AspNetUsers_UserId",
                table: "ShoppingCartProducts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShoppingCartProducts",
                newName: "shoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartProducts_UserId",
                table: "ShoppingCartProducts",
                newName: "IX_ShoppingCartProducts_shoppingCartId");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    totalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_shoppingCartId",
                table: "ShoppingCartProducts",
                column: "shoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
