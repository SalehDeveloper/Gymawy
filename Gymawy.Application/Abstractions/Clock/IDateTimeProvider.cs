using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Clock
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
