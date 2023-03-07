﻿using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Shared.Tests.Explanations;

public record ExplanationBuilder
{
    public ExplanationIdBuilder ExplanationId { get; init; }
    public ContentBuilder Content { get; init; }
    public TargetBuilder Target { get; init; }
    public SourceUrlBuilder SourceUrl { get; init; }

    private ExplanationBuilder(
        ExplanationIdBuilder explanationId,
        ContentBuilder content,
        TargetBuilder target,
        SourceUrlBuilder sourceUrl
    )
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Target = target;
        this.SourceUrl = sourceUrl;
    }

    public static ExplanationBuilder Create()
    {
        return new(
            new(Guid.Parse("644320B4-4FAD-4039-86DF-92EAB2987F6E")),
            new(),
            new(),
            new());
    }

    public static ExplanationBuilder Create(Guid id)
    {
        return new(
            new(id),
            new(),
            new(),
            new());
    }

    public static implicit operator Explanation(ExplanationBuilder builder) =>
        Explanation.Create(
            builder.ExplanationId.Value,
            builder.Content.Value,
            builder.Target.Value,
            builder.SourceUrl.Value);

    public ExplanationEntity BuildEntity()
    {
        return new()
        {
            Id = this.ExplanationId.Value,
            Content = this.Content.Value,
            Target = this.Target.Value,
            SourceUrl = this.SourceUrl.Value
        };
    }

    public Explanation BuildDomain()
    {
        return Explanation.Hydrate(
            this.ExplanationId.Value, 
            this.Content.Value, 
            this.Target.Value,
            this.SourceUrl.Value);
    }
}