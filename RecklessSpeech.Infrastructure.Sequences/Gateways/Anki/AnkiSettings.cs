﻿using System.ComponentModel.DataAnnotations;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki
{
    public class AnkiSettings
    {
        [Required] public string Url { get; set; }

        [Required] public string MediaPath { get; set; }
    }
}