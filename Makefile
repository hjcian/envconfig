test:
	dotnet test EnvConfig --logger "console;verbosity=detailed"

pack:
	dotnet clean EnvConfig
	dotnet pack EnvConfig


.EXPORT_ALL_VARIABLES:
NUGET_API_KEY ?= $(shell test -f .nuget_api_key && cat .nuget_api_key) # for local development
NUGET_API_KEY ?= $(NUGET_API_KEY_ENV) # for CI pipeline override

push:
	dotnet nuget push \
	EnvConfig/bin/Release/CS.EnvConfig.0.0.2.nupkg \
	--source nuget.org \
	--api-key $(NUGET_API_KEY)

# NOTE: change version to the local built version
install-from-local-pack:
	cd ExampleApp/ && \
	dotnet add package CS.EnvConfig --version 0.0.2 --source ../EnvConfig/bin/Release/

run-example-app:
	dotnet run --project ExampleApp