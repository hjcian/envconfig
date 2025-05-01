# run: export MY_ENV=haha
run: export GRPC_PORT=7777
run:
	dotnet run --project EnvConfig

test:
	dotnet test EnvConfig

local:
	dotnet run --project localtest