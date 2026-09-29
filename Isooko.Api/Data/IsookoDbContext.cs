using Microsoft.EntityFrameworkCore;

namespace Isooko.Api.Data;

public class IsookoDbContext(DbContextOptions<IsookoDbContext> options) : DbContext(options)
{

}