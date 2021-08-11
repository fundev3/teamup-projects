namespace Jalasoft.TeamUp.Projects.ProjectsException
{
    public class Error
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object AttemptedValue { get; set; }
    }
}