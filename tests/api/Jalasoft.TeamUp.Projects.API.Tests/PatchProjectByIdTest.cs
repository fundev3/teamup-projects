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

    public class PatchProjectByIdTest
{
    private readonly Mock<IProjectsService> mockProjectsService;
    private readonly DefaultHttpContext mockHttpContext;
    private readonly PatchProjectById updateProject;

    public PatchProjectByIdTest()
    {
        this.mockProjectsService = new Mock<IProjectsService>();
        this.mockHttpContext = new DefaultHttpContext();
        this.updateProject = new PatchProjectById(this.mockProjectsService.Object);
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
        static Stream SetStream(string body)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(body);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }

        HttpRequest request = this.mockHttpContext.Request;
        request.Body = SetStream(
                @"[
        {""op"" : ""replace"", ""path"" : ""/name"", ""value"" : ""Tony""},
        { ""op"" : ""replace"", ""path"" : ""/description"", ""value"" : ""asff""}
        ]");
        this.mockProjectsService.Setup(service => service.GetProject(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubProject);
        this.mockProjectsService.Setup(service => service.UpdateProject(null)).Returns(stubProject);
        var response = await this.updateProject.UpdateProject(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
        var okObjectResult = Assert.IsType<OkObjectResult>(response);

        Assert.Equal(200, okObjectResult.StatusCode);
        }
}
}
