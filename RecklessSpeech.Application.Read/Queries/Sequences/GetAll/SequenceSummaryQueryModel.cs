namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

public record SequenceSummaryQueryModel(Guid Id, string HtmlContent, string AudioFileNameWithExtension, string Tags);