// DbSeeder.cs
using GanLink.BovinueSystem.Infraestructure.Persistence.EF.Seeders;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(GanLinkDbContext db)
    {
        // 1) Categorías (no dependen de nadie)
        if (!await db.BovinueMetricCategories.AnyAsync())
        {
            db.BovinueMetricCategories.AddRange(BovinueMetricCategorySeeder.GetData());
            await db.SaveChangesAsync();
        }

        // 2) Parámetros (dependen de CategoryId)
        if (!await db.BovinueMetricParameters.AnyAsync())
        {
            db.BovinueMetricParameters.AddRange(BovinueMetricParameterSeeder.GetData());
            await db.SaveChangesAsync();
        }

        // 3) Cattle Health Records (catálogo independiente)
        if (!await db.BovinueCattleHealthRecords.AnyAsync())
        {
            db.BovinueCattleHealthRecords.AddRange(BovinueCattleHealthRecordSeeder.GetData());
            await db.SaveChangesAsync();
        }
    }
}