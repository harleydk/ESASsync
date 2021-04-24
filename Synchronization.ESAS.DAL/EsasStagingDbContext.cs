using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using esas.Dynamics.Models.Contracts;
using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.DAL
{
    public class EsasStagingDbContext : DbContext
    {
        public DbSet<Adgangskrav> Adgangskrav { get; set; }
        public DbSet<Afdeling> Afdeling { get; set; }
        public DbSet<Afslagsbegrundelse> Afslagsbegrundelse { get; set; }
        public DbSet<Aktivitetsudbud> Aktivitetsudbud { get; set; }
        public DbSet<AndenAktivitet> AndenAktivitet { get; set; }
        public DbSet<Ansoeger> Ansoeger { get; set; }
        public DbSet<Ansoegning> Ansoegning { get; set; }
        public DbSet<AnsoegningPlanlaegningsUddannelseselement> AnsoegningPlanlaegningsUddannelseselement { get; set; }
        public DbSet<Ansoegningshandling> Ansoegningshandling { get; set; }
        public DbSet<Ansoegningskort> Ansoegningskort { get; set; }
        public DbSet<AnsoegningskortOpsaetning> AnsoegningskortOpsaetning { get; set; }
        public DbSet<AnsoegningskortTekst> AnsoegningskortTekst { get; set; }
        public DbSet<Ansoegningsopsaetning> Ansoegningsopsaetning { get; set; }
        public DbSet<Bedoemmelse> Bedoemmelse { get; set; }
        public DbSet<Bedoemmelsesrunde> Bedoemmelsesrunde { get; set; }
        public DbSet<Bevisgrundlag> Bevisgrundlag { get; set; }
        public DbSet<Bilag> Bilag { get; set; }
        public DbSet<Branche> Branche { get; set; }
        public DbSet<Eksamenstype> Eksamenstype { get; set; }
        public DbSet<Enkeltfag> Enkeltfag { get; set; }
        public DbSet<Erfaringer> Erfaringer { get; set; }
        public DbSet<Fagpersonsrelation> Fagpersonsrelation { get; set; }
        public DbSet<Fravaersaarsag> Fravaersaarsag { get; set; }
        public DbSet<Gebyrtype> Gebyrtype { get; set; }
        public DbSet<GebyrtypePUERelation> GebyrtypePUERelation { get; set; }
        public DbSet<GennemfoerelsesUddannelseselement> GennemfoerelsesUddannelseselement { get; set; }
        public DbSet<GymnasielleFagkrav> GymnasielleFagkrav { get; set; }
        public DbSet<GymnasielleKarakterkrav> GymnasielleKarakterkrav { get; set; }
        public DbSet<Hold> Hold { get; set; }
        public DbSet<InstitutionVirksomhed> InstitutionVirksomhed { get; set; }
        public DbSet<Institutionsoplysninger> Institutionsoplysninger { get; set; }
        public DbSet<Internationalisering> Internationalisering { get; set; }
        public DbSet<KOTGruppe> KOTGruppe { get; set; }
        public DbSet<KOTGruppeTilmelding> KOTGruppeTilmelding { get; set; }
        public DbSet<Karakter> Karakter { get; set; }
        public DbSet<Kommunikation> Kommunikation { get; set; }
        public DbSet<KurserSkoleophold> KurserSkoleophold { get; set; }
        public DbSet<Kvalifikationskriterie> Kvalifikationskriterie { get; set; }
        public DbSet<KvalifikationskriterieOmraadenummeropsaetning> KvalifikationskriterieOmraadenummeropsaetning { get; set; }
        public DbSet<Kvalifikationspoint> Kvalifikationspoint { get; set; }
        public DbSet<KvalifikationspointAnsoegning> KvalifikationspointAnsoegning { get; set; }
        public DbSet<Land> Land { get; set; }
        public DbSet<MeritRegistrering> MeritRegistrering { get; set; }
        public DbSet<NationalAfgangsaarsag> NationalAfgangsaarsag { get; set; }
        public DbSet<Omraadenummer> Omraadenummer { get; set; }
        public DbSet<Omraadenummeropsaetning> Omraadenummeropsaetning { get; set; }
        public DbSet<Omraadespecialisering> Omraadespecialisering { get; set; }
        public DbSet<OptionSetValueString> OptionSetValueString { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Personoplysning> Personoplysning { get; set; }
        public DbSet<PlanlaegningsUddannelseselement> PlanlaegningsUddannelseselement { get; set; }
        public DbSet<Postnummer> Postnummer { get; set; }
        public DbSet<Praktikomraade> Praktikomraade { get; set; }
        public DbSet<Praktikophold> Praktikophold { get; set; }
        public DbSet<Proeve> Proeve { get; set; }
        public DbSet<Publicering> Publicering { get; set; }
        public DbSet<RelationsStatus> RelationsStatus { get; set; }
        public DbSet<StruktureltUddannelseselement> StruktureltUddannelseselement { get; set; }
        public DbSet<Studieforloeb> Studieforloeb { get; set; }
        public DbSet<StudieinaktivPeriode> StudieinaktivPeriode { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<Uddannelsesaktivitet> Uddannelsesaktivitet { get; set; }
        public DbSet<Uddannelsesstruktur> Uddannelsesstruktur { get; set; }
        public DbSet<UdlandsopholdAnsoegning> UdlandsopholdAnsoegning { get; set; }
        public DbSet<VideregaaendeUddannelse> VideregaaendeUddannelse { get; set; }

        // automation
        public DbSet<EsasSyncResult> EsasSyncResults { get; set; }
        public DbSet<EsasWebServiceHealthCheck> HealthChecks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Så vores tabeller ikke ender på 's' alle sammen...

            // Aktører
            modelBuilder.Entity<Person>().HasKey(o => o.ContactId);
            modelBuilder.Entity<Person>().HasOptional(o => o.esas_land).WithMany(o => o.list_esas_person).HasForeignKey(o => o.esas_land_id);
            modelBuilder.Entity<Person>().HasOptional(o => o.esas_statsborgerskab).WithMany().HasForeignKey(o => o.esas_statsborgerskab_id);
            modelBuilder.Entity<Land>().HasKey(o => o.esas_landId);
            modelBuilder.Entity<Person>().HasOptional(o => o.esas_postnummer_by).WithMany(o => o.list_esas_person).HasForeignKey(o => o.esas_postnummer_by_id);
            modelBuilder.Entity<Postnummer>().HasKey(o => o.esas_postnummerId);
            modelBuilder.Entity<InstitutionVirksomhed>().HasKey(o => o.AccountId);
            modelBuilder.Entity<InstitutionVirksomhed>().HasOptional(o => o.esas_postnummer_by).WithMany(o => o.list_esas_institution_virksomhed).HasForeignKey(o => o.esas_postnummer_by_id);
            modelBuilder.Entity<InstitutionVirksomhed>().HasOptional(o => o.esas_branche).WithMany(o => o.list_esas_institution_virksomhed).HasForeignKey(o => o.esas_branche_id);
            modelBuilder.Entity<Branche>().HasKey(o => o.esas_brancheId);
            modelBuilder.Entity<InstitutionVirksomhed>().HasOptional(o => o.esas_vist_institutionsoplysning).WithMany(o => o.list_esas_institution_virksomhed).HasForeignKey(o => o.esas_vist_institutionsoplysning_id);
            modelBuilder.Entity<InstitutionVirksomhed>().HasOptional(o => o.esas_land).WithMany(o => o.list_esas_institution_virksomhed).HasForeignKey(o => o.esas_land_id);
            modelBuilder.Entity<Institutionsoplysninger>().HasKey(o => o.esas_institutionsoplysningerId);

            modelBuilder.Entity<Personoplysning>().HasKey(o => o.esas_personoplysningerId);
            modelBuilder.Entity<Personoplysning>().HasOptional(o => o.esas_person).WithMany(o => o.list_esas_personoplysning).HasForeignKey(o => o.esas_person_id);

            modelBuilder.Entity<Afdeling>().HasKey(o => o.esas_afdelingId);
            modelBuilder.Entity<Afdeling>().HasOptional(o => o.esas_account).WithMany().HasForeignKey(o => o.esas_account_id);

            // Uddannelsesstrukturer
            modelBuilder.Entity<Uddannelsesaktivitet>().HasKey(o => o.esas_uddannelsesaktivitetId);

            modelBuilder.Entity<Uddannelsesstruktur>().HasKey(o => o.esas_uddannelsesstrukturId);
            modelBuilder.Entity<Uddannelsesstruktur>().HasOptional(o => o.esas_uddannelsesaktivitet).WithMany(o => o.list_esas_uddannelsesstruktur).HasForeignKey(o => o.esas_uddannelsesaktivitet_id);

            modelBuilder.Entity<StruktureltUddannelseselement>().HasKey(o => o.esas_uddannelseselementId);
            modelBuilder.Entity<StruktureltUddannelseselement>().HasOptional(o => o.esas_adgangskrav).WithMany().HasForeignKey(o => o.esas_adgangskrav_id);
            modelBuilder.Entity<StruktureltUddannelseselement>().HasOptional(o => o.esas_publicering).WithMany().HasForeignKey(o => o.esas_publicering_id);
            modelBuilder.Entity<StruktureltUddannelseselement>().HasOptional(o => o.esas_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_uddannelsesstruktur_id);

            modelBuilder.Entity<Aktivitetsudbud>().HasKey(o => o.esas_aktivitetsudbudId);
            modelBuilder.Entity<Aktivitetsudbud>().HasOptional(o => o.esas_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_aktivitetsudbudId);
            modelBuilder.Entity<Aktivitetsudbud>().HasOptional(o => o.esas_aktivitetsafdeling).WithMany().HasForeignKey(o => o.esas_aktivitetsafdeling_id);
            modelBuilder.Entity<Aktivitetsudbud>().HasOptional(o => o.esas_institutionsafdeling).WithMany().HasForeignKey(o => o.esas_institutionsafdeling_id);
            modelBuilder.Entity<Aktivitetsudbud>().HasOptional(o => o.esas_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_uddannelsesstruktur_id);

            // PUE/Hold
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasKey(o => o.esas_uddannelseselement_planlaegningId);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_uddannelseselement).WithMany().HasForeignKey(o => o.esas_uddannelseselement_id);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_aktivitetsafdeling).WithMany().HasForeignKey(o => o.esas_aktivitetsafdeling_id);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_aktivitetsafdeling).WithMany().HasForeignKey(o => o.esas_aktivitetsafdeling_id);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_postnummer_by).WithMany().HasForeignKey(o => o.esas_postnummer_by_id);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().HasOptional(o => o.esas_publicering).WithMany().HasForeignKey(o => o.esas_publicering_id);
            modelBuilder.Entity<Hold>().HasKey(o => o.esas_holdId);
            modelBuilder.Entity<Hold>().HasOptional(o => o.esas_institution).WithMany().HasForeignKey(o => o.esas_institution_id);
            modelBuilder.Entity<Hold>().HasOptional(o => o.esas_planlaegningsuddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsuddannelseselement_id);

            // studieforløb
            modelBuilder.Entity<Studieforloeb>().HasKey(o => o.esas_studieforloebId);
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_studerende).WithMany().HasForeignKey(o => o.esas_studerende_id);
            //modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_uddannelsesstruktur_id);
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_tidligere_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_tidligere_uddannelsesstruktur_id);
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_afdeling).WithMany().HasForeignKey(o => o.esas_afdeling_id);
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);
            //modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);// kan ikke sættes på p.t. - test data er mangelfulde.
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_eksamenstype).WithMany().HasForeignKey(o => o.esas_eksamenstype_id);
            modelBuilder.Entity<Studieforloeb>().HasOptional(o => o.esas_national_afgangsaarsag).WithMany().HasForeignKey(o => o.esas_national_afgangsaarsag_id);

            modelBuilder.Entity<Karakter>().HasKey(o => o.esas_karakterId);

            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasKey(o => o.esas_uddannelseselement_gennemfoerelseId);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_pue).WithMany().HasForeignKey(o => o.esas_pue_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_studieforloeb).WithMany().HasForeignKey(o => o.esas_studieforloeb_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_uddannelseselement).WithMany().HasForeignKey(o => o.esas_uddannelseselement_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_udbud_af_valgfag).WithMany().HasForeignKey(o => o.esas_udbud_af_valgfag_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_hold).WithMany().HasForeignKey(o => o.esas_hold_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_aktivitetsafdeling).WithMany().HasForeignKey(o => o.esas_aktivitetsafdeling_id);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().HasOptional(o => o.esas_bedoemmelsesresultat).WithMany().HasForeignKey(o => o.esas_bedoemmelsesresultat_id);

            modelBuilder.Entity<Fravaersaarsag>().HasKey(o => o.esas_fravaersaarsagId);

            modelBuilder.Entity<StudieinaktivPeriode>().HasKey(o => o.esas_studieinaktiv_periodeId);
            modelBuilder.Entity<StudieinaktivPeriode>().HasOptional(o => o.esas_studieforloeb).WithMany().HasForeignKey(o => o.esas_studieforloeb_id);
            modelBuilder.Entity<StudieinaktivPeriode>().HasOptional(o => o.esas_studerende).WithMany().HasForeignKey(o => o.esas_studerende_id);
            //modelBuilder.Entity<StudieinaktivPeriode>().HasOptional(o => o.esas_fravaersaarsag).WithMany().HasForeignKey(o => o.esas_aarsag_id);/ kan ikke sættes på p.t. - test data er mangelfulde.

            modelBuilder.Entity<Praktikomraade>().HasKey(o => o.esas_praktikomraadeId);

            modelBuilder.Entity<Praktikophold>().HasKey(o => o.esas_praktikopholdId);
            modelBuilder.Entity<Praktikophold>().HasOptional(o => o.esas_gennemfoerelsesuddannelseselement).WithMany().HasForeignKey(o => o.esas_gennemfoerelsesuddannelseselement_id);
            modelBuilder.Entity<Praktikophold>().HasOptional(o => o.esas_praktikomraade).WithMany().HasForeignKey(o => o.esas_praktikomraade_id);
            modelBuilder.Entity<Praktikophold>().HasOptional(o => o.esas_praktiksted).WithMany().HasForeignKey(o => o.esas_praktiksted_id);
            modelBuilder.Entity<Praktikophold>().HasOptional(o => o.esas_praktikvejleder).WithMany().HasForeignKey(o => o.esas_praktikvejleder_id);
            modelBuilder.Entity<Praktikophold>().HasOptional(o => o.esas_studieforloeb).WithMany().HasForeignKey(o => o.esas_studieforloeb_id);

            //eksamens
            modelBuilder.Entity<Eksamenstype>().HasKey(o => o.esas_eksamenstypeId);

            modelBuilder.Entity<Bedoemmelse>().HasKey(o => o.esas_bedoemmelseId);
            modelBuilder.Entity<Bedoemmelse>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);
            modelBuilder.Entity<Bedoemmelse>().HasOptional(o => o.esas_bedoemmelsesrunde).WithMany().HasForeignKey(o => o.esas_bedoemmelsesrunde_id);

       

            modelBuilder.Entity<Bedoemmelsesrunde>().HasKey(o => o.esas_bedoemmelsesrundeId);
            modelBuilder.Entity<Bedoemmelsesrunde>().HasOptional(o => o.esas_omraadeopsaetning).WithMany().HasForeignKey(o => o.esas_omraadeopsaetning_id);
            modelBuilder.Entity<Bedoemmelsesrunde>().HasOptional(o => o.esas_omraadespecialisering).WithMany().HasForeignKey(o => o.esas_omraadespecialisering_id);
            modelBuilder.Entity<Bedoemmelsesrunde>().HasOptional(o => o.esas_planlaegningsuddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsuddannelseselement_id);

            modelBuilder.Entity<Bevisgrundlag>().HasKey(o => o.esas_bevisgrundlagId);

            // ansøgning
            modelBuilder.Entity<Ansoeger>().HasKey(o => o.LeadId);

            modelBuilder.Entity<Ansoegning>().HasKey(o => o.esas_ansoegningId);
            //modelBuilder.Entity<Ansoegning>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);// kan ikke sættes på p.t. - test data er mangelfulde.
            //modelBuilder.Entity<Ansoegning>().HasOptional(o => o.esas_ansoeger).WithMany().HasForeignKey(o => o.esas_ansoeger_id); // kan ikke sættes på p.t. - test data er mangelfulde.
            //modelBuilder.Entity<Ansoegning>().HasOptional(o => o.esas_ansoegningsopsaetning).WithMany().HasForeignKey(o => o.esas_ansoegningsopsaetning_id); // kan ikke sættes på p.t. - test data er mangelfulde.
            modelBuilder.Entity<Ansoegning>().HasOptional(o => o.esas_eksamenstype).WithMany().HasForeignKey(o => o.esas_eksamenstype_id);
            modelBuilder.Entity<Ansoegning>().HasOptional(o => o.esas_ag_eksamensland).WithMany().HasForeignKey(o => o.esas_ag_eksamensland_id);

            modelBuilder.Entity<AnsoegningPlanlaegningsUddannelseselement>().HasKey(o => o.esas_ansoegning_esas_pueid);

            modelBuilder.Entity<Ansoegningshandling>().HasKey(o => o.esas_ansoegningshandlingId);
            modelBuilder.Entity<Ansoegningshandling>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Ansoegningskort>().HasKey(o => o.esas_ansoegningskortid);
            modelBuilder.Entity<Ansoegningskort>().HasOptional(o => o.esas_ansoegningskorttekst).WithMany().HasForeignKey(o => o.esas_ansoegningskorttekst_id);
            modelBuilder.Entity<Ansoegningskort>().HasOptional(o => o.esas_ansoegningskortopsaetning).WithMany().HasForeignKey(o => o.esas_ansoegningskortopsaetning_id);

            modelBuilder.Entity<AnsoegningskortOpsaetning>().HasKey(o => o.esas_ansoegningskortopsaetningid);

            modelBuilder.Entity<AnsoegningskortTekst>().HasKey(o => o.esas_ansoegningskorttekstid);

            modelBuilder.Entity<Ansoegningsopsaetning>().HasKey(o => o.esas_ansoegningsopsaetningId);

            modelBuilder.Entity<Adgangskrav>().HasKey(o => o.esas_adgangskravId); //.Property(o => o.esas_adgangskravId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsClustered = true, IsUnique = true }));
            modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);
            modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_planlaegningsUddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsUddannelseselement_id);
            modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_struktureltUddannelseselement).WithMany().HasForeignKey(o => o.esas_struktureltUddannelseselement_id);

            modelBuilder.Entity<Afslagsbegrundelse>().HasKey(o => o.esas_afslagsbegrundelseId);

            modelBuilder.Entity<Bilag>().HasKey(o => o.esas_bilagid);
            modelBuilder.Entity<Bilag>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Omraadenummer>().HasKey(o => o.esas_omraadenummerId);

            modelBuilder.Entity<Omraadenummeropsaetning>().HasKey(o => o.esas_omraadeopsaetningid);
            modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_adgangskrav).WithMany().HasForeignKey(o => o.esas_adgangskrav_id);
            modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);
            modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);

            modelBuilder.Entity<Omraadespecialisering>().HasKey(o => o.esas_omraadespecialiseringid);
            //modelBuilder.Entity<Omraadespecialisering>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id); // kan ikke sættes på p.t. - test data er mangelfulde.
            modelBuilder.Entity<Omraadespecialisering>().HasOptional(o => o.esas_uddannelsesstruktur).WithMany().HasForeignKey(o => o.esas_uddannelsesstruktur_id);

            modelBuilder.Entity<NationalAfgangsaarsag>().HasKey(o => o.esas_national_afgangsaarsagId);

            modelBuilder.Entity<MeritRegistrering>().HasKey(o => o.esas_meritregistreringId);
            modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_gennemfoerelsesuddannelseselement).WithMany().HasForeignKey(o => o.esas_gennemfoerelsesuddannelseselement_id);
            modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_godkender).WithMany().HasForeignKey(o => o.esas_godkender_id);
            modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_karakter).WithMany().HasForeignKey(o => o.esas_karakter_id);

            modelBuilder.Entity<Enkeltfag>().HasKey(o => o.esas_ansoegning_enkeltfagid);

            // diff
            modelBuilder.Entity<AndenAktivitet>().HasKey(o => o.esas_ansoegning_andre_aktiviteterid);
            modelBuilder.Entity<AndenAktivitet>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Erfaringer>().HasKey(o => o.esas_ansoegning_erfaringerid);
            modelBuilder.Entity<Erfaringer>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Fagpersonsrelation>().HasKey(o => o.esas_fagpersonsrelationId);
            modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_bedoemmelse).WithMany().HasForeignKey(o => o.esas_bedoemmelse_id);
            modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_fagperson).WithMany().HasForeignKey(o => o.esas_fagperson_id);
            modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_hold).WithMany().HasForeignKey(o => o.esas_hold_id);
            modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_planlaegningsuddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsuddannelseselement_id);

            modelBuilder.Entity<Gebyrtype>().HasKey(o => o.esas_gebyrtypeid);
            modelBuilder.Entity<GebyrtypePUERelation>().HasKey(o => o.esas_gebyrtype_esas_uddannelseselement_plid);

            modelBuilder.Entity<GymnasielleFagkrav>().HasKey(o => o.esas_gymnasielle_fagkravId);
            modelBuilder.Entity<GymnasielleFagkrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            modelBuilder.Entity<GymnasielleKarakterkrav>().HasKey(o => o.esas_gymnasielle_karakterkravid);
            modelBuilder.Entity<GymnasielleKarakterkrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            modelBuilder.Entity<Internationalisering>().HasKey(o => o.esas_internationaliseringId);
            modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_godkender).WithMany().HasForeignKey(o => o.esas_godkender_id);
            modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_institution).WithMany().HasForeignKey(o => o.esas_institution_id);
            modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_studieforloeb).WithMany().HasForeignKey(o => o.esas_studieforloeb_id);

            modelBuilder.Entity<KOTGruppe>().HasKey(o => o.esas_kot_gruppeid);
            modelBuilder.Entity<KOTGruppe>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            modelBuilder.Entity<KOTGruppeTilmelding>().HasKey(o => o.esas_kot_gruppe_tilmeldingid);
            modelBuilder.Entity<KOTGruppeTilmelding>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);
            modelBuilder.Entity<KOTGruppeTilmelding>().HasOptional(o => o.kot_gruppe).WithMany().HasForeignKey(o => o.kot_gruppe_id);

            modelBuilder.Entity<Kommunikation>().HasKey(o => o.esas_kommunikationId);
            modelBuilder.Entity<Kommunikation>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<KurserSkoleophold>().HasKey(o => o.esas_ansoegning_kurser_og_skoleopholdid);
            modelBuilder.Entity<KurserSkoleophold>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Kvalifikationskriterie>().HasKey(o => o.esas_kvalifikationskriterieid);

            modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasKey(o => o.esas_kvalifikationskriterier_for_omraadenumid);
            modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasOptional(o => o.esas_kvalifikationskriterie).WithMany().HasForeignKey(o => o.esas_kvalifikationskriterieid);
            modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasOptional(o => o.esas_omraadeopsaetning).WithMany().HasForeignKey(o => o.esas_omraadeopsaetningid);

            modelBuilder.Entity<Kvalifikationspoint>().HasKey(o => o.esas_kvalifikationspointid);
            modelBuilder.Entity<Kvalifikationspoint>().HasOptional(o => o.esas_kvalifikationskriterie).WithMany().HasForeignKey(o => o.esas_kvalifikationskriterie_id);

            modelBuilder.Entity<KvalifikationspointAnsoegning>().HasKey(o => o.esas_kvalifikationspoint_esas_ansoegningid);
            modelBuilder.Entity<KvalifikationspointAnsoegning>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegningid);
            modelBuilder.Entity<KvalifikationspointAnsoegning>().HasOptional(o => o.esas_kvalifikationspoint).WithMany().HasForeignKey(o => o.esas_kvalifikationspointid);

            modelBuilder.Entity<Proeve>().HasKey(o => o.esas_ansoegning_proeveid);
            modelBuilder.Entity<Proeve>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<Publicering>().HasKey(o => o.esas_publiceringid);
            modelBuilder.Entity<Publicering>().HasOptional(o => o.esas_ansoegningskortopsaetning).WithMany().HasForeignKey(o => o.esas_ansoegningskortopsaetning_id);

            modelBuilder.Entity<UdlandsopholdAnsoegning>().HasKey(o => o.esas_ansoegning_udlandsopholdid);
            modelBuilder.Entity<UdlandsopholdAnsoegning>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<VideregaaendeUddannelse>().HasKey(o => o.esas_ansoegning_videregaaende_uddannelseid);
            modelBuilder.Entity<VideregaaendeUddannelse>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            modelBuilder.Entity<RelationsStatus>().HasKey(o => o.esas_relations_statusId);
            modelBuilder.Entity<OptionSetValueString>().HasKey(o => o.Id);
            modelBuilder.Entity<SystemUser>().HasKey(o => o.SystemUserId);

            //modelBuilder.Entity<StruktureltUddannelseselement>().HasOptional(o => o.esas_overordnet_uddannelseselement).WithMany().HasForeignKey(o => o.esas_overordnet_uddannelseselement_id);
            //.HasOptional( sue => sue.esas_relateret_sue)
            base.OnModelCreating(modelBuilder);
        }

        public EsasStagingDbContext()
        : base("Name=ESAS.ConnectionString")
        {
        }

        public EsasStagingDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public EsasStagingDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        public EsasStagingDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public EsasStagingDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

    }



}
