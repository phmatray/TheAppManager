# TheAppManager

> A .NET library that simplifies ASP.NET Core web application startup using the Strategy pattern.

[![CI](https://github.com/phmatray/TheAppManager/actions/workflows/ci.yml/badge.svg)](https://github.com/phmatray/TheAppManager/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/TheAppManager.svg)](https://www.nuget.org/packages/TheAppManager)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Overview

TheAppManager provides a clean abstraction (`IAppConfigurationStrategy`) for defining custom startup configurations in ASP.NET Core applications. Using the Strategy pattern, it makes it easy to swap configurations across environments, keep startup logic organized, and reduce boilerplate.

### Architecture

```
                    ┌──────────────────────┐
                    │   AppManager          │
                    │   .StartApplication() │
                    └──────────┬───────────┘
                               │
                    ┌──────────▼───────────┐
                    │  AppManagerBuilder    │
                    │  ┌─────────────────┐ │
                    │  │ WebApplication  │ │
                    │  │ Builder         │ │
                    │  └─────────────────┘ │
                    └──────────┬───────────┘
                               │ applies
              ┌────────────────▼────────────────┐
              │  IAppConfigurationStrategy       │
              │  ├─ ConfigureServices()          │
              │  ├─ ConfigureMiddleware()        │
              │  └─ ConfigureEndpoints()         │
              └────────────────┬────────────────┘
                    ┌──────────┴──────────┐
                    │                     │
          ┌─────────▼──────┐   ┌──────────▼─────┐
          │ DefaultApp     │   │ YourCustom     │
          │ Configuration  │   │ Configuration  │
          └────────────────┘   └────────────────┘
```

## Installation

```bash
dotnet add package TheAppManager
```

## Quick Start

The simplest way to use TheAppManager:

```csharp
using TheAppManager.Startup;

AppManager.StartApplication(args);
```

This uses `DefaultAppConfiguration`, which sets up HTTPS redirection and a root `/` endpoint.

## Custom Configuration

Create your own strategy by implementing `IAppConfigurationStrategy`:

```csharp
using TheAppManager.Startup;

public class MyAppConfiguration : IAppConfigurationStrategy
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<MyService>();
    }

    public void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
    }

    public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => "Hello World!");
        endpoints.MapGet("/api/data", (MyService svc) => svc.GetData());
    }
}
```

Then use it in `Program.cs`:

```csharp
AppManager.StartApplication(args, new MyAppConfiguration());
```

### Extending the Default Configuration

You can also extend `DefaultAppConfiguration`:

```csharp
public class MyAppConfiguration : DefaultAppConfiguration
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddScoped<MyService>();
    }

    public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        base.ConfigureEndpoints(endpoints);
        endpoints.MapGet("/api/data", (MyService svc) => svc.GetData());
    }
}
```

### Async Support

Use `StartApplicationAsync` for async startup:

```csharp
await AppManager.StartApplicationAsync(args, new MyAppConfiguration());
```

### Builder Configuration Hook

Customize the `WebApplicationBuilder` directly:

```csharp
AppManager.StartApplication(
    args,
    new MyAppConfiguration(),
    builder =>
    {
        builder.Configuration.AddJsonFile("custom-settings.json", optional: true);
    });
```

### Advanced: Using AppManagerBuilder

For more control, use `AppManagerBuilder` directly:

```csharp
var appManager = new AppManagerBuilder(args)
    .ConfigureBuilder(builder =>
    {
        builder.Configuration.AddJsonFile("custom-settings.json");
    })
    .Build(new MyAppConfiguration());

await appManager.RunAsync();
```

## Project Structure

```
src/TheAppManager/              → Class library (NuGet package)
samples/TheAppManager.Sample/   → Sample web app demonstrating usage
tests/TheAppManager.Tests/      → Unit and integration tests
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Make your changes
4. Run tests (`dotnet test`)
5. Commit your changes (`git commit -am 'Add my feature'`)
6. Push to the branch (`git push origin feature/my-feature`)
7. Open a Pull Request

## License

[MIT](LICENSE)
