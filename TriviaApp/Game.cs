using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        private List<string> _players;
        private int NumberPlayers => _players.Count;

        private int _currentPlayer;

        private int[] _positionOfEachPlayer;
        private int[] _goldCoinsOfEachPlayer;

        private bool[] _inPenaltyBox;
        private bool _isGettingOutOfPenaltyBox;


        private List<string> _popQuestions;
        private List<string> _scienceQuestions;
        private List<string> _sportsQuestions;
        private List<string> _rockQuestions;
        private List<List<string>> _decksOfQuestions;

        private readonly int NumberQuestionsPerDeck = 50;
        private Dictionary<List<string>, string> _deckToHeader;


        public Game()
        {
            InitializeAllFields();
            PopulateAllDecksWithQuestions();
        }

        private void InitializeAllFields()
        {
            _players = new List<string>();
            _positionOfEachPlayer = new int[6];
            _goldCoinsOfEachPlayer = new int[6];
            _inPenaltyBox = new bool[6];
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>()
                {_popQuestions, _scienceQuestions, _sportsQuestions, _rockQuestions};
            _deckToHeader = new Dictionary<List<string>, string>()
            {
                {_popQuestions, "Pop"},
                {_scienceQuestions, "Science"},
                {_sportsQuestions, "Sports"},
                {_rockQuestions, "Rock"}
            };
        }

        private void PopulateAllDecksWithQuestions()
        {
            for (var questionIndex = 0; questionIndex < NumberQuestionsPerDeck; questionIndex++)
            {
                foreach (var deck in _decksOfQuestions)
                {
                    GenerateQuestion(deck, questionIndex);
                }
            }
        }

        private void GenerateQuestion(List<string> deck, int questionIndex)
        {
            deck.Add($"{_deckToHeader[deck]} Question " + questionIndex);
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
            DisplayPlayerAdded(playerName);
        }

        private void DisplayPlayerAdded(string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + NumberPlayers);
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
                    _positionOfEachPlayer[_currentPlayer] = _positionOfEachPlayer[_currentPlayer] + diceValue;
                    if (_positionOfEachPlayer[_currentPlayer] > 11) _positionOfEachPlayer[_currentPlayer] = _positionOfEachPlayer[_currentPlayer] - 12;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _positionOfEachPlayer[_currentPlayer]);
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

                _positionOfEachPlayer[_currentPlayer] = _positionOfEachPlayer[_currentPlayer] + diceValue;

                if (_positionOfEachPlayer[_currentPlayer] > 11) 
                    _positionOfEachPlayer[_currentPlayer] = _positionOfEachPlayer[_currentPlayer] - 12;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _positionOfEachPlayer[_currentPlayer]);
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
            if (_positionOfEachPlayer[_currentPlayer] == 0) return "Pop";
            if (_positionOfEachPlayer[_currentPlayer] == 4) return "Pop";
            if (_positionOfEachPlayer[_currentPlayer] == 8) return "Pop";
            if (_positionOfEachPlayer[_currentPlayer] == 1) return "Science";
            if (_positionOfEachPlayer[_currentPlayer] == 5) return "Science";
            if (_positionOfEachPlayer[_currentPlayer] == 9) return "Science";
            if (_positionOfEachPlayer[_currentPlayer] == 2) return "Sports";
            if (_positionOfEachPlayer[_currentPlayer] == 6) return "Sports";
            if (_positionOfEachPlayer[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _goldCoinsOfEachPlayer[_currentPlayer]++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _goldCoinsOfEachPlayer[_currentPlayer]
                            + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == NumberPlayers) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == NumberPlayers) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _goldCoinsOfEachPlayer[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _goldCoinsOfEachPlayer[_currentPlayer]
                        + " Gold Coins.");

                bool winner = DidPlayerWin();
                _currentPlayer++;

                if (_currentPlayer == NumberPlayers) 
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

            if (_currentPlayer == NumberPlayers) 
                _currentPlayer = 0;

            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_goldCoinsOfEachPlayer[_currentPlayer] == 6);
        }
    }

}
