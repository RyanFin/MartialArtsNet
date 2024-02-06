.PHONY: run
run:
	dotnet run --launch-profile https

.PHONY: new-app
new-app:
	dotnet new webapi --use-controllers -o TodoApi

# 
.PHONY: trust-app
trust-app:
	dotnet dev-certs https --trust
