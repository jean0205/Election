using Election.API.Data.Audit;
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Election.API.Data
{
    public class DataContext : AuditDataContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Voter> Voters { get; set; }
        public DbSet<Canvas> Canvas { get; set; }
        public DbSet<CanvasType> CanvasTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Constituency> Constituencies { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<PollingDivision> PollingDivisions { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<NationalElection> NationalElections { get; set; }        
        public DbSet<ElectionVote> ElectionVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Voter>().HasIndex(v => v.Reg);
            modelBuilder.Entity<Constituency>().HasIndex(v => v.SGSE);
            modelBuilder.Entity<Party>().HasIndex(v => v.Name);
            modelBuilder.Entity<PollingDivision>().HasIndex(v => v.Name);

        }


        }
}
