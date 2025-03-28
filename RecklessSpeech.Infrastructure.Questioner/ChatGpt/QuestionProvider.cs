using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public static class QuestionProvider
    {
        private static string CreateQuestionFullText(IReadOnlyCollection<Note> relatedNotes, Completion commandCompletion) =>
            "Dans le texte suivant, peux tu écrire une liste de question/réponse " +
            "qui seraient des candidats pour des fiches Anki ?" +
            "Génère un ensemble de questions/réponses en format JSON :" +
            "{\n  \"cards\": [\n    {\n      \"question\": \"…\",\n      \"answer\": \"…\"\n    },\n    …\n  ]\n}\n" +
            "N’inclus aucun texte hors de la structure JSON." +
            "---" +
            "Le texte est le suivant : " + commandCompletion.Value +
            "---" +
            "Voici des fiches extraites de Anki déjà existantes pour que tu puisses t'en inspirer " +
            "mais je ne les veux pas dans ma réponse car elles sont déjà dans mon Anki." +
            string.Join("\n", relatedNotes.Select(n => n.Slimmed))+
            "---";

        public static string CreateQuestionFunctionCall(string subject, Completion commandCompletion)
        {
            return @"Voici un texte :" + commandCompletion.Value +
                $"Produit 5 questions/réponses au format JSON,en utilisant la fonction create_cards, sur le thème {subject}";
        }
    }
}