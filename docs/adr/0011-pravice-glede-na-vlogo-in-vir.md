---
status: accepted
date: 2026-10-28
---

# 0011 – Pravice glede na sistemsko vlogo in konkretni vir

## Kontekst in problem

Sama prijava ali članstvo ne smeta razkriti osebnih bančnih podatkov drugih. Organizator potrebuje upravljanje skupine brez pooblastila za tuji denar.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Vloga in preverjanje pravic na viru
- Samo preverjanje vloge
- Skupinski skrbnik z dostopom do računov

## Sprejeta odločitev in utemeljitev

Backend preveri eno sistemsko vlogo CUSTOMER, CLERK ali ADMIN ter lastništvo, članstvo in vključenost v predlog. Organizator je pravica članstva. Uporabimo strežniške seje s piškotkom, 30 minut neaktivnosti, preverjanje aktivnega dostopa pri vsaki zahtevi in CSRF zaščito.

Izbira je sprejeta, ker združi administrativno ločitev odgovornosti z dejanskimi pravicami na posameznem računu, skupini ali deležu.

## Posledice

- Pozitivne: Organizator ne plačuje za druge; namenski DTO preprečijo razkritje bančnih podatkov v skupini; deaktivacija učinkuje na odprte seje.
- Negativne: Vsaka pot potrebuje dosledno preverjanje in teste dostopa; pomnilniški ITicketStore ob ponovnem zagonu zahteva novo prijavo in ne podpira več instanc brez nadgradnje.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
