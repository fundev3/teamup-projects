namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using System.Collections.Generic;
    using FluentValidation;

    public class ErrorMessage
    {
        public IEnumerable<FluentValidation.Results.ValidationFailure> Errors { get; set; }

        public string Message { get; set; }
    }
}
