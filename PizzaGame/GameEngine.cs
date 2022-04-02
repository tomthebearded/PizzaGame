using PizzaGame.Graphics;
using PizzaGame.Models;
using System;
using System.Collections.Generic;

namespace PizzaGame
{
    internal class GameEngine
    {
        private GameRulesModel Rules { get; set; }
        private List<string> Pizzas { get; set; }

        public GameEngine(GameRulesModel rules)
        {
            Rules = rules;
            Pizzas = AsciiArt.GetPizzaBoxes(rules.MaxNumberOfPizzas);
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
                    break;
                case Enums.GameMode.PVC:
                    break;
                default:
                    break;
            }
        }

        private void StartAutoplayGame()
        {
            var random = new Random();
            Console.WriteLine($"Starting Autoplay with {Rules.MaxNumberOfPizzas} pizzas");
            var playerSwitch = true;
            AutoPlayTurn(random, playerSwitch, Rules.MaxNumberOfPizzas);
        }

        private void AutoPlayTurn(Random random, bool playerSwitch, int pizzasLeft, bool lastPizza = false, int? previousEating = null)
        {
            if (previousEating.HasValue)
                Pizzas.RemoveRange(0, previousEating.Value);
            Pizzas.ForEach(x => Console.WriteLine(x));

            var player = playerSwitch ? "Computer 1" : "Computer 2";

            if (lastPizza)
            {
                Console.WriteLine($"{player} ate the last pizza and died");
                return;
            }

            if (pizzasLeft == 1)
                AutoPlayTurn(random, !playerSwitch, pizzasLeft, true);

            var options = new List<int>(GameRulesModel.Options);
            if (previousEating.HasValue)
                options.Remove(previousEating.Value);
            Console.WriteLine($"{player} has these options");
            options.ForEach(x => Console.WriteLine(x));

            var option = options[random.Next(0, options.Count - 1)];

            var outcome = pizzasLeft - option;
            if (outcome >= 2)
            {
                Console.WriteLine($"{player} ate {option} {(option > 1 ? "pizzas" : "pizza")}");
                Console.WriteLine($"{outcome} {(outcome > 1 ? "pizzas" : "pizza")} left");
                AutoPlayTurn(random, !playerSwitch, outcome, false, option);
            }
            else
            {
                options.Remove(option);
                outcome = pizzasLeft - options[0];
                if (outcome > 2)
                {
                    Console.WriteLine($"{player} ate {option} {(option > 1 ? "pizzas" : "pizza")}");
                    Console.WriteLine($"{outcome} {(outcome > 1 ? "pizzas" : "pizza")} left");
                    AutoPlayTurn(random, !playerSwitch, outcome, false, option);
                }
                else
                {
                    Console.WriteLine($"{player} ate the last pizza and died");
                    return;
                }
            }
        }
    }
}
