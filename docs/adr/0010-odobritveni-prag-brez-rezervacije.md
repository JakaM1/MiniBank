---
status: accepted
date: 2026-10-28
---

# 0010 – Odobritev nad 500 EUR brez rezervacije sredstev

## Kontekst in problem

Večja nakazila pregleduje uslužbenec. Med oddajo in obravnavo lahko komitent porabi sredstva drugje, zato prvotno preverjanje ne zagotavlja uspeha.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Čakanje brez rezervacije
- Rezervacija ob oddaji
- Takojšnja izvedba vseh nakazil

## Sprejeta odločitev in utemeljitev

Do vključno 500,00 EUR nakazilo izvedemo neposredno. Nad pragom dobi PENDING_APPROVAL brez rezervacije; ob odobritvi pod zaklepi ponovno preverimo sredstva in aktivnost. Poslovni neuspeh dobi FAILED, tehnična napaka razveljavi transakcijo in ohrani čakanje.

Izbira je sprejeta, ker ohranja zahtevani pregled večjih prenosov in se izogne dodatnemu modelu rezervacij v MVP.

## Posledice

- Pozitivne: Preprostejši model razpoložljivega stanja; odločitev uslužbenca in rezultat sta sledljiva.
- Negativne: Čakajoče nakazilo nima zagotovljenih sredstev; tudi odobreno nakazilo lahko poslovno ne uspe in dolžnik potrebuje nov poskus.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
