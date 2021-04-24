namespace KP.Synchronization.ESAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foobar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryProducts", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.CategoryProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "ID", "dbo.ProductDetails");
            DropForeignKey("dbo.Products", "Supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.Advertisements", "FeaturedProduct_ID", "dbo.Products");
            DropIndex("dbo.Products", new[] { "ID" });
            DropIndex("dbo.Products", new[] { "Supplier_ID" });
            DropIndex("dbo.Advertisements", new[] { "FeaturedProduct_ID" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_ID" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_ID" });
            CreateTable(
                "dbo.Uddannelses",
                c => new
                    {
                        esas_uddannelseId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        StateCode = c.Int(),
                        esas_navn = c.String(),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_afsluttet_inden_interval = c.Int(),
                        esas_antal_semestre = c.Int(),
                        esas_delformaal = c.Int(),
                        esas_ds_ordinaer = c.Boolean(),
                        esas_ds_aau = c.Boolean(),
                        esas_ects_min = c.Decimal(precision: 18, scale: 2),
                        esas_ects_max = c.Decimal(precision: 18, scale: 2),
                        esas_ects_samlet = c.Decimal(precision: 18, scale: 2),
                        esas_faerdiggoerelse = c.Boolean(),
                        esas_ikrafttraedelsesdato = c.DateTimeOffset(precision: 7),
                        esas_internationalisering = c.Boolean(),
                        esas_normeret_tid = c.Decimal(precision: 18, scale: 2),
                        esas_opgoerelsesmetode = c.Int(),
                        esas_ophoersdato = c.DateTimeOffset(precision: 7),
                        esas_optag_og_budgetteret_optag = c.Boolean(),
                        esas_ordinaer_staa = c.Boolean(),
                        esas_soefartsstyrelsen = c.Boolean(),
                        esas_su = c.Boolean(),
                        esas_titel = c.String(),
                        esas_title_of_qualification = c.String(),
                        esas_aarstal = c.String(),
                        esas_aau_staa = c.Boolean(),
                        esas_aktivitetsgruppekode_id = c.Guid(),
                        esas_aktivitetsgruppekode_idName = c.String(),
                        esas_dansk_oversaettelse_id = c.Guid(),
                        esas_dansk_oversaettelse_idName = c.String(),
                        esas_engelsk_oversaettelse_id = c.Guid(),
                        esas_engelsk_oversaettelse_idName = c.String(),
                        esas_su_kode_id = c.Guid(),
                        esas_su_kode_idName = c.String(),
                        esas_uddannelsestype = c.Int(),
                        esas_uddannelsestype_idName = c.String(),
                        esas_ds_uddannelseskode_id = c.Guid(),
                        esas_ds_uddannelseskode_idName = c.String(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseId);
            
            CreateTable(
                "dbo.Praktikopholds",
                c => new
                    {
                        esas_praktikopholdId = c.Guid(nullable: false),
                        esas_fartstid = c.Int(),
                        esas_loennet = c.Boolean(),
                        esas_navn = c.String(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_type = c.Int(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_praktiksted_id = c.Guid(),
                        esas_praktikomraade_id = c.Guid(),
                        esas_praktikvejleder_id = c.Guid(),
                        StateCode = c.Int(),
                        StatusCode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_studieforloeb_esas_studieforloebId = c.Guid(),
                        esas_praktikvejleder_esas_personid = c.String(maxLength: 128),
                        esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId = c.Guid(),
                        esas_praktikomraade_esas_praktikomraadeId = c.Guid(),
                        esas_praktiksted_esas_vist_institutionsoplysning_id = c.Guid(),
                        Uddannelse_esas_uddannelseId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_praktikopholdId)
                .ForeignKey("dbo.Studieforloebs", t => t.esas_studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.People", t => t.esas_praktikvejleder_esas_personid)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselements", t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Praktikomraades", t => t.esas_praktikomraade_esas_praktikomraadeId)
                .ForeignKey("dbo.InstitutionVirksomheds", t => t.esas_praktiksted_esas_vist_institutionsoplysning_id)
                .ForeignKey("dbo.Uddannelses", t => t.Uddannelse_esas_uddannelseId)
                .Index(t => t.esas_studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_praktikvejleder_esas_personid)
                .Index(t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .Index(t => t.esas_praktikomraade_esas_praktikomraadeId)
                .Index(t => t.esas_praktiksted_esas_vist_institutionsoplysning_id)
                .Index(t => t.Uddannelse_esas_uddannelseId);
            
            CreateTable(
                "dbo.GennemfoerelsesUddannelseselements",
                c => new
                    {
                        esas_uddannelseselement_gennemfoerelseId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        esas_bedoemmelsesresultat_7_trin = c.Int(),
                        esas_bedoemmelsesresultat_bestaaet_ikke = c.Int(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_beregnet_bestaaet = c.Boolean(),
                        esas_antal_forsoeg = c.Int(),
                        esas_antal_merit_registreringer = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_registeret_merit = c.Boolean(),
                        esas_semester_nummer = c.Int(nullable: false),
                        esas_bevisgrundlag_id = c.Guid(),
                        esas_bevisgrundlag_idName = c.String(),
                        esas_uvm_fagkode = c.Int(),
                        esas_pue_id = c.Guid(),
                        esas_uddannelseselement_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_hold_id = c.Guid(),
                        esas_bedoemmelsesresultat_id = c.Guid(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_hold_esas_holdId = c.Guid(),
                        esas_bevisgrundlag_esas_bevisgrundlagId = c.Guid(),
                        esas_uddannelseselement_esas_uddannelseselementId = c.Guid(),
                        esas_studieforloeb_esas_studieforloebId = c.Guid(),
                        esas_pue_esas_adgangskrav_id = c.Guid(),
                        esas_bedoemmelsesresultat_esas_karakterId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Holds", t => t.esas_hold_esas_holdId)
                .ForeignKey("dbo.Bevisgrundlags", t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_uddannelseselement_esas_uddannelseselementId)
                .ForeignKey("dbo.Studieforloebs", t => t.esas_studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_pue_esas_adgangskrav_id)
                .ForeignKey("dbo.Karakters", t => t.esas_bedoemmelsesresultat_esas_karakterId)
                .Index(t => t.esas_hold_esas_holdId)
                .Index(t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .Index(t => t.esas_uddannelseselement_esas_uddannelseselementId)
                .Index(t => t.esas_studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_pue_esas_adgangskrav_id)
                .Index(t => t.esas_bedoemmelsesresultat_esas_karakterId);
            
            CreateTable(
                "dbo.Karakters",
                c => new
                    {
                        esas_karakterId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_name = c.String(),
                        esas_bestaaet = c.Boolean(nullable: false),
                        esas_karakterskala = c.Int(),
                        esas_karakter = c.String(),
                        esas_oversaettelse = c.String(),
                        esas_etcs_skala = c.String(),
                        esas_taeller_som_forsoeg = c.Boolean(nullable: false),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_karakterId);
            
            CreateTable(
                "dbo.Bedoemmelses",
                c => new
                    {
                        esas_bedoemmelseId = c.Guid(nullable: false),
                        esas_bedoemmelses_nummer = c.String(),
                        esas_bedmmelses_nummer_bool = c.Boolean(),
                        esas_bedoemt_ikke_afmeldt = c.Boolean(),
                        esas_taeller_som_forsoeg = c.Boolean(),
                        esas_loebenummer = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_redigeringsgrund = c.Int(),
                        esas_titel = c.String(),
                        esas_engelsk_titel = c.String(),
                        esas_navn = c.String(),
                        esas_karakterskala = c.Int(),
                        esas_oevrige_skala = c.Int(),
                        esas_bedoemmelsesdato = c.DateTimeOffset(precision: 7),
                        esas_bedoemmelsestype = c.Int(),
                        esas_bestaaet = c.Boolean(),
                        esas_evaluering = c.String(),
                        esas_bedoemmelsesrunde_id = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_karakter_id = c.Guid(),
                        esas_sprog_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_bedoemmelsesform_mundtlig = c.Boolean(),
                        esas_bedoemmelsesform_skriftlig = c.Boolean(),
                        esas_bedoemmelsesform_praktisk_proeve = c.Boolean(),
                        esas_bedoemmelsesform_projekt = c.Boolean(),
                        esas_bedoemmelsesform_gennemfoerelse = c.Boolean(),
                        esas_bedoemmelsesform_kmb = c.Boolean(),
                        esas_bedoemmelsesform_multiple_choice_test = c.Boolean(),
                        esas_bedoemmelsesform_realkompetencevurdering = c.Boolean(),
                        esas_bedoemmelse_godkendt_af_id = c.Guid(),
                        esas_bedoemmelse_registreret_af_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_bedoemmelse_godkendt_af_SystemUserId = c.Guid(),
                        esas_bedoemmelse_registreret_af_SystemUserId = c.Guid(),
                        esas_bedoemmelsesrunde_esas_bedoemmelsesrundeId = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId = c.Guid(),
                        esas_karakter_esas_karakterId = c.Guid(),
                        esas_studieforloeb_esas_studieforloebId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelseId)
                .ForeignKey("dbo.SystemUsers", t => t.esas_bedoemmelse_godkendt_af_SystemUserId)
                .ForeignKey("dbo.SystemUsers", t => t.esas_bedoemmelse_registreret_af_SystemUserId)
                .ForeignKey("dbo.Bedoemmelsesrundes", t => t.esas_bedoemmelsesrunde_esas_bedoemmelsesrundeId)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselements", t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Karakters", t => t.esas_karakter_esas_karakterId)
                .ForeignKey("dbo.Studieforloebs", t => t.esas_studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_bedoemmelse_godkendt_af_SystemUserId)
                .Index(t => t.esas_bedoemmelse_registreret_af_SystemUserId)
                .Index(t => t.esas_bedoemmelsesrunde_esas_bedoemmelsesrundeId)
                .Index(t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .Index(t => t.esas_karakter_esas_karakterId)
                .Index(t => t.esas_studieforloeb_esas_studieforloebId);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        SystemUserId = c.Guid(nullable: false),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.SystemUserId);
            
            CreateTable(
                "dbo.Bedoemmelsesrundes",
                c => new
                    {
                        esas_bedoemmelsesrundeId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_nummer = c.String(),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_planlaegningsuddannelseselement_esas_adgangskrav_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelsesrundeId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id)
                .Index(t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id);
            
            CreateTable(
                "dbo.PlanlaegningsUddannelseselements",
                c => new
                    {
                        esas_adgangskrav_id = c.Guid(nullable: false),
                        esas_uddannelseselement_planlaegningId = c.Guid(nullable: false),
                        esas_adresselinje_1 = c.String(),
                        esas_adresselinje_2 = c.String(),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_tilmeldingsfrist = c.DateTimeOffset(precision: 7),
                        esas_antal_uddannelseselement_gennemfoerelse = c.Int(),
                        esas_karakterskala = c.Int(),
                        esas_loebenr = c.String(),
                        esas_logisk_startdato = c.DateTimeOffset(precision: 7),
                        esas_pue_id = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_navn = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_uddannelseselement_id = c.Guid(),
                        esas_uddannelse_id = c.Guid(),
                        esas_sprog_id = c.Guid(),
                        esas_sprog_idName = c.String(),
                        esas_semester_modul_id = c.Guid(),
                        esas_gruppering_id = c.Guid(),
                        esas_postnummer_by_idName = c.String(),
                        esas_samlaesning_id = c.Guid(),
                        esas_fordeling = c.Int(),
                        esas_fordelingsnoegle_min = c.Int(),
                        esas_fordelingsnoegle_max = c.Int(),
                        esas_fordelingsnoegle_prio_1 = c.Int(),
                        esas_fordeling_prio_1 = c.Boolean(),
                        esas_fordelingsnoegle_prio_2 = c.Int(),
                        esas_fordeling_prio_2 = c.Boolean(),
                        esas_bedoemmelsesform_mundtlig = c.Boolean(),
                        esas_bedoemmelsesform_skriftlig = c.Boolean(),
                        esas_bedoemmelsesform_praktisk_proeve = c.Boolean(),
                        esas_bedoemmelsesform_projekt = c.Boolean(),
                        esas_bedoemmelsesform_gennemfoerelse = c.Boolean(),
                        esas_bedoemmelsesform_kmb = c.Boolean(),
                        esas_bedoemmelsesform_multiple_choice_test = c.Boolean(),
                        esas_bedoemmelsesform_realkompetencevurdering = c.Boolean(),
                        esas_bedoemmelsestype = c.Int(),
                        esas_praktiske_oplysninger_publicering = c.String(),
                        esas_publiceringsperiode_fra = c.DateTimeOffset(precision: 7),
                        esas_publiceringsperiode_til = c.DateTimeOffset(precision: 7),
                        esas_publiceringsmulighed = c.Int(),
                        esas_aflysningsaarsag = c.Int(),
                        esas_undervisningsform = c.Int(),
                        esas_undervisning_ugedag = c.String(),
                        esas_sluttidspunkt = c.String(),
                        esas_starttidspunkt = c.String(),
                        esas_maximum_antal_deltagere = c.Int(),
                        esas_minimum_antal_deltagere = c.Int(),
                        esas_tilmeldingslink = c.String(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        StruktureltUddannelseselement_esas_uddannelseselementId = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId1 = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId2 = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                        Studieforloeb_esas_studieforloebId1 = c.Guid(),
                        esas_postnummer_by_esas_postnummerId = c.Guid(),
                        esas_gruppering_esas_uddannelseselementId = c.Guid(),
                        esas_publicering_esas_publiceringid = c.Guid(),
                        esas_samlaesning_esas_uddannelseselementId = c.Guid(),
                        esas_semester_modul_esas_uddannelseselementId = c.Guid(),
                        esas_uddannelse_esas_uddannelseId = c.Guid(),
                        esas_uddannelseselement_esas_uddannelseselementId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .ForeignKey("dbo.Studieforloebs", t => t.Studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.Studieforloebs", t => t.Studieforloeb_esas_studieforloebId1)
                .ForeignKey("dbo.Postnummers", t => t.esas_postnummer_by_esas_postnummerId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_gruppering_esas_uddannelseselementId)
                .ForeignKey("dbo.Publicerings", t => t.esas_publicering_esas_publiceringid)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_samlaesning_esas_uddannelseselementId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_semester_modul_esas_uddannelseselementId)
                .ForeignKey("dbo.Uddannelses", t => t.esas_uddannelse_esas_uddannelseId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_uddannelseselement_esas_uddannelseselementId)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId1)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId2)
                .Index(t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.Studieforloeb_esas_studieforloebId1)
                .Index(t => t.esas_postnummer_by_esas_postnummerId)
                .Index(t => t.esas_gruppering_esas_uddannelseselementId)
                .Index(t => t.esas_publicering_esas_publiceringid)
                .Index(t => t.esas_samlaesning_esas_uddannelseselementId)
                .Index(t => t.esas_semester_modul_esas_uddannelseselementId)
                .Index(t => t.esas_uddannelse_esas_uddannelseId)
                .Index(t => t.esas_uddannelseselement_esas_uddannelseselementId);
            
            CreateTable(
                "dbo.Adgangskravs",
                c => new
                    {
                        esas_adgangskravId = c.Guid(nullable: false),
                        esas_adgangsgrundlag = c.String(),
                        esas_ansoegning_med_saerlig_tilladelse_muligt = c.Int(),
                        esas_dispensation_for_adgangskrav_muligt = c.Int(),
                        esas_navn = c.String(),
                        esas_saerlige_krav = c.Boolean(),
                        esas_saerlige_krav_beskrivelse = c.String(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_planlaegningsUddannelseselement_id = c.Guid(),
                        esas_struktureltUddannelseselement_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.esas_adgangskravId)
                .ForeignKey("dbo.Omraadenummeropsaetnings", t => t.esas_adgangskravId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_adgangskravId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_adgangskravId)
                .Index(t => t.esas_adgangskravId);
            
            CreateTable(
                "dbo.Omraadenummeropsaetnings",
                c => new
                    {
                        esas_omraadeopsaetningid = c.Guid(nullable: false),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_dimensionering_sommer = c.Int(),
                        esas_dimensionering_vinter = c.Int(),
                        esas_institutionsafdeling_id = c.Guid(),
                        esas_kategori = c.Int(),
                        esas_kot_grupper = c.String(),
                        esas_min_kvotient = c.Decimal(precision: 18, scale: 2),
                        esas_navn = c.String(),
                        esas_omraadenummer_id = c.Guid(),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_studiestart_sommer = c.DateTimeOffset(precision: 7),
                        esas_studiestart_vinter = c.DateTimeOffset(precision: 7),
                        esas_type = c.Int(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_adgangskrav_id = c.Guid(),
                        esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                        esas_institutionsafdeling_esas_afdelingId = c.Guid(),
                        esas_publicering_esas_publiceringid = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_omraadeopsaetningid)
                .ForeignKey("dbo.Ansoegningsopsaetnings", t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .ForeignKey("dbo.Afdelings", t => t.esas_institutionsafdeling_esas_afdelingId)
                .ForeignKey("dbo.Publicerings", t => t.esas_publicering_esas_publiceringid)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.esas_institutionsafdeling_esas_afdelingId)
                .Index(t => t.esas_publicering_esas_publiceringid)
                .Index(t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId);
            
            CreateTable(
                "dbo.Ansoegningsopsaetnings",
                c => new
                    {
                        esas_ansoegningsopsaetningId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_optag_dk_optag = c.Boolean(),
                        esas_optagelsesperiode_slut = c.DateTimeOffset(precision: 7),
                        esas_optagelsesperiode_start = c.DateTimeOffset(precision: 7),
                        ownerid = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_klargjort = c.Boolean(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningsopsaetningId);
            
            CreateTable(
                "dbo.GymnasielleFagkravs",
                c => new
                    {
                        esas_gymnasielle_fagkravId = c.Guid(nullable: false),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_fagkrav = c.String(),
                        esas_omraadenummer_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_fagkravId)
                .ForeignKey("dbo.Ansoegningsopsaetnings", t => t.Ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.Ansoegningsopsaetning_esas_ansoegningsopsaetningId);
            
            CreateTable(
                "dbo.GymnasielleKarakterkravs",
                c => new
                    {
                        esas_gymnasielle_karakterkravid = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_fag = c.String(),
                        esas_karakterkrav = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_omraadenummer_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_karakterkravid)
                .ForeignKey("dbo.Ansoegningsopsaetnings", t => t.Ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.Ansoegningsopsaetning_esas_ansoegningsopsaetningId);
            
            CreateTable(
                "dbo.Ansoegnings",
                c => new
                    {
                        esas_ansoegningId = c.Guid(nullable: false),
                        esas_afslagsbegrundelse = c.String(),
                        esas_afslagsbegrundelsestype_id = c.Guid(),
                        esas_afslagsbegrundelsestype_idname = c.String(),
                        esas_ansoeger_id = c.Guid(),
                        esas_ansoeger_kendt_i_esas = c.Boolean(),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_ansoegningstype = c.Int(),
                        esas_ansoegt_dato = c.DateTimeOffset(precision: 7),
                        esas_betingelser = c.String(),
                        esas_betinget_optaget = c.Boolean(),
                        esas_dobbelt_uddannelse = c.Boolean(),
                        esas_evu_idv_aktivitet = c.String(),
                        esas_foretraekker_vinter = c.Boolean(),
                        esas_groenlandsk_saerordning = c.Boolean(),
                        esas_kilde = c.Int(),
                        esas_kotid = c.String(),
                        esas_omraadenummer_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_person_studerende_id = c.Guid(),
                        esas_planlaegningselement_id = c.Guid(),
                        esas_virksomhed_id = c.Guid(),
                        esas_sagsbehandlet_af_id = c.Guid(),
                        esas_standby = c.Boolean(),
                        esas_status_aendret_af_id = c.Guid(),
                        esas_status_dato = c.DateTimeOffset(precision: 7),
                        esas_studieretning = c.String(),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_tidligere_fuldfoert_videregaaende_udd = c.Boolean(),
                        esas_tilsagn = c.Boolean(),
                        esas_type = c.Int(),
                        esas_undervisningssted = c.String(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_integrationsstatus = c.Int(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_opfoelgningsdato = c.DateTimeOffset(precision: 7),
                        esas_opfylder_betingelser = c.Boolean(),
                        esas_optagelsesafgoerelse = c.Int(),
                        esas_optagelsesstatus = c.Int(),
                        esas_optagelsesstatus_dato = c.DateTimeOffset(precision: 7),
                        esas_relaterede_poster_aendret = c.DateTimeOffset(precision: 7),
                        esas_sagsbehandlingsafgoerelse = c.Int(),
                        esas_sagsbehandlingsstatus = c.Int(),
                        esas_prioritet = c.Int(),
                        esas_tildelt_studiestart = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_gs_i_gang = c.Boolean(),
                        esas_gs_i_gang_fag = c.String(),
                        esas_gs_tilmeldt = c.Boolean(),
                        esas_gs_tilmeldt_fag = c.String(),
                        esas_st_ansoegning_med_saerlig_tilladelse = c.Boolean(),
                        esas_st_bemaerkninger = c.String(),
                        esas_st_dato = c.DateTimeOffset(precision: 7),
                        esas_st_journalnummer = c.String(),
                        esas_st_tidligere_saerlig_tilladelse = c.Boolean(),
                        esas_sb_adgangskrav_opfyldt = c.Boolean(),
                        esas_sb_alle_bilag_modtaget = c.Boolean(),
                        esas_sb_ansoegning_om_dispensation_dobb_udd = c.Int(),
                        esas_sb_bemaerkninger = c.String(),
                        esas_sb_bestaaede_fag_godkendt = c.Boolean(),
                        esas_sb_dokumentation_for_erhvervserfaring = c.Int(),
                        esas_sb_dokumentation_for_uddannelse = c.Int(),
                        esas_frist_manglende_dokumentation = c.DateTimeOffset(precision: 7),
                        esas_sb_efteroptag = c.Boolean(),
                        esas_sb_eksamensbevis = c.Int(),
                        esas_sb_faerdigbehandlet_dato = c.DateTimeOffset(precision: 7),
                        esas_sb_forhaandsoptaget = c.Boolean(),
                        esas_sb_motiveret_ansoegning = c.Int(),
                        esas_sb_obligatorisk_merit = c.String(),
                        esas_rykker_sendt = c.Boolean(),
                        esas_institution_id = c.Guid(),
                        esas_integrationsfelt_1_definition = c.String(),
                        esas_integrationsfelt_2_definition = c.String(),
                        esas_integrationsfelt_3_definition = c.String(),
                        esas_integrationsfelt_4_definition = c.String(),
                        esas_integrationsfelt_5_definition = c.String(),
                        esas_integrationsfelt_1_indhold = c.String(),
                        esas_integrationsfelt_2_indhold = c.String(),
                        esas_integrationsfelt_3_indhold = c.String(),
                        esas_integrationsfelt_4_indhold = c.String(),
                        esas_integrationsfelt_5_indhold = c.String(),
                        esas_me_bemaerkninger = c.String(),
                        esas_me_dokumentation = c.Int(),
                        esas_me_erhvervserfaring = c.Boolean(),
                        esas_me_uddannelse = c.Boolean(),
                        esas_startmerit = c.Boolean(),
                        esas_baggrund_for_merit = c.String(),
                        esas_ag_adgangsgrundlag = c.Int(),
                        esas_ag_danskproeve = c.Boolean(),
                        esas_ag_danskproeve_aar = c.String(),
                        esas_ag_danskproeve_niveau = c.Int(),
                        esas_ag_eksamensaar = c.String(),
                        esas_ag_eksamensgennemsnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        esas_ag_eksamensland_id = c.Guid(),
                        esas_ag_navn = c.String(),
                        esas_ag_total_points = c.Int(),
                        esas_ag_type = c.Int(),
                        esas_ia_agent = c.String(),
                        esas_ia_kvote3 = c.Boolean(),
                        esas_ia_opholdstilladelse_ok = c.Boolean(),
                        esas_ia_opholdstilladelse = c.Boolean(),
                        esas_ia_scholarship = c.Boolean(),
                        esas_ia_sprogtest = c.Boolean(),
                        esas_ia_st1_sendt = c.DateTimeOffset(precision: 7),
                        esas_ia_tuition_fee_betalt = c.Boolean(),
                        esas_di_adgangskrav = c.Boolean(),
                        esas_di_ansoegningsfrist = c.Boolean(),
                        esas_di_dobbeltuddannelse = c.Boolean(),
                        esas_aarsag_til_ingen_fakturering_id = c.Guid(),
                        esas_aarsag_til_ingen_fakturering_idName = c.String(),
                        esas_debitortype = c.Int(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_person_studerende_esas_personid = c.String(maxLength: 128),
                        esas_ansoeger_esas_person_id = c.Guid(),
                        esas_ag_eksamensland_esas_landId = c.Guid(),
                        esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                        esas_institution_esas_afdelingId = c.Guid(),
                        esas_omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                        esas_planlaegningselement_esas_adgangskrav_id = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        esas_virksomhed_esas_vist_institutionsoplysning_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningId)
                .ForeignKey("dbo.People", t => t.esas_person_studerende_esas_personid)
                .ForeignKey("dbo.Ansoegers", t => t.esas_ansoeger_esas_person_id)
                .ForeignKey("dbo.Lands", t => t.esas_ag_eksamensland_esas_landId)
                .ForeignKey("dbo.Ansoegningsopsaetnings", t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .ForeignKey("dbo.Afdelings", t => t.esas_institution_esas_afdelingId)
                .ForeignKey("dbo.Omraadenummeropsaetnings", t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_planlaegningselement_esas_adgangskrav_id)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.InstitutionVirksomheds", t => t.esas_virksomhed_esas_vist_institutionsoplysning_id)
                .Index(t => t.esas_person_studerende_esas_personid)
                .Index(t => t.esas_ansoeger_esas_person_id)
                .Index(t => t.esas_ag_eksamensland_esas_landId)
                .Index(t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.esas_institution_esas_afdelingId)
                .Index(t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_planlaegningselement_esas_adgangskrav_id)
                .Index(t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_virksomhed_esas_vist_institutionsoplysning_id);
            
            CreateTable(
                "dbo.Lands",
                c => new
                    {
                        esas_landId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_iso2 = c.String(),
                        esas_iso3 = c.String(),
                        esas_engelsknavn = c.String(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_landId);
            
            CreateTable(
                "dbo.Ansoegers",
                c => new
                    {
                        esas_person_id = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        esas_land_id = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        Telephone1 = c.String(),
                        MobilePhone = c.String(),
                        EmailAddress1 = c.String(),
                        FullName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        esas_foedselsdato = c.DateTimeOffset(precision: 7),
                        esas_statsborgerskab_id = c.Guid(),
                        esas_koen = c.Int(),
                        esas_alternativt_cpr_nummer = c.String(),
                        esas_cpr_nummer = c.String(),
                        esas_eidas_pid = c.String(),
                        esas_navne_adressebeskyttet = c.Boolean(),
                        esas_optagelse_dk_id = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        LeadId = c.Guid(nullable: false),
                        OwningBusinessUnit = c.Guid(),
                        esas_land_esas_landId = c.Guid(),
                        esas_postnummer_by_esas_postnummerId = c.Guid(),
                        esas_statsborgerskab_esas_landId = c.Guid(),
                        Land_esas_landId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_person_id)
                .ForeignKey("dbo.Lands", t => t.esas_land_esas_landId)
                .ForeignKey("dbo.Postnummers", t => t.esas_postnummer_by_esas_postnummerId)
                .ForeignKey("dbo.Lands", t => t.esas_statsborgerskab_esas_landId)
                .ForeignKey("dbo.Lands", t => t.Land_esas_landId)
                .Index(t => t.esas_land_esas_landId)
                .Index(t => t.esas_postnummer_by_esas_postnummerId)
                .Index(t => t.esas_statsborgerskab_esas_landId)
                .Index(t => t.Land_esas_landId);
            
            CreateTable(
                "dbo.Postnummers",
                c => new
                    {
                        esas_postnummerId = c.Guid(nullable: false),
                        esas_postnummer = c.String(),
                        esas_by = c.String(),
                        esas_land_id = c.Guid(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_land_esas_landId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_postnummerId)
                .ForeignKey("dbo.Lands", t => t.esas_land_esas_landId)
                .Index(t => t.esas_land_esas_landId);
            
            CreateTable(
                "dbo.InstitutionVirksomheds",
                c => new
                    {
                        esas_vist_institutionsoplysning_id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        Name = c.String(),
                        esas_navn = c.String(),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        Address1_City = c.String(),
                        Address1_PostalCode = c.String(),
                        esas_cvr_nummer = c.String(),
                        esas_ean_nummer = c.String(),
                        esas_p_nummer = c.String(),
                        esas_branche_id = c.Guid(),
                        esas_branche_idName = c.String(),
                        esas_cvr_id = c.String(),
                        esas_cvr_status = c.String(),
                        AccountNumber = c.String(),
                        esas_virksomhedsnummer = c.Int(),
                        esas_juridisk_enhed = c.Boolean(),
                        esas_land_id = c.Guid(),
                        esas_land_idName = c.String(),
                        esas_udenlandsk = c.Int(),
                        esas_offentlig_myndighed = c.Boolean(),
                        WebSiteUrl = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_antal_pladser = c.Int(),
                        esas_sammenflet = c.Boolean(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_postnummer_by_idName = c.String(),
                        esas_sidste_cvr_opdatering = c.DateTimeOffset(precision: 7),
                        esas_status_fra_integration = c.Int(),
                        StatusCode = c.Int(),
                        StateCode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_branche_esas_brancheId = c.Guid(),
                        esas_land_esas_landId = c.Guid(),
                        esas_postnummer_by_esas_postnummerId = c.Guid(),
                        Institutionsoplysninger_esas_institutionsoplysningerId = c.Guid(),
                        esas_vist_institutionsoplysning_esas_institutionsoplysningerId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_vist_institutionsoplysning_id)
                .ForeignKey("dbo.Branches", t => t.esas_branche_esas_brancheId)
                .ForeignKey("dbo.Lands", t => t.esas_land_esas_landId)
                .ForeignKey("dbo.Postnummers", t => t.esas_postnummer_by_esas_postnummerId)
                .ForeignKey("dbo.Institutionsoplysningers", t => t.Institutionsoplysninger_esas_institutionsoplysningerId)
                .ForeignKey("dbo.Institutionsoplysningers", t => t.esas_vist_institutionsoplysning_esas_institutionsoplysningerId)
                .Index(t => t.esas_branche_esas_brancheId)
                .Index(t => t.esas_land_esas_landId)
                .Index(t => t.esas_postnummer_by_esas_postnummerId)
                .Index(t => t.Institutionsoplysninger_esas_institutionsoplysningerId)
                .Index(t => t.esas_vist_institutionsoplysning_esas_institutionsoplysningerId);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        esas_brancheId = c.Guid(nullable: false),
                        esas_branchekode = c.String(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_brancheId);
            
            CreateTable(
                "dbo.Institutionsoplysningers",
                c => new
                    {
                        esas_institutionsoplysningerId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_institution_virksomhed_id = c.Guid(),
                        esas_markedsfoering = c.Boolean(),
                        esas_ns_debitor_nummer = c.String(),
                        esas_samarbejdstype_rekvirent_privat = c.Boolean(),
                        esas_samarbejdstype_rekvirent_offentlig = c.Boolean(),
                        esas_samarbejdstype_praktiksted = c.Boolean(),
                        esas_samarbejdstype_virksomhed = c.Boolean(),
                        esas_samarbejdstype_uddannelsesinstitution = c.Boolean(),
                        esas_samarbejdstype_offentlig_institution = c.Boolean(),
                        StatusCode = c.Int(),
                        StateCode = c.Int(),
                        esas_dokumentafsendelsesmetode = c.Int(),
                        esas_sidst_faktureret = c.DateTimeOffset(precision: 7),
                        esas_virksomhedsemail = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_institution_virksomhed_esas_vist_institutionsoplysning_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_institutionsoplysningerId)
                .ForeignKey("dbo.InstitutionVirksomheds", t => t.esas_institution_virksomhed_esas_vist_institutionsoplysning_id)
                .Index(t => t.esas_institution_virksomhed_esas_vist_institutionsoplysning_id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        esas_personid = c.String(nullable: false, maxLength: 128),
                        ContactId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        BirthDate = c.DateTimeOffset(precision: 7),
                        esas_cpr_nummer = c.String(),
                        esas_cpr_id = c.String(),
                        esas_alternativt_cpr_nummer = c.String(),
                        esas_forventet_afslutning = c.DateTimeOffset(precision: 7),
                        esas_internationalisering = c.Boolean(),
                        esas_modtager_su = c.Boolean(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_postnummer_by_idName = c.String(),
                        statuscode = c.Int(),
                        esas_stillingsbetegnelse = c.String(),
                        esas_talentbekendtgoerelse = c.Boolean(),
                        esas_uddannelsespaalaeg = c.Boolean(),
                        GenderCode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_cpr_nummer_uden_formatering = c.String(),
                        esas_cpr_personstatus = c.Int(),
                        esas_cpr_personsta = c.String(),
                        esas_cpr_seneste_opdatering = c.DateTimeOffset(precision: 7),
                        esas_kommune_id = c.Guid(),
                        esas_kommune_idName = c.String(),
                        esas_statsborgerskab_id = c.Guid(),
                        esas_statsborgerskab_idName = c.String(),
                        esas_land_id = c.Guid(),
                        esas_land_idName = c.String(),
                        esas_navne_addressebeskyttet = c.Boolean(),
                        esas_navne_addressebeesas_sammenfletskyttet = c.Boolean(),
                        StateCode = c.Int(),
                        esas_eidas_pid = c.String(),
                        esas_land_esas_landId = c.Guid(),
                        esas_postnummer_by_esas_postnummerId = c.Guid(),
                        esas_statsborgerskab_esas_landId = c.Guid(),
                        Land_esas_landId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_personid)
                .ForeignKey("dbo.Lands", t => t.esas_land_esas_landId)
                .ForeignKey("dbo.Postnummers", t => t.esas_postnummer_by_esas_postnummerId)
                .ForeignKey("dbo.Lands", t => t.esas_statsborgerskab_esas_landId)
                .ForeignKey("dbo.Lands", t => t.Land_esas_landId)
                .Index(t => t.esas_land_esas_landId)
                .Index(t => t.esas_postnummer_by_esas_postnummerId)
                .Index(t => t.esas_statsborgerskab_esas_landId)
                .Index(t => t.Land_esas_landId);
            
            CreateTable(
                "dbo.Personoplysnings",
                c => new
                    {
                        esas_personoplysningerId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_lokalt_studienummer = c.String(),
                        esas_fagpersontype = c.Int(),
                        esas_person_id = c.Guid(),
                        esas_kaldenavn = c.String(),
                        esas_integrations_id = c.Int(),
                        esas_arbejdsemail = c.String(),
                        esas_arbejdstelefonnummer = c.String(),
                        esas_eksternt_link = c.String(),
                        esas_privatemail = c.String(),
                        esas_privattelefonnummer = c.String(),
                        esas_studieemail = c.String(),
                        esas_mobiltelefonnummer = c.String(),
                        esas_rolle = c.Int(),
                        statuscode = c.Int(),
                        StateCode = c.Int(),
                        esas_dokumentafsendelsesmetode = c.Int(),
                        esas_sidst_faktureret = c.DateTimeOffset(precision: 7),
                        esas_sso_id = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_person_esas_personid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.esas_personoplysningerId)
                .ForeignKey("dbo.People", t => t.esas_person_esas_personid)
                .Index(t => t.esas_person_esas_personid);
            
            CreateTable(
                "dbo.Fagpersonsrelations",
                c => new
                    {
                        esas_fagpersonsrelationId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_bedoemmelse_id = c.Guid(),
                        esas_fagperson_id = c.Guid(),
                        esas_hold_id = c.Guid(),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_bedoemmelse_esas_bedoemmelseId = c.Guid(),
                        esas_fagperson_esas_personoplysningerId = c.Guid(),
                        esas_hold_esas_holdId = c.Guid(),
                        esas_planlaegningsuddannelseselement_esas_adgangskrav_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_fagpersonsrelationId)
                .ForeignKey("dbo.Bedoemmelses", t => t.esas_bedoemmelse_esas_bedoemmelseId)
                .ForeignKey("dbo.Personoplysnings", t => t.esas_fagperson_esas_personoplysningerId)
                .ForeignKey("dbo.Holds", t => t.esas_hold_esas_holdId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id)
                .Index(t => t.esas_bedoemmelse_esas_bedoemmelseId)
                .Index(t => t.esas_fagperson_esas_personoplysningerId)
                .Index(t => t.esas_hold_esas_holdId)
                .Index(t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id);
            
            CreateTable(
                "dbo.Holds",
                c => new
                    {
                        esas_holdId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_navn_dansk = c.String(),
                        esas_navn_engelsk = c.String(),
                        esas_holdtype = c.Int(),
                        esas_holdnummer = c.String(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        esas_antal_uddannelseselement_gennemfoerelse = c.Int(),
                        esas_loebenr = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        esas_holdrelation_id = c.Guid(),
                        esas_institution_id = c.Guid(),
                        esas_institution_esas_vist_institutionsoplysning_id = c.Guid(),
                        esas_planlaegningsuddannelseselement_esas_adgangskrav_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_holdId)
                .ForeignKey("dbo.InstitutionVirksomheds", t => t.esas_institution_esas_vist_institutionsoplysning_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id)
                .Index(t => t.esas_institution_esas_vist_institutionsoplysning_id)
                .Index(t => t.esas_planlaegningsuddannelseselement_esas_adgangskrav_id);
            
            CreateTable(
                "dbo.HoldStudieforloebs",
                c => new
                    {
                        esas_hold_esas_studieforloebid = c.Guid(nullable: false),
                        esas_holdid = c.Guid(),
                        esas_studieforloebid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_hold_esas_studieforloebid)
                .ForeignKey("dbo.Holds", t => t.esas_holdid)
                .ForeignKey("dbo.Studieforloebs", t => t.esas_studieforloebid)
                .Index(t => t.esas_holdid)
                .Index(t => t.esas_studieforloebid);
            
            CreateTable(
                "dbo.Studieforloebs",
                c => new
                    {
                        esas_studieforloebId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_studerende_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_afdeling_id = c.Guid(),
                        esas_ansoegning_id = c.Guid(),
                        esas_forventet_afslutning = c.DateTimeOffset(precision: 7),
                        esas_afgangsdato = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        esas_prioritet = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_semester_modul_id = c.Guid(),
                        esas_opnaaet_ects = c.Decimal(precision: 18, scale: 2),
                        esas_rest_ects = c.Decimal(precision: 18, scale: 2),
                        esas_sammenlagt_fartstid = c.Int(),
                        esas_udloest_staa = c.Decimal(precision: 18, scale: 2),
                        esas_resterende_staa = c.Decimal(precision: 18, scale: 2),
                        esas_rekvirent = c.Boolean(nullable: false),
                        esas_modtager_su = c.Boolean(nullable: false),
                        esas_internationalisering = c.Boolean(nullable: false),
                        esas_talentbekendtgoerelse = c.Boolean(nullable: false),
                        esas_uddannelsespaalaeg = c.Boolean(nullable: false),
                        esas_bevisgrundlag_id = c.Guid(),
                        esas_cpr_nummer = c.String(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_inaktiv_status_per = c.DateTimeOffset(precision: 7),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        esas_rekrevirent_id = c.Guid(),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_tidligere_uddannelsesstruktur_id = c.Guid(),
                        esas_national_afgangsaarsag_id = c.Guid(),
                        esas_afdeling_esas_afdelingId = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                        esas_bevisgrundlag_esas_bevisgrundlagId = c.Guid(),
                        esas_national_afgangsaarsag_esas_national_afgangsaarsagId = c.Guid(),
                        Uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        esas_semester_modul_esas_uddannelseselementId = c.Guid(),
                        esas_studerende_esas_personid = c.String(maxLength: 128),
                        esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_studieforloebId)
                .ForeignKey("dbo.Afdelings", t => t.esas_afdeling_esas_afdelingId)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .ForeignKey("dbo.Bevisgrundlags", t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .ForeignKey("dbo.NationalAfgangsaarsags", t => t.esas_national_afgangsaarsag_esas_national_afgangsaarsagId)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.Uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.StruktureltUddannelseselements", t => t.esas_semester_modul_esas_uddannelseselementId)
                .ForeignKey("dbo.People", t => t.esas_studerende_esas_personid)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_afdeling_esas_afdelingId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .Index(t => t.esas_national_afgangsaarsag_esas_national_afgangsaarsagId)
                .Index(t => t.Uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_semester_modul_esas_uddannelseselementId)
                .Index(t => t.esas_studerende_esas_personid)
                .Index(t => t.esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId);
            
            CreateTable(
                "dbo.Afdelings",
                c => new
                    {
                        esas_afdelingId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_afdelingsniveau_id = c.Guid(),
                        esas_account_id = c.Guid(),
                        esas_afdelingsniveau_idname = c.String(),
                        esas_navn = c.String(),
                        esas_overordnet_afdeling_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_institutionsnummer = c.String(),
                        esas_i_drift = c.Boolean(),
                        esas_team_idname = c.String(),
                        esas_alarmer_om_uhentede_bilag = c.Int(),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_antal_uger_foer_periode_start = c.Int(),
                        esas_educational_institution = c.String(),
                        esas_haandtering_af_studienummer = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_account_esas_vist_institutionsoplysning_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_afdelingId)
                .ForeignKey("dbo.InstitutionVirksomheds", t => t.esas_account_esas_vist_institutionsoplysning_id)
                .Index(t => t.esas_account_esas_vist_institutionsoplysning_id);
            
            CreateTable(
                "dbo.Bevisgrundlags",
                c => new
                    {
                        esas_bevisgrundlagId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        esas_xml = c.String(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bevisgrundlagId);
            
            CreateTable(
                "dbo.NationalAfgangsaarsags",
                c => new
                    {
                        esas_national_afgangsaarsagId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_central_afgangsaarsag = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_national_afgangsaarsagId);
            
            CreateTable(
                "dbo.StruktureltUddannelseselements",
                c => new
                    {
                        esas_uddannelseselementId = c.Guid(nullable: false),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_afsluttet_inden_interval = c.Int(),
                        esas_aktivitetsgruppekode_id = c.Guid(),
                        esas_aktivitetsgruppekode_idName = c.String(),
                        esas_aktivitetstype = c.Int(),
                        esas_antal_uger_foer_periode_start = c.Int(),
                        esas_bedoemmelsesform_gennemfoerelse = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_kmb = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_multiple_choice_test = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_mundlig = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_praktisk_proeve = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_projekt = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_realkompetencevurdering = c.Boolean(nullable: false),
                        esas_bedoemmelsesform_skriftlig = c.Boolean(nullable: false),
                        esas_bedoemmelsestype = c.Int(),
                        esas_beskrivelse = c.String(),
                        esas_betingelser_laast = c.Boolean(nullable: false),
                        esas_dansk_oversaettelse_id = c.Guid(),
                        esas_dansk_oversaettelse_idName = c.String(),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_ects_kraevet = c.Decimal(precision: 18, scale: 2),
                        esas_ects_max = c.Decimal(precision: 18, scale: 2),
                        esas_ects_min = c.Decimal(precision: 18, scale: 2),
                        esas_eksamenssprog_id = c.Guid(),
                        esas_eksamenssprog_idName = c.String(),
                        esas_engelsk_oversaettelse_id = c.Guid(),
                        esas_engelsk_oversaettelse_idName = c.String(),
                        esas_fagtype = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_fordeling = c.Int(),
                        esas_fordeling_prio_1 = c.Boolean(nullable: false),
                        esas_fordeling_prio_2 = c.Boolean(nullable: false),
                        esas_fordelingsnoegle_max = c.Int(),
                        esas_fordelingsnoegle_min = c.Int(),
                        esas_fordelingsnoegle_prio_1 = c.Int(),
                        esas_fordelingsnoegle_prio_2 = c.Int(),
                        esas_karakterskala = c.Int(),
                        esas_loebenummer = c.String(),
                        esas_loennet = c.Boolean(nullable: false),
                        esas_laas_uddannelseselement = c.Boolean(nullable: false),
                        esas_oevrige_krav_laast = c.Boolean(),
                        esas_navn = c.String(),
                        esas_rund_op_til_bestaaet = c.Boolean(nullable: false),
                        esas_order = c.Int(),
                        esas_staa_indberettes = c.Boolean(),
                        OwningBusinessUnit = c.Guid(),
                        esas_paakraevet_udlandsophold = c.Boolean(),
                        esas_redigeringsgrund = c.String(),
                        esas_semester_nummer = c.Int(),
                        esas_skal_bestaas = c.Boolean(),
                        esas_skal_paa_bevis = c.Boolean(),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_sortering_raekkefoelge = c.Int(),
                        esas_sprog_id = c.Guid(),
                        esas_sprog_idName = c.String(),
                        esas_sue_nummer = c.String(),
                        esas_timevaegt = c.Decimal(precision: 18, scale: 2),
                        esas_type = c.Int(),
                        esas_udlandsophold_laast = c.Boolean(),
                        esas_underkarakter = c.Boolean(nullable: false),
                        esas_vaegtning = c.Decimal(precision: 18, scale: 2),
                        esas_valgfrit = c.Boolean(),
                        esas_varighed = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_adgangskrav_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_uddannelse_id = c.Guid(),
                        esas_publiceringsperiode_fra = c.DateTimeOffset(precision: 7),
                        esas_publiceringsperiode_til = c.DateTimeOffset(precision: 7),
                        esas_publiceringsmulighed = c.Int(),
                        esas_uvm_fagkode = c.Int(),
                        esas_antal_antal_timers_undervisning = c.Int(),
                        esas_fagkode_niveau = c.String(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        esas_uddannelse_esas_uddannelseId = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselementId)
                .ForeignKey("dbo.Uddannelses", t => t.esas_uddannelse_esas_uddannelseId)
                .ForeignKey("dbo.Uddannelsesstrukturs", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_uddannelse_esas_uddannelseId)
                .Index(t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId);
            
            CreateTable(
                "dbo.Uddannelsesstrukturs",
                c => new
                    {
                        esas_uddannelsesstrukturId = c.Guid(nullable: false),
                        esas_overordnet_uddannelsesstruktur_id = c.Guid(),
                        esas_aktivitetsgruppekode_id = c.Guid(),
                        esas_aktivitetsgruppekode_idName = c.String(),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_field_of_study = c.String(),
                        esas_grundstruktur = c.Boolean(),
                        esas_institutionsnummer = c.String(),
                        esas_niveau = c.Int(),
                        esas_navn = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_studieretning = c.String(),
                        esas_tjekstatus = c.Int(),
                        esas_tjekstatusbeskrivelse = c.String(),
                        esas_uddannelsestype = c.Int(),
                        esas_uddannelsestype_idName = c.String(),
                        esas_uddannelsens_hjemmeside_link = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_sprog_idName = c.String(),
                        esas_uddannelse_id = c.Guid(),
                        esas_institutionsafdeling_id = c.Guid(),
                        esas_institutionsafdeling_esas_afdelingId = c.Guid(),
                        esas_uddannelse_esas_uddannelseId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Afdelings", t => t.esas_institutionsafdeling_esas_afdelingId)
                .ForeignKey("dbo.Uddannelses", t => t.esas_uddannelse_esas_uddannelseId)
                .Index(t => t.esas_institutionsafdeling_esas_afdelingId)
                .Index(t => t.esas_uddannelse_esas_uddannelseId);
            
            CreateTable(
                "dbo.AndenAktivitets",
                c => new
                    {
                        esas_ansoegning_andre_aktiviteterid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_aktivitet = c.String(),
                        esas_ansoegning_id = c.Guid(),
                        esas_antal_maaneder = c.Int(),
                        esas_institution_organisation = c.String(),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_tid = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_andre_aktiviteterid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Ansoegningshandlings",
                c => new
                    {
                        esas_ansoegningshandlingId = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_beskrivelse_engelsk = c.String(),
                        esas_deadline = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_navn_engelsk = c.String(),
                        esas_type = c.Int(),
                        esas_udfoert_af_ansoeger = c.DateTimeOffset(precision: 7),
                        esas_valg = c.Int(),
                        esas_valgmulighed = c.Int(),
                        esas_valgtype = c.Int(),
                        esas_ansoegning_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningshandlingId)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Bilags",
                c => new
                    {
                        esas_bilagid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        esas_original_filnavn = c.String(),
                        esas_fil_url = c.String(),
                        esas_sidst_hentet = c.DateTimeOffset(precision: 7),
                        esas_fil_content_type = c.String(),
                        esas_bilagskategorier = c.String(),
                        esas_optagelse_dk_fil_id = c.String(),
                        esas_laest = c.Boolean(),
                        esas_filstoerrelse_mb = c.Double(),
                        esas_upload_dato = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bilagid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Enkeltfags",
                c => new
                    {
                        esas_ansoegning_enkeltfagid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_bevistype = c.String(),
                        esas_eksamenstype = c.String(),
                        esas_institution = c.String(),
                        esas_karakter = c.String(),
                        esas_karakterskala = c.String(),
                        esas_navn = c.String(),
                        esas_niveau = c.String(),
                        esas_termin_aar = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansogning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_enkeltfagid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansogning_esas_ansoegningId)
                .Index(t => t.esas_ansogning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Erfaringers",
                c => new
                    {
                        esas_ansoegning_erfaringerid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        esas_antal_maaneder = c.Decimal(precision: 18, scale: 2),
                        esas_arbejdsart = c.String(),
                        esas_arbejdsgiver = c.String(),
                        esas_navn = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_tid = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_erfaringerid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Kommunikations",
                c => new
                    {
                        esas_kommunikationId = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_besked_tilladt_i_svar = c.Int(),
                        esas_bilag_tilladt_i_svar = c.Int(),
                        esas_kilde = c.Int(),
                        esas_kommunikationsskabelon_id = c.Guid(),
                        esas_laest_af_bruger = c.DateTimeOffset(precision: 7),
                        esas_least_af_sagsbehandler = c.DateTimeOffset(precision: 7),
                        esas_meddelelse = c.String(),
                        esas_navn = c.String(),
                        esas_type = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_kommunikationsskabelon_esas_kommunikationsskabelonid = c.Guid(nullable: false),
                        esas_kommunikationsskabelon_esas_besked_tilladt_i_svar = c.Int(),
                        esas_kommunikationsskabelon_esas_beskrivelse = c.String(),
                        esas_kommunikationsskabelon_esas_bilag_tilladt_i_svar = c.Int(),
                        esas_kommunikationsskabelon_esas_navn = c.String(),
                        esas_kommunikationsskabelon_esas_skabelon_engelsk = c.String(),
                        esas_kommunikationsskabelon_esas_skabelon_dansk = c.String(),
                        esas_kommunikationsskabelon_esas_type = c.Int(),
                        esas_kommunikationsskabelon_OwningBusinessUnit = c.Guid(),
                        esas_kommunikationsskabelon_ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_kommunikationsskabelon_ModifiedBy = c.Guid(),
                        esas_kommunikationsskabelon_CreatedBy = c.Guid(),
                        esas_kommunikationsskabelon_CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kommunikationId)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.KurserSkoleopholds",
                c => new
                    {
                        esas_ansoegning_kurser_og_skoleopholdid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        esas_antal_maaneder = c.Decimal(precision: 18, scale: 2),
                        esas_skole = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_kurser_og_skoleopholdid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Proeves",
                c => new
                    {
                        esas_ansoegning_proeveid = c.Guid(nullable: false),
                        esas_bestaaet = c.String(),
                        esas_bestaaet_aar = c.String(),
                        esas_fag = c.String(),
                        esas_mundlig_karakter = c.String(),
                        esas_navn = c.String(),
                        esas_niveau = c.String(),
                        esas_skriftlig_karakter = c.String(),
                        esas_type = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_proeveid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.UdlandsopholdAnsoegnings",
                c => new
                    {
                        esas_ansoegning_udlandsopholdid = c.Guid(nullable: false),
                        esas_aktivitet = c.String(),
                        esas_ansoegning_id = c.Guid(),
                        esas_antal_maaneder = c.Int(),
                        esas_land_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_start = c.DateTimeOffset(precision: 7),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_udlandsopholdid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.VideregaaendeUddannelses",
                c => new
                    {
                        esas_ansoegning_videregaaende_uddannelseid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_optagelsesomraadenavn = c.String(),
                        esas_institutionsnavn = c.String(),
                        esas_paabegyndt_aar = c.String(),
                        esas_bestaaet_aar = c.String(),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_stadig_optaget = c.Boolean(),
                        esas_ansoegning_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_afbrudt_aar = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_videregaaende_uddannelseid)
                .ForeignKey("dbo.Ansoegnings", t => t.esas_ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Publicerings",
                c => new
                    {
                        esas_publiceringid = c.Guid(nullable: false),
                        esas_ansoegningssynkronisering_aktiv = c.Boolean(),
                        esas_beskeder_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_bilagsupload_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_praktiske_oplysninger = c.String(),
                        esas_publiceringsmuligheder = c.String(),
                        esas_publiceringsperiode_fra = c.DateTimeOffset(precision: 7),
                        esas_publiceringsperiode_til = c.DateTimeOffset(precision: 7),
                        esas_ansoegningskortopsaetning_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_publiceringid)
                .ForeignKey("dbo.AnsoegningskortOpsaetnings", t => t.esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid)
                .Index(t => t.esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.AnsoegningskortOpsaetnings",
                c => new
                    {
                        esas_ansoegningskortopsaetningid = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_navn = c.String(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.Ansoegningskorts",
                c => new
                    {
                        esas_ansoegningskortid = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_obligatorisk = c.Boolean(),
                        esas_skal_vises = c.Boolean(),
                        esas_type = c.Int(),
                        esas_ansoegningskortopsaetning_id = c.Guid(),
                        esas_ansoegningskorttekst_id = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansoegningskorttekst_esas_ansoegningskorttekstid = c.Guid(nullable: false),
                        esas_ansoegningskorttekst_esas_beskrivelse = c.String(),
                        esas_ansoegningskorttekst_esas_hjaelpetekst_dansk = c.String(),
                        esas_ansoegningskorttekst_esas_hjaelpetekst_engelsk = c.String(),
                        esas_ansoegningskorttekst_esas_navn = c.String(),
                        esas_ansoegningskorttekst_ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegningskorttekst_ModifiedBy = c.Guid(),
                        esas_ansoegningskorttekst_CreatedBy = c.Guid(),
                        esas_ansoegningskorttekst_CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegningskorttekst_OwningBusinessUnit = c.Guid(),
                        esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskortid)
                .ForeignKey("dbo.AnsoegningskortOpsaetnings", t => t.esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid)
                .Index(t => t.esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.GebyrtypePUERelations",
                c => new
                    {
                        esas_gebyrtype_esas_uddannelseselement_plid = c.Guid(nullable: false),
                        esas_uddannelseselement_planlaegningid = c.Guid(),
                        esas_gebyrtypeid = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_gebyrtype_esas_beloeb = c.Decimal(precision: 18, scale: 2),
                        esas_gebyrtype_esas_beloeb_valuta = c.String(),
                        esas_gebyrtype_esas_beskrivelse = c.String(),
                        esas_gebyrtype_esas_debitortype = c.Int(),
                        esas_gebyrtype_esas_kategori = c.Int(),
                        esas_gebyrtype_esas_kontostreng_id = c.Guid(nullable: false),
                        esas_gebyrtype_esas_kontostreng_idName = c.String(),
                        esas_gebyrtype_esas_navn = c.String(),
                        esas_gebyrtype_esas_skabelon_id = c.Guid(nullable: false),
                        esas_gebyrtype_esas_skabelon_idName = c.String(),
                        esas_gebyrtype_esas_type = c.Int(nullable: false),
                        esas_gebyrtype_esas_gebyrtypeid = c.Guid(nullable: false),
                        esas_gebyrtype_CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_gebyrtype_ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_gebyrtype_statecode = c.Int(),
                        esas_gebyrtype_statuscode = c.Int(),
                        esas_gebyrtype_OwningBusinessUnit = c.Guid(),
                        esas_uddannelseselement_planlaegning_esas_adgangskrav_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gebyrtype_esas_uddannelseselement_plid)
                .ForeignKey("dbo.PlanlaegningsUddannelseselements", t => t.esas_uddannelseselement_planlaegning_esas_adgangskrav_id)
                .Index(t => t.esas_uddannelseselement_planlaegning_esas_adgangskrav_id);
            
            CreateTable(
                "dbo.Praktikomraades",
                c => new
                    {
                        esas_praktikomraadeId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_beskrivelse = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_praktikomraadeId);
            
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Advertisements");
            DropTable("dbo.CategoryProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Product_ID });
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        AirDate = c.DateTime(nullable: false),
                        FeaturedProduct_ID = c.Int(nullable: false),
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
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            DropForeignKey("dbo.Praktikopholds", "Uddannelse_esas_uddannelseId", "dbo.Uddannelses");
            DropForeignKey("dbo.Praktikopholds", "esas_praktiksted_esas_vist_institutionsoplysning_id", "dbo.InstitutionVirksomheds");
            DropForeignKey("dbo.Praktikopholds", "esas_praktikomraade_esas_praktikomraadeId", "dbo.Praktikomraades");
            DropForeignKey("dbo.Praktikopholds", "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId", "dbo.GennemfoerelsesUddannelseselements");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_bedoemmelsesresultat_esas_karakterId", "dbo.Karakters");
            DropForeignKey("dbo.Bedoemmelses", "esas_studieforloeb_esas_studieforloebId", "dbo.Studieforloebs");
            DropForeignKey("dbo.Bedoemmelses", "esas_karakter_esas_karakterId", "dbo.Karakters");
            DropForeignKey("dbo.Bedoemmelses", "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId", "dbo.GennemfoerelsesUddannelseselements");
            DropForeignKey("dbo.Bedoemmelses", "esas_bedoemmelsesrunde_esas_bedoemmelsesrundeId", "dbo.Bedoemmelsesrundes");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_pue_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.GebyrtypePUERelations", "esas_uddannelseselement_planlaegning_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.Bedoemmelsesrundes", "esas_planlaegningsuddannelseselement_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_uddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_uddannelse_esas_uddannelseId", "dbo.Uddannelses");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_semester_modul_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_samlaesning_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_publicering_esas_publiceringid", "dbo.Publicerings");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_gruppering_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.Adgangskravs", "esas_adgangskravId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.Adgangskravs", "esas_adgangskravId", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.Adgangskravs", "esas_adgangskravId", "dbo.Omraadenummeropsaetnings");
            DropForeignKey("dbo.Omraadenummeropsaetnings", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Omraadenummeropsaetnings", "esas_publicering_esas_publiceringid", "dbo.Publicerings");
            DropForeignKey("dbo.Publicerings", "esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid", "dbo.AnsoegningskortOpsaetnings");
            DropForeignKey("dbo.Ansoegningskorts", "esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid", "dbo.AnsoegningskortOpsaetnings");
            DropForeignKey("dbo.Omraadenummeropsaetnings", "esas_institutionsafdeling_esas_afdelingId", "dbo.Afdelings");
            DropForeignKey("dbo.Omraadenummeropsaetnings", "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetnings");
            DropForeignKey("dbo.VideregaaendeUddannelses", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.UdlandsopholdAnsoegnings", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Proeves", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.KurserSkoleopholds", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Kommunikations", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Erfaringers", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Enkeltfags", "esas_ansogning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Bilags", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Ansoegningshandlings", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.AndenAktivitets", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Ansoegnings", "esas_virksomhed_esas_vist_institutionsoplysning_id", "dbo.InstitutionVirksomheds");
            DropForeignKey("dbo.Ansoegnings", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Ansoegnings", "esas_planlaegningselement_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.Ansoegnings", "esas_omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetnings");
            DropForeignKey("dbo.Ansoegnings", "esas_institution_esas_afdelingId", "dbo.Afdelings");
            DropForeignKey("dbo.Ansoegnings", "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetnings");
            DropForeignKey("dbo.People", "Land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.Ansoegnings", "esas_ag_eksamensland_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.Ansoegers", "Land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.Ansoegnings", "esas_ansoeger_esas_person_id", "dbo.Ansoegers");
            DropForeignKey("dbo.Ansoegers", "esas_statsborgerskab_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "esas_postnummer_by_esas_postnummerId", "dbo.Postnummers");
            DropForeignKey("dbo.Praktikopholds", "esas_praktikvejleder_esas_personid", "dbo.People");
            DropForeignKey("dbo.Fagpersonsrelations", "esas_planlaegningsuddannelseselement_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "Studieforloeb_esas_studieforloebId1", "dbo.Studieforloebs");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloebs");
            DropForeignKey("dbo.HoldStudieforloebs", "esas_studieforloebid", "dbo.Studieforloebs");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_studieforloeb_esas_studieforloebId", "dbo.Studieforloebs");
            DropForeignKey("dbo.Praktikopholds", "esas_studieforloeb_esas_studieforloebId", "dbo.Studieforloebs");
            DropForeignKey("dbo.Studieforloebs", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Studieforloebs", "esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Studieforloebs", "esas_studerende_esas_personid", "dbo.People");
            DropForeignKey("dbo.PlanlaegningsUddannelseselements", "StruktureltUddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_uddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.Studieforloebs", "esas_semester_modul_esas_uddannelseselementId", "dbo.StruktureltUddannelseselements");
            DropForeignKey("dbo.StruktureltUddannelseselements", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Studieforloebs", "Uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstrukturs");
            DropForeignKey("dbo.Uddannelsesstrukturs", "esas_uddannelse_esas_uddannelseId", "dbo.Uddannelses");
            DropForeignKey("dbo.Uddannelsesstrukturs", "esas_institutionsafdeling_esas_afdelingId", "dbo.Afdelings");
            DropForeignKey("dbo.StruktureltUddannelseselements", "esas_uddannelse_esas_uddannelseId", "dbo.Uddannelses");
            DropForeignKey("dbo.Studieforloebs", "esas_national_afgangsaarsag_esas_national_afgangsaarsagId", "dbo.NationalAfgangsaarsags");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_bevisgrundlag_esas_bevisgrundlagId", "dbo.Bevisgrundlags");
            DropForeignKey("dbo.Studieforloebs", "esas_bevisgrundlag_esas_bevisgrundlagId", "dbo.Bevisgrundlags");
            DropForeignKey("dbo.Studieforloebs", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegnings");
            DropForeignKey("dbo.Studieforloebs", "esas_afdeling_esas_afdelingId", "dbo.Afdelings");
            DropForeignKey("dbo.Afdelings", "esas_account_esas_vist_institutionsoplysning_id", "dbo.InstitutionVirksomheds");
            DropForeignKey("dbo.HoldStudieforloebs", "esas_holdid", "dbo.Holds");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselements", "esas_hold_esas_holdId", "dbo.Holds");
            DropForeignKey("dbo.Fagpersonsrelations", "esas_hold_esas_holdId", "dbo.Holds");
            DropForeignKey("dbo.Holds", "esas_planlaegningsuddannelseselement_esas_adgangskrav_id", "dbo.PlanlaegningsUddannelseselements");
            DropForeignKey("dbo.Holds", "esas_institution_esas_vist_institutionsoplysning_id", "dbo.InstitutionVirksomheds");
            DropForeignKey("dbo.Fagpersonsrelations", "esas_fagperson_esas_personoplysningerId", "dbo.Personoplysnings");
            DropForeignKey("dbo.Fagpersonsrelations", "esas_bedoemmelse_esas_bedoemmelseId", "dbo.Bedoemmelses");
            DropForeignKey("dbo.Personoplysnings", "esas_person_esas_personid", "dbo.People");
            DropForeignKey("dbo.Ansoegnings", "esas_person_studerende_esas_personid", "dbo.People");
            DropForeignKey("dbo.People", "esas_statsborgerskab_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.People", "esas_postnummer_by_esas_postnummerId", "dbo.Postnummers");
            DropForeignKey("dbo.People", "esas_land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.InstitutionVirksomheds", "esas_vist_institutionsoplysning_esas_institutionsoplysningerId", "dbo.Institutionsoplysningers");
            DropForeignKey("dbo.InstitutionVirksomheds", "Institutionsoplysninger_esas_institutionsoplysningerId", "dbo.Institutionsoplysningers");
            DropForeignKey("dbo.Institutionsoplysningers", "esas_institution_virksomhed_esas_vist_institutionsoplysning_id", "dbo.InstitutionVirksomheds");
            DropForeignKey("dbo.InstitutionVirksomheds", "esas_postnummer_by_esas_postnummerId", "dbo.Postnummers");
            DropForeignKey("dbo.InstitutionVirksomheds", "esas_land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.InstitutionVirksomheds", "esas_branche_esas_brancheId", "dbo.Branches");
            DropForeignKey("dbo.Ansoegers", "esas_postnummer_by_esas_postnummerId", "dbo.Postnummers");
            DropForeignKey("dbo.Postnummers", "esas_land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.Ansoegers", "esas_land_esas_landId", "dbo.Lands");
            DropForeignKey("dbo.GymnasielleKarakterkravs", "Ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetnings");
            DropForeignKey("dbo.GymnasielleFagkravs", "Ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetnings");
            DropForeignKey("dbo.Bedoemmelses", "esas_bedoemmelse_registreret_af_SystemUserId", "dbo.SystemUsers");
            DropForeignKey("dbo.Bedoemmelses", "esas_bedoemmelse_godkendt_af_SystemUserId", "dbo.SystemUsers");
            DropIndex("dbo.GebyrtypePUERelations", new[] { "esas_uddannelseselement_planlaegning_esas_adgangskrav_id" });
            DropIndex("dbo.Ansoegningskorts", new[] { "esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid" });
            DropIndex("dbo.Publicerings", new[] { "esas_ansoegningskortopsaetning_esas_ansoegningskortopsaetningid" });
            DropIndex("dbo.VideregaaendeUddannelses", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.UdlandsopholdAnsoegnings", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Proeves", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.KurserSkoleopholds", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Kommunikations", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Erfaringers", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Enkeltfags", new[] { "esas_ansogning_esas_ansoegningId" });
            DropIndex("dbo.Bilags", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Ansoegningshandlings", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.AndenAktivitets", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Uddannelsesstrukturs", new[] { "esas_uddannelse_esas_uddannelseId" });
            DropIndex("dbo.Uddannelsesstrukturs", new[] { "esas_institutionsafdeling_esas_afdelingId" });
            DropIndex("dbo.StruktureltUddannelseselements", new[] { "esas_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.StruktureltUddannelseselements", new[] { "esas_uddannelse_esas_uddannelseId" });
            DropIndex("dbo.Afdelings", new[] { "esas_account_esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_studerende_esas_personid" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_semester_modul_esas_uddannelseselementId" });
            DropIndex("dbo.Studieforloebs", new[] { "Uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_national_afgangsaarsag_esas_national_afgangsaarsagId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_bevisgrundlag_esas_bevisgrundlagId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Studieforloebs", new[] { "esas_afdeling_esas_afdelingId" });
            DropIndex("dbo.HoldStudieforloebs", new[] { "esas_studieforloebid" });
            DropIndex("dbo.HoldStudieforloebs", new[] { "esas_holdid" });
            DropIndex("dbo.Holds", new[] { "esas_planlaegningsuddannelseselement_esas_adgangskrav_id" });
            DropIndex("dbo.Holds", new[] { "esas_institution_esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.Fagpersonsrelations", new[] { "esas_planlaegningsuddannelseselement_esas_adgangskrav_id" });
            DropIndex("dbo.Fagpersonsrelations", new[] { "esas_hold_esas_holdId" });
            DropIndex("dbo.Fagpersonsrelations", new[] { "esas_fagperson_esas_personoplysningerId" });
            DropIndex("dbo.Fagpersonsrelations", new[] { "esas_bedoemmelse_esas_bedoemmelseId" });
            DropIndex("dbo.Personoplysnings", new[] { "esas_person_esas_personid" });
            DropIndex("dbo.People", new[] { "Land_esas_landId" });
            DropIndex("dbo.People", new[] { "esas_statsborgerskab_esas_landId" });
            DropIndex("dbo.People", new[] { "esas_postnummer_by_esas_postnummerId" });
            DropIndex("dbo.People", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.Institutionsoplysningers", new[] { "esas_institution_virksomhed_esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.InstitutionVirksomheds", new[] { "esas_vist_institutionsoplysning_esas_institutionsoplysningerId" });
            DropIndex("dbo.InstitutionVirksomheds", new[] { "Institutionsoplysninger_esas_institutionsoplysningerId" });
            DropIndex("dbo.InstitutionVirksomheds", new[] { "esas_postnummer_by_esas_postnummerId" });
            DropIndex("dbo.InstitutionVirksomheds", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.InstitutionVirksomheds", new[] { "esas_branche_esas_brancheId" });
            DropIndex("dbo.Postnummers", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.Ansoegers", new[] { "Land_esas_landId" });
            DropIndex("dbo.Ansoegers", new[] { "esas_statsborgerskab_esas_landId" });
            DropIndex("dbo.Ansoegers", new[] { "esas_postnummer_by_esas_postnummerId" });
            DropIndex("dbo.Ansoegers", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_virksomhed_esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_planlaegningselement_esas_adgangskrav_id" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_institution_esas_afdelingId" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_ag_eksamensland_esas_landId" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_ansoeger_esas_person_id" });
            DropIndex("dbo.Ansoegnings", new[] { "esas_person_studerende_esas_personid" });
            DropIndex("dbo.GymnasielleKarakterkravs", new[] { "Ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.GymnasielleFagkravs", new[] { "Ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.Omraadenummeropsaetnings", new[] { "esas_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Omraadenummeropsaetnings", new[] { "esas_publicering_esas_publiceringid" });
            DropIndex("dbo.Omraadenummeropsaetnings", new[] { "esas_institutionsafdeling_esas_afdelingId" });
            DropIndex("dbo.Omraadenummeropsaetnings", new[] { "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.Adgangskravs", new[] { "esas_adgangskravId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_uddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_uddannelse_esas_uddannelseId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_semester_modul_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_samlaesning_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_publicering_esas_publiceringid" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_gruppering_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "esas_postnummer_by_esas_postnummerId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "Studieforloeb_esas_studieforloebId1" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId2" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId1" });
            DropIndex("dbo.PlanlaegningsUddannelseselements", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.Bedoemmelsesrundes", new[] { "esas_planlaegningsuddannelseselement_esas_adgangskrav_id" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_karakter_esas_karakterId" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_bedoemmelsesrunde_esas_bedoemmelsesrundeId" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_bedoemmelse_registreret_af_SystemUserId" });
            DropIndex("dbo.Bedoemmelses", new[] { "esas_bedoemmelse_godkendt_af_SystemUserId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_bedoemmelsesresultat_esas_karakterId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_pue_esas_adgangskrav_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_uddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_bevisgrundlag_esas_bevisgrundlagId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselements", new[] { "esas_hold_esas_holdId" });
            DropIndex("dbo.Praktikopholds", new[] { "Uddannelse_esas_uddannelseId" });
            DropIndex("dbo.Praktikopholds", new[] { "esas_praktiksted_esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.Praktikopholds", new[] { "esas_praktikomraade_esas_praktikomraadeId" });
            DropIndex("dbo.Praktikopholds", new[] { "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId" });
            DropIndex("dbo.Praktikopholds", new[] { "esas_praktikvejleder_esas_personid" });
            DropIndex("dbo.Praktikopholds", new[] { "esas_studieforloeb_esas_studieforloebId" });
            DropTable("dbo.Praktikomraades");
            DropTable("dbo.GebyrtypePUERelations");
            DropTable("dbo.Ansoegningskorts");
            DropTable("dbo.AnsoegningskortOpsaetnings");
            DropTable("dbo.Publicerings");
            DropTable("dbo.VideregaaendeUddannelses");
            DropTable("dbo.UdlandsopholdAnsoegnings");
            DropTable("dbo.Proeves");
            DropTable("dbo.KurserSkoleopholds");
            DropTable("dbo.Kommunikations");
            DropTable("dbo.Erfaringers");
            DropTable("dbo.Enkeltfags");
            DropTable("dbo.Bilags");
            DropTable("dbo.Ansoegningshandlings");
            DropTable("dbo.AndenAktivitets");
            DropTable("dbo.Uddannelsesstrukturs");
            DropTable("dbo.StruktureltUddannelseselements");
            DropTable("dbo.NationalAfgangsaarsags");
            DropTable("dbo.Bevisgrundlags");
            DropTable("dbo.Afdelings");
            DropTable("dbo.Studieforloebs");
            DropTable("dbo.HoldStudieforloebs");
            DropTable("dbo.Holds");
            DropTable("dbo.Fagpersonsrelations");
            DropTable("dbo.Personoplysnings");
            DropTable("dbo.People");
            DropTable("dbo.Institutionsoplysningers");
            DropTable("dbo.Branches");
            DropTable("dbo.InstitutionVirksomheds");
            DropTable("dbo.Postnummers");
            DropTable("dbo.Ansoegers");
            DropTable("dbo.Lands");
            DropTable("dbo.Ansoegnings");
            DropTable("dbo.GymnasielleKarakterkravs");
            DropTable("dbo.GymnasielleFagkravs");
            DropTable("dbo.Ansoegningsopsaetnings");
            DropTable("dbo.Omraadenummeropsaetnings");
            DropTable("dbo.Adgangskravs");
            DropTable("dbo.PlanlaegningsUddannelseselements");
            DropTable("dbo.Bedoemmelsesrundes");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.Bedoemmelses");
            DropTable("dbo.Karakters");
            DropTable("dbo.GennemfoerelsesUddannelseselements");
            DropTable("dbo.Praktikopholds");
            DropTable("dbo.Uddannelses");
            CreateIndex("dbo.CategoryProducts", "Product_ID");
            CreateIndex("dbo.CategoryProducts", "Category_ID");
            CreateIndex("dbo.Advertisements", "FeaturedProduct_ID");
            CreateIndex("dbo.Products", "Supplier_ID");
            CreateIndex("dbo.Products", "ID");
            AddForeignKey("dbo.Advertisements", "FeaturedProduct_ID", "dbo.Products", "ID");
            AddForeignKey("dbo.Products", "Supplier_ID", "dbo.Suppliers", "ID");
            AddForeignKey("dbo.Products", "ID", "dbo.ProductDetails", "ProductID");
            AddForeignKey("dbo.CategoryProducts", "Product_ID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProducts", "Category_ID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
