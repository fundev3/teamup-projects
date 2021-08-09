namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using System;
    using FluentValidation;

    public class ProjectsException : Exception
    {
        public enum ProjectsErrors
        {
            InternalServerError = 500,
            BadRequest = 400,
            NotFound = 404,
        }

        public ErrorMessage ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public ProjectsException(ProjectsErrors code)
        {
            this.ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ProjectsErrors.InternalServerError:
                    this.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.StatusCode = (int)ProjectsErrors.InternalServerError;
                    break;
                case ProjectsErrors.NotFound:
                    this.ErrorMessage.Message = "The resource couldn't be found";
                    this.StatusCode = (int)ProjectsErrors.NotFound;
                    break;
                default:
                    break;
            }
        }

        public ProjectsException(ProjectsErrors code, ValidationException validationException)
        {
            this.ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ProjectsErrors.BadRequest:
                    this.ErrorMessage.Message = "Please review the errors, inconsistent data.";
                    this.ErrorMessage.Errors = validationException.Errors;
                    this.StatusCode = (int)ProjectsErrors.BadRequest;
                    break;
            }
        }
    }
}
