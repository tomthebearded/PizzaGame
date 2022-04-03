using PizzaGame.Models;
using System;
using System.Linq;

namespace PizzaGame.Helpers
{
    internal static class ValidationMethods
    {
        public static bool SelectedOptionIsValid(this string input, int[] options, int? previousOption)
        {
            var inputIsANumber = int.TryParse(input, out int number);
            if (!inputIsANumber)
                return false;

            if (options.Any(x => x == number))
            {
                if (previousOption.HasValue)
                    return number != previousOption.Value;

                return true;
            }

            return false;
        }

        public static NumberOfPizzasValidationModel NumberOfPizzasIsValid(this string input, int minNumberOfPizzas, int maxNumberOfPizzas)
        {
            var result = new NumberOfPizzasValidationModel();
            if (string.IsNullOrWhiteSpace(input))
            {
                result.IsValid = true;
                result.NumberOfPizzas = new Random().Next(minNumberOfPizzas, maxNumberOfPizzas + 1);
            }
            else
            {
                var inputIsANumber = int.TryParse(input, out int number);
                if (!inputIsANumber)
                {
                    result.IsValid = false;
                    result.ErroMessage = "Please type a valid number";
                }
                else if (number > maxNumberOfPizzas || number < minNumberOfPizzas)
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
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Please choose between A(autoplay), C(vs computer) & P(vs player)";
                return false;
            }

            if (input == "A" || input == "C" || input == "P")
                return true;


            errorMessage = "Invalid game mode selected, please choose between A(autoplay), C(vs computer) & P(vs player)";
            return false;
        }

        public static bool ConfigurationIsValid(int[] options, int? maxNumber, int? minNumber)
        {
            if (!maxNumber.HasValue || !minNumber.HasValue || options == null || options.Length < 1)
                return false;

            return minNumber.Value > 0 && maxNumber.Value >= minNumber.Value && options.All(x => x < maxNumber.Value && x > 0);
        }
    }
}
