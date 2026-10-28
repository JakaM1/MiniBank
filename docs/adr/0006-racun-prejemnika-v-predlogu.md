---
status: accepted
date: 2026-10-28
---

# 0006 – Račun prejemnika določen v potrjenem predlogu

## Kontekst in problem

Dolžnik poravnava dogovorjen dolg določenemu upniku. Prosto spreminjanje cilja bi omogočilo plačilo drugam in napačno oznako poravnave.

Zapis formalizira odločitev iz [zasnove arhitekture](../04-arhitektura-in-tehnoloski-sklad.md); gre za načrt implementacije.

## Obravnavane alternative

- Račun iz potrjene različice
- Prosta izbira prejemnika pri plačilu
- Trenutni privzeti račun upnika

## Sprejeta odločitev in utemeljitev

Plačnik v predlogu določi lasten račun za povračila. Obveznost prevzame ta račun in znesek; dolžnik izbere samo lasten izvorni račun. Backend zavrne prepis cilja ali zneska.

Izbira je sprejeta, ker poveže soglasje s konkretnim prejemnikom in odpravi odvisnost od kasnejših sprememb privzetega računa.

## Posledice

- Pozitivne: Predvidljiva in preverljiva poravnava; manj možnosti napačnega ali zlonamernega vnosa.
- Negativne: Prejemnega računa po potrditvi ni mogoče preprosto zamenjati; prihodnje zapiranje računov zahteva novo odločitev in postopek.

## Povezave

- [Podatkovni model](../03-podatkovni-model.md)
- [Funkcionalnosti in obseg](../02-funkcionalnosti-in-obseg.md)
- [Register ADR in pravila sprememb](README.md)
