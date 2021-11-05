# Webapplikasjoner1

<h4>Gruppeeksamen nr. 1 - Bestilling av båtbilletter</h4>

<i>AnvendtLine</i> er et konsept om et cruiseskip som kjører opp og ned den sørlige delen av norgeskysten.
Her kan passasjerer velge fra hvor de vil reise, og hvor mange stopp de vil sitte på. 
For hvert stopp betaler passasjeren kun 49,-, og man kan få retur til samme pris.
Cruiseskipet går mellom 6 norske havnebyer og alle tilbyr på- og avstigning.

<h3>Trøbbel med å kjøre prosjektet?</h3>

Front end kjøres av React og krever node installert på datamaskinen. 
Her må package.json også installeres inne i `ClientApp`-mappen.

Naviger inn i `ClientApp`-mappen gjennom terminal og skriv `npm install`
Om man fortsatt får trøbbel med npm og imports i front end, slett package-lock.json og prøv på nytt.


<h4>Valg av validering</h4>
Validering er blitt gjort på en alternativ måte til det som har blitt gjennomgått. Dette er fordi vi hadde noen problemer med bruken av modelState og legge inn regex rett i Billett-klassen som vi ikke forsto årsaken til. Løsningen vi har valgt kan sees i Validering.cs.

<h4>Innlogging til Admin-side</h4>
For å logge inn som Admin på applikasjonen kan brukeren navigere til <i>Admin</i> i navigasjonsbaren.
Dersom man skal få tilgang til de ulike administrerende funksjonene krever dette innlogging.

Følgende brukernavn og passord vil gi tilgang:
Brukernavn: Adminbruker
Passord: Test12345

<h4>Kjøp av billetter</h4>
Når brukeren åpner brukergrensesnittet får den tildelt en unik kunde-id. Denne kunde-id'en er lagret i <i>state</i>, og vil derfor forsvinne ved <i>refresh</i>.
Alle billetter kjøpt mellom hver <i>refresh</i> vil vises under Dine reiser. Etter refresh vil fortsatt billettene være lagret i databasen, men være 
koblet til den unike kunde-id'en og ikke lenger vises under Dine reiser.

<h4>Oppretting av Avgang</h4>
For å opprette en Avgang i admin-grensesnittet må en strekning opprettes. 
Strekningen krever også at man oppretter to lokasjoner (Avgangslokasjon og Ankomstlokasjon), 
som strekingen skal gå mellom. Disse kan man opprette i sidene med samme navn.

<h4>Sletting av Strekning</h4>
For å slette en Strekning så må også alle avgangene som bruker strekningen være slettet på forhånd. 
For eksempel om man skal slette Oslo-Bergen, så må alle avganger som bruker denne strekningen slettes 
før dette kan skje. Det samme gjelder også Lokasjon -> Strekning, hvor man må slette strekningen før man sletter lokasjonen.

<h4>Studentnummer</h4>
<ul>
  <li>s344230</li>
  <li>s344223</li>
  <li>s346201</li>
  <li>s344245</li>
  <li>s344207</li>
</ul>
