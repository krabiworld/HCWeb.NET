# HCWeb.NET

## Build and run

1. Clone repository:
```sh
git clone https://github.com/HeadcrabJ/HCWeb.NET.git
cd HCWeb.NET
```
2. Rename `appsettings.Example.json` to `appsettings.Development.json` and fill it with the required data
3. Trust development certificates
```sh
dotnet dev-certs https --trust
```
4. Run or build
```sh
dotnet run -lp https # Run with https
dotnet build # Build without https
```
