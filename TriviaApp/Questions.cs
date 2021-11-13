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
            _game = game;
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>
            {
                _popQuestions, _scienceQuestions, _sportsQuestions, _rockQuestions
            };
            _questionCategoryDeckToName = new Dictionary<List<string>, string>
            {
                {_popQuestions, "Pop"},
                {_scienceQuestions, "Science"},
                {_sportsQuestions, "Sports"},
                {_rockQuestions, "Rock"}
            };
            _questionCategoryNameToDeck = new Dictionary<string, List<string>>
            {
                {"Pop", _popQuestions},
                {"Science", _scienceQuestions},
                {"Sports", _sportsQuestions},
                {"Rock", _rockQuestions}
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