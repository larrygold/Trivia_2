using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        private readonly List<string> _players; 
        private int _currentPlayer;

        private readonly int[] _places;
        private readonly int[] _purses;

        private readonly bool[] _inPenaltyBox;
        private bool _isGettingOutOfPenaltyBox;


        private readonly List<string> _popQuestions;
        private readonly List<string> _scienceQuestions;
        private readonly List<string> _sportsQuestions;
        private readonly List<string> _rockQuestions;

        private readonly int NumberQuestionsPerDeck = 50;


        public Game()
        {
            _players = new List<string>();
            _places = new int[6];
            _purses = new int[6];
            _inPenaltyBox = new bool[6];
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _rockQuestions = new List<string>();

            for (var questionIndex = 0; questionIndex < NumberQuestionsPerDeck; questionIndex++)
            {
                GenerateQuestion(_popQuestions, "Pop Question ", questionIndex);
                GenerateQuestion(_scienceQuestions, "Science Question ", questionIndex);
                GenerateQuestion(_sportsQuestions, "Sports Question ", questionIndex);
                GenerateQuestion(_rockQuestions, "Rock Question ", questionIndex);
            }
        }

        private static void GenerateQuestion(List<string> deck, string header, int questionIndex)
        {
            deck.Add(header + questionIndex);
        }

        public string CreateRockQuestion(int questionIndex)
        {
            return "Rock Question " + questionIndex;
        }

        public bool AddPlayer(string playerName)
        {
            _players.Add(playerName);
            _places[GetNumberPlayers()] = 0;
            _purses[GetNumberPlayers()] = 0;
            _inPenaltyBox[GetNumberPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int GetNumberPlayers()
        {
            return _players.Count;
        }

        public void Roll(int diceValue)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + diceValue);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (diceValue % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + diceValue;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + GetCurrentCategory());
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

                _places[_currentPlayer] = _places[_currentPlayer] + diceValue;

                if (_places[_currentPlayer] > 11) 
                    _places[_currentPlayer] = _places[_currentPlayer] - 12;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + GetCurrentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (GetCurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveAt(0);
            }
            if (GetCurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveAt(0);
            }
            if (GetCurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveAt(0);
            }
            if (GetCurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveAt(0);
            }
        }


        private string GetCurrentCategory()
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

        public bool WasCorrectlyAnswered()
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

                    bool winner = DidPlayerWin();
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

                bool winner = DidPlayerWin();
                _currentPlayer++;

                if (_currentPlayer == _players.Count) 
                    _currentPlayer = 0;

                return winner;
            }
        }

        public bool WasIncorrectlyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;

            if (_currentPlayer == _players.Count) 
                _currentPlayer = 0;

            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
