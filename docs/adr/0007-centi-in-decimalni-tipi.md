---
status: accepted
date: 2026-10-28
---

# 0007 – Natančni denarni zneski in delitev centov

## Kontekst in problem

Delitev 10 EUR med tri osebe ne sme izgubiti centa. Binarna plavajoča vejica in neodvisno zaokroževanje lahko povzročita napačno vsoto.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Celi centi in decimalni tipi
- Binarni float/double
- Neodvisno zaokroževanje posameznih deležev

## Sprejeta odločitev in utemeljitev

Delitev računa s celimi centi, baza uporablja numeric(15,2), C# decimal, API decimalne nize. Ostanek centov se razdeli po determinističnem vrstnem redu; backend ponovno izračuna ali preveri vsoto.

Izbira je sprejeta, ker omogoča natančno ohranitev skupnega zneska ter ponovljiv rezultat na strežniku in odjemalcu.

## Posledice

- Pozitivne: Vsota deležev vedno ustreza strošku; primer 10 EUR da 3,34 + 3,33 + 3,33 EUR.
- Negativne: Potrebne so izrecne pretvorbe in omejitve obsega; vrstni red dodelitve preostalih centov mora biti jasno določen in testiran.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
