namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.IO;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
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

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/name"", ""value"" : ""Tony""},
        { ""op"" : ""replace"", ""path"" : ""/description"", ""value"" : ""asff""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/textinvitation"", ""value"" : ""You are officially invited""},
        { ""op"" : ""replace"", ""path"" : ""/contact/name"", ""value"" : ""Martin""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/name"", ""value"" : ""Lucas""},
        { ""op"" : ""replace"", ""path"" : ""/textinvitation"", ""value"" : ""Wanna join?""}
        ]")]
        public async void UpdateProjectById_IdIsValid_OkObjectResult(string body)
        {
            Project stubProject = StubProject.GetStubProject();

            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockProjectsService.Setup(service => service.GetProject(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubProject);
            this.mockProjectsService.Setup(service => service.UpdateProject(null)).Returns(stubProject);
            var response = await this.updateProject.UpdateProject(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var okObjectResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/name"", ""value"" : ""Tony""},
        { ""op"" : ""replace"", ""path"" : ""/description"", ""value"" : ""asff""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/textinvitation"", ""value"" : ""You are officially invited""},
        { ""op"" : ""replace"", ""path"" : ""/contact/name"", ""value"" : ""Martin""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/name"", ""value"" : ""Lucas""},
        { ""op"" : ""replace"", ""path"" : ""/textinvitation"", ""value"" : ""Wanna join?""}
        ]")]
        public async void UpdateProjectById_IdIsNotValid_NotFoundResult(string body)
        {
            Project stubProject = StubProject.GetStubProject();

            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockProjectsService.Setup(service => service.GetProject(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new ProjectsException(ProjectsErrors.NotFound));
            this.mockProjectsService.Setup(service => service.UpdateProject(null)).Throws(new ProjectsException(ProjectsErrors.NotFound));
            var response = await this.updateProject.UpdateProject(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var result = Assert.IsType<ProjectsErrors>(ProjectsErrors.NotFound);

            Assert.IsType<ProjectsErrors>(ProjectsErrors.NotFound);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""badrequestbody"", ""path"" : ""/naaame"", ""value"" : ""Tony""},
        { ""op"" : ""badrequestbody"", ""path"" : ""/descriptionnn"", ""value"" : ""asff""}
        ]")]
        [InlineData(@"[
        {""op"" : ""badrequestbody"", ""path"" : ""/textssinvitation"", ""value"" : ""You are officially invited""},
        { ""op"" : ""badrequestbody"", ""path"" : ""/contaceet/name"", ""value"" : ""Martin""}
        ]")]
        [InlineData(@"[
        {""op"" : ""badrequestbody"", ""path"" : ""/nameee"", ""value"" : ""Lucas""},
        { ""op"" : ""badrequestbody"", ""path"" : ""/textttinvitation"", ""value"" : ""Wanna join?""}
        ]")]
        public async void UpdateProjectById_BadRequestBody_BadRequestResult(string body)
        {
            Project stubProject = StubProject.GetStubProject();

            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockProjectsService.Setup(service => service.GetProject(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubProject);
            this.mockProjectsService.Setup(service => service.UpdateProject(null)).Returns(stubProject);
            var response = await this.updateProject.UpdateProject(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var result = Assert.IsType<ProjectsErrors>(ProjectsErrors.BadRequest);

            Assert.IsType<ProjectsErrors>(ProjectsErrors.BadRequest);
        }
    }
}
