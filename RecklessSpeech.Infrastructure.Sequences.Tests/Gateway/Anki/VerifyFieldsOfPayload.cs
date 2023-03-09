using AutoMapper;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.Gateway.Anki
{
    public class VerifyFieldsOfPayload
    {
        [Fact]
        public void Should_NoteDto_and_Fields_Have_same_attributes()
        {
            MapperConfiguration configuration = new(
                cfg =>
                {
                    cfg.CreateMap<Fields, NoteDto>();
                    cfg.CreateMap<NoteDto, Fields>();
                });

            configuration.AssertConfigurationIsValid();
        }
    }
}