﻿using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class WordBuilder
    {
        public WordBuilder() { } //todo mettre privé et remplacer par Create

        public WordBuilder(string value) => this.Value = value;

        public string Value { get; set; } = "gimmicks";

        public static implicit operator Word(WordBuilder builder) => new(builder.Value);
    }
}