
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Synchronization.ESAS.DAL;
using Synchronization.ESAS.SyncDestinations;
using KellermanSoftware.CompareNetObjects;
using System.Data.Entity;
using esas.Dynamics.Models.Contracts;

/*
	Disse klasser er auto-genererede, på basis af deres korresponderende T4-template. 
	Hvis der skal rettes i klasserne, skal rettelserne foregå i template'n.
*/
namespace Synchronization.ESAS.Synchronizations.EsasStagingDbSyncStrategies
{
	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AdgangskravEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Adgangskrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Adgangskrav> nyeObjekter = new List<Adgangskrav>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Adgangskrav.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Adgangskrav freshObject = (Adgangskrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_adgangskravId == freshObject.esas_adgangskravId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Adgangskrav.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Adgangskrav;
            var b = existingObject as Adgangskrav;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Afdeling))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Afdeling> nyeObjekter = new List<Afdeling>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Afdeling.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Afdeling freshObject = (Afdeling) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_afdelingId == freshObject.esas_afdelingId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Afdeling.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Afdeling;
            var b = existingObject as Afdeling;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfslagsbegrundelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Afslagsbegrundelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Afslagsbegrundelse> nyeObjekter = new List<Afslagsbegrundelse>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Afslagsbegrundelse.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Afslagsbegrundelse freshObject = (Afslagsbegrundelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_afslagsbegrundelseId == freshObject.esas_afslagsbegrundelseId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Afslagsbegrundelse.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Afslagsbegrundelse;
            var b = existingObject as Afslagsbegrundelse;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AktivitetsudbudEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Aktivitetsudbud))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Aktivitetsudbud> nyeObjekter = new List<Aktivitetsudbud>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Aktivitetsudbud.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Aktivitetsudbud freshObject = (Aktivitetsudbud) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_aktivitetsudbudId == freshObject.esas_aktivitetsudbudId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Aktivitetsudbud.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Aktivitetsudbud;
            var b = existingObject as Aktivitetsudbud;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AndenAktivitetEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AndenAktivitet))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<AndenAktivitet> nyeObjekter = new List<AndenAktivitet>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.AndenAktivitet.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					AndenAktivitet freshObject = (AndenAktivitet) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_andre_aktiviteterid == freshObject.esas_ansoegning_andre_aktiviteterid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.AndenAktivitet.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as AndenAktivitet;
            var b = existingObject as AndenAktivitet;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoeger))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Ansoeger> nyeObjekter = new List<Ansoeger>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Ansoeger.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Ansoeger freshObject = (Ansoeger) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.LeadId == freshObject.LeadId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Ansoeger.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoeger;
            var b = existingObject as Ansoeger;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Ansoegning> nyeObjekter = new List<Ansoegning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Ansoegning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Ansoegning freshObject = (Ansoegning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningId == freshObject.esas_ansoegningId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Ansoegning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegning;
            var b = existingObject as Ansoegning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningPlanlaegningsUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AnsoegningPlanlaegningsUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<AnsoegningPlanlaegningsUddannelseselement> nyeObjekter = new List<AnsoegningPlanlaegningsUddannelseselement>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.AnsoegningPlanlaegningsUddannelseselement.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					AnsoegningPlanlaegningsUddannelseselement freshObject = (AnsoegningPlanlaegningsUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_esas_pueid == freshObject.esas_ansoegning_esas_pueid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.AnsoegningPlanlaegningsUddannelseselement.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as AnsoegningPlanlaegningsUddannelseselement;
            var b = existingObject as AnsoegningPlanlaegningsUddannelseselement;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningshandlingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningshandling))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Ansoegningshandling> nyeObjekter = new List<Ansoegningshandling>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Ansoegningshandling.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Ansoegningshandling freshObject = (Ansoegningshandling) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningshandlingId == freshObject.esas_ansoegningshandlingId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Ansoegningshandling.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningshandling;
            var b = existingObject as Ansoegningshandling;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningskort))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Ansoegningskort> nyeObjekter = new List<Ansoegningskort>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Ansoegningskort.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Ansoegningskort freshObject = (Ansoegningskort) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskortid == freshObject.esas_ansoegningskortid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Ansoegningskort.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningskort;
            var b = existingObject as Ansoegningskort;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortOpsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AnsoegningskortOpsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<AnsoegningskortOpsaetning> nyeObjekter = new List<AnsoegningskortOpsaetning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.AnsoegningskortOpsaetning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					AnsoegningskortOpsaetning freshObject = (AnsoegningskortOpsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskortopsaetningid == freshObject.esas_ansoegningskortopsaetningid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.AnsoegningskortOpsaetning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as AnsoegningskortOpsaetning;
            var b = existingObject as AnsoegningskortOpsaetning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortTekstEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AnsoegningskortTekst))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<AnsoegningskortTekst> nyeObjekter = new List<AnsoegningskortTekst>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.AnsoegningskortTekst.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					AnsoegningskortTekst freshObject = (AnsoegningskortTekst) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskorttekstid == freshObject.esas_ansoegningskorttekstid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.AnsoegningskortTekst.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as AnsoegningskortTekst;
            var b = existingObject as AnsoegningskortTekst;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningsopsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningsopsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Ansoegningsopsaetning> nyeObjekter = new List<Ansoegningsopsaetning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Ansoegningsopsaetning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Ansoegningsopsaetning freshObject = (Ansoegningsopsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningsopsaetningId == freshObject.esas_ansoegningsopsaetningId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Ansoegningsopsaetning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningsopsaetning;
            var b = existingObject as Ansoegningsopsaetning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bedoemmelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Bedoemmelse> nyeObjekter = new List<Bedoemmelse>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Bedoemmelse.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Bedoemmelse freshObject = (Bedoemmelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bedoemmelseId == freshObject.esas_bedoemmelseId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Bedoemmelse.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Bedoemmelse;
            var b = existingObject as Bedoemmelse;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelsesrundeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bedoemmelsesrunde))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Bedoemmelsesrunde> nyeObjekter = new List<Bedoemmelsesrunde>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Bedoemmelsesrunde.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Bedoemmelsesrunde freshObject = (Bedoemmelsesrunde) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bedoemmelsesrundeId == freshObject.esas_bedoemmelsesrundeId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Bedoemmelsesrunde.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Bedoemmelsesrunde;
            var b = existingObject as Bedoemmelsesrunde;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BevisgrundlagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bevisgrundlag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Bevisgrundlag> nyeObjekter = new List<Bevisgrundlag>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Bevisgrundlag.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Bevisgrundlag freshObject = (Bevisgrundlag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bevisgrundlagId == freshObject.esas_bevisgrundlagId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Bevisgrundlag.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Bevisgrundlag;
            var b = existingObject as Bevisgrundlag;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BilagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bilag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Bilag> nyeObjekter = new List<Bilag>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Bilag.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Bilag freshObject = (Bilag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bilagid == freshObject.esas_bilagid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Bilag.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Bilag;
            var b = existingObject as Bilag;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BrancheEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Branche))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Branche> nyeObjekter = new List<Branche>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Branche.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Branche freshObject = (Branche) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_brancheId == freshObject.esas_brancheId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Branche.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Branche;
            var b = existingObject as Branche;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EksamenstypeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Eksamenstype))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Eksamenstype> nyeObjekter = new List<Eksamenstype>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Eksamenstype.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Eksamenstype freshObject = (Eksamenstype) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_eksamenstypeId == freshObject.esas_eksamenstypeId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Eksamenstype.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Eksamenstype;
            var b = existingObject as Eksamenstype;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EnkeltfagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Enkeltfag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Enkeltfag> nyeObjekter = new List<Enkeltfag>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Enkeltfag.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Enkeltfag freshObject = (Enkeltfag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_enkeltfagid == freshObject.esas_ansoegning_enkeltfagid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Enkeltfag.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Enkeltfag;
            var b = existingObject as Enkeltfag;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ErfaringerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Erfaringer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Erfaringer> nyeObjekter = new List<Erfaringer>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Erfaringer.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Erfaringer freshObject = (Erfaringer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_erfaringerid == freshObject.esas_ansoegning_erfaringerid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Erfaringer.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Erfaringer;
            var b = existingObject as Erfaringer;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FagpersonsrelationEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Fagpersonsrelation))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Fagpersonsrelation> nyeObjekter = new List<Fagpersonsrelation>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Fagpersonsrelation.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Fagpersonsrelation freshObject = (Fagpersonsrelation) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_fagpersonsrelationId == freshObject.esas_fagpersonsrelationId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Fagpersonsrelation.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Fagpersonsrelation;
            var b = existingObject as Fagpersonsrelation;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FravaersaarsagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Fravaersaarsag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Fravaersaarsag> nyeObjekter = new List<Fravaersaarsag>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Fravaersaarsag.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Fravaersaarsag freshObject = (Fravaersaarsag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_fravaersaarsagId == freshObject.esas_fravaersaarsagId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Fravaersaarsag.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Fravaersaarsag;
            var b = existingObject as Fravaersaarsag;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GebyrtypeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Gebyrtype))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Gebyrtype> nyeObjekter = new List<Gebyrtype>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Gebyrtype.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Gebyrtype freshObject = (Gebyrtype) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gebyrtypeid == freshObject.esas_gebyrtypeid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Gebyrtype.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Gebyrtype;
            var b = existingObject as Gebyrtype;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GebyrtypePUERelationEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GebyrtypePUERelation))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<GebyrtypePUERelation> nyeObjekter = new List<GebyrtypePUERelation>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.GebyrtypePUERelation.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					GebyrtypePUERelation freshObject = (GebyrtypePUERelation) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gebyrtype_esas_uddannelseselement_plid == freshObject.esas_gebyrtype_esas_uddannelseselement_plid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.GebyrtypePUERelation.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as GebyrtypePUERelation;
            var b = existingObject as GebyrtypePUERelation;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GennemfoerelsesUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GennemfoerelsesUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<GennemfoerelsesUddannelseselement> nyeObjekter = new List<GennemfoerelsesUddannelseselement>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.GennemfoerelsesUddannelseselement.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					GennemfoerelsesUddannelseselement freshObject = (GennemfoerelsesUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselement_gennemfoerelseId == freshObject.esas_uddannelseselement_gennemfoerelseId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.GennemfoerelsesUddannelseselement.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as GennemfoerelsesUddannelseselement;
            var b = existingObject as GennemfoerelsesUddannelseselement;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleFagkravEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GymnasielleFagkrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<GymnasielleFagkrav> nyeObjekter = new List<GymnasielleFagkrav>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.GymnasielleFagkrav.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					GymnasielleFagkrav freshObject = (GymnasielleFagkrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gymnasielle_fagkravId == freshObject.esas_gymnasielle_fagkravId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.GymnasielleFagkrav.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as GymnasielleFagkrav;
            var b = existingObject as GymnasielleFagkrav;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleKarakterkravEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GymnasielleKarakterkrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<GymnasielleKarakterkrav> nyeObjekter = new List<GymnasielleKarakterkrav>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.GymnasielleKarakterkrav.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					GymnasielleKarakterkrav freshObject = (GymnasielleKarakterkrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gymnasielle_karakterkravid == freshObject.esas_gymnasielle_karakterkravid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.GymnasielleKarakterkrav.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as GymnasielleKarakterkrav;
            var b = existingObject as GymnasielleKarakterkrav;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class HoldEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Hold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Hold> nyeObjekter = new List<Hold>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Hold.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Hold freshObject = (Hold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_holdId == freshObject.esas_holdId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Hold.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Hold;
            var b = existingObject as Hold;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionVirksomhedEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(InstitutionVirksomhed))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<InstitutionVirksomhed> nyeObjekter = new List<InstitutionVirksomhed>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.InstitutionVirksomhed.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					InstitutionVirksomhed freshObject = (InstitutionVirksomhed) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.AccountId == freshObject.AccountId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.InstitutionVirksomhed.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as InstitutionVirksomhed;
            var b = existingObject as InstitutionVirksomhed;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionsoplysningerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Institutionsoplysninger))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Institutionsoplysninger> nyeObjekter = new List<Institutionsoplysninger>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Institutionsoplysninger.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Institutionsoplysninger freshObject = (Institutionsoplysninger) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_institutionsoplysningerId == freshObject.esas_institutionsoplysningerId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Institutionsoplysninger.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Institutionsoplysninger;
            var b = existingObject as Institutionsoplysninger;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InternationaliseringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Internationalisering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Internationalisering> nyeObjekter = new List<Internationalisering>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Internationalisering.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Internationalisering freshObject = (Internationalisering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_internationaliseringId == freshObject.esas_internationaliseringId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Internationalisering.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Internationalisering;
            var b = existingObject as Internationalisering;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KOTGruppe))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<KOTGruppe> nyeObjekter = new List<KOTGruppe>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.KOTGruppe.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					KOTGruppe freshObject = (KOTGruppe) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kot_gruppeid == freshObject.esas_kot_gruppeid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.KOTGruppe.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as KOTGruppe;
            var b = existingObject as KOTGruppe;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeTilmeldingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KOTGruppeTilmelding))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<KOTGruppeTilmelding> nyeObjekter = new List<KOTGruppeTilmelding>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.KOTGruppeTilmelding.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					KOTGruppeTilmelding freshObject = (KOTGruppeTilmelding) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kot_gruppe_tilmeldingid == freshObject.esas_kot_gruppe_tilmeldingid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.KOTGruppeTilmelding.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as KOTGruppeTilmelding;
            var b = existingObject as KOTGruppeTilmelding;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KarakterEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Karakter))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Karakter> nyeObjekter = new List<Karakter>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Karakter.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Karakter freshObject = (Karakter) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_karakterId == freshObject.esas_karakterId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Karakter.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Karakter;
            var b = existingObject as Karakter;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommunikationEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kommunikation))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Kommunikation> nyeObjekter = new List<Kommunikation>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Kommunikation.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Kommunikation freshObject = (Kommunikation) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kommunikationId == freshObject.esas_kommunikationId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Kommunikation.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Kommunikation;
            var b = existingObject as Kommunikation;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KurserSkoleopholdEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KurserSkoleophold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<KurserSkoleophold> nyeObjekter = new List<KurserSkoleophold>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.KurserSkoleophold.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					KurserSkoleophold freshObject = (KurserSkoleophold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_kurser_og_skoleopholdid == freshObject.esas_ansoegning_kurser_og_skoleopholdid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.KurserSkoleophold.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as KurserSkoleophold;
            var b = existingObject as KurserSkoleophold;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationskriterieEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kvalifikationskriterie))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Kvalifikationskriterie> nyeObjekter = new List<Kvalifikationskriterie>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Kvalifikationskriterie.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Kvalifikationskriterie freshObject = (Kvalifikationskriterie) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationskriterieid == freshObject.esas_kvalifikationskriterieid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Kvalifikationskriterie.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Kvalifikationskriterie;
            var b = existingObject as Kvalifikationskriterie;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationskriterieOmraadenummeropsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KvalifikationskriterieOmraadenummeropsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<KvalifikationskriterieOmraadenummeropsaetning> nyeObjekter = new List<KvalifikationskriterieOmraadenummeropsaetning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.KvalifikationskriterieOmraadenummeropsaetning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					KvalifikationskriterieOmraadenummeropsaetning freshObject = (KvalifikationskriterieOmraadenummeropsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationskriterier_for_omraadenumid == freshObject.esas_kvalifikationskriterier_for_omraadenumid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.KvalifikationskriterieOmraadenummeropsaetning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as KvalifikationskriterieOmraadenummeropsaetning;
            var b = existingObject as KvalifikationskriterieOmraadenummeropsaetning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationspointEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kvalifikationspoint))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Kvalifikationspoint> nyeObjekter = new List<Kvalifikationspoint>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Kvalifikationspoint.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Kvalifikationspoint freshObject = (Kvalifikationspoint) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationspointid == freshObject.esas_kvalifikationspointid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Kvalifikationspoint.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Kvalifikationspoint;
            var b = existingObject as Kvalifikationspoint;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationspointAnsoegningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KvalifikationspointAnsoegning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<KvalifikationspointAnsoegning> nyeObjekter = new List<KvalifikationspointAnsoegning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.KvalifikationspointAnsoegning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					KvalifikationspointAnsoegning freshObject = (KvalifikationspointAnsoegning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationspoint_esas_ansoegningid == freshObject.esas_kvalifikationspoint_esas_ansoegningid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.KvalifikationspointAnsoegning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as KvalifikationspointAnsoegning;
            var b = existingObject as KvalifikationspointAnsoegning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class LandEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Land))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Land> nyeObjekter = new List<Land>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Land.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Land freshObject = (Land) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_landId == freshObject.esas_landId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Land.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Land;
            var b = existingObject as Land;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class MeritRegistreringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(MeritRegistrering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<MeritRegistrering> nyeObjekter = new List<MeritRegistrering>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.MeritRegistrering.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					MeritRegistrering freshObject = (MeritRegistrering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_meritregistreringId == freshObject.esas_meritregistreringId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.MeritRegistrering.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as MeritRegistrering;
            var b = existingObject as MeritRegistrering;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class NationalAfgangsaarsagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(NationalAfgangsaarsag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<NationalAfgangsaarsag> nyeObjekter = new List<NationalAfgangsaarsag>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.NationalAfgangsaarsag.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					NationalAfgangsaarsag freshObject = (NationalAfgangsaarsag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_national_afgangsaarsagId == freshObject.esas_national_afgangsaarsagId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.NationalAfgangsaarsag.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as NationalAfgangsaarsag;
            var b = existingObject as NationalAfgangsaarsag;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadenummer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Omraadenummer> nyeObjekter = new List<Omraadenummer>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Omraadenummer.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Omraadenummer freshObject = (Omraadenummer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadenummerId == freshObject.esas_omraadenummerId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Omraadenummer.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadenummer;
            var b = existingObject as Omraadenummer;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummeropsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadenummeropsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Omraadenummeropsaetning> nyeObjekter = new List<Omraadenummeropsaetning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Omraadenummeropsaetning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Omraadenummeropsaetning freshObject = (Omraadenummeropsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadeopsaetningid == freshObject.esas_omraadeopsaetningid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Omraadenummeropsaetning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadenummeropsaetning;
            var b = existingObject as Omraadenummeropsaetning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadespecialiseringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadespecialisering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Omraadespecialisering> nyeObjekter = new List<Omraadespecialisering>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Omraadespecialisering.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Omraadespecialisering freshObject = (Omraadespecialisering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadespecialiseringid == freshObject.esas_omraadespecialiseringid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Omraadespecialisering.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadespecialisering;
            var b = existingObject as Omraadespecialisering;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OptionSetValueStringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(OptionSetValueString))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<OptionSetValueString> nyeObjekter = new List<OptionSetValueString>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.OptionSetValueString.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					OptionSetValueString freshObject = (OptionSetValueString) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.Id == freshObject.Id);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.OptionSetValueString.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as OptionSetValueString;
            var b = existingObject as OptionSetValueString;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Person))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Person> nyeObjekter = new List<Person>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Person.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Person freshObject = (Person) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.ContactId == freshObject.ContactId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Person.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Person;
            var b = existingObject as Person;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonoplysningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Personoplysning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Personoplysning> nyeObjekter = new List<Personoplysning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Personoplysning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Personoplysning freshObject = (Personoplysning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_personoplysningerId == freshObject.esas_personoplysningerId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Personoplysning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Personoplysning;
            var b = existingObject as Personoplysning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PlanlaegningsUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(PlanlaegningsUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<PlanlaegningsUddannelseselement> nyeObjekter = new List<PlanlaegningsUddannelseselement>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.PlanlaegningsUddannelseselement.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					PlanlaegningsUddannelseselement freshObject = (PlanlaegningsUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselement_planlaegningId == freshObject.esas_uddannelseselement_planlaegningId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.PlanlaegningsUddannelseselement.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as PlanlaegningsUddannelseselement;
            var b = existingObject as PlanlaegningsUddannelseselement;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PostnummerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Postnummer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Postnummer> nyeObjekter = new List<Postnummer>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Postnummer.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Postnummer freshObject = (Postnummer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_postnummerId == freshObject.esas_postnummerId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Postnummer.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Postnummer;
            var b = existingObject as Postnummer;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikomraadeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Praktikomraade))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Praktikomraade> nyeObjekter = new List<Praktikomraade>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Praktikomraade.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Praktikomraade freshObject = (Praktikomraade) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_praktikomraadeId == freshObject.esas_praktikomraadeId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Praktikomraade.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Praktikomraade;
            var b = existingObject as Praktikomraade;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikopholdEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Praktikophold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Praktikophold> nyeObjekter = new List<Praktikophold>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Praktikophold.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Praktikophold freshObject = (Praktikophold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_praktikopholdId == freshObject.esas_praktikopholdId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Praktikophold.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Praktikophold;
            var b = existingObject as Praktikophold;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ProeveEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Proeve))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Proeve> nyeObjekter = new List<Proeve>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Proeve.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Proeve freshObject = (Proeve) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_proeveid == freshObject.esas_ansoegning_proeveid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Proeve.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Proeve;
            var b = existingObject as Proeve;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PubliceringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Publicering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Publicering> nyeObjekter = new List<Publicering>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Publicering.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Publicering freshObject = (Publicering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_publiceringid == freshObject.esas_publiceringid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Publicering.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Publicering;
            var b = existingObject as Publicering;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RelationsStatusEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(RelationsStatus))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<RelationsStatus> nyeObjekter = new List<RelationsStatus>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.RelationsStatus.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					RelationsStatus freshObject = (RelationsStatus) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_relations_statusId == freshObject.esas_relations_statusId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.RelationsStatus.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as RelationsStatus;
            var b = existingObject as RelationsStatus;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StruktureltUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(StruktureltUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<StruktureltUddannelseselement> nyeObjekter = new List<StruktureltUddannelseselement>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.StruktureltUddannelseselement.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					StruktureltUddannelseselement freshObject = (StruktureltUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselementId == freshObject.esas_uddannelseselementId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.StruktureltUddannelseselement.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as StruktureltUddannelseselement;
            var b = existingObject as StruktureltUddannelseselement;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieforloebEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Studieforloeb))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Studieforloeb> nyeObjekter = new List<Studieforloeb>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Studieforloeb.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Studieforloeb freshObject = (Studieforloeb) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_studieforloebId == freshObject.esas_studieforloebId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Studieforloeb.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Studieforloeb;
            var b = existingObject as Studieforloeb;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieinaktivPeriodeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(StudieinaktivPeriode))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<StudieinaktivPeriode> nyeObjekter = new List<StudieinaktivPeriode>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.StudieinaktivPeriode.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					StudieinaktivPeriode freshObject = (StudieinaktivPeriode) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_studieinaktiv_periodeId == freshObject.esas_studieinaktiv_periodeId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.StudieinaktivPeriode.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as StudieinaktivPeriode;
            var b = existingObject as StudieinaktivPeriode;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SystemUserEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(SystemUser))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<SystemUser> nyeObjekter = new List<SystemUser>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.SystemUser.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					SystemUser freshObject = (SystemUser) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.SystemUserId == freshObject.SystemUserId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.SystemUser.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as SystemUser;
            var b = existingObject as SystemUser;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesaktivitetEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Uddannelsesaktivitet))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Uddannelsesaktivitet> nyeObjekter = new List<Uddannelsesaktivitet>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Uddannelsesaktivitet.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Uddannelsesaktivitet freshObject = (Uddannelsesaktivitet) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelsesaktivitetId == freshObject.esas_uddannelsesaktivitetId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Uddannelsesaktivitet.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Uddannelsesaktivitet;
            var b = existingObject as Uddannelsesaktivitet;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesstrukturEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Uddannelsesstruktur))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<Uddannelsesstruktur> nyeObjekter = new List<Uddannelsesstruktur>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.Uddannelsesstruktur.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					Uddannelsesstruktur freshObject = (Uddannelsesstruktur) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelsesstrukturId == freshObject.esas_uddannelsesstrukturId);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.Uddannelsesstruktur.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as Uddannelsesstruktur;
            var b = existingObject as Uddannelsesstruktur;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UdlandsopholdAnsoegningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(UdlandsopholdAnsoegning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<UdlandsopholdAnsoegning> nyeObjekter = new List<UdlandsopholdAnsoegning>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.UdlandsopholdAnsoegning.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					UdlandsopholdAnsoegning freshObject = (UdlandsopholdAnsoegning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_udlandsopholdid == freshObject.esas_ansoegning_udlandsopholdid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.UdlandsopholdAnsoegning.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as UdlandsopholdAnsoegning;
            var b = existingObject as UdlandsopholdAnsoegning;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class VideregaaendeUddannelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(VideregaaendeUddannelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            compareLogic = new CompareLogic(cc);

			List<VideregaaendeUddannelse> nyeObjekter = new List<VideregaaendeUddannelse>();

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{

	            var existingEntities = dbContext.VideregaaendeUddannelse.AsNoTracking();
				int numOfChanges = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					numOfChanges++;
					VideregaaendeUddannelse freshObject = (VideregaaendeUddannelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_videregaaende_uddannelseid == freshObject.esas_ansoegning_videregaaende_uddannelseid);
					if (existingObject == null)
	                {
	                    // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
	                    bool objectHasChanged = HasObjectChanged(freshObject, existingObject);
	                    if (objectHasChanged)
	                    {
						    dbContext.Entry(existingObject).State = EntityState.Modified;
	                        dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
	                    }
	                }
	
					if ( numOfChanges % 1000 == 0)
					  System.Diagnostics.Debug.WriteLine(numOfChanges);
	            }

				dbContext.SaveChanges();
            }

			// Indsæt evt. nye objekter, 1000 af gangen
            for (int i = 0; i < nyeObjekter.Count(); i = i + 1000)
            {
                var items = nyeObjekter.Skip(i).Take(1000);
                using (var foobar = new EsasDbContextFactory().CreateDbContext())
                {
                    foobar.VideregaaendeUddannelse.AddRange(items);
                    foobar.SaveChanges();
                }
            }

		}

        private bool HasObjectChanged(object freshObject, object existingObject)
        {
            var a = freshObject as VideregaaendeUddannelse;
            var b = existingObject as VideregaaendeUddannelse;

            var resul = compareLogic.Compare(a, b);
            return !resul.AreEqual;
        }
    }


} // namespace end