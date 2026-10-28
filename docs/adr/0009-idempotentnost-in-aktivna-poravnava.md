---
status: accepted
date: 2026-10-28
---

# 0009 – Idempotentnost in največ ena aktivna poravnava

## Kontekst in problem

Omrežna ponovitev ali dvojni klik lahko ponovi zahtevo. Dve zahtevi z različnima ključema pa morata prav tako preprečiti dvojno poravnavo iste obveznosti.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Idempotentni ključ in omejitev aktivne poravnave
- Samo onemogočen gumb
- Samo idempotentni ključ

## Sprejeta odločitev in utemeljitev

Nakazila imajo enoličen par created_by in idempotency_key. Ista vsebina vrne obstoječi rezultat, druga vsebina konflikt. Delni enolični indeks dovoli največ eno PENDING_APPROVAL ali EXECUTED nakazilo na obveznost.

Izbira je sprejeta, ker ločeno zaščiti ponovitve iste operacije ter konkurenčne nove operacije; kontrola samo na frontendu ne zagotavlja celovitosti.

## Posledice

- Pozitivne: Varno ponavljanje po nejasnem omrežnem izidu; zavrnjen ali neuspešen poskus dopušča nov ključ in nov poskus.
- Negativne: Strežnik mora primerjati vsebino in obdržati rezultate; delne poravnave zahtevajo spremembo indeksa in transakcijskih kontrol.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
