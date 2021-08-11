namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    using System;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsException : Exception
    {
        public ProjectsException(ProjectsErrors code)
        {
            this.BaseError = new BaseError();
            this.BaseError.Message = "The resource couldn't be found";
            this.Error = new ObjectResult(this.BaseError);
            this.Error.StatusCode = (int)ProjectsErrors.NotFound;
        }

        public ProjectsException(ProjectsErrors code, Exception exception)
        {
            this.BaseError = new BaseError();
            this.ValidationError = new ValidationError();

            switch (code)
            {
                case ProjectsErrors.BadRequest:
                    var validationException = (ValidationException)exception;
                    this.ValidationError.Message = "Please review the errors, inconsistent data.";

                    foreach (var error in validationException.Errors)
                    {
                        var myError = new Error();
                        myError.PropertyName = error.PropertyName;
                        myError.ErrorMessage = error.ErrorMessage;
                        myError.AttemptedValue = error.AttemptedValue;
                        this.ValidationError.Errors.Add(myError);
                    }

                    this.Error = new ObjectResult(this.ValidationError);
                    this.Error.StatusCode = (int)ProjectsErrors.BadRequest;
                    break;
                case ProjectsErrors.InternalServerError:
                    this.BaseError.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.Error = new ObjectResult(this.BaseError);
                    this.Error.StatusCode = (int)ProjectsErrors.InternalServerError;
                    break;
                default:
                    break;
            }
        }

        public BaseError BaseError { get; set; }

        public ValidationError ValidationError { get; set; }

        public ObjectResult Error { get; set; }
    }
}
