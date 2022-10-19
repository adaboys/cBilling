# ASP.NET Core Api server

This is asp.net core api server.


## Quick Start

	```bash
	# Clone the project
	cd api-server
	git checkout develop

	# Generate files for current environment.
	# Lets modify setting in result files to match with current env.
	cp Properties/launchSettings.sample Properties/launchSettings.json

	# Modify app settings to match with current env.
	cp appsettings.sample appsettings.json
	nano appsettings.json

	# Obtain submodules for this project
	# Note: to pull submodules, go with: git submodule init; git submodule update --remote --merge
	 #ubuntu
	 ./git-pull.sh
     #window
	 .\git-pull.bat
	
	# [Optional] For Apple silicon chip arm64, we need create link of dotnet x64
	sudo ln -s /usr/local/share/dotnet/x64/dotnet /usr/local/bin/

	# Start server manually.
	# For IIS server, just restart the site.
	dotnet run

	# View api doc at
	http://localhost:8100/swagger
	```


## Tips

- Migration

	```bash
	# Create a migration
	dotnet ef migrations add InitDB
	dotnet ef migrations add CreateUserTable

	# Migrate (apply) the changes to database
	dotnet ef database update

	# Delete database
	dotnet ef database drop -f
	```

- Public objects in aws

> Ref: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example-bucket-policies.html
> Granting read-only permission to an anonymous user

	```json
	{
		"Version": "2012-10-17",
		"Statement": [
				{
						"Sid": "PublicRead",
						"Effect": "Allow",
						"Principal": "*",
						"Action": [
								"s3:GetObject",
								"s3:GetObjectVersion"
						],
						"Resource": [
								"arn:aws:s3:::DOC-EXAMPLE-BUCKET/*"
						]
				}
		]
	}
	```
