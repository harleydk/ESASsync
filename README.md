# ESASsync

Referenceimplementering af en .NET windows service der synkroniserer data fra det studieadministrative system ESAS til en lokal MSSQL db.

Synkroniseringen er ment at foregå kontinuérligt, hvorved aftager altid vil have et tidstro 'spejl' af ESAS-data. Denne kontinuitet opnås ved filtrering på datas seneste opdateringstidsstempel ('ModifiedOn'), hvorved belastningen på OData-servicen holdes på et absolut minimum.

[Københavns Professionshøjskole](https://kp.dk/) har lagt denne løsning ud som open source, med henblik på at vidensdele/inspirere. Al anvendelse af koden er på eget ansvar.

# Visual Studio løsningen består af 3 dele:

- Synchronization.Esas.Ws 

Danner ESAS-modeller på baggrund af OData-servicens meta-data.
Anvender Microsoft OData connected service - https://docs.microsoft.com/en-us/odata/connectedservice/getting-started

- Synchronization.Esas.DAL

Et code-first Entity-Framework data-access-layer, hvis entiteter vedligeholdes på baggrund af modellerne fra Synchronization.Esas.Ws projektet. Ved anvendelse af code-first migrations kan databasen vedligeholdes automatisk, når/hvis OData-servicens meta-data ændres. Denne EF-forbindelse anvendes ved skrivning af ESAS data til den bagvedliggende MSSQL db.

- Synchronization.Esas

En reference-implementering af en windows service, der på konfigurérbare frekvente tidspunkter - som eksempel angivet hvert 20. minut - henter nye og opdaterede data fra OData-servicen, på baggrund af disses 'ModifiedOn' angivne tidspunkt. Afhentede data sammenlignes med eksisterende; blot nye eller opdaterede objekter skrives til databasen.

# Sparring/brug for hjælp/pull requests/m.v.?

Send mig en PM.

