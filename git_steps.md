# Házi feladat: Git alapok és Merge Conflict feloldása

Ebben a feladatban egy C# konzolos alkalmazás (`DistanceCalculator`) fejlesztése történik. A cél a Git alapvető parancsainak (commit, push, pull, branch, merge) gyakorlása, valamint egy szándékosan előidézett **merge conflict** (ütközés) feloldása.

---

## 1. Repository inicializálása és összekötése

A helyi tároló és a kiinduló fájl létrehozása, majd a GitHub-tárolóhoz kapcsolás:

```bash
git init
echo "# DistanceCalculator" >> README.md
git add .
git commit -m "init"
git branch -M main
git remote add origin https://github.com/Hsanyi82/DistanceCalculator.git
```

## 2. Első funkció: C# konzolalkalmazás létrehozása

Új ág létrehozása az alapalkalmazáshoz, majd az ág beolvasztása a `main` ágba:

```bash
# Új ág létrehozása és váltás
git switch -c create_C_console_project
git add .
git commit -m "empty c console app created"
git push -u origin create_C_console_project

# Visszaváltás a main ágra és beolvasztás
git switch main
git pull origin main
git merge create_C_console_project
```

## 3. Második funkció: Haversine távolság számítás

Az alapvető matematikai logika hozzáadása egy új ágon:

```bash
# Új ág létrehozása és váltás
git switch -c CalculateHaversineDistance
git add .
git commit -m "Haversine distance calculation (result in km)"
git push -u origin CalculateHaversineDistance               

# Visszaváltás a main ágra és beolvasztás
git switch main
git pull origin main
git merge CalculateHaversineDistance               
```

## 4. A Merge Conflict előkészítése (Párhuzamos fejlesztés)

A később ütköző két új ág létrehozása:

```bash
# A mérföldág létrehozása, majd váltás a tengeri mérföld ágára
git switch -c resultInMile
git switch -c resultInNauticalMile
```

A `resultInNauticalMile` ág módosítása, beleértve a konzolos kiírás megváltoztatását, majd az ág beolvasztása a `main` ágba:

```bash
# Módosítások rögzítése (a konfliktus előidézéséhez szükséges kódváltoztatással együtt)
git add .
git commit -m "result in nautical miles added, km changed to kilometers in the console display"
git push -u origin resultInNauticalMile

# Visszaváltás a main-re és beolvasztás
git switch main  
git pull origin main
git merge resultInNauticalMile
```

Ezt követően a `resultInMile` ágon végzendő fejlesztés és annak rögzítése:

```bash
git switch resultInMile
git add .
git commit -m "result in miles added"
git push -u origin resultInMile
```

## 5. A konfliktus (Conflict) előidézése

A `resultInMile` ág beolvasztása a `main` ágba. Mivel mindkét ág módosította a `Program.cs` fájl azonos kódrészletét, a Git ütközést jelez.

```bash
git switch main
git merge resultInMile
```

**Várt terminál kimenet:**
```text
Auto-merging Program.cs
CONFLICT (content): Merge conflict in Program.cs
Automatic merge failed; fix conflicts and then commit the result.
```

Az aktuális állapot lekérdezése:
```bash
git status
```

**Várt terminál kimenet:**
```text
On branch main
Your branch is ahead of 'origin/main' by 3 commits.
  (use "git push" to publish your local commits)

You have unmerged paths.
  (fix conflicts and run "git commit")
  (use "git merge --abort" to abort the merge)

Unmerged paths:
  (use "git add <file>..." to mark resolution)
        both modified:   Program.cs
```

## 6. A Merge Conflict feloldása

A konfliktus feloldásának lépései:

1. **A kód javítása:** A `Program.cs` fájl megnyitása egy kódszerkesztőben.
2. Az ütközést jelző sorok (`<<<<<<< HEAD`, `=======`, `>>>>>>> resultInMile`) megkeresése.
3. A Git által beszúrt jelölők eltávolítása, valamint a kód módosítása úgy, hogy a tengeri mérföldes és a mérföldes logika, továbbá a szöveges módosítások is megfelelően és hibamentesen szerepeljenek. A fájl mentése.
4. **A javított fájl hozzáadása:** A konfliktus feloldásának jelzése a Git számára:
   ```bash
   git add Program.cs
   ```
5. **A merge lezárása (commit):** A konfliktus feloldását rögzítő commit létrehozása.
   ```bash
   git commit -m "Merge conflict resolved in Program.cs"
   ```
6. **Feltöltés a GitHubra:** Az egyesített (merged) állapot feltöltése a távoli tárolóba.
   ```bash
   git push origin main
   ```
