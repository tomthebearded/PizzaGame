using PizzaGame.Graphics;
using PizzaGame.Helpers;
using PizzaGame.Models;
using System;
using System.Collections.Generic;

namespace PizzaGame
{
    internal class GameEngine
    {
        private GameRulesModel Rules { get; set; }
        private List<string> Pizzas { get; set; }
        private Random Random { get; set; }
        private Action<Random, bool, int, bool, bool, int> Turn { get; set; }

        public GameEngine(GameRulesModel rules)
        {
            Rules = rules;
            Pizzas = AsciiArt.GetPizzaBoxes(rules.MaxNumberOfPizzas);
            Random = new Random();
        }

        public void ResetPizzas(int pizzas)
        {
            Rules.MaxNumberOfPizzas = pizzas;
            Pizzas = AsciiArt.GetPizzaBoxes(pizzas);

        }

        public void StartGame()
        {
            switch (Rules.GameMode)
            {
                case Enums.GameMode.Autoplay:
                    StartAutoplayGame();
                    break;
                case Enums.GameMode.PVP:
                    StartPVPGame();
                    break;
                case Enums.GameMode.PVC:
                    StartPVCGame();
                    break;
                default:
                    break;
            }
        }

        private void StartPVPGame()
        {
            Console.WriteLine($"Starting PVP with {Rules.MaxNumberOfPizzas} pizzas");
            var currentPlayer = Convert.ToBoolean(Random.Next(0, 1));
            PVPTurn(Random, currentPlayer, Rules.MaxNumberOfPizzas);
        }

        private void PVPTurn(Random random, bool currentPlayer, int pizzasLeft, bool lastPizza = false, bool playagain = false, int? previousOption = null)
        {
            if (!playagain)
                DisplayAsciiPizzas(previousOption);
            var player = currentPlayer ? "Player 1" : "Player 2";
            Console.WriteLine($"{player} turn");

            if (lastPizza)
                DisplayLoserMessage(player);

            if (pizzasLeft == 1)
                PVPTurn(random, !currentPlayer, pizzasLeft, true);

            Console.WriteLine($"{player} has these options");
            var options = new List<int>(Rules.Options);
            if (previousOption.HasValue)
                options.Remove(previousOption.Value);
            options.ForEach(x => Console.WriteLine(x));

            var input = Console.ReadLine().Trim();
            if (!input.SelectedOptionIsValid(Rules.Options, previousOption))
            {
                Console.WriteLine("Please enter a valid number, your options are");
                options.ForEach(x => Console.WriteLine(x));
                PVPTurn(Random, currentPlayer, pizzasLeft, lastPizza, true, previousOption);
            }
            else
            {
                var option = int.Parse(input);
                var outcome = pizzasLeft - option;
                if (outcome >= 2)
                    GoToNextTurn(outcome, player, option, () => PVPTurn(random, !currentPlayer, outcome, false, false, option));
                else
                    DisplayLoserMessage(player);
            }
        }


        private void StartPVCGame()
        {
            Console.WriteLine($"Starting PVC with {Rules.MaxNumberOfPizzas} pizzas");
            var currentPlayer = Convert.ToBoolean(Random.Next(0, 1));
            PVCTurn(Random, currentPlayer, Rules.MaxNumberOfPizzas);
        }

        private void PVCTurn(Random random, bool currentPlayer, int pizzasLeft, bool lastPizza = false, bool playagain = false, int? previousOption = null)
        {
            if (!playagain)
                DisplayAsciiPizzas(previousOption);
            var player = currentPlayer ? "Player" : "Computer";
            Console.WriteLine($"{player} turn");
            Console.WriteLine($"{player} has these options");


            if (lastPizza)
                DisplayLoserMessage(player);

            if (pizzasLeft == 1)
                PVCTurn(random, !currentPlayer, pizzasLeft, true);

            var options = new List<int>(Rules.Options);
            if (previousOption.HasValue)
                options.Remove(previousOption.Value);
            options.ForEach(x => Console.WriteLine(x));

            if (currentPlayer)
            {
                var input = Console.ReadLine().Trim();
                if (!input.SelectedOptionIsValid(Rules.Options, previousOption))
                {
                    Console.WriteLine("Please enter a valid number, your options are");
                    options.ForEach(x => Console.WriteLine(x));
                    PVCTurn(Random, currentPlayer, pizzasLeft, lastPizza, true, previousOption);
                }
                else
                {
                    var option = int.Parse(input);
                    var outcome = pizzasLeft - option;
                    if (outcome >= 2)
                        GoToNextTurn(outcome, player, option, () => PVCTurn(random, !currentPlayer, outcome, false, false, option));
                    else
                        DisplayLoserMessage(player);
                }
            }
            else
            {
                var i = random.Next(0, options.Count);
                var option = options[i];
                var outcome = pizzasLeft - option;
                if (outcome >= 2)
                    GoToNextTurn(outcome, player, option, () => PVCTurn(random, !currentPlayer, outcome, false, false, option));
                else
                {
                    options.Remove(option);
                    outcome = pizzasLeft - options[0];
                    if (outcome > 2)
                        GoToNextTurn(outcome, player, option, () => PVCTurn(random, !currentPlayer, outcome, false, false, option));
                    else
                        DisplayLoserMessage(player);
                }
            }
        }

        private void StartAutoplayGame()
        {
            Console.WriteLine($"Starting Autoplay with {Rules.MaxNumberOfPizzas} pizzas");
            var currentPlayer = true;
            AutoPlayTurn(Random, currentPlayer, Rules.MaxNumberOfPizzas);
        }

        private void AutoPlayTurn(Random random, bool currentPlayer, int pizzasLeft, bool lastPizza = false, bool playagain = false, int? previousOption = null)
        {
            if (!playagain)
                DisplayAsciiPizzas(previousOption);

            var player = currentPlayer ? "Computer 1" : "Computer 2";
            Console.WriteLine($"{player} turn");

            if (lastPizza)
                DisplayLoserMessage(player);

            if (pizzasLeft == 1)
                AutoPlayTurn(random, !currentPlayer, pizzasLeft, true);

            var options = new List<int>(Rules.Options);
            if (previousOption.HasValue)
                options.Remove(previousOption.Value);

            Console.WriteLine($"{player} has these options");
            options.ForEach(x => Console.WriteLine(x));

            var i = random.Next(0, options.Count);
            var option = options[i];

            var outcome = pizzasLeft - option;
            if (outcome >= 2)
                GoToNextTurn(outcome, player, option, () => AutoPlayTurn(random, !currentPlayer, outcome, false, false, option));
            else
            {
                options.Remove(option);
                outcome = pizzasLeft - options[0];
                if (outcome > 2)
                    GoToNextTurn(outcome, player, option, () => AutoPlayTurn(random, !currentPlayer, outcome, false, false, option));
                else
                    DisplayLoserMessage(player);
            }
        }

        private void GoToNextTurn(int outcome, string player, int option, Action action)
        {
            Console.WriteLine($"{player} ate {option} {(option > 1 ? "pizzas" : "pizza")}");
            Console.WriteLine($"{outcome} {(outcome > 1 ? "pizzas" : "pizza")} left");
            action.Invoke();
        }

        private void DisplayLoserMessage(string player)
        {
            Console.WriteLine($"{player} ate the last pizza and died");
        }

        private void DisplayAsciiPizzas(int? previousOption)
        {
            if (previousOption.HasValue)
                Pizzas.RemoveRange(0, previousOption.Value);
            Pizzas.ForEach(x => Console.WriteLine(x));
        }
    }
}
