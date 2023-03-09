using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecklessSpeech.Web.Configuration
{
    public static class ApiErrors
    {
        internal const string ReadSequenceNotFound = "Read_Sequence_NotFound";
        internal const string GenericInternalServerError = "Generic_InternalServerError";

        internal static IEnumerable<string> Errors() => typeof(ApiErrors)
#pragma warning disable S3011
            .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
#pragma warning restore S3011
            .Select(x => x.GetValue(null))
            .Cast<string>()
            .ToArray();
    }
}