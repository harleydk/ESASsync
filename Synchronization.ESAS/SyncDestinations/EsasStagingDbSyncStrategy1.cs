
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
		private ILogger _logger;

		public AdgangskravEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Adgangskrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Adgangskrav> nyeObjekter = new List<Adgangskrav>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Adgangskrav;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Adgangskrav freshObject = (Adgangskrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_adgangskravId == freshObject.esas_adgangskravId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Adgangskrav) with id {freshObject.esas_adgangskravId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Adgangskrav) {freshObject.esas_adgangskravId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Adgangskrav.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_adgangskravId.ToString()}'");
                            try
                            {
                                context.Adgangskrav.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_adgangskravId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_adgangskravId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_adgangskravId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Adgangskrav;
            var b = existingObject as Adgangskrav;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AfdelingEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Afdeling))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Afdeling> nyeObjekter = new List<Afdeling>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Afdeling;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Afdeling freshObject = (Afdeling) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_afdelingId == freshObject.esas_afdelingId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Afdeling) with id {freshObject.esas_afdelingId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Afdeling) {freshObject.esas_afdelingId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Afdeling.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_afdelingId.ToString()}'");
                            try
                            {
                                context.Afdeling.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_afdelingId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afdelingId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afdelingId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Afdeling;
            var b = existingObject as Afdeling;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingsniveauEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AfdelingsniveauEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Afdelingsniveau))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Afdelingsniveau> nyeObjekter = new List<Afdelingsniveau>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Afdelingsniveau;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Afdelingsniveau freshObject = (Afdelingsniveau) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_afdelingsniveauId == freshObject.esas_afdelingsniveauId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Afdelingsniveau) with id {freshObject.esas_afdelingsniveauId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Afdelingsniveau) {freshObject.esas_afdelingsniveauId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Afdelingsniveau.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_afdelingsniveauId.ToString()}'");
                            try
                            {
                                context.Afdelingsniveau.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_afdelingsniveauId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afdelingsniveauId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afdelingsniveauId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Afdelingsniveau;
            var b = existingObject as Afdelingsniveau;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfslagsbegrundelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AfslagsbegrundelseEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Afslagsbegrundelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Afslagsbegrundelse> nyeObjekter = new List<Afslagsbegrundelse>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Afslagsbegrundelse;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Afslagsbegrundelse freshObject = (Afslagsbegrundelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_afslagsbegrundelseId == freshObject.esas_afslagsbegrundelseId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Afslagsbegrundelse) with id {freshObject.esas_afslagsbegrundelseId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Afslagsbegrundelse) {freshObject.esas_afslagsbegrundelseId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Afslagsbegrundelse.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_afslagsbegrundelseId.ToString()}'");
                            try
                            {
                                context.Afslagsbegrundelse.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_afslagsbegrundelseId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afslagsbegrundelseId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_afslagsbegrundelseId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Afslagsbegrundelse;
            var b = existingObject as Afslagsbegrundelse;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AktivitetsudbudEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AktivitetsudbudEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Aktivitetsudbud))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Aktivitetsudbud> nyeObjekter = new List<Aktivitetsudbud>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Aktivitetsudbud;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Aktivitetsudbud freshObject = (Aktivitetsudbud) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_aktivitetsudbudId == freshObject.esas_aktivitetsudbudId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Aktivitetsudbud) with id {freshObject.esas_aktivitetsudbudId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Aktivitetsudbud) {freshObject.esas_aktivitetsudbudId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Aktivitetsudbud.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_aktivitetsudbudId.ToString()}'");
                            try
                            {
                                context.Aktivitetsudbud.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_aktivitetsudbudId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_aktivitetsudbudId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_aktivitetsudbudId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Aktivitetsudbud;
            var b = existingObject as Aktivitetsudbud;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AndenAktivitetEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AndenAktivitetEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AndenAktivitet))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<AndenAktivitet> nyeObjekter = new List<AndenAktivitet>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.AndenAktivitet;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					AndenAktivitet freshObject = (AndenAktivitet) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_andre_aktiviteterid == freshObject.esas_ansoegning_andre_aktiviteterid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type AndenAktivitet) with id {freshObject.esas_ansoegning_andre_aktiviteterid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type AndenAktivitet) {freshObject.esas_ansoegning_andre_aktiviteterid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.AndenAktivitet.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_andre_aktiviteterid.ToString()}'");
                            try
                            {
                                context.AndenAktivitet.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_andre_aktiviteterid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_andre_aktiviteterid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_andre_aktiviteterid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as AndenAktivitet;
            var b = existingObject as AndenAktivitet;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegerEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoeger))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Ansoeger> nyeObjekter = new List<Ansoeger>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Ansoeger;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Ansoeger freshObject = (Ansoeger) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.LeadId == freshObject.LeadId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Ansoeger) with id {freshObject.LeadId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Ansoeger) {freshObject.LeadId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Ansoeger.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].LeadId.ToString()}'");
                            try
                            {
                                context.Ansoeger.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.LeadId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.LeadId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.LeadId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoeger;
            var b = existingObject as Ansoeger;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Ansoegning> nyeObjekter = new List<Ansoegning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Ansoegning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Ansoegning freshObject = (Ansoegning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningId == freshObject.esas_ansoegningId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Ansoegning) with id {freshObject.esas_ansoegningId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Ansoegning) {freshObject.esas_ansoegningId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Ansoegning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningId.ToString()}'");
                            try
                            {
                                context.Ansoegning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegning;
            var b = existingObject as Ansoegning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningshandlingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningshandlingEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningshandling))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Ansoegningshandling> nyeObjekter = new List<Ansoegningshandling>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Ansoegningshandling;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Ansoegningshandling freshObject = (Ansoegningshandling) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningshandlingId == freshObject.esas_ansoegningshandlingId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Ansoegningshandling) with id {freshObject.esas_ansoegningshandlingId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Ansoegningshandling) {freshObject.esas_ansoegningshandlingId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Ansoegningshandling.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningshandlingId.ToString()}'");
                            try
                            {
                                context.Ansoegningshandling.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningshandlingId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningshandlingId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningshandlingId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningshandling;
            var b = existingObject as Ansoegningshandling;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningskortEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningskort))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Ansoegningskort> nyeObjekter = new List<Ansoegningskort>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Ansoegningskort;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Ansoegningskort freshObject = (Ansoegningskort) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskortid == freshObject.esas_ansoegningskortid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Ansoegningskort) with id {freshObject.esas_ansoegningskortid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Ansoegningskort) {freshObject.esas_ansoegningskortid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Ansoegningskort.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningskortid.ToString()}'");
                            try
                            {
                                context.Ansoegningskort.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningskortid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskortid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskortid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningskort;
            var b = existingObject as Ansoegningskort;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortOpsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningskortOpsaetningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AnsoegningskortOpsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<AnsoegningskortOpsaetning> nyeObjekter = new List<AnsoegningskortOpsaetning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.AnsoegningskortOpsaetning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					AnsoegningskortOpsaetning freshObject = (AnsoegningskortOpsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskortopsaetningid == freshObject.esas_ansoegningskortopsaetningid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type AnsoegningskortOpsaetning) with id {freshObject.esas_ansoegningskortopsaetningid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type AnsoegningskortOpsaetning) {freshObject.esas_ansoegningskortopsaetningid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.AnsoegningskortOpsaetning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningskortopsaetningid.ToString()}'");
                            try
                            {
                                context.AnsoegningskortOpsaetning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningskortopsaetningid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskortopsaetningid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskortopsaetningid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as AnsoegningskortOpsaetning;
            var b = existingObject as AnsoegningskortOpsaetning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortTekstEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningskortTekstEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(AnsoegningskortTekst))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<AnsoegningskortTekst> nyeObjekter = new List<AnsoegningskortTekst>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.AnsoegningskortTekst;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					AnsoegningskortTekst freshObject = (AnsoegningskortTekst) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningskorttekstid == freshObject.esas_ansoegningskorttekstid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type AnsoegningskortTekst) with id {freshObject.esas_ansoegningskorttekstid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type AnsoegningskortTekst) {freshObject.esas_ansoegningskorttekstid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.AnsoegningskortTekst.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningskorttekstid.ToString()}'");
                            try
                            {
                                context.AnsoegningskortTekst.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningskorttekstid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskorttekstid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningskorttekstid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as AnsoegningskortTekst;
            var b = existingObject as AnsoegningskortTekst;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningsopsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public AnsoegningsopsaetningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Ansoegningsopsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Ansoegningsopsaetning> nyeObjekter = new List<Ansoegningsopsaetning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Ansoegningsopsaetning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Ansoegningsopsaetning freshObject = (Ansoegningsopsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegningsopsaetningId == freshObject.esas_ansoegningsopsaetningId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Ansoegningsopsaetning) with id {freshObject.esas_ansoegningsopsaetningId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Ansoegningsopsaetning) {freshObject.esas_ansoegningsopsaetningId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Ansoegningsopsaetning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegningsopsaetningId.ToString()}'");
                            try
                            {
                                context.Ansoegningsopsaetning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegningsopsaetningId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningsopsaetningId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegningsopsaetningId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Ansoegningsopsaetning;
            var b = existingObject as Ansoegningsopsaetning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public BedoemmelseEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bedoemmelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Bedoemmelse> nyeObjekter = new List<Bedoemmelse>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Bedoemmelse;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Bedoemmelse freshObject = (Bedoemmelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bedoemmelseId == freshObject.esas_bedoemmelseId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Bedoemmelse) with id {freshObject.esas_bedoemmelseId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Bedoemmelse) {freshObject.esas_bedoemmelseId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Bedoemmelse.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_bedoemmelseId.ToString()}'");
                            try
                            {
                                context.Bedoemmelse.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_bedoemmelseId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bedoemmelseId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bedoemmelseId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Bedoemmelse;
            var b = existingObject as Bedoemmelse;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelsesrundeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public BedoemmelsesrundeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bedoemmelsesrunde))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Bedoemmelsesrunde> nyeObjekter = new List<Bedoemmelsesrunde>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Bedoemmelsesrunde;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Bedoemmelsesrunde freshObject = (Bedoemmelsesrunde) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bedoemmelsesrundeId == freshObject.esas_bedoemmelsesrundeId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Bedoemmelsesrunde) with id {freshObject.esas_bedoemmelsesrundeId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Bedoemmelsesrunde) {freshObject.esas_bedoemmelsesrundeId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Bedoemmelsesrunde.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_bedoemmelsesrundeId.ToString()}'");
                            try
                            {
                                context.Bedoemmelsesrunde.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_bedoemmelsesrundeId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bedoemmelsesrundeId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bedoemmelsesrundeId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Bedoemmelsesrunde;
            var b = existingObject as Bedoemmelsesrunde;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BilagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public BilagEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Bilag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Bilag> nyeObjekter = new List<Bilag>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Bilag;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Bilag freshObject = (Bilag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_bilagid == freshObject.esas_bilagid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Bilag) with id {freshObject.esas_bilagid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Bilag) {freshObject.esas_bilagid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Bilag.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_bilagid.ToString()}'");
                            try
                            {
                                context.Bilag.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_bilagid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bilagid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_bilagid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Bilag;
            var b = existingObject as Bilag;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BrancheEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public BrancheEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Branche))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Branche> nyeObjekter = new List<Branche>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Branche;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Branche freshObject = (Branche) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_brancheId == freshObject.esas_brancheId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Branche) with id {freshObject.esas_brancheId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Branche) {freshObject.esas_brancheId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Branche.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_brancheId.ToString()}'");
                            try
                            {
                                context.Branche.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_brancheId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_brancheId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_brancheId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Branche;
            var b = existingObject as Branche;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EksamenstypeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public EksamenstypeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Eksamenstype))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Eksamenstype> nyeObjekter = new List<Eksamenstype>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Eksamenstype;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Eksamenstype freshObject = (Eksamenstype) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_eksamenstypeId == freshObject.esas_eksamenstypeId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Eksamenstype) with id {freshObject.esas_eksamenstypeId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Eksamenstype) {freshObject.esas_eksamenstypeId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Eksamenstype.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_eksamenstypeId.ToString()}'");
                            try
                            {
                                context.Eksamenstype.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_eksamenstypeId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_eksamenstypeId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_eksamenstypeId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Eksamenstype;
            var b = existingObject as Eksamenstype;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EnkeltfagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public EnkeltfagEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Enkeltfag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Enkeltfag> nyeObjekter = new List<Enkeltfag>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Enkeltfag;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Enkeltfag freshObject = (Enkeltfag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_enkeltfagid == freshObject.esas_ansoegning_enkeltfagid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Enkeltfag) with id {freshObject.esas_ansoegning_enkeltfagid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Enkeltfag) {freshObject.esas_ansoegning_enkeltfagid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Enkeltfag.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_enkeltfagid.ToString()}'");
                            try
                            {
                                context.Enkeltfag.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_enkeltfagid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_enkeltfagid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_enkeltfagid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Enkeltfag;
            var b = existingObject as Enkeltfag;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ErfaringerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public ErfaringerEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Erfaringer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Erfaringer> nyeObjekter = new List<Erfaringer>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Erfaringer;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Erfaringer freshObject = (Erfaringer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_erfaringerid == freshObject.esas_ansoegning_erfaringerid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Erfaringer) with id {freshObject.esas_ansoegning_erfaringerid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Erfaringer) {freshObject.esas_ansoegning_erfaringerid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Erfaringer.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_erfaringerid.ToString()}'");
                            try
                            {
                                context.Erfaringer.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_erfaringerid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_erfaringerid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_erfaringerid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Erfaringer;
            var b = existingObject as Erfaringer;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FagpersonsrelationEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public FagpersonsrelationEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Fagpersonsrelation))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Fagpersonsrelation> nyeObjekter = new List<Fagpersonsrelation>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Fagpersonsrelation;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Fagpersonsrelation freshObject = (Fagpersonsrelation) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_fagpersonsrelationId == freshObject.esas_fagpersonsrelationId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Fagpersonsrelation) with id {freshObject.esas_fagpersonsrelationId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Fagpersonsrelation) {freshObject.esas_fagpersonsrelationId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Fagpersonsrelation.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_fagpersonsrelationId.ToString()}'");
                            try
                            {
                                context.Fagpersonsrelation.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_fagpersonsrelationId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_fagpersonsrelationId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_fagpersonsrelationId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Fagpersonsrelation;
            var b = existingObject as Fagpersonsrelation;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FravaersaarsagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public FravaersaarsagEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Fravaersaarsag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Fravaersaarsag> nyeObjekter = new List<Fravaersaarsag>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Fravaersaarsag;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Fravaersaarsag freshObject = (Fravaersaarsag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_fravaersaarsagId == freshObject.esas_fravaersaarsagId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Fravaersaarsag) with id {freshObject.esas_fravaersaarsagId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Fravaersaarsag) {freshObject.esas_fravaersaarsagId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Fravaersaarsag.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_fravaersaarsagId.ToString()}'");
                            try
                            {
                                context.Fravaersaarsag.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_fravaersaarsagId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_fravaersaarsagId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_fravaersaarsagId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Fravaersaarsag;
            var b = existingObject as Fravaersaarsag;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GebyrtypeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public GebyrtypeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Gebyrtype))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Gebyrtype> nyeObjekter = new List<Gebyrtype>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Gebyrtype;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Gebyrtype freshObject = (Gebyrtype) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gebyrtypeid == freshObject.esas_gebyrtypeid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Gebyrtype) with id {freshObject.esas_gebyrtypeid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Gebyrtype) {freshObject.esas_gebyrtypeid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Gebyrtype.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_gebyrtypeid.ToString()}'");
                            try
                            {
                                context.Gebyrtype.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_gebyrtypeid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gebyrtypeid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gebyrtypeid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Gebyrtype;
            var b = existingObject as Gebyrtype;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GennemfoerelsesUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public GennemfoerelsesUddannelseselementEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GennemfoerelsesUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<GennemfoerelsesUddannelseselement> nyeObjekter = new List<GennemfoerelsesUddannelseselement>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.GennemfoerelsesUddannelseselement;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					GennemfoerelsesUddannelseselement freshObject = (GennemfoerelsesUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselement_gennemfoerelseId == freshObject.esas_uddannelseselement_gennemfoerelseId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type GennemfoerelsesUddannelseselement) with id {freshObject.esas_uddannelseselement_gennemfoerelseId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type GennemfoerelsesUddannelseselement) {freshObject.esas_uddannelseselement_gennemfoerelseId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.GennemfoerelsesUddannelseselement.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_uddannelseselement_gennemfoerelseId.ToString()}'");
                            try
                            {
                                context.GennemfoerelsesUddannelseselement.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_uddannelseselement_gennemfoerelseId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselement_gennemfoerelseId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselement_gennemfoerelseId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as GennemfoerelsesUddannelseselement;
            var b = existingObject as GennemfoerelsesUddannelseselement;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleFagkravEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public GymnasielleFagkravEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GymnasielleFagkrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<GymnasielleFagkrav> nyeObjekter = new List<GymnasielleFagkrav>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.GymnasielleFagkrav;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					GymnasielleFagkrav freshObject = (GymnasielleFagkrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gymnasielle_fagkravId == freshObject.esas_gymnasielle_fagkravId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type GymnasielleFagkrav) with id {freshObject.esas_gymnasielle_fagkravId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type GymnasielleFagkrav) {freshObject.esas_gymnasielle_fagkravId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.GymnasielleFagkrav.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_gymnasielle_fagkravId.ToString()}'");
                            try
                            {
                                context.GymnasielleFagkrav.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_gymnasielle_fagkravId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gymnasielle_fagkravId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gymnasielle_fagkravId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as GymnasielleFagkrav;
            var b = existingObject as GymnasielleFagkrav;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleKarakterkravEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public GymnasielleKarakterkravEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(GymnasielleKarakterkrav))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<GymnasielleKarakterkrav> nyeObjekter = new List<GymnasielleKarakterkrav>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.GymnasielleKarakterkrav;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					GymnasielleKarakterkrav freshObject = (GymnasielleKarakterkrav) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_gymnasielle_karakterkravid == freshObject.esas_gymnasielle_karakterkravid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type GymnasielleKarakterkrav) with id {freshObject.esas_gymnasielle_karakterkravid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type GymnasielleKarakterkrav) {freshObject.esas_gymnasielle_karakterkravid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.GymnasielleKarakterkrav.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_gymnasielle_karakterkravid.ToString()}'");
                            try
                            {
                                context.GymnasielleKarakterkrav.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_gymnasielle_karakterkravid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gymnasielle_karakterkravid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_gymnasielle_karakterkravid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as GymnasielleKarakterkrav;
            var b = existingObject as GymnasielleKarakterkrav;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class HoldEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public HoldEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Hold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Hold> nyeObjekter = new List<Hold>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Hold;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Hold freshObject = (Hold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_holdId == freshObject.esas_holdId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Hold) with id {freshObject.esas_holdId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Hold) {freshObject.esas_holdId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Hold.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_holdId.ToString()}'");
                            try
                            {
                                context.Hold.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_holdId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_holdId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_holdId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Hold;
            var b = existingObject as Hold;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class IndskrivningsformEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public IndskrivningsformEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Indskrivningsform))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Indskrivningsform> nyeObjekter = new List<Indskrivningsform>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Indskrivningsform;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Indskrivningsform freshObject = (Indskrivningsform) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_indskrivningsformId == freshObject.esas_indskrivningsformId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Indskrivningsform) with id {freshObject.esas_indskrivningsformId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Indskrivningsform) {freshObject.esas_indskrivningsformId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Indskrivningsform.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_indskrivningsformId.ToString()}'");
                            try
                            {
                                context.Indskrivningsform.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_indskrivningsformId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_indskrivningsformId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_indskrivningsformId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Indskrivningsform;
            var b = existingObject as Indskrivningsform;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionVirksomhedEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public InstitutionVirksomhedEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(InstitutionVirksomhed))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<InstitutionVirksomhed> nyeObjekter = new List<InstitutionVirksomhed>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.InstitutionVirksomhed;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					InstitutionVirksomhed freshObject = (InstitutionVirksomhed) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.AccountId == freshObject.AccountId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type InstitutionVirksomhed) with id {freshObject.AccountId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type InstitutionVirksomhed) {freshObject.AccountId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.InstitutionVirksomhed.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].AccountId.ToString()}'");
                            try
                            {
                                context.InstitutionVirksomhed.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.AccountId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.AccountId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.AccountId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as InstitutionVirksomhed;
            var b = existingObject as InstitutionVirksomhed;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionsoplysningerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public InstitutionsoplysningerEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Institutionsoplysninger))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Institutionsoplysninger> nyeObjekter = new List<Institutionsoplysninger>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Institutionsoplysninger;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Institutionsoplysninger freshObject = (Institutionsoplysninger) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_institutionsoplysningerId == freshObject.esas_institutionsoplysningerId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Institutionsoplysninger) with id {freshObject.esas_institutionsoplysningerId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Institutionsoplysninger) {freshObject.esas_institutionsoplysningerId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Institutionsoplysninger.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_institutionsoplysningerId.ToString()}'");
                            try
                            {
                                context.Institutionsoplysninger.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_institutionsoplysningerId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_institutionsoplysningerId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_institutionsoplysningerId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Institutionsoplysninger;
            var b = existingObject as Institutionsoplysninger;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InternationaliseringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public InternationaliseringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Internationalisering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Internationalisering> nyeObjekter = new List<Internationalisering>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Internationalisering;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Internationalisering freshObject = (Internationalisering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_internationaliseringId == freshObject.esas_internationaliseringId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Internationalisering) with id {freshObject.esas_internationaliseringId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Internationalisering) {freshObject.esas_internationaliseringId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Internationalisering.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_internationaliseringId.ToString()}'");
                            try
                            {
                                context.Internationalisering.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_internationaliseringId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_internationaliseringId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_internationaliseringId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Internationalisering;
            var b = existingObject as Internationalisering;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KOTGruppeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KOTGruppe))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<KOTGruppe> nyeObjekter = new List<KOTGruppe>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.KOTGruppe;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					KOTGruppe freshObject = (KOTGruppe) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kot_gruppeid == freshObject.esas_kot_gruppeid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type KOTGruppe) with id {freshObject.esas_kot_gruppeid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type KOTGruppe) {freshObject.esas_kot_gruppeid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.KOTGruppe.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kot_gruppeid.ToString()}'");
                            try
                            {
                                context.KOTGruppe.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kot_gruppeid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kot_gruppeid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kot_gruppeid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as KOTGruppe;
            var b = existingObject as KOTGruppe;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeTilmeldingEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KOTGruppeTilmeldingEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KOTGruppeTilmelding))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<KOTGruppeTilmelding> nyeObjekter = new List<KOTGruppeTilmelding>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.KOTGruppeTilmelding;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					KOTGruppeTilmelding freshObject = (KOTGruppeTilmelding) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kot_gruppe_tilmeldingid == freshObject.esas_kot_gruppe_tilmeldingid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type KOTGruppeTilmelding) with id {freshObject.esas_kot_gruppe_tilmeldingid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type KOTGruppeTilmelding) {freshObject.esas_kot_gruppe_tilmeldingid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.KOTGruppeTilmelding.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kot_gruppe_tilmeldingid.ToString()}'");
                            try
                            {
                                context.KOTGruppeTilmelding.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kot_gruppe_tilmeldingid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kot_gruppe_tilmeldingid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kot_gruppe_tilmeldingid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as KOTGruppeTilmelding;
            var b = existingObject as KOTGruppeTilmelding;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KarakterEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KarakterEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Karakter))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Karakter> nyeObjekter = new List<Karakter>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Karakter;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Karakter freshObject = (Karakter) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_karakterId == freshObject.esas_karakterId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Karakter) with id {freshObject.esas_karakterId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Karakter) {freshObject.esas_karakterId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Karakter.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_karakterId.ToString()}'");
                            try
                            {
                                context.Karakter.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_karakterId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_karakterId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_karakterId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Karakter;
            var b = existingObject as Karakter;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommunikationEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KommunikationEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kommunikation))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Kommunikation> nyeObjekter = new List<Kommunikation>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Kommunikation;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Kommunikation freshObject = (Kommunikation) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kommunikationId == freshObject.esas_kommunikationId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Kommunikation) with id {freshObject.esas_kommunikationId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Kommunikation) {freshObject.esas_kommunikationId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Kommunikation.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kommunikationId.ToString()}'");
                            try
                            {
                                context.Kommunikation.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kommunikationId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kommunikationId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kommunikationId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Kommunikation;
            var b = existingObject as Kommunikation;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommuneEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KommuneEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kommune))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Kommune> nyeObjekter = new List<Kommune>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Kommune;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Kommune freshObject = (Kommune) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kommuneId == freshObject.esas_kommuneId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Kommune) with id {freshObject.esas_kommuneId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Kommune) {freshObject.esas_kommuneId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Kommune.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kommuneId.ToString()}'");
                            try
                            {
                                context.Kommune.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kommuneId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kommuneId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kommuneId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Kommune;
            var b = existingObject as Kommune;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KurserSkoleopholdEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KurserSkoleopholdEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(KurserSkoleophold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<KurserSkoleophold> nyeObjekter = new List<KurserSkoleophold>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.KurserSkoleophold;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					KurserSkoleophold freshObject = (KurserSkoleophold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_kurser_og_skoleopholdid == freshObject.esas_ansoegning_kurser_og_skoleopholdid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type KurserSkoleophold) with id {freshObject.esas_ansoegning_kurser_og_skoleopholdid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type KurserSkoleophold) {freshObject.esas_ansoegning_kurser_og_skoleopholdid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.KurserSkoleophold.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_kurser_og_skoleopholdid.ToString()}'");
                            try
                            {
                                context.KurserSkoleophold.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_kurser_og_skoleopholdid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_kurser_og_skoleopholdid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_kurser_og_skoleopholdid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as KurserSkoleophold;
            var b = existingObject as KurserSkoleophold;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationskriterieEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KvalifikationskriterieEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kvalifikationskriterie))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Kvalifikationskriterie> nyeObjekter = new List<Kvalifikationskriterie>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Kvalifikationskriterie;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Kvalifikationskriterie freshObject = (Kvalifikationskriterie) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationskriterieid == freshObject.esas_kvalifikationskriterieid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Kvalifikationskriterie) with id {freshObject.esas_kvalifikationskriterieid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Kvalifikationskriterie) {freshObject.esas_kvalifikationskriterieid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Kvalifikationskriterie.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kvalifikationskriterieid.ToString()}'");
                            try
                            {
                                context.Kvalifikationskriterie.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kvalifikationskriterieid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kvalifikationskriterieid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kvalifikationskriterieid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Kvalifikationskriterie;
            var b = existingObject as Kvalifikationskriterie;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationspointEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public KvalifikationspointEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Kvalifikationspoint))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Kvalifikationspoint> nyeObjekter = new List<Kvalifikationspoint>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Kvalifikationspoint;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Kvalifikationspoint freshObject = (Kvalifikationspoint) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_kvalifikationspointid == freshObject.esas_kvalifikationspointid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Kvalifikationspoint) with id {freshObject.esas_kvalifikationspointid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Kvalifikationspoint) {freshObject.esas_kvalifikationspointid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Kvalifikationspoint.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_kvalifikationspointid.ToString()}'");
                            try
                            {
                                context.Kvalifikationspoint.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_kvalifikationspointid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kvalifikationspointid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_kvalifikationspointid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Kvalifikationspoint;
            var b = existingObject as Kvalifikationspoint;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class LandEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public LandEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Land))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Land> nyeObjekter = new List<Land>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Land;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Land freshObject = (Land) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_landId == freshObject.esas_landId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Land) with id {freshObject.esas_landId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Land) {freshObject.esas_landId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Land.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_landId.ToString()}'");
                            try
                            {
                                context.Land.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_landId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_landId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_landId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Land;
            var b = existingObject as Land;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class MeritRegistreringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public MeritRegistreringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(MeritRegistrering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<MeritRegistrering> nyeObjekter = new List<MeritRegistrering>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.MeritRegistrering;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					MeritRegistrering freshObject = (MeritRegistrering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_meritregistreringId == freshObject.esas_meritregistreringId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type MeritRegistrering) with id {freshObject.esas_meritregistreringId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type MeritRegistrering) {freshObject.esas_meritregistreringId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.MeritRegistrering.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_meritregistreringId.ToString()}'");
                            try
                            {
                                context.MeritRegistrering.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_meritregistreringId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_meritregistreringId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_meritregistreringId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as MeritRegistrering;
            var b = existingObject as MeritRegistrering;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class NationalAfgangsaarsagEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public NationalAfgangsaarsagEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(NationalAfgangsaarsag))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<NationalAfgangsaarsag> nyeObjekter = new List<NationalAfgangsaarsag>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.NationalAfgangsaarsag;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					NationalAfgangsaarsag freshObject = (NationalAfgangsaarsag) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_national_afgangsaarsagId == freshObject.esas_national_afgangsaarsagId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type NationalAfgangsaarsag) with id {freshObject.esas_national_afgangsaarsagId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type NationalAfgangsaarsag) {freshObject.esas_national_afgangsaarsagId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.NationalAfgangsaarsag.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_national_afgangsaarsagId.ToString()}'");
                            try
                            {
                                context.NationalAfgangsaarsag.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_national_afgangsaarsagId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_national_afgangsaarsagId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_national_afgangsaarsagId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as NationalAfgangsaarsag;
            var b = existingObject as NationalAfgangsaarsag;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public OmraadenummerEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadenummer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Omraadenummer> nyeObjekter = new List<Omraadenummer>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Omraadenummer;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Omraadenummer freshObject = (Omraadenummer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadenummerId == freshObject.esas_omraadenummerId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Omraadenummer) with id {freshObject.esas_omraadenummerId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Omraadenummer) {freshObject.esas_omraadenummerId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Omraadenummer.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_omraadenummerId.ToString()}'");
                            try
                            {
                                context.Omraadenummer.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_omraadenummerId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadenummerId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadenummerId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadenummer;
            var b = existingObject as Omraadenummer;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummeropsaetningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public OmraadenummeropsaetningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadenummeropsaetning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Omraadenummeropsaetning> nyeObjekter = new List<Omraadenummeropsaetning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Omraadenummeropsaetning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Omraadenummeropsaetning freshObject = (Omraadenummeropsaetning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadeopsaetningid == freshObject.esas_omraadeopsaetningid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Omraadenummeropsaetning) with id {freshObject.esas_omraadeopsaetningid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Omraadenummeropsaetning) {freshObject.esas_omraadeopsaetningid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Omraadenummeropsaetning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_omraadeopsaetningid.ToString()}'");
                            try
                            {
                                context.Omraadenummeropsaetning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_omraadeopsaetningid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadeopsaetningid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadeopsaetningid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadenummeropsaetning;
            var b = existingObject as Omraadenummeropsaetning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadespecialiseringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public OmraadespecialiseringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Omraadespecialisering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Omraadespecialisering> nyeObjekter = new List<Omraadespecialisering>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Omraadespecialisering;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Omraadespecialisering freshObject = (Omraadespecialisering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_omraadespecialiseringid == freshObject.esas_omraadespecialiseringid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Omraadespecialisering) with id {freshObject.esas_omraadespecialiseringid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Omraadespecialisering) {freshObject.esas_omraadespecialiseringid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Omraadespecialisering.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_omraadespecialiseringid.ToString()}'");
                            try
                            {
                                context.Omraadespecialisering.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_omraadespecialiseringid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadespecialiseringid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_omraadespecialiseringid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Omraadespecialisering;
            var b = existingObject as Omraadespecialisering;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OptionSetValueStringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public OptionSetValueStringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(OptionSetValueString))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<OptionSetValueString> nyeObjekter = new List<OptionSetValueString>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.OptionSetValueString;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					OptionSetValueString freshObject = (OptionSetValueString) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.Id == freshObject.Id);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type OptionSetValueString) with id {freshObject.Id}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type OptionSetValueString) {freshObject.Id}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.OptionSetValueString.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].Id.ToString()}'");
                            try
                            {
                                context.OptionSetValueString.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.Id.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.Id.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.Id.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as OptionSetValueString;
            var b = existingObject as OptionSetValueString;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PersonEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Person))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Person> nyeObjekter = new List<Person>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Person;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Person freshObject = (Person) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.ContactId == freshObject.ContactId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Person) with id {freshObject.ContactId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Person) {freshObject.ContactId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Person.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].ContactId.ToString()}'");
                            try
                            {
                                context.Person.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.ContactId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.ContactId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.ContactId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Person;
            var b = existingObject as Person;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonoplysningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PersonoplysningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Personoplysning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Personoplysning> nyeObjekter = new List<Personoplysning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Personoplysning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Personoplysning freshObject = (Personoplysning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_personoplysningerId == freshObject.esas_personoplysningerId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Personoplysning) with id {freshObject.esas_personoplysningerId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Personoplysning) {freshObject.esas_personoplysningerId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Personoplysning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_personoplysningerId.ToString()}'");
                            try
                            {
                                context.Personoplysning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_personoplysningerId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_personoplysningerId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_personoplysningerId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Personoplysning;
            var b = existingObject as Personoplysning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PlanlaegningsUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PlanlaegningsUddannelseselementEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(PlanlaegningsUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<PlanlaegningsUddannelseselement> nyeObjekter = new List<PlanlaegningsUddannelseselement>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.PlanlaegningsUddannelseselement;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					PlanlaegningsUddannelseselement freshObject = (PlanlaegningsUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselement_planlaegningId == freshObject.esas_uddannelseselement_planlaegningId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type PlanlaegningsUddannelseselement) with id {freshObject.esas_uddannelseselement_planlaegningId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type PlanlaegningsUddannelseselement) {freshObject.esas_uddannelseselement_planlaegningId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.PlanlaegningsUddannelseselement.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_uddannelseselement_planlaegningId.ToString()}'");
                            try
                            {
                                context.PlanlaegningsUddannelseselement.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_uddannelseselement_planlaegningId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselement_planlaegningId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselement_planlaegningId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as PlanlaegningsUddannelseselement;
            var b = existingObject as PlanlaegningsUddannelseselement;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PostnummerEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PostnummerEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Postnummer))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Postnummer> nyeObjekter = new List<Postnummer>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Postnummer;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Postnummer freshObject = (Postnummer) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_postnummerId == freshObject.esas_postnummerId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Postnummer) with id {freshObject.esas_postnummerId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Postnummer) {freshObject.esas_postnummerId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Postnummer.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_postnummerId.ToString()}'");
                            try
                            {
                                context.Postnummer.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_postnummerId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_postnummerId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_postnummerId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Postnummer;
            var b = existingObject as Postnummer;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikomraadeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PraktikomraadeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Praktikomraade))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Praktikomraade> nyeObjekter = new List<Praktikomraade>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Praktikomraade;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Praktikomraade freshObject = (Praktikomraade) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_praktikomraadeId == freshObject.esas_praktikomraadeId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Praktikomraade) with id {freshObject.esas_praktikomraadeId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Praktikomraade) {freshObject.esas_praktikomraadeId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Praktikomraade.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_praktikomraadeId.ToString()}'");
                            try
                            {
                                context.Praktikomraade.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_praktikomraadeId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_praktikomraadeId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_praktikomraadeId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Praktikomraade;
            var b = existingObject as Praktikomraade;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikopholdEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PraktikopholdEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Praktikophold))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Praktikophold> nyeObjekter = new List<Praktikophold>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Praktikophold;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Praktikophold freshObject = (Praktikophold) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_praktikopholdId == freshObject.esas_praktikopholdId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Praktikophold) with id {freshObject.esas_praktikopholdId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Praktikophold) {freshObject.esas_praktikopholdId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Praktikophold.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_praktikopholdId.ToString()}'");
                            try
                            {
                                context.Praktikophold.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_praktikopholdId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_praktikopholdId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_praktikopholdId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Praktikophold;
            var b = existingObject as Praktikophold;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ProeveEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public ProeveEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Proeve))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Proeve> nyeObjekter = new List<Proeve>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Proeve;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Proeve freshObject = (Proeve) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_proeveid == freshObject.esas_ansoegning_proeveid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Proeve) with id {freshObject.esas_ansoegning_proeveid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Proeve) {freshObject.esas_ansoegning_proeveid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Proeve.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_proeveid.ToString()}'");
                            try
                            {
                                context.Proeve.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_proeveid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_proeveid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_proeveid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Proeve;
            var b = existingObject as Proeve;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PubliceringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public PubliceringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Publicering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Publicering> nyeObjekter = new List<Publicering>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Publicering;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Publicering freshObject = (Publicering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_publiceringid == freshObject.esas_publiceringid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Publicering) with id {freshObject.esas_publiceringid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Publicering) {freshObject.esas_publiceringid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Publicering.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_publiceringid.ToString()}'");
                            try
                            {
                                context.Publicering.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_publiceringid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_publiceringid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_publiceringid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Publicering;
            var b = existingObject as Publicering;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RelationsStatusEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public RelationsStatusEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(RelationsStatus))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<RelationsStatus> nyeObjekter = new List<RelationsStatus>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.RelationsStatus;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					RelationsStatus freshObject = (RelationsStatus) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_relations_statusId == freshObject.esas_relations_statusId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type RelationsStatus) with id {freshObject.esas_relations_statusId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type RelationsStatus) {freshObject.esas_relations_statusId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.RelationsStatus.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_relations_statusId.ToString()}'");
                            try
                            {
                                context.RelationsStatus.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_relations_statusId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_relations_statusId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_relations_statusId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as RelationsStatus;
            var b = existingObject as RelationsStatus;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RekvirenttypeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public RekvirenttypeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Rekvirenttype))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Rekvirenttype> nyeObjekter = new List<Rekvirenttype>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Rekvirenttype;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Rekvirenttype freshObject = (Rekvirenttype) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_rekvirenttypeId == freshObject.esas_rekvirenttypeId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Rekvirenttype) with id {freshObject.esas_rekvirenttypeId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Rekvirenttype) {freshObject.esas_rekvirenttypeId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Rekvirenttype.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_rekvirenttypeId.ToString()}'");
                            try
                            {
                                context.Rekvirenttype.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_rekvirenttypeId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_rekvirenttypeId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_rekvirenttypeId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Rekvirenttype;
            var b = existingObject as Rekvirenttype;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SamlaesningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public SamlaesningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Samlaesning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Samlaesning> nyeObjekter = new List<Samlaesning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Samlaesning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Samlaesning freshObject = (Samlaesning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_samlaesningId == freshObject.esas_samlaesningId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Samlaesning) with id {freshObject.esas_samlaesningId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Samlaesning) {freshObject.esas_samlaesningId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Samlaesning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_samlaesningId.ToString()}'");
                            try
                            {
                                context.Samlaesning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_samlaesningId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_samlaesningId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_samlaesningId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Samlaesning;
            var b = existingObject as Samlaesning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SpecialiseringEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public SpecialiseringEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Specialisering))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Specialisering> nyeObjekter = new List<Specialisering>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Specialisering;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Specialisering freshObject = (Specialisering) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_specialiseringid == freshObject.esas_ansoegning_specialiseringid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Specialisering) with id {freshObject.esas_ansoegning_specialiseringid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Specialisering) {freshObject.esas_ansoegning_specialiseringid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Specialisering.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_specialiseringid.ToString()}'");
                            try
                            {
                                context.Specialisering.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_specialiseringid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_specialiseringid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_specialiseringid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Specialisering;
            var b = existingObject as Specialisering;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StruktureltUddannelseselementEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public StruktureltUddannelseselementEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(StruktureltUddannelseselement))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<StruktureltUddannelseselement> nyeObjekter = new List<StruktureltUddannelseselement>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.StruktureltUddannelseselement;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					StruktureltUddannelseselement freshObject = (StruktureltUddannelseselement) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelseselementId == freshObject.esas_uddannelseselementId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type StruktureltUddannelseselement) with id {freshObject.esas_uddannelseselementId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type StruktureltUddannelseselement) {freshObject.esas_uddannelseselementId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.StruktureltUddannelseselement.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_uddannelseselementId.ToString()}'");
                            try
                            {
                                context.StruktureltUddannelseselement.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_uddannelseselementId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselementId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelseselementId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as StruktureltUddannelseselement;
            var b = existingObject as StruktureltUddannelseselement;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieforloebEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public StudieforloebEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Studieforloeb))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Studieforloeb> nyeObjekter = new List<Studieforloeb>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Studieforloeb;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Studieforloeb freshObject = (Studieforloeb) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_studieforloebId == freshObject.esas_studieforloebId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Studieforloeb) with id {freshObject.esas_studieforloebId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Studieforloeb) {freshObject.esas_studieforloebId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Studieforloeb.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_studieforloebId.ToString()}'");
                            try
                            {
                                context.Studieforloeb.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_studieforloebId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_studieforloebId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_studieforloebId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Studieforloeb;
            var b = existingObject as Studieforloeb;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieinaktivPeriodeEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public StudieinaktivPeriodeEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(StudieinaktivPeriode))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<StudieinaktivPeriode> nyeObjekter = new List<StudieinaktivPeriode>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.StudieinaktivPeriode;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					StudieinaktivPeriode freshObject = (StudieinaktivPeriode) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_studieinaktiv_periodeId == freshObject.esas_studieinaktiv_periodeId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type StudieinaktivPeriode) with id {freshObject.esas_studieinaktiv_periodeId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type StudieinaktivPeriode) {freshObject.esas_studieinaktiv_periodeId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.StudieinaktivPeriode.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_studieinaktiv_periodeId.ToString()}'");
                            try
                            {
                                context.StudieinaktivPeriode.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_studieinaktiv_periodeId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_studieinaktiv_periodeId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_studieinaktiv_periodeId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as StudieinaktivPeriode;
            var b = existingObject as StudieinaktivPeriode;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SystemUserEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public SystemUserEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(SystemUser))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<SystemUser> nyeObjekter = new List<SystemUser>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.SystemUser;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					SystemUser freshObject = (SystemUser) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.SystemUserId == freshObject.SystemUserId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type SystemUser) with id {freshObject.SystemUserId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type SystemUser) {freshObject.SystemUserId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.SystemUser.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].SystemUserId.ToString()}'");
                            try
                            {
                                context.SystemUser.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.SystemUserId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.SystemUserId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.SystemUserId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as SystemUser;
            var b = existingObject as SystemUser;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesaktivitetEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public UddannelsesaktivitetEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Uddannelsesaktivitet))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Uddannelsesaktivitet> nyeObjekter = new List<Uddannelsesaktivitet>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Uddannelsesaktivitet;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Uddannelsesaktivitet freshObject = (Uddannelsesaktivitet) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelsesaktivitetId == freshObject.esas_uddannelsesaktivitetId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Uddannelsesaktivitet) with id {freshObject.esas_uddannelsesaktivitetId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Uddannelsesaktivitet) {freshObject.esas_uddannelsesaktivitetId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Uddannelsesaktivitet.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_uddannelsesaktivitetId.ToString()}'");
                            try
                            {
                                context.Uddannelsesaktivitet.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_uddannelsesaktivitetId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelsesaktivitetId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelsesaktivitetId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Uddannelsesaktivitet;
            var b = existingObject as Uddannelsesaktivitet;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesstrukturEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public UddannelsesstrukturEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(Uddannelsesstruktur))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<Uddannelsesstruktur> nyeObjekter = new List<Uddannelsesstruktur>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.Uddannelsesstruktur;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					Uddannelsesstruktur freshObject = (Uddannelsesstruktur) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_uddannelsesstrukturId == freshObject.esas_uddannelsesstrukturId);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type Uddannelsesstruktur) with id {freshObject.esas_uddannelsesstrukturId}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type Uddannelsesstruktur) {freshObject.esas_uddannelsesstrukturId}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.Uddannelsesstruktur.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_uddannelsesstrukturId.ToString()}'");
                            try
                            {
                                context.Uddannelsesstruktur.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_uddannelsesstrukturId.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelsesstrukturId.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_uddannelsesstrukturId.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as Uddannelsesstruktur;
            var b = existingObject as Uddannelsesstruktur;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UdlandsopholdAnsoegningEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public UdlandsopholdAnsoegningEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(UdlandsopholdAnsoegning))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<UdlandsopholdAnsoegning> nyeObjekter = new List<UdlandsopholdAnsoegning>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.UdlandsopholdAnsoegning;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					UdlandsopholdAnsoegning freshObject = (UdlandsopholdAnsoegning) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_udlandsopholdid == freshObject.esas_ansoegning_udlandsopholdid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type UdlandsopholdAnsoegning) with id {freshObject.esas_ansoegning_udlandsopholdid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type UdlandsopholdAnsoegning) {freshObject.esas_ansoegning_udlandsopholdid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.UdlandsopholdAnsoegning.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_udlandsopholdid.ToString()}'");
                            try
                            {
                                context.UdlandsopholdAnsoegning.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_udlandsopholdid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_udlandsopholdid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_udlandsopholdid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as UdlandsopholdAnsoegning;
            var b = existingObject as UdlandsopholdAnsoegning;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }

	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class VideregaaendeUddannelseEsasStagingDbDestination : IEsasStagingDbDestination
    {
		private ComparisonConfig cc = new ComparisonConfig();
        private CompareLogic compareLogic;
		private ILogger _logger;

		public VideregaaendeUddannelseEsasStagingDbDestination(ILogger logger)
		{
			_logger = logger;
		}

        public bool IsStrategyMatch(Type type)
        {
            if (type == typeof(VideregaaendeUddannelse))
                return true;

            return false;
        }

        public void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory)
        {
			_logger.LogInformation("Klar til opdatering/tilføjelse af objekter.");
			System.Diagnostics.Debug.WriteLine("Delivering by " + this.GetType().Name);
			// initialize compare-logic
		    cc.CompareChildren = false;
            cc.IgnoreObjectTypes = true; // ignorer objekt-type - fordi objekter fra Entity Frameworket får en 'dynamic' type, som ikke er sammenlignelig med kp.domain-typen.
            compareLogic = new CompareLogic(cc);

			List<VideregaaendeUddannelse> nyeObjekter = new List<VideregaaendeUddannelse>();
            int antalÆndredeObjekter = 0;

			var dbContext = dbContextFactory.CreateDbContext();
			using (dbContext)
			{
	            var existingEntities = dbContext.VideregaaendeUddannelse;
				int recordNo = 0;
			
				for( int i=0; i < objects.Length; i++)
	            {
					recordNo++;
					VideregaaendeUddannelse freshObject = (VideregaaendeUddannelse) objects[i];
	
					var existingObject = existingEntities.SingleOrDefault(x => x.esas_ansoegning_videregaaende_uddannelseid == freshObject.esas_ansoegning_videregaaende_uddannelseid);
					if (existingObject == null)
	                {
                        // tilføj til listen over nye objekter, som vi bulk-insert'er nedenfor
                        string newObjectMessage = $"Sync-delivery: new object (of type VideregaaendeUddannelse) with id {freshObject.esas_ansoegning_videregaaende_uddannelseid}";
                        _logger.LogInformation(newObjectMessage);

	                    nyeObjekter.Add(freshObject);
	                }
	                else
	                {
	                    // update
                        ComparisonResult objectChangeResult = ObjectChangeResult(freshObject, existingObject);
                        bool objectHasChanged = !objectChangeResult.AreEqual;
                        if (objectHasChanged)
	                    {
                            string newObjectMessage = $"Sync-delivery: changed object (of type VideregaaendeUddannelse) {freshObject.esas_ansoegning_videregaaende_uddannelseid}. Changes to '{objectChangeResult.DifferencesString}";
                            _logger.LogInformation(newObjectMessage);
                            try
 
                            {
                                 antalÆndredeObjekter += 1;
						        dbContext.Entry(existingObject).State = EntityState.Modified; // remember to keep the 'existingObject' tracked by EF - or we'll get an error as we try to update the existing navigational properties (with a null-value, from the 'freshObject').
	                            dbContext.Entry(existingObject).CurrentValues.SetValues(freshObject);
                                dbContext.SaveChanges();
                            }
                            catch( Exception ex)
                            {
                             _logger.LogError(ex.Message, ex);
                             throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                            }
                           
	                    }
	                }
	
					// if ( recordNo % 1000 == 0)
                    // {
					//   System.Diagnostics.Debug.WriteLine(recordNo);
                    //   dbContext.SaveChanges();
                    // }
	            }

				// dbContext.SaveChanges();
            }

			if (nyeObjekter.Any())
            {
                _logger.LogInformation($"Fandt {nyeObjekter.Count()} til indsættelse.");
                try
                {
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                            context.VideregaaendeUddannelse.AddRange(nyeObjekter);
                            context.SaveChanges();
                    }
                }
                catch (Exception ex) when (ex.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                {
                    _logger.LogError("Der opstod en fejl i.f.m. tilføjelsen af samtlige nye objekter. Denne fejl skyldes sandsynligvis dét at en reference ikke kunne opfyldes, eksempelvis som ved indsættelse af en GUE der refererer til et ikke-eksisterende studieforløb. Vil i stedet gennemløbe hver enkelt record, og indsætte disse enkeltvis.");

                    _logger.LogInformation("Indsætter records enkelt-vis.");
                    using (var context = new EsasDbContextFactory().CreateDbContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false; // just adding, no need to track'em.
                        for (int i = 0; i < nyeObjekter.Count(); i++)
                        {
                            var nytObjekt = nyeObjekter[i];

                            _logger.LogInformation($"Indsætter objekt med id '{nyeObjekter[i].esas_ansoegning_videregaaende_uddannelseid.ToString()}'");
                            try
                            {
                                context.VideregaaendeUddannelse.Add(nytObjekt);
                                context.SaveChanges();
                                _logger.LogInformation($"Objekt med id '{nytObjekt.esas_ansoegning_videregaaende_uddannelseid.ToString()}' blev indsat.");
                            }
                            catch (Exception exe) when (exe.InnerException.Message.StartsWith("An error occurred while updating the entries. See the inner exception for details."))
                            {
                                string relationExceptionMessage = exe.InnerException.InnerException.Message;
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_videregaaende_uddannelseid.ToString()}' kunne ikke indsættes pga. dårlige db-relationer ('{relationExceptionMessage}'). Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                            catch (Exception excep)
                            {
                                _logger.LogError($"Objekt med id '{ nytObjekt.esas_ansoegning_videregaaende_uddannelseid.ToString()}' kunne ikke indsættes pga. '{ excep.Message }'. Springer videre til næste.");
                                //detach the entry, or EF will try and add it again. And again. 
                                context.Entry(nytObjekt).State = EntityState.Detached;
                                continue;
                            }
                        }
                    }

                    
                    _logger.LogError(ex.Message, ex);
                    throw ex; // kast den oprindelig exception for at forlade synkroniseringen. Ellers ville denne blive markeret som en succes, og timestamp'et vil blive mejslet i sten - og fortidige opdateringer vil blive skippet, dersom der er et nyere timestamp at tage i betragtning.
                }
            }

            _logger.LogInformation("Antal nye objekter: " + nyeObjekter.Count());
            _logger.LogInformation("Antal ændrede objekter: " + antalÆndredeObjekter);
		}

        private ComparisonResult ObjectChangeResult(object freshObject, object existingObject)
        {
            var a = freshObject as VideregaaendeUddannelse;
            var b = existingObject as VideregaaendeUddannelse;
            
            ComparisonResult resul = compareLogic.Compare(a, b);
            return resul;
        }
    }


} // namespace end