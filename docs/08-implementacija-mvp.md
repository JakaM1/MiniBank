# Implementacija MVP

[GitHub Project – MVP Board](https://github.com/users/JakaM1/projects/3) uporablja `To Do`, `Doing`, `Done`. Vsak Issue vsebuje oznako US, izvedljivo nalogo in povezavo na pogoje sprejema iz 2. naloge.

## Vrstni red razvoja

| Iteracija | Naloge |
| --- | --- |
| 1: prijava in računi | #16 seje/CSRF, #3 podatki in API, #4 React vmesnik |
| 2: denar | #5 nakazilo, #6 idempotentnost/sočasnost, #14 odobritve |
| 3: skupinski dogovor | #7 skupine, #8 povabila, #9 delitev, #10 oddaja, #11 soglasja |
| 4: poravnave in dostopi | #12 pregled dolgov, #13 plačilo, #15 administracija |

Odvisnosti: poravnava potrebuje potrjene obveznosti in nakazila; administracija pri spremembi dostopa mora posodobiti `SecurityStamp`. Nova vloga ali reaktivacija zahteva novo prijavo.

## Preverjanje prve iteracije

| Pogoji iz 2. naloge | Dokaz / meja |
| --- | --- |
| US-18/1–3 | Integracijski testi prijave vseh vlog, enotne napake in odjave; zaščitene poti po odjavi vrnejo 401. |
| US-18/4 | Test ure: dejavnost podaljša sejo, 30 minut neaktivnosti jo prekine. Nova shramba ne pozna prejšnjih vstopnic. Poslovni podatki so v PostgreSQL. |
| US-18/5 | Testi zavrnejo drugo vlogo, prijavo in odjavo brez CSRF; neuspešna odjava seje ne prekine. |
| US-01/1,2,5 | Testi lastništva, praznega seznama, decimalnega niza stanja in zadnjih 20 izvedenih nakazil; tuji račun vrne 404. Skupinske obveznosti še niso implementirane. |
| US-01/3,4 | Preverjanje statusov oddanih nakazil in sprememb stanj sledi v #5 in #14. Celotna US-01 zato še ni zaključena. |

Preverjeno 10. 11. 2026: vseh 8 API testov uspe na SQLite in PostgreSQL 17.6; oba frontend testa ter produkcijska gradnja uspejo. Model nima sprememb brez migracije. npm audit in NuGet pregled neposrednih ter tranzitivnih odvisnosti ne poročata o znanih ranljivostih.

V brskalniku je preverjen tok prijava Ane → lastni račun 200 EUR → prazna zgodovina → odjava. Backend je združen prek [PR #17](https://github.com/JakaM1/MiniBank/pull/17); nadaljnje spremembe sledijo istemu postopku.

Frontend testi preverjajo prenos CSRF, piškotkov in obravnavo poteka seje. `npm run build` preveri TypeScript in produkcijsko gradnjo. Preverjanje sočasnih denarnih prenosov še sledi v #6 in #13. Lokalni Docker Desktop se je ustavil z notranjo napako; za preverjanje je bila uporabljena izolirana prenosna baza PostgreSQL 17.6 na localhost:55432.

Različice so zaklenjene v `global.json`, projektnih datotekah, `packages.lock.json` in `package-lock.json`. Migracija `InitialBanking` pripravi uporabnike, račune, nakazila, tuje ključe, indekse in omejitvi nenegativnega stanja ter pozitivnega nakazila.
