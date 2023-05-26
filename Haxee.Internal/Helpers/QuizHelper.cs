namespace Haxee.Internal.Helpers
{
    /// <summary>
    /// Trieda obsahujúce pomocné funkcie na prácu s kvízovými otázkami.
    /// </summary>
    public static class QuizHelper
    {
        /// <summary>
        /// Funkcia na získanie otázky z reťazca obsahujúceho otázku aj odpoveď.
        /// </summary>
        /// <returns>Otázka</returns>
        public static string? GetQuestion(string questionAndAnswer)
            => questionAndAnswer.Contains(Constants.QA_SEPARATOR) ? questionAndAnswer.Split(Constants.QA_SEPARATOR)[0].Trim() : null;

        /// <summary>
        /// Funkcia na získanie odpoveďe z reťazca obsahujúceho otázku aj odpoveď.
        /// </summary>
        /// <returns>Odpoveď</returns>
        public static string? GetAnswer(string questionAndAnswer)
            => questionAndAnswer.Contains(Constants.QA_SEPARATOR) ? questionAndAnswer.Split(Constants.QA_SEPARATOR)[1].Trim() : null;

        /// <summary>
        /// Funkcia na overenie správnosti odpovede.
        /// </summary>
        /// <returns>True / false podľa toho či odpoveď je správna</returns>
        public static bool IsAnswerCorrect(string questionAndAnswer, string providedAnswer)
            => providedAnswer.ToLower().Equals(GetAnswer(questionAndAnswer)?.ToLower());

        /// <summary>
        /// Funkcia ktorá z dvoch reťazcov obsahujúcich otázku a odpoveď vytvorí jeden rozdelený vopred definovaným znakom . <see cref="Constants.QA_SEPARATOR">Zobraziť znak</see>
        /// </summary>
        /// <returns>Reťazec obsahujúci otázku a odpoveď.</returns>
        public static string GetQuestionAnswerPair(string question, string answer)
            => $"{question} {Constants.QA_SEPARATOR} {answer}";
    }
}
