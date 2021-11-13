using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Questions
    {
        private const int NumberQuestionsPerDeck = 50;
        private Game _game;
        private List<List<string>> _decksOfQuestions;
        private List<string> _popQuestions;
        private List<string> _rockQuestions;
        private List<string> _scienceQuestions;
        private List<string> _sportsQuestions;
        private Dictionary<List<string>, string> _questionCategoryDeckToName;
        private Dictionary<string, List<string>> _questionCategoryNameToDeck;

        public Questions(Game game)
        {
            PopulateAllDecksWithQuestions();
            _game = game;
        }

        private void PopulateAllDecksWithQuestions()
        {
            for (var questionIndex = 0; questionIndex < NumberQuestionsPerDeck; questionIndex++)
                foreach (var deck in _decksOfQuestions)
                    GenerateQuestion(deck, questionIndex);
        }

        private void GenerateQuestion(List<string> deck, int questionIndex)
        {
            deck.Add($"{_questionCategoryDeckToName[deck]} Question " + questionIndex);
        }

        private void AskQuestion()
        {
            DisplayQuestion();
            RemoveQuestionFromDeck();
        }

        private void RemoveQuestionFromDeck()
        {
            _questionCategoryNameToDeck[_game.GetCurrentCategory()].RemoveAt(0);
        }

        private void DisplayQuestion()
        {
            Console.WriteLine(_questionCategoryNameToDeck[_game.GetCurrentCategory()].First());
        }
    }
}