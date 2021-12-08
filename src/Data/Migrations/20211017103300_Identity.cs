using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace PureWebApi.Data.Migrations
{
    //20211017103300_Identity
    public class Identity : Migration
    {
        protected override void Up([NotNullAttribute] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gift",
                columns: table => new
                {
                    GiftID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gift", x => x.GiftID);
                });

            //migrationBuilder.CreateTable(
            //    name: "Gift",
            //    columns: table => new
            //    {
            //        GiftID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(nullable: false),
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Gift", x => x.GiftID);
            //    });
        }
    }
}
