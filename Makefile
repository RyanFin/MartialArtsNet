.PHONY: run
run:
	dotnet watch run

.PHONE: new-app
new-app:
	dotnet new webapi --use-controllers -o TodoApi