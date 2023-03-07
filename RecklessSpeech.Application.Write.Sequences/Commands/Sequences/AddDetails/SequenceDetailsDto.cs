namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails
{
    public class SequenceDetailsDto
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string itemType { get; set; }
        public string langCode_G { get; set; }
        public Context context { get; set; }
        public string[] tags { get; set; }
        public string learningStage { get; set; }
        public string[] wordTranslationsArr { get; set; }
        public string translationLangCode_G { get; set; }
        public string wordType { get; set; }
        public Word word { get; set; }
        public long timeModified_ms { get; set; }
        public Audio audio { get; set; }
        public int freqRank { get; set; }
    }

    public class Context
    {
        public int wordIndex { get; set; }
        public Phrase phrase { get; set; }
    }

    public class Phrase
    {
        public Subtitletokens subtitleTokens { get; set; }
        public Subtitles subtitles { get; set; }
        public Mtranslations mTranslations { get; set; }
        public object hTranslations { get; set; }
        public Reference reference { get; set; }
        public Thumb_Prev thumb_prev { get; set; }
        public Thumb_Next thumb_next { get; set; }
    }

    public class Subtitletokens
    {
        public _0[] _0 { get; set; }
        public _1[] _1 { get; set; }
        public _2[] _2 { get; set; }
    }

    public class _0
    {
        public string pos { get; set; }
        public Form form { get; set; }
        public string xpos { get; set; }
        public Feats feats { get; set; }
        public Lemma lemma { get; set; }
        public string deprel { get; set; }
        public int? pointer { get; set; }
        public object diocoFreq { get; set; }
        public Form_Norm form_norm { get; set; }
        public int freq { get; set; }
    }

    public class Form
    {
        public string text { get; set; }
    }

    public class Feats
    {
        public string Gender { get; set; }
        public string Number { get; set; }
    }

    public class Lemma
    {
        public string text { get; set; }
    }

    public class Form_Norm
    {
        public string text { get; set; }
    }

    public class _1
    {
        public string pos { get; set; }
        public Form1 form { get; set; }
        public string xpos { get; set; }
        public Feats1 feats { get; set; }
        public Lemma1 lemma { get; set; }
        public string deprel { get; set; }
        public int? pointer { get; set; }
        public object diocoFreq { get; set; }
        public Form_Norm1 form_norm { get; set; }
        public int freq { get; set; }
    }

    public class Form1
    {
        public string text { get; set; }
    }

    public class Feats1
    {
        public string Case { get; set; }
        public string Person { get; set; }
        public string PronType { get; set; }
        public string Tense { get; set; }
        public string Number { get; set; }
        public string VerbForm { get; set; }
        public string Gender { get; set; }
    }

    public class Lemma1
    {
        public string text { get; set; }
    }

    public class Form_Norm1
    {
        public string text { get; set; }
    }

    public class _2
    {
        public string pos { get; set; }
        public Form2 form { get; set; }
        public string xpos { get; set; }
        public Feats2 feats { get; set; }
        public Lemma2 lemma { get; set; }
        public string deprel { get; set; }
        public int? pointer { get; set; }
        public object diocoFreq { get; set; }
        public Form_Norm2 form_norm { get; set; }
        public int freq { get; set; }
    }

    public class Form2
    {
        public string text { get; set; }
    }

    public class Feats2
    {
        public string Gender { get; set; }
        public string Number { get; set; }
        public string Person { get; set; }
        public string PronType { get; set; }
        public string Tense { get; set; }
        public string VerbForm { get; set; }
        public string Degree { get; set; }
    }

    public class Lemma2
    {
        public string text { get; set; }
    }

    public class Form_Norm2
    {
        public string text { get; set; }
    }

    public class Subtitles
    {
        public string _0 { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
    }

    public class Mtranslations
    {
        public string _0 { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
    }

    public class Reference
    {
        public string source { get; set; }
        public string movieId { get; set; }
        public string langCode_N { get; set; }
        public string langCode_G { get; set; }
        public string title { get; set; }
        public int subtitleIndex { get; set; }
        public int numSubs { get; set; }
        public int startTime_ms { get; set; }
        public int endTime_ms { get; set; }
    }

    public class Thumb_Prev
    {
        public int time { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string dataURL { get; set; }
        public int pixelAspectWidth { get; set; }
        public int pixelAspectHeight { get; set; }
    }

    public class Thumb_Next
    {
        public int time { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string dataURL { get; set; }
        public int pixelAspectWidth { get; set; }
        public int pixelAspectHeight { get; set; }
    }

    public class Word
    {
        public string text { get; set; }
    }

    public class Audio
    {
        public string voice { get; set; }
        public string source { get; set; }
        public string dataURL { get; set; }
        public long dateCreated { get; set; }
        public string outputFormat { get; set; }
    }
}