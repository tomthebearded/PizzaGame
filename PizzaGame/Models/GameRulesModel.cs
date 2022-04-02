using PizzaGame.Enums;

namespace PizzaGame.Models
{
    internal class GameRulesModel
    {
        public static readonly int[] Options =  { 1, 2, 3 };

        public GameMode GameMode { get; set; }
        public int MinNumberOfPizzas { get; set; }
        public int MaxNumberOfPizzas { get; set; }
        
        public GameRulesModel(int minNumberOfPizzas, int maxNumberOfPizzas, string gameMode)
        {
            this.MinNumberOfPizzas = minNumberOfPizzas;
            this.MaxNumberOfPizzas = maxNumberOfPizzas;
            this.GameMode = gameMode == "A" ? GameMode.Autoplay : (gameMode == "P" ? GameMode.PVP : GameMode.PVC);
        }
    }
}
