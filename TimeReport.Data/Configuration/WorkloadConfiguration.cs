namespace TimeReport.Data.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimeReport.Model;

public sealed class WorkloadConfiguration : IEntityTypeConfiguration<Workload>
{
    public void Configure(EntityTypeBuilder<Workload> builder)
    {
    }
}