using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaRestaurant.Data.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Pizzas_PizzasId",
                table: "PizzaProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Product_ProductsId",
                table: "PizzaProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "PizzaProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "PizzasId",
                table: "PizzaProduct",
                newName: "PizzaId");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaProduct_ProductsId",
                table: "PizzaProduct",
                newName: "IX_PizzaProduct_ProductId");

            migrationBuilder.InsertData(
                table: "Doughs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Classic Neapolitan" },
                    { 2, "Whole Wheat" },
                    { 3, "Gluten-Free" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tomato Sauce" },
                    { 2, "Cheese" },
                    { 3, "Pepperoni" },
                    { 4, "Vegetables" },
                    { 5, "Pineapple" }
                });

            migrationBuilder.InsertData(
                table: "Toppings",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Cheese", 1.20m },
                    { 2, "Mushrooms", 1.30m },
                    { 3, "Olives", 1.00m },
                    { 4, "Garlic", 1.50m }
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "Description", "DoughId", "ImageUrl", "InitialPrice", "MenuId", "Name" },
                values: new object[] { 1, "Classic pizza with tomato sauce and mozzarella cheese", 1, "https://drive.google.com/file/d/1iSiFsUSFdY_1CCIL8J6tJLzNS8k-USd0/view?usp=sharing", 10.99m, null, "Margherita" });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "Description", "DoughId", "ImageUrl", "InitialPrice", "MenuId", "Name" },
                values: new object[] { 2, "Traditional pizza topped with tomato sauce and slices of pepperoni.", 2, "https://drive.google.com/file/d/1XZNR8QzYuP_6R2jAmgGtPzkphCjIJr1E/view?usp=sharing", 12.99m, null, "Pepperoni" });

            migrationBuilder.InsertData(
                table: "PizzaProduct",
                columns: new[] { "PizzaId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Pizzas_PizzaId",
                table: "PizzaProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaProduct_Product_ProductId",
                table: "PizzaProduct");

            migrationBuilder.DeleteData(
                table: "Doughs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "PizzaProduct",
                keyColumns: new[] { "PizzaId", "ProductId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doughs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doughs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PizzaProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "PizzaId",
                table: "PizzaProduct",
                newName: "PizzasId");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaProduct_ProductId",
                table: "PizzaProduct",
                newName: "IX_PizzaProduct_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaProduct_Pizzas_PizzasId",
                table: "PizzaProduct",
                column: "PizzasId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaProduct_Product_ProductsId",
                table: "PizzaProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
