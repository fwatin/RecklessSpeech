using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklessSpeech.Front.WPF.Dtos
{
    public record DictionaryDto(
    Guid Id,
    string Name,
    string FromLanguage,
    string ToLanguage);
}
