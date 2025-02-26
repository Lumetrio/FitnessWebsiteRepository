using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortableFitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimeToDateOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // конвертация
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDateTemp",
                table: "Users",
                type: "date",
                nullable: true);   


         
            migrationBuilder.Sql(
                @"UPDATE Users 
          SET BirthDateTemp = CAST(BirthDate AS date)");

           
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

        
            migrationBuilder.RenameColumn(
                name: "BirthDateTemp",
                table: "Users",
                newName: "BirthDate");

           
            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Users",
                type: "date",
                nullable: false,  //not null
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

     
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDateTemp",
                table: "Users",
                type: "datetime2",
                nullable: true);

         
            migrationBuilder.Sql(
                @"UPDATE Users 
          SET BirthDateTemp = CAST(BirthDate AS datetime2)");

    
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

           
            migrationBuilder.RenameColumn(
                name: "BirthDateTemp",
                table: "Users",
                newName: "BirthDate");

            
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

          
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
