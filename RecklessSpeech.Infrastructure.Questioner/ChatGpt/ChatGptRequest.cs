namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public record ChatGptRequest(string model, List<ChatGptMessage> messages);
    //
    // public static class FunctionProvider
    // {
    //     public static void Get()
    //     {
    //         var functions = new[]
    //         {
    //             new
    //             {
    //                 name = "create_cards",
    //                 description = "Crée des cartes de questions-réponses pour Anki",
    //                 parameters = new
    //                 {
    //                     type = "object",
    //                     properties = new
    //                     {
    //                         cards = new
    //                         {
    //                             type = "array",
    //                             items = new
    //                             {
    //                                 type = "object",
    //                                 properties = new
    //                                 {
    //                                     question = new { type = "string" },
    //                                     answer   = new { type = "string" }
    //                                 },
    //                                 required = new[] { "question", "answer" }
    //                             }
    //                         }
    //                     },
    //                     required = new[] { "cards" }
    //                 }
    //             }
    //         };
    //         return functions;
    //     }
    // }
}