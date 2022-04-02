using PizzaGame.Models;
using System;

namespace PizzaGame.Helpers
{
    internal static class ValidationMethods
    {
        public static NumberOfPizzasValidationModel NumberOfPizzasIsValid(this string input, int minNumberOfPizzas, int maxNumberOfPizzas)
        {
            var result = new NumberOfPizzasValidationModel();
            if (String.IsNullOrWhiteSpace(input))
            {
                result.IsValid = true;
                result.NumberOfPizzas = new Random().Next(minNumberOfPizzas, maxNumberOfPizzas);
            }
            else
            {
                var inputIsANumber = int.TryParse(input, out int number);
                if (!inputIsANumber)
                {
                    result.IsValid = false;
                    result.ErroMessage = "Please type a valid number";
                }else if (number > maxNumberOfPizzas || number < minNumberOfPizzas)
                {
                    result.IsValid = false;
                    result.ErroMessage = $"Please type a number between {minNumberOfPizzas} & {maxNumberOfPizzas}";
                }
                else
                {
                    result.IsValid = true;
                    result.NumberOfPizzas = number;
                }
            }
            return result;
        }

        public static bool GameModeSelectedIsValid(this string input, out string errorMessage)
        {
            errorMessage = String.Empty;


            if (String.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Please choose between A(autoplay), C(vs computer) & P(vs player)";
                return false;
            }

            if (input == "A" || input == "C" || input == "P")
                return true;


            errorMessage = "Invalid game mode selected, please choose between A(autoplay), C(vs computer) & P(vs player)";
            return false;
        }
    }
}
