using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortableFitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNormalizedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// Удаляем индекс, зависящий от NormalizedEmail
			migrationBuilder.DropIndex(
				name: "EmailIndex",
				table: "AspNetUsers");

			// Удаляем столбец NormalizedEmail
			migrationBuilder.DropColumn(
				name: "NormalizedEmail",
				table: "AspNetUsers");

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<string>(
		   name: "NormalizedEmail",
		   table: "AspNetUsers",
		   type: "nvarchar(256)",
		   maxLength: 256,
		   nullable: true);

			// Восстанавливаем индекс EmailIndex
			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");
		}
    }
}
