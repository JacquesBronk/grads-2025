# WELCOME! To the Agile Bridge dotnet training day

Here is where you'll put into practice what we've learned so far. You will be tested on your general knowledge of what you've learned and will be expected to apply what you've learned.


## Business Proposition

Our primary source of income is in the sales of retro games, to that effect we've developed a store that would service our clients needs for any retro gaming fix. 

We've charged the agile bridge with this development task using cloud native technologies so that we can leverage scalable, reliable, secure and performant transactions for our clients.

We want our store to become the premier store in the world to buy your retro games.

## Our Solution Overview
![Retro-Shop-Infra.png](docs/assets/retro-shop-infra.png)


### Important Links
* [Consul](http://localhost:8500)
  * used for configuring your application
* [Keycloak](http://localhost:8080)


## How to run
1. Clone the repo
2. Checkout your development branch (it will be your name)
3. Open terminal in the root directory of the repository and run the following command in the root directory of the project. e.g. `c:/grads-2025/`
   1.  `docker-compose up -d`
   2. This will start building all of these service, note it may take some time
4. Open `http://localhost:8500` to see the consul dashboard

## How to stop
1. Open terminal in the root directory
2. Run `docker-compose down -v` to stop the services
   > [!NOTE]  
   > The `-v` flag is used to remove the volumes as well if you want to keep your data, remove `-v`

## How to use compose in development
1. Open terminal in the root directory
2. Run `docker-compose build` to build the services
3. Run `docker-compose up -d` to start the services (No need to Stop the services again, just run the up command so the latest image is being used)
   1. If you make changes to the code, you can run `docker-compose up -d --build` to rebuild the services


## The Challenge

You are expected to do the following.

* Fix all the tests in the project, this will be scored. https://retroscore.azurewebsites.net/
* Refactor the profile API so that it follows the same solution structure as the rest of the projects.
* Build and design the Vendor API, look at /docs/Vendor-Spec.md for details
* You get 1 Freebie question from any of the trainers, and after that it will cost 20 pinnies & double with every additional question.
* You have until 3PM today, whereafter you need to present your vendor api and explain why you chose to go down the route.
* Scores will be tallied at the end of the day, and the top 3 will receive some pinnies.

## How to run the tests
1. Make sure that you have run `docker-compose build` & `docker-compose up -d` this is required.
2. Open your terminal and run the following command:
   1. Powershell: `./run-tests`
   2. CMD: `run-tests`


## Hints

### Use the Bruno Collection
We have included the bruno api collection in the solution folder, under the "bruno" directory. Use it to assist in your debugging efforts.

### I want to debug locally
You will have to update the configuration in consul under "Key / Value", Then "app-settings", "vendor-api" (NOTE! Every time you build and `docker-compose up -d` this will be reset.) DO NOT CHANGE THE json files located in the init directory.

You can update your configuration to the following:
```json
{
    "ConnectionStrings": {
      "Redis": "localhost:6379,abortConnect=false,ssl=false,allowAdmin=true,connectTimeout=5000,syncTimeout=5000,connectRetry=5,defaultDatabase=0",
      "Mongo": "mongodb://localhost:27017",
      "RabbitMq": "amqp://guest:guest@retro-rabbitmq:5672"
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "Mongo": {
      "Database": "vendor-db-dev"
    },
    "AllowedHosts": "*",
    "Keycloak": {
      "Authority": "http://localhost:8080/realms/retro-realm",
      "PublicClient": true,
      "ClientId": "retro-client",
      "SslRequired": "none",
      "ConfidentialPort": 0,
      "VerifyTokenAudience": false,
      "ClientSercret":  "k6LE3kUdj18kMa6eewhBWHLJTSeBPF2r"
    },
    "ServiceDetails": {
      "ID": "vendors-api",
      "Name": "vendors-api",
      "Address": "vendors-api",
      "Port": 8080,
      "Tags": ["api", "vendors"],
      "Check": {
        "HTTP": "http://vendors-api:8080/health",
        "Interval": "10",
        "Timeout": "5"
      }
    }
  }
```