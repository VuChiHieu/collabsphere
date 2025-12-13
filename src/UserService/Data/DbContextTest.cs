using Microsoft.EntityFrameworkCore;

namespace UserService.Data;

public class DbContextTest
{
    public static async Task Test(UserServiceDbContext context)
    {
        Console.WriteLine("\n🔍 Testing UserService DbContext...");
        Console.WriteLine("=".PadRight(60, '='));
        
        try
        {
            // Test 1: Database Connection
            var canConnect = await context.Database.CanConnectAsync();
            Console.WriteLine($"✅ Can Connect: {canConnect}");
            
            // Test 2: Database Name
            var dbName = context.Database.GetDbConnection().Database;
            Console.WriteLine($"✅ Database: {dbName}");
            
            // Test 3: Entity Types Count
            var entityCount = context.Model.GetEntityTypes().Count();
            Console.WriteLine($"✅ Entity Types: {entityCount}");
            
            // Test 4: List All Tables
            Console.WriteLine("\n📋 Tables:");
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                Console.WriteLine($"   - {tableName}");
            }
            
            Console.WriteLine("\n" + "=".PadRight(60, '='));
            Console.WriteLine("✅ DbContext initialized successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ DbContext test failed: {ex.Message}\n");
            throw;
        }
    }
}