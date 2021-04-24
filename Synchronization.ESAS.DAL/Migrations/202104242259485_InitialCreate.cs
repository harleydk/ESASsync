namespace Synchronization.ESAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adgangskrav",
                c => new
                    {
                        esas_adgangskravId = c.Guid(nullable: false),
                        esas_adgangsgrundlag = c.String(),
                        esas_saerlige_krav = c.Boolean(),
                        esas_saerlige_krav_beskrivelse = c.String(),
                        esas_dispensation_for_adgangskrav_muligt = c.Int(),
                        esas_ansoegning_med_saerlig_tilladelse_muligt = c.Int(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_adgangskravId);
            
            CreateTable(
                "dbo.Afdeling",
                c => new
                    {
                        esas_afdelingId = c.Guid(nullable: false),
                        esas_account_id = c.Guid(),
                        esas_overordnet_afdeling_id = c.Guid(),
                        esas_periode_lige_semester_sommer_id = c.Guid(),
                        esas_periode_lige_semester_vinter_id = c.Guid(),
                        esas_periode_ulige_semester_sommer_id = c.Guid(),
                        esas_periode_ulige_semester_vinter_id = c.Guid(),
                        esas_afdelingsniveau_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_institutionsnummer = c.String(),
                        esas_alarmer_om_uhentede_bilag = c.Int(),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_antal_uger_foer_periode_start = c.Int(),
                        esas_educational_institution = c.String(),
                        esas_kommunikation_klage_ou = c.String(),
                        esas_kommunikation_klage_ou_engelsk = c.String(),
                        esas_kommunikation_klage_aau = c.String(),
                        esas_kommunikation_klage_aau_engelsk = c.String(),
                        esas_kommunikation_betinget_ou = c.String(),
                        esas_kommunikation_betinget_ou_engelsk = c.String(),
                        esas_kommunikation_betinget_aau = c.String(),
                        esas_kommunikation_betinget_aau_engelsk = c.String(),
                        esas_kommunikation_hjaelp_ou = c.String(),
                        esas_kommunikation_hjaelp_ou_engelsk = c.String(),
                        esas_kommunikation_hjaelp_aau = c.String(),
                        esas_kommunikation_hjaelp_aau_engelsk = c.String(),
                        esas_kommunikation_ikv_rkv_ou = c.String(),
                        esas_kommunikation_ikv_rkv_ou_engelsk = c.String(),
                        esas_kommunikation_ikv_rkv_aau = c.String(),
                        esas_kommunikation_ikv_rkv_aau_engelsk = c.String(),
                        esas_kommunikation_ledige_pladser_ou = c.String(),
                        esas_kommunikation_ledige_pladser_ou_engelsk = c.String(),
                        esas_kommunikation_ledige_pladser_aau = c.String(),
                        esas_kommunikation_ledige_pladser_aau_engelsk = c.String(),
                        esas_kommunikation_studiestart_ou = c.String(),
                        esas_kommunikation_studiestart_ou_engelsk = c.String(),
                        esas_kommunikation_studiestart_aau = c.String(),
                        esas_kommunikation_studiestart_aau_engelsk = c.String(),
                        esas_fire_oejneprincip = c.Boolean(),
                        esas_i_drift = c.Boolean(),
                        esas_haandtering_af_studienummer = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_afdelingsniveau_esas_afdelingsniveauId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_afdelingId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_account_id)
                .ForeignKey("dbo.Afdelingsniveau", t => t.esas_afdelingsniveau_esas_afdelingsniveauId)
                .ForeignKey("dbo.Afdeling", t => t.esas_overordnet_afdeling_id);
            
            CreateTable(
                "dbo.InstitutionVirksomhed",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        esas_vist_institutionsoplysning_id = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_branche_id = c.Guid(),
                        esas_land_id = c.Guid(),
                        Name = c.String(),
                        esas_navn = c.String(),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        Address1_City = c.String(),
                        Address1_PostalCode = c.String(),
                        esas_cvr_nummer = c.String(),
                        esas_ean_nummer = c.String(),
                        esas_p_nummer = c.String(),
                        esas_antal_pladser = c.Int(),
                        esas_udenlandsk = c.Int(),
                        AccountNumber = c.String(),
                        esas_virksomhedsnummer = c.Int(),
                        esas_juridisk_enhed = c.Boolean(),
                        esas_offentlig_myndighed = c.Boolean(),
                        ParentAccountId = c.Guid(),
                        WebSiteUrl = c.String(),
                        esas_sammenflet = c.Boolean(),
                        esas_cvr_id = c.String(),
                        esas_cvr_status = c.String(),
                        esas_sidste_cvr_opdatering = c.DateTimeOffset(precision: 7),
                        esas_status_fra_integration = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        Institutionsoplysninger_esas_institutionsoplysningerId = c.Guid(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Branche", t => t.esas_branche_id)
                .ForeignKey("dbo.Land", t => t.esas_land_id)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Institutionsoplysninger", t => t.Institutionsoplysninger_esas_institutionsoplysningerId)
                .ForeignKey("dbo.Institutionsoplysninger", t => t.esas_vist_institutionsoplysning_id);
            
            CreateTable(
                "dbo.Branche",
                c => new
                    {
                        esas_brancheId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_branchekode = c.String(),
                        esas_branchetekst = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_brancheId);
            
            CreateTable(
                "dbo.Land",
                c => new
                    {
                        esas_landId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_iso2 = c.String(),
                        esas_iso3 = c.String(),
                        esas_engelsknavn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_landId);
            
            CreateTable(
                "dbo.Postnummer",
                c => new
                    {
                        esas_postnummerId = c.Guid(nullable: false),
                        esas_land_id = c.Guid(),
                        esas_postnummer = c.String(),
                        esas_navn = c.String(),
                        esas_by = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_postnummerId)
                .ForeignKey("dbo.Land", t => t.esas_land_id);
            
            CreateTable(
                "dbo.Institutionsoplysninger",
                c => new
                    {
                        esas_institutionsoplysningerId = c.Guid(nullable: false),
                        esas_institution_virksomhed_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_markedsfoering = c.Boolean(),
                        esas_ns_debitor_nummer = c.String(),
                        esas_samarbejdstype_rekvirent_privat = c.Boolean(),
                        esas_samarbejdstype_rekvirent_offentlig = c.Boolean(),
                        esas_samarbejdstype_praktiksted = c.Boolean(),
                        esas_samarbejdstype_virksomhed = c.Boolean(),
                        esas_samarbejdstype_uddannelsesinstitution = c.Boolean(),
                        esas_samarbejdstype_offentlig_institution = c.Boolean(),
                        esas_dokumentafsendelsesmetode = c.Int(),
                        esas_sidst_faktureret = c.DateTimeOffset(precision: 7),
                        esas_virksomhedsemail = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        InstitutionVirksomhed_AccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_institutionsoplysningerId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_virksomhed_id)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.InstitutionVirksomhed_AccountId);
            
            CreateTable(
                "dbo.Hold",
                c => new
                    {
                        esas_holdId = c.Guid(nullable: false),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        esas_institution_id = c.Guid(),
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_antal_uddannelseselement_gennemfoerelse = c.Int(),
                        esas_holdnummer = c.String(),
                        esas_holdrelation_id = c.Guid(),
                        esas_holdtype = c.Int(),
                        esas_loebenr = c.String(),
                        esas_navn = c.String(),
                        esas_navn_dansk = c.String(),
                        esas_navn_engelsk = c.String(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_fordelingsnoegle_prio_1 = c.Int(),
                        esas_fordelingsnoegle_prio_2 = c.Int(),
                        esas_fordelingsnoegle_max = c.Int(),
                        esas_fordelingsnoegle_min = c.Int(),
                        esas_fordeling_prio_1 = c.Boolean(),
                        esas_fordeling_prio_2 = c.Boolean(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        Personoplysning_esas_personoplysningerId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_holdId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id)
                .ForeignKey("dbo.Personoplysning", t => t.Personoplysning_esas_personoplysningerId);
            
            CreateTable(
                "dbo.Aktivitetsudbud",
                c => new
                    {
                        esas_aktivitetsudbudId = c.Guid(nullable: false),
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_institutionsafdeling_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_antal_hold = c.Int(),
                        esas_nomenklatur_sommer = c.String(),
                        esas_nomenklatur_vinter = c.String(),
                        esas_periode_for_lige_semestre_sommer_id = c.Guid(),
                        esas_periode_for_lige_semestre_vinter_id = c.Guid(),
                        esas_periode_for_ulige_semestre_sommer_id = c.Guid(),
                        esas_periode_for_ulige_semestre_vinter_id = c.Guid(),
                        esas_sprog_id = c.Guid(),
                        esas_ophoersdato = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_aktivitetsudbudId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Afdeling", t => t.esas_institutionsafdeling_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_id);
            
            CreateTable(
                "dbo.Uddannelsesstruktur",
                c => new
                    {
                        esas_uddannelsesstrukturId = c.Guid(nullable: false),
                        esas_uddannelsesaktivitet_id = c.Guid(),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_field_of_study = c.String(),
                        esas_navn = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_institutionsafdeling_id = c.Guid(),
                        esas_audd_kode_id = c.Guid(),
                        esas_studieretning = c.String(),
                        esas_tjekstatus = c.Int(),
                        esas_tjekstatusbeskrivelse = c.String(),
                        esas_uddannelsestype = c.Int(),
                        esas_uddannelsens_hjemmeside_link = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Uddannelsesaktivitet", t => t.esas_uddannelsesaktivitet_id);
            
            CreateTable(
                "dbo.Uddannelsesaktivitet",
                c => new
                    {
                        esas_uddannelsesaktivitetId = c.Guid(nullable: false),
                        esas_bekendtgoerelse_id = c.Guid(),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_aktivitetsgruppekode = c.String(),
                        esas_antal_semestre = c.Int(),
                        esas_dags_dato = c.DateTimeOffset(precision: 7),
                        esas_betegnelse = c.String(),
                        esas_betegnelse_engelsk = c.String(),
                        esas_cosa_formaalskode = c.String(),
                        esas_delformaal = c.String(),
                        esas_dst_kode = c.String(),
                        esas_ects_loft_praktik = c.Decimal(precision: 18, scale: 2),
                        esas_ects_loft_teori = c.Decimal(precision: 18, scale: 2),
                        esas_ects_loft_vaerkstedsskole = c.Decimal(precision: 18, scale: 2),
                        esas_staa_loft_praktik = c.Decimal(precision: 18, scale: 2),
                        esas_staa_loft_teori = c.Decimal(precision: 18, scale: 2),
                        esas_staa_loft_vaerkstedskole = c.Decimal(precision: 18, scale: 2),
                        esas_indberetning_af_optag = c.Boolean(),
                        esas_indberetning_af_staa_au = c.Boolean(),
                        esas_indberetning_af_staa_ou = c.Boolean(),
                        esas_indberetning_af_su = c.Boolean(),
                        esas_indberetning_til_soefartsstyrelsen = c.Boolean(),
                        esas_opgoerelsesmetode = c.Int(),
                        esas_ophoersdato = c.DateTimeOffset(precision: 7),
                        esas_samlet_ects = c.Decimal(precision: 18, scale: 2),
                        esas_start_indberetning_af_optag = c.DateTimeOffset(precision: 7),
                        esas_start_indberetning_af_staa_au = c.DateTimeOffset(precision: 7),
                        esas_start_indberetning_af_staa_ou = c.DateTimeOffset(precision: 7),
                        esas_start_indberetning_af_su = c.DateTimeOffset(precision: 7),
                        esas_start_indberetning_til_soefartsstyrelsen = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_stop_indberetning_af_optag = c.DateTimeOffset(precision: 7),
                        esas_stop_indberetning_af_staa_au = c.DateTimeOffset(precision: 7),
                        esas_stop_indberetning_af_staa_ou = c.DateTimeOffset(precision: 7),
                        esas_stop_indberetning_af_su = c.DateTimeOffset(precision: 7),
                        esas_stop_indberetning_til_soefartsstyrelsen = c.DateTimeOffset(precision: 7),
                        esas_su_retningskode = c.String(),
                        esas_titel = c.String(),
                        esas_titel_engelsk = c.String(),
                        esas_uddannelsesdel = c.Int(),
                        esas_uddannelsesform = c.Int(),
                        esas_uddannelsestype = c.Int(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_uddannelsesaktivitetId);
            
            CreateTable(
                "dbo.PlanlaegningsUddannelseselement",
                c => new
                    {
                        esas_uddannelseselement_planlaegningId = c.Guid(nullable: false),
                        esas_uddannelseselement_id = c.Guid(),
                        esas_semester_modul_id = c.Guid(),
                        esas_gruppering_id = c.Guid(),
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_samlaesning_id = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_adgangskrav_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_adresselinje_1 = c.String(),
                        esas_adresselinje_2 = c.String(),
                        esas_antal_uddannelseselement_gennemfoerelse = c.Int(),
                        esas_bedoemmelsesform_gennemfoerelse = c.Boolean(),
                        esas_bedoemmelsesform_kmb = c.Boolean(),
                        esas_bedoemmelsesform_multiple_choice_test = c.Boolean(),
                        esas_bedoemmelsesform_mundtlig = c.Boolean(),
                        esas_bedoemmelsesform_praktisk_proeve = c.Boolean(),
                        esas_bedoemmelsesform_projekt = c.Boolean(),
                        esas_bedoemmelsesform_realkompetencevurdering = c.Boolean(),
                        esas_bedoemmelsesform_skriftlig = c.Boolean(),
                        esas_bedoemmelsestype = c.Int(),
                        esas_deltagerpris = c.Decimal(precision: 18, scale: 2),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_fagperson_id = c.Guid(),
                        esas_fordeling = c.Int(),
                        esas_fordelingsnoegle_prio_1 = c.Int(),
                        esas_fordelingsnoegle_prio_2 = c.Int(),
                        esas_fordelingsnoegle_max = c.Int(),
                        esas_fordelingsnoegle_min = c.Int(),
                        esas_fordeling_prio_1 = c.Boolean(),
                        esas_fordeling_prio_2 = c.Boolean(),
                        esas_karakterskala = c.Int(),
                        esas_loebenr = c.String(),
                        esas_logisk_startdato = c.DateTimeOffset(precision: 7),
                        esas_maximum_antal_deltagere = c.Int(),
                        esas_minimum_antal_deltagere = c.Int(),
                        esas_navn = c.String(),
                        esas_kaldenavn = c.String(),
                        esas_pue_id = c.String(),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_sluttidspunkt = c.String(),
                        esas_starttidspunkt = c.String(),
                        esas_sprog_id = c.Guid(),
                        esas_tilmeldingsfrist = c.DateTimeOffset(precision: 7),
                        esas_tilmeldingslink = c.String(),
                        esas_undervisningsform = c.Int(),
                        esas_undervisning_ugedag = c.String(),
                        esas_valgfag_grupperingstype = c.Int(),
                        esas_aflysningsaarsag = c.Int(),
                        esas_indskrivningsform_id = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_gruppering_esas_uddannelseselementId = c.Guid(),
                        esas_samlaesning_esas_samlaesningId = c.Guid(),
                        Gebyrtype_esas_gebyrtypeid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_gruppering_esas_uddannelseselementId)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id)
                .ForeignKey("dbo.Samlaesning", t => t.esas_samlaesning_esas_samlaesningId)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_semester_modul_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_uddannelseselement_id)
                .ForeignKey("dbo.Gebyrtype", t => t.Gebyrtype_esas_gebyrtypeid);
            
            CreateTable(
                "dbo.StruktureltUddannelseselement",
                c => new
                    {
                        esas_uddannelseselementId = c.Guid(nullable: false),
                        esas_adgangskrav_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_afsluttet_inden_interval = c.Int(),
                        esas_aktivitet_type = c.Int(),
                        esas_antal_uger_foer_periode_start = c.Int(),
                        esas_antal_timers_undervisning = c.Int(),
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
                        esas_beskrivelse_engelsk = c.String(),
                        esas_betingelser_laast = c.Boolean(nullable: false),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_ects_kraevet = c.Decimal(precision: 18, scale: 2),
                        esas_ects_max = c.Decimal(precision: 18, scale: 2),
                        esas_ects_min = c.Decimal(precision: 18, scale: 2),
                        esas_teori_ects_staa = c.Decimal(precision: 18, scale: 2),
                        esas_praktik_ects_staa = c.Decimal(precision: 18, scale: 2),
                        esas_vaerkstedsskole_ects_staa = c.Decimal(precision: 18, scale: 2),
                        esas_eksamenssprog_id = c.Guid(),
                        esas_navn_engelsk = c.String(),
                        esas_fagkode_niveau = c.String(),
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
                        esas_overordnet_uddannelseselement_id = c.Guid(),
                        esas_paakraevet_udlandsophold = c.Boolean(),
                        esas_redigeringsgrund = c.String(),
                        esas_relateret_sue_id = c.Guid(),
                        esas_semester_nummer = c.Int(),
                        esas_skal_bestaas = c.Boolean(),
                        esas_skal_paa_bevis = c.Boolean(),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_sortering_raekkefoelge = c.Int(),
                        esas_sprog_id = c.Guid(),
                        esas_sue_nummer = c.String(),
                        esas_timevaegt = c.Decimal(precision: 18, scale: 2),
                        esas_type = c.Int(),
                        esas_udbud_af_valgfag_id = c.Guid(),
                        esas_valgfag_paa_semester = c.String(),
                        esas_udlandsophold_laast = c.Boolean(),
                        esas_delkarakter_af_id = c.Guid(),
                        esas_samlekarakter = c.Boolean(),
                        esas_uvm_fagkode = c.Int(),
                        esas_vaegtning = c.Decimal(precision: 18, scale: 2),
                        esas_valgfrit = c.Boolean(),
                        esas_staa_indberettes = c.Boolean(),
                        esas_indskrivningsform_id = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselementId)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_id);
            
            CreateTable(
                "dbo.Publicering",
                c => new
                    {
                        esas_publiceringid = c.Guid(nullable: false),
                        esas_ansoegningskortopsaetning_id = c.Guid(),
                        esas_aktiver_ansoegningssynkronisering = c.DateTimeOffset(precision: 7),
                        esas_ansoegningssynkronisering_aktiv = c.Boolean(),
                        esas_beskeder_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_bilagsupload_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_supplerende_oplysninger = c.String(),
                        esas_publiceringsmuligheder = c.String(),
                        esas_publiceringsmulighed_institutionsspecifik = c.String(),
                        esas_publiceringsperiode_fra = c.DateTimeOffset(precision: 7),
                        esas_publiceringsperiode_til = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_publiceringid)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.esas_ansoegningskortopsaetning_id);
            
            CreateTable(
                "dbo.AnsoegningskortOpsaetning",
                c => new
                    {
                        esas_ansoegningskortopsaetningid = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.Omraadenummeropsaetning",
                c => new
                    {
                        esas_omraadeopsaetningid = c.Guid(nullable: false),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_adgangskrav_id = c.Guid(),
                        esas_dimensionering_sommer = c.Int(),
                        esas_dimensionering_vinter = c.Int(),
                        esas_omraadespecialiseringsprioriteter_max = c.Int(),
                        esas_kot_grupper = c.String(),
                        esas_min_kvotient = c.Decimal(precision: 18, scale: 2),
                        esas_navn = c.String(),
                        esas_omraadenummer_id = c.Guid(),
                        esas_studiestart_sommer = c.DateTimeOffset(precision: 7),
                        esas_studiestart_vinter = c.DateTimeOffset(precision: 7),
                        esas_pladstilbudssvarfrist = c.DateTimeOffset(precision: 7),
                        esas_forventet_afslutning_vinter = c.DateTimeOffset(precision: 7),
                        esas_forventet_afslutning_sommer = c.DateTimeOffset(precision: 7),
                        esas_ledige_standbypladser = c.Int(),
                        esas_ledige_pladser_sommer = c.Int(),
                        esas_ledige_pladser_vinter = c.Int(),
                        esas_ledige_studiepladser_sommer = c.Int(),
                        esas_ledige_studiepladser_vinter = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_omraadeopsaetningid)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.Ansoegningsopsaetning", t => t.esas_ansoegningsopsaetning_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id);
            
            CreateTable(
                "dbo.Ansoegningsopsaetning",
                c => new
                    {
                        esas_ansoegningsopsaetningId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_optag_dk_optag = c.Boolean(),
                        esas_optagelsesperiode_slut = c.DateTimeOffset(precision: 7),
                        esas_optagelsesperiode_start = c.DateTimeOffset(precision: 7),
                        ownerid = c.Guid(),
                        esas_klargjort = c.Boolean(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegningsopsaetningId);
            
            CreateTable(
                "dbo.Ansoegning",
                c => new
                    {
                        esas_ansoegningId = c.Guid(nullable: false),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_person_studerende_id = c.Guid(),
                        esas_planlaegningselement_id = c.Guid(),
                        esas_virksomhed_id = c.Guid(),
                        esas_ansoeger_id = c.Guid(),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_karaktergennemsnit_genberegnet = c.Boolean(),
                        esas_ag_eksamensland_id = c.Guid(),
                        esas_eksamenstype_id = c.Guid(),
                        esas_rekvirenttype_id = c.Guid(),
                        esas_afslagsbegrundelse = c.String(),
                        esas_afslagsbegrundelsestype_id = c.Guid(),
                        esas_aarsag_til_ingen_fakturering_id = c.Guid(),
                        esas_adgangsgrundlag = c.Int(),
                        esas_su_foer_studiestart = c.DateTimeOffset(precision: 7),
                        esas_ag_danskproeve = c.Boolean(),
                        esas_ag_danskproeve_aar = c.String(),
                        esas_genindskrivningsdato = c.DateTimeOffset(precision: 7),
                        esas_danskproeve_niveau = c.String(),
                        esas_ag_eksamensaar = c.String(),
                        esas_ag_eksamensgennemsnit = c.Decimal(precision: 18, scale: 2),
                        esas_eksamensresultat = c.Decimal(precision: 18, scale: 2),
                        esas_ag_navn = c.String(),
                        esas_ag_total_points = c.Int(),
                        esas_ag_type = c.Int(),
                        esas_ag_eksamenstype = c.String(),
                        esas_ansoeger_kendt_i_esas = c.Boolean(),
                        esas_ansoegningstype = c.Int(),
                        esas_ansoegt_dato = c.DateTimeOffset(precision: 7),
                        esas_betingelser = c.String(),
                        esas_ag_beskrivelse = c.String(),
                        esas_ag_opfyldt_system = c.Boolean(),
                        esas_ag_opfyldt_manuel = c.Boolean(),
                        esas_betinget_optaget = c.Boolean(),
                        esas_di_adgangskrav = c.Boolean(),
                        esas_di_ansoegningsfrist = c.Boolean(),
                        esas_dobbelt_uddannelse = c.Boolean(),
                        esas_foretraekker_vinter = c.Boolean(),
                        esas_groenlandsk_saerordning = c.Boolean(),
                        esas_gs_i_gang = c.Boolean(),
                        esas_gs_i_gang_fag = c.String(),
                        esas_gs_tilmeldt = c.Boolean(),
                        esas_gs_tilmeldt_fag = c.String(),
                        esas_ia_agent = c.String(),
                        esas_ia_kvote3 = c.Boolean(),
                        esas_ia_opholdstilladelse_ok = c.Boolean(),
                        esas_ia_opholdstilladelse = c.Boolean(),
                        esas_ia_scholarship = c.Boolean(),
                        esas_ia_sprogtest = c.Boolean(),
                        esas_ia_st1_sendt = c.DateTimeOffset(precision: 7),
                        esas_ia_tuition_fee_betalt = c.Boolean(),
                        esas_integrationsstatus = c.Int(),
                        esas_kilde = c.Int(),
                        esas_kotid = c.String(),
                        esas_me_bemaerkninger = c.String(),
                        esas_me_dokumentation = c.Int(),
                        esas_me_erhvervserfaring = c.Boolean(),
                        esas_me_uddannelse = c.Boolean(),
                        esas_navn = c.String(),
                        esas_omraadenummer_id = c.Guid(),
                        esas_opfoelgningsdato = c.DateTimeOffset(precision: 7),
                        esas_opfylder_betingelser = c.Boolean(),
                        esas_optagelsesafgoerelse = c.Int(),
                        esas_optagelsesstatus = c.Int(),
                        esas_optagelsesstatus_dato = c.DateTimeOffset(precision: 7),
                        esas_pladstilbudssvarfrist = c.DateTimeOffset(precision: 7),
                        esas_relaterede_poster_aendret = c.DateTimeOffset(precision: 7),
                        esas_startmerit = c.Boolean(),
                        esas_baggrund_for_merit = c.String(),
                        esas_debitortype = c.Int(),
                        esas_sagsbehandlet_af_id = c.Guid(),
                        esas_sagsbehandlingsafgoerelse = c.Int(),
                        esas_sagsbehandlingsstatus = c.Int(),
                        esas_sb_adgangskrav_opfyldt = c.Boolean(),
                        esas_sb_alle_bilag_modtaget = c.Boolean(),
                        esas_sb_ansoegning_om_dispensation_dobb_udd = c.Int(),
                        esas_sb_bemaerkninger = c.String(),
                        esas_sb_bestaaede_fag_godkendt = c.Boolean(),
                        esas_sb_dokumentation_for_erhvervserfaring = c.Int(),
                        esas_sb_dokumentation_for_uddannelse = c.Int(),
                        esas_frist_eksamensbevis = c.DateTimeOffset(precision: 7),
                        esas_bemaerkning_eksamensbevis = c.String(),
                        esas_frist_manglende_dokumentation = c.DateTimeOffset(precision: 7),
                        esas_sb_efteroptag = c.Boolean(),
                        esas_sb_eksamensbevis = c.Int(),
                        esas_sb_faerdigbehandlet_dato = c.DateTimeOffset(precision: 7),
                        esas_sb_forhaandsoptaget = c.Boolean(),
                        esas_sb_motiveret_ansoegning = c.Int(),
                        esas_sb_obligatorisk_merit = c.String(),
                        esas_rykker_sendt = c.Boolean(),
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
                        esas_seneste_opdatering_integration = c.DateTimeOffset(precision: 7),
                        esas_standby = c.Boolean(),
                        esas_status_aendret_af_id = c.Guid(),
                        esas_status_dato = c.DateTimeOffset(precision: 7),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_st_ansoegning_med_saerlig_tilladelse = c.Boolean(),
                        esas_st_bemaerkninger = c.String(),
                        esas_st_dato = c.DateTimeOffset(precision: 7),
                        esas_st_journalnummer = c.String(),
                        esas_st_tidligere_saerlig_tilladelse = c.Boolean(),
                        esas_tidligere_fuldfoert_videregaaende_udd = c.Boolean(),
                        esas_tildelt_studiestart = c.DateTimeOffset(precision: 7),
                        esas_tilsagn = c.Boolean(),
                        esas_type = c.Int(),
                        esas_prioritet = c.Int(),
                        esas_betalingsbemaerkninger = c.String(),
                        esas_studieforloeb_id = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_rekvirenttype_esas_rekvirenttypeId = c.Guid(),
                        Kvalifikationspoint_esas_kvalifikationspointid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningId)
                .ForeignKey("dbo.Land", t => t.esas_ag_eksamensland_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.Ansoeger", t => t.esas_ansoeger_id)
                .ForeignKey("dbo.Ansoegningsopsaetning", t => t.esas_ansoegningsopsaetning_id)
                .ForeignKey("dbo.Eksamenstype", t => t.esas_eksamenstype_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id)
                .ForeignKey("dbo.Person", t => t.esas_person_studerende_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningselement_id)
                .ForeignKey("dbo.Rekvirenttype", t => t.esas_rekvirenttype_esas_rekvirenttypeId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_virksomhed_id)
                .ForeignKey("dbo.Kvalifikationspoint", t => t.Kvalifikationspoint_esas_kvalifikationspointid);
            
            CreateTable(
                "dbo.Ansoeger",
                c => new
                    {
                        LeadId = c.Guid(nullable: false),
                        esas_postnummer_by_id = c.Guid(),
                        esas_statsborgerskab_id = c.Guid(),
                        esas_land_id = c.Guid(),
                        FullName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        EmailAddress1 = c.String(),
                        esas_alternativt_cpr_nummer = c.String(),
                        esas_cpr_nummer = c.String(),
                        esas_eidas_pid = c.String(),
                        esas_cpr_personstatus = c.Int(),
                        esas_koen = c.Int(),
                        Telephone1 = c.String(),
                        MobilePhone = c.String(),
                        esas_navne_adressebeskyttet = c.Boolean(),
                        esas_optagelse_dk_id = c.String(),
                        esas_foedselsdato = c.DateTimeOffset(precision: 7),
                        esas_cpr_seneste_opdatering = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.LeadId)
                .ForeignKey("dbo.Land", t => t.esas_land_id)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Land", t => t.esas_statsborgerskab_id);
            
            CreateTable(
                "dbo.Eksamenstype",
                c => new
                    {
                        esas_eksamenstypeId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_id_optagelsedk = c.String(),
                        esas_adgangsgrundlag = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_eksamenstypeId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        esas_postnummer_by_id = c.Guid(),
                        esas_statsborgerskab_id = c.Guid(),
                        esas_land_id = c.Guid(),
                        esas_kommune_id = c.Guid(),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        Address1_Line3 = c.String(),
                        esas_alternativt_cpr_nummer = c.String(),
                        esas_cpr_id = c.String(),
                        esas_cpr_nummer = c.String(),
                        esas_cpr_nummer_uden_formatering = c.String(),
                        esas_cpr_personstatus = c.Int(),
                        esas_eidas_pid = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        FirstName = c.String(),
                        BirthDate = c.DateTimeOffset(precision: 7),
                        esas_cpr_seneste_opdatering = c.DateTimeOffset(precision: 7),
                        FullName = c.String(),
                        esas_personid = c.String(),
                        GenderCode = c.Int(),
                        esas_navne_addressebeskyttet = c.Boolean(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_kommune_esas_kommuneId = c.Guid(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Kommune", t => t.esas_kommune_esas_kommuneId)
                .ForeignKey("dbo.Land", t => t.esas_land_id)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Land", t => t.esas_statsborgerskab_id);
            
            CreateTable(
                "dbo.Kommune",
                c => new
                    {
                        esas_kommuneId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_kode = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kommuneId);
            
            CreateTable(
                "dbo.Rekvirenttype",
                c => new
                    {
                        esas_rekvirenttypeId = c.Guid(nullable: false),
                        esas_kategorisering = c.String(),
                        esas_navn = c.String(),
                        esas_blanketkode = c.String(),
                        esas_udloebsdato = c.DateTimeOffset(precision: 7),
                        esas_aktoer = c.String(),
                        esas_rekvirenttype = c.String(),
                        esas_ressourceudloesende = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_rekvirenttypeId);
            
            CreateTable(
                "dbo.ProeveIkkeGymEllerVideregNiveau",
                c => new
                    {
                        esas_proever_ikke_gym_eller_videreg_niveauId = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_fag = c.String(),
                        esas_niveau = c.String(),
                        esas_skriftlig_karakter = c.String(),
                        esas_mundtlig_karakter = c.String(),
                        esas_bestaaet_maaned = c.String(),
                        esas_bestaaet_aar = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_proever_ikke_gym_eller_videreg_niveauId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.SupplerendeKursus",
                c => new
                    {
                        esas_supplerende_kursusId = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_niveau = c.String(),
                        esas_fag = c.String(),
                        esas_bestaaet_aar = c.String(),
                        esas_bestaaet_maaned = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_supplerende_kursusId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.GymnasielleFagkrav",
                c => new
                    {
                        esas_gymnasielle_fagkravId = c.Guid(nullable: false),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_fagkrav_liste = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_fagkravId)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id);
            
            CreateTable(
                "dbo.GymnasielleKarakterkrav",
                c => new
                    {
                        esas_gymnasielle_karakterkravid = c.Guid(nullable: false),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_karakterkrav = c.Int(),
                        esas_fag_id = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_karakterkravid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id);
            
            CreateTable(
                "dbo.KOTGruppe",
                c => new
                    {
                        esas_kot_gruppeid = c.Guid(nullable: false),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_automatisk_tildeling = c.Boolean(nullable: false),
                        esas_betegnelse = c.String(),
                        esas_gruppenummer = c.Int(),
                        esas_navn = c.String(),
                        esas_periode_slut = c.DateTimeOffset(nullable: false, precision: 7),
                        esas_periode_start = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kot_gruppeid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id);
            
            CreateTable(
                "dbo.Kvalifikationskriterie",
                c => new
                    {
                        esas_kvalifikationskriterieid = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_max_point = c.Int(),
                        esas_navn = c.String(),
                        esas_point_deling = c.Boolean(),
                        esas_raekkefoelge = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationskriterieid);
            
            CreateTable(
                "dbo.Kvalifikationspoint",
                c => new
                    {
                        esas_kvalifikationspointid = c.Guid(nullable: false),
                        esas_kvalifikationskriterie_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_point = c.Int(),
                        esas_raekkefoelge = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationspointid)
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.esas_kvalifikationskriterie_id);
            
            CreateTable(
                "dbo.Omraadespecialisering",
                c => new
                    {
                        esas_omraadespecialiseringid = c.Guid(nullable: false),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_studieretning = c.String(),
                        esas_uddannelsesstation = c.String(),
                        esas_ledige_standbypladser = c.Int(),
                        esas_ledige_studiepladser_sommer = c.Int(),
                        esas_ledige_studiepladser_vinter = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_omraadespecialiseringid)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id);
            
            CreateTable(
                "dbo.Samlaesning",
                c => new
                    {
                        esas_samlaesningId = c.Guid(nullable: false),
                        esas_primaer_strukturelt_uddannelseselement_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_aarstal = c.String(),
                        esas_gentagende = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_primaer_strukturelt_uddannelseselement_esas_uddannelseselementId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_samlaesningId)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_primaer_strukturelt_uddannelseselement_esas_uddannelseselementId);
            
            CreateTable(
                "dbo.Studieforloeb",
                c => new
                    {
                        esas_studieforloebId = c.Guid(nullable: false),
                        esas_tidligere_uddannelsesstruktur_id = c.Guid(),
                        esas_national_afgangsaarsag_id = c.Guid(),
                        esas_studerende_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_afdeling_id = c.Guid(),
                        esas_ansoegning_id = c.Guid(),
                        esas_stamhold_id = c.Guid(),
                        esas_skabelonhold_id = c.Guid(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_eksamenstype_id = c.Guid(),
                        esas_ag_eksamensland_id = c.Guid(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_forventet_afslutning = c.DateTimeOffset(precision: 7),
                        esas_afgangsdato = c.DateTimeOffset(precision: 7),
                        esas_su_foer_studiestart = c.DateTimeOffset(precision: 7),
                        esas_tilsagn = c.Boolean(),
                        esas_navne_addressebeskyttet = c.Boolean(),
                        esas_prioritet = c.Int(),
                        esas_sammenlagt_fartstid = c.Int(),
                        esas_opnaaet_ects = c.Decimal(precision: 18, scale: 2),
                        esas_rest_ects = c.Decimal(precision: 18, scale: 2),
                        esas_udloest_staa = c.Decimal(precision: 18, scale: 2),
                        esas_resterende_staa = c.Decimal(precision: 18, scale: 2),
                        esas_teori_ects = c.Decimal(precision: 18, scale: 2),
                        esas_praktik_ects = c.Decimal(precision: 18, scale: 2),
                        esas_vaerkstedsskole_ects = c.Decimal(precision: 18, scale: 2),
                        esas_teori_staa = c.Decimal(precision: 18, scale: 2),
                        esas_praktik_staa = c.Decimal(precision: 18, scale: 2),
                        esas_vaerkstedsskole_staa = c.Decimal(precision: 18, scale: 2),
                        esas_merit_teori_ects = c.Decimal(precision: 18, scale: 2),
                        esas_merit_praktik_ects = c.Decimal(precision: 18, scale: 2),
                        esas_merit_vaerkstedsskole_ects = c.Decimal(precision: 18, scale: 2),
                        esas_merit_teori_staa = c.Decimal(precision: 18, scale: 2),
                        esas_merit_praktik_staa = c.Decimal(precision: 18, scale: 2),
                        esas_merit_vaerkstedsskole_staa = c.Decimal(precision: 18, scale: 2),
                        esas_staa_overskredet = c.Boolean(),
                        esas_internationalisering = c.Boolean(),
                        esas_talentbekendtgoerelse = c.Boolean(),
                        esas_uddannelsespaalaeg = c.Boolean(),
                        esas_cpr_nummer = c.String(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_inaktiv_status_per = c.DateTimeOffset(precision: 7),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_genindskrivningsdato = c.DateTimeOffset(precision: 7),
                        esas_audd_kode_id = c.Guid(),
                        esas_adgangsgrundlag = c.Int(),
                        esas_ag_eksamenstype = c.String(),
                        esas_ag_eksamensgennemsnit = c.Decimal(precision: 18, scale: 2),
                        esas_eksamensresultat = c.Decimal(precision: 18, scale: 2),
                        esas_ag_eksamensaar = c.String(),
                        esas_ag_total_points = c.Int(),
                        esas_ag_danskproeve = c.Boolean(),
                        esas_danskproeve_niveau = c.String(),
                        esas_ag_danskproeve_aar = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_ag_eksamensland_esas_landId = c.Guid(),
                        esas_indskrivningsform_esas_indskrivningsformId = c.Guid(),
                        esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        Hold_esas_holdId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_studieforloebId)
                .ForeignKey("dbo.Afdeling", t => t.esas_afdeling_id)
                .ForeignKey("dbo.Land", t => t.esas_ag_eksamensland_esas_landId)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Eksamenstype", t => t.esas_eksamenstype_id)
                .ForeignKey("dbo.Indskrivningsform", t => t.esas_indskrivningsform_esas_indskrivningsformId)
                .ForeignKey("dbo.NationalAfgangsaarsag", t => t.esas_national_afgangsaarsag_id)
                .ForeignKey("dbo.Hold", t => t.esas_skabelonhold_id)
                .ForeignKey("dbo.Hold", t => t.esas_stamhold_id)
                .ForeignKey("dbo.Person", t => t.esas_studerende_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Hold", t => t.Hold_esas_holdId);
            
            CreateTable(
                "dbo.Indskrivningsform",
                c => new
                    {
                        esas_indskrivningsformId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_indskrivningsform = c.Int(),
                        esas_gaestestuderende = c.Boolean(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_indskrivningsformId);
            
            CreateTable(
                "dbo.NationalAfgangsaarsag",
                c => new
                    {
                        esas_national_afgangsaarsagId = c.Guid(nullable: false),
                        esas_central_afgangsaarsag = c.Int(),
                        esas_tillad_genindskrivning = c.Boolean(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_national_afgangsaarsagId);
            
            CreateTable(
                "dbo.Internationalisering",
                c => new
                    {
                        esas_internationaliseringId = c.Guid(nullable: false),
                        esas_studieforloeb_id = c.Guid(),
                        esas_godkender_id = c.Guid(),
                        esas_institution_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_godkendelsesdato = c.DateTimeOffset(precision: 7),
                        esas_int_retning = c.Int(),
                        esas_int_opholdstype = c.Int(),
                        esas_int_udvekslingsaftale = c.Int(),
                        esas_int_udvekslingsaftaletype = c.Int(),
                        esas_semester = c.Int(),
                        esas_varighed = c.Int(),
                        esas_agent = c.String(),
                        esas_redigeringsgrund = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_godkender_esas_personoplysningerId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_internationaliseringId)
                .ForeignKey("dbo.Personoplysning", t => t.esas_godkender_esas_personoplysningerId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id);
            
            CreateTable(
                "dbo.Personoplysning",
                c => new
                    {
                        esas_personoplysningerId = c.Guid(nullable: false),
                        esas_person_id = c.Guid(),
                        esas_arbejdsemail = c.String(),
                        esas_arbejdstelefonnummer = c.String(),
                        esas_eksternt_link = c.String(),
                        esas_fagpersontype = c.Int(),
                        esas_integration_id = c.String(),
                        esas_kaldenavn = c.String(),
                        esas_lokalt_studienummer = c.String(),
                        esas_mobiltelefonnummer = c.String(),
                        esas_navn = c.String(),
                        esas_privatemail = c.String(),
                        esas_privattelefonnummer = c.String(),
                        esas_rolle = c.Int(),
                        esas_studieemail = c.String(),
                        esas_sso_id = c.String(),
                        esas_dokumentafsendelsesmetode = c.Int(),
                        esas_stillingsbetegnelse = c.String(),
                        esas_sidst_faktureret = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_personoplysningerId)
                .ForeignKey("dbo.Person", t => t.esas_person_id);
            
            CreateTable(
                "dbo.MeritRegistrering",
                c => new
                    {
                        esas_meritregistreringId = c.Guid(nullable: false),
                        esas_godkender_id = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_karakter_id = c.Guid(),
                        esas_aktivitetstype_id = c.Guid(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_bedoemmelsesdato = c.DateTimeOffset(precision: 7),
                        esas_fra_institution_id = c.Guid(),
                        esas_bedoemmelse_id = c.Guid(),
                        esas_titel_dansk = c.String(),
                        esas_titel_engelsk = c.String(),
                        esas_studietidsforkortende = c.Boolean(),
                        esas_navn = c.String(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_godkendelsesdato = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_godkender_esas_personoplysningerId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_meritregistreringId)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_id)
                .ForeignKey("dbo.Personoplysning", t => t.esas_godkender_esas_personoplysningerId)
                .ForeignKey("dbo.Karakter", t => t.esas_karakter_id);
            
            CreateTable(
                "dbo.GennemfoerelsesUddannelseselement",
                c => new
                    {
                        esas_uddannelseselement_gennemfoerelseId = c.Guid(nullable: false),
                        esas_pue_id = c.Guid(),
                        esas_uddannelseselement_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_hold_id = c.Guid(),
                        esas_bedoemmelsesresultat_id = c.Guid(),
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_udbud_af_valgfag_id = c.Guid(),
                        esas_antal_forsoeg = c.Int(),
                        esas_aktivitet_type = c.Int(),
                        esas_antal_merit_registreringer = c.Int(),
                        esas_bedoemmelsesresultat_7_trin = c.Int(),
                        esas_bedoemmelsesresultat_bestaaet_ikke = c.Int(),
                        esas_beregnet_bestaaet = c.Boolean(),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_navn = c.String(),
                        esas_registreret_merit = c.Boolean(),
                        esas_semester_nummer = c.Int(nullable: false),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_uvm_fagkode = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_hold_esas_holdId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Karakter", t => t.esas_bedoemmelsesresultat_id)
                .ForeignKey("dbo.Hold", t => t.esas_hold_esas_holdId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_pue_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_udbud_af_valgfag_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_uddannelseselement_id);
            
            CreateTable(
                "dbo.Karakter",
                c => new
                    {
                        esas_karakterId = c.Guid(nullable: false),
                        esas_bestaaet = c.Boolean(nullable: false),
                        esas_karakterskala = c.Int(),
                        esas_taeller_som_forsoeg = c.Boolean(nullable: false),
                        esas_karakter = c.String(),
                        esas_name = c.String(),
                        esas_karakter1 = c.String(),
                        esas_oversaettelse = c.String(),
                        esas_ects_skala = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_karakterId);
            
            CreateTable(
                "dbo.Praktikophold",
                c => new
                    {
                        esas_praktikopholdId = c.Guid(nullable: false),
                        esas_studieforloeb_id = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_praktiksted_id = c.Guid(),
                        esas_praktikomraade_id = c.Guid(),
                        esas_praktikvejleder_id = c.Guid(),
                        esas_fartstid = c.Int(),
                        esas_loennet = c.Boolean(),
                        esas_navn = c.String(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_type = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_praktikopholdId)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_id)
                .ForeignKey("dbo.Praktikomraade", t => t.esas_praktikomraade_id)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_praktiksted_id)
                .ForeignKey("dbo.Person", t => t.esas_praktikvejleder_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id);
            
            CreateTable(
                "dbo.Praktikomraade",
                c => new
                    {
                        esas_praktikomraadeId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_beskrivelse = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_praktikomraadeId);
            
            CreateTable(
                "dbo.Afdelingsniveau",
                c => new
                    {
                        esas_afdelingsniveauId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_afdelingsniveauId);
            
            CreateTable(
                "dbo.Afslagsbegrundelse",
                c => new
                    {
                        esas_afslagsbegrundelseId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_afslagsbegrundelseId);
            
            CreateTable(
                "dbo.AndenAktivitet",
                c => new
                    {
                        esas_ansoegning_andre_aktiviteterid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_institution_organisation = c.String(),
                        esas_aktivitet = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_tid = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_andre_aktiviteterid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.Ansoegningshandling",
                c => new
                    {
                        esas_ansoegningshandlingId = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_navn_engelsk = c.String(),
                        esas_beskrivelse = c.String(),
                        esas_beskrivelse_engelsk = c.String(),
                        esas_deadline = c.DateTimeOffset(precision: 7),
                        esas_type = c.Int(),
                        esas_valgmulighed = c.Int(),
                        esas_valgtype = c.Int(),
                        esas_valg = c.Int(),
                        esas_udfoert_af_ansoeger = c.DateTimeOffset(precision: 7),
                        esas_bilagskategori = c.Int(),
                        esas_oprettelsesaarsag = c.Int(),
                        esas_ansoegningshandlingsgruppe = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegningshandlingId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.Ansoegningskort",
                c => new
                    {
                        esas_ansoegningskortid = c.Guid(nullable: false),
                        esas_ansoegningskortopsaetning_id = c.Guid(),
                        esas_ansoegningskorttekst_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_obligatorisk = c.Boolean(),
                        esas_skal_vises = c.Boolean(),
                        esas_type = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskortid)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.esas_ansoegningskortopsaetning_id)
                .ForeignKey("dbo.AnsoegningskortTekst", t => t.esas_ansoegningskorttekst_id);
            
            CreateTable(
                "dbo.AnsoegningskortTekst",
                c => new
                    {
                        esas_ansoegningskorttekstid = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_hjaelpetekst_dansk = c.String(),
                        esas_hjaelpetekst_engelsk = c.String(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskorttekstid);
            
            CreateTable(
                "dbo.Bedoemmelse",
                c => new
                    {
                        esas_bedoemmelseId = c.Guid(nullable: false),
                        esas_bedoemmelsesrunde_id = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_karakter_id = c.Guid(),
                        esas_ansoegning_id = c.Guid(),
                        esas_bedoemmelse_godkendt_af_id = c.Guid(),
                        esas_bedoemmelse_registreret_af_id = c.Guid(),
                        esas_bedoemmelses_nummer = c.String(),
                        esas_bedmmelses_nummer_bool = c.Boolean(),
                        esas_titel = c.String(),
                        esas_bedoemmelsesdato = c.DateTimeOffset(precision: 7),
                        esas_bedoemt_ikke_afmeldt = c.Boolean(),
                        esas_taeller_som_forsoeg = c.Boolean(),
                        esas_bedoemmelsesform_mundtlig = c.Boolean(),
                        esas_bedoemmelsesform_skriftlig = c.Boolean(),
                        esas_bedoemmelsesform_praktisk_proeve = c.Boolean(),
                        esas_bedoemmelsesform_projekt = c.Boolean(),
                        esas_bedoemmelsesform_gennemfoerelse = c.Boolean(),
                        esas_bedoemmelsesform_kmb = c.Boolean(),
                        esas_bedoemmelsesform_multiple_choice_test = c.Boolean(),
                        esas_bedoemmelsesform_realkompetencevurdering = c.Boolean(),
                        esas_bedoemmelsestype = c.Int(),
                        esas_bestaaet = c.Boolean(),
                        esas_engelsk_titel = c.String(),
                        esas_evaluering = c.String(),
                        esas_godkendt = c.Boolean(),
                        esas_loebenummer = c.String(),
                        esas_navn = c.String(),
                        esas_oevrige_skala = c.Int(),
                        esas_redigeringsgrund = c.Int(),
                        esas_sprog_id = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_ansoegning_esas_ansoegningId = c.Guid(),
                        esas_bedoemmelse_godkendt_af_SystemUserId = c.Guid(),
                        esas_bedoemmelse_registreret_af_SystemUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelseId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_esas_ansoegningId)
                .ForeignKey("dbo.SystemUser", t => t.esas_bedoemmelse_godkendt_af_SystemUserId)
                .ForeignKey("dbo.SystemUser", t => t.esas_bedoemmelse_registreret_af_SystemUserId)
                .ForeignKey("dbo.Bedoemmelsesrunde", t => t.esas_bedoemmelsesrunde_id)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_id)
                .ForeignKey("dbo.Karakter", t => t.esas_karakter_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id);
            
            CreateTable(
                "dbo.SystemUser",
                c => new
                    {
                        SystemUserId = c.Guid(nullable: false),
                        FullName = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.SystemUserId);
            
            CreateTable(
                "dbo.Bedoemmelsesrunde",
                c => new
                    {
                        esas_bedoemmelsesrundeId = c.Guid(nullable: false),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        esas_omraadeopsaetning_id = c.Guid(),
                        esas_omraadespecialisering_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_nummer = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelsesrundeId)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadeopsaetning_id)
                .ForeignKey("dbo.Omraadespecialisering", t => t.esas_omraadespecialisering_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id);
            
            CreateTable(
                "dbo.Bilag",
                c => new
                    {
                        esas_bilagid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_original_filnavn = c.String(),
                        esas_fil_url = c.String(),
                        esas_fil_content_type = c.String(),
                        esas_sidst_hentet = c.DateTimeOffset(precision: 7),
                        esas_bilagskategorier = c.String(),
                        esas_laest = c.Boolean(),
                        esas_optagelse_dk_fil_id = c.String(),
                        esas_filstoerrelse_mb = c.Double(),
                        esas_upload_dato = c.DateTimeOffset(precision: 7),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_bilagid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.Enkeltfag",
                c => new
                    {
                        esas_ansoegning_enkeltfagid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_bevistype = c.String(),
                        esas_institution = c.String(),
                        esas_eksamenstype = c.String(),
                        esas_niveau = c.String(),
                        esas_karakter = c.String(),
                        esas_karakterskala = c.String(),
                        esas_termin_aar = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_enkeltfagid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id);
            
            CreateTable(
                "dbo.Erfaringer",
                c => new
                    {
                        esas_ansoegning_erfaringerid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_arbejdsgiver = c.String(),
                        esas_arbejdsart = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_tid = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_erfaringerid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.EsasSyncResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SyncStartTimeUTC = c.DateTime(nullable: false),
                        esasLoadResult_EsasLoadStatus = c.Int(nullable: false),
                        esasLoadResult_LoadTimeMs = c.Long(),
                        esasLoadResult_LoadStartTimeUTC = c.DateTime(),
                        esasLoadResult_LoadEndTimeUTC = c.DateTime(),
                        esasLoadResult_NumberOfObjectsLoaded = c.Int(),
                        esasLoadResult_ModifiedOnDateTimeUTC = c.DateTime(),
                        esasLoadResult_LoaderStrategyName = c.String(),
                        esasLoadResult_Message = c.String(),
                        esasLoadResult_HasMoreRecords = c.Boolean(nullable: false),
                        esasSendResult_SendToDestinationStatus = c.Int(nullable: false),
                        esasSendResult_SendTimeMs = c.Long(),
                        esasSendResult_SendStartTimeUTC = c.DateTime(),
                        esasSendResult_SendEndTimeUTC = c.DateTime(),
                        esasSendResult_SendDestinationStrategyName = c.String(),
                        Message = c.String(),
                        SyncStrategyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fagpersonsrelation",
                c => new
                    {
                        esas_fagpersonsrelationId = c.Guid(nullable: false),
                        esas_bedoemmelse_id = c.Guid(),
                        esas_fagperson_id = c.Guid(),
                        esas_hold_id = c.Guid(),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        esas_navn = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_fagpersonsrelationId)
                .ForeignKey("dbo.Bedoemmelse", t => t.esas_bedoemmelse_id)
                .ForeignKey("dbo.Personoplysning", t => t.esas_fagperson_id)
                .ForeignKey("dbo.Hold", t => t.esas_hold_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id);
            
            CreateTable(
                "dbo.Fravaersaarsag",
                c => new
                    {
                        esas_fravaersaarsagId = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_navn = c.String(),
                        esas_paavirker_su = c.Int(),
                        esas_paavirker_suname = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_fravaersaarsagId);
            
            CreateTable(
                "dbo.Gebyrtype",
                c => new
                    {
                        esas_gebyrtypeid = c.Guid(nullable: false),
                        esas_beloeb = c.Decimal(precision: 18, scale: 2),
                        esas_beloeb_valuta = c.String(),
                        esas_beskrivelse = c.String(),
                        esas_debitortype = c.Int(),
                        esas_kategori = c.Int(),
                        esas_kontostreng_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_skabelon_id = c.Guid(),
                        esas_type = c.Int(nullable: false),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_gebyrtypeid);
            
            CreateTable(
                "dbo.EsasWebServiceHealthCheck",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        CheckTime = c.DateTime(nullable: false),
                        HttpStatusCode = c.String(),
                        Message = c.String(),
                        CheckTimeMs = c.Long(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Kommunikation",
                c => new
                    {
                        esas_kommunikationId = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_besked_tilladt_i_svar = c.Int(),
                        esas_bilag_tilladt_i_svar = c.Int(),
                        esas_kilde = c.Int(),
                        esas_laest_af_bruger = c.DateTimeOffset(precision: 7),
                        esas_laest_af_sagsbehandler = c.DateTimeOffset(precision: 7),
                        esas_meddelelse = c.String(),
                        esas_modtager_email = c.String(),
                        esas_navn = c.String(),
                        esas_type = c.Int(),
                        esas_fejlbeskrivelse = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kommunikationId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.KOTGruppeTilmelding",
                c => new
                    {
                        esas_kot_gruppe_tilmeldingid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_kot_gruppe_id = c.Guid(),
                        esas_ventelistenummer = c.Int(),
                        esas_kot_status = c.Int(),
                        esas_kot_status_kode = c.Int(),
                        esas_navn = c.String(),
                        esas_kot_bemaerkning = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_kot_gruppe_tilmeldingid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.KOTGruppe", t => t.esas_kot_gruppe_id);
            
            CreateTable(
                "dbo.KurserSkoleophold",
                c => new
                    {
                        esas_ansoegning_kurser_og_skoleopholdid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_skole = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_kurser_og_skoleopholdid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.Omraadenummer",
                c => new
                    {
                        esas_omraadenummerId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_omraadenummer = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_omraadenummerId);
            
            CreateTable(
                "dbo.OptionSetValueString",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AttributeName = c.String(),
                        Value = c.Int(nullable: false),
                        Label = c.String(),
                        EntityName = c.String(),
                        EntityType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Proeve",
                c => new
                    {
                        esas_ansoegning_proeveid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_type = c.String(),
                        esas_fag = c.String(),
                        esas_bestaaet = c.String(),
                        esas_bestaaet_aar = c.String(),
                        esas_niveau = c.String(),
                        esas_skriftlig_karakter = c.String(),
                        esas_mundlig_karakter = c.String(),
                        esas_from_eksamensdatabasen = c.Boolean(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_proeveid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.RelationsStatus",
                c => new
                    {
                        esas_relations_statusId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_parentid = c.String(),
                        esas_childid = c.String(),
                        esas_relation = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_relations_statusId);
            
            CreateTable(
                "dbo.Specialisering",
                c => new
                    {
                        esas_ansoegning_specialiseringid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_omraadespecialisering_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_oensket_prioritet = c.Int(nullable: false),
                        esas_tildelt_prioritet = c.Int(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_specialiseringid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Omraadespecialisering", t => t.esas_omraadespecialisering_id);
            
            CreateTable(
                "dbo.StudieinaktivPeriode",
                c => new
                    {
                        esas_studieinaktiv_periodeId = c.Guid(nullable: false),
                        esas_studieforloeb_id = c.Guid(),
                        esas_aarsag_id = c.Guid(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_idag = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_semester = c.Int(),
                        esas_indberettet_til_su = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_studieinaktiv_periodeId)
                .ForeignKey("dbo.Fravaersaarsag", t => t.esas_aarsag_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id);
            
            CreateTable(
                "dbo.UdlandsopholdAnsoegning",
                c => new
                    {
                        esas_ansoegning_udlandsopholdid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_land_id = c.Guid(),
                        esas_aktivitet = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_udlandsopholdid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.VideregaaendeUddannelse",
                c => new
                    {
                        esas_ansoegning_videregaaende_uddannelseid = c.Guid(nullable: false),
                        esas_ansoegning_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_optagelsesomraadenavn = c.String(),
                        esas_institutionsnavn = c.String(),
                        esas_paabegyndt_aar = c.String(),
                        esas_bestaaet_aar = c.String(),
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_stadig_optaget = c.Boolean(),
                        esas_afbrudt_aar = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_videregaaende_uddannelseid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id);
            
            CreateTable(
                "dbo.Bevisgrundlag",
                c => new
                    {
                        esas_bevisgrundlagId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_xml = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_bevisgrundlagId);
            
            CreateTable(
                "dbo.KvalifikationskriterieOmraadenummeropsaetning",
                c => new
                    {
                        Kvalifikationskriterie_esas_kvalifikationskriterieid = c.Guid(nullable: false),
                        Omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Kvalifikationskriterie_esas_kvalifikationskriterieid, t.Omraadenummeropsaetning_esas_omraadeopsaetningid })
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.Kvalifikationskriterie_esas_kvalifikationskriterieid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideregaaendeUddannelse", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.UdlandsopholdAnsoegning", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.StudieinaktivPeriode", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.StudieinaktivPeriode", "esas_aarsag_id", "dbo.Fravaersaarsag");
            DropForeignKey("dbo.Specialisering", "esas_omraadespecialisering_id", "dbo.Omraadespecialisering");
            DropForeignKey("dbo.Specialisering", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Proeve", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.KurserSkoleophold", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.KOTGruppeTilmelding", "esas_kot_gruppe_id", "dbo.KOTGruppe");
            DropForeignKey("dbo.KOTGruppeTilmelding", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Kommunikation", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "Gebyrtype_esas_gebyrtypeid", "dbo.Gebyrtype");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_hold_id", "dbo.Hold");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_fagperson_id", "dbo.Personoplysning");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_bedoemmelse_id", "dbo.Bedoemmelse");
            DropForeignKey("dbo.Erfaringer", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Enkeltfag", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Enkeltfag", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Bilag", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Bedoemmelse", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Bedoemmelse", "esas_karakter_id", "dbo.Karakter");
            DropForeignKey("dbo.Bedoemmelse", "esas_gennemfoerelsesuddannelseselement_id", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelsesrunde_id", "dbo.Bedoemmelsesrunde");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_omraadespecialisering_id", "dbo.Omraadespecialisering");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_omraadeopsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelse_registreret_af_SystemUserId", "dbo.SystemUser");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelse_godkendt_af_SystemUserId", "dbo.SystemUser");
            DropForeignKey("dbo.Bedoemmelse", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Ansoegningskort", "esas_ansoegningskorttekst_id", "dbo.AnsoegningskortTekst");
            DropForeignKey("dbo.Ansoegningskort", "esas_ansoegningskortopsaetning_id", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.Ansoegningshandling", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.AndenAktivitet", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Afdeling", "esas_overordnet_afdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Afdeling", "esas_afdelingsniveau_esas_afdelingsniveauId", "dbo.Afdelingsniveau");
            DropForeignKey("dbo.Afdeling", "esas_account_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Praktikophold", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Praktikophold", "esas_praktikvejleder_id", "dbo.Person");
            DropForeignKey("dbo.Praktikophold", "esas_praktiksted_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Praktikophold", "esas_praktikomraade_id", "dbo.Praktikomraade");
            DropForeignKey("dbo.Praktikophold", "esas_gennemfoerelsesuddannelseselement_id", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.Internationalisering", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Internationalisering", "esas_institution_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.MeritRegistrering", "esas_karakter_id", "dbo.Karakter");
            DropForeignKey("dbo.MeritRegistrering", "esas_godkender_esas_personoplysningerId", "dbo.Personoplysning");
            DropForeignKey("dbo.MeritRegistrering", "esas_gennemfoerelsesuddannelseselement_id", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_uddannelseselement_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_udbud_af_valgfag_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_pue_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_hold_esas_holdId", "dbo.Hold");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_bedoemmelsesresultat_id", "dbo.Karakter");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Internationalisering", "esas_godkender_esas_personoplysningerId", "dbo.Personoplysning");
            DropForeignKey("dbo.Hold", "Personoplysning_esas_personoplysningerId", "dbo.Personoplysning");
            DropForeignKey("dbo.Personoplysning", "esas_person_id", "dbo.Person");
            DropForeignKey("dbo.Institutionsoplysninger", "InstitutionVirksomhed_AccountId", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Studieforloeb", "Hold_esas_holdId", "dbo.Hold");
            DropForeignKey("dbo.Studieforloeb", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Studieforloeb", "esas_tidligere_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Studieforloeb", "esas_studerende_id", "dbo.Person");
            DropForeignKey("dbo.Studieforloeb", "esas_stamhold_id", "dbo.Hold");
            DropForeignKey("dbo.Studieforloeb", "esas_skabelonhold_id", "dbo.Hold");
            DropForeignKey("dbo.Studieforloeb", "esas_national_afgangsaarsag_id", "dbo.NationalAfgangsaarsag");
            DropForeignKey("dbo.Studieforloeb", "esas_indskrivningsform_esas_indskrivningsformId", "dbo.Indskrivningsform");
            DropForeignKey("dbo.Studieforloeb", "esas_eksamenstype_id", "dbo.Eksamenstype");
            DropForeignKey("dbo.Studieforloeb", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Studieforloeb", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Studieforloeb", "esas_ag_eksamensland_esas_landId", "dbo.Land");
            DropForeignKey("dbo.Studieforloeb", "esas_afdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Hold", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.Hold", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_uddannelseselement_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_semester_modul_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_samlaesning_esas_samlaesningId", "dbo.Samlaesning");
            DropForeignKey("dbo.Samlaesning", "esas_primaer_strukturelt_uddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_gruppering_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.Omraadespecialisering", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Omraadespecialisering", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "Omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "Kvalifikationskriterie_esas_kvalifikationskriterieid", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.Ansoegning", "Kvalifikationspoint_esas_kvalifikationspointid", "dbo.Kvalifikationspoint");
            DropForeignKey("dbo.Kvalifikationspoint", "esas_kvalifikationskriterie_id", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.KOTGruppe", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleKarakterkrav", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleFagkrav", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_ansoegningsopsaetning_id", "dbo.Ansoegningsopsaetning");
            DropForeignKey("dbo.SupplerendeKursus", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.ProeveIkkeGymEllerVideregNiveau", "esas_ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Ansoegning", "esas_virksomhed_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Ansoegning", "esas_rekvirenttype_esas_rekvirenttypeId", "dbo.Rekvirenttype");
            DropForeignKey("dbo.Ansoegning", "esas_planlaegningselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Ansoegning", "esas_person_studerende_id", "dbo.Person");
            DropForeignKey("dbo.Person", "esas_statsborgerskab_id", "dbo.Land");
            DropForeignKey("dbo.Person", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.Person", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.Person", "esas_kommune_esas_kommuneId", "dbo.Kommune");
            DropForeignKey("dbo.Ansoegning", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Ansoegning", "esas_eksamenstype_id", "dbo.Eksamenstype");
            DropForeignKey("dbo.Ansoegning", "esas_ansoegningsopsaetning_id", "dbo.Ansoegningsopsaetning");
            DropForeignKey("dbo.Ansoegning", "esas_ansoeger_id", "dbo.Ansoeger");
            DropForeignKey("dbo.Ansoeger", "esas_statsborgerskab_id", "dbo.Land");
            DropForeignKey("dbo.Ansoeger", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.Ansoeger", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.Ansoegning", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Ansoegning", "esas_ag_eksamensland_id", "dbo.Land");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_adgangskrav_id", "dbo.Adgangskrav");
            DropForeignKey("dbo.Publicering", "esas_ansoegningskortopsaetning_id", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_adgangskrav_id", "dbo.Adgangskrav");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_adgangskrav_id", "dbo.Adgangskrav");
            DropForeignKey("dbo.Hold", "esas_institution_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Hold", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Uddannelsesstruktur", "esas_uddannelsesaktivitet_id", "dbo.Uddannelsesaktivitet");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_institutionsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Hold", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_vist_institutionsoplysning_id", "dbo.Institutionsoplysninger");
            DropForeignKey("dbo.InstitutionVirksomhed", "Institutionsoplysninger_esas_institutionsoplysningerId", "dbo.Institutionsoplysninger");
            DropForeignKey("dbo.Institutionsoplysninger", "esas_institution_virksomhed_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.Postnummer", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_branche_id", "dbo.Branche");
            DropTable("dbo.KvalifikationskriterieOmraadenummeropsaetning");
            DropTable("dbo.Bevisgrundlag");
            DropTable("dbo.VideregaaendeUddannelse");
            DropTable("dbo.UdlandsopholdAnsoegning");
            DropTable("dbo.StudieinaktivPeriode");
            DropTable("dbo.Specialisering");
            DropTable("dbo.RelationsStatus");
            DropTable("dbo.Proeve");
            DropTable("dbo.OptionSetValueString");
            DropTable("dbo.Omraadenummer");
            DropTable("dbo.KurserSkoleophold");
            DropTable("dbo.KOTGruppeTilmelding");
            DropTable("dbo.Kommunikation");
            DropTable("dbo.EsasWebServiceHealthCheck");
            DropTable("dbo.Gebyrtype");
            DropTable("dbo.Fravaersaarsag");
            DropTable("dbo.Fagpersonsrelation");
            DropTable("dbo.EsasSyncResult");
            DropTable("dbo.Erfaringer");
            DropTable("dbo.Enkeltfag");
            DropTable("dbo.Bilag");
            DropTable("dbo.Bedoemmelsesrunde");
            DropTable("dbo.SystemUser");
            DropTable("dbo.Bedoemmelse");
            DropTable("dbo.AnsoegningskortTekst");
            DropTable("dbo.Ansoegningskort");
            DropTable("dbo.Ansoegningshandling");
            DropTable("dbo.AndenAktivitet");
            DropTable("dbo.Afslagsbegrundelse");
            DropTable("dbo.Afdelingsniveau");
            DropTable("dbo.Praktikomraade");
            DropTable("dbo.Praktikophold");
            DropTable("dbo.Karakter");
            DropTable("dbo.GennemfoerelsesUddannelseselement");
            DropTable("dbo.MeritRegistrering");
            DropTable("dbo.Personoplysning");
            DropTable("dbo.Internationalisering");
            DropTable("dbo.NationalAfgangsaarsag");
            DropTable("dbo.Indskrivningsform");
            DropTable("dbo.Studieforloeb");
            DropTable("dbo.Samlaesning");
            DropTable("dbo.Omraadespecialisering");
            DropTable("dbo.Kvalifikationspoint");
            DropTable("dbo.Kvalifikationskriterie");
            DropTable("dbo.KOTGruppe");
            DropTable("dbo.GymnasielleKarakterkrav");
            DropTable("dbo.GymnasielleFagkrav");
            DropTable("dbo.SupplerendeKursus");
            DropTable("dbo.ProeveIkkeGymEllerVideregNiveau");
            DropTable("dbo.Rekvirenttype");
            DropTable("dbo.Kommune");
            DropTable("dbo.Person");
            DropTable("dbo.Eksamenstype");
            DropTable("dbo.Ansoeger");
            DropTable("dbo.Ansoegning");
            DropTable("dbo.Ansoegningsopsaetning");
            DropTable("dbo.Omraadenummeropsaetning");
            DropTable("dbo.AnsoegningskortOpsaetning");
            DropTable("dbo.Publicering");
            DropTable("dbo.StruktureltUddannelseselement");
            DropTable("dbo.PlanlaegningsUddannelseselement");
            DropTable("dbo.Uddannelsesaktivitet");
            DropTable("dbo.Uddannelsesstruktur");
            DropTable("dbo.Aktivitetsudbud");
            DropTable("dbo.Hold");
            DropTable("dbo.Institutionsoplysninger");
            DropTable("dbo.Postnummer");
            DropTable("dbo.Land");
            DropTable("dbo.Branche");
            DropTable("dbo.InstitutionVirksomhed");
            DropTable("dbo.Afdeling");
            DropTable("dbo.Adgangskrav");
        }
    }
}
