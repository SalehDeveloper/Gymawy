using ErrorOr;
using Gymawy.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Gyms.Commands.CreateGym
{
    public record CreateGymCommand(
        string Name
        ) : IRequest<ErrorOr<Gym>>;


}
