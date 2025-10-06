#Tradecenter 

##tradecenter## är em konsolbaserad C# applikation som låter användaren lägga upp items och ta emot bytesförslag, applikationen låter dig skapa konto, logga in, bläddra i andras föremål och skicka bytesföreslag, acceptera bytesförslag samt avslå byten.

## Funktioner

- Skapa konto
- Logga in / Logga ut
- Ladda upp ett föremål för byte
- Skicka bytesförfrågningar till andra användare
- Se inkommande bytesförfrågningar
- Acceptera eller neka bytesförfrågningar
- Se slutförda byten
- Sparar automatiskt historik på användare och färdiga byten

## Filstruktur / Klasser

- **User.cs** - Hanterar användarens inloggningsuppgifter och status
- **Item.cs** - Skapar och tar hand om item objekten
- **Trade.cs** - Hanterar nästan hela tradesystemet 
- **FileManager.cs** - hanterar all data som behöver sparas och laddas in
- **Program.cs** - huvudprogramet, Main menyn, användarinteraktion

## Installation & Körning

### 1. Klona repot

1. git clone https://github.com/ditt-användarnamn/tradecenter.git
2. cd tradecenter

### 2. Uppstart

1. Ta upp terminalen
2. cd /repos/Trade-system
3. skriv "dotnet run" i terminalen

### 3. Andvändning

1. skapa ett konto
2. lägg upp ett föremål på tradecenter
3. titta på andras föremål på tradecenter
4. gå till browse trade offers för att acceptera eller neka förfrågningar


## implementationsval

Jag har valt att använda mig av komposition då jag inte såg anledningen av att använda mig a inheritance(arv), Då det inte riktigt finns något i alla olika klasser som matchar varandra, ifall jag hade lagt till typ en admin user så hade det kunnat underlätta med arv men nu valde jag att göra det utan då jag inte tyckte det behövdes,

användt komposition, inheritance, osv.

## Förbättringar

- Jag hade nu i efterhand velat ha ett personligt inverntory för att kunna ge lite mer äkta känsla av ett byte
- lagt in fler saker i funktioner, då det blev lite ont om tid hann jag inte göra det