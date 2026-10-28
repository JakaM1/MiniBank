---
status: accepted
date: 2026-10-28
---

# 0003 – Ločitev skupinskega stroška od bančnega prenosa

## Kontekst in problem

Plačnik je večerjo ali izlet že plačal zunaj simulacije. Vnos tega stroška zato ne sme ponovno bremeniti računa MiniBanke.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Ločena evidenca stroškov in nakazil
- Samodejna bremenitev ob vnosu stroška
- Skupni bančni račun skupine

## Sprejeta odločitev in utemeljitev

Vnos in potrditev stroška evidentirata dogovor in obveznosti. Denar se prenese izključno ob uspešni izvedbi nakazila, ki ga sproži dolžnik.

Izbira je sprejeta, ker preprečuje podvojeno plačilo in jasno ločuje dogovor o povračilu od dejanskega denarnega dogodka.

## Posledice

- Pozitivne: Bančna stanja ostanejo pravilna; skupina ne razpolaga z denarjem članov.
- Negativne: Vmesnik mora posebej prikazovati obveznosti in stanja; zunanji izdatek temelji na navedbi plačnika in se ne preverja z bančno integracijo.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
