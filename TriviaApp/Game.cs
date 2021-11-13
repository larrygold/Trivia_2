﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Game
    {
        private const int NumberPositionsOnBoard = 11;


        private List<string> _players;

        private int _currentPlayerIndex;

        private int[] _positionOfEachPlayer;

        private int[] _goldCoinsOfEachPlayer;
        
        private bool[] _isInPenaltyBoxForEachPlayer;
        
        private bool _isCurrentPlayerGettingOutOfPenaltyBox;


        private Dictionary<int, string> _positionOnBoardToQuestionCategoryName;

        private readonly Questions _questions;


        public Game()
        {
            InitializeAllFields();
            _questions = new Questions(this);
        }

        private int NumberPlayers => _players.Count;
        private string CurrentPlayerName => _players[_currentPlayerIndex];

        private int CurrentPlayerPosition
        {
            get => _positionOfEachPlayer[_currentPlayerIndex];
            set => _positionOfEachPlayer[_currentPlayerIndex] = value;
        }

        private int CurrentPlayerGoldCoins
        {
            get => _goldCoinsOfEachPlayer[_currentPlayerIndex];
            set => _goldCoinsOfEachPlayer[_currentPlayerIndex] = value;
        }

        private bool CurrentPlayerIsInPenaltyBox
        {
            get => _isInPenaltyBoxForEachPlayer[_currentPlayerIndex];
            set => _isInPenaltyBoxForEachPlayer[_currentPlayerIndex] = value;
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
            DisplayPlayerAdded(playerName);
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
            return _goldCoinsOfEachPlayer.All(x => x != 6);
        }

        private void InitializeAllFields()
        {
            _players = new List<string>();
            _positionOfEachPlayer = new int[6];
            _goldCoinsOfEachPlayer = new int[6];
            _isInPenaltyBoxForEachPlayer = new bool[6];
            _questions._popQuestions = new List<string>();
            _questions._scienceQuestions = new List<string>();
            _questions._sportsQuestions = new List<string>();
            _questions._rockQuestions = new List<string>();
            _questions._decksOfQuestions = new List<List<string>>
                {
                    _questions._popQuestions, _questions._scienceQuestions, _questions._sportsQuestions, _questions._rockQuestions};
            _questions._questionCategoryDeckToName = new Dictionary<List<string>, string>
            {
                {_questions._popQuestions, "Pop"},
                {_questions._scienceQuestions, "Science"},
                {_questions._sportsQuestions, "Sports"},
                {_questions._rockQuestions, "Rock"}
            };
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
            _questions._questionCategoryNameToDeck = new Dictionary<string, List<string>>
            {
                {"Pop", _questions._popQuestions},
                {"Science", _questions._scienceQuestions},
                {"Sports", _questions._sportsQuestions},
                {"Rock", _questions._rockQuestions}
            };
        }

        private void DisplayPlayerAdded(string playerName)
        {
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + NumberPlayers);
        }

        private void PlayNormally(int dieValue)
        {
            UpdatePosition(dieValue);
            DisplayUpdatedPosition();
            DisplayCurrentCategory();
            _questions.AskQuestion();
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
                              + CurrentPlayerPosition);
        }

        private void UpdatePosition(int dieValue)
        {
            CurrentPlayerPosition += dieValue;
            if (CurrentPlayerPosition > NumberPositionsOnBoard)
                CurrentPlayerPosition -= NumberPositionsOnBoard + 1;
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


        private string GetCurrentCategory()
        {
            return _positionOnBoardToQuestionCategoryName[CurrentPlayerPosition];
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