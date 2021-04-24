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
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsUddannelseselement_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_struktureltUddannelseselement_id)
                .Index(t => t.esas_omraadenummeropsaetning_id)
                .Index(t => t.esas_planlaegningsUddannelseselement_id)
                .Index(t => t.esas_struktureltUddannelseselement_id);
            
            CreateTable(
                "dbo.Omraadenummeropsaetning",
                c => new
                    {
                        esas_omraadeopsaetningid = c.Guid(nullable: false),
                        esas_ansoegningsopsaetning_id = c.Guid(),
                        esas_dimensionering_sommer = c.Int(),
                        esas_dimensionering_vinter = c.Int(),
                        esas_kot_grupper = c.String(),
                        esas_min_kvotient = c.Decimal(precision: 18, scale: 2),
                        esas_navn = c.String(),
                        esas_omraadespecialiseringsprioriteter_max = c.Int(),
                        esas_omraadenummer_id = c.Guid(),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
                        esas_studiestart_sommer = c.DateTimeOffset(precision: 7),
                        esas_studiestart_vinter = c.DateTimeOffset(precision: 7),
                        esas_pladstilbudssvarfrist = c.DateTimeOffset(precision: 7),
                        esas_type = c.Int(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_adgangskrav_id = c.Guid(),
                        esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                        esas_publicering_esas_publiceringid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_omraadeopsaetningid)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.Ansoegningsopsaetning", t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_esas_publiceringid)
                .Index(t => t.esas_aktivitetsudbud_id)
                .Index(t => t.esas_adgangskrav_id)
                .Index(t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.esas_publicering_esas_publiceringid);
            
            CreateTable(
                "dbo.Aktivitetsudbud",
                c => new
                    {
                        esas_aktivitetsudbudId = c.Guid(nullable: false),
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_institutionsafdeling_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_ophoersdato = c.DateTimeOffset(precision: 7),
                        esas_periode_for_lige_semestre_sommer_idName = c.String(),
                        esas_periode_for_lige_semestre_vinter_idName = c.String(),
                        esas_periode_for_ulige_semestre_sommer_idName = c.String(),
                        esas_periode_for_ulige_semestre_vinter_idName = c.String(),
                        esas_sprog_idName = c.String(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_aktivitetsudbudId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Afdeling", t => t.esas_institutionsafdeling_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_id)
                .Index(t => t.esas_aktivitetsafdeling_id)
                .Index(t => t.esas_institutionsafdeling_id)
                .Index(t => t.esas_uddannelsesstruktur_id);
            
            CreateTable(
                "dbo.Afdeling",
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
                        esas_periode_lige_semester_sommer_idName = c.String(),
                        esas_periode_ulige_semester_sommer_idName = c.String(),
                        esas_periode_lige_semester_vinter_idName = c.String(),
                        esas_periode_ulige_semester_vinter_idName = c.String(),
                        esas_alarmer_om_uhentede_bilag = c.Int(),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_antal_uger_foer_periode_start = c.Int(),
                        esas_educational_institution = c.String(),
                        esas_haandtering_af_studienummer = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_overordnet_afdeling_esas_afdelingId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_afdelingId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_account_id)
                .ForeignKey("dbo.Afdeling", t => t.esas_overordnet_afdeling_esas_afdelingId)
                .Index(t => t.esas_account_id)
                .Index(t => t.esas_overordnet_afdeling_esas_afdelingId);
            
            CreateTable(
                "dbo.InstitutionVirksomhed",
                c => new
                    {
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
                        esas_vist_institutionsoplysning_id = c.Guid(),
                        ParentAccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Branche", t => t.esas_branche_id)
                .ForeignKey("dbo.Land", t => t.esas_land_id)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Institutionsoplysninger", t => t.esas_vist_institutionsoplysning_id)
                .Index(t => t.esas_branche_id)
                .Index(t => t.esas_land_id)
                .Index(t => t.esas_postnummer_by_id)
                .Index(t => t.esas_vist_institutionsoplysning_id);
            
            CreateTable(
                "dbo.Branche",
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
                "dbo.Land",
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
                "dbo.Ansoeger",
                c => new
                    {
                        LeadId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        Address1_Line1 = c.String(),
                        Address1_Line2 = c.String(),
                        esas_land_id = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        Telephone1 = c.String(),
                        MobilePhone = c.String(),
                        EmailAddress1 = c.String(),
                        esas_person_id = c.Guid(),
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
                        esas_cpr_seneste_opdatering = c.DateTimeOffset(precision: 7),
                        esas_cpr_personstatus = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_land_esas_landId = c.Guid(),
                        esas_postnummer_by_esas_postnummerId = c.Guid(),
                        esas_statsborgerskab_esas_landId = c.Guid(),
                        Land_esas_landId = c.Guid(),
                    })
                .PrimaryKey(t => t.LeadId)
                .ForeignKey("dbo.Land", t => t.esas_land_esas_landId)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_esas_postnummerId)
                .ForeignKey("dbo.Land", t => t.esas_statsborgerskab_esas_landId)
                .ForeignKey("dbo.Land", t => t.Land_esas_landId)
                .Index(t => t.esas_land_esas_landId)
                .Index(t => t.esas_postnummer_by_esas_postnummerId)
                .Index(t => t.esas_statsborgerskab_esas_landId)
                .Index(t => t.Land_esas_landId);
            
            CreateTable(
                "dbo.Postnummer",
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
                .ForeignKey("dbo.Land", t => t.esas_land_esas_landId)
                .Index(t => t.esas_land_esas_landId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
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
                        statuscode = c.Int(),
                        GenderCode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        StateCode = c.Int(),
                        esas_cpr_nummer = c.String(),
                        esas_cpr_id = c.String(),
                        esas_alternativt_cpr_nummer = c.String(),
                        esas_personid = c.String(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_postnummer_by_idName = c.String(),
                        esas_cpr_nummer_uden_formatering = c.String(),
                        esas_cpr_personstatus = c.Int(),
                        esas_cpr_seneste_opdatering = c.DateTimeOffset(precision: 7),
                        esas_kommune_id = c.Guid(),
                        esas_kommune_idName = c.String(),
                        esas_statsborgerskab_id = c.Guid(),
                        esas_statsborgerskab_idName = c.String(),
                        esas_land_id = c.Guid(),
                        esas_land_idName = c.String(),
                        esas_navne_addressebeskyttet = c.Boolean(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Land", t => t.esas_land_id)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Land", t => t.esas_statsborgerskab_id)
                .Index(t => t.esas_postnummer_by_id)
                .Index(t => t.esas_statsborgerskab_id)
                .Index(t => t.esas_land_id);
            
            CreateTable(
                "dbo.Ansoegning",
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
                        esas_ag_beskrivelse = c.String(),
                        esas_ag_opfyldt_manuel = c.Boolean(),
                        esas_ag_opfyldt_system = c.Boolean(),
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
                        esas_betalingsbemaerkninger = c.String(),
                        esas_uddannelsesstation = c.String(),
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_integrationsstatus = c.Int(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_opfoelgningsdato = c.DateTimeOffset(precision: 7),
                        esas_opfylder_betingelser = c.Boolean(),
                        esas_optagelsesafgoerelse = c.Int(),
                        esas_optagelsesstatus = c.Int(),
                        esas_optagelsesstatus_dato = c.DateTimeOffset(precision: 7),
                        esas_pladstilbudssvarfrist = c.DateTimeOffset(precision: 7),
                        esas_relaterede_poster_aendret = c.DateTimeOffset(precision: 7),
                        esas_sagsbehandlingsafgoerelse = c.Int(),
                        esas_sagsbehandlingsstatus = c.Int(),
                        esas_prioritet = c.Int(),
                        esas_omraadespecialisering_prioriteter = c.String(),
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
                        esas_adgangsgrundlag = c.Int(),
                        esas_ag_danskproeve = c.Boolean(),
                        esas_ag_danskproeve_aar = c.String(),
                        esas_danskproeve_niveau = c.String(),
                        esas_ag_eksamensaar = c.String(),
                        esas_ag_eksamensgennemsnit = c.Decimal(precision: 18, scale: 2),
                        esas_ag_eksamensland_id = c.Guid(),
                        esas_ag_eksamenstype = c.String(),
                        esas_eksamenstype_id = c.Guid(),
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
                        esas_aktivitetsudbud_esas_aktivitetsudbudId = c.Guid(),
                        esas_ansoeger_LeadId = c.Guid(),
                        esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId = c.Guid(),
                        esas_omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                        esas_person_studerende_ContactId = c.Guid(),
                        esas_planlaegningselement_esas_uddannelseselement_planlaegningId = c.Guid(),
                        esas_virksomhed_AccountId = c.Guid(),
                        Land_esas_landId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningId)
                .ForeignKey("dbo.Land", t => t.esas_ag_eksamensland_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_esas_aktivitetsudbudId)
                .ForeignKey("dbo.Ansoeger", t => t.esas_ansoeger_LeadId)
                .ForeignKey("dbo.Ansoegningsopsaetning", t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .ForeignKey("dbo.Eksamenstype", t => t.esas_eksamenstype_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid)
                .ForeignKey("dbo.Person", t => t.esas_person_studerende_ContactId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningselement_esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_virksomhed_AccountId)
                .ForeignKey("dbo.Land", t => t.Land_esas_landId)
                .Index(t => t.esas_ag_eksamensland_id)
                .Index(t => t.esas_eksamenstype_id)
                .Index(t => t.esas_aktivitetsudbud_esas_aktivitetsudbudId)
                .Index(t => t.esas_ansoeger_LeadId)
                .Index(t => t.esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId)
                .Index(t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_person_studerende_ContactId)
                .Index(t => t.esas_planlaegningselement_esas_uddannelseselement_planlaegningId)
                .Index(t => t.esas_virksomhed_AccountId)
                .Index(t => t.Land_esas_landId);
            
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
                "dbo.Eksamenstype",
                c => new
                    {
                        esas_eksamenstypeId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_adgangsgrundlag = c.Int(),
                        esas_id_optagelsedk = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_eksamenstypeId);
            
            CreateTable(
                "dbo.Studieforloeb",
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
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_eksamenstype_id = c.Guid(),
                        esas_ag_eksamensland_id = c.Guid(),
                        esas_forventet_afslutning = c.DateTimeOffset(precision: 7),
                        esas_afgangsdato = c.DateTimeOffset(precision: 7),
                        esas_navne_addressebeskyttet = c.Boolean(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        esas_prioritet = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_opnaaet_ects = c.Decimal(precision: 18, scale: 2),
                        esas_rest_ects = c.Decimal(precision: 18, scale: 2),
                        esas_sammenlagt_fartstid = c.Int(),
                        esas_udloest_staa = c.Decimal(precision: 18, scale: 2),
                        esas_resterende_staa = c.Decimal(precision: 18, scale: 2),
                        esas_rekvirent = c.Boolean(),
                        esas_internationalisering = c.Boolean(),
                        esas_talentbekendtgoerelse = c.Boolean(),
                        esas_uddannelsespaalaeg = c.Boolean(),
                        esas_bevisgrundlag_id = c.Guid(),
                        esas_cpr_nummer = c.String(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_inaktiv_status_per = c.DateTimeOffset(precision: 7),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        esas_profil_idName = c.String(),
                        esas_rekrevirent_id = c.Guid(),
                        esas_studiestart = c.DateTimeOffset(precision: 7),
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
                        esas_tidligere_uddannelsesstruktur_id = c.Guid(),
                        esas_national_afgangsaarsag_id = c.Guid(),
                        esas_ag_eksamensland_esas_landId = c.Guid(),
                        esas_aktivitetsudbud_esas_aktivitetsudbudId = c.Guid(),
                        esas_bevisgrundlag_esas_bevisgrundlagId = c.Guid(),
                        esas_uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId = c.Guid(),
                        NationalAfgangsaarsag_esas_national_afgangsaarsagId = c.Guid(),
                        Eksamenstype_esas_eksamenstypeId = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                        Person_ContactId = c.Guid(),
                        Afdeling_esas_afdelingId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_studieforloebId)
                .ForeignKey("dbo.Afdeling", t => t.esas_afdeling_id)
                .ForeignKey("dbo.Land", t => t.esas_ag_eksamensland_esas_landId)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_esas_aktivitetsudbudId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Bevisgrundlag", t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .ForeignKey("dbo.Eksamenstype", t => t.esas_eksamenstype_id)
                .ForeignKey("dbo.NationalAfgangsaarsag", t => t.NationalAfgangsaarsag_esas_national_afgangsaarsagId)
                .ForeignKey("dbo.NationalAfgangsaarsag", t => t.esas_national_afgangsaarsag_id)
                .ForeignKey("dbo.Person", t => t.esas_studerende_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_tidligere_uddannelsesstruktur_id)
                .ForeignKey("dbo.Eksamenstype", t => t.Eksamenstype_esas_eksamenstypeId)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .ForeignKey("dbo.Person", t => t.Person_ContactId)
                .ForeignKey("dbo.Afdeling", t => t.Afdeling_esas_afdelingId)
                .Index(t => t.esas_studerende_id)
                .Index(t => t.esas_afdeling_id)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.esas_eksamenstype_id)
                .Index(t => t.esas_tidligere_uddannelsesstruktur_id)
                .Index(t => t.esas_national_afgangsaarsag_id)
                .Index(t => t.esas_ag_eksamensland_esas_landId)
                .Index(t => t.esas_aktivitetsudbud_esas_aktivitetsudbudId)
                .Index(t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .Index(t => t.esas_uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .Index(t => t.NationalAfgangsaarsag_esas_national_afgangsaarsagId)
                .Index(t => t.Eksamenstype_esas_eksamenstypeId)
                .Index(t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.Person_ContactId)
                .Index(t => t.Afdeling_esas_afdelingId);
            
            CreateTable(
                "dbo.Bevisgrundlag",
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
                "dbo.GennemfoerelsesUddannelseselement",
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
                        esas_aktivitetsafdeling_id = c.Guid(),
                        esas_udbud_af_valgfag_id = c.Guid(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        OwningBusinessUnit = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId = c.Guid(),
                        Hold_esas_holdId = c.Guid(),
                        PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId = c.Guid(),
                        Karakter_esas_karakterId = c.Guid(),
                        esas_bevisgrundlag_esas_bevisgrundlagId = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .ForeignKey("dbo.Hold", t => t.Hold_esas_holdId)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.Karakter", t => t.Karakter_esas_karakterId)
                .ForeignKey("dbo.Karakter", t => t.esas_bedoemmelsesresultat_id)
                .ForeignKey("dbo.Bevisgrundlag", t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .ForeignKey("dbo.Hold", t => t.esas_hold_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_pue_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_udbud_af_valgfag_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_uddannelseselement_id)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_pue_id)
                .Index(t => t.esas_uddannelseselement_id)
                .Index(t => t.esas_studieforloeb_id)
                .Index(t => t.esas_hold_id)
                .Index(t => t.esas_bedoemmelsesresultat_id)
                .Index(t => t.esas_aktivitetsafdeling_id)
                .Index(t => t.esas_udbud_af_valgfag_id)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .Index(t => t.Hold_esas_holdId)
                .Index(t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId)
                .Index(t => t.Karakter_esas_karakterId)
                .Index(t => t.esas_bevisgrundlag_esas_bevisgrundlagId)
                .Index(t => t.Studieforloeb_esas_studieforloebId);
            
            CreateTable(
                "dbo.Karakter",
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
                        esas_ects_skala = c.String(),
                        esas_taeller_som_forsoeg = c.Boolean(nullable: false),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_karakterId);
            
            CreateTable(
                "dbo.Bedoemmelse",
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
                        esas_ansoegning_id = c.Guid(),
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
                        Bedoemmelsesrunde_esas_bedoemmelsesrundeId = c.Guid(),
                        esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId = c.Guid(),
                        esas_karakter_esas_karakterId = c.Guid(),
                        esas_studieforloeb_esas_studieforloebId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelseId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.SystemUser", t => t.esas_bedoemmelse_godkendt_af_SystemUserId)
                .ForeignKey("dbo.SystemUser", t => t.esas_bedoemmelse_registreret_af_SystemUserId)
                .ForeignKey("dbo.Bedoemmelsesrunde", t => t.Bedoemmelsesrunde_esas_bedoemmelsesrundeId)
                .ForeignKey("dbo.Bedoemmelsesrunde", t => t.esas_bedoemmelsesrunde_id)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Karakter", t => t.esas_karakter_esas_karakterId)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_bedoemmelsesrunde_id)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.esas_bedoemmelse_godkendt_af_SystemUserId)
                .Index(t => t.esas_bedoemmelse_registreret_af_SystemUserId)
                .Index(t => t.Bedoemmelsesrunde_esas_bedoemmelsesrundeId)
                .Index(t => t.esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .Index(t => t.esas_karakter_esas_karakterId)
                .Index(t => t.esas_studieforloeb_esas_studieforloebId);
            
            CreateTable(
                "dbo.SystemUser",
                c => new
                    {
                        SystemUserId = c.Guid(nullable: false),
                        FullName = c.String(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.SystemUserId);
            
            CreateTable(
                "dbo.Bedoemmelsesrunde",
                c => new
                    {
                        esas_bedoemmelsesrundeId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_nummer = c.String(),
                        esas_planlaegningsuddannelseselement_id = c.Guid(),
                        esas_omraadeopsaetning_id = c.Guid(),
                        esas_omraadespecialisering_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bedoemmelsesrundeId)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadeopsaetning_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.Omraadespecialisering", t => t.esas_omraadespecialisering_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id)
                .Index(t => t.esas_planlaegningsuddannelseselement_id)
                .Index(t => t.esas_omraadeopsaetning_id)
                .Index(t => t.esas_omraadespecialisering_id)
                .Index(t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId);
            
            CreateTable(
                "dbo.Omraadespecialisering",
                c => new
                    {
                        esas_omraadespecialiseringid = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_studieretning = c.String(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        esas_omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_omraadespecialiseringid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_id)
                .Index(t => t.esas_uddannelsesstruktur_id)
                .Index(t => t.esas_omraadenummeropsaetning_esas_omraadeopsaetningid);
            
            CreateTable(
                "dbo.Uddannelsesstruktur",
                c => new
                    {
                        esas_uddannelsesstrukturId = c.Guid(nullable: false),
                        esas_antal_dage_foer_periodes_start = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_field_of_study = c.String(),
                        esas_navn = c.String(),
                        esas_regeltjek_koert = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_studieretning = c.String(),
                        esas_uddannelsestype = c.Int(),
                        esas_uddannelsestype_idName = c.String(),
                        esas_uddannelsens_hjemmeside_link = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_uddannelsesaktivitet_id = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelsesstrukturId)
                .ForeignKey("dbo.Uddannelsesaktivitet", t => t.esas_uddannelsesaktivitet_id)
                .Index(t => t.esas_uddannelsesaktivitet_id);
            
            CreateTable(
                "dbo.Uddannelsesaktivitet",
                c => new
                    {
                        esas_uddannelsesaktivitetId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statuscode = c.Int(),
                        StateCode = c.Int(),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_aktivitetsgruppekode = c.String(),
                        esas_antal_semestre = c.Int(),
                        esas_betegnelse = c.String(),
                        esas_betegnelse_engelsk = c.String(),
                        esas_cosa_formaalskode = c.String(),
                        esas_delformaal = c.String(),
                        esas_dst_kode = c.String(),
                        esas_ects_loft_praktik = c.Decimal(precision: 18, scale: 2),
                        esas_ects_loft_teori = c.Decimal(precision: 18, scale: 2),
                        esas_samlet_ects = c.Decimal(precision: 18, scale: 2),
                        esas_su_retningskode = c.String(),
                        esas_titel = c.String(),
                        esas_titel_engelsk = c.String(),
                        esas_navn = c.String(),
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
                        esas_ophoersdato = c.DateTimeOffset(precision: 7),
                        esas_bekendtgoerelse_id = c.Guid(),
                        esas_opgoerelsesmetode = c.Int(),
                        esas_uddannelsesform = c.Int(),
                        esas_uddannelsestype = c.Int(),
                        esas_uddannelsesdel = c.Int(),
                        esas_status = c.Int(),
                        esas_indberetning_af_optag = c.Boolean(),
                        esas_indberetning_af_staa_au = c.Boolean(),
                        esas_indberetning_af_staa_ou = c.Boolean(),
                        esas_indberetning_af_su = c.Boolean(),
                        esas_indberetning_til_soefartsstyrelsen = c.Boolean(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelsesaktivitetId);
            
            CreateTable(
                "dbo.StruktureltUddannelseselement",
                c => new
                    {
                        esas_uddannelseselementId = c.Guid(nullable: false),
                        esas_afsluttet_inden = c.Decimal(precision: 18, scale: 2),
                        esas_afsluttet_inden_interval = c.Int(),
                        esas_aktivitet_type = c.Int(),
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
                        esas_ects = c.Decimal(precision: 18, scale: 2),
                        esas_ects_kraevet = c.Decimal(precision: 18, scale: 2),
                        esas_ects_max = c.Decimal(precision: 18, scale: 2),
                        esas_ects_min = c.Decimal(precision: 18, scale: 2),
                        esas_fordeling = c.Int(),
                        esas_fordeling_prio_1 = c.Boolean(nullable: false),
                        esas_fordeling_prio_2 = c.Boolean(nullable: false),
                        esas_fordelingsnoegle_max = c.Int(),
                        esas_fordelingsnoegle_min = c.Int(),
                        esas_fordelingsnoegle_prio_1 = c.Int(),
                        esas_fordelingsnoegle_prio_2 = c.Int(),
                        esas_eksamenssprog_id = c.Guid(),
                        esas_eksamenssprog_idName = c.String(),
                        esas_fagtype = c.Int(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_karakterskala = c.Int(),
                        esas_loebenummer = c.String(),
                        esas_loennet = c.Boolean(nullable: false),
                        esas_laas_uddannelseselement = c.Boolean(nullable: false),
                        esas_oevrige_krav_laast = c.Boolean(),
                        esas_navn = c.String(),
                        esas_rund_op_til_bestaaet = c.Boolean(nullable: false),
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
                        esas_valgfag_paa_semester = c.String(),
                        esas_sprog_id = c.Guid(),
                        esas_sprog_idName = c.String(),
                        esas_profil_idName = c.String(),
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
                        esas_overordnet_uddannelseselement_id = c.Guid(),
                        esas_relateret_sue_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_uddannelsesstruktur_id = c.Guid(),
                        esas_udbud_af_valgfag_id = c.Guid(),
                        esas_uvm_fagkode = c.Int(),
                        esas_antal_antal_timers_undervisning = c.Int(),
                        esas_navn_engelsk = c.String(),
                        esas_beskrivelse_engelsk = c.String(),
                        esas_fagkode_niveau = c.String(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        Uddannelsesstruktur_esas_uddannelsesstrukturId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselementId)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.esas_uddannelsesstruktur_id)
                .ForeignKey("dbo.Uddannelsesstruktur", t => t.Uddannelsesstruktur_esas_uddannelsesstrukturId)
                .Index(t => t.esas_adgangskrav_id)
                .Index(t => t.esas_publicering_id)
                .Index(t => t.esas_uddannelsesstruktur_id)
                .Index(t => t.Uddannelsesstruktur_esas_uddannelsesstrukturId);
            
            CreateTable(
                "dbo.Publicering",
                c => new
                    {
                        esas_publiceringid = c.Guid(nullable: false),
                        esas_ansoegningssynkronisering_aktiv = c.Boolean(),
                        esas_beskeder_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_bilagsupload_tilladt_indtil = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_praktiske_oplysninger = c.String(),
                        esas_publiceringsmuligheder = c.String(),
                        esas_publiceringsmulighed_institutionsspecifik = c.String(),
                        esas_publiceringsperiode_fra = c.DateTimeOffset(precision: 7),
                        esas_publiceringsperiode_til = c.DateTimeOffset(precision: 7),
                        esas_ansoegningskortopsaetning_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_publiceringid)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.esas_ansoegningskortopsaetning_id)
                .Index(t => t.esas_ansoegningskortopsaetning_id)
                .Index(t => t.AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.AnsoegningskortOpsaetning",
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
                "dbo.Ansoegningskort",
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
                        AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskortid)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.esas_ansoegningskortopsaetning_id)
                .ForeignKey("dbo.AnsoegningskortTekst", t => t.esas_ansoegningskorttekst_id)
                .ForeignKey("dbo.AnsoegningskortOpsaetning", t => t.AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid)
                .Index(t => t.esas_ansoegningskortopsaetning_id)
                .Index(t => t.esas_ansoegningskorttekst_id)
                .Index(t => t.AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid);
            
            CreateTable(
                "dbo.AnsoegningskortTekst",
                c => new
                    {
                        esas_ansoegningskorttekstid = c.Guid(nullable: false),
                        esas_beskrivelse = c.String(),
                        esas_hjaelpetekst_dansk = c.String(),
                        esas_hjaelpetekst_engelsk = c.String(),
                        esas_navn = c.String(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningskorttekstid);
            
            CreateTable(
                "dbo.PlanlaegningsUddannelseselement",
                c => new
                    {
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
                        esas_aktivitetsudbud_id = c.Guid(),
                        esas_postnummer_by_id = c.Guid(),
                        esas_publicering_id = c.Guid(),
                        esas_adgangskrav_id = c.Guid(),
                        esas_navn = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_uddannelseselement_id = c.Guid(),
                        esas_sprog_id = c.Guid(),
                        esas_sprog_idName = c.String(),
                        esas_semester_modul_id = c.Guid(),
                        esas_gruppering_id = c.Guid(),
                        esas_postnummer_by_idName = c.String(),
                        esas_aktivitetsafdeling_id = c.Guid(),
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
                        esas_undervisningsform = c.Int(),
                        esas_undervisning_ugedag = c.String(),
                        esas_sluttidspunkt = c.String(),
                        esas_starttidspunkt = c.String(),
                        esas_maximum_antal_deltagere = c.Int(),
                        esas_minimum_antal_deltagere = c.Int(),
                        esas_tilmeldingslink = c.String(),
                        esas_indskrivningsform_id = c.Guid(),
                        esas_indskrivningsform_idName = c.String(),
                        esas_aflysningsaarsag = c.Int(),
                        esas_adgangskrav_esas_adgangskravId = c.Guid(),
                        esas_gruppering_esas_uddannelseselementId = c.Guid(),
                        esas_samlaesning_esas_uddannelseselementId = c.Guid(),
                        esas_semester_modul_esas_uddannelseselementId = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId1 = c.Guid(),
                        StruktureltUddannelseselement_esas_uddannelseselementId2 = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                        Studieforloeb_esas_studieforloebId1 = c.Guid(),
                        Postnummer_esas_postnummerId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.Adgangskrav", t => t.esas_adgangskrav_esas_adgangskravId)
                .ForeignKey("dbo.Afdeling", t => t.esas_aktivitetsafdeling_id)
                .ForeignKey("dbo.Aktivitetsudbud", t => t.esas_aktivitetsudbud_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_gruppering_esas_uddannelseselementId)
                .ForeignKey("dbo.Postnummer", t => t.esas_postnummer_by_id)
                .ForeignKey("dbo.Publicering", t => t.esas_publicering_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_samlaesning_esas_uddannelseselementId)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_semester_modul_esas_uddannelseselementId)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.esas_uddannelseselement_id)
                .ForeignKey("dbo.StruktureltUddannelseselement", t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId1)
                .ForeignKey("dbo.Postnummer", t => t.Postnummer_esas_postnummerId)
                .Index(t => t.esas_aktivitetsudbud_id)
                .Index(t => t.esas_postnummer_by_id)
                .Index(t => t.esas_publicering_id)
                .Index(t => t.esas_uddannelseselement_id)
                .Index(t => t.esas_aktivitetsafdeling_id)
                .Index(t => t.esas_adgangskrav_esas_adgangskravId)
                .Index(t => t.esas_gruppering_esas_uddannelseselementId)
                .Index(t => t.esas_samlaesning_esas_uddannelseselementId)
                .Index(t => t.esas_semester_modul_esas_uddannelseselementId)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId1)
                .Index(t => t.StruktureltUddannelseselement_esas_uddannelseselementId2)
                .Index(t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.Studieforloeb_esas_studieforloebId1)
                .Index(t => t.Postnummer_esas_postnummerId);
            
            CreateTable(
                "dbo.Fagpersonsrelation",
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
                        Personoplysning_esas_personoplysningerId = c.Guid(),
                        Hold_esas_holdId = c.Guid(),
                        PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId = c.Guid(),
                        Bedoemmelse_esas_bedoemmelseId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_fagpersonsrelationId)
                .ForeignKey("dbo.Bedoemmelse", t => t.esas_bedoemmelse_id)
                .ForeignKey("dbo.Personoplysning", t => t.Personoplysning_esas_personoplysningerId)
                .ForeignKey("dbo.Personoplysning", t => t.esas_fagperson_id)
                .ForeignKey("dbo.Hold", t => t.Hold_esas_holdId)
                .ForeignKey("dbo.Hold", t => t.esas_hold_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId)
                .ForeignKey("dbo.Bedoemmelse", t => t.Bedoemmelse_esas_bedoemmelseId)
                .Index(t => t.esas_bedoemmelse_id)
                .Index(t => t.esas_fagperson_id)
                .Index(t => t.esas_hold_id)
                .Index(t => t.esas_planlaegningsuddannelseselement_id)
                .Index(t => t.Personoplysning_esas_personoplysningerId)
                .Index(t => t.Hold_esas_holdId)
                .Index(t => t.PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId)
                .Index(t => t.Bedoemmelse_esas_bedoemmelseId);
            
            CreateTable(
                "dbo.Personoplysning",
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
                        esas_integration_id = c.String(),
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
                    })
                .PrimaryKey(t => t.esas_personoplysningerId)
                .ForeignKey("dbo.Person", t => t.esas_person_id)
                .Index(t => t.esas_person_id);
            
            CreateTable(
                "dbo.Hold",
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
                    })
                .PrimaryKey(t => t.esas_holdId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_id)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_planlaegningsuddannelseselement_id)
                .Index(t => t.esas_planlaegningsuddannelseselement_id)
                .Index(t => t.esas_institution_id);
            
            CreateTable(
                "dbo.GebyrtypePUERelation",
                c => new
                    {
                        esas_gebyrtype_esas_uddannelseselement_plid = c.Guid(nullable: false),
                        esas_uddannelseselement_planlaegningid = c.Guid(),
                        esas_gebyrtypeid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gebyrtype_esas_uddannelseselement_plid)
                .ForeignKey("dbo.Gebyrtype", t => t.esas_gebyrtypeid)
                .ForeignKey("dbo.PlanlaegningsUddannelseselement", t => t.esas_uddannelseselement_planlaegningid)
                .Index(t => t.esas_uddannelseselement_planlaegningid)
                .Index(t => t.esas_gebyrtypeid);
            
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
                        esas_kontostreng_id = c.Guid(nullable: false),
                        esas_kontostreng_idName = c.String(),
                        esas_navn = c.String(),
                        esas_skabelon_id = c.Guid(nullable: false),
                        esas_skabelon_idName = c.String(),
                        esas_type = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gebyrtypeid);
            
            CreateTable(
                "dbo.MeritRegistrering",
                c => new
                    {
                        esas_meritregistreringId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_aktivitetstype_id = c.Guid(nullable: false),
                        esas_aktivitetstype_idName = c.String(),
                        esas_gennemfoerelsesuddannelseselement_id = c.Guid(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_karakter_id = c.Guid(),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_bedoemmelsesdato = c.DateTimeOffset(precision: 7),
                        esas_fra_institution_id = c.Guid(),
                        esas_bedoemmelse_id = c.Guid(),
                        esas_titel_dansk = c.String(),
                        esas_titel_engelsk = c.String(),
                        esas_studietidsforkortende = c.Boolean(),
                        esas_navn = c.String(),
                        esas_godkendelsesdato = c.DateTimeOffset(precision: 7),
                        esas_godkender_id = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_meritregistreringId)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_id)
                .ForeignKey("dbo.Fagpersonsrelation", t => t.esas_godkender_id)
                .ForeignKey("dbo.Karakter", t => t.esas_karakter_id)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .Index(t => t.esas_gennemfoerelsesuddannelseselement_id)
                .Index(t => t.esas_karakter_id)
                .Index(t => t.esas_godkender_id)
                .Index(t => t.GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId);
            
            CreateTable(
                "dbo.Praktikophold",
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
                        Praktikomraade_esas_praktikomraadeId = c.Guid(),
                        GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                        Person_ContactId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_praktikopholdId)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.esas_gennemfoerelsesuddannelseselement_id)
                .ForeignKey("dbo.Praktikomraade", t => t.Praktikomraade_esas_praktikomraadeId)
                .ForeignKey("dbo.Praktikomraade", t => t.esas_praktikomraade_id)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_praktiksted_id)
                .ForeignKey("dbo.Person", t => t.esas_praktikvejleder_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id)
                .ForeignKey("dbo.GennemfoerelsesUddannelseselement", t => t.GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.Person", t => t.Person_ContactId)
                .Index(t => t.esas_studieforloeb_id)
                .Index(t => t.esas_gennemfoerelsesuddannelseselement_id)
                .Index(t => t.esas_praktiksted_id)
                .Index(t => t.esas_praktikomraade_id)
                .Index(t => t.esas_praktikvejleder_id)
                .Index(t => t.Praktikomraade_esas_praktikomraadeId)
                .Index(t => t.GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId)
                .Index(t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.Person_ContactId);
            
            CreateTable(
                "dbo.Praktikomraade",
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
            
            CreateTable(
                "dbo.NationalAfgangsaarsag",
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
                "dbo.Enkeltfag",
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
                        esas_studieforloeb_id = c.Guid(),
                        esas_termin_aar = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        esas_ansogning_esas_ansoegningId = c.Guid(),
                        esas_studieforloeb_esas_studieforloebId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_enkeltfagid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansogning_esas_ansoegningId)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_ansogning_esas_ansoegningId)
                .Index(t => t.esas_studieforloeb_esas_studieforloebId);
            
            CreateTable(
                "dbo.Internationalisering",
                c => new
                    {
                        esas_internationaliseringId = c.Guid(nullable: false),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_studieforloeb_id = c.Guid(),
                        esas_godkender_id = c.Guid(),
                        esas_godkenderelsesdato = c.DateTimeOffset(precision: 7),
                        esas_institution_id = c.Guid(),
                        esas_retning = c.Int(),
                        esas_opholdstype = c.Int(),
                        esas_udvekslingsaftale = c.Int(),
                        esas_udvekslingsaftaletype = c.Int(),
                        esas_semester = c.Int(),
                        esas_varighed = c.Int(),
                        esas_agent = c.String(),
                        StateCode = c.Int(),
                        StatusCode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_internationaliseringId)
                .ForeignKey("dbo.Personoplysning", t => t.esas_godkender_id)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.esas_studieforloeb_id)
                .Index(t => t.esas_godkender_id)
                .Index(t => t.esas_institution_id)
                .Index(t => t.Studieforloeb_esas_studieforloebId);
            
            CreateTable(
                "dbo.StudieinaktivPeriode",
                c => new
                    {
                        esas_studieinaktiv_periodeId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_fejlbesked_fra_inrule = c.String(),
                        esas_idag = c.DateTimeOffset(precision: 7),
                        esas_slutdato = c.DateTimeOffset(precision: 7),
                        esas_startdato = c.DateTimeOffset(precision: 7),
                        esas_studerende_id = c.Guid(),
                        esas_studerende_idName = c.String(),
                        esas_studieforloeb_id = c.Guid(),
                        esas_studieforloeb_idName = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_aarsag_id = c.Guid(),
                        esas_aarsag_idName = c.String(),
                        esas_indberettet_til_su = c.Boolean(nullable: false),
                        OwningBusinessUnit = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_fravaersaarsag_esas_fravaersaarsagId = c.Guid(),
                        Studieforloeb_esas_studieforloebId = c.Guid(),
                        Person_ContactId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_studieinaktiv_periodeId)
                .ForeignKey("dbo.Fravaersaarsag", t => t.esas_fravaersaarsag_esas_fravaersaarsagId)
                .ForeignKey("dbo.Person", t => t.esas_studerende_id)
                .ForeignKey("dbo.Studieforloeb", t => t.esas_studieforloeb_id)
                .ForeignKey("dbo.Studieforloeb", t => t.Studieforloeb_esas_studieforloebId)
                .ForeignKey("dbo.Person", t => t.Person_ContactId)
                .Index(t => t.esas_studerende_id)
                .Index(t => t.esas_studieforloeb_id)
                .Index(t => t.esas_fravaersaarsag_esas_fravaersaarsagId)
                .Index(t => t.Studieforloeb_esas_studieforloebId)
                .Index(t => t.Person_ContactId);
            
            CreateTable(
                "dbo.Fravaersaarsag",
                c => new
                    {
                        esas_fravaersaarsagId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        esas_beskrivelse = c.String(),
                        esas_navn = c.String(),
                        esas_paavirker_su = c.Int(),
                        esas_paavirker_suname = c.String(),
                        ModifiedBy = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_fravaersaarsagId);
            
            CreateTable(
                "dbo.AndenAktivitet",
                c => new
                    {
                        esas_ansoegning_andre_aktiviteterid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_aktivitet = c.String(),
                        esas_ansoegning_id = c.Guid(),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_institution_organisation = c.String(),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_tid = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_andre_aktiviteterid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Ansoegningshandling",
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
                        esas_bilagskategori = c.Int(),
                        esas_oprettelsesaarsag = c.Int(),
                        esas_ansoegningshandlingsgruppe = c.Int(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegningshandlingId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Bilag",
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
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_bilagid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Erfaringer",
                c => new
                    {
                        esas_ansoegning_erfaringerid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_arbejdsart = c.String(),
                        esas_arbejdsgiver = c.String(),
                        esas_navn = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_tid = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_erfaringerid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
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
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        ModifiedBy = c.Guid(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kommunikationId)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.KOTGruppeTilmelding",
                c => new
                    {
                        esas_kot_gruppe_tilmeldingid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        StateCode = c.Int(),
                        StatusCode = c.Int(),
                        kot_gruppe_id = c.Guid(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kot_gruppe_tilmeldingid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.KOTGruppe", t => t.kot_gruppe_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.kot_gruppe_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.KOTGruppe",
                c => new
                    {
                        esas_kot_gruppeid = c.Guid(nullable: false),
                        esas_automatisk_tildeling = c.Boolean(nullable: false),
                        esas_betegnelse = c.String(),
                        esas_gruppenummer = c.Int(),
                        esas_navn = c.String(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_omraadenummeropsaetning_idName = c.String(),
                        esas_periode_slut = c.DateTimeOffset(nullable: false, precision: 7),
                        esas_periode_start = c.DateTimeOffset(nullable: false, precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        StateCode = c.Int(),
                        StatusCode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        Omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kot_gruppeid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_omraadenummeropsaetning_id)
                .Index(t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid);
            
            CreateTable(
                "dbo.KurserSkoleophold",
                c => new
                    {
                        esas_ansoegning_kurser_og_skoleopholdid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_ansoegning_id = c.Guid(),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_skole = c.String(),
                        esas_start = c.DateTimeOffset(precision: 7),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_kurser_og_skoleopholdid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.KvalifikationspointAnsoegning",
                c => new
                    {
                        esas_kvalifikationspoint_esas_ansoegningid = c.Guid(nullable: false),
                        esas_kvalifikationspointid = c.Guid(),
                        esas_ansoegningid = c.Guid(),
                        Kvalifikationspoint_esas_kvalifikationspointid = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationspoint_esas_ansoegningid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegningid)
                .ForeignKey("dbo.Kvalifikationspoint", t => t.Kvalifikationspoint_esas_kvalifikationspointid)
                .ForeignKey("dbo.Kvalifikationspoint", t => t.esas_kvalifikationspointid)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_kvalifikationspointid)
                .Index(t => t.esas_ansoegningid)
                .Index(t => t.Kvalifikationspoint_esas_kvalifikationspointid)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Kvalifikationspoint",
                c => new
                    {
                        esas_kvalifikationspointid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_kvalifikationskriterie_id = c.Guid(),
                        esas_point = c.Int(),
                        esas_raekkefoelge = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        Kvalifikationskriterie_esas_kvalifikationskriterieid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationspointid)
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.Kvalifikationskriterie_esas_kvalifikationskriterieid)
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.esas_kvalifikationskriterie_id)
                .Index(t => t.esas_kvalifikationskriterie_id)
                .Index(t => t.Kvalifikationskriterie_esas_kvalifikationskriterieid);
            
            CreateTable(
                "dbo.Kvalifikationskriterie",
                c => new
                    {
                        esas_kvalifikationskriterieid = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_navn = c.String(),
                        esas_beskrivelse = c.String(),
                        esas_max_point = c.Int(),
                        esas_point_deling = c.Boolean(),
                        esas_raekkefoelge = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationskriterieid);
            
            CreateTable(
                "dbo.KvalifikationskriterieOmraadenummeropsaetning",
                c => new
                    {
                        esas_kvalifikationskriterier_for_omraadenumid = c.Guid(nullable: false),
                        esas_kvalifikationskriterieid = c.Guid(),
                        esas_omraadeopsaetningid = c.Guid(),
                        Kvalifikationskriterie_esas_kvalifikationskriterieid = c.Guid(),
                        Omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_kvalifikationskriterier_for_omraadenumid)
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.esas_kvalifikationskriterieid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadeopsaetningid)
                .ForeignKey("dbo.Kvalifikationskriterie", t => t.Kvalifikationskriterie_esas_kvalifikationskriterieid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_kvalifikationskriterieid)
                .Index(t => t.esas_omraadeopsaetningid)
                .Index(t => t.Kvalifikationskriterie_esas_kvalifikationskriterieid)
                .Index(t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid);
            
            CreateTable(
                "dbo.Proeve",
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
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_proeveid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.UdlandsopholdAnsoegning",
                c => new
                    {
                        esas_ansoegning_udlandsopholdid = c.Guid(nullable: false),
                        esas_aktivitet = c.String(),
                        esas_ansoegning_id = c.Guid(),
                        esas_maaned_antal = c.Decimal(precision: 18, scale: 2),
                        esas_land_id = c.Guid(),
                        esas_navn = c.String(),
                        esas_slut = c.DateTimeOffset(precision: 7),
                        esas_start = c.DateTimeOffset(precision: 7),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_udlandsopholdid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.VideregaaendeUddannelse",
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
                        Ansoegning_esas_ansoegningId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_videregaaende_uddannelseid)
                .ForeignKey("dbo.Ansoegning", t => t.esas_ansoegning_id)
                .ForeignKey("dbo.Ansoegning", t => t.Ansoegning_esas_ansoegningId)
                .Index(t => t.esas_ansoegning_id)
                .Index(t => t.Ansoegning_esas_ansoegningId);
            
            CreateTable(
                "dbo.Institutionsoplysninger",
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
                        esas_institution_virksomhed_AccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_institutionsoplysningerId)
                .ForeignKey("dbo.InstitutionVirksomhed", t => t.esas_institution_virksomhed_AccountId)
                .Index(t => t.esas_institution_virksomhed_AccountId);
            
            CreateTable(
                "dbo.GymnasielleFagkrav",
                c => new
                    {
                        esas_gymnasielle_fagkravId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_fagkrav_liste = c.String(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        Omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_fagkravId)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_omraadenummeropsaetning_id)
                .Index(t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid);
            
            CreateTable(
                "dbo.GymnasielleKarakterkrav",
                c => new
                    {
                        esas_gymnasielle_karakterkravid = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_omraadenummeropsaetning_id = c.Guid(),
                        esas_fag_idname = c.String(),
                        esas_karakterkrav = c.Int(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        OwningBusinessUnit = c.Guid(),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        Omraadenummeropsaetning_esas_omraadeopsaetningid = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_gymnasielle_karakterkravid)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.esas_omraadenummeropsaetning_id)
                .ForeignKey("dbo.Omraadenummeropsaetning", t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid)
                .Index(t => t.esas_omraadenummeropsaetning_id)
                .Index(t => t.Omraadenummeropsaetning_esas_omraadeopsaetningid);
            
            CreateTable(
                "dbo.Afslagsbegrundelse",
                c => new
                    {
                        esas_afslagsbegrundelseId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                        esas_navn = c.String(),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_afslagsbegrundelseId);
            
            CreateTable(
                "dbo.AnsoegningPlanlaegningsUddannelseselement",
                c => new
                    {
                        esas_ansoegning_esas_pueid = c.Guid(nullable: false),
                        esas_ansoegningid = c.Guid(),
                        esas_uddannelseselement_planlaegningid = c.Guid(),
                        statecode = c.Int(),
                        statuscode = c.Int(),
                    })
                .PrimaryKey(t => t.esas_ansoegning_esas_pueid);
            
            CreateTable(
                "dbo.EsasSyncResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SyncStartTime = c.DateTime(nullable: false),
                        esasLoadResult_EsasLoadStatus = c.Int(nullable: false),
                        esasLoadResult_LoadTimeMs = c.Long(),
                        esasLoadResult_LoadStartTime = c.DateTime(),
                        esasLoadResult_LoadEndTime = c.DateTime(),
                        esasLoadResult_NumberOfObjectsLoaded = c.Int(),
                        esasLoadResult_ModifiedOnDateTimeValue = c.DateTime(),
                        esasLoadResult_LoaderStrategyName = c.String(),
                        esasLoadResult_Message = c.String(),
                        esasSendResult_SendToDestinationStatus = c.Int(nullable: false),
                        esasSendResult_SendTimeMs = c.Long(),
                        esasSendResult_SendStartTime = c.DateTime(),
                        esasSendResult_SendEndTime = c.DateTime(),
                        esasSendResult_SendDestinationStrategyName = c.String(),
                        Message = c.String(),
                        SyncStrategyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Omraadenummer",
                c => new
                    {
                        esas_omraadenummerId = c.Guid(nullable: false),
                        esas_omraadenummer = c.Int(),
                        esas_navn = c.String(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_omraadenummerId);
            
            CreateTable(
                "dbo.OptionSetValueString",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AttributeName = c.String(),
                        EntityName = c.String(),
                        Label = c.String(),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelationsStatus",
                c => new
                    {
                        esas_relations_statusId = c.Guid(nullable: false),
                        esas_navn = c.String(),
                        esas_parentid = c.String(),
                        esas_childid = c.String(),
                        esas_relation = c.Int(),
                        statuscode = c.Int(),
                        statecode = c.Int(),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                        OwningBusinessUnit = c.Guid(),
                    })
                .PrimaryKey(t => t.esas_relations_statusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adgangskrav", "esas_struktureltUddannelseselement_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.Adgangskrav", "esas_planlaegningsUddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Adgangskrav", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "Omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.KOTGruppe", "Omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleKarakterkrav", "Omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleKarakterkrav", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleFagkrav", "Omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.GymnasielleFagkrav", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_publicering_esas_publiceringid", "dbo.Publicering");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetning");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_institutionsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Aktivitetsudbud", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Studieforloeb", "Afdeling_esas_afdelingId", "dbo.Afdeling");
            DropForeignKey("dbo.Afdeling", "esas_overordnet_afdeling_esas_afdelingId", "dbo.Afdeling");
            DropForeignKey("dbo.Afdeling", "esas_account_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_vist_institutionsoplysning_id", "dbo.Institutionsoplysninger");
            DropForeignKey("dbo.Institutionsoplysninger", "esas_institution_virksomhed_AccountId", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.Ansoegning", "Land_esas_landId", "dbo.Land");
            DropForeignKey("dbo.Ansoeger", "Land_esas_landId", "dbo.Land");
            DropForeignKey("dbo.Ansoeger", "esas_statsborgerskab_esas_landId", "dbo.Land");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "Postnummer_esas_postnummerId", "dbo.Postnummer");
            DropForeignKey("dbo.StudieinaktivPeriode", "Person_ContactId", "dbo.Person");
            DropForeignKey("dbo.Studieforloeb", "Person_ContactId", "dbo.Person");
            DropForeignKey("dbo.Praktikophold", "Person_ContactId", "dbo.Person");
            DropForeignKey("dbo.VideregaaendeUddannelse", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.VideregaaendeUddannelse", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.UdlandsopholdAnsoegning", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.UdlandsopholdAnsoegning", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Studieforloeb", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Proeve", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Proeve", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.KvalifikationspointAnsoegning", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.KvalifikationspointAnsoegning", "esas_kvalifikationspointid", "dbo.Kvalifikationspoint");
            DropForeignKey("dbo.KvalifikationspointAnsoegning", "Kvalifikationspoint_esas_kvalifikationspointid", "dbo.Kvalifikationspoint");
            DropForeignKey("dbo.Kvalifikationspoint", "esas_kvalifikationskriterie_id", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.Kvalifikationspoint", "Kvalifikationskriterie_esas_kvalifikationskriterieid", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "Kvalifikationskriterie_esas_kvalifikationskriterieid", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.KvalifikationskriterieOmraadenummeropsaetning", "esas_kvalifikationskriterieid", "dbo.Kvalifikationskriterie");
            DropForeignKey("dbo.KvalifikationspointAnsoegning", "esas_ansoegningid", "dbo.Ansoegning");
            DropForeignKey("dbo.KurserSkoleophold", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.KurserSkoleophold", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.KOTGruppeTilmelding", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.KOTGruppeTilmelding", "kot_gruppe_id", "dbo.KOTGruppe");
            DropForeignKey("dbo.KOTGruppe", "esas_omraadenummeropsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.KOTGruppeTilmelding", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Kommunikation", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Kommunikation", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Erfaringer", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Erfaringer", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Bilag", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Bilag", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Ansoegningshandling", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Ansoegningshandling", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.AndenAktivitet", "Ansoegning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.AndenAktivitet", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Ansoegning", "esas_virksomhed_AccountId", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Ansoegning", "esas_planlaegningselement_esas_uddannelseselement_planlaegningId", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Ansoegning", "esas_person_studerende_ContactId", "dbo.Person");
            DropForeignKey("dbo.Ansoegning", "esas_omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Ansoegning", "esas_eksamenstype_id", "dbo.Eksamenstype");
            DropForeignKey("dbo.Studieforloeb", "Eksamenstype_esas_eksamenstypeId", "dbo.Eksamenstype");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "Studieforloeb_esas_studieforloebId1", "dbo.Studieforloeb");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.StudieinaktivPeriode", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.StudieinaktivPeriode", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.StudieinaktivPeriode", "esas_studerende_id", "dbo.Person");
            DropForeignKey("dbo.StudieinaktivPeriode", "esas_fravaersaarsag_esas_fravaersaarsagId", "dbo.Fravaersaarsag");
            DropForeignKey("dbo.Praktikophold", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.Internationalisering", "Studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.Internationalisering", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Internationalisering", "esas_institution_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Internationalisering", "esas_godkender_id", "dbo.Personoplysning");
            DropForeignKey("dbo.Enkeltfag", "esas_studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.Enkeltfag", "esas_ansogning_esas_ansoegningId", "dbo.Ansoegning");
            DropForeignKey("dbo.Studieforloeb", "esas_tidligere_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Studieforloeb", "esas_studerende_id", "dbo.Person");
            DropForeignKey("dbo.Studieforloeb", "esas_national_afgangsaarsag_id", "dbo.NationalAfgangsaarsag");
            DropForeignKey("dbo.Studieforloeb", "NationalAfgangsaarsag_esas_national_afgangsaarsagId", "dbo.NationalAfgangsaarsag");
            DropForeignKey("dbo.Studieforloeb", "esas_eksamenstype_id", "dbo.Eksamenstype");
            DropForeignKey("dbo.Praktikophold", "GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.Praktikophold", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.Praktikophold", "esas_praktikvejleder_id", "dbo.Person");
            DropForeignKey("dbo.Praktikophold", "esas_praktiksted_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Praktikophold", "esas_praktikomraade_id", "dbo.Praktikomraade");
            DropForeignKey("dbo.Praktikophold", "Praktikomraade_esas_praktikomraadeId", "dbo.Praktikomraade");
            DropForeignKey("dbo.Praktikophold", "esas_gennemfoerelsesuddannelseselement_id", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.MeritRegistrering", "GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.MeritRegistrering", "esas_karakter_id", "dbo.Karakter");
            DropForeignKey("dbo.MeritRegistrering", "esas_godkender_id", "dbo.Fagpersonsrelation");
            DropForeignKey("dbo.MeritRegistrering", "esas_gennemfoerelsesuddannelseselement_id", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_uddannelseselement_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_udbud_af_valgfag_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_studieforloeb_id", "dbo.Studieforloeb");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_pue_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_hold_id", "dbo.Hold");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_bevisgrundlag_esas_bevisgrundlagId", "dbo.Bevisgrundlag");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_bedoemmelsesresultat_id", "dbo.Karakter");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "Karakter_esas_karakterId", "dbo.Karakter");
            DropForeignKey("dbo.Fagpersonsrelation", "Bedoemmelse_esas_bedoemmelseId", "dbo.Bedoemmelse");
            DropForeignKey("dbo.Bedoemmelse", "esas_studieforloeb_esas_studieforloebId", "dbo.Studieforloeb");
            DropForeignKey("dbo.Bedoemmelse", "esas_karakter_esas_karakterId", "dbo.Karakter");
            DropForeignKey("dbo.Bedoemmelse", "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId", "dbo.GennemfoerelsesUddannelseselement");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelsesrunde_id", "dbo.Bedoemmelsesrunde");
            DropForeignKey("dbo.Bedoemmelse", "Bedoemmelsesrunde_esas_bedoemmelsesrundeId", "dbo.Bedoemmelsesrunde");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_omraadespecialisering_id", "dbo.Omraadespecialisering");
            DropForeignKey("dbo.Omraadespecialisering", "esas_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.StruktureltUddannelseselement", "Uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "StruktureltUddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.GebyrtypePUERelation", "esas_uddannelseselement_planlaegningid", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.GebyrtypePUERelation", "esas_gebyrtypeid", "dbo.Gebyrtype");
            DropForeignKey("dbo.Fagpersonsrelation", "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_hold_id", "dbo.Hold");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "Hold_esas_holdId", "dbo.Hold");
            DropForeignKey("dbo.Fagpersonsrelation", "Hold_esas_holdId", "dbo.Hold");
            DropForeignKey("dbo.Hold", "esas_planlaegningsuddannelseselement_id", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.Hold", "esas_institution_id", "dbo.InstitutionVirksomhed");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_fagperson_id", "dbo.Personoplysning");
            DropForeignKey("dbo.Fagpersonsrelation", "Personoplysning_esas_personoplysningerId", "dbo.Personoplysning");
            DropForeignKey("dbo.Personoplysning", "esas_person_id", "dbo.Person");
            DropForeignKey("dbo.Fagpersonsrelation", "esas_bedoemmelse_id", "dbo.Bedoemmelse");
            DropForeignKey("dbo.Bedoemmelsesrunde", "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId", "dbo.PlanlaegningsUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_uddannelseselement_id", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_semester_modul_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_samlaesning_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_gruppering_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_aktivitetsudbud_id", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.PlanlaegningsUddannelseselement", "esas_adgangskrav_esas_adgangskravId", "dbo.Adgangskrav");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "StruktureltUddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.Studieforloeb", "StruktureltUddannelseselement_esas_uddannelseselementId", "dbo.StruktureltUddannelseselement");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_uddannelsesstruktur_id", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_publicering_id", "dbo.Publicering");
            DropForeignKey("dbo.Publicering", "esas_ansoegningskortopsaetning_id", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.Publicering", "AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.Ansoegningskort", "AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.Ansoegningskort", "esas_ansoegningskorttekst_id", "dbo.AnsoegningskortTekst");
            DropForeignKey("dbo.Ansoegningskort", "esas_ansoegningskortopsaetning_id", "dbo.AnsoegningskortOpsaetning");
            DropForeignKey("dbo.StruktureltUddannelseselement", "esas_adgangskrav_id", "dbo.Adgangskrav");
            DropForeignKey("dbo.Studieforloeb", "esas_uddannelsesstruktur_esas_uddannelsesstrukturId", "dbo.Uddannelsesstruktur");
            DropForeignKey("dbo.Uddannelsesstruktur", "esas_uddannelsesaktivitet_id", "dbo.Uddannelsesaktivitet");
            DropForeignKey("dbo.Omraadespecialisering", "esas_omraadenummeropsaetning_esas_omraadeopsaetningid", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Bedoemmelsesrunde", "esas_omraadeopsaetning_id", "dbo.Omraadenummeropsaetning");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelse_registreret_af_SystemUserId", "dbo.SystemUser");
            DropForeignKey("dbo.Bedoemmelse", "esas_bedoemmelse_godkendt_af_SystemUserId", "dbo.SystemUser");
            DropForeignKey("dbo.Bedoemmelse", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.GennemfoerelsesUddannelseselement", "esas_aktivitetsafdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Studieforloeb", "esas_bevisgrundlag_esas_bevisgrundlagId", "dbo.Bevisgrundlag");
            DropForeignKey("dbo.Studieforloeb", "esas_ansoegning_id", "dbo.Ansoegning");
            DropForeignKey("dbo.Studieforloeb", "esas_aktivitetsudbud_esas_aktivitetsudbudId", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Studieforloeb", "esas_ag_eksamensland_esas_landId", "dbo.Land");
            DropForeignKey("dbo.Studieforloeb", "esas_afdeling_id", "dbo.Afdeling");
            DropForeignKey("dbo.Ansoegning", "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId", "dbo.Ansoegningsopsaetning");
            DropForeignKey("dbo.Ansoegning", "esas_ansoeger_LeadId", "dbo.Ansoeger");
            DropForeignKey("dbo.Ansoegning", "esas_aktivitetsudbud_esas_aktivitetsudbudId", "dbo.Aktivitetsudbud");
            DropForeignKey("dbo.Ansoegning", "esas_ag_eksamensland_id", "dbo.Land");
            DropForeignKey("dbo.Person", "esas_statsborgerskab_id", "dbo.Land");
            DropForeignKey("dbo.Person", "esas_postnummer_by_id", "dbo.Postnummer");
            DropForeignKey("dbo.Person", "esas_land_id", "dbo.Land");
            DropForeignKey("dbo.Ansoeger", "esas_postnummer_by_esas_postnummerId", "dbo.Postnummer");
            DropForeignKey("dbo.Postnummer", "esas_land_esas_landId", "dbo.Land");
            DropForeignKey("dbo.Ansoeger", "esas_land_esas_landId", "dbo.Land");
            DropForeignKey("dbo.InstitutionVirksomhed", "esas_branche_id", "dbo.Branche");
            DropForeignKey("dbo.Omraadenummeropsaetning", "esas_adgangskrav_id", "dbo.Adgangskrav");
            DropIndex("dbo.GymnasielleKarakterkrav", new[] { "Omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.GymnasielleKarakterkrav", new[] { "esas_omraadenummeropsaetning_id" });
            DropIndex("dbo.GymnasielleFagkrav", new[] { "Omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.GymnasielleFagkrav", new[] { "esas_omraadenummeropsaetning_id" });
            DropIndex("dbo.Institutionsoplysninger", new[] { "esas_institution_virksomhed_AccountId" });
            DropIndex("dbo.VideregaaendeUddannelse", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.VideregaaendeUddannelse", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.UdlandsopholdAnsoegning", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.UdlandsopholdAnsoegning", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Proeve", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Proeve", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.KvalifikationskriterieOmraadenummeropsaetning", new[] { "Omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.KvalifikationskriterieOmraadenummeropsaetning", new[] { "Kvalifikationskriterie_esas_kvalifikationskriterieid" });
            DropIndex("dbo.KvalifikationskriterieOmraadenummeropsaetning", new[] { "esas_omraadeopsaetningid" });
            DropIndex("dbo.KvalifikationskriterieOmraadenummeropsaetning", new[] { "esas_kvalifikationskriterieid" });
            DropIndex("dbo.Kvalifikationspoint", new[] { "Kvalifikationskriterie_esas_kvalifikationskriterieid" });
            DropIndex("dbo.Kvalifikationspoint", new[] { "esas_kvalifikationskriterie_id" });
            DropIndex("dbo.KvalifikationspointAnsoegning", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.KvalifikationspointAnsoegning", new[] { "Kvalifikationspoint_esas_kvalifikationspointid" });
            DropIndex("dbo.KvalifikationspointAnsoegning", new[] { "esas_ansoegningid" });
            DropIndex("dbo.KvalifikationspointAnsoegning", new[] { "esas_kvalifikationspointid" });
            DropIndex("dbo.KurserSkoleophold", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.KurserSkoleophold", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.KOTGruppe", new[] { "Omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.KOTGruppe", new[] { "esas_omraadenummeropsaetning_id" });
            DropIndex("dbo.KOTGruppeTilmelding", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.KOTGruppeTilmelding", new[] { "kot_gruppe_id" });
            DropIndex("dbo.KOTGruppeTilmelding", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Kommunikation", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Kommunikation", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Erfaringer", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Erfaringer", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Bilag", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Bilag", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Ansoegningshandling", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Ansoegningshandling", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.AndenAktivitet", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.AndenAktivitet", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.StudieinaktivPeriode", new[] { "Person_ContactId" });
            DropIndex("dbo.StudieinaktivPeriode", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.StudieinaktivPeriode", new[] { "esas_fravaersaarsag_esas_fravaersaarsagId" });
            DropIndex("dbo.StudieinaktivPeriode", new[] { "esas_studieforloeb_id" });
            DropIndex("dbo.StudieinaktivPeriode", new[] { "esas_studerende_id" });
            DropIndex("dbo.Internationalisering", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.Internationalisering", new[] { "esas_institution_id" });
            DropIndex("dbo.Internationalisering", new[] { "esas_godkender_id" });
            DropIndex("dbo.Internationalisering", new[] { "esas_studieforloeb_id" });
            DropIndex("dbo.Enkeltfag", new[] { "esas_studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.Enkeltfag", new[] { "esas_ansogning_esas_ansoegningId" });
            DropIndex("dbo.Praktikophold", new[] { "Person_ContactId" });
            DropIndex("dbo.Praktikophold", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.Praktikophold", new[] { "GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId" });
            DropIndex("dbo.Praktikophold", new[] { "Praktikomraade_esas_praktikomraadeId" });
            DropIndex("dbo.Praktikophold", new[] { "esas_praktikvejleder_id" });
            DropIndex("dbo.Praktikophold", new[] { "esas_praktikomraade_id" });
            DropIndex("dbo.Praktikophold", new[] { "esas_praktiksted_id" });
            DropIndex("dbo.Praktikophold", new[] { "esas_gennemfoerelsesuddannelseselement_id" });
            DropIndex("dbo.Praktikophold", new[] { "esas_studieforloeb_id" });
            DropIndex("dbo.MeritRegistrering", new[] { "GennemfoerelsesUddannelseselement_esas_uddannelseselement_gennemfoerelseId" });
            DropIndex("dbo.MeritRegistrering", new[] { "esas_godkender_id" });
            DropIndex("dbo.MeritRegistrering", new[] { "esas_karakter_id" });
            DropIndex("dbo.MeritRegistrering", new[] { "esas_gennemfoerelsesuddannelseselement_id" });
            DropIndex("dbo.GebyrtypePUERelation", new[] { "esas_gebyrtypeid" });
            DropIndex("dbo.GebyrtypePUERelation", new[] { "esas_uddannelseselement_planlaegningid" });
            DropIndex("dbo.Hold", new[] { "esas_institution_id" });
            DropIndex("dbo.Hold", new[] { "esas_planlaegningsuddannelseselement_id" });
            DropIndex("dbo.Personoplysning", new[] { "esas_person_id" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "Bedoemmelse_esas_bedoemmelseId" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "Hold_esas_holdId" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "Personoplysning_esas_personoplysningerId" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "esas_planlaegningsuddannelseselement_id" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "esas_hold_id" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "esas_fagperson_id" });
            DropIndex("dbo.Fagpersonsrelation", new[] { "esas_bedoemmelse_id" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "Postnummer_esas_postnummerId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "Studieforloeb_esas_studieforloebId1" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId2" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId1" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_semester_modul_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_samlaesning_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_gruppering_esas_uddannelseselementId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_adgangskrav_esas_adgangskravId" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_aktivitetsafdeling_id" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_uddannelseselement_id" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_publicering_id" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_postnummer_by_id" });
            DropIndex("dbo.PlanlaegningsUddannelseselement", new[] { "esas_aktivitetsudbud_id" });
            DropIndex("dbo.Ansoegningskort", new[] { "AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid" });
            DropIndex("dbo.Ansoegningskort", new[] { "esas_ansoegningskorttekst_id" });
            DropIndex("dbo.Ansoegningskort", new[] { "esas_ansoegningskortopsaetning_id" });
            DropIndex("dbo.Publicering", new[] { "AnsoegningskortOpsaetning_esas_ansoegningskortopsaetningid" });
            DropIndex("dbo.Publicering", new[] { "esas_ansoegningskortopsaetning_id" });
            DropIndex("dbo.StruktureltUddannelseselement", new[] { "Uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.StruktureltUddannelseselement", new[] { "esas_uddannelsesstruktur_id" });
            DropIndex("dbo.StruktureltUddannelseselement", new[] { "esas_publicering_id" });
            DropIndex("dbo.StruktureltUddannelseselement", new[] { "esas_adgangskrav_id" });
            DropIndex("dbo.Uddannelsesstruktur", new[] { "esas_uddannelsesaktivitet_id" });
            DropIndex("dbo.Omraadespecialisering", new[] { "esas_omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.Omraadespecialisering", new[] { "esas_uddannelsesstruktur_id" });
            DropIndex("dbo.Bedoemmelsesrunde", new[] { "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId" });
            DropIndex("dbo.Bedoemmelsesrunde", new[] { "esas_omraadespecialisering_id" });
            DropIndex("dbo.Bedoemmelsesrunde", new[] { "esas_omraadeopsaetning_id" });
            DropIndex("dbo.Bedoemmelsesrunde", new[] { "esas_planlaegningsuddannelseselement_id" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_karakter_esas_karakterId" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_gennemfoerelsesuddannelseselement_esas_uddannelseselement_gennemfoerelseId" });
            DropIndex("dbo.Bedoemmelse", new[] { "Bedoemmelsesrunde_esas_bedoemmelsesrundeId" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_bedoemmelse_registreret_af_SystemUserId" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_bedoemmelse_godkendt_af_SystemUserId" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Bedoemmelse", new[] { "esas_bedoemmelsesrunde_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "Studieforloeb_esas_studieforloebId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_bevisgrundlag_esas_bevisgrundlagId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "Karakter_esas_karakterId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "PlanlaegningsUddannelseselement_esas_uddannelseselement_planlaegningId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "Hold_esas_holdId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_udbud_af_valgfag_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_aktivitetsafdeling_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_bedoemmelsesresultat_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_hold_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_studieforloeb_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_uddannelseselement_id" });
            DropIndex("dbo.GennemfoerelsesUddannelseselement", new[] { "esas_pue_id" });
            DropIndex("dbo.Studieforloeb", new[] { "Afdeling_esas_afdelingId" });
            DropIndex("dbo.Studieforloeb", new[] { "Person_ContactId" });
            DropIndex("dbo.Studieforloeb", new[] { "Ansoegning_esas_ansoegningId" });
            DropIndex("dbo.Studieforloeb", new[] { "Eksamenstype_esas_eksamenstypeId" });
            DropIndex("dbo.Studieforloeb", new[] { "NationalAfgangsaarsag_esas_national_afgangsaarsagId" });
            DropIndex("dbo.Studieforloeb", new[] { "StruktureltUddannelseselement_esas_uddannelseselementId" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_uddannelsesstruktur_esas_uddannelsesstrukturId" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_bevisgrundlag_esas_bevisgrundlagId" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_aktivitetsudbud_esas_aktivitetsudbudId" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_ag_eksamensland_esas_landId" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_national_afgangsaarsag_id" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_tidligere_uddannelsesstruktur_id" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_eksamenstype_id" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_ansoegning_id" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_afdeling_id" });
            DropIndex("dbo.Studieforloeb", new[] { "esas_studerende_id" });
            DropIndex("dbo.Ansoegning", new[] { "Land_esas_landId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_virksomhed_AccountId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_planlaegningselement_esas_uddannelseselement_planlaegningId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_person_studerende_ContactId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_omraadenummeropsaetning_esas_omraadeopsaetningid" });
            DropIndex("dbo.Ansoegning", new[] { "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_ansoeger_LeadId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_aktivitetsudbud_esas_aktivitetsudbudId" });
            DropIndex("dbo.Ansoegning", new[] { "esas_eksamenstype_id" });
            DropIndex("dbo.Ansoegning", new[] { "esas_ag_eksamensland_id" });
            DropIndex("dbo.Person", new[] { "esas_land_id" });
            DropIndex("dbo.Person", new[] { "esas_statsborgerskab_id" });
            DropIndex("dbo.Person", new[] { "esas_postnummer_by_id" });
            DropIndex("dbo.Postnummer", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.Ansoeger", new[] { "Land_esas_landId" });
            DropIndex("dbo.Ansoeger", new[] { "esas_statsborgerskab_esas_landId" });
            DropIndex("dbo.Ansoeger", new[] { "esas_postnummer_by_esas_postnummerId" });
            DropIndex("dbo.Ansoeger", new[] { "esas_land_esas_landId" });
            DropIndex("dbo.InstitutionVirksomhed", new[] { "esas_vist_institutionsoplysning_id" });
            DropIndex("dbo.InstitutionVirksomhed", new[] { "esas_postnummer_by_id" });
            DropIndex("dbo.InstitutionVirksomhed", new[] { "esas_land_id" });
            DropIndex("dbo.InstitutionVirksomhed", new[] { "esas_branche_id" });
            DropIndex("dbo.Afdeling", new[] { "esas_overordnet_afdeling_esas_afdelingId" });
            DropIndex("dbo.Afdeling", new[] { "esas_account_id" });
            DropIndex("dbo.Aktivitetsudbud", new[] { "esas_uddannelsesstruktur_id" });
            DropIndex("dbo.Aktivitetsudbud", new[] { "esas_institutionsafdeling_id" });
            DropIndex("dbo.Aktivitetsudbud", new[] { "esas_aktivitetsafdeling_id" });
            DropIndex("dbo.Omraadenummeropsaetning", new[] { "esas_publicering_esas_publiceringid" });
            DropIndex("dbo.Omraadenummeropsaetning", new[] { "esas_ansoegningsopsaetning_esas_ansoegningsopsaetningId" });
            DropIndex("dbo.Omraadenummeropsaetning", new[] { "esas_adgangskrav_id" });
            DropIndex("dbo.Omraadenummeropsaetning", new[] { "esas_aktivitetsudbud_id" });
            DropIndex("dbo.Adgangskrav", new[] { "esas_struktureltUddannelseselement_id" });
            DropIndex("dbo.Adgangskrav", new[] { "esas_planlaegningsUddannelseselement_id" });
            DropIndex("dbo.Adgangskrav", new[] { "esas_omraadenummeropsaetning_id" });
            DropTable("dbo.RelationsStatus");
            DropTable("dbo.OptionSetValueString");
            DropTable("dbo.Omraadenummer");
            DropTable("dbo.EsasWebServiceHealthCheck");
            DropTable("dbo.EsasSyncResult");
            DropTable("dbo.AnsoegningPlanlaegningsUddannelseselement");
            DropTable("dbo.Afslagsbegrundelse");
            DropTable("dbo.GymnasielleKarakterkrav");
            DropTable("dbo.GymnasielleFagkrav");
            DropTable("dbo.Institutionsoplysninger");
            DropTable("dbo.VideregaaendeUddannelse");
            DropTable("dbo.UdlandsopholdAnsoegning");
            DropTable("dbo.Proeve");
            DropTable("dbo.KvalifikationskriterieOmraadenummeropsaetning");
            DropTable("dbo.Kvalifikationskriterie");
            DropTable("dbo.Kvalifikationspoint");
            DropTable("dbo.KvalifikationspointAnsoegning");
            DropTable("dbo.KurserSkoleophold");
            DropTable("dbo.KOTGruppe");
            DropTable("dbo.KOTGruppeTilmelding");
            DropTable("dbo.Kommunikation");
            DropTable("dbo.Erfaringer");
            DropTable("dbo.Bilag");
            DropTable("dbo.Ansoegningshandling");
            DropTable("dbo.AndenAktivitet");
            DropTable("dbo.Fravaersaarsag");
            DropTable("dbo.StudieinaktivPeriode");
            DropTable("dbo.Internationalisering");
            DropTable("dbo.Enkeltfag");
            DropTable("dbo.NationalAfgangsaarsag");
            DropTable("dbo.Praktikomraade");
            DropTable("dbo.Praktikophold");
            DropTable("dbo.MeritRegistrering");
            DropTable("dbo.Gebyrtype");
            DropTable("dbo.GebyrtypePUERelation");
            DropTable("dbo.Hold");
            DropTable("dbo.Personoplysning");
            DropTable("dbo.Fagpersonsrelation");
            DropTable("dbo.PlanlaegningsUddannelseselement");
            DropTable("dbo.AnsoegningskortTekst");
            DropTable("dbo.Ansoegningskort");
            DropTable("dbo.AnsoegningskortOpsaetning");
            DropTable("dbo.Publicering");
            DropTable("dbo.StruktureltUddannelseselement");
            DropTable("dbo.Uddannelsesaktivitet");
            DropTable("dbo.Uddannelsesstruktur");
            DropTable("dbo.Omraadespecialisering");
            DropTable("dbo.Bedoemmelsesrunde");
            DropTable("dbo.SystemUser");
            DropTable("dbo.Bedoemmelse");
            DropTable("dbo.Karakter");
            DropTable("dbo.GennemfoerelsesUddannelseselement");
            DropTable("dbo.Bevisgrundlag");
            DropTable("dbo.Studieforloeb");
            DropTable("dbo.Eksamenstype");
            DropTable("dbo.Ansoegningsopsaetning");
            DropTable("dbo.Ansoegning");
            DropTable("dbo.Person");
            DropTable("dbo.Postnummer");
            DropTable("dbo.Ansoeger");
            DropTable("dbo.Land");
            DropTable("dbo.Branche");
            DropTable("dbo.InstitutionVirksomhed");
            DropTable("dbo.Afdeling");
            DropTable("dbo.Aktivitetsudbud");
            DropTable("dbo.Omraadenummeropsaetning");
            DropTable("dbo.Adgangskrav");
        }
    }
}
