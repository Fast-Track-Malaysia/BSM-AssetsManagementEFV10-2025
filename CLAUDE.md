# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Tech stack

- **DevExpress XAF (eXpressApp Framework) v17.1** — UI/application framework. All views, editors, actions, navigation and the Application Model are driven by XAF, not by hand-rolled ASP.NET/WinForms plumbing. Most UI customization lives in `Model.xafml`/`Model.DesignedDiffs.xafml` files or in XAF `Controller` subclasses, not in the `.aspx` or Program.cs files.
- **Entity Framework 6.1.3** with Code-First migrations — ORM. Targets SQL Server.
- **.NET Framework 4.5.2**, C# 7.3, MSBuild/Visual Studio 2019 format (sln v16). No .NET Core / .NET (5+).
- **SAP Business One integration** via `SAPbobsCOM` — used to post Purchase Requests / contracts to SAP B1. Server/DB credentials live in Web.config `appSettings` (`B1*` keys) and are copied into `GeneralSettings` statics at `Session_Start`.

## Solution layout

Six projects in `AssetsManagementEF.sln`:

| Project | Role |
|---|---|
| `AssetsManagementEF.Module` | **Platform-agnostic core.** All business objects (`BusinessObjects/*.cs`), the `AssetsManagementEFDbContext`, EF migrations (`Migrations/`), XAF `ModuleUpdater` (`DatabaseUpdate/Updater.cs`), platform-neutral controllers, and the static `GeneralSettings` constants/config cache. |
| `AssetsManagementEF.Module.Web` | Web-specific XAF controllers and editors (everything under `Controllers/` — DeviationController, WorkOrderControllers, PurchaseRequestControllers, etc.). This is where most UI business logic lives — the `.Win` equivalent is mostly empty. |
| `AssetsManagementEF.Module.Win` | WinForms-specific XAF controllers/editors (minimal). |
| `AssetsManagementEF.Web` | ASP.NET WebForms host (`Global.asax.cs`, `Default.aspx`, `Login.aspx`, `WebApplication.cs`, `Web.config`). Entry point for the web app. |
| `AssetsManagementEF.Win` | WinForms host (`Program.cs`, `WinApplication.cs`). |
| `AssetsManagementEF.WorkflowServerService` | Windows Service hosting the XAF Workflow Server. Install instructions are in `Install WorkflowServiceServer.docx` at the repo root. |

The dependency direction is strictly `Web`/`Win` → `Module.Web`/`Module.Win` → `Module`. Don't leak platform types into `AssetsManagementEF.Module`.

## Build

Use Visual Studio 2019 or MSBuild for .NET Framework (Developer Command Prompt):

```cmd
msbuild AssetsManagementEF.sln /p:Configuration=Debug /p:Platform="Any CPU"
msbuild AssetsManagementEF.sln /p:Configuration=Release /p:Platform=x64
```

Configurations: `Debug`, `Release`, `EasyTest` × `Any CPU`, `x64`. The `EasyTest` configuration defines `EASYTEST` and switches the web app to use `EasyTestConnectionString` (see `Global.asax.cs` `#if EASYTEST` blocks).

There is no `dotnet build`, no npm, no package.json. NuGet packages live in the repo-root `packages/` folder (restored by VS/msbuild).

## Running

- **Web**: open the solution in VS and F5 the `AssetsManagementEF.Web` project — it is configured for IIS Express on port 2065 (`<IISUrl>http://localhost:2065/</IISUrl>` in the csproj). `Session_Start` in `Global.asax.cs` bootstraps the `AssetsManagementEFAspNetApplication`, copies `appSettings` into `GeneralSettings`, sets the connection string from the `ConnectionString` entry, and calls `Setup()`/`Start()`.
- **Win**: set `AssetsManagementEF.Win` as startup project.
- **WorkflowServerService**: this is a Windows Service, not a console app in normal operation — use the procedure in `Install WorkflowServiceServer.docx`.

## Database

- Connection strings are configured in each host's `.config` under the name **`ConnectionString`** (this exact name is hard-coded in `AssetsManagementEFDbContext()` — `base("name=ConnectionString")`). Changing the name will break DbContext construction.
- Schema changes go through **both** EF Code-First migrations (`AssetsManagementEF.Module/Migrations/`) **and** XAF's `ModuleUpdater` (`AssetsManagementEF.Module/DatabaseUpdate/Updater.cs`). EF migrations handle schema; `Updater.cs` seeds reference data (DocTypes, roles, etc.) keyed on the constants in `GeneralSettings`. When adding a seed row, use `ObjectSpace.FindObject<T>` with a `BinaryOperator` before creating — the updater runs on every version bump.
- To add a migration, use the EF Package Manager Console against `AssetsManagementEF.Module` as the default project (`Add-Migration <name>`). Do not hand-edit the timestamped migration filenames.
- Ad-hoc SQL patches applied to production databases live in `SQL/<YYYYMMDD>/` — these are the record of what has been run against each environment, not something the app applies automatically.

## Reports

Report definitions are DevExpress `.repx` files stored in multiple folders at the repo root: `Reports/`, `ReportsNew/`, `Reports Deviation/`, `Reports Deviation Backup/`. The running web app also has `AssetsManagementEF.Web/Reports/`. Report metadata is persisted in the `ReportDataV2` table (registered in `Module.cs` via `ReportsModuleV2.ReportDataType`), so uploading a `.repx` through the app's Reports UI is the canonical way to deploy — dropping files on disk alone does not make them visible.

## Business objects

All persistent entities are in `AssetsManagementEF.Module/BusinessObjects/` and registered as `DbSet<T>` on `AssetsManagementEFDbContext`. Every new entity must be added to the DbContext **and** will typically require a new EF migration. Pluralized class names (e.g. `WorkOrders`, `Equipments`) are the convention — follow it; they are table names, not collections.

`NonPersistentObjects.cs` holds `[DomainComponent]` types used for transient XAF views (search dialogs, report parameter screens) — do not add them to the DbContext.

## Controllers and UI customization

- Platform-agnostic rules (validation, default property values, cross-entity logic) → `AssetsManagementEF.Module/Controllers/`.
- Web-only UI behavior (popup dialogs, ASPxHtmlEditor wiring, web-specific actions) → `AssetsManagementEF.Module.Web/Controllers/`. This is where the bulk of active development happens.
- Look at the existing `*Controller.cs` / `*Controller.Designer.cs` pairs before adding new controllers — the `.Designer.cs` is regenerated by Visual Studio's Controller designer, don't hand-edit it.

## Tests

Tests are **EasyTest** scripts, not xUnit/NUnit. See `AssetsManagementEF.Module/FunctionalTests/sample.ets` and `config.xml`. They are run by building the `EasyTest` solution configuration and executing via DevExpress's EasyTest runner. There is no in-process unit test project.

## Conventions worth knowing

- **Date formatting** is forced to `dd/MM/yyyy` and `CurrencySymbol` is blanked globally via `Instance_CustomizeFormattingCulture` in `Global.asax.cs`. Don't override per-view unless you have a reason.
- **Security**: the web app uses Forms auth with `<deny users="?"/>`. `DXX.axd`, `Error.aspx`, and `Images` are explicitly opened to anonymous users in `Web.config` — preserve those `<location>` exceptions if you add more.
- **`GeneralSettings`** (static class) holds string constants (document type codes like `"PM"`, `"CM"`, `"PR"`, `"WR"`, `"DE"`, status strings like `"Under Review"`, `"Approved"`) and a runtime cache of SAP B1 / email settings loaded from `appSettings`. Reference the constants — do not re-literal them.
- **`.csproj.bak`** files are committed alongside each `.csproj`. They are prior backups, not active — don't edit them.
- DevExpress assembly references are version-pinned to `17.1.4.0`. Upgrading the DevExpress suite is a cross-cutting change that touches every `.csproj` and `Web.config` `<assemblies>` / `<handlers>` / `<httpModules>` section.
