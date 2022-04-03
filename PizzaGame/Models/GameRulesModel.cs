using PizzaGame.Enums;

namespace PizzaGame.Models
{
    internal class GameRulesModel
    {
        public int[] Options { get; set; }

        public GameMode GameMode { get; set; }
        public int MinNumberOfPizzas { get; set; }
        public int MaxNumberOfPizzas { get; set; }
        
        public GameRulesModel(int minNumberOfPizzas, int maxNumberOfPizzas, string gameMode, int[] options)
        {
            this.MinNumberOfPizzas = minNumberOfPizzas;
            this.MaxNumberOfPizzas = maxNumberOfPizzas;
            this.GameMode = gameMode == "A" ? GameMode.Autoplay : (gameMode == "P" ? GameMode.PVP : GameMode.PVC);
            this.Options = options;
        }
    }
}
