using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        private List<string> _players;

        private int[] _places = new int[6];
        private int[] _purses = new int[6];
        
        private bool[] _inPenaltyBox = new bool[6];

        private List<string> _popQuestions = new List<string>();
        private List<string> _scienceQuestions = new List<string>();
        private List<string> _sportsQuestions = new List<string>();
        private List<string> _rockQuestions = new List<string>();

        private int _currentPlayer = 0;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            _players = new List<string>();

            for (int i = 0; i < 50; i++)
            {
                _popQuestions.Add("Pop Question " + i);
                _scienceQuestions.Add(("Science Question " + i));
                _sportsQuestions.Add(("Sports Question " + i));
                _rockQuestions.Add(createRockQuestion(i));
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
            _places[howManyPlayers()] = 0;
            _purses[howManyPlayers()] = 0;
            _inPenaltyBox[howManyPlayers()] = false;

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
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveAt(0);
            }
        }


        private String currentCategory()
        {
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
