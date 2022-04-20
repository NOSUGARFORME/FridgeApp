using System.ComponentModel.DataAnnotations;
using System.Data;
using FridgeApp.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FridgeApp.Infrastructure.Persistence.Config;

internal sealed class ReadConfiguration : 
    IEntityTypeConfiguration<FridgeReadModel>,
    IEntityTypeConfiguration<ProductReadModel>,
    IEntityTypeConfiguration<FridgeProductsReadModel>,
    IEntityTypeConfiguration<FridgeModelReadModel>
{
    public void Configure(EntityTypeBuilder<FridgeReadModel> builder)
    {
        builder.ToTable("fridges");
        builder.HasKey(f => f.Id);

        builder
            .Property(f => f.OwnerName)
            .HasConversion(o => o.ToString(),
                o => OwnerNameReadModel.Create(o));
    }

    public void Configure(EntityTypeBuilder<ProductReadModel> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
    }

    public void Configure(EntityTypeBuilder<FridgeProductsReadModel> builder)
    {
        builder.ToTable("fridge_products");
        builder.HasKey(fp => new {fp.FridgeId, fp.ProductId});

        builder
            .HasOne(fp => fp.Fridge)
            .WithMany(f => f.Products)
            .HasForeignKey(fp => fp.FridgeId);
            
        builder
            .HasOne(fp => fp.Product)
            .WithMany(p => p.FridgeProducts)
            .HasForeignKey(fp => fp.ProductId);
    }

    public void Configure(EntityTypeBuilder<FridgeModelReadModel> builder)
    {
        builder.ToTable("fridge_models");
        builder.HasKey(fm => fm.Id);

        builder
            .HasMany(fm => fm.Fridges)
            .WithOne(f => f.FridgeModel)
            .HasForeignKey(f => f.FridgeModelId);
    }
}