using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Questions
    {
        private const int NumberQuestionsPerDeck = 50;
        private readonly List<List<string>> _decksOfQuestions;
        private readonly Dictionary<List<string>, string> _questionCategoryDeckToName;
        private readonly Dictionary<string, List<string>> _questionCategoryNameToDeck;

        public Questions()
        {
            var popQuestions = new List<string>();
            var scienceQuestions = new List<string>();
            var sportsQuestions = new List<string>();
            var rockQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>
            {
                popQuestions, scienceQuestions, sportsQuestions, rockQuestions
            };
            _questionCategoryDeckToName = new Dictionary<List<string>, string>
            {
                {popQuestions, "Pop"},
                {scienceQuestions, "Science"},
                {sportsQuestions, "Sports"},
                {rockQuestions, "Rock"}
            };
            _questionCategoryNameToDeck = new Dictionary<string, List<string>>
            {
                {"Pop", popQuestions},
                {"Science", scienceQuestions},
                {"Sports", sportsQuestions},
                {"Rock", rockQuestions}
            };
            PopulateAllDecksWithQuestions();
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

        public void AskQuestion(string currentCategory)
        {
            DisplayQuestion(currentCategory);
            RemoveQuestionFromDeck(currentCategory);
        }

        private void RemoveQuestionFromDeck(string currentCategory)
        {
            _questionCategoryNameToDeck[currentCategory].RemoveAt(0);
        }

        private void DisplayQuestion(string currentCategory)
        {
            Console.WriteLine(_questionCategoryNameToDeck[currentCategory].First());
        }
    }
}