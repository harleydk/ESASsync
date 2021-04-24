using Default; // Unchase OData connected service 'default' container name
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

/*
	Disse klasser er auto-genererede, på basis af deres korresponderende T4-template. 
	Hvis der skal rettes i klasserne, skal rettelserne foregå i template'n.
*/
namespace KP.Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    public class AdgangskravFullLoadStrategy : BaseLoadStrategy
    {
        public AdgangskravFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Adgangskrav;
            return modifiedObjects;
        }
    }


    public class AfdelingFullLoadStrategy : BaseLoadStrategy
    {
        public AfdelingFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Afdeling;
            return modifiedObjects;
        }
    }


    public class AndenAktivitetFullLoadStrategy : BaseLoadStrategy
    {
        public AndenAktivitetFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.AndenAktivitet;
            return modifiedObjects;
        }
    }


    public class AnsoegerFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegerFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoeger;
            return modifiedObjects;
        }
    }


    public class AnsoegningFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegningFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegning;
            return modifiedObjects;
        }
    }


    public class AnsoegningshandlingFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegningshandlingFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningshandling;
            return modifiedObjects;
        }
    }


    public class AnsoegningskortFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegningskortFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningskort;
            return modifiedObjects;
        }
    }


    public class AnsoegningskortOpsaetningFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegningskortOpsaetningFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.AnsoegningskortOpsaetning;
            return modifiedObjects;
        }
    }


    public class AnsoegningsopsaetningFullLoadStrategy : BaseLoadStrategy
    {
        public AnsoegningsopsaetningFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningsopsaetning;
            return modifiedObjects;
        }
    }


    public class BedoemmelseFullLoadStrategy : BaseLoadStrategy
    {
        public BedoemmelseFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bedoemmelse;
            return modifiedObjects;
        }
    }


    public class BedoemmelsesrundeFullLoadStrategy : BaseLoadStrategy
    {
        public BedoemmelsesrundeFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bedoemmelsesrunde;
            return modifiedObjects;
        }
    }


    public class BevisgrundlagFullLoadStrategy : BaseLoadStrategy
    {
        public BevisgrundlagFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bevisgrundlag;
            return modifiedObjects;
        }
    }


    public class BilagFullLoadStrategy : BaseLoadStrategy
    {
        public BilagFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bilag;
            return modifiedObjects;
        }
    }


    public class BrancheFullLoadStrategy : BaseLoadStrategy
    {
        public BrancheFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Branche;
            return modifiedObjects;
        }
    }


    public class EnkeltfagFullLoadStrategy : BaseLoadStrategy
    {
        public EnkeltfagFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Enkeltfag;
            return modifiedObjects;
        }
    }


    public class ErfaringerFullLoadStrategy : BaseLoadStrategy
    {
        public ErfaringerFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Erfaringer;
            return modifiedObjects;
        }
    }


    public class FagpersonsrelationFullLoadStrategy : BaseLoadStrategy
    {
        public FagpersonsrelationFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Fagpersonsrelation;
            return modifiedObjects;
        }
    }


    public class GebyrtypePUERelationFullLoadStrategy : BaseLoadStrategy
    {
        public GebyrtypePUERelationFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.GebyrtypePUERelation;
            return modifiedObjects;
        }
    }


    public class GennemfoerelsesUddannelseselementFullLoadStrategy : BaseLoadStrategy
    {
        public GennemfoerelsesUddannelseselementFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.GennemfoerelsesUddannelseselement;
            return modifiedObjects;
        }
    }


    public class HoldFullLoadStrategy : BaseLoadStrategy
    {
        public HoldFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Hold;
            return modifiedObjects;
        }
    }


    public class HoldStudieforloebFullLoadStrategy : BaseLoadStrategy
    {
        public HoldStudieforloebFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.HoldStudieforloeb;
            return modifiedObjects;
        }
    }


    public class InstitutionVirksomhedFullLoadStrategy : BaseLoadStrategy
    {
        public InstitutionVirksomhedFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.InstitutionVirksomhed;
            return modifiedObjects;
        }
    }


    public class InstitutionsoplysningerFullLoadStrategy : BaseLoadStrategy
    {
        public InstitutionsoplysningerFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Institutionsoplysninger;
            return modifiedObjects;
        }
    }


    public class KarakterFullLoadStrategy : BaseLoadStrategy
    {
        public KarakterFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Karakter;
            return modifiedObjects;
        }
    }


    public class KommunikationFullLoadStrategy : BaseLoadStrategy
    {
        public KommunikationFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Kommunikation;
            return modifiedObjects;
        }
    }


    public class KurserSkoleopholdFullLoadStrategy : BaseLoadStrategy
    {
        public KurserSkoleopholdFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.KurserSkoleophold;
            return modifiedObjects;
        }
    }


    public class LandFullLoadStrategy : BaseLoadStrategy
    {
        public LandFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Land;
            return modifiedObjects;
        }
    }


    public class NationalAfgangsaarsagFullLoadStrategy : BaseLoadStrategy
    {
        public NationalAfgangsaarsagFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.NationalAfgangsaarsag;
            return modifiedObjects;
        }
    }


    public class OmraadenummeropsaetningFullLoadStrategy : BaseLoadStrategy
    {
        public OmraadenummeropsaetningFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Omraadenummeropsaetning;
            return modifiedObjects;
        }
    }


    public class PersonFullLoadStrategy : BaseLoadStrategy
    {
        public PersonFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Person;
            return modifiedObjects;
        }
    }


    public class PersonoplysningerFullLoadStrategy : BaseLoadStrategy
    {
        public PersonoplysningerFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Personoplysninger;
            return modifiedObjects;
        }
    }


    public class PlanlaegningsUddannelseselementFullLoadStrategy : BaseLoadStrategy
    {
        public PlanlaegningsUddannelseselementFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.PlanlaegningsUddannelseselement;
            return modifiedObjects;
        }
    }


    public class PostnummerFullLoadStrategy : BaseLoadStrategy
    {
        public PostnummerFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Postnummer;
            return modifiedObjects;
        }
    }


    public class PraktikomraadeFullLoadStrategy : BaseLoadStrategy
    {
        public PraktikomraadeFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Praktikomraade;
            return modifiedObjects;
        }
    }


    public class PraktikopholdFullLoadStrategy : BaseLoadStrategy
    {
        public PraktikopholdFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Praktikophold;
            return modifiedObjects;
        }
    }


    public class ProeveFullLoadStrategy : BaseLoadStrategy
    {
        public ProeveFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Proeve;
            return modifiedObjects;
        }
    }


    public class PubliceringFullLoadStrategy : BaseLoadStrategy
    {
        public PubliceringFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Publicering;
            return modifiedObjects;
        }
    }


    public class StruktureltUddannelseselementFullLoadStrategy : BaseLoadStrategy
    {
        public StruktureltUddannelseselementFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.StruktureltUddannelseselement;
            return modifiedObjects;
        }
    }


    public class StudieforloebFullLoadStrategy : BaseLoadStrategy
    {
        public StudieforloebFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Studieforloeb;
            return modifiedObjects;
        }
    }


    public class UddannelseFullLoadStrategy : BaseLoadStrategy
    {
        public UddannelseFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Uddannelse;
            return modifiedObjects;
        }
    }


    public class UddannelsesstrukturFullLoadStrategy : BaseLoadStrategy
    {
        public UddannelsesstrukturFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Uddannelsesstruktur;
            return modifiedObjects;
        }
    }


    public class UdlandsopholdAnsoegningFullLoadStrategy : BaseLoadStrategy
    {
        public UdlandsopholdAnsoegningFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.UdlandsopholdAnsoegning;
            return modifiedObjects;
        }
    }


    public class VideregaaendeUddannelseFullLoadStrategy : BaseLoadStrategy
    {
        public VideregaaendeUddannelseFullLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.VideregaaendeUddannelse;
            return modifiedObjects;
        }
    }



} // namespace end