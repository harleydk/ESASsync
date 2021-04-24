using Default; // Unchase OData connected service 'default' container name
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

/*
	Disse klasser er auto-genererede, på basis af deres korresponderende T4-template. 
	Hvis der skal rettes i klasserne, skal rettelserne foregå i template'n.
*/
namespace KP.Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    public class AdgangskravDeltaLoadStrategy : BaseLoadStrategy
    {
         public AdgangskravDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Adgangskrav.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AfdelingDeltaLoadStrategy : BaseLoadStrategy
    {
         public AfdelingDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Afdeling.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AndenAktivitetDeltaLoadStrategy : BaseLoadStrategy
    {
         public AndenAktivitetDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.AndenAktivitet.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegerDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegerDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoeger.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegningDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegning.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegningshandlingDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningshandlingDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningshandling.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegningskortDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningskort.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegningskortOpsaetningDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortOpsaetningDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.AnsoegningskortOpsaetning.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class AnsoegningsopsaetningDeltaLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningsopsaetningDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Ansoegningsopsaetning.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class BedoemmelseDeltaLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelseDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bedoemmelse.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class BedoemmelsesrundeDeltaLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelsesrundeDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bedoemmelsesrunde.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class BevisgrundlagDeltaLoadStrategy : BaseLoadStrategy
    {
         public BevisgrundlagDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bevisgrundlag.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class BilagDeltaLoadStrategy : BaseLoadStrategy
    {
         public BilagDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Bilag.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class BrancheDeltaLoadStrategy : BaseLoadStrategy
    {
         public BrancheDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Branche.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class EnkeltfagDeltaLoadStrategy : BaseLoadStrategy
    {
         public EnkeltfagDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Enkeltfag.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class ErfaringerDeltaLoadStrategy : BaseLoadStrategy
    {
         public ErfaringerDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Erfaringer.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class FagpersonsrelationDeltaLoadStrategy : BaseLoadStrategy
    {
         public FagpersonsrelationDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Fagpersonsrelation.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class GebyrtypePUERelationDeltaLoadStrategy : BaseLoadStrategy
    {
         public GebyrtypePUERelationDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.GebyrtypePUERelation.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class GennemfoerelsesUddannelseselementDeltaLoadStrategy : BaseLoadStrategy
    {
         public GennemfoerelsesUddannelseselementDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.GennemfoerelsesUddannelseselement.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class HoldDeltaLoadStrategy : BaseLoadStrategy
    {
         public HoldDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Hold.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class InstitutionVirksomhedDeltaLoadStrategy : BaseLoadStrategy
    {
         public InstitutionVirksomhedDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.InstitutionVirksomhed.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class InstitutionsoplysningerDeltaLoadStrategy : BaseLoadStrategy
    {
         public InstitutionsoplysningerDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Institutionsoplysninger.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class KarakterDeltaLoadStrategy : BaseLoadStrategy
    {
         public KarakterDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Karakter.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class KommunikationDeltaLoadStrategy : BaseLoadStrategy
    {
         public KommunikationDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Kommunikation.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class KurserSkoleopholdDeltaLoadStrategy : BaseLoadStrategy
    {
         public KurserSkoleopholdDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.KurserSkoleophold.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class LandDeltaLoadStrategy : BaseLoadStrategy
    {
         public LandDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Land.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class NationalAfgangsaarsagDeltaLoadStrategy : BaseLoadStrategy
    {
         public NationalAfgangsaarsagDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.NationalAfgangsaarsag.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class OmraadenummeropsaetningDeltaLoadStrategy : BaseLoadStrategy
    {
         public OmraadenummeropsaetningDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Omraadenummeropsaetning.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PersonDeltaLoadStrategy : BaseLoadStrategy
    {
         public PersonDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Person.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PersonoplysningerDeltaLoadStrategy : BaseLoadStrategy
    {
         public PersonoplysningerDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Personoplysninger.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PlanlaegningsUddannelseselementDeltaLoadStrategy : BaseLoadStrategy
    {
         public PlanlaegningsUddannelseselementDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.PlanlaegningsUddannelseselement.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PostnummerDeltaLoadStrategy : BaseLoadStrategy
    {
         public PostnummerDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Postnummer.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PraktikomraadeDeltaLoadStrategy : BaseLoadStrategy
    {
         public PraktikomraadeDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Praktikomraade.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PraktikopholdDeltaLoadStrategy : BaseLoadStrategy
    {
         public PraktikopholdDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Praktikophold.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class ProeveDeltaLoadStrategy : BaseLoadStrategy
    {
         public ProeveDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Proeve.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class PubliceringDeltaLoadStrategy : BaseLoadStrategy
    {
         public PubliceringDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Publicering.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class StruktureltUddannelseselementDeltaLoadStrategy : BaseLoadStrategy
    {
         public StruktureltUddannelseselementDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.StruktureltUddannelseselement.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class StudieforloebDeltaLoadStrategy : BaseLoadStrategy
    {
         public StudieforloebDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Studieforloeb.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class UddannelseDeltaLoadStrategy : BaseLoadStrategy
    {
         public UddannelseDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Uddannelse.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class UddannelsesstrukturDeltaLoadStrategy : BaseLoadStrategy
    {
         public UddannelsesstrukturDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.Uddannelsesstruktur.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class UdlandsopholdAnsoegningDeltaLoadStrategy : BaseLoadStrategy
    {
         public UdlandsopholdAnsoegningDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.UdlandsopholdAnsoegning.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class VideregaaendeUddannelseDeltaLoadStrategy : BaseLoadStrategy
    {
         public VideregaaendeUddannelseDeltaLoadStrategy(Container esasContainer, ILogger logger, ILoadResultsDestination loadResultsDestination) 
			: base(esasContainer, logger, loadResultsDestination)
        {
        }

        protected override IEnumerable<object> LoadObjects()
        {
            var modifiedObjects = _esasContainer.VideregaaendeUddannelse.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }



} // namespace end