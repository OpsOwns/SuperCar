using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperCar.CarView.Infrastructure.Database.Models;

namespace SuperCar.CarView.Infrastructure.Database.Configuration
{
    public class ConfigurationCar : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Body);
            builder.Property(x => x.Color);
            builder.Property(x => x.Country);
            builder.Property(x => x.Doors);
            builder.Property(x => x.Engine);
            builder.Property(x => x.Fuel);
            builder.Property(x => x.ImageLink);
            builder.Property(x => x.Make);
            builder.Property(x => x.Model);
            builder.Property(x => x.ProductionYear);
            builder.Property(x => x.Type);
            builder.Property(x => x.Trunk);
            builder.Property(x => x.Seats);
        }
    }
}
