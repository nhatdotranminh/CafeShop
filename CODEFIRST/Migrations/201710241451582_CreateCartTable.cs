namespace CODEFIRST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCartTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartModels");
            AlterColumn("dbo.CartModels", "CartID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CartModels", "CartID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CartModels");
            AlterColumn("dbo.CartModels", "CartID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CartModels", "CartID");
        }
    }
}
