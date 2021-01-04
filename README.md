# Torpedó Játék

Ez a projekt a Szoftverfejlesztés C# nyelven nagyvállalati környezetben nevű tárgyra készült.

## A játék leírása

A torpedó (battleship) egy kétszemélyes stratégiai táblajáték. A győzelemhez ki kell lőni az ellenfél összes hajóját.
Mindkét játékos előtt két darab tábla van, ahol az egyiken az adott player jelöli a lövéseit, a másikon a saját hajói vannak és az ellenfele lövéseit jelöli.

## A játékmenet

A játékosok egymásnak felváltva mondanak pozíciókat, (pl. D4) és mindketten kijelölik a megjelölt mezőt. Találatnak számít, ha eltaláltunk egy hajót és
süllyedésnek, ha minden részét eltaláltuk. Ha nem találunk el egy hajót, azt X-el jelöljük, ha pedig eltaláljuk, akkor +-al. (Ha egy hajó elsüllyedt, akkor azt kisatírozzuk)
A játéknak akkor van vége, ha valamelyik játékosnak ki lett lőve az összes hajója.

## Követelmények
- #### Egymás ellen lehessen játszani ugyanazon a gépen, ugyanabban az alkalmazásban
- #### AI ellen lehessen játszani
    - Kezdő játékos véletlenszerűen választva
    - AI elemezési sorrend
        1. Random találgat
        2. Ha van találat akkor már a mellette lévő mezőket lövi
            - UNIT tesztet írni a logikához (randomhoz nem muszáj)
- #### Játékmenet
    - Belépéskor kérjen nevet, majd ahhoz mentse az eredményeket
    - Eredményjelző
        - Körök száma
        - Saját találatok
        - Ellenfél találatai
        - Milyen hajók vannak még és melyek lettek elsüllyesztve
        - Billentyűkombinációra mutassa meg az AI hajóit (Csak AI ellen működjön)
- #### Játék vége
    - Tárolja le az eredményeket
        - Adatok tárolása: JSON, XML vagy adatbázis
    - Mindenkori eredménylista beolvasva tárolt adatokból (nyert - vesztett)

## Készítők

+ Oláh Sándor
+ Szegedi Marcell
+ Stiegelmayer Máté

## Használt eszközök

### Fejlesztőkörnyezet
+ Visual Studio - [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)

### Verziókövetés
+ Git - [https://github.com/](https://github.com/)

### Kommunikáció
+ Discord - [https://discord.com/](https://discord.com/)
+ Facebook/Messenger - [https://www.facebook.com/](https://www.facebook.com/)