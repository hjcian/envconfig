test:
	dotnet test EnvConfig --logger "console;verbosity=detailed"

pack:
	dotnet clean EnvConfig
	dotnet pack EnvConfig

# NOTE: change version to the local built version
install-from-local-pack:
	cd ExampleApp/ && \
	dotnet add package CS.EnvConfig --version 0.0.2 --source ../EnvConfig/bin/Release/

run-example-app:
	dotnet run --project ExampleApp