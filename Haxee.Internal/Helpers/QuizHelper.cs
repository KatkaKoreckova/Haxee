namespace Haxee.Internal.Helpers
{
    public static class QuizHelper
    {
        public const string SEPARATOR = "---";

        public static string? GetQuestion(string questionAndAnswer)
            => questionAndAnswer.Contains(SEPARATOR) ? questionAndAnswer.Split(SEPARATOR)[0].Trim() : null;

        public static string? GetAnswer(string questionAndAnswer)
            => questionAndAnswer.Contains(SEPARATOR) ? questionAndAnswer.Split(SEPARATOR)[1].Trim() : null;

        public static bool IsAnswerCorrect(string questionAndAnswer, string providedAnswer)
            => providedAnswer.ToLower().Equals(GetAnswer(questionAndAnswer)?.ToLower());

        public static string GetQuestionAnswerPair(string question, string answer)
            => $"{question} {SEPARATOR} {answer}";
    }
}
