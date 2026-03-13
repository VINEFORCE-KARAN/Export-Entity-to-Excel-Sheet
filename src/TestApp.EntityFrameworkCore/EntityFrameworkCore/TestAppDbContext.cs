using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Authorization.Roles;
using TestApp.Authorization.Users;
using TestApp.MultiTenancy;
using TestApp.Students;

namespace TestApp.EntityFrameworkCore;

public class TestAppDbContext : AbpZeroDbContext<Tenant, Role, User, TestAppDbContext>
{
    /* Define a DbSet for each entity of the application */

    public TestAppDbContext(DbContextOptions<TestAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
