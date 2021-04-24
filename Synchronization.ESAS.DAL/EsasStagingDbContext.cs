using esas.Dynamics.Models.Contracts;
using Synchronization.ESAS.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.OData.Client;

namespace Synchronization.ESAS.DAL
{
    public class EsasStagingDbContext : DbContext
    {
        public DbSet<Adgangskrav> Adgangskrav { get; set; }
        public DbSet<Afdeling> Afdeling { get; set; }
        public DbSet<Afdelingsniveau> Afdelingsniveau { get; set; }
        public DbSet<Afslagsbegrundelse> Afslagsbegrundelse { get; set; }
        public DbSet<Aktivitetsudbud> Aktivitetsudbud { get; set; }
        public DbSet<AndenAktivitet> AndenAktivitet { get; set; }
        public DbSet<Ansoeger> Ansoeger { get; set; }
        public DbSet<Ansoegning> Ansoegning { get; set; }
        public DbSet<Ansoegningshandling> Ansoegningshandling { get; set; }
        public DbSet<Ansoegningskort> Ansoegningskort { get; set; }
        public DbSet<AnsoegningskortOpsaetning> AnsoegningskortOpsaetning { get; set; }
        public DbSet<AnsoegningskortTekst> AnsoegningskortTekst { get; set; }
        public DbSet<Ansoegningsopsaetning> Ansoegningsopsaetning { get; set; }
        public DbSet<Bedoemmelse> Bedoemmelse { get; set; }
        public DbSet<Bedoemmelsesrunde> Bedoemmelsesrunde { get; set; }
        //public DbSet<Bevisgrundlag> Bevisgrundlag { get; set; } -- meget stor og omfattende tabel. Er usikker på dens brug - sløjfer den, for now.
        public DbSet<Bilag> Bilag { get; set; }
        public DbSet<Branche> Branche { get; set; }
        public DbSet<Eksamenstype> Eksamenstype { get; set; }
        public DbSet<Enkeltfag> Enkeltfag { get; set; }
        public DbSet<Erfaringer> Erfaringer { get; set; }
        public DbSet<Fagpersonsrelation> Fagpersonsrelation { get; set; }
        public DbSet<Fravaersaarsag> Fravaersaarsag { get; set; }
        public DbSet<Gebyrtype> Gebyrtype { get; set; }
        public DbSet<GennemfoerelsesUddannelseselement> GennemfoerelsesUddannelseselement { get; set; }
        public DbSet<GymnasielleFagkrav> GymnasielleFagkrav { get; set; }
        public DbSet<GymnasielleKarakterkrav> GymnasielleKarakterkrav { get; set; }
        public DbSet<Hold> Hold { get; set; }
        public DbSet<Indskrivningsform> Indskrivningsform { get; set; }
        public DbSet<InstitutionVirksomhed> InstitutionVirksomhed { get; set; }
        public DbSet<Institutionsoplysninger> Institutionsoplysninger { get; set; }
        public DbSet<Internationalisering> Internationalisering { get; set; }
        public DbSet<KOTGruppe> KOTGruppe { get; set; }
        public DbSet<KOTGruppeTilmelding> KOTGruppeTilmelding { get; set; }
        public DbSet<Karakter> Karakter { get; set; }
        public DbSet<Kommunikation> Kommunikation { get; set; }
        public DbSet<Kommune> Kommune { get; set; }
        public DbSet<KurserSkoleophold> KurserSkoleophold { get; set; }
        public DbSet<Kvalifikationskriterie> Kvalifikationskriterie { get; set; }
        public DbSet<Kvalifikationspoint> Kvalifikationspoint { get; set; }
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
        public DbSet<Rekvirenttype> Rekvirenttype { get; set; }
        public DbSet<Specialisering> Specialisering { get; set; }
        public DbSet<Samlaesning> Samlaesning { get; set; }
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
            
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            // Ignorér alle collections referencer. Vi har ikke brug for dem i EF sammenhæng, frameworket ved godt selv hvordan tabellerne hænger sammen.

            // Aktører
            modelBuilder.Entity<Person>().HasOptional(o => o.esas_statsborgerskab).WithMany().HasForeignKey(o => o.esas_statsborgerskab_id);
            modelBuilder.Entity<Person>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<Person>().Ignore(o => o.list_esas_personoplysning);
            modelBuilder.Entity<Person>().Ignore(o => o.list_esas_ansoegning);
            modelBuilder.Entity<Person>().Ignore(o => o.list_esas_praktikophold);
            
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_ansoeger);
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_ansoegning);
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_institution_virksomhed);
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_person);
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_postnummer);
            modelBuilder.Entity<Land>().Ignore(o => o.list_esas_studieforloeb);

            modelBuilder.Entity<Postnummer>().Ignore(o => o.list_esas_ansoeger);
            modelBuilder.Entity<Postnummer>().Ignore(o => o.list_esas_institution_virksomhed);
            modelBuilder.Entity<Postnummer>().Ignore(o => o.list_esas_person);
            modelBuilder.Entity<Postnummer>().Ignore(o => o.list_esas_planlaegningsUddannelseselement);

            modelBuilder.Entity<Kommune>().Ignore(o => o.list_esas_person);

            modelBuilder.Entity<Rekvirenttype>().Ignore(o => o.list_esas_ansoegning);
            
            modelBuilder.Entity<Afdelingsniveau>().Ignore(o => o.list_esas_afdeling);

            modelBuilder.Entity<Branche>().Ignore(o => o.list_esas_institution_virksomhed);

            modelBuilder.Entity<Personoplysning>().Ignore(o => o.list_esas_fagpersonrelation);
            
            modelBuilder.Entity<Afdeling>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<Afdeling>().Ignore(o => o.esas_periode_lige_semester_sommer);
            modelBuilder.Entity<Afdeling>().Ignore(o => o.esas_periode_lige_semester_vinter);
            modelBuilder.Entity<Afdeling>().Ignore(o => o.esas_periode_ulige_semester_sommer);
            modelBuilder.Entity<Afdeling>().Ignore(o => o.esas_periode_ulige_semester_vinter);

            //// Uddannelsesstrukturer

            modelBuilder.Entity<Uddannelsesaktivitet>().Ignore(o => o.esas_bekendtgoerelse);
            modelBuilder.Entity<Uddannelsesaktivitet>().Ignore(o => o.list_esas_uddannelsesstruktur);

            modelBuilder.Entity<Uddannelsesstruktur>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<Uddannelsesstruktur>().Ignore(o => o.list_esas_uddannelseselement);

            modelBuilder.Entity<StruktureltUddannelseselement>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<StruktureltUddannelseselement>().Ignore(o => o.list_esas_uddannelseselement_gennemfoerelse);
            modelBuilder.Entity<StruktureltUddannelseselement>().Ignore(o => o.list_esas_uddannelseselement_planlaegning);
            modelBuilder.Entity<StruktureltUddannelseselement>().Ignore(o => o.list_esas_uddannelseselement_planlaegning_gruppering);
            modelBuilder.Entity<StruktureltUddannelseselement>().Ignore(o => o.list_esas_uddannelseselement_planlaegning_semester);


            //// PUE/Hold

            modelBuilder.Entity<PlanlaegningsUddannelseselement>().Ignore(o => o.list_esas_bedoemmelsesrunde);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().Ignore(o => o.list_esas_fagpersonsrelation);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().Ignore(o => o.list_esas_gebyrtype);
            modelBuilder.Entity<PlanlaegningsUddannelseselement>().Ignore(o => o.list_esas_uddannelseselement_gennemfoerelse);

            modelBuilder.Entity<Hold>().Ignore(o => o.list_esas_fagpersonsrelation);
            modelBuilder.Entity<Hold>().Ignore(o => o.list_esas_uddannelseselement_gennemfoerelse);

            //// studieforløb
            modelBuilder.Entity<Indskrivningsform>().Ignore(o => o.list_esas_studieforloeb);

            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.list_esas_enkeltfag);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.list_esas_internationalisering);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.list_esas_praktikophold);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.list_esas_studieinaktiv_periodes);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.list_esas_uddannelseselement_gennemfoerelse);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.Valgfag);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.UdbudteValgfag);
            modelBuilder.Entity<Studieforloeb>().Ignore(o => o.esas_bevisgrundlag);


            modelBuilder.Entity<Karakter>().Ignore(o => o.list_esas_bedoemmelse);
            modelBuilder.Entity<Karakter>().Ignore(o => o.list_esas_uddannelseselement_gennemfoerelse);

            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().Ignore(o => o.list_esas_bedoemmelse);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().Ignore(o => o.list_esas_meritregistrering);
            modelBuilder.Entity<GennemfoerelsesUddannelseselement>().Ignore(o => o.list_esas_praktikophold);

            modelBuilder.Entity<Fravaersaarsag>().Ignore(o => o.list_esas_studieinaktiv_periodes);


            modelBuilder.Entity<Praktikomraade>().Ignore(o => o.list_esas_praktikophold);


            ////eksamens
            modelBuilder.Entity<Eksamenstype>().Ignore(o => o.list_esas_studieforloeb);

            modelBuilder.Entity<Bedoemmelse>().Ignore(o => o.list_esas_fagpersonsrelation);

            modelBuilder.Entity<Bedoemmelsesrunde>().Ignore(o => o.list_esas_bedoemmelse);

            modelBuilder.Entity<Bevisgrundlag>().Ignore(o => o.list_esas_studieforloeb);


            //// ansøgning


            modelBuilder.Entity<Ansoeger>().Ignore(o => o.list_esas_ansoegning);
            
            modelBuilder.Entity<Uddannelsesstruktur>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<Uddannelsesstruktur>().Ignore(o => o.list_esas_uddannelseselement);

            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_andre_aktiviteter);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_ansoegningshandling);
            //modelBuilder.Entity<Ansoegning>().Ignore(o => o.KotGrupper);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_bilag);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_enkeltfag);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_erfaringer);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_kommunikation);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_kotGruppe_tilmeldings);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_kurser_skoleophold);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_kvalifikationspoint);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_proeve);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_specialisering);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_studieforloeb);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_udlandsophold_ansoegning);
            modelBuilder.Entity<Ansoegning>().Ignore(o => o.list_esas_vidregaaende_uddannelse);


            modelBuilder.Entity<AnsoegningskortOpsaetning>().Ignore(o => o.list_esas_ansoegningskort);
            modelBuilder.Entity<AnsoegningskortOpsaetning>().Ignore(o => o.list_esas_publicering);

            //modelBuilder.Entity<AnsoegningskortTekst>().HasKey(o => o.esas_ansoegningskorttekstid);

            //modelBuilder.Entity<Ansoegningsopsaetning>().HasKey(o => o.esas_ansoegningsopsaetningId);

            modelBuilder.Entity<Adgangskrav>().Ignore(o => o.list_esas_omraadenummeropsaetning);
            modelBuilder.Entity<Adgangskrav>().Ignore(o => o.list_esas_planlaegningsUddannelseselement);
            modelBuilder.Entity<Adgangskrav>().Ignore(o => o.list_esas_struktureltUddannelseselement);

            //modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);
            //modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_planlaegningsUddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsUddannelseselement_id);
            //modelBuilder.Entity<Adgangskrav>().HasOptional(o => o.esas_struktureltUddannelseselement).WithMany().HasForeignKey(o => o.esas_struktureltUddannelseselement_id);

            //modelBuilder.Entity<Afslagsbegrundelse>().HasKey(o => o.esas_afslagsbegrundelseId);

            //modelBuilder.Entity<Bilag>().HasKey(o => o.esas_bilagid);
            //modelBuilder.Entity<Bilag>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<Omraadenummer>().HasKey(o => o.esas_omraadenummerId);

            //modelBuilder.Entity<Omraadenummeropsaetning>().HasKey(o => o.esas_omraadeopsaetningid);
            //modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_adgangskrav).WithMany().HasForeignKey(o => o.esas_adgangskrav_id);
            //modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);
            //modelBuilder.Entity<Omraadenummeropsaetning>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);

            //modelBuilder.Entity<Omraadespecialisering>().HasKey(o => o.esas_omraadespecialiseringid);
            ////modelBuilder.Entity<Omraadespecialisering>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id); // kan ikke sættes på p.t. - test data er mangelfulde.
            //modelBuilder.Entity<Omraadespecialisering>().HasOptional(o => o.esas_aktivitetsudbud).WithMany().HasForeignKey(o => o.esas_aktivitetsudbud_id);

            //modelBuilder.Entity<Specialisering>().HasKey(o => o.esas_ansoegning_specialiseringid);
            //modelBuilder.Entity<Specialisering>().HasOptional(o => o.esas_ansogning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);
            //modelBuilder.Entity<Specialisering>().HasOptional(o => o.esas_omraadespecialisering).WithMany().HasForeignKey(o => o.esas_omraadespecialisering_id);

            //modelBuilder.Entity<NationalAfgangsaarsag>().HasKey(o => o.esas_national_afgangsaarsagId);

            //modelBuilder.Entity<MeritRegistrering>().HasKey(o => o.esas_meritregistreringId);
            //modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_gennemfoerelsesuddannelseselement).WithMany().HasForeignKey(o => o.esas_gennemfoerelsesuddannelseselement_id);
            //modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_godkender).WithMany().HasForeignKey(o => o.esas_godkender_id);
            //modelBuilder.Entity<MeritRegistrering>().HasOptional(o => o.esas_karakter).WithMany().HasForeignKey(o => o.esas_karakter_id);

            //modelBuilder.Entity<Enkeltfag>().HasKey(o => o.esas_ansoegning_enkeltfagid);

            //// diff
            //modelBuilder.Entity<AndenAktivitet>().HasKey(o => o.esas_ansoegning_andre_aktiviteterid);
            //modelBuilder.Entity<AndenAktivitet>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<Erfaringer>().HasKey(o => o.esas_ansoegning_erfaringerid);
            //modelBuilder.Entity<Erfaringer>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<Fagpersonsrelation>().HasKey(o => o.esas_fagpersonsrelationId);
            //modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_bedoemmelse).WithMany().HasForeignKey(o => o.esas_bedoemmelse_id);
            //modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_fagperson).WithMany().HasForeignKey(o => o.esas_fagperson_id);
            //modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_hold).WithMany().HasForeignKey(o => o.esas_hold_id);
            //modelBuilder.Entity<Fagpersonsrelation>().HasOptional(o => o.esas_planlaegningsuddannelseselement).WithMany().HasForeignKey(o => o.esas_planlaegningsuddannelseselement_id);

            //modelBuilder.Entity<Gebyrtype>().HasKey(o => o.esas_gebyrtypeid);
            //modelBuilder.Entity<GebyrtypePUERelation>().HasKey(o => o.esas_gebyrtype_esas_uddannelseselement_plid);

            //modelBuilder.Entity<GymnasielleFagkrav>().HasKey(o => o.esas_gymnasielle_fagkravId);
            //modelBuilder.Entity<GymnasielleFagkrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            //modelBuilder.Entity<GymnasielleKarakterkrav>().HasKey(o => o.esas_gymnasielle_karakterkravid);
            //modelBuilder.Entity<GymnasielleKarakterkrav>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            //modelBuilder.Entity<Internationalisering>().HasKey(o => o.esas_internationaliseringId);
            //modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_godkender).WithMany().HasForeignKey(o => o.esas_godkender_id);
            //modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_institution).WithMany().HasForeignKey(o => o.esas_institution_id);
            //modelBuilder.Entity<Internationalisering>().HasOptional(o => o.esas_studieforloeb).WithMany().HasForeignKey(o => o.esas_studieforloeb_id);

            //modelBuilder.Entity<KOTGruppe>().HasKey(o => o.esas_kot_gruppeid);
            //modelBuilder.Entity<KOTGruppe>().HasOptional(o => o.esas_omraadenummeropsaetning).WithMany().HasForeignKey(o => o.esas_omraadenummeropsaetning_id);

            //modelBuilder.Entity<KOTGruppeTilmelding>().HasKey(o => o.esas_kot_gruppe_tilmeldingid);
            //modelBuilder.Entity<KOTGruppeTilmelding>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);
            //modelBuilder.Entity<KOTGruppeTilmelding>().HasOptional(o => o.kot_gruppe).WithMany().HasForeignKey(o => o.kot_gruppe_id);

            //modelBuilder.Entity<Kommunikation>().HasKey(o => o.esas_kommunikationId);
            //modelBuilder.Entity<Kommunikation>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<KurserSkoleophold>().HasKey(o => o.esas_ansoegning_kurser_og_skoleopholdid);
            //modelBuilder.Entity<KurserSkoleophold>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<Kvalifikationskriterie>().HasKey(o => o.esas_kvalifikationskriterieid);

            //modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasKey(o => o.esas_kvalifikationskriterier_for_omraadenumid);
            //modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasOptional(o => o.esas_kvalifikationskriterie).WithMany().HasForeignKey(o => o.esas_kvalifikationskriterieid);
            //modelBuilder.Entity<KvalifikationskriterieOmraadenummeropsaetning>().HasOptional(o => o.esas_omraadeopsaetning).WithMany().HasForeignKey(o => o.esas_omraadeopsaetningid);

            //modelBuilder.Entity<Kvalifikationspoint>().HasKey(o => o.esas_kvalifikationspointid);
            //modelBuilder.Entity<Kvalifikationspoint>().HasOptional(o => o.esas_kvalifikationskriterie).WithMany().HasForeignKey(o => o.esas_kvalifikationskriterie_id);

            //modelBuilder.Entity<KvalifikationspointAnsoegning>().HasKey(o => o.esas_kvalifikationspoint_esas_ansoegningid);
            //modelBuilder.Entity<KvalifikationspointAnsoegning>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegningid);
            //modelBuilder.Entity<KvalifikationspointAnsoegning>().HasOptional(o => o.esas_kvalifikationspoint).WithMany().HasForeignKey(o => o.esas_kvalifikationspointid);

            //modelBuilder.Entity<Proeve>().HasKey(o => o.esas_ansoegning_proeveid);
            //modelBuilder.Entity<Proeve>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<Publicering>().HasKey(o => o.esas_publiceringid);
            //modelBuilder.Entity<Publicering>().HasOptional(o => o.esas_ansoegningskortopsaetning).WithMany().HasForeignKey(o => o.esas_ansoegningskortopsaetning_id);

            //modelBuilder.Entity<UdlandsopholdAnsoegning>().HasKey(o => o.esas_ansoegning_udlandsopholdid);
            //modelBuilder.Entity<UdlandsopholdAnsoegning>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<VideregaaendeUddannelse>().HasKey(o => o.esas_ansoegning_videregaaende_uddannelseid);
            //modelBuilder.Entity<VideregaaendeUddannelse>().HasOptional(o => o.esas_ansoegning).WithMany().HasForeignKey(o => o.esas_ansoegning_id);

            //modelBuilder.Entity<RelationsStatus>().HasKey(o => o.esas_relations_statusId);
            //modelBuilder.Entity<OptionSetValueString>().HasKey(o => o.Id);
            //modelBuilder.Entity<SystemUser>().HasKey(o => o.SystemUserId);

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
