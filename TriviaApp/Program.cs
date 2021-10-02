using System;
using UglyTrivia;

namespace TriviaApp
{
    public class Program
    {
        private static bool notAWinner;

        public static void Main(String[] args)
        {
            Game(null);
        }

        public static void Game(int? seed)
        {
            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            Random rand;

            if (seed != null)
                rand = new Random(seed.Value);
            else
                rand = new Random();

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);

        }
    }
}
