# MiniBank – uporabniške zgodbe

Dokument vsebuje 15 zgodb za funkcionalnosti F01–F15 in tri dodatne za uslužbenca, administratorja ter prijavo. Vsaka zgodba ima pet  pogojev sprejema. Zgodbe sledijo načelu INVEST: imajo jasen cilj, korist za uporabnika in omejen obseg, ki ga je mogoče oceniti in preizkusiti.

MVP zajema zgodbe US-01–US-07 in US-16–US-18. Ostale so kasnejše nadgradnje.
## US-01 – Pregled računov

Funkcionalnost: F01; vloga: komitent; obseg: MVP.

Kot komitent želim videti svoje račune in nakazila, da vem, koliko denarja imam na voljo.

Pogoji sprejema:

1. Uporabnik vidi samo svoje račune, njihove oznake in stanja v EUR. Če nima računa, se prikaže ustrezno sporočilo.
2. Za izbrani račun se prikaže zadnjih 20 izvedenih nakazil z zneskom, smerjo, namenom in datumom.
3. Pri oddanih nakazilih uporabnik vidi tudi čakanje, zavrnitev ali neuspeh ter razlog za tak izid.
4. Po uspešnem nakazilu 30 EUR z računa z 200 EUR je novo stanje 170 EUR. Čakajoče nakazilo stanja ne spremeni.
5. Skupinski dolgovi so prikazani ločeno od bančnega stanja. Uporabnik ne more odpreti računov drugih komitentov.

## US-02 – Interno nakazilo

Funkcionalnost: F02; vloga: komitent; obseg: MVP.

Kot komitent želim poslati denar na drug račun MiniBanke, da poravnam plačilo prejemniku.

Pogoji sprejema:

1. Uporabnik izbere svoj račun, vnese prejemnikov račun, pozitiven znesek z največ dvema decimalkama in namen.
2. Nakazilo 500 EUR se izvede takoj, če so izpolnjeni pogoji. Nakazilo 500,01 EUR čaka na odobritev in še ne spremeni stanj.
3. Neobstoječ prejemnik, nakazilo na isti račun ali premalo sredstev povzročijo napako brez prenosa denarja.
4. Ponovitev iste zahteve ne izvede drugega nakazila. Sprememba zneska ob uporabi iste oznake zahteve se zavrne.
5. Pri dveh sočasnih nakazilih po 80 EUR z računa s 100 EUR se izvede največ eno. Stanje ne postane negativno.

## US-03 – Skupine in povabila

Funkcionalnost: F03; vloga: komitent oziroma organizator skupine; obseg: MVP.

Kot komitent želim ustvariti skupino in povabiti prijatelje, da skupne stroške spremljamo na enem mestu.

Pogoji sprejema:

1. Uporabnik ustvari skupino z nepraznim imenom in postane njen organizator.
2. Organizator lahko po e-pošti povabi obstoječega aktivnega komitenta. Podvojeno povabilo ne ustvari dodatnega članstva.
3. Povabljeni vidi ime skupine in organizatorja. Šele po sprejemu povabila lahko vidi skupinske stroške in zgodovino.
4. Povabilo lahko sprejme ali zavrne samo povabljeni. Sprejem ga ne doda samodejno k že oddanim stroškom.
5. Navaden član ne more vabiti novih uporabnikov. Nečlan ne vidi skupine, organizator pa ne vidi osebnih bančnih stanj članov.

## US-04 – Delitev stroška

Funkcionalnost: F04; vloga: član skupine, ki je plačal strošek; obseg: MVP.

Kot plačnik skupnega stroška želim določiti deleže udeležencev, da vsak dobi jasen predlog povračila.

Pogoji sprejema:

1. Plačnik vnese naziv, datum, kategorijo, znesek in svoj račun za povračila. Datum plačila ne sme biti v prihodnosti.
2. Izbere aktivne člane skupine, vključno s seboj. Vsaj en drug član mora imeti pozitiven delež.
3. Enakomerna delitev 10 EUR med tri člane da 3,34, 3,33 in 3,33 EUR. Dodatni cent pripada prvemu po določenem vrstnem redu.
4. Pri ročni delitvi mora biti vsota deležev enaka strošku. Za strošek 90 EUR se vsota 89,99 EUR ali negativen delež zavrne.
5. Osnutek lahko vidi in ureja samo avtor. Po oddaji vsebine ni mogoče spreminjati; oddaja sama še ne ustvari dolga ali nakazila.

## US-05 – Potrditev deleža

Funkcionalnost: F05; vloga: član, vključen v strošek; obseg: MVP.

Kot član skupine želim potrditi svoj delež ali mu ugovarjati, da dolg nastane po dogovoru vseh udeležencev.

Pogoji sprejema:

1. Član vidi plačnika, znesek, številko predloga in vse deleže. Odloči lahko samo o svojem deležu.
2. Plačnik potrdi svoj delež ob oddaji. Pri večerji za 90 EUR dolgova Bora in Ceneta do Ane po 30 EUR nastaneta šele po potrditvi vseh.
3. Ugovor zahteva razlog in ustavi potrjevanje predloga. Iz spornega predloga ne nastanejo dolgovi.
4. Avtor lahko pripravi nov predlog. Stari odgovori ostanejo v zgodovini, udeleženci pa morajo novo različico ponovno potrditi.
5. Ponovljene ali sočasne potrditve ne ustvarijo podvojenih dolgov. Potrjenega stroška ni mogoče naknadno spreminjati.

## US-06 – Pregled obveznosti

Funkcionalnost: F06; vloga: član skupine; obseg: MVP.

Kot član skupine želim videti svoje dolgove in terjatve, da vem, komu moram plačati in od koga pričakujem povračilo.

Pogoji sprejema:

1. Vsaka obveznost pokaže dolžnika, prejemnika, strošek, znesek in stanje plačila. Nepotrjeni predlogi niso vključeni v dolg.
2. Po večerji za 90 EUR Ana pričakuje 60 EUR, Bor in Cene pa dolgujeta vsak po 30 EUR.
3. Po Borovem uspešnem plačilu Ana pričakuje še 30 EUR. Če plačilo samo čaka na odobritev, njen pričakovani znesek ostane 60 EUR.
4. Uporabnik lahko loči svoje dolgove, terjatve in poravnane postavke. Prazen seznam pokaže sporočilo, da za izbrani filter ni obveznosti.
5. Pregled ne razkrije bančnih stanj ali razlogov zavrnitve plačil drugih članov in ne izvaja samodejnih pobotov.

## US-07 – Poravnava obveznosti

Funkcionalnost: F07; vloga: dolžnik; obseg: MVP.

Kot dolžnik želim plačati potrjeno obveznost s svojega računa, da povrnem strošek in poravnam dolg.

Pogoji sprejema:

1. Plačilo lahko sproži samo dolžnik. Izbere svoj račun, prejemnika in zneska potrjene obveznosti pa ne more spremeniti.
2. Plačilo 30 EUR zmanjša stanje dolžnika z 200 na 170 EUR, poveča stanje prejemnika za 30 EUR in označi dolg kot poravnan.
3. Plačilo 650 EUR čaka na odobritev. Dolg do izvedbe ostane odprt, novo plačilo iste obveznosti pa je blokirano.
4. Premalo sredstev, zavrnitev ali neuspešna izvedba ne zmanjšajo dolga. Po zavrnjenem ali neuspešnem poskusu je dovoljen nov poskus.
5. Ponovljene in sočasne zahteve ne morejo povzročiti dvojnega plačila iste obveznosti.

## US-08 – Poenostavitev poravnav

Funkcionalnost: F08; vloga: član skupine; obseg: kasnejša nadgradnja.

Kot član skupine želim potrditi predlog z manj nakazili, da lažje poravnamo medsebojne dolgove.

Pogoji sprejema:

1. Pri dolgovih Ana → Bor 20 EUR in Bor → Cene 20 EUR sistem predlaga eno nakazilo Ana → Cene 20 EUR.
2. Predlog ohrani končni položaj vsakega člana in ne poveča števila nakazil.
3. Strinjati se morajo vsi prizadeti člani, tudi Bor v navedenem primeru. En ugovor prepreči spremembo.
4. Čakajoče plačilo prepreči uporabo predloga. Če se dolgovi spremenijo, je treba pripraviti in potrditi nov predlog.
5. Po potrditvi se stare obveznosti povežejo z novimi, novi prejemniki pa določijo svoje račune. Denar se prenese šele ob ločenem plačilu dolžnika.

## US-09 – Delno plačilo

Funkcionalnost: F09; vloga: dolžnik; obseg: kasnejša nadgradnja.

Kot dolžnik želim plačati del dolga, da ga lahko poravnam postopoma.

Pogoji sprejema:

1. Pri dolgu 80 EUR uspešno plačilo 30 EUR pusti še 50 EUR odprtega dolga.
2. Ničelno ali negativno plačilo in plačilo nad preostalim dolgom se zavrnejo.
3. Čakajoče ali neuspešno plačilo ne zmanjša dolga. Za isto obveznost lahko hkrati čaka največ eno plačilo.
4. Uporabnik vidi zgodovino plačil. Po uspešnih plačilih 30 in 50 EUR je dolg 80 EUR v celoti poravnan.
5. Tudi pri sočasnih zahtevah vsota plačil ne sme preseči dolga. Za delna plačila velja isti odobritveni prag kot za druga nakazila.

## US-10 – Ponavljajoči se stroški

Funkcionalnost: F10; vloga: član skupine, ki redno plačuje skupni strošek; obseg: kasnejša nadgradnja.

Kot redni plačnik želim shraniti mesečno predlogo stroška, da mi ni treba vsak mesec ponovno vnašati istih podatkov.

Pogoji sprejema:

1. Predloga vsebuje naziv, kategorijo, znesek, datum, lasten račun ter člane in njihove deleže. Vsota deležev mora ustrezati znesku.
2. Sistem vsak mesec ustvari največ en osnutek. Če izbrani dan ne obstaja, uporabi zadnji dan meseca.
3. Plačnik osnutek pregleda in odda, člani pa potrdijo deleže. Predloga sama ne ustvarja dolga in ne plačuje računov.
4. Če kateri od članov ni več aktiven, mora plačnik pred oddajo popraviti razdelitev.
5. Ustavitev predloge prepreči nove osnutke, že nastali stroški in dolgovi pa ostanejo.

## US-11 – Skupinski proračun

Funkcionalnost: F11; vloga: organizator skupine; obseg: kasnejša nadgradnja.

Kot organizator želim spremljati porabo glede na proračun, da opazim prekoračitev načrtovanih stroškov.

Pogoji sprejema:

1. Organizator določi pozitiven proračun in veljavno obdobje. Končni datum ne sme biti pred začetnim.
2. Pri proračunu 300 EUR in potrjenih stroških 90 ter 60 EUR je poraba 150 EUR oziroma 50 %.
3. Štejejo samo potrjeni stroški z datumom v izbranem obdobju. Povračila in nepotrjeni predlogi ne povečajo porabe.
4. Pri porabi 320 EUR se pokaže presežek 20 EUR. Prekoračitev sama ne blokira novih stroškov ali nakazil.
5. Člani vidijo skupno porabo in porabo po kategorijah. Proračun lahko spreminja samo organizator.

## US-12 – Skupni cilj

Funkcionalnost: F12; vloga: član skupine; obseg: kasnejša nadgradnja.

Kot član skupine želim zabeležiti svoj prispevek k skupnemu cilju, da spremljamo napredek proti načrtovanemu znesku.

Pogoji sprejema:

1. Organizator določi ime cilja, pozitiven znesek in rok. Cilj je označen kot informativen.
2. Član lahko vpiše ali popravi samo svoj nenegativni prispevek.
3. Prispevka 100 in 200 EUR pri cilju 600 EUR pomenita 300 EUR oziroma 50 % doseženega cilja.
4. Vpis prispevka ne prenese ali rezervira denarja in ne ustvari dolga.
5. Ko je cilj dosežen, se prikaže ustrezno sporočilo. Morebitni presežek ostane v skupnem znesku.

## US-13 – Obvestila

Funkcionalnost: F13; vloga: komitent; obseg: kasnejša nadgradnja.

Kot komitent želim prejemati obvestila o potrditvah in plačilih, da pravočasno opravim potrebna dejanja.

Pogoji sprejema:

1. Nov predlog ustvari obvestilo za vključene člane, ki ga morajo potrditi. Povezava odpre njihov delež v ustreznem predlogu.
2. Ob uspešnem plačilu sta obveščena dolžnik in prejemnik. Razlog neuspeha ali zavrnitve prejme samo dolžnik.
3. Za odprt dolg z rokom 17. oktobra dolžnik 18. oktobra prejme opomnik. Poravnan dolg ali dolg brez roka opomnika ne ustvari.
4. Isti dogodek ali ponovljen dnevni pregled ne ustvari podvojenega obvestila.
5. Uporabnik vidi samo svoja obvestila in jih lahko označi kot prebrana. Obvestila se prikazujejo znotraj aplikacije.

## US-14 – Odhod in zaključek skupine

Funkcionalnost: F14; vloga: član skupine; za zaključek skupine organizator; obseg: kasnejša nadgradnja.

Kot član skupine želim zaključiti sodelovanje po poravnavi stroškov, da zapustim skupino in ohranim pregled svoje zgodovine.

Pogoji sprejema:

1. Član lahko odide, ko nima dolgov, terjatev, lastnih osnutkov, nerešenih predlogov, v katerih sodeluje, ali čakajočih plačil.
2. Če ima član dolg ali terjatev 30 EUR, je odhod blokiran. Sistem prikaže, kaj mora še urediti.
3. Organizator mora pred odhodom prenesti svojo pravico na drugega aktivnega člana.
4. Samo organizator lahko zaključi skupino, ko so vse obveznosti poravnane in ni nerešenih predlogov, osnutkov ali čakajočih plačil.
5. Zaključena skupina ne sprejema novih članov in stroškov. Nekdanji član ohrani pregled lastnih preteklih deležev in poravnav.

## US-15 – Izvoz obračuna

Funkcionalnost: F15; vloga: član skupine; obseg: kasnejša nadgradnja.

Kot član skupine želim izvoziti obračun stroškov, da shranim pregled dogovorov in plačil.

Pogoji sprejema:

1. Član izbere obdobje stroškov in čas, do katerega se upoštevajo plačila. Neveljavno obdobje ali prihodnji čas se zavrne.
2. Datoteka CSV vsebuje strošek, datum, plačnika, udeleženca, njegov delež, izvedena povračila in odprti znesek.
3. Pri dolgu 30 EUR izvoz pred izvedbo plačila pokaže odprtih 30 EUR, po izvedbi pa 0 EUR. Čakajoče plačilo dolga ne zmanjša.
4. Izvoz je dostopen samo članom in ne vsebuje osebnih bančnih stanj. Prazno obdobje vrne samo naslove stolpcev.
5. Datoteka pravilno ohrani šumnike, narekovaje in vejice. Besedilna polja se ob odprtju v preglednici ne izvajajo kot formule.

## US-16 – Odobritev nakazila

Funkcionalnost: F02 in F07; vloga: bančni uslužbenec; obseg: MVP.

Kot bančni uslužbenec želim odobriti ali zavrniti čakajoče nakazilo, da je odločitev o prenosu sledljiva.

Pogoji sprejema:

1. Čakalna vrsta pokaže največ 20 nakazil na stran, od najstarejšega. Uslužbenec vidi pošiljatelja, prejemnika, računa, znesek in namen.
2. Odobritev nakazila 650 EUR ob zadostnih sredstvih prenese denar in poravna povezano obveznost.
3. Zavrnitev zahteva razlog. Shranijo se odločitev, uslužbenec in čas; denar se ne prenese.
4. Če je ob odobritvi na voljo samo 600 EUR, nakazilo 650 EUR ne uspe. Tehnična napaka pa pusti nakazilo čakajoče brez delnega prenosa.
5. Isto nakazilo je mogoče obravnavati samo enkrat. Uslužbenec ne vidi osebnih stanj ali vsebine skupinskih stroškov.

## US-17 – Upravljanje dostopov

Funkcionalnost: skupna podlaga F01–F07; vloga: administrator; obseg: MVP.

Kot administrator želim upravljati uporabniške dostope, da imajo uporabniki ustrezne pravice.

Pogoji sprejema:

1. Administrator ustvari uporabnika z imenom, enolično e-pošto, začetnim geslom in vlogo komitenta, uslužbenca ali administratorja.
2. Deaktivacija prekine tudi že odprto sejo. Po ponovni aktivaciji se mora uporabnik znova prijaviti.
3. Vloge komitenta z računom ali članstvom ni mogoče spremeniti. Zadnjemu aktivnemu administratorju ni mogoče odvzeti dostopa ali vloge.
4. Deaktivacija ne izbriše računov ali dolgov. Nov uporabniški dostop sam ne ustvari računa ali denarja.
5. Administrator ne vidi gesel ali finančnih podatkov. Drugi vlogi ne moreta uporabljati administracije.

## US-18 – Prijava in odjava

Funkcionalnost: skupna podlaga vseh zaščitenih funkcionalnosti; vloga: uporabnik z ustvarjenim dostopom; obseg: MVP.

Kot uporabnik želim prijavo in odjavo, da lahko varno uporabljam funkcionalnosti svoje vloge.

Pogoji sprejema:

1. Pravilni podatki aktivnega uporabnika odprejo začetni zaslon njegove vloge.
2. Napačno geslo, neobstoječa e-pošta ali neaktiven dostop prikažejo enako sporočilo: »Prijava ni uspela«.
3. Po odjavi uporabnik brez nove prijave ne more odpirati zaščitenih vsebin.
4. Po 30 minutah neaktivnosti ali ponovnem zagonu strežnika je potrebna nova prijava. Poslovni podatki ostanejo shranjeni.
5. Uporabnik ne more izvajati dejanj druge vloge. Zahteve brez veljavne zaščite pred ponarejenimi zahtevami (CSRF) ne spremenijo podatkov.
