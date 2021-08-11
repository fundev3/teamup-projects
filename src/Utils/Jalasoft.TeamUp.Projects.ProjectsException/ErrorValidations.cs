namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using System.Collections.Generic;

    public class ErrorValidations : ErrorMessage
    {
        public ErrorValidations()
        {
            this.Errors = new List<ErrorDAO>();
        }

        public List<ErrorDAO> Errors { get; set; }
    }
}
