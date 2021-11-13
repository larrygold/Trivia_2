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
            var players = new Players();
            players.Add("Chet");
            players.Add("Pat");
            players.Add("Sue");

            Game aGame = new Game(players);
            
            Random rand;

            if (seed != null)
                rand = new Random(seed.Value);
            else
                rand = new Random();

            do
            {

                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    aGame.WasIncorrectlyAnswered();
                }
                else
                {
                    aGame.WasCorrectlyAnswered();
                }



            } while (aGame.DoesGameContinue());

        }
    }
}
