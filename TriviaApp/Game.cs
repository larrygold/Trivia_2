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

        private int _currentPlayerIndex;
        private string CurrentPlayerName => _players[_currentPlayerIndex];

        private bool CurrentPlayerIsInPenaltyBox
        {
            get => _inPenaltyBox[_currentPlayerIndex];
            set => _inPenaltyBox[_currentPlayerIndex] = value;
        }

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

        public void Roll(int dieValue)
        {
            DisplayCurrentPlayerAndDieValue(dieValue);

            if (CurrentPlayerIsInPenaltyBox)
            {
                if (dieValue % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;
                    Console.WriteLine(CurrentPlayerName + " is getting out of the penalty box");

                    _positionOfEachPlayer[_currentPlayerIndex] += dieValue;

                    if (_positionOfEachPlayer[_currentPlayerIndex] > 11) 
                        _positionOfEachPlayer[_currentPlayerIndex] -= 12;

                    Console.WriteLine(CurrentPlayerName
                            + "'s new location is "
                            + _positionOfEachPlayer[_currentPlayerIndex]);
                    Console.WriteLine("The category is " + GetCurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(CurrentPlayerName + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _positionOfEachPlayer[_currentPlayerIndex] = _positionOfEachPlayer[_currentPlayerIndex] + dieValue;

                if (_positionOfEachPlayer[_currentPlayerIndex] > 11) 
                    _positionOfEachPlayer[_currentPlayerIndex] = _positionOfEachPlayer[_currentPlayerIndex] - 12;

                Console.WriteLine(CurrentPlayerName
                        + "'s new location is "
                        + _positionOfEachPlayer[_currentPlayerIndex]);
                Console.WriteLine("The category is " + GetCurrentCategory());
                AskQuestion();
            }
        }

        private void DisplayCurrentPlayerAndDieValue(int dieValue)
        {
            Console.WriteLine(CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + dieValue);
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
            if (_positionOfEachPlayer[_currentPlayerIndex] == 0) return "Pop";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 4) return "Pop";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 8) return "Pop";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 1) return "Science";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 5) return "Science";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 9) return "Science";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 2) return "Sports";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 6) return "Sports";
            if (_positionOfEachPlayer[_currentPlayerIndex] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (CurrentPlayerIsInPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _goldCoinsOfEachPlayer[_currentPlayerIndex]++;
                    Console.WriteLine(CurrentPlayerName
                            + " now has "
                            + _goldCoinsOfEachPlayer[_currentPlayerIndex]
                            + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == NumberPlayers) _currentPlayerIndex = 0;

                    return winner;
                }
                else
                {
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == NumberPlayers) _currentPlayerIndex = 0;
                    return true;
                }
            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _goldCoinsOfEachPlayer[_currentPlayerIndex]++;
                Console.WriteLine(CurrentPlayerName
                        + " now has "
                        + _goldCoinsOfEachPlayer[_currentPlayerIndex]
                        + " Gold Coins.");

                bool winner = DidPlayerWin();
                _currentPlayerIndex++;

                if (_currentPlayerIndex == NumberPlayers) 
                    _currentPlayerIndex = 0;

                return winner;
            }
        }

        public bool WasIncorrectlyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayerName + " was sent to the penalty box");
            CurrentPlayerIsInPenaltyBox = true;

            _currentPlayerIndex++;

            if (_currentPlayerIndex == NumberPlayers) 
                _currentPlayerIndex = 0;

            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_goldCoinsOfEachPlayer[_currentPlayerIndex] == 6);
        }
    }

}
