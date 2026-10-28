---
status: accepted
date: 2026-10-28
---

# 0012 – Omejen MVP brez delnih poravnav in samodejnega pobota

## Kontekst in problem

Delne poravnave, spremembe upnikov in skupni računi bistveno razširijo transakcijska pravila in model soglasja. Prva različica mora omogočiti izvedljiv, preverljiv celoten proces.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- F01–F07 in skupna podlaga
- Vse F01–F15 takoj
- Skupni račun s samodejnim pobotom

## Sprejeta odločitev in utemeljitev

MVP zajema F01–F07, prijavo in administracijo: en plačnik, soglasje vseh, celotna poravnava, aktivne skupine, EUR. F08–F15 ostanejo nadgradnje; poenostavitev, delna plačila in odhodi zahtevajo nove ADR ter migracije.

Izbira je sprejeta, ker omeji zahtevnost in omogoča najprej preveriti sklenjen proces od dogovora do pravilnega nakazila.

## Posledice

- Pozitivne: Jasni pogoji sprejema in manj kombinacij stanj; zgodbe in žični modeli ločujejo MVP od nadgradenj.
- Negativne: Uporabniki še ne morejo delno plačevati, odhajati ali poenostavljati dolgov; prihodnji razvoj bo posegel v omejitve in izračune modela.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
