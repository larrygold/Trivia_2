using System.Collections.Generic;

namespace TriviaApp
{
    public class Questions
    {
        public List<string> _popQuestions;
        public List<string> _scienceQuestions;

        public Questions()
        {
            _popQuestions = new List<string>();
            _scienceQuestions = new List<string>();
        }

    }
}