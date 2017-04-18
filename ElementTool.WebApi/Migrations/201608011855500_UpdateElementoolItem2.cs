namespace ElemenTool.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateElementoolItem2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "CreatedAt", c => c.DateTime());
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "UpdatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "CreatedAt", c => c.DateTimeOffset(precision: 7));
        }
    }
}
