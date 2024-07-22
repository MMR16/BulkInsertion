using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using BulkInsertion;
using EFCore.BulkExtensions;
using System.Security.Cryptography;
using static BulkInsertion.ApplicationDbContext;

internal class Program
{
    public  static void Main(string[] args)
    {

        var config = new ManualConfig()
        .WithOptions(ConfigOptions.DisableOptimizationsValidator)
        .AddValidator(JitOptimizationsValidator.DontFailOnError)
        .AddLogger(ConsoleLogger.Default)
        .AddColumnProvider(DefaultColumnProviders.Instance);

        BenchmarkRunner.Run<BulkTest>(config);


        //var listOfByers = SeedData.GenerateRandomProspectiveBuyers(20);
        //ApplicationDbContext dbContex = new ApplicationDbContext();
        //foreach (var item in listOfByers)
        //{
        //     dbContex.ProspectiveBuyer.Add(item);
        //}
        // dbContex.SaveChanges();
      
    }




}


[MemoryDiagnoser(false)]
public class BulkTest
{

    [Benchmark]
    public async Task AddRegular()
    {
        var listOfByers = SeedData.GenerateRandomProspectiveBuyers(2000);
        ApplicationDbContext dbContex = new ApplicationDbContext();
        await dbContex.ProspectiveBuyer.AddRangeAsync(listOfByers);
        await dbContex.SaveChangesAsync();
    }

    [Benchmark]
    public async Task AddRegularLoop()
    {
        var listOfByers = SeedData.GenerateRandomProspectiveBuyers(2000);
        ApplicationDbContext dbContex = new ApplicationDbContext();
        foreach (var item in listOfByers)
        {
            await dbContex.ProspectiveBuyer.AddAsync(item);
        }
        await dbContex.SaveChangesAsync();
    }

    [Benchmark]
    public async Task AddBulk()
    {
        var listOfByers = SeedData.GenerateRandomProspectiveBuyers(2000);
        using var context = new ApplicationDbContext();

        await context.BulkInsertAsync(listOfByers);
    }
}

public static class SeedData
{
    public static List<ProspectiveBuyer> GenerateRandomProspectiveBuyers(int count)
    {
        var random = new Random();
        var buyers = new List<ProspectiveBuyer>();

        for (int i = 1; i <= count; i++)
        {
            buyers.Add(new ProspectiveBuyer
            {
                ProspectAlternateKey = $"ALT{i}",
                FirstName = $"FirstName{i}",
                MiddleName = $"MiddleName{i}",
                LastName = $"LastName{i}",
                BirthDate = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28)),
                MaritalStatus = random.Next(0, 2) == 0 ? 'M' : 'S',
                Gender = random.Next(0, 2) == 0 ? "M" : "F",
                EmailAddress = $"buyer{i}@example.com",
                YearlyIncome = (decimal)(random.NextDouble() * 100000),
                TotalChildren = (byte)random.Next(0, 10),
                NumberChildrenAtHome = (byte)random.Next(0, 10),
                Education = $"EducationLevel{i}",
                Occupation = $"Occupation{i}",
                HouseOwnerFlag = random.Next(0, 2) == 0 ? 'Y' : 'N',
                NumberCarsOwned = (byte)random.Next(0, 5),
                AddressLine1 = $"Address1_{i}",
                AddressLine2 = $"Address2_{i}",
                City = $"City{i}",
                StateProvinceCode = $"14",
                PostalCode = $"PC{i}",
                Phone = $"Phone{i}",
                Salutation = $"Sal{i}",
                Unknown = random.Next(0, 100)
            });
        }

        return buyers;
    }
}
