using war_card_game_tdd;
using war_card_game_tdd.Utils;

var players = 0;

Console.WriteLine("Welcome to War card game simulator.");
Console.Write("Please enter the number (2 - 6) of players: ");

do
{
    var playersFromConsole = Console.ReadLine();
    int.TryParse(playersFromConsole, out players);

    if(!PlayerUtils.ValidateNumberOfPlayers(players))
    {
        players = 0;
        Console.Write("Wrong number of players. Try again: ");
    }
} 
while (players == 0);

var game = new Game(players);


/*
 - Witaj
 - Podaj liczbę graczy: <liczba_graczy>
 
 -> Wygeneruj nową grę: Gracze, Ilosc interacji
 -> Wygeneruj graczy (liczba_graczy): Imie, TaliaDostepna, TaliaUzyta
    - Wylosuj imię
 -> Wygeneruj talie 
    - Nowa
    - Tasuj
    - Rozdaj -> do graczy: TaliaDostepna
 -> Graj
    ? Jesli gracz nie ma dostepej tali a ma talie uzyta dodaj karty i potasuj
    ? Jesli tylko 1 gracz ma karty > Wygrana
    - Wybierz graczy którzy mają jeszcze karty
    - Pobierz po 1 karcie z każdego deku
    - Porównaj karty
        - Jeśli jest wojna (gracz musi miec conajmniej jeszcze 2 karty, jak nie to przegrywa wojne)
            - Pobierz 1 karte
            - Pobierz 1 karte i porownaj
                ? Jesli dalej wojna powtórz
    - Zwyciezca zabiera karty > TaliaUzyta
  -> Wyświetl zwycięzce i ilość interacji              
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 */