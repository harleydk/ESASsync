namespace KP.Synchronization.ESAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        DiscontinuedDate = c.DateTime(),
                        Rating = c.Short(nullable: false),
                        Price = c.Double(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Supplier_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductDetails", t => t.ID)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_ID)
                .Index(t => t.ID)
                .Index(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_Street = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_ZipCode = c.String(),
                        Address_Country = c.String(),
                        Concurrency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        AirDate = c.DateTime(nullable: false),
                        FeaturedProduct_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.FeaturedProduct_ID)
                .Index(t => t.FeaturedProduct_ID);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Product_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "FeaturedProduct_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "Supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.Products", "ID", "dbo.ProductDetails");
            DropForeignKey("dbo.CategoryProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_ID", "dbo.Categories");
            DropIndex("dbo.CategoryProducts", new[] { "Product_ID" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_ID" });
            DropIndex("dbo.Advertisements", new[] { "FeaturedProduct_ID" });
            DropIndex("dbo.Products", new[] { "Supplier_ID" });
            DropIndex("dbo.Products", new[] { "ID" });
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.Advertisements");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.ProductDetails");
        }
    }
}
