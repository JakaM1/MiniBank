# MiniBank – zasnova rešitve

## Ime, namen in problem

**MiniBank** je spletna simulacija bančnega okolja s poudarkom na skupnostnem bančništvu (*community-oriented banking*). Komitentom poleg računov in internih nakazil omogoča oblikovanje skupin, dogovor o delitvi skupnih stroškov in poravnavo potrjenih obveznosti neposredno z računa MiniBanke.

Prijatelji po večerji ali izletu pogosto nimajo enotnega pregleda, kdo je kaj plačal, s katero delitvijo so se udeleženci strinjali in kdo je svoj delež že vrnil. Podobno težavo imajo sostanovalci pri skupnih nakupih. MiniBank poveže dogovor o strošku z izvedbo nakazila: obveznost se označi kot poravnana šele ob uspešnem prenosu sredstev. Uporabnikom ni treba ločeno usklajevati preglednice in bančnih potrdil.

Posebnost rešitve je sklenjen proces **skupina → strošek → potrditev razdelitve → obveznost → nakazilo → poravnava**, s sledljivostjo odločitev in preverjanjem pravic. Skupina nima lastnega bančnega računa in ne more razpolagati z denarjem članov.

## Ciljni uporabniki

- **Komitenti:** prijatelji na izletu, udeleženci večerje ali sostanovalci, ki želijo razdeliti že plačan strošek in povrniti svoj delež.
- **Organizator skupine:** komitent, ki ustvari skupino in povabi člane. To je pravica znotraj določene skupine, ne četrta sistemska vloga.
- **Bančni uslužbenci:** pregledajo nakazila nad odobritvenim pragom ter jih odobrijo ali zavrnejo z razlogom.
- **Administrator:** upravlja uporabniške dostope in sistemske vloge v učnem okolju.

## Kratek opis delovanja

Komitent po prijavi vidi svoje račune in skupinske obveznosti kot ločena pregleda. Ustvari skupino ter povabi obstoječe komitente. Povabljeni pridobi dostop do skupinskih stroškov šele po sprejemu povabila.

Član, ki je plačal strošek, vnese naziv, datum, kategorijo, znesek in svoj račun za povračila. Izbere udeležence ter enakomerno delitev ali ročne zneske. Sistem preveri vsoto deležev in pripravi predlog. Plačnik predlog potrdi ob oddaji; drugi vključeni člani potrdijo svoje deleže ali podajo ugovor. Šele soglasje vseh ustvari obveznosti. Ugovor ustavi potrjevanje te različice; popravljena različica zahteva nove potrditve.

Dolžnik v pregledu obveznosti izbere »Poravnaj«, preveri prejemnika in znesek ter izbere svoj izvorni račun. Nakazilo do vključno **500,00 EUR** se izvede neposredno, višje pa čaka na bančnega uslužbenca. Pri čakanju denar ni rezerviran. Ob odobritvi se sredstva preverijo ponovno. Uspešna izvedba v isti podatkovni transakciji spremeni stanji računov in evidentira poravnavo; zavrnitev ali poslovni neuspeh pustita obveznost odprto.

### Primer: večerja treh prijateljev

Ana, Bor in Cene so člani skupine »Izlet v Prago«. Ana je zunaj MiniBanke plačala večerjo za 90,00 EUR. Vnese strošek s tremi deleži po 30,00 EUR. Bor in Cene potrdita predlog. Nastaneta obveznosti Bor → Ana 30,00 EUR in Cene → Ana 30,00 EUR. Ana svojemu računu ne nakazuje lastnega deleža.

Sam vnos in potrditev stroška ne spremenita bančnih stanj: plačilo restavraciji je že nastalo zunaj simulacije. Če ima Bor 200,00 EUR in Ana 1.000,00 EUR, Borova uspešna poravnava spremeni stanji na 170,00 EUR in 1.030,00 EUR. Njegova obveznost je poravnana, Cenetova pa ostane odprta.

## Prva različica in nadaljnji razvoj

MVP obsega sedem funkcionalnosti F01–F07: finančni pregled, interna nakazila z odobritvami, skupine in povabila, delitev stroškov, soglasje udeležencev, pregled obveznosti in njihovo poravnavo. Prijava ter administracija dostopov sta obvezna skupna podlaga. Vsebina presega osnovni CRUD z usklajevanjem različic, natančno delitvijo centov in transakcijsko povezavo dolga z nakazilom.

Kasnejše nadgradnje F08–F15 vključujejo poenostavitev poravnav, delna plačila, ponavljajoče se stroške, proračune, informativne skupne cilje, opomnike, zaključevanje skupin in izvoz skupinskega obračuna. Posebej zanimiva je poenostavitev poravnav: namesto Ana → Bor 20 EUR in Bor → Cene 20 EUR sistem lahko predlaga Ana → Cene 20 EUR, ob soglasju prizadetih članov in ohranitvi sledljivosti prvotnih obveznosti.

## Omejitve

- Gre za učno spletno aplikacijo z izmišljenimi podatki, ne za pravo banko.
- Edina valuta je EUR; nakazila potekajo med računi MiniBanke. Ni prekoračitev, provizij ali rezervacij sredstev.
- Vsak uporabnik ima eno sistemsko vlogo; samo komitenti imajo račune in članstva v skupinah.
- Začetni računi in stanja se pripravijo z razvojnimi začetnimi podatki. Uporabniški vmesnik ne omogoča ustvarjanja denarja.
- MVP podpira enega plačnika na strošek, celotno poravnavo posamezne obveznosti in aktivno skupino brez odhoda članov. Naprednejši postopki so izrecno ločeni kot nadgradnje.
- Organizator ne more potrjevati v imenu drugih, sprožati njihovih nakazil ali pregledovati njihovih osebnih stanj.

## Dokumentacija

- [Funkcionalnosti in obseg](02-funkcionalnosti-in-obseg.md)
- [Podatkovni model](03-podatkovni-model.md)
- [Arhitektura in tehnološki sklad](04-arhitektura-in-tehnoloski-sklad.md)
- [Uporabniške zgodbe in pogoji sprejema](05-uporabniske-zgodbe.md)
- [Žični modeli uporabniškega vmesnika](06-wireframes.md)
