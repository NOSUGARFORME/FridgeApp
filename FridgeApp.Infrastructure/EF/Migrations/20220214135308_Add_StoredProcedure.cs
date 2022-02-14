using Microsoft.EntityFrameworkCore.Migrations;

namespace FridgeApp.Infrastructure.EF.Migrations
{
    public partial class Add_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE fridges.sp_GetMissingFridgeProducts(
                                    @fridgeId uniqueidentifier    
                                    )
                                    AS
                                    BEGIN
                                        select p.*
                                        from fridges.Products p
                                            left join fridges.FridgeProducts fp on p.Id = fp.ProductId
                                        where fp.Quantity = 0 and fp.FridgeId = fridgeId
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure fridges.sp_GetMissingFridgeProducts");
        }
    }
}
