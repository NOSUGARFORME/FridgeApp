using FridgeApp.Domain.Entities;
using FridgeApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FridgeApp.Infrastructure.Persistence.Config;

internal sealed class WriteConfiguration : 
    IEntityTypeConfiguration<Fridge>,
    IEntityTypeConfiguration<Product>,
    IEntityTypeConfiguration<FridgeProduct>,
    IEntityTypeConfiguration<FridgeModel>
{
    public void Configure(EntityTypeBuilder<Fridge> builder)
    {
        builder.HasKey(f => f.Id);

        var ownerConverter = new ValueConverter<OwnerName, string>(o => o.ToString(),
            o => OwnerName.Create(o));

        builder
            .Property(f => f.Id)
            .HasConversion(id => id.Value,
                id => new FridgeId(id));

        builder
            .Property(f => f.OwnerName)
            .HasConversion(on => on.ToString(),
                on => OwnerName.Create(on))
            .HasColumnName("OwnerName");

        builder
            .Property(f => f.Name)
            .HasConversion(name => name.Value, 
                name => new FridgeName(name))
            .HasColumnName("Name");
            
        builder.ToTable("fridges");
    }

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(f => f.Id);
            
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value,
                id => new ProductId(id));

        builder.Property(p => p.Name)
            .HasConversion(pn => pn.Value,
                pn => new ProductName(pn));
            
        builder.Property(p => p.DefaultQuantity)
            .HasConversion(dq => dq.Value,
                dq => new ProductQuantity(dq));
            
        builder.ToTable("products");
    }

    public void Configure(EntityTypeBuilder<FridgeProduct> builder)
    {
        builder.ToTable("fridge_products");
        builder.HasKey(fp => new {fp.FridgeId, fp.ProductId});
            
        builder
            .HasOne(fp => fp.Fridge)
            .WithMany(f => f.FridgeProducts)
            .HasForeignKey(fp => fp.FridgeId);

        builder
            .HasOne(fp => fp.Product)
            .WithMany(p => p.FridgeProducts)
            .HasForeignKey(fp => fp.ProductId);

        builder
            .Property(fp => fp.Quantity)
            .HasConversion(dq => dq.Value, dq => new ProductQuantity(dq));
    }

    public void Configure(EntityTypeBuilder<FridgeModel> builder)
    {
        builder.ToTable("fridge_models");
        builder.HasKey(fm => fm.Id);

        builder
            .Property(fm => fm.Id)
            .HasConversion(id => id.Value,
                id => new FridgeModelId(id));

        builder
            .Property(fm => fm.FridgeModelName)
            .HasConversion(name => name.Value,
                name => new FridgeModelName(name))
            .HasColumnName("Name");

        builder
            .Property(fm => fm.FridgeModelYear)
            .HasConversion(year => year.Value,
                year => new FridgeModelYear(year))
            .HasColumnName("Year");
            
        builder.HasMany(fm => fm.Fridges)
            .WithOne(f => f.FridgeModel)
            .HasForeignKey(f => f.FridgeModelId);
    }
}