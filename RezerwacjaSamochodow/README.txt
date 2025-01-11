Dokumentacja Specyfikacji Projektu
Przegląd Projektu
System rezerwacji samochodów zbudowany przy użyciu Blazor i .NET 8 umożliwia:
•	Przeglądanie, tworzenie, edytowanie i usuwanie rezerwacji samochodów.
•	Zarządzanie flotą samochodów i rezerwacjami przez administratorów.
________________________________________
Instalacja
Wymagania wstępne
•	.NET 8 SDK
•	Visual Studio 2022
•	SQL Server lub inna kompatybilna baza danych
Kroki instalacji
1.	Sklonowanie repozytorium:
git clone <repository-url>
2.	Otwarcie projektu:
Otwórz plik rozwiązania w Visual Studio 2022.
3.	Przywrócenie pakietów NuGet:
dotnet restore
4.	Aktualizacja łańcucha połączenia z bazą danych: W pliku appsettings.json:
"ConnectionStrings": {
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RezerwacjaSamochodow;Trusted_Connection=True;MultipleActiveResultSets=true"}
5.	Zastosowanie migracji:
dotnet ef database update
6.	Uruchomienie aplikacji:
dotnet run
________________________________________

Wymagania
•	.NET 8 SDK
•	Visual Studio 2022
•	SQL Server lub inna kompatybilna baza danych
________________________________________
Konfiguracja
Połączenie z Bazą Danych
Plik appsettings.json zawiera konfigurację łańcucha połączenia z bazą danych:
"ConnectionStrings": {
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RezerwacjaSamochodow;Trusted_Connection=True;MultipleActiveResultSets=true"}
Użytkownicy Testowi
W celach testowych można skorzystać z następujących danych:
•	Administrator:
o	Login: admin@example.com
o	Hasło: Haslo123!
•	Użytkownik:
o	Login: regularuser@example.com
o	Hasło: Haslo123!
Ustawienia Aplikacji
•	Upewnij się, że plik appsettings.json jest poprawnie skonfigurowany.
•	W razie potrzeby zainicjuj bazę danych danymi początkowymi.
________________________________________




Działanie Aplikacji
Role Użytkowników
•	Admin: Może zarządzać samochodami i rezerwacjami.
Funkcje
1.	Zarządzanie Samochodami:
o	Przeglądanie listy samochodów.
o	Dodawanie nowych samochodów.
o	Edytowanie istniejących samochodów.
o	Usuwanie samochodów.
2.	Zarządzanie Rezerwacjami:
o	Przeglądanie listy rezerwacji.
o	Dodawanie nowych rezerwacji.
o	Edytowanie istniejących rezerwacji.
o	Usuwanie rezerwacji.
________________________________________
Przewodnik Użytkownika
Logowanie
1.	Przejdź do strony logowania.
2.	Wprowadź nazwę użytkownika i hasło.
3.	Kliknij przycisk "Login".
Zarządzanie Samochodami
1.	Przejdź do sekcji "Cars".
2.	Przeglądaj listę samochodów.
3.	Kliknij "Create New Car", aby dodać nowy samochód.
4.	Kliknij "Edit", aby zmodyfikować istniejący samochód.
5.	Kliknij "Delete", aby usunąć samochód.
6.	Kliknij "Details", aby sprawdzić specyfikacje samochodu.


Zarządzanie Rezerwacjami
1.	Przejdź do sekcji "Reservations".
2.	Przeglądaj listę rezerwacji.
3.	Kliknij "Create New Reservation", aby dodać nową rezerwację.
4.	Kliknij "Details", aby sprawdzić dane rezerwacji.
5.	Kliknij "Edit", aby zmodyfikować istniejącą rezerwację.
6.	Kliknij "Delete", aby usunąć rezerwację.
________________________________________
Testowanie
•	Zaloguj się jako administrator.
•	Dodaj pojazd i stwórz rezerwację.
•	Sprawdź czy formularze walidują poprawność danych.
Podsumowanie
Ten dokument zapewnia przegląd instalacji, wymagań, konfiguracji i działania systemu rezerwacji samochodów. Postępuj zgodnie z opisanymi krokami, aby poprawnie skonfigurować i uruchomić aplikację.

