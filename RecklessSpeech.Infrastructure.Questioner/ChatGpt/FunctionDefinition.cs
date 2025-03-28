using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecklessSpeech.Infrastructure.Questioner.ChatGpt
{
    public class FunctionDefinition
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        // On laisse "parameters" sous forme de JsonElement pour ne pas avoir
        // à tout redéfinir en classes C# (on va juste le passer tel quel à OpenAI)
        [JsonPropertyName("parameters")]
        public JsonElement Parameters { get; set; }
    }
}