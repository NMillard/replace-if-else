# This repo is used for a [medium article]()

The purpose is to showcase three different approaches to how controller and actions are designed.  

1. gen1: Service class based controllers. One controller take a dependency on one or more service classes.
These controllers are notoriously difficult to deal with once the controllers grow large enough.
2. gen2: The controller takes a dependency on one or more command classes that act as a command and handler.
3. gen3: The controller only utilizes a special dispatcher to send commands. It doesn't care about which handlers
are invoked to deal with the command.

## Generations of controllers
  
### Prerequisites
- dotnet core 3.1
- MS-SQL database
- Redis

### Startup instructions

#### Connection strings
For anyone new or not used to dotnet, connection strings are simply a means to connect to external services.  

All connection strings should be set using the `dotnet user-secrets set 'key' 'value'`  

**Keys**  
SQL server: "ConnectionString:default"  
Azure queue: "ConnectionString:queue"  
Redis cache: "ConnectionString:cache"  

Have a look at the `appsettings.json` in the WebClient project.
  
This application is configured to require an MS-SQL database and redis cache. You can easily just pull the docker images for this.  
The azure queue storage is optional.

#### Database tables
1. Go to scripts folder
2. Run the `database.sql` script on the database you're using 

## HTTP Requests
I've created a collection of http requests in `http-tests/tests.http`.  
You can run these directly from JetBrains Rider.  



