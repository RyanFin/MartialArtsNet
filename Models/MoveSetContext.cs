using Microsoft.EntityFrameworkCore;
using MartialArtsNet.Models;

namespace MartialArtsNet.Models;

public class MoveSetContext : DbContext{
    // constructor
    public MoveSetContext(DbContextOptions<MoveSetContext> options): base(options){

    }

    public DbSet<MoveSet> MoveSets {get; set;} = null!;

public DbSet<MartialArtsNet.Models.MoveSetDTO> MoveSetDTO { get; set; } = default!;
}