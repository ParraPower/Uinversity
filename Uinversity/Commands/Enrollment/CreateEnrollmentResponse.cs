using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Commands.Cart
{
    public record class CreateEnrollmentResponse(int EnrollmentId, int CourseId, int StudentId);
}
