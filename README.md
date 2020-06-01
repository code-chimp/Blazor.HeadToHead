# Blazor vs Razor: Head to Head

Blazor: SPA performance without all of the front-end JavaScript https://blazor.net

A simple project to highlight a use case of Server-Side Blazor vs Razor Pages hopefully highlighting client side 
performance differences.

## Datastore Setup

Thought the easiest setup would be SQL Server running in a Docker container.  **NOTE:** If you want to go with 
your own local SQL Server instance you will likely need to change the connection string in `appsettings.json` as
the setup script is mapping the container to the non-standard port of __1455__.

- Generate a disposable SQL Server instance in Docker and run migrations to initialize databse
```shell
./setup.sh

# or on Windows

.\setup.ps1
```
- Stop and delete the temporary datastore

```shell
./teardown.sh

# or on Windows

.\teardown.ps1
```
- Convenience scripts to create and apply migrations
```shell
./add-migration.sh <MigrationName>
./update-db.sh

# or on Windows

.\add-migration.ps1 -migration <MigrationName>
.\update-db.ps1
```
