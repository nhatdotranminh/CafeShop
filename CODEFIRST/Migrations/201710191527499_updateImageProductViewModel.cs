namespace CODEFIRST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateImageProductViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductViewModels", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductViewModels", "Image");
        }
    }
}
