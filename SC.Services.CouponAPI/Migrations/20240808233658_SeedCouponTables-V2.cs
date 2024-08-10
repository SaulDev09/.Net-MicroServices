using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTablesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 3, "3ABCD", 4.0, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 3);
        }
    }
}
