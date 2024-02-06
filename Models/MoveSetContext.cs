using Microsoft.EntityFrameworkCore;

namespace MartialArtsNet.Models;

public class MoveSetContext : DbContext{
    // constructor
    public MoveSetContext(DbContextOptions<MoveSetContext> options): base(options){

    }

    public DbSet<MoveSet> MoveSets {get; set;} = null!;
}