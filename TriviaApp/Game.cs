using System;
using System.Collections.Generic;
using System.Linq;
using TriviaApp;

namespace UglyTrivia
{
    public class Game
    {
        private Players _players;

        private const int NumberPositionsOnBoard = 11;

        private int _currentPlayerIndex;

        private bool _isCurrentPlayerGettingOutOfPenaltyBox;

        private Questions _questions;

        private Dictionary<int, string> _positionOnBoardToQuestionCategoryName;


        public Game(Players players)
        {
            _players = players;
            InitializeAllFields();
        }

        private string CurrentPlayerName => _players._players[_currentPlayerIndex];

        private bool CurrentPlayerIsInPenaltyBox
        {
            get => _players._isInPenaltyBoxForEachPlayer[_currentPlayerIndex];
            set => _players._isInPenaltyBoxForEachPlayer[_currentPlayerIndex] = value;
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

        public void WasCorrectlyAnswered()
        {
            if (CurrentPlayerStaysInPenaltyBox())
            {
                MoveToNextPlayer();
                return;
            }

            ProcessCorrectAnswer();
        }

        public void WasIncorrectlyAnswered()
        {
            ProcessIncorrectAnswer();
        }

        public bool DoesGameContinue()
        {
            return _players.DoesAPlayerHave(6);
        }

        private void InitializeAllFields()
        {
            _questions = new Questions();
            _positionOnBoardToQuestionCategoryName = new Dictionary<int, string>
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
            _isCurrentPlayerGettingOutOfPenaltyBox = false;
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
                              + _players.GetPlace(_currentPlayerIndex));
        }

        private void UpdatePosition(int dieValue)
        {
            int val = _players.GetPlace(_currentPlayerIndex) + dieValue;
            _players.AddToPlace(_currentPlayerIndex, val - _players.GetPlace(_currentPlayerIndex));
            if (_players.GetPlace(_currentPlayerIndex) > NumberPositionsOnBoard)
            {
                int val1 = _players.GetPlace(_currentPlayerIndex) - (NumberPositionsOnBoard + 1);
                _players.AddToPlace(_currentPlayerIndex, val1 - _players.GetPlace(_currentPlayerIndex));
            }
        }

        private void DisplayGetOutOfPenaltyBox()
        {
            Console.WriteLine(CurrentPlayerName + " is getting out of the penalty box");
        }

        private void GetOutOfPenaltyBox()
        {
            _isCurrentPlayerGettingOutOfPenaltyBox = true;
        }

        private void DisplayCurrentPlayerAndDieValue(int dieValue)
        {
            Console.WriteLine(CurrentPlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + dieValue);
        }

        private void AskQuestion()
        {
            Console.WriteLine(_questions.GetNextQuestion(GetCurrentCategory()));
        }

        private string GetCurrentCategory()
        {
            return _positionOnBoardToQuestionCategoryName[_players.GetPlace(_currentPlayerIndex)];
        }

        private void ProcessIncorrectAnswer()
        {
            DisplayIncorrectAnswer();
            DisplaySentToPenaltyBox();
            SendToPenaltyBox();
            MoveToNextPlayer();
        }

        private bool CurrentPlayerStaysInPenaltyBox()
        {
            return CurrentPlayerIsInPenaltyBox && !_isCurrentPlayerGettingOutOfPenaltyBox;
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
            if (_currentPlayerIndex == _players.NumberPlayers)
                _currentPlayerIndex = 0;
        }

        private void DisplayCurrentPlayerGoldCoins()
        {
            Console.WriteLine(CurrentPlayerName
                              + " now has "
                              + _players.GetGoldCoins(_currentPlayerIndex)
                              + " Gold Coins.");
        }

        private void AddGoldCoin()
        {
            _players.AddToPurse(_currentPlayerIndex, 1);
        }

        private static void DisplayCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
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
    }
}