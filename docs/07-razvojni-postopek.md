# Razvoj po GitHub Flow

## Običajna sprememba

1. Posodobite lokalno vejo: `git switch main`, nato `git pull --ff-only`.
2. Ustvarite kratko razvojno vejo, npr. `git switch -c feature/pregled-racunov`.
3. Pripravite spremembo in smiselna preverjanja. Pomembno arhitekturno odločitev dodajte kot nov [ADR](adr/README.md).
4. Preglejte `git diff` in `git status`, nato izbrane datoteke dodajte in shranite s smiselnim commit sporočilom.
5. Vejo potisnite z `git push -u origin feature/pregled-racunov` in odprite pull request v `main`. Opišite problem, spremembo ter rezultate preverjanja.
6. Preglejte diff in rezultate preverjanj ter združite pull request. Zahtevanih odobritev je 0, zato avtor lahko združi svojo spremembo.
7. Posodobite lokalni `main`; razvojno vejo po združitvi lahko odstranite.

## Zahtevana zaščita veje main

V Settings → Branches ustvarite pravilo za `main`:

| Nastavitev | Vrednost |
| --- | --- |
| Require a pull request before merging | Vključeno |
| Zahtevano število odobritev | 0; možnost Require approvals izključena |
| Allow force pushes | Izključeno |
| Allow deletions | Izključeno |
| Do not allow bypassing the above settings | Vključeno, tudi za administratorje |

Po shranjevanju preverite nastavitve z dejanskim pull requestom iz razvojne veje v `main`. Izbris ali prisilno prepisovanje glavne veje se zaradi preverjanja ne izvajata.

## Evidenca preverjanja

Povezava do izvedenega pull requesta in rezultat bosta dodana po nastavitvi oddaljenega repozitorija. Ta dokument sam ne dokazuje, da je zaščita na GitHubu vključena.

## Viri

- [GitHub Flow](https://docs.github.com/en/get-started/using-github/github-flow)
- [Upravljanje zaščite vej](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/managing-a-branch-protection-rule)
