---
status: accepted
date: 2026-10-28
---

# 0004 – Nespremenljiv oddani predlog in novo soglasje

## Kontekst in problem

Član mora potrditi točno določen znesek, račun in udeležence. Popravek po pridobljenem soglasju bi spremenil pomen njegove odločitve.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Nespremenljive oddane različice
- Urejanje predloga ob ohranitvi potrditev
- Brisanje predloga in zgodovine

## Sprejeta odločitev in utemeljitev

Oddaja zamrzne vsebino različice. Ugovor ustavi potrjevanje; nova različica nastane iz sporne, staro označi kot SUPERSEDED in zahteva nove potrditve. Potrjene različice ni mogoče nadomestiti.

Izbira je sprejeta, ker ohrani dokaz, s katero vsebino je vsak član soglašal, in prepreči nastanek dolgov iz spornega predloga.

## Posledice

- Pozitivne: Sledljivost odločitev in jasni prehodi stanj; zadnja potrditev ter vse obveznosti nastanejo atomarno.
- Negativne: Več zapisov in zahtevnejši vmesnik za zgodovino; tudi majhen popravek zahteva nov krog soglasij.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
