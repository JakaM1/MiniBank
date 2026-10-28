# MiniBank

MiniBank je učna spletna simulacija bančnega okolja s poudarkom na skupnostnem bančništvu. Poveže skupino, razdelitev že plačanega stroška, soglasje udeležencev in poravnavo potrjene obveznosti z internim nakazilom. Skupina nima svojega bančnega računa. Aplikacija uporablja izmišljene podatke in EUR.

## Stanje projekta

Repozitorij vsebuje zasnovo, uporabniške zgodbe, žične modele in arhitekturne odločitve. Implementacija še ni pripravljena; mapi `frontend/` in `backend/` vsebujeta samo `.gitkeep`.

## Načrtovane tehnologije

- Frontend: React, TypeScript, Vite, HTML in CSS; Node.js 22 in npm.
- Backend: C#, .NET 10, ASP.NET Core Web API.
- Podatki: PostgreSQL 17, EF Core 10 in združljiv Npgsql.
- Dostop: strežniške seje, Cookie Authentication in CSRF zaščita.
- Razvoj in preverjanje: Docker Compose, Git, xUnit, WebApplicationFactory, Testcontainers in Vitest.

Točne združljive različice bodo zaklenjene pred implementacijo. Podrobnosti in načrt zagona so v [arhitekturi](docs/04-arhitektura-in-tehnoloski-sklad.md).

## Dokumentacija

1. [Zasnova rešitve](docs/01-zasnova-resitve.md)
2. [Funkcionalnosti in obseg](docs/02-funkcionalnosti-in-obseg.md)
3. [Podatkovni model](docs/03-podatkovni-model.md)
4. [Arhitektura in tehnološki sklad](docs/04-arhitektura-in-tehnoloski-sklad.md)
5. [Uporabniške zgodbe](docs/05-uporabniske-zgodbe.md)
6. [Žični modeli](docs/06-wireframes.md)
7. [Arhitekturne odločitve – register ADR](docs/adr/README.md)
8. [GitHub Flow in preverjanje zaščite](docs/07-razvojni-postopek.md)

Dokumenti 01–04 izvirajo iz prve naloge, 05–06 in PNG iz druge; ob prenosu so usklajene relativne povezave in imena slik.

## Razvoj

Uporabljamo [GitHub Flow](https://docs.github.com/en/get-started/using-github/github-flow): kratka razvojna veja, pregledljiv commit, pull request v `main`, preverjanje in združitev. Pomembno novo odločitev dokumentiramo z novim ADR. Lokalnih gesel, `.env` in povezovalnih nizov ne vključujemo v Git.
