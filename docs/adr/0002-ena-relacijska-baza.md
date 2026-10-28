---
status: accepted
date: 2026-10-28
---

# 0002 – Ena relacijska baza PostgreSQL

## Kontekst in problem

Članstva, različice, soglasja, obveznosti in nakazila so tesno povezani. Potrebujemo trajnost ter preverljivo celovitost tudi ob sočasnih zahtevah.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Ena relacijska baza PostgreSQL
- Ločena baza za vsak modul
- Dokumentna baza

## Sprejeta odločitev in utemeljitev

Ena baza PostgreSQL 17 z EF Core 10 in združljivim ponudnikom Npgsql. FK, CHECK, enolični in delni indeksi dopolnjujejo poslovno validacijo; shemo upravljajo migracije.

Izbira je sprejeta, ker relacijske povezave in ACID transakcije neposredno podpirajo model, delni indeksi pa preprečujejo podvojene aktivne poravnave.

## Posledice

- Pozitivne: Baza uveljavlja ključne omejitve; povezane spremembe se potrdijo skupaj.
- Negativne: Skupna shema zahteva usklajene migracije; delni indeksi in vrstični zaklepi povečajo vezanost na PostgreSQL.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
