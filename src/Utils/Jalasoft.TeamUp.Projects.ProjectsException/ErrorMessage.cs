namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using FluentValidation;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ErrorMessage
    {
        public IEnumerable<FluentValidation.Results.ValidationFailure> Errors { get; set; }

        public string Message { get; set; }
    }
}
