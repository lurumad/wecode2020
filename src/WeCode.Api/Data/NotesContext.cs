using Microsoft.EntityFrameworkCore;
using WeCode.Api.Models;

namespace WeCode.Api.Data
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {

        }
    }
}
