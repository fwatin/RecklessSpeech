﻿using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public record SequenceBuilder
{
    public SequenceId SequenceId { get; init; }
    public HtmlContentBuilder HtmlContent { get; init; }
    public AudioFileNameWithExtensionBuilder AudioFileNameWithExtension { get; init; }
    public TagsBuilder Tags { get; init; }

    public WordBuilder Word { get; init; }

    public string? RawCsvContent { get; init; }


    private SequenceBuilder(
        SequenceIdBuilder sequenceId,
        HtmlContentBuilder htmlContent,
        AudioFileNameWithExtensionBuilder audioFileNameWithExtension,
        TagsBuilder tags,
        WordBuilder word)
    {
        this.SequenceId = sequenceId;
        this.HtmlContent = htmlContent;
        this.AudioFileNameWithExtension = audioFileNameWithExtension;
        this.Tags = tags;
        this.Word = word;
        this.RawCsvContent = null;
    }


    public SequencesImportRequestedEvent BuildEvent() =>
        new(this.SequenceId, this.HtmlContent, this.AudioFileNameWithExtension, this.Tags, this.Word);

    public static SequenceBuilder Create(Guid id)
    {
        return new SequenceBuilder(
            new(id),
            new(),
            new(),
            new(),
            new());
    }

    public SequenceEntity BuildEntity()
    {
        return new()
        {
            Id = this.SequenceId.Value,
            AudioFileNameWithExtension = this.AudioFileNameWithExtension.Value,
            Tags = this.Tags.Value,
            HtmlContent = this.HtmlContent.Value
        };
    }

    public SequenceSummaryQueryModel BuildQueryModel()
    {
        return new SequenceSummaryQueryModel(
            this.SequenceId.Value,
            this.HtmlContent.Value,
            this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }

    public SequenceSummaryPresentation BuildSummaryPresentation()
    {
        return new SequenceSummaryPresentation(
            this.SequenceId.Value,
            this.HtmlContent.Value,
            this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }

    public ImportSequencesCommand BuildImportCommand()
    {
        return new ImportSequencesCommand(this.RawCsvContent ?? DefaultExampleFromMoneyBall());
    }

    private string DefaultExampleFromMoneyBall() =>
        "\"<style>\n\n    html,\n    body {\n        padding: 0;\n        margin: 0;\n    }\n\n    .card {\n        " +
        "background: rgb(255,243,248);\n        background: linear-gradient(76deg, rgba(255,243,248,1) 0%, " +
        "rgba(238,246,255,1) 100%);\n    }\n\n    .nightMode.card {\n        background: black;\n    }\n    \n    " +
        ".dc-card {\n        background-color: white;\n        padding-bottom: 1rem;\n        " +
        "border-bottom: 0.5px solid grey;\n    }\n\n    .nightMode .dc-card {\n        background: #333;\n        " +
        "border-bottom: none;\n    }\n\n    .nightMode .cloze {\n        color: #1569C7;\n    }\n\n    .dc-title {\n" +
        "        color: rgb(127, 127, 127);\n        font-size: 0.8rem;\n        text-align: center;\n        " +
        "padding-top: 0.65rem;\n        margin-bottom: 0.65rem;\n    }\n\n    .dc-images {\n        " +
        "text-align: center;\n        max-width: 450px;\n        margin: 0 auto 0.5rem;\n    }\n\n    " +
        ".dc-image {\n       margin-left: 2px;\n       margin-right: 2px;\n        display: inline-block;\n        " +
        "width: calc(50% - 10px);\n        padding-bottom: 29%;\n        background-position: center;\n        " +
        "background-repeat: no-repeat;\n        background-size: cover;\n    }\n\n    .dc-line {\n        " +
        "padding: 0.4rem;\n    }\n\n    .dc-up {\n        display: block;\n        font-size: 0.75rem;\n    }\n\n    " +
        ".dc-translation {\n        color: fuchsia;\n    }\n\n    .dc-layer {\n        display: inline-block;\n        " +
        "text-align: center;\n        line-height: 1.3rem;\n    }\n\n    .dc-lang-zh-CN.dc-orig {\n        " +
        "font-size: 1.2rem;\n    }\n    .dc-lang-zh-TW.dc-orig {\n        font-size: 1.2rem;\n    }\n    " +
        ".dc-lang-ja.dc-orig {\n        font-size: 1.2rem;\n    }\n    .dc-lang-th.dc-orig {\n        " +
        "font-size: 1.2rem;\n    }\n    .dc-lang-ar.dc-orig {\n        font-size: 1.2rem;\n    }\n    " +
        ".dc-lang-fa.dc-orig {\n        font-size: 1.2rem;\n    }\n    .dc-lang-ur.dc-orig {\n        " +
        "font-size: 1.2rem;\n    }\n</style><div class=\"\"dc-bg\"\"><div class=\"\"dc-card dc-lang-en dc-word-naked\"\">" +
        "<div class=\"\"dc-title\"\">Moneyball</div><div class=\"\"dc-images\"\">" +
        "<div class=\"\"dc-image dc-image-prev\"\" style=\"\"background-image: url(1653367161565_prev.jpg)\"\">" +
        "</div><div class=\"\"dc-image dc-image-next\"\" style=\"\"background-image: url(1653367161565_next.jpg)\"\">" +
        "</div></div><div class=\"\"dc-line\"\"><span class=\"\"dc-down dc-lang-en dc-orig\"\">And</span>" +
        "<span class=\"\"dc-down dc-lang-en dc-orig\"\"> </span><span class=\"\"dc-down dc-lang-en dc-orig\"\">you</span>" +
        "<span class=\"\"dc-down dc-lang-en dc-orig\"\"> </span><span class=\"\"dc-down dc-lang-en dc-orig\"\">do</span>" +
        "<span class=\"\"dc-down dc-lang-en dc-orig\"\">n't</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">do</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">that</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">with</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">a</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">bunch</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">of</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">statistical</span><span class=\"\"dc-down dc-lang-en dc-orig\"\"> " +
        "</span><span class=\"\"dc-gap\"\"><span class=\"\"dc-down dc-lang-en dc-orig\"\">gimmicks</span>" +
        "</span><span class=\"\"dc-down dc-lang-en dc-orig\"\">.</span></div>" +
        "<div class=\"\"dc-line dc-translation dc-lang-fr\"\">Et ça n'arrive pas par quelques astuces statistiques.</div></div></div>" +
        "\"	[sound:" + this.AudioFileNameWithExtension.Value + "]	\"word-naked lang-nl netflix Green pron \"";
}