using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaRestaurant.Data.Migrations
{
    public partial class AddMenusPizzasAndPizzasProductsDbContexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuPizza_Menus_MenuId",
                table: "MenuPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPizza_Pizzas_PizzaId",
                table: "MenuPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Pizzas_PizzaId",
                table: "PizzaProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Product_ProductId",
                table: "PizzaProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzaProduct",
                table: "PizzaProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuPizza",
                table: "MenuPizza");

            migrationBuilder.RenameTable(
                name: "PizzaProduct",
                newName: "PizzasProducts");

            migrationBuilder.RenameTable(
                name: "MenuPizza",
                newName: "MenusPizzas");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaProduct_ProductId",
                table: "PizzasProducts",
                newName: "IX_PizzasProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuPizza_MenuId",
                table: "MenusPizzas",
                newName: "IX_MenusPizzas_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzasProducts",
                table: "PizzasProducts",
                columns: new[] { "PizzaId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenusPizzas",
                table: "MenusPizzas",
                columns: new[] { "PizzaId", "MenuId" });

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSFF8PErjfcRq_lYAHhj2OrrqqTdY0FKohDA&usqp=CAU");

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT_4_lOV1P3_db6HITLwflwzROi6IZsHppD_g&usqp=CAU");

            migrationBuilder.AddForeignKey(
                name: "FK_MenusPizzas_Menus_MenuId",
                table: "MenusPizzas",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenusPizzas_Pizzas_PizzaId",
                table: "MenusPizzas",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzasProducts_Pizzas_PizzaId",
                table: "PizzasProducts",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzasProducts_Product_ProductId",
                table: "PizzasProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenusPizzas_Menus_MenuId",
                table: "MenusPizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_MenusPizzas_Pizzas_PizzaId",
                table: "MenusPizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzasProducts_Pizzas_PizzaId",
                table: "PizzasProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzasProducts_Product_ProductId",
                table: "PizzasProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzasProducts",
                table: "PizzasProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenusPizzas",
                table: "MenusPizzas");

            migrationBuilder.RenameTable(
                name: "PizzasProducts",
                newName: "PizzaProduct");

            migrationBuilder.RenameTable(
                name: "MenusPizzas",
                newName: "MenuPizza");

            migrationBuilder.RenameIndex(
                name: "IX_PizzasProducts_ProductId",
                table: "PizzaProduct",
                newName: "IX_PizzaProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MenusPizzas_MenuId",
                table: "MenuPizza",
                newName: "IX_MenuPizza_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzaProduct",
                table: "PizzaProduct",
                columns: new[] { "PizzaId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuPizza",
                table: "MenuPizza",
                columns: new[] { "PizzaId", "MenuId" });

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://drive.google.com/file/d/1iSiFsUSFdY_1CCIL8J6tJLzNS8k-USd0/view?usp=sharing");

            migrationBuilder.UpdateData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://drive.google.com/file/d/1XZNR8QzYuP_6R2jAmgGtPzkphCjIJr1E/view?usp=sharing");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPizza_Menus_MenuId",
                table: "MenuPizza",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPizza_Pizzas_PizzaId",
                table: "MenuPizza",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaProduct_Pizzas_PizzaId",
                table: "PizzaProduct",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaProduct_Product_ProductId",
                table: "PizzaProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
