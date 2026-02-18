# TheAppManager

> A .NET application startup manager with a strategy pattern for configurable web application initialization.

## Description
TheAppManager is a .NET library that simplifies the initialization and configuration of ASP.NET Core web applications using the Strategy pattern. It provides a clean abstraction (`IAppConfigurationStrategy`) for defining custom startup configurations, making it easy to swap configurations across environments.

## Features
- Strategy pattern for pluggable application configuration
- `AppManagerBuilder` for fluent app initialization
- Supports custom and default startup strategies
- Minimal boilerplate for ASP.NET Core startup

## Getting Started
```bash
dotnet add package phmatray.TheAppManager
```

```csharp
AppManager.StartApplication(args, new CustomAppConfiguration());
```

## License
MIT