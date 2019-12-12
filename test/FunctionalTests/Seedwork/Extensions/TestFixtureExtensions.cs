using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeCode.Api.Models;

namespace FunctionalTests.Seedwork
{
    public static class TestFixtureExtensions
    {
        public static async Task<List<Note>> AListOfNotesInTheDatabase(this TestFixture fixture, int number = 20)
        {
            var notes = new List<Note>();

            for (int i = 1; i <= number; i++)
            {
                notes.Add(new Note { Content = $"Content {i}", Date = DateTime.UtcNow });
            }

            await fixture.ExecuteDbContextAsync(async context =>
            {
                context.AddRange(notes);
                await context.SaveChangesAsync();
            });

            return notes;
        }

        public static async Task<Note> ANoteInTheDatabase(this TestFixture fixture)
        {
            var note = new Note { Content = "Content", Date = DateTime.UtcNow };
            
            await fixture.ExecuteDbContextAsync(async context =>
            {
                context.Add(note);
                await context.SaveChangesAsync();
            });

            return note;
        }
    }
}
