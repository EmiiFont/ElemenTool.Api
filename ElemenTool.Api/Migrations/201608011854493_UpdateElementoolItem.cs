namespace ElemenTool.Api.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateElementoolItem : DbMigration
    {
        public override void Up()
        {
            DropIndex("[ElemenTool.Api].ElemenToolItem", new[] { "CreatedAt" });
            DropPrimaryKey("[ElemenTool.Api].ElemenToolItem");
            AddColumn("[ElemenTool.Api].ElemenToolItem", "JwtToken", c => c.String());
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "Id", c => c.Int(nullable: false, identity: true,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: "Id", newValue: null)
                    },
                }));
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "CreatedAt", c => c.DateTimeOffset(precision: 7,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: "CreatedAt", newValue: null)
                    },
                }));
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "UpdatedAt", c => c.DateTimeOffset(precision: 7,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: "UpdatedAt", newValue: null)
                    },
                }));
            AddPrimaryKey("[ElemenTool.Api].ElemenToolItem", "Id");
            DropColumn("[ElemenTool.Api].ElemenToolItem", "Version",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "ServiceTableColumn", "Version" },
                });
            DropColumn("[ElemenTool.Api].ElemenToolItem", "Deleted",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "ServiceTableColumn", "Deleted" },
                });
        }
        
        public override void Down()
        {
            AddColumn("[ElemenTool.Api].ElemenToolItem", "Deleted", c => c.Boolean(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: null, newValue: "Deleted")
                    },
                }));
            AddColumn("[ElemenTool.Api].ElemenToolItem", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: null, newValue: "Version")
                    },
                }));
            DropPrimaryKey("[ElemenTool.Api].ElemenToolItem");
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "UpdatedAt", c => c.DateTimeOffset(precision: 7,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
                    },
                }));
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: null, newValue: "CreatedAt")
                    },
                }));
            AlterColumn("[ElemenTool.Api].ElemenToolItem", "Id", c => c.String(nullable: false, maxLength: 128,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "ServiceTableColumn",
                        new AnnotationValues(oldValue: null, newValue: "Id")
                    },
                }));
            DropColumn("[ElemenTool.Api].ElemenToolItem", "JwtToken");
            AddPrimaryKey("[ElemenTool.Api].ElemenToolItem", "Id");
            CreateIndex("[ElemenTool.Api].ElemenToolItem", "CreatedAt", clustered: true);
        }
    }
}
