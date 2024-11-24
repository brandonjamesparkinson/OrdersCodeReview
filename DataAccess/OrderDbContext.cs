using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess;

public class OrderDbContext : DbContext
{
    private readonly DbConnection _connection;

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
        _connection = RelationalOptionsExtension.Extract(options).Connection!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public override void Dispose()
    {
        _connection.Dispose();

        base.Dispose();
    }
}

