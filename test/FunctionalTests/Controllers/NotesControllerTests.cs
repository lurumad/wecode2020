using FluentAssertions;
using FunctionalTests.Seedwork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeCode.Api.Models;
using Xunit;

namespace FunctionalTests.Controllers
{
    [Collection(nameof(TestCollectionFixture))]
    public class notes_api
    {
        private readonly TestFixture Given;

        public notes_api(TestFixture fixture)
        {
            Given = fixture;
        }

        [Fact]
        [ResetDatabase]
        public async Task should_allow_get_back_notes()
        {
            var notes = await Given.AListOfNotesInTheDatabase();

            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.Get)
                .GetAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var result = await response.Content.ReadAsAsync<List<Note>>();
            result.Should().HaveCount(notes.Count);
        }

        [Fact]
        [ResetDatabase]
        public async Task should_allow_get_back_a_note_by_id()
        {
            var note = await Given.ANoteInTheDatabase();

            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.GetBy(note.Id))
                .GetAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var result = await response.Content.ReadAsAsync<Note>();
            result.Id.Should().Be(note.Id);
            result.Content.Should().Be(note.Content);
            result.Important.Should().Be(note.Important);
        }

        [Fact]
        public async Task should_not_allow_get_back_a_note_by_id_when_id_does_not_exists()
        {
            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.GetBy(0))
                .GetAsync();

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        [ResetDatabase]
        public async Task should_allow_to_add_new_notes()
        {
            var note = new Note { Content = "Test Content", Date = DateTime.UtcNow, Important = true };

            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.Post)
                .PostAsJsonAsync(note);

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            var result = await response.Content.ReadAsAsync<Note>();
            result.Id.Should().BeGreaterThan(0);
            result.Content.Should().Be(note.Content);
            result.Date.Should().Be(note.Date);
            result.Important.Should().Be(note.Important);
        }

        [Fact]
        [ResetDatabase]
        public async Task allow_to_update_an_existing_note()
        {
            const string value = "Updated!";
            var note = await Given.ANoteInTheDatabase();
            var patch = new JsonPatchDocument<Note>().Replace(x => x.Content, value);
            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.Patch(note.Id))
                .PatchAsJsonAsync(patch);

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.GetBy(note.Id))
                .GetAsync();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var result = await response.Content.ReadAsAsync<Note>();
            result.Id.Should().Be(note.Id);
            result.Content.Should().Be(value);
        }

        [Fact]
        [ResetDatabase]
        public async Task allow_to_delete_an_existing_note()
        {
            var note = await Given.ANoteInTheDatabase();

            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.Delete(note.Id))
                .DeleteAsync();

            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        [ResetDatabase]
        public async Task not_allow_to_delete_a_non_existing_note()
        {
            var response = await Given
                .TestServer
                .CreateRequest(Api.V1.Notes.Delete(0))
                .DeleteAsync();

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
