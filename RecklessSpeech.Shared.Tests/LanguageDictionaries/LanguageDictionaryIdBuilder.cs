using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.LanguageDictionaries
{
    public class LanguageDictionaryIdBuilder
    {
        public LanguageDictionaryIdBuilder(Guid value)
        {
            this.Value = value;
        }
        
        public LanguageDictionaryIdBuilder()
        {
            this.Value = Guid.NewGuid();
        }

        public Guid Value { get; }

        public static implicit operator LanguageDictionaryId?(LanguageDictionaryIdBuilder? builder)
        {
            if (builder is null)
            {
                return null;
            }

            return new(builder.Value);
        }
    }
}