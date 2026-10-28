---
status: accepted
date: 2026-10-28
---

# 0008 – Zaklep skupine in enoten vrstni red zaklepanja

## Kontekst in problem

Sočasni potrditvi ali plačili lahko ustvarita podvojene dolgove oziroma negativno stanje. Različen vrstni red zaklepanja poveča nevarnost medsebojne blokade.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Vrstični zaklepi z enotnim vrstnim redom
- Samo optimistično preverjanje
- Zaklep celotne baze

## Sprejeta odločitev in utemeljitev

Spreminjajoče skupinske operacije zaklenejo skupino, nato po potrebi strošek, obveznost, nakazilo, uporabnike po ID in račune po ID. Uporabimo SELECT FOR UPDATE v eksplicitni transakciji in ponovno preverimo pogoje; osnutek dodatno varuje row_version.

Izbira je sprejeta, ker poenostavi zagotavljanje pravilnosti v skupini, zaklepi računov pa zaščitijo tudi prenose med skupinami.

## Posledice

- Pozitivne: Največ en nabor obveznosti; sočasna poraba ne ustvari negativnega stanja. Skupen vrstni red zmanjša blokade.
- Negativne: Spremembe iste skupine se izvajajo zaporedno; dolge transakcije omejujejo prepustnost. Morebitno blokado je treba razveljaviti in varno ponoviti.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
