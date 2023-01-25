# ASP.NET Core Api Cardano

For api that related to Cardano.
We create this repo since query with Cardano node takes long running time.


## Quick Start

	```bash
	# Clone the project
	cd api-cardano
	git checkout develop

	# Generate files for current environment.
	# Lets modify setting in result files to match with current env.
	cp Properties/launchSettings.sample Properties/launchSettings.json

	# Modify app settings to match with current env.
	cp appsettings.sample appsettings.json
	nano appsettings.json

	# Obtain submodules for this project
	# Note: to pull submodules, go with: git submodule init; git submodule update --remote --merge
	./git-pull.sh

	# [Optional] For Apple silicon chip arm64, we need create link of dotnet x64
	sudo ln -s /usr/local/share/dotnet/x64/dotnet /usr/local/bin/

	# Start server manually.
	# For IIS server, just restart the site.
	dotnet run

	# View api doc at
	http://localhost:8100/swagger
	```
