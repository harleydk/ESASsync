using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// De relationer som vi får fra OData connected service har vi brug for at hæfte attributter på, så de kan anvendes i 
/// en Entity Framework sammenhæng. Disse attributter påføres hér.
/// </summary>
namespace esas.Dynamics.Models.Contracts
{

    [MetadataTypeAttribute(typeof(ProeveIkkeGymEllerVideregNiveau.Metadata))]
    public partial class ProeveIkkeGymEllerVideregNiveau
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_proever_ikke_gym_eller_videreg_niveauId;
        }
    }


    [MetadataTypeAttribute(typeof(SupplerendeKursus.Metadata))]
    public partial class SupplerendeKursus
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_supplerende_kursusId;
        }
    }


    [MetadataTypeAttribute(typeof(Indskrivningsform.Metadata))]
    public partial class Indskrivningsform
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_indskrivningsformId;
        }
    }


    [MetadataTypeAttribute(typeof(Adgangskrav.Metadata))]
    public partial class Adgangskrav
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_adgangskravId;

        }

    }


    [MetadataTypeAttribute(typeof(Kommune.Metadata))]
    public partial class Kommune
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kommuneId;
        }

    }

    [MetadataTypeAttribute(typeof(Rekvirenttype.Metadata))]
    public partial class Rekvirenttype
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_rekvirenttypeId;
        }

    }

    [MetadataTypeAttribute(typeof(Afdelingsniveau.Metadata))]
    public partial class Afdelingsniveau
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_afdelingsniveauId;
        }

    }


    [MetadataTypeAttribute(typeof(Omraadenummeropsaetning.Metadata))]
    public partial class Omraadenummeropsaetning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_omraadeopsaetningid;

            [ForeignKey("esas_ansoegningsopsaetning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningsopsaetning_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;

            [ForeignKey("esas_publicering")]
            public global::System.Nullable<global::System.Guid> esas_publicering_id;

            [ForeignKey("esas_adgangskrav")]
            public global::System.Nullable<global::System.Guid> esas_adgangskrav_id;
        }
    }

    [MetadataTypeAttribute(typeof(PlanlaegningsUddannelseselement.Metadata))]
    public partial class PlanlaegningsUddannelseselement
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_uddannelseselement_planlaegningId;

            [ForeignKey("esas_postnummer_by")]
            public global::System.Nullable<global::System.Guid> esas_postnummer_by_id;

            [ForeignKey("esas_publicering")]
            public global::System.Nullable<global::System.Guid> esas_publicering_id;

            [ForeignKey("esas_adgangskrav")]
            public global::System.Nullable<global::System.Guid> esas_adgangskrav_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;

            [ForeignKey("esas_uddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_uddannelseselement_id;

            [ForeignKey("esas_semester_modul")]
            public global::System.Nullable<global::System.Guid> esas_semester_modul_id;

            [ForeignKey("esas_aktivitetsafdeling")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsafdeling_id;

        }
    }

    [MetadataTypeAttribute(typeof(Samlaesning.Metadata))]
    public partial class Samlaesning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_samlaesningId;
        }
    }


    [MetadataTypeAttribute(typeof(StruktureltUddannelseselement.Metadata))]
    public partial class StruktureltUddannelseselement
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_uddannelseselementId;

            [ForeignKey("esas_adgangskrav")]
            public global::System.Nullable<global::System.Guid> esas_adgangskrav_id;

            [ForeignKey("esas_publicering")]
            public global::System.Nullable<global::System.Guid> esas_publicering_id;

            [ForeignKey("esas_uddannelsesstruktur")]
            public global::System.Nullable<global::System.Guid> esas_uddannelsesstruktur_id;
        }
    }


    [MetadataTypeAttribute(typeof(Eksamenstype.Metadata))]
    public partial class Eksamenstype
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_eksamenstypeId;
        }
    }

    [MetadataTypeAttribute(typeof(Studieforloeb.Metadata))]
    public partial class Studieforloeb
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_studieforloebId;

            [ForeignKey("esas_studerende")]
            public global::System.Nullable<global::System.Guid> esas_studerende_id;

            [ForeignKey("esas_afdeling")]
            public global::System.Nullable<global::System.Guid> esas_afdeling_id;

            [ForeignKey("esas_stamhold")]
            public global::System.Nullable<global::System.Guid> esas_stamhold_id;

            [ForeignKey("esas_skabelonhold")]
            public global::System.Nullable<global::System.Guid> esas_skabelonhold_id;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;

            [ForeignKey("esas_eksamenstype")]
            public global::System.Nullable<global::System.Guid> esas_eksamenstype_id;

            [ForeignKey("esas_national_afgangsaarsag")]
            public global::System.Nullable<global::System.Guid> esas_national_afgangsaarsag_id;

            //[ForeignKey("esas_bevisgrundlag_id")]
            [NotMapped]
            public global::System.Nullable<global::System.Guid> esas_bevisgrundlag_id;
        }
    }

    [MetadataTypeAttribute(typeof(Afdeling.Metadata))]
    public partial class Afdeling
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_afdelingId;

            [ForeignKey("esas_account")]
            public global::System.Nullable<global::System.Guid> esas_account_id;

            [ForeignKey("esas_overordnet_afdeling")]
            public global::System.Nullable<global::System.Guid> esas_overordnet_afdeling_id;

            //(24,10) : error 3004: Problem in mapping fragments starting at line 24:No mapping specified for properties Afdeling.esas_periode_lige_semester_sommer, Afdeling.esas_periode_lige_semester_vinter, Afdeling.esas_periode_ulige_semester_sommer, Afdeling.esas_periode_ulige_semester_vinter in Set Afdeling.
            //          An Entity with Key(PK) will not round-trip when:
            //Entity is type[KP.Synchronization.ESAS.DAL.Afdeling]

        }
    }

    [MetadataTypeAttribute(typeof(InstitutionVirksomhed.Metadata))]
    public partial class InstitutionVirksomhed
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid AccountId;

            [ForeignKey("esas_vist_institutionsoplysning")]
            public global::System.Nullable<global::System.Guid> esas_vist_institutionsoplysning_id;

            [ForeignKey("esas_postnummer_by")]
            public global::System.Nullable<global::System.Guid> esas_postnummer_by_id;

            [ForeignKey("esas_branche")]
            public global::System.Nullable<global::System.Guid> esas_branche_id;

            [ForeignKey("esas_land")]
            public global::System.Nullable<global::System.Guid> esas_land_id;

        }
    }

    [MetadataTypeAttribute(typeof(Afslagsbegrundelse.Metadata))]
    public partial class Afslagsbegrundelse
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_afslagsbegrundelseId;
        }
    }

    [MetadataTypeAttribute(typeof(Postnummer.Metadata))]
    public partial class Postnummer
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_postnummerId;

            [ForeignKey("esas_land")]
            public global::System.Nullable<global::System.Guid> esas_land_id;
        }
    }

    [MetadataTypeAttribute(typeof(Person.Metadata))]
    public partial class Person
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid ContactId;

            [ForeignKey("esas_postnummer_by")]
            public global::System.Nullable<global::System.Guid> esas_postnummer_by_id;

            [ForeignKey("esas_statsborgerskab")]
            public global::System.Nullable<global::System.Guid> esas_statsborgerskab_id;

            [ForeignKey("esas_land")]
            public global::System.Nullable<global::System.Guid> esas_land_id;

        }
    }

    [MetadataTypeAttribute(typeof(Ansoeger.Metadata))]
    public partial class Ansoeger
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid LeadId;

            [ForeignKey("esas_postnummer_by")]
            public global::System.Nullable<global::System.Guid> esas_postnummer_by_id;

            [ForeignKey("esas_statsborgerskab")]
            public global::System.Nullable<global::System.Guid> esas_statsborgerskab_id;

            [ForeignKey("esas_land")]
            public global::System.Nullable<global::System.Guid> esas_land_id;

        }
    }

    [MetadataTypeAttribute(typeof(Land.Metadata))]
    public partial class Land
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_landId;

        }
    }

    [MetadataTypeAttribute(typeof(Ansoegning.Metadata))]
    public partial class Ansoegning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningId;


            [ForeignKey("esas_ansoeger")]
            public global::System.Nullable<global::System.Guid> esas_ansoeger_id;

            [ForeignKey("esas_ansoegningsopsaetning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningsopsaetning_id;

            [ForeignKey("esas_person_studerende")]
            public global::System.Nullable<global::System.Guid> esas_person_studerende_id;


            [ForeignKey("esas_planlaegningselement")]
            public global::System.Nullable<global::System.Guid> esas_planlaegningselement_id;

            [ForeignKey("esas_virksomhed")]
            public global::System.Nullable<global::System.Guid> esas_virksomhed_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;


            [ForeignKey("esas_ag_eksamensland")]
            public global::System.Nullable<global::System.Guid> esas_ag_eksamensland_id;

            [ForeignKey("esas_eksamenstype")]
            public global::System.Nullable<global::System.Guid> esas_eksamenstype_id;

            [ForeignKey("esas_omraadenummeropsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadenummeropsaetning_id;

        }
    }

    [MetadataTypeAttribute(typeof(Branche.Metadata))]
    public partial class Branche
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_brancheId;

        }
    }


    [MetadataTypeAttribute(typeof(Aktivitetsudbud.Metadata))]
    public partial class Aktivitetsudbud
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_aktivitetsudbudId;


            [ForeignKey("esas_aktivitetsafdeling")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsafdeling_id;

            [ForeignKey("esas_institutionsafdeling")]
            public global::System.Nullable<global::System.Guid> esas_institutionsafdeling_id;

            [ForeignKey("esas_uddannelsesstruktur")]
            public global::System.Nullable<global::System.Guid> esas_uddannelsesstruktur_id;

        }
    }


    [MetadataTypeAttribute(typeof(Uddannelsesstruktur.Metadata))]
    public partial class Uddannelsesstruktur
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_uddannelsesstrukturId;


            [ForeignKey("esas_uddannelsesaktivitet")]
            public global::System.Nullable<global::System.Guid> esas_uddannelsesaktivitet_id;
        }
    }

    [MetadataTypeAttribute(typeof(Ansoegningsopsaetning.Metadata))]
    public partial class Ansoegningsopsaetning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningsopsaetningId;

        }
    }

    [MetadataTypeAttribute(typeof(Bilag.Metadata))]
    public partial class Bilag
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_bilagid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(AndenAktivitet.Metadata))]
    public partial class AndenAktivitet
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_andre_aktiviteterid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(Enkeltfag.Metadata))]
    public partial class Enkeltfag
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_enkeltfagid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;


            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;
        }
    }

    [MetadataTypeAttribute(typeof(Erfaringer.Metadata))]
    public partial class Erfaringer
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_erfaringerid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(KurserSkoleophold.Metadata))]
    public partial class KurserSkoleophold
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_kurser_og_skoleopholdid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(Proeve.Metadata))]
    public partial class Proeve
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_proeveid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(Kommunikation.Metadata))]
    public partial class Kommunikation
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kommunikationId;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(UdlandsopholdAnsoegning.Metadata))]
    public partial class UdlandsopholdAnsoegning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_udlandsopholdid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(VideregaaendeUddannelse.Metadata))]
    public partial class VideregaaendeUddannelse
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_videregaaende_uddannelseid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(Ansoegningshandling.Metadata))]
    public partial class Ansoegningshandling
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningshandlingId;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;
        }
    }

    [MetadataTypeAttribute(typeof(KvalifikationspointAnsoegning.Metadata))]
    public partial class KvalifikationspointAnsoegning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kvalifikationspoint_esas_ansoegningid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningid;

            [ForeignKey("esas_kvalifikationspoint")]
            public global::System.Nullable<global::System.Guid> esas_kvalifikationspointid;

        }
    }

    [MetadataTypeAttribute(typeof(KOTGruppeTilmelding.Metadata))]
    public partial class KOTGruppeTilmelding
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kot_gruppe_tilmeldingid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;

            [ForeignKey("esas_kot_gruppe")]
            public global::System.Nullable<global::System.Guid> esas_kot_gruppe_id;

        }
    }



    [MetadataTypeAttribute(typeof(Specialisering.Metadata))]
    public partial class Specialisering
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegning_specialiseringid;

            [ForeignKey("esas_ansoegning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegning_id;

            [ForeignKey("esas_omraadespecialisering")]
            public global::System.Nullable<global::System.Guid> esas_omraadespecialisering_id;

        }
    }


    [MetadataTypeAttribute(typeof(Ansoegningskort.Metadata))]
    public partial class Ansoegningskort
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningskortid;

            [ForeignKey("esas_ansoegningskortopsaetning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningskortopsaetning_id;

            [ForeignKey("esas_ansoegningskorttekst")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningskorttekst_id;

        }
    }

    [MetadataTypeAttribute(typeof(AnsoegningskortOpsaetning.Metadata))]
    public partial class AnsoegningskortOpsaetning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningskortopsaetningid;
        }
    }

    [MetadataTypeAttribute(typeof(AnsoegningskortTekst.Metadata))]
    public partial class AnsoegningskortTekst
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_ansoegningskorttekstid;
        }
    }

    [MetadataTypeAttribute(typeof(Publicering.Metadata))]
    public partial class Publicering
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_publiceringid;

            [ForeignKey("esas_ansoegningskortopsaetning")]
            public global::System.Nullable<global::System.Guid> esas_ansoegningskortopsaetning_id;

        }
    }

    [MetadataTypeAttribute(typeof(Bedoemmelse.Metadata))]
    public partial class Bedoemmelse
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_bedoemmelseId;

            [ForeignKey("esas_bedoemmelsesrunde")]
            public global::System.Nullable<global::System.Guid> esas_bedoemmelsesrunde_id;

            [ForeignKey("esas_gennemfoerelsesuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_gennemfoerelsesuddannelseselement_id;

            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;

            [ForeignKey("esas_karakter")]
            public global::System.Nullable<global::System.Guid> esas_karakter_id;

        }
    }


    [MetadataTypeAttribute(typeof(Fagpersonsrelation.Metadata))]
    public partial class Fagpersonsrelation
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_fagpersonsrelationId;

            [ForeignKey("esas_bedoemmelse")]
            public global::System.Nullable<global::System.Guid> esas_bedoemmelse_id;

            [ForeignKey("esas_fagperson")]
            public global::System.Nullable<global::System.Guid> esas_fagperson_id;

            [ForeignKey("esas_hold")]
            public global::System.Nullable<global::System.Guid> esas_hold_id;

            [ForeignKey("esas_planlaegningsuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_planlaegningsuddannelseselement_id;
        }
    }

    [MetadataTypeAttribute(typeof(Bedoemmelsesrunde.Metadata))]
    public partial class Bedoemmelsesrunde
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_bedoemmelsesrundeId;

            [ForeignKey("esas_omraadeopsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadeopsaetning_id;

            [ForeignKey("esas_omraadespecialisering")]
            public global::System.Nullable<global::System.Guid> esas_omraadespecialisering_id;

            [ForeignKey("esas_planlaegningsuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_planlaegningsuddannelseselement_id;
        }

    }

    [MetadataTypeAttribute(typeof(GennemfoerelsesUddannelseselement.Metadata))]
    public partial class GennemfoerelsesUddannelseselement
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_uddannelseselement_gennemfoerelseId;

            [ForeignKey("esas_pue")]
            public global::System.Nullable<global::System.Guid> esas_pue_id;

            [ForeignKey("esas_uddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_uddannelseselement_id;

            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;

            [ForeignKey("esas_bedoemmelsesresultat")]
            public global::System.Nullable<global::System.Guid> esas_bedoemmelsesresultat_id;

            [ForeignKey("esas_aktivitetsafdeling")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsafdeling_id;

            [ForeignKey("esas_udbud_af_valgfag")]
            public global::System.Nullable<global::System.Guid> esas_udbud_af_valgfag_id;


        }

    }

    [MetadataTypeAttribute(typeof(Karakter.Metadata))]
    public partial class Karakter
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_karakterId;

        }
    }


    [MetadataTypeAttribute(typeof(Omraadespecialisering.Metadata))]
    public partial class Omraadespecialisering
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_omraadespecialiseringid;

            [ForeignKey("esas_omraadenummeropsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadenummeropsaetning_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;

        }

    }


    [MetadataTypeAttribute(typeof(Bevisgrundlag.Metadata))]
    public partial class Bevisgrundlag
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_bevisgrundlagId;

        }
    }

    [MetadataTypeAttribute(typeof(Personoplysning.Metadata))]
    public partial class Personoplysning
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_personoplysningerId;

            [ForeignKey("esas_person")]
            public global::System.Nullable<global::System.Guid> esas_person_id;
        }

    }


    [MetadataTypeAttribute(typeof(Hold.Metadata))]
    public partial class Hold
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_holdId;

            [ForeignKey("esas_planlaegningsuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_planlaegningsuddannelseselement_id;

            [ForeignKey("esas_institution")]
            public global::System.Nullable<global::System.Guid> esas_institution_id;

            [ForeignKey("esas_aktivitetsudbud")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsudbud_id;

            [ForeignKey("esas_publicering")]
            public global::System.Nullable<global::System.Guid> esas_publicering_id;

            [ForeignKey("esas_aktivitetsafdeling")]
            public global::System.Nullable<global::System.Guid> esas_aktivitetsafdeling_id;
        }

    }

    [MetadataTypeAttribute(typeof(Fravaersaarsag.Metadata))]
    public partial class Fravaersaarsag
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_fravaersaarsagId;
        }

    }


    [MetadataTypeAttribute(typeof(StudieinaktivPeriode.Metadata))]
    public partial class StudieinaktivPeriode
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_studieinaktiv_periodeId;

            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;

            [ForeignKey("esas_aarsag")]
            public global::System.Nullable<global::System.Guid> esas_aarsag_id;

        }
    }

    [MetadataTypeAttribute(typeof(Gebyrtype.Metadata))]
    public partial class Gebyrtype
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_gebyrtypeid;
        }
    }


    [MetadataTypeAttribute(typeof(Praktikophold.Metadata))]
    public partial class Praktikophold
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_praktikopholdId;

            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;

            [ForeignKey("esas_gennemfoerelsesuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_gennemfoerelsesuddannelseselement_id;

            [ForeignKey("esas_praktiksted")]
            public global::System.Nullable<global::System.Guid> esas_praktiksted_id;

            [ForeignKey("esas_praktikomraade")]
            public global::System.Nullable<global::System.Guid> esas_praktikomraade_id;

            [ForeignKey("esas_praktikvejleder")]
            public global::System.Nullable<global::System.Guid> esas_praktikvejleder_id;

        }

    }

    [MetadataTypeAttribute(typeof(MeritRegistrering.Metadata))]
    public partial class MeritRegistrering
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_meritregistreringId;

            [ForeignKey("esas_gennemfoerelsesuddannelseselement")]
            public global::System.Nullable<global::System.Guid> esas_gennemfoerelsesuddannelseselement_id;

            [ForeignKey("esas_karakter")]
            public global::System.Nullable<global::System.Guid> esas_karakter_id;
        }

    }

    [MetadataTypeAttribute(typeof(GymnasielleFagkrav.Metadata))]
    public partial class GymnasielleFagkrav
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_gymnasielle_fagkravId;

            [ForeignKey("esas_omraadenummeropsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadenummeropsaetning_id;
        }

    }

    [MetadataTypeAttribute(typeof(GymnasielleKarakterkrav.Metadata))]
    public partial class GymnasielleKarakterkrav
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_gymnasielle_karakterkravid;

            [ForeignKey("esas_omraadenummeropsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadenummeropsaetning_id;
        }

    }

    [MetadataTypeAttribute(typeof(Institutionsoplysninger.Metadata))]
    public partial class Institutionsoplysninger
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_institutionsoplysningerId;

            [ForeignKey("esas_institution_virksomhed")]
            public global::System.Nullable<global::System.Guid> esas_institution_virksomhed_id;
        }

    }

    [MetadataTypeAttribute(typeof(Internationalisering.Metadata))]
    public partial class Internationalisering
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_internationaliseringId;

            [ForeignKey("esas_studieforloeb")]
            public global::System.Nullable<global::System.Guid> esas_studieforloeb_id;

            [ForeignKey("esas_institution")]
            public global::System.Nullable<global::System.Guid> esas_institution_id;

        }

    }

    [MetadataTypeAttribute(typeof(Kvalifikationskriterie.Metadata))]
    public partial class Kvalifikationskriterie
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kvalifikationskriterieid;
        }

    }


    [MetadataTypeAttribute(typeof(Kvalifikationspoint.Metadata))]
    public partial class Kvalifikationspoint
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kvalifikationspointid;


            [ForeignKey("esas_kvalifikationskriterie")]
            public global::System.Nullable<global::System.Guid> esas_kvalifikationskriterie_id;
        }

    }



    [MetadataTypeAttribute(typeof(NationalAfgangsaarsag.Metadata))]
    public partial class NationalAfgangsaarsag
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_national_afgangsaarsagId;
        }

    }


    [MetadataTypeAttribute(typeof(Omraadenummer.Metadata))]
    public partial class Omraadenummer
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_omraadenummerId;
        }
    }


    [MetadataTypeAttribute(typeof(KOTGruppe.Metadata))]
    public partial class KOTGruppe
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_kot_gruppeid;


            [ForeignKey("esas_omraadenummeropsaetning")]
            public global::System.Nullable<global::System.Guid> esas_omraadenummeropsaetning_id;
        }

    }

    [MetadataTypeAttribute(typeof(OptionSetValueString.Metadata))]
    public partial class OptionSetValueString
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.String Id;
        }
    }

    [MetadataTypeAttribute(typeof(Praktikomraade.Metadata))]
    public partial class Praktikomraade
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_praktikomraadeId;
        }
    }

    [MetadataTypeAttribute(typeof(RelationsStatus.Metadata))]
    public partial class RelationsStatus
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_relations_statusId;
        }
    }

    [MetadataTypeAttribute(typeof(Uddannelsesaktivitet.Metadata))]
    public partial class Uddannelsesaktivitet
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Key]
            public global::System.Guid esas_uddannelsesaktivitetId;

            //[ForeignKey("esas_bekendtgoerelse")]
            //public global::System.Nullable<global::System.Guid> esas_bekendtgoerelse_id;

        }
    }


}
