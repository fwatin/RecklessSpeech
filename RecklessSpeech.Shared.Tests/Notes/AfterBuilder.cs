using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public class AfterBuilder
{
    public AfterBuilder() { }
    public AfterBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "translated sentence from Netflix: \"Et ça n'arrive pas par quelques astuces statistiques.\"" +
                                        "and many explanations from any dictionary";
    

    public static implicit operator After(AfterBuilder builder) => new(builder.Value);
}
