using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        private List<string> _players;

        private int[] places = new int[6];
        private int[] purses = new int[6];
        
        private bool[] inPenaltyBox = new bool[6];

        private List<string> popQuestions = new List<string>();
        private List<string> scienceQuestions = new List<string>();
        private List<string> sportsQuestions = new List<string>();
        private List<string> rockQuestions = new List<string>();

        private int currentPlayer = 0;
        private bool isGettingOutOfPenaltyBox;

        public Game()
        {
            _players = new List<string>();

            for (int i = 0; i < 50; i++)
            {
                popQuestions.Add("Pop Question " + i);
                scienceQuestions.Add(("Science Question " + i));
                sportsQuestions.Add(("Sports Question " + i));
                rockQuestions.Add(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {


            _players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return _players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(_players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    Console.WriteLine(_players[currentPlayer]
                            + "'s new location is "
                            + places[currentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                Console.WriteLine(_players[currentPlayer]
                        + "'s new location is "
                        + places[currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveAt(0);
            }
        }


        private String currentCategory()
        {
            if (places[currentPlayer] == 0) return "Pop";
            if (places[currentPlayer] == 4) return "Pop";
            if (places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1) return "Science";
            if (places[currentPlayer] == 5) return "Science";
            if (places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2) return "Sports";
            if (places[currentPlayer] == 6) return "Sports";
            if (places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(_players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(_players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == _players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == _players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
