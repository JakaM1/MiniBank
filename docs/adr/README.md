# Register arhitekturnih odločitev

ADR formalizirajo vseh 12 odločitev iz razdelka »Pomembne arhitekturne odločitve« v [prvi nalogi](../04-arhitektura-in-tehnoloski-sklad.md). Status `accepted` pomeni sprejet načrt, ne že izvedene implementacije. Datum 2026-10-28 označuje zapis odločitev v ADR.

| ADR | Odločitev |
| --- | --- |
| 0001 | [Modularni monolit s skupnimi transakcijami](0001-modularni-monolit.md) |
| 0002 | [Ena relacijska baza PostgreSQL](0002-ena-relacijska-baza.md) |
| 0003 | [Ločitev skupinskega stroška od bančnega prenosa](0003-locitev-stroska-od-denarja.md) |
| 0004 | [Nespremenljiv oddani predlog in novo soglasje](0004-nespremenljivi-predlogi.md) |
| 0005 | [Izvedeno nakazilo kot vir resnice o poravnavi](0005-izracunano-stanje-obveznosti.md) |
| 0006 | [Račun prejemnika določen v potrjenem predlogu](0006-racun-prejemnika-v-predlogu.md) |
| 0007 | [Natančni denarni zneski in delitev centov](0007-centi-in-decimalni-tipi.md) |
| 0008 | [Zaklep skupine in enoten vrstni red zaklepanja](0008-zaklepi-in-vrstni-red.md) |
| 0009 | [Idempotentnost in največ ena aktivna poravnava](0009-idempotentnost-in-aktivna-poravnava.md) |
| 0010 | [Odobritev nad 500 EUR brez rezervacije sredstev](0010-odobritveni-prag-brez-rezervacije.md) |
| 0011 | [Pravice glede na sistemsko vlogo in konkretni vir](0011-pravice-glede-na-vlogo-in-vir.md) |
| 0012 | [Omejen MVP brez delnih poravnav in samodejnega pobota](0012-omejen-mvp.md) |

## Dodajanje in nadomeščanje

1. Kopirajte [predlogo](adr-template.md) v naslednjo prosto številko `NNNN-kratek-naslov.md`; številk ne uporabljajte ponovno.
2. Zapišite kontekst, realne alternative, izbiro z razlogi in obe vrsti posledic. Predlog ima status `proposed`.
3. ADR preglejte v pull requestu skupaj s povezano spremembo; ob sprejetju nastavite `accepted` in datum odločitve. Zavrnjen predlog ima `rejected`.
4. Ob spremembi sprejete odločitve ustvarite nov ADR in v njem povežite »Nadomešča«. Starega ohranite, nastavite status `superseded` in dodajte »Nadomeščen z« s povezavo na novi zapis. Prvotne utemeljitve ne prepisujte.
5. Posodobite ta register in povezave v zasnovi.

Oblika sledi [MADR](https://adr.github.io/madr/#applying-madr-to-your-project) in njegovi [minimalni predlogi](https://github.com/adr/madr/blob/develop/template/adr-template-minimal.md); naslovi so prevedeni v slovenščino.
