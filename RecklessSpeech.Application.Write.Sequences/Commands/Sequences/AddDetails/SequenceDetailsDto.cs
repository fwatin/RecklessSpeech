namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails
{
    public class SequenceDetailsDto
    {
        public Class1[] Property1 { get; init; }
    }

    public class Class1
    {
        public string itemType { get; init; }
        public string langCode_G { get; init; }
        public Context context { get; init; }
        public string[] tags { get; init; }
        public string learningStage { get; init; }
        public string[] wordTranslationsArr { get; init; }
        public string translationLangCode_G { get; init; }
        public string wordType { get; init; }
        public Word word { get; init; }
        public long timeModified_ms { get; init; }
        public Audio audio { get; init; }
        public int freqRank { get; init; }
    }

    public class Context
    {
        public int wordIndex { get; init; }
        public Phrase phrase { get; init; }
    }

    public class Phrase
    {
        public Subtitletokens subtitleTokens { get; init; }
        public Subtitles subtitles { get; init; }
        public Mtranslations mTranslations { get; init; }
        public object hTranslations { get; init; }
        public Reference reference { get; init; }
        public Thumb_Prev thumb_prev { get; init; }
        public Thumb_Next thumb_next { get; init; }
    }

    public class Subtitletokens
    {
        public _0[] _0 { get; init; }
        public _1[] _1 { get; init; }
        public _2[] _2 { get; init; }
    }

    public class _0
    {
        public string pos { get; init; }
        public Form form { get; init; }
        public string xpos { get; init; }
        public Feats feats { get; init; }
        public Lemma lemma { get; init; }
        public string deprel { get; init; }
        public int? pointer { get; init; }
        public object diocoFreq { get; init; }
        public Form_Norm form_norm { get; init; }
        public int freq { get; init; }
    }

    public class Form
    {
        public string text { get; init; }
    }

    public class Feats
    {
        public string Gender { get; init; }
        public string Number { get; init; }
    }

    public class Lemma
    {
        public string text { get; init; }
    }

    public class Form_Norm
    {
        public string text { get; init; }
    }

    public class _1
    {
        public string pos { get; init; }
        public Form1 form { get; init; }
        public string xpos { get; init; }
        public Feats1 feats { get; init; }
        public Lemma1 lemma { get; init; }
        public string deprel { get; init; }
        public int? pointer { get; init; }
        public object diocoFreq { get; init; }
        public Form_Norm1 form_norm { get; init; }
        public int freq { get; init; }
    }

    public class Form1
    {
        public string text { get; init; }
    }

    public class Feats1
    {
        public string Case { get; init; }
        public string Person { get; init; }
        public string PronType { get; init; }
        public string Tense { get; init; }
        public string Number { get; init; }
        public string VerbForm { get; init; }
        public string Gender { get; init; }
    }

    public class Lemma1
    {
        public string text { get; init; }
    }

    public class Form_Norm1
    {
        public string text { get; init; }
    }

    public class _2
    {
        public string pos { get; init; }
        public Form2 form { get; init; }
        public string xpos { get; init; }
        public Feats2 feats { get; init; }
        public Lemma2 lemma { get; init; }
        public string deprel { get; init; }
        public int? pointer { get; init; }
        public object diocoFreq { get; init; }
        public Form_Norm2 form_norm { get; init; }
        public int freq { get; init; }
    }

    public class Form2
    {
        public string text { get; init; }
    }

    public class Feats2
    {
        public string Gender { get; init; }
        public string Number { get; init; }
        public string Person { get; init; }
        public string PronType { get; init; }
        public string Tense { get; init; }
        public string VerbForm { get; init; }
        public string Degree { get; init; }
    }

    public class Lemma2
    {
        public string text { get; init; }
    }

    public class Form_Norm2
    {
        public string text { get; init; }
    }

    public class Subtitles
    {
        public string _0 { get; init; }
        public string _1 { get; init; }
        public string _2 { get; init; }
    }

    public class Mtranslations
    {
        public string _0 { get; init; }
        public string _1 { get; init; }
        public string _2 { get; init; }
    }

    public class Reference
    {
        public string source { get; init; }
        public string movieId { get; init; }
        public string langCode_N { get; init; }
        public string langCode_G { get; init; }
        public string title { get; init; }
        public int subtitleIndex { get; init; }
        public int numSubs { get; init; }
        public int startTime_ms { get; init; }
        public int endTime_ms { get; init; }
    }

    public class Thumb_Prev
    {
        public int time { get; init; }
        public int width { get; init; }
        public int height { get; init; }
        public string dataURL { get; init; }
        public int pixelAspectWidth { get; init; }
        public int pixelAspectHeight { get; init; }
    }

    public class Thumb_Next
    {
        public int time { get; init; }
        public int width { get; init; }
        public int height { get; init; }
        public string dataURL { get; init; }
        public int pixelAspectWidth { get; init; }
        public int pixelAspectHeight { get; init; }
    }

    public class Word
    {
        public string text { get; init; }
    }

    public class Audio
    {
        public string voice { get; init; }
        public string source { get; init; }
        public string dataURL { get; init; }
        public long dateCreated { get; init; }
        public string outputFormat { get; init; }
    }
}