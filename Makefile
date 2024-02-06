.PHONY: run
run:
	dotnet run --launch-profile https

.PHONY: new-app
new-app:
	dotnet new webapi --use-controllers -o MartialArtsNet
	dotnet add package Microsoft.EntityFrameworkCore.InMemory

# 
.PHONY: trust-app
trust-app:
	dotnet dev-certs https --trust
