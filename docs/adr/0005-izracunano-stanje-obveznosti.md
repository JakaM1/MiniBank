---
status: accepted
date: 2026-10-28
---

# 0005 – Izvedeno nakazilo kot vir resnice o poravnavi

## Kontekst in problem

Ločeno polje plačano se lahko razide z dejanskim bančnim prenosom. Čakajoče ali zavrnjeno plačilo ne sme zmanjšati odprtega dolga.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Izračun iz povezanih nakazil
- Ročna oznaka plačano
- Ločeno shranjen status z asinhronim usklajevanjem

## Sprejeta odločitev in utemeljitev

Stanje obveznosti izračunamo: EXECUTED pomeni SETTLED, sicer PENDING_APPROVAL pomeni PAYMENT_PENDING, sicer OPEN. V MVP je odprti znesek celotna obveznost ali nič.

Izbira je sprejeta, ker odstrani drugo neodvisno resnico in omogoči, da atomsko potrjeno nakazilo takoj določi tudi poravnavo.

## Posledice

- Pozitivne: Ni lažno poravnanih dolgov; vsi poskusi ostanejo sledljivi.
- Negativne: Pregledi potrebujejo poizvedbe in indekse nad nakazili; uvedba delnih plačil zahteva nov izračun preostanka.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
