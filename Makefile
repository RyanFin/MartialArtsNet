.PHONY: run
run:
	dotnet run --launch-profile https

.PHONY: new-app
new-app:
	dotnet new webapi --use-controllers -o MartialArtsNet
	dotnet add package Microsoft.EntityFrameworkCore.InMemory

.PHONY: trust-app
trust-app:
	dotnet dev-certs https --trust

# Add NuGet packages required for scaffolding a new controller
# Install the scaffolding engine (dotnet-aspnet-codegenerator) after uninstalling any possible previous version.
.PHONY: controller-gen
controller-gen:
	dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
	dotnet add package Microsoft.EntityFrameworkCore.Design
	dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	dotnet add package Microsoft.EntityFrameworkCore.Tools
	dotnet tool uninstall -g dotnet-aspnet-codegenerator
	dotnet tool install -g dotnet-aspnet-codegenerator
	dotnet tool update -g dotnet-aspnet-codegenerator

# Build Project with the required controller
# This code will generate all the CRUD operations required for the API 
# Use the Data Transfer Object (DTO) approach to obfuscate info that shouldn't be passed to the API (e.g. secret_key)
.PHONY: update-controller
update-controller:
	dotnet aspnet-codegenerator controller -name MoveSetController -async -api -m MoveSetDTO -dc MoveSetContext -outDir Controllers


.PHONY: inst-packages
inst-packages:
	dotnet list package