# IRF_Project

A tervezett funkciók Burka Dávid Tanár Úrral egyeztetve:

1) Login felület alapján (UserID+jelszó) tud a user bejelentkezni
2) Bloomberg terminal alapján egy tőzsdei adatbázis elkészítése, relatív nagyobb adatmennyiségről beszélve
3) Ezt az adatbázist hozzá kapcsoljuk a programhoz de helyi példányon dolgozunk a továbbiakban optimalizációs célokból.
4) 2 fő eleme lenne a formnak. Egy táblázat, amely a loginolt user aktuális stock portfoliojat tartalmazza és egy chart amely az egyes részvények teljesítményét mutatja.
5) Még nem döntöttem el milyen módon pontosan, de lehetne egy olyan feature, hogy ha a user kiválaszt graphon, vagy a táblában egy részvényt az spotlightba kerül.
6) Lenne összes stock gomb amely új űrlapon jeleníti meg a porfolióban található összes részvényt jelenlegi értékét. Itt tud majd a user uj részvényt felvenni a protfoliojába
7) A fő formon lenne egy export gomb, amely a user portfolioját exportálná CSV formában
8) A portfolio mentése (nem exportálás) esetén az adatok visszakerülnek az adatbázisba így amikor a user legközelebb visszalép tudja folytatni, ahol abbahagyta.

A megvalósított funkciók:
1) pipa, a jelszavak hash algoritmussal lefordított formájukban kerülnek tárolásra így növelve az adatbiztonságot. (eredeti jelszo kombinációkat ellenorzes céljából a "user-password.csv" tartalmazza a Resources mappán belül)
2) pipa, Bloomberg terminál api használatával SQL adatbázis feltöltésre került.
3) pipa
4) pipa
5) pipa, Datagridviewba kerülnek az adatok betöltésre. Egy sor kiválasztásával a charton a részvény grafikonja megvastagodik.
6) pipa
7) pipa
8) pipa, SYNC gomb használata visszatölti a változatásokat az SQL adatbázisba.
9) Analóg óra a graphics tétel teljesítéséhez.
