namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.IO;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class UpdateProjectTest
{
    private readonly Mock<IProjectsService> mockProjectsService;
    private readonly DefaultHttpContext mockHttpContext;
    private readonly UpdateProject updateProject;

    public UpdateProjectTest()
    {
        this.mockProjectsService = new Mock<IProjectsService>();
        this.mockHttpContext = new DefaultHttpContext();
        this.updateProject = new UpdateProject(this.mockProjectsService.Object);
    }

    [Fact]
    public async void UpdateProject_Returns_CreatedResult()
    {
        var stubProject = new Project
        {
            Id = new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"),
            Name = "TeamUp",
            Contact = new Contact()
            {
                Name = "Jose Ecos",
                IdResume = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de")
            },
            Description = "Centralize resumes and project",
            Logo = "https://www.example.com/images/dinosaur.jpg",
            MemberList = new Contact[1]
    {
                        new Contact
                        {
                            Name = "Paola Quintanilla",
                            IdResume = new Guid("536316e6-f8f6-41ea-b1ce-455b92be9303")
                        }
    },
            State = true,
            TextInvitation = "You are invited to be part of TeamUp",
            CreationDate = DateTime.Today.AddDays(-10),
        };
        var request = this.mockHttpContext.Request;
        this.mockProjectsService.Setup(service => service.GetProject(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubProject);
        this.mockProjectsService.Setup(service => service.UpdateProject(null)).Returns(stubProject);
        var response = await this.updateProject.UpdateProjectTask(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
        var createdResult = Assert.IsType<CreatedResult>(response);
        Assert.IsType<Project>(createdResult.Value);
    }
}
}
