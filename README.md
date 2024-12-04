## May they fight the good fight and survive.

### Join the discord
[![Discord](https://img.shields.io/discord/1313564843600511098?color=7289DA&label=Discord&logo=discord&logoColor=white)](https://discord.gg/z6XrHevnmY)

### Repo-Rules
* Always branch off with your tasks with one of these templates. 
    policy: _one liners, under 50-char_ 
    1. doc: update/create/remove-{because}
    2. feat: built this thing
    3. bug: fixed a thing
    4. test: stuff to do with testing
* Comment - Methods with full summary tag. /// purpose? args[]
* have fun, leave comedic comments if you'd like. No excessive swearing(the hard words).

## Our Solution Overview
![Retro-Shop-Infra.png](docs/assets/retro-shop-infra.png)

### Important Links
* [Consul](http://localhost:8500)
* [Retro-Shop](http://localhost:5001)
* [Keycloak](http://localhost:8080)
* [Swagger-Pages](http://localhost:*/swagger/index.html)

### Shop Services
* [hellooo-api](http://localhost:5000/hellooo-api/swagger/index.html)
* [ads-api](http://localhost:5000/ads-api/swagger/index.html)
* [ads-admin-api](http://localhost:5000/ads-admin-api/swagger/index.html)
* [stock-api](http://localhost:5000/stock-api/swagger/index.html)
* [retro-payments](http://localhost:5000/retro-payments/swagger/index.html)
* [cart-api](http://localhost:5000/cart-api/swagger/index.html)
* [pay-me-api](http://localhost:5000/pay-me-api/swagger/index.html)
* [orders-api](http://localhost:5000/orders-api/swagger/index.html)
* [profile-api](http://localhost:5000/profile-api/swagger/index.html)

## How to run
1. Clone the repo
2. Open terminal in the root directory (works best with WSL terminal)
   1. wsl: `cd /mnt/{drive-letter}/{path-to-repo}` & `docker-compose up -d`
3. Run `docker-compose up -d` to start the services
4. Open `http://localhost:8500` to see the consul dashboard

## How to stop
1. Open terminal in the root directory
2. Run `docker-compose down -v` to stop the services
   > [!NOTE]  
   > The `-v` flag is used to remove the volumes as well if you want to keep your data, remove `-v`

## How to use compose in development
1. Open terminal in the root directory
2. Run `docker-compose build` to build the services
3. Run `docker-compose up -d` to start the services
4. If you make changes to the code, you can run `docker-compose up -d --build` to rebuild the services
5. Open `http://localhost:8500` to see the consul dashboard
6. Run `docker-compose down -v` to stop the services



# TO-DO's
* [Bounties](#open-bounties) 
* Docs


# Tech stack


# Questions go here?


# Open Bounties

#### Retro.Ads
* Create the service, register & setup seeding
* Create libs @ `/services/libs/**`
* Make sure to create seeder strategy
* Add Repo's, services, controllers
* Create a bruno collection @ `/bruno/**`

#### Retro.Ads.Admin
* Create the service, register & setup seeding
* Create libs @ `/services/libs/**`
* Make sure to create seeder strategy
* Add Repo's, services, controllers
* Create a bruno collection @ `/bruno/**`

#### Retro.Cart
* Create the service, register & setup seeding
* Create libs @ `/services/libs/**`
* Make sure to create seeder strategy
* Add Repo's, services, controllers
* Create a bruno collection @ `/bruno/**`

#### Retro.Greeter
* Create the service, register & setup seeding
* Create libs @ `/services/libs/**`
* Make sure to create seeder strategy
* Add Repo's, services, controllers
* Create a bruno collection @ `/bruno/**`