namespace PizzaGame.Models
{
    internal class NumberOfPizzasValidationModel
    {
        public int? NumberOfPizzas { get; set; }
        public bool IsValid { get; set; } = false;
        public string ErroMessage { get; set; } = string.Empty;
    }
}
