# Repository Guidelines

## Project Structure & Module Organization
The solution `Sumo.sln` hosts the single ASP.NET Core application defined by `Sumo.csproj`, with startup wiring in `Program.cs`. Razor UI assets live under `Components/` (`Components/Pages` for routable pages, `Components/Layout` for shells, `Components/Views` for reusable fragments). Domain entities sit in `Models/`, while EF Core configuration and relationships reside in `Data/SumoDbContext.cs`. Static assets, styles, and bundled libraries go in `wwwroot/`. Build artefacts appear in `bin/` and `obj/`; keep them out of commits.

## Build, Test, and Development Commands
Run `dotnet restore` to hydrate NuGet dependencies before building. Use `dotnet build` for compile-time verification. Start the site locally with `dotnet run --project Sumo.csproj` and browse the reported Kestrel URL. When refreshing icon assets, run `npm install` to update `bootstrap-icons` in `node_modules/`.

## Coding Style & Naming Conventions
Follow modern C# conventions: four-space indentation, `PascalCase` for public types/members, and `camelCase` for locals and injected services (prefix private fields with `_`). Razor component filenames stay `PascalCase.razor` to match the component class. Keep EF mappings fluent and favor expression-bodied members for simple getters. Run `dotnet format` (install via `dotnet tool install --global dotnet-format` if needed) before opening a PR to normalize spacing and `using` directives.

## Testing Guidelines
The repository currently has no test project; new features should add an xUnit-based project under `Tests/` and include it in `Sumo.sln`. Name test classes `<TypeUnderTest>Tests` and methods `Should_<behavior>`. Execute the suite with `dotnet test`. For data access logic, prefer in-memory providers or SQLite to validate the navigation properties configured in `Data/SumoDbContext.cs`.

## Commit & Pull Request Guidelines
History to date uses short, lower-case summaries; keep subjects imperative and under ~72 characters (e.g., `add fighter rank lookup`). Expand with contextual body text when schema or UI changes are non-trivial. Pull requests should link related issues, outline schema/UI impact, and include screenshots for user-facing updates. Highlight new migrations, seed data, or configuration changes (`appsettings*.json`, `.env`) so reviewers can reproduce your environment.

## Environment & Configuration Tips
Duplicate `appsettings.json` into `appsettings.Development.json` for local overrides, keeping secrets in `.env` rather than source control. After updating the model, run `dotnet ef migrations add <Name>` (with the EF Core tool installed) and commit the generated migration files. Document required connection strings or feature flags in the PR description.
