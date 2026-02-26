# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

### Changed

- Restructured project into class library (`src/TheAppManager`), sample app (`samples/TheAppManager.Sample`), and test project (`tests/TheAppManager.Tests`)
- Upgraded target framework to .NET 10
- Updated CI actions: `actions/checkout` to v6, `actions/setup-dotnet` to v5, `actions/upload-artifact` to v6
- Updated `Swashbuckle.AspNetCore` to v10
- Updated `xunit.runner.visualstudio` to 3.1.5
- Updated `dotnet-sdk` to v10.0.103

### Added

- Renovate configuration for automated dependency updates
- Updated README with architecture diagram, installation instructions, and usage examples

## [0.1.0] - 2024-05-01

### Added

- Initial project setup with ASP.NET Core web application
- `IAppConfigurationStrategy` interface for defining custom startup configurations using the Strategy pattern
- `DefaultAppConfiguration` base implementation with HTTPS redirection and root endpoint
- `AppManager` static entry point with `StartApplication` and `StartApplicationAsync` methods
- `AppManagerBuilder` for advanced builder-level configuration
- Support for builder configuration hooks via `Action<WebApplicationBuilder>`
- Endpoint refactoring for cleaner route definitions
