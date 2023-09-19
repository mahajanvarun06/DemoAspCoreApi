﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleCore.Entities;
using System;
using System.Linq;

namespace SampleCoreApi.Test
{
    //public class CustomSampleCoreApplicationFactory<TStartup>
    //: WebApplicationFactory<TStartup> where TStartup : class
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //        // Remove the app's ApplicationDbContext registration.
    //        var descriptor = services.SingleOrDefault(
    //                d => d.ServiceType ==
    //                    typeof(DbContextOptions<SampleCoreDbContext>));

    //            if (descriptor != null)
    //            {
    //                services.Remove(descriptor);
    //            }

    //        // Add ApplicationDbContext using an in-memory database for testing.
    //        services.AddDbContext<SampleCoreDbContext>(options =>
    //            {
    //                options.UseInMemoryDatabase("InMemoryDbForTesting");
    //            });

    //        // Build the service provider.
    //        var sp = services.BuildServiceProvider();

    //        // Create a scope to obtain a reference to the database
    //        // context (ApplicationDbContext).
    //        using (var scope = sp.CreateScope())
    //            {
    //                var scopedServices = scope.ServiceProvider;
    //                var db = scopedServices.GetRequiredService<SampleCoreDbContext>();
    //                var logger = scopedServices
    //                    .GetRequiredService<ILogger<CustomSampleCoreApplicationFactory<TStartup>>>();

    //            // Ensure the database is created.
    //            db.Database.EnsureCreated();

    //                try
    //                {
    //                    // Seed the database with test data.
    //                    DummyDataDBInitializer.InitializeDbForTests(db);
    //                }
    //                catch (Exception ex)
    //                {
    //                    logger.LogError(ex, "An error occurred seeding the " +
    //                        "database with test messages. Error: {Message}", ex.Message);
    //                }
    //            }
    //        });
    //    }
    //}

    public class CustomSampleCoreApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Remove the app's SampleCoreDbContext registration.
                var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<SampleCoreDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContextPool<SampleCoreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");
                    //options.UseInternalServiceProvider(serviceProvider);
                });

                

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<SampleCoreDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomSampleCoreApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        DummyDataDBInitializer.ReinitializeDbForTests(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

}


    