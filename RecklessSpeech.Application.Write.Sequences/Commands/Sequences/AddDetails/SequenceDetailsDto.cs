#pragma warning disable CS8618
namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails;
public class Class1
{
    public string itemType { get; set; }
    public string langCode_G { get; set; }
    public Context? context { get; set; }
    public string[] tags { get; set; }
    public string learningStage { get; set; }
    public string[] wordTranslationsArr { get; set; }
    public string translationLangCode_G { get; set; }
    public string wordType { get; set; }
    public Word word { get; set; }
    public long timeModified_ms { get; set; }
    public Audio audio { get; set; }
    public int? freqRank { get; set; }
}

public class Context
{
    public int wordIndex { get; set; }
    public Phrase? phrase { get; set; }
}

public class Phrase
{
    public Dictionary<string,Subtitletokens[]> subtitleTokens { get; set; }
    public Dictionary<string,string> subtitles { get; set; }
    public Mtranslations mTranslations { get; set; }
    public Htranslations hTranslations { get; set; }
    public Reference reference { get; set; }
    public object thumb_prev { get; set; }
    public object thumb_next { get; set; }
}

public class Subtitletokens
{
    public string pos { get; set; }
    public Form form { get; set; }
    public int freq { get; set; }
    public string xpos { get; set; }
    public Feats feats { get; set; }
    public Lemma lemma { get; set; }
    public string deprel { get; set; }
    public int? pointer { get; set; }
    public object diocoFreq { get; set; }
    public Form_Norm form_norm { get; set; }
}

public class Form
{
    public string text { get; set; }
}

public class Feats
{
    public string Poss { get; set; }
    public string Person { get; set; }
    public string PronType { get; set; }
    public string Tense { get; set; }
    public string Number { get; set; }
    public string VerbForm { get; set; }
    public string Foreign { get; set; }
    public string Case { get; set; }
    public string Gender { get; set; }
}

public class Lemma
{
    public string text { get; set; }
}

public class Form_Norm
{
    public string text { get; set; }
}

public class Mtranslations
{
    public string _0 { get; set; }
    public string _1 { get; set; }
    public string _2 { get; set; }
}

public class Htranslations
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