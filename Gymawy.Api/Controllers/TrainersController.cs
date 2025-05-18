using MediatR;

namespace Gymawy.Api.Controllers
{
    public class TrainersController : ApiController
    {

        private readonly ISender _sender;

        public TrainersController(ISender sender)
        {
            _sender = sender;
        }



    }
}
