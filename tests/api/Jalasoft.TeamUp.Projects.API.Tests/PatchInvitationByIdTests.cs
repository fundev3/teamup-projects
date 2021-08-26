namespace Jalasoft.TeamUp.Projects.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Projects.API.Controllers;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Projects.Core.Interfaces;
    using Jalasoft.TeamUp.Projects.Models;
    using Jalasoft.TeamUp.Projects.ProjectsException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class PatchInvitationByIdTests
    {
        private readonly Mock<IInvitationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PatchInvitationById updateInvitation;

        public PatchInvitationByIdTests()
        {
            this.mockService = new Mock<IInvitationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.updateInvitation = new PatchInvitationById(this.mockService.Object);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Accepted""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Rejected""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Invited""}
        ]")]
        public async void PatchInvitationById_IdIsValid_OkObjectResult(string body)
        {
            Invitation stubInvitation = StubInvitation.GetStubInvitation();

            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockService.Setup(service => service.GetInvitation(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubInvitation);
            var response = await this.updateInvitation.UpdateInvitation(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var okObjectResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Accepted""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Rejected""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Invited""}
        ]")]
        public async void PatchInvitationById_IdIsNotValid_NotFoundResult(string body)
        {
            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockService.Setup(service => service.GetInvitation(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns((Invitation)null);
            var response = await this.updateInvitation.UpdateInvitation(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var result = Assert.IsType<ObjectResult>(response);

            Assert.Equal(404, result.StatusCode);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Acced""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Rejectd""}
        ]")]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/status"", ""value"" : ""Inited""}
        ]")]
        public async void PatchInvitationById_BadRequestBody_BadRequestResult(string body)
        {
            Invitation stubInvitation = StubInvitation.GetStubInvitation();
            HttpRequest request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockService.Setup(service => service.GetInvitation(new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"))).Returns(stubInvitation);
            this.mockService.Setup(service => service.UpdateInvitation(stubInvitation)).Throws(new ProjectsException(ProjectsErrors.BadRequest, new FluentValidation.ValidationException("BadRequest")));
            var response = await this.updateInvitation.UpdateInvitation(request, new Guid("5a7939fd-59de-33bd-a092-f5d8434584de"));
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
