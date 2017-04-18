namespace ElemenTool.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateElementoolItem4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("[ElemenTool.Api].ElemenToolItem");
            AddColumn("[ElemenTool.Api].ElemenToolItem", "ElemenToolItemId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("[ElemenTool.Api].ElemenToolItem", "ElemenToolItemId");
            DropColumn("[ElemenTool.Api].ElemenToolItem", "Id");
        }
        
        public override void Down()
        {
            AddColumn("[ElemenTool.Api].ElemenToolItem", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("[ElemenTool.Api].ElemenToolItem");
            DropColumn("[ElemenTool.Api].ElemenToolItem", "ElemenToolItemId");
            AddPrimaryKey("[ElemenTool.Api].ElemenToolItem", "Id");
        }
    }
}
