# ASP.NET Core Api server

This is asp.net core api server.


## How this project was made


#### Make project at local

	```bash
	# Check available templates to choose best suitable template to proceed.
	dotnet new --list

	# Create webapi project with name as `CBilling`.
	# But we will rename folder to api-partner.
	dotnet new webapi -o CBilling
	mv CBilling api-partner
	cd api-partner

	# Init git
	# This is good time to create files as: .editorconfig, .gitignore,...
	git init

	# [Setup] Trust the HTTPS development certificate
	dotnet dev-certs https --trust

	# Cleanup and Add submodules
	rm Controllers WeatherForecast.cs
	cp Properties/launchSettings.json Properties/launchSettings.sample
	mv appsettings.Development.json appsettings.sample
	cp appsettings.sample appsettings.json

	mkdir Source; mv Program.cs Source/

	# NOTE: we should add submodule at main branch first, so can add more
	# at another branches (develop,...).
	# This is important behavior since when first pull/checkout at another place
	# will normally checkout main branch first, so submodule is initialized well.
	mkdir Shared && cd Shared
	git submodule add git@gitlab.com:hoanb-cbilling/api-core.git
	cd ..

	mkdir Tool; mkdir Tool/Compet; cd Tool/Compet;
	git submodule add https://github.com/darkcompet/csharp-core.git
	git submodule add https://github.com/darkcompet/csharp-asp-core.git
	git submodule add https://github.com/darkcompet/csharp-net-core.git
	git submodule add https://github.com/darkcompet/csharp-net-json.git
	git submodule add https://github.com/darkcompet/csharp-net-log.git
	git submodule add https://github.com/darkcompet/csharp-core-http.git
	git submodule add https://github.com/darkcompet/csharp-net-http.git
	git submodule add https://github.com/darkcompet/csharp-net-efcore.git
	cd ../../

	# [Modify setting files]
	# Don't forget copy to sample file after modified.
	nano app/appsettings.json
	nano app/Properties/launchSettings.json

	# [Add JWT authentication packages]
	# Ref: https://www.c-sharpcorner.com/article/asp-net-core-web-api-5-0-authentication-using-jwtjson-base-token/
	dotnet add package Microsoft.AspNetCore.Authentication
	dotnet add package System.IdentityModel.Tokens.Jwt
	dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

	# [Add EntityFrameworkCore packages]
	dotnet add package Microsoft.EntityFrameworkCore
	dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	dotnet add package Microsoft.EntityFrameworkCore.Tools

	# [File Logging]
	dotnet add package Serilog.AspNetCore

	# Decode OAuth
	dotnet add package Google.Apis.Auth

	# Swagger for api doc
	dotnet add package Swashbuckle.AspNetCore
	```
