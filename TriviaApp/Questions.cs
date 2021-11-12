using System.Collections.Generic;

namespace TriviaApp
{
    public class Questions
    {
        public List<string> _popQuestions;
        public List<string> _scienceQuestions;
        public List<string> _rockQuestions;
        public List<string> _sportsQuestions;
        public List<List<string>> _decksOfQuestions;
        public const int NumberQuestionsPerDeck = 50;
        public Dictionary<List<string>, string> _questionCategoryDeckToName;

        public Questions()
        {
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
            _rockQuestions = new List<string>();
            _sportsQuestions = new List<string>();
            _decksOfQuestions = new List<List<string>>
            {_popQuestions, _scienceQuestions, _sportsQuestions,
                _rockQuestions};
            _questionCategoryDeckToName = new Dictionary<List<string>, string>
            {
                {_popQuestions, "Pop"},
                {_scienceQuestions, "Science"},
                {_sportsQuestions, "Sports"},
                {_rockQuestions, "Rock"}
            };

        }

        public void GenerateQuestion(List<string> deck, int questionIndex)
        {
            deck.Add($"{_questionCategoryDeckToName[deck]} Question " + questionIndex);
        }

        public void PopulateAllDecksWithQuestions()
        {
            for (var questionIndex = 0; questionIndex < Questions.NumberQuestionsPerDeck; questionIndex++)
                foreach (var deck in _decksOfQuestions)
                    GenerateQuestion(deck, questionIndex);
        }


    }
}