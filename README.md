# ESASsync

Referenceimplementering af en .NET windows service der synkroniserer data fra det studieadministrative system ESAS til en lokal MSSQL db.

[K�benhavns Professionsh�jskole](https://kp.dk/) har lagt denne l�sning ud som open source, med henblik p� at vidensdele/inspirere. Al anvendelse af koden er p� eget ansvar.

# Visual Studio l�sningen best�r af 3 dele:

- Synchronization.Esas.Ws 

Danner ESAS-modeller p� baggrund af OData-servicens meta-data.
Anvender Microsoft OData connected service - https://docs.microsoft.com/en-us/odata/connectedservice/getting-started

- Synchronization.Esas.DAL

Et code-first Entity-Framework data-access-layer, hvis entiteter vedligeholdes p� baggrund af modellerne fra Synchronization.Esas.Ws projektet. Ved anvendelse af code-first migrations kan databasen vedligeholdes automatisk, n�r/hvis OData-servicens meta-data �ndres. Denne EF-forbindelse anvendes ved skrivning af ESAS data til den bagvedliggende MSSQL db.

- Synchronization.Esas

En reference-implementering af en windows service, der p� konfigur�rbare frekvente tidspunkter - som eksempel angivet hvert 20. minut - henter nye og opdaterede data fra OData-servicen, p� baggrund af disses 'ModifiedOn' angivne tidspunkt. Afhentede data sammenlignes med eksisterende; blot nye eller opdaterede objekter skrives til databasen.

# Sparring/brug for hj�lp/pull requests/m.v.?

mnoe@kp.dk

