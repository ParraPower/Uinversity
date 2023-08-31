using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using University.Commands.Cart;

namespace University.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EnrollmentController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.Facade.Enrollment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEnrollmentResponse>> CreateEnrollment([FromBody] Models.Facade.Enrollment enrollment)
        {
            var request = new CreateEnrollmentQuery(enrollment);
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
