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

do
{
    game.Play();

    Console.Clear();
    Console.Write(game.Display());

    Thread.Sleep(100);
}
while (game.Winner() == null);

var winner = game.Winner();

Console.WriteLine(string.Format("{0} win in {1} iterations", winner?.Name.ToUpper(), game.Iteration));
Console.ReadLine();