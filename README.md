# MiniBank

Učna simulacija skupnostnega bančništva z izmišljenimi podatki in EUR.

Implementirani so prijava, odjava, strežniške seje, CSRF ter pregled lastnih računov in zadnjih 20 izvedenih nakazil. Nakazila, skupine, stroški, obveznosti in upravljanje dostopov sledijo v naslednjih iteracijah.

[MVP Board](https://github.com/users/JakaM1/projects/3) · [Načrt in preverjanje](docs/08-implementacija-mvp.md) · [Uporabniške zgodbe](docs/05-uporabniske-zgodbe.md) · [Arhitektura](docs/04-arhitektura-in-tehnoloski-sklad.md) · [ADR](docs/adr/README.md)

## Lokalni zagon

Potrebujete .NET SDK 10.0.100 (ali novejši popravek 10.0.1xx), Node.js 22.14+ in Docker Compose z delujočim pogonom. Ukaze izvajajte v korenu repozitorija.

1. Kopirajte `.env.example` v `.env` in nastavite lokalno geslo baze.
2. Zaženite bazo in pripravite konfiguracijo. V povezovalnem nizu uporabite isto geslo kot v `.env`; `Seed:Password` je vaše lokalno demonstracijsko geslo z vsaj 12 znaki.

```sh
docker compose up -d db
dotnet tool restore
dotnet restore --locked-mode --configfile NuGet.Config
dotnet user-secrets set "ConnectionStrings:MiniBank" "Host=localhost;Database=minibank;Username=minibank;Password=VASE_GESLO_BAZE" --project backend/MiniBank.Api
dotnet user-secrets set "Seed:Password" "VASE_DEMONSTRACIJSKO_GESLO" --project backend/MiniBank.Api
dotnet user-secrets set "Seed:Enabled" "true" --project backend/MiniBank.Api
dotnet ef database update --project backend/MiniBank.Api
dotnet run --project backend/MiniBank.Api --launch-profile Development
```

3. V drugem terminalu:

```sh
cd frontend
npm ci
npm run dev
```

Odprite [localhost:5173](http://localhost:5173). Razvojni inicializator samo v prazni bazi pripravi `ana@minibank.test`, `bor@minibank.test`, `cene@minibank.test`, `usluzbenec@minibank.test` in `admin@minibank.test` z vašim `Seed:Password`. Komitenti dobijo po 200 EUR. Zgodovina nakazil je sprva prazna. Uslužbenec in administrator imata za zdaj začetni zaslon brez poslovnih dejanj.

Ponovni zagon API zahteva novo prijavo in ohrani podatke. `docker compose stop` ohrani bazo. Namesto Dockerja lahko uporabite lokalni PostgreSQL 17 in prilagodite povezovalni niz. Backend `.env` ne bere; uporablja user-secrets ali okoljske spremenljivke.

## Preverjanje in razvoj

```sh
dotnet test
cd frontend
npm test
npm run build
```

API testi privzeto uporabljajo izolirano SQLite bazo. Z okoljsko spremenljivko `MINIBANK_TEST_POSTGRES` (povezovalni niz lokalne testne baze in uporabnik s pravico `CREATEDB`) iste teste izvedete na PostgreSQL; za vsak test se ustvari in odstrani ločena baza `minibank_tests_*`. CI preveri oba ponudnika in migracije. Sprejemni scenariji so v [evidenci](docs/08-implementacija-mvp.md).

Za vsako spremembo ustvarite razvojno vejo in PR z opisom, preverjanji ter `Closes #številka`. Nalogo premaknite `To Do` → `Doing` → `Done` po uspešnem preverjanju in združitvi. Podrobnosti: [GitHub Flow](docs/07-razvojni-postopek.md). Lokalnih gesel in `.env` ne vključujte v Git.
