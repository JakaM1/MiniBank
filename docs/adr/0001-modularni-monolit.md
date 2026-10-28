---
status: accepted
date: 2026-10-28
---

# 0001 – Modularni monolit s skupnimi transakcijami

## Kontekst in problem

Poravnava združuje obveznost in bančni prenos, zadnja potrditev pa ustvarjanje vseh dolgov. Delni uspeh bi povzročil napačno finančno stanje.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Modularni monolit
- Mikroservisi z ločenimi bazami in sago
- Monolit brez modulnih meja

## Sprejeta odločitev in utemeljitev

Modularni monolit z moduli Identity, Banking, Groups, Expenses in Settlements v enem ASP.NET Core procesu. Aplikacijska storitev vodi skupno transakcijo; Banking je med poravnavo ne potrjuje samostojno.

Izbira je sprejeta, ker zagotovi atomsko izvedbo brez porazdeljenega usklajevanja, obseg učnega projekta pa ne upravičuje operativne zahtevnosti mikroservisov.

## Posledice

- Pozitivne: Enostavnejši zagon, preizkušanje in odpravljanje napak; jasne odgovornosti modulov.
- Negativne: Moduli se nameščajo in skalirajo skupaj; disciplinirano je treba ohranjati meje in preprečevati krožne odvisnosti.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
