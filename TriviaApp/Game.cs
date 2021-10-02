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

        private int[] _positionOfEachPlayer;

        private int CurrentPlayerPosition
        {
            get => _positionOfEachPlayer[_currentPlayerIndex];
            set => _positionOfEachPlayer[_currentPlayerIndex] = value;
        }

        private int[] _goldCoinsOfEachPlayer;
        private int CurrentPlayerGoldCoins
        {
            get => _goldCoinsOfEachPlayer[_currentPlayerIndex];
            set => _goldCoinsOfEachPlayer[_currentPlayerIndex] = value;
        }


        private bool[] _inPenaltyBox;
        private bool CurrentPlayerIsInPenaltyBox
        {
            get => _inPenaltyBox[_currentPlayerIndex];
            set => _inPenaltyBox[_currentPlayerIndex] = value;
        }

        private bool _isGettingOutOfPenaltyBox;


        private List<string> _popQuestions;

        private List<string> _scienceQuestions;

        private List<string> _sportsQuestions;

        private List<string> _rockQuestions;

        private List<List<string>> _decksOfQuestions;

        private readonly int NumberQuestionsPerDeck = 50;

        private Dictionary<List<string>, string> _questionCategoryDeckToName;
        private Dictionary<string, List<string>> _questionCategoryNameToDeck;
        private Dictionary<int, string> _positionOnBoardToQuestionCategoryName;

        private int _numberPositionsOnBoard;

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
            _questionCategoryDeckToName = new Dictionary<List<string>, string>()
            {
                {_popQuestions, "Pop"},
                {_scienceQuestions, "Science"},
                {_sportsQuestions, "Sports"},
                {_rockQuestions, "Rock"}
            };
            _positionOnBoardToQuestionCategoryName = new Dictionary<int, string>()
            {
                {0, "Pop"},
                {1, "Science"},
                {2, "Sports"},
                {3, "Rock"},
                {4, "Pop"},
                {5, "Science"},
                {6, "Sports"},
                {7, "Rock"},
                {8, "Pop"},
                {9, "Science"},
                {10, "Sports"},
                {11, "Rock"}
            };
            _questionCategoryNameToDeck = new Dictionary<string, List<string>>()
            {
                {"Pop", _popQuestions},
                {"Science", _scienceQuestions},
                {"Sports", _sportsQuestions},
                {"Rock", _rockQuestions}
            };


            _numberPositionsOnBoard = 11;
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
            deck.Add($"{_questionCategoryDeckToName[deck]} Question " + questionIndex);
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
                if (MustGetOutOfPenaltyBox(dieValue))
                {
                    GetOutOfPenaltyBox();
                    DisplayGetOutOfPenaltyBox();
                }
                else
                {
                    StayInPenaltyBox();
                    DisplayStayInPenaltyBox();
                    return;
                }
            }

            PlayNormally(dieValue);

        }

        private void PlayNormally(int dieValue)
        {
            UpdatePosition(dieValue);
            DisplayUpdatedPosition();
            DisplayCurrentCategory();
            AskQuestion();
        }

        private static bool MustGetOutOfPenaltyBox(int dieValue)
        {
            return dieValue % 2 != 0;
        }

        private void StayInPenaltyBox()
        {
            _isGettingOutOfPenaltyBox = false;
        }

        private void DisplayStayInPenaltyBox()
        {
            Console.WriteLine(CurrentPlayerName + " is not getting out of the penalty box");
        }

        private void DisplayCurrentCategory()
        {
            Console.WriteLine("The category is " + GetCurrentCategory());
        }

        private void DisplayUpdatedPosition()
        {
            Console.WriteLine(CurrentPlayerName
                              + "'s new location is "
                              + CurrentPlayerPosition);
        }

        private void UpdatePosition(int dieValue)
        {
            CurrentPlayerPosition += dieValue;
            if (CurrentPlayerPosition > _numberPositionsOnBoard)
                CurrentPlayerPosition -= _numberPositionsOnBoard + 1;
        }

        private void DisplayGetOutOfPenaltyBox()
        {
            Console.WriteLine(CurrentPlayerName + " is getting out of the penalty box");
        }

        private void GetOutOfPenaltyBox()
        {
            _isGettingOutOfPenaltyBox = true;
        }

        private void DisplayCurrentPlayerAndDieValue(int dieValue)
        {
            Console.WriteLine(CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + dieValue);
        }

        private void AskQuestion()
        {
            DisplayQuestion();
            RemoveQuestionFromDeck();
        }

        private void RemoveQuestionFromDeck()
        {
            _questionCategoryNameToDeck[GetCurrentCategory()].RemoveAt(0);
        }

        private void DisplayQuestion()
        {
            Console.WriteLine(_questionCategoryNameToDeck[GetCurrentCategory()].First());
        }


        private string GetCurrentCategory()
        {
            return _positionOnBoardToQuestionCategoryName[CurrentPlayerPosition];
        }

        public void WasCorrectlyAnswered()
        {
            if (CurrentPlayerIsInPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    ProcessCorrectAnswer();
                }
                else
                {
                    MoveToNextPlayer();
                }
            }
            ProcessCorrectAnswer();
        }

        private void ProcessCorrectAnswer()
        {
            DisplayCorrectAnswer();
            AddGoldCoin();
            DisplayCurrentPlayerGoldCoins();
            MoveToNextPlayer();
        }

        private void MoveToNextPlayer()
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex == NumberPlayers)
                _currentPlayerIndex = 0;
        }

        private void DisplayCurrentPlayerGoldCoins()
        {
            Console.WriteLine(CurrentPlayerName
                              + " now has "
                              + CurrentPlayerGoldCoins
                              + " Gold Coins.");
        }

        private void AddGoldCoin()
        {
            CurrentPlayerGoldCoins++;
        }

        private static void DisplayCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
        }

        public void WasIncorrectlyAnswered()
        {
            DisplayIncorrectAnswer();
            DisplaySentToPenaltyBox();
            SendToPenaltyBox();
            MoveToNextPlayer();
        }

        private void SendToPenaltyBox()
        {
            CurrentPlayerIsInPenaltyBox = true;
        }

        private void DisplaySentToPenaltyBox()
        {
            Console.WriteLine(CurrentPlayerName + " was sent to the penalty box");
        }

        private static void DisplayIncorrectAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
        }


        public bool DoesGameContinue()
        {
            return _goldCoinsOfEachPlayer.All(x => x != 6);
        }
    }

}
