using System.Collections.Generic;
using System.Linq;

namespace TriviaApp
{
    public class Questions
    {
        public const int NumberQuestionsPerDeck = 50;
        private readonly List<List<string>> _decksOfQuestions;
        private readonly List<string> _popQuestions;
        private readonly Dictionary<List<string>, string> _questionCategoryDeckToName;
        private readonly Dictionary<string, List<string>> _questionCategoryNameToDeck;
        private readonly List<string> _rockQuestions;
        private readonly List<string> _scienceQuestions;
        private readonly List<string> _sportsQuestions;

        public Questions()
        {
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>
            {
                _popQuestions, _scienceQuestions, _sportsQuestions,
                _rockQuestions
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

        public string GetNextQuestion(string currentCategory)
        {
            var questionToDisplay = _questionCategoryNameToDeck[currentCategory].First();
            RemoveQuestionFromDeck(currentCategory);
            return questionToDisplay;
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

        private void RemoveQuestionFromDeck(string currentCategory)
        {
            _questionCategoryNameToDeck[currentCategory].RemoveAt(0);
        }
    }
}