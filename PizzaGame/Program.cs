using Microsoft.Extensions.Configuration;
using PizzaGame.Graphics;
using PizzaGame.Helpers;
using PizzaGame.Models;
using System;

namespace PizzaGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            var maxNumberOfPizzas = Configuration.GetValue<int?>("maxNumberOfPizzas");
            var minNumberOfPizzas = Configuration.GetValue<int?>("minNumberOfPizzas");
            var options = Configuration.GetSection("options").Get<int[]>();

            if (!ValidationMethods.ConfigurationIsValid(options, maxNumberOfPizzas, minNumberOfPizzas))
            {
                Console.WriteLine("Game Configuration is invalid");
                Environment.Exit(0);
            }

            var minSelectableNumberOfPizzas = minNumberOfPizzas.Value + 1;
            var playModeInputIsValid = false;
            var pizzaNumberInputIsValid = false;
            var selectedNumberOfPizzas = 0;
            var selectedGameMode = String.Empty;
            var keepPlaying = true;
            Console.WriteLine(AsciiArt.skullAndPizza);
            Console.WriteLine("Welcome to PizzaGame");
            Console.WriteLine("Rules:");
            Console.WriteLine(" * Two players");
            Console.WriteLine(" * On a table there is a pile of pizzas, the last one is poisoned");
            Console.WriteLine(" * If a player eats the poisoned pizza loses");
            Console.WriteLine(" * You can't skip your turn");
            Console.WriteLine(" * In your turn you must eat a chosen number of pizzas from the top of the pile between the given options");
            Console.WriteLine(" * You can't eat the same number of pizzas chosen by the previous player");
            Console.WriteLine(" * If a player in his turn doesn't have any valid option must skip the turn, the other player must eat the poisoned pizza");

            Console.WriteLine($"If you want autoplay press 'A', if you want to play against the computer press 'C', if you want to play with another player press 'P'");
            do
            {
                var playModeInput = Console.ReadLine().ToUpper().Trim();
                playModeInputIsValid = playModeInput.GameModeSelectedIsValid(out string errorMesage);
                if (!playModeInputIsValid)
                    Console.WriteLine(errorMesage);
                else
                    selectedGameMode = playModeInput;
            }
            while (!playModeInputIsValid);
            Console.WriteLine($"If you want to play with a random number of pizzas press ENTER, otherwise type a number between {minSelectableNumberOfPizzas} & {maxNumberOfPizzas.Value} and press ENTER");
            do
            {
                var pizzaNumberInput = Console.ReadLine().Trim();
                var result = pizzaNumberInput.NumberOfPizzasIsValid(minSelectableNumberOfPizzas, maxNumberOfPizzas.Value);
                pizzaNumberInputIsValid = result.IsValid;
                if (!pizzaNumberInputIsValid)
                    Console.WriteLine(result.ErroMessage);
                else
                    selectedNumberOfPizzas = result.NumberOfPizzas.Value;

            }
            while (!pizzaNumberInputIsValid);
            var rules = new GameRulesModel(minNumberOfPizzas.Value, selectedNumberOfPizzas, selectedGameMode, options);
            var engine = new GameEngine(rules);
            do
            {
                engine.StartGame();
                Console.WriteLine("If you want to play again press any key, otherwise press ESC to exit");
                var key = Console.ReadKey();
                keepPlaying = key.Key != ConsoleKey.Escape;
                engine.ResetPizzas(selectedNumberOfPizzas);
            } while (keepPlaying);
            Environment.Exit(0);
        }
    }
}
