namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using System;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsException : Exception
    {
        public ProjectsException(ProjectsErrors code)
        {
            this.ErrorMessage = new ErrorMessage();
            this.ErrorMessage.Message = "The resource couldn't be found";
            this.StatusCode = (int)ProjectsErrors.NotFound;
            this.Error = new ObjectResult(this.ErrorMessage);
            this.Error.StatusCode = this.StatusCode;
        }

        public ProjectsException(ProjectsErrors code, Exception exception)
        {
            this.ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ProjectsErrors.BadRequest:
                    var validationException = (ValidationException)exception;
                    this.ErrorMessage.Message = "Please review the errors, inconsistent data.";
                    this.ErrorMessage.Errors = validationException.Errors;
                    this.StatusCode = (int)ProjectsErrors.BadRequest;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                case ProjectsErrors.InternalServerError:
                    this.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.StatusCode = (int)ProjectsErrors.InternalServerError;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                case ProjectsErrors.NotFound:
                    this.ErrorMessage.Message = "The resource couldn't be found";
                    this.StatusCode = (int)ProjectsErrors.NotFound;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                default:
                    break;
            }
        }

        public ErrorMessage ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public ObjectResult Error { get; set; }
    }
}
