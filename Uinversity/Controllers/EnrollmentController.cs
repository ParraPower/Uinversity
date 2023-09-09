using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using University.Commands.Enrollment;

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

        [HttpGet("{enrollmentID}", Name = "enrollmentID")]
        [ProducesResponseType(typeof(Models.Facade.Enrollment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetEnrollmentResponse>> GetEnrollment(int enrollmentID)
        {
            var request = new GetEnrollmentQuery(enrollmentID);
            var enrollment = await _mediator.Send(request);
            return Ok(enrollment);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.Facade.Enrollment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateEnrollmentResponse>> CreateEnrollment([FromBody] Models.Facade.Enrollment enrollment)
        {
            var request = new CreateEnrollmentQuery(enrollment);
            var updatedeEnrollment = await _mediator.Send(request);
            return Ok(updatedeEnrollment);
        }
    }
}
