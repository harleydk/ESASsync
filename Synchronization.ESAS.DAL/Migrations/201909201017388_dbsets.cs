namespace KP.Synchronization.ESAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbsets : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "esas_forventet_afslutning");
            DropColumn("dbo.People", "esas_internationalisering");
            DropColumn("dbo.People", "esas_stillingsbetegnelse");
            DropColumn("dbo.People", "esas_talentbekendtgoerelse");
            DropColumn("dbo.People", "esas_uddannelsespaalaeg");
            DropColumn("dbo.People", "esas_cpr_personsta");
            DropColumn("dbo.People", "esas_navne_addressebeesas_sammenfletskyttet");
            DropColumn("dbo.People", "esas_eidas_pid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "esas_eidas_pid", c => c.String());
            AddColumn("dbo.People", "esas_navne_addressebeesas_sammenfletskyttet", c => c.Boolean());
            AddColumn("dbo.People", "esas_cpr_personsta", c => c.String());
            AddColumn("dbo.People", "esas_uddannelsespaalaeg", c => c.Boolean());
            AddColumn("dbo.People", "esas_talentbekendtgoerelse", c => c.Boolean());
            AddColumn("dbo.People", "esas_stillingsbetegnelse", c => c.String());
            AddColumn("dbo.People", "esas_internationalisering", c => c.Boolean());
            AddColumn("dbo.People", "esas_forventet_afslutning", c => c.DateTimeOffset(precision: 7));
        }
    }
}
