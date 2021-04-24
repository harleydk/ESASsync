using Default; // Unchase OData connected service 'default' container name
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

/*
	Disse klasser er auto-genererede, på basis af deres korresponderende T4-template. 
	Hvis der skal rettes i klasserne, skal rettelserne foregå i template'n!
*/
namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AdgangskravLoadStrategy : BaseLoadStrategy
    {
         public AdgangskravLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Adgangskrav.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingLoadStrategy : BaseLoadStrategy
    {
         public AfdelingLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Afdeling.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfslagsbegrundelseLoadStrategy : BaseLoadStrategy
    {
         public AfslagsbegrundelseLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Afslagsbegrundelse.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AktivitetsudbudLoadStrategy : BaseLoadStrategy
    {
         public AktivitetsudbudLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Aktivitetsudbud.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AndenAktivitetLoadStrategy : BaseLoadStrategy
    {
         public AndenAktivitetLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.AndenAktivitet.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegerLoadStrategy : BaseLoadStrategy
    {
         public AnsoegerLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Ansoeger.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Ansoegning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningshandlingLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningshandlingLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Ansoegningshandling.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Ansoegningskort.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortOpsaetningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortOpsaetningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.AnsoegningskortOpsaetning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortTekstLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortTekstLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.AnsoegningskortTekst.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningsopsaetningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningsopsaetningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Ansoegningsopsaetning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelseLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelseLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Bedoemmelse.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelsesrundeLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelsesrundeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Bedoemmelsesrunde.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BevisgrundlagLoadStrategy : BaseLoadStrategy
    {
         public BevisgrundlagLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Bevisgrundlag.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BilagLoadStrategy : BaseLoadStrategy
    {
         public BilagLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Bilag.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BrancheLoadStrategy : BaseLoadStrategy
    {
         public BrancheLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Branche.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EksamenstypeLoadStrategy : BaseLoadStrategy
    {
         public EksamenstypeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Eksamenstype.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EnkeltfagLoadStrategy : BaseLoadStrategy
    {
         public EnkeltfagLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Enkeltfag.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ErfaringerLoadStrategy : BaseLoadStrategy
    {
         public ErfaringerLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Erfaringer.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FagpersonsrelationLoadStrategy : BaseLoadStrategy
    {
         public FagpersonsrelationLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Fagpersonsrelation.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FravaersaarsagLoadStrategy : BaseLoadStrategy
    {
         public FravaersaarsagLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Fravaersaarsag.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GebyrtypeLoadStrategy : BaseLoadStrategy
    {
         public GebyrtypeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Gebyrtype.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GennemfoerelsesUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public GennemfoerelsesUddannelseselementLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.GennemfoerelsesUddannelseselement.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleFagkravLoadStrategy : BaseLoadStrategy
    {
         public GymnasielleFagkravLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.GymnasielleFagkrav.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleKarakterkravLoadStrategy : BaseLoadStrategy
    {
         public GymnasielleKarakterkravLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.GymnasielleKarakterkrav.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class HoldLoadStrategy : BaseLoadStrategy
    {
         public HoldLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Hold.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionVirksomhedLoadStrategy : BaseLoadStrategy
    {
         public InstitutionVirksomhedLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.InstitutionVirksomhed.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionsoplysningerLoadStrategy : BaseLoadStrategy
    {
         public InstitutionsoplysningerLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Institutionsoplysninger.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InternationaliseringLoadStrategy : BaseLoadStrategy
    {
         public InternationaliseringLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Internationalisering.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeLoadStrategy : BaseLoadStrategy
    {
         public KOTGruppeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.KOTGruppe.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeTilmeldingLoadStrategy : BaseLoadStrategy
    {
         public KOTGruppeTilmeldingLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.KOTGruppeTilmelding.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KarakterLoadStrategy : BaseLoadStrategy
    {
         public KarakterLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Karakter.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommunikationLoadStrategy : BaseLoadStrategy
    {
         public KommunikationLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Kommunikation.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KurserSkoleopholdLoadStrategy : BaseLoadStrategy
    {
         public KurserSkoleopholdLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.KurserSkoleophold.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationskriterieLoadStrategy : BaseLoadStrategy
    {
         public KvalifikationskriterieLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Kvalifikationskriterie.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationspointLoadStrategy : BaseLoadStrategy
    {
         public KvalifikationspointLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Kvalifikationspoint.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class LandLoadStrategy : BaseLoadStrategy
    {
         public LandLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Land.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class MeritRegistreringLoadStrategy : BaseLoadStrategy
    {
         public MeritRegistreringLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.MeritRegistrering.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class NationalAfgangsaarsagLoadStrategy : BaseLoadStrategy
    {
         public NationalAfgangsaarsagLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.NationalAfgangsaarsag.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummerLoadStrategy : BaseLoadStrategy
    {
         public OmraadenummerLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Omraadenummer.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummeropsaetningLoadStrategy : BaseLoadStrategy
    {
         public OmraadenummeropsaetningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Omraadenummeropsaetning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadespecialiseringLoadStrategy : BaseLoadStrategy
    {
         public OmraadespecialiseringLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Omraadespecialisering.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonLoadStrategy : BaseLoadStrategy
    {
         public PersonLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Person.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonoplysningLoadStrategy : BaseLoadStrategy
    {
         public PersonoplysningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Personoplysning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PlanlaegningsUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public PlanlaegningsUddannelseselementLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.PlanlaegningsUddannelseselement.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PostnummerLoadStrategy : BaseLoadStrategy
    {
         public PostnummerLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Postnummer.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikomraadeLoadStrategy : BaseLoadStrategy
    {
         public PraktikomraadeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Praktikomraade.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikopholdLoadStrategy : BaseLoadStrategy
    {
         public PraktikopholdLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Praktikophold.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ProeveLoadStrategy : BaseLoadStrategy
    {
         public ProeveLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Proeve.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PubliceringLoadStrategy : BaseLoadStrategy
    {
         public PubliceringLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Publicering.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RelationsStatusLoadStrategy : BaseLoadStrategy
    {
         public RelationsStatusLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.RelationsStatus.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StruktureltUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public StruktureltUddannelseselementLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.StruktureltUddannelseselement.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieforloebLoadStrategy : BaseLoadStrategy
    {
         public StudieforloebLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Studieforloeb.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieinaktivPeriodeLoadStrategy : BaseLoadStrategy
    {
         public StudieinaktivPeriodeLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.StudieinaktivPeriode.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SystemUserLoadStrategy : BaseLoadStrategy
    {
         public SystemUserLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.SystemUser.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesaktivitetLoadStrategy : BaseLoadStrategy
    {
         public UddannelsesaktivitetLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Uddannelsesaktivitet.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesstrukturLoadStrategy : BaseLoadStrategy
    {
         public UddannelsesstrukturLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.Uddannelsesstruktur.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UdlandsopholdAnsoegningLoadStrategy : BaseLoadStrategy
    {
         public UdlandsopholdAnsoegningLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.UdlandsopholdAnsoegning.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class VideregaaendeUddannelseLoadStrategy : BaseLoadStrategy
    {
         public VideregaaendeUddannelseLoadStrategy(Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasContainer, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            var modifiedObjects = _esasContainer.VideregaaendeUddannelse.Where(t => t.ModifiedOn >= loadTimeCutoff).Skip(indexToStartLoadFrom).Take(howManyRecordsToGet).ToArray();
            return modifiedObjects;
        }
    }



} // namespace end