using System;

namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    public class ProjectsException : Exception
    {
        public enum ProjectsErros
        {
            InternalServerError = 500,
            NotFound = 404,
        }
        public string ProjectsErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ProjectsException(ProjectsErros code)
        {
            switch (code)
            {
                case ProjectsErros.InternalServerError:
                    this.StatusCode = (int) ProjectsErros.InternalServerError;
                    this.ProjectsErrorMessage = "{\"message\": \"Something went wrong, please contact the TeamUp administrator.\"";
                    break;
                case ProjectsErros.NotFound:
                    this.StatusCode = (int) ProjectsErros.NotFound;
                    this.ProjectsErrorMessage = "{\"message\": \"The resource couldn't be found.\"";
                    break;
                default:
                    break;
            }
        }
    }
}
