namespace Haxee.Internal.Helpers
{
    public static class QuizHelper
    {
        public static string? GetQuestion(string questionAndAnswer)
            => questionAndAnswer.Contains(Constants.QA_SEPARATOR) ? questionAndAnswer.Split(Constants.QA_SEPARATOR)[0].Trim() : null;

        public static string? GetAnswer(string questionAndAnswer)
            => questionAndAnswer.Contains(Constants.QA_SEPARATOR) ? questionAndAnswer.Split(Constants.QA_SEPARATOR)[1].Trim() : null;

        public static bool IsAnswerCorrect(string questionAndAnswer, string providedAnswer)
            => providedAnswer.ToLower().Equals(GetAnswer(questionAndAnswer)?.ToLower());

        public static string GetQuestionAnswerPair(string question, string answer)
            => $"{question} {Constants.QA_SEPARATOR} {answer}";
    }
}
