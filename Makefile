# run: export MY_ENV=haha
run: export GRPC_PORT=7777
run:
	dotnet run --project config-parser

test:
	dotnet test config-parser

local:
	dotnet run --project localtest