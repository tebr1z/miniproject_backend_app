﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juan.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductUpdate2Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Products");
        }
    }
}
