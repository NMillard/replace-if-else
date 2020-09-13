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
- MS-SQL database, you can use docker if'd like

### Startup instructions

#### Database connection setup
1. Go to Medium.ReplacingIfElse.WebClient
2. Open appsettings.Development.json
3. Insert your connection string to the database

or

Set the connection string in your donet user-secrets.

#### Database tables
1. Go to scripts folder
2. Run the `database.sql` script on the database you're using 

#### Azure Queues
1. Set the `ConnectionStrings:queue` setting if you'd like to use the messaging service and background service.

## HTTP Requests
I've created a collection of http requests in `http-tests/tests.http`.  
You can run these directly from JetBrains Rider.  



