namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using FluentValidation;
    using System;

    public class ProjectsException : Exception
    {
        public ErrorMessage _ErrorMessage;
        public enum ProjectsErrors
        {
            InternalServerError = 500,
            BadRequest = 400,
            NotFound = 404,
        }
        //public string ProjectsErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ProjectsException(ProjectsErrors code)
        {
            _ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ProjectsErrors.InternalServerError:
                    this._ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.StatusCode = (int) ProjectsErrors.InternalServerError;
                    break;
                case ProjectsErrors.NotFound:
                    this._ErrorMessage.Message = "The resource couldn't be found";
                    this.StatusCode = (int) ProjectsErrors.NotFound;
                    break;
                default:
                    break;
            }
        }

        public ProjectsException(ProjectsErrors code, ValidationException ValidationException)
        {
            _ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ProjectsErrors.BadRequest:
                    this._ErrorMessage.Message = "Please review the errors, inconsistent data.";
                    this._ErrorMessage.Errors = ValidationException.Errors;
                    this.StatusCode = (int)ProjectsErrors.BadRequest;
                    break;
            }
        }
    }
}
