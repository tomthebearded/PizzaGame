using System.Collections.Generic;

namespace PizzaGame.Graphics
{
    public static class AsciiArt
    {
        public static string skullAndPizza = @"                 uuuuuuu
             uu$$$$$$$$$$$uu                                     ___
          uu$$$$$$$$$$$$$$$$$uu                                 |  ~~--.
         u$$$$$$$$$$$$$$$$$$$$$u                                |%=@%%/
        u$$$$$$$$$$$$$$$$$$$$$$$u                               |o%%%/
       u$$$$$$$$$$$$$$$$$$$$$$$$$u                           __ |%%o/
       u$$$$$$$$$$$$$$$$$$$$$$$$$u                     _,--~~ | |(_/ ._
       u$$$$$$'   '$$$'   '$$$$$$u                  ,/'  m%%%%| |o/ /  `\.
       '$$$$'      u$u      '$$$$'                 /' m%%o(_)%| |/ /o%%m `\
        $$$u       u$u       u$$$                /' %%@=%o%%%o|   /(_)o%%% `\
        $$$u      u$$$u      u$$$               /  %o%%%%%=@%%|  /%%o%%@=%%  \
         '$$$$uu$$$   $$$uu$$$$'               |  (_)%(_)%%o%%| /%%%=@(_)%%%  |
          '$$$$$$$'   '$$$$$$$'                | %%o%%%%o%%%(_|/%o%%o%%%%o%%% |
            u$$$$$$$u$$$$$$$u                  | %%o%(_)%%%%%o%(_)%%%o%%o%o%% |
             u$'$'$'$'$'$'$u                   | %%o%(_)%%%%%o%(_)%%%o%%o%o%% |
  uuu        $$u$ $ $ $ $u$$       uuu         |  (_)%%=@%(_)%o%o%%(_)%o(_)%  |
 u$$$$        $$$$$u$u$u$$$       u$$$$         \ ~%%o%%%%%o%o%=@%%o%%@%%o%~ /
  $$$$$uu      '$$$$$$$$$'     uu$$$$$$          \. ~o%%(_)%%%o%(_)%%(_)o~ ,/
u$$$$$$$$$$$uu    '''''    uuuu$$$$$$$$$$          \_ ~o%=@%(_)%o%%(_)%~ _/
$$$$'''$$$$$$$$$$uuu   uu$$$$$$$$$'''$$$'            `\_~~o%%%o%%%%%~~_/'
 '''      ''$$$$$$$$$$$uu ''$'''                       `--..____,,--'
           uuuu ''$$$$$$$$$$uuu
  u$$$uuu$$$$$$$$$uu ''$$$$$$$$$$$uuu$$$
  $$$$$$$$$$''''           ''$$$$$$$$$$$'
   '$$$$$'                      ''$$$$''
     $$$'                         $$$$'                                  ";

        private static string pizzaBox = @" _______
| PIZZA |
 ¯¯¯¯¯¯¯";

        public static List<string> GetPizzaBoxes(int pizzas)
        {
            var pizzaBoxes = new List<string>();
            for (int i = 0; i < pizzas; i++)
                pizzaBoxes.Add(pizzaBox);
            return pizzaBoxes;
        }
    }
}
