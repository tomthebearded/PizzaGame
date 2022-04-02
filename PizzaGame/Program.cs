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
            var maxNumberOfPizzas = Configuration.GetValue<int>("maxNumberOfPizzas");
            var minNumberOfPizzas = Configuration.GetValue<int>("minNumberOfPizzas");
            var minSelectableNumberOfPizzas = minNumberOfPizzas + 1;
            var playModeInputIsValid = false;
            var pizzaNumberInputIsValid = false;
            var selectedNumberOfPizzas = 0;
            var selectedGameMode = String.Empty;
            var keepPlaying = true;
            Console.WriteLine(AsciiArt.skullAndPizza);
            Console.WriteLine("Welcome to PizzaGame");
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
            Console.WriteLine($"If you want to play with a random number of pizzas press ENTER, otherwise type a number between {minSelectableNumberOfPizzas} & {maxNumberOfPizzas} and press ENTER");
            do
            {
                var pizzaNumberInput = Console.ReadLine().Trim();
                var result = pizzaNumberInput.NumberOfPizzasIsValid(minSelectableNumberOfPizzas, maxNumberOfPizzas);
                pizzaNumberInputIsValid = result.IsValid;
                if (!pizzaNumberInputIsValid)
                    Console.WriteLine(result.ErroMessage);
                else
                    selectedNumberOfPizzas = result.NumberOfPizzas.Value;

            }
            while (!pizzaNumberInputIsValid);
            var rules = new GameRulesModel(minNumberOfPizzas, selectedNumberOfPizzas, selectedGameMode);
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
