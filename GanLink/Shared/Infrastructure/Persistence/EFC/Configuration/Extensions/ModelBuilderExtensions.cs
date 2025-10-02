namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions  
{  
    public static void UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)  
    {        foreach (var entity in modelBuilder.Model.GetEntityTypes())  
    {            var tableName = entity.GetTableName();  
        if (!string.IsNullOrEmpty(tableName))  
        {                entity.SetTableName(tableName.ToPlural().ToSnakeCase());  
        }  
        foreach (var property in entity.GetProperties())  
        {                property.SetColumnName(property.GetColumnName().ToSnakeCase());  
        }  
        foreach (var key in entity.GetKeys())  
        {                var keyName = key.GetName();  
            if (!string.IsNullOrEmpty(keyName))  
            {                    key.SetName(keyName.ToSnakeCase());  
            }                            }  
  
        foreach (var foreignKey in entity.GetForeignKeys())  
        {                var foreignKeyName = foreignKey.GetConstraintName();  
            if (!string.IsNullOrEmpty(foreignKeyName))  
            {                    foreignKey.SetConstraintName(foreignKeyName.ToSnakeCase());  
            }            }  
        foreach (var index in entity.GetIndexes())  
        {                var indexDatabaseName = index.GetDatabaseName();  
            if (!string.IsNullOrEmpty(indexDatabaseName))  
            {                    index.SetDatabaseName(indexDatabaseName.ToSnakeCase());  
            }            }        }    }}