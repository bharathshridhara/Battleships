# Battleships
A Battleships game state tracking Api

**Deployment location:**
This Api is deployed at `https://battleships-api-52ynxxmukq-ts.a.run.app/index.html`

**Deploy locally:**
1. Clone this repository locally
2. Inside the repository, perform a dotnet build and publish
```
dotnet publish -c release
```
3. Create a docker image using the Dockerfile in the repo, and run it
```
docker build -t battleships -f Dockerfile .
docker run -d -p 5000:80 --name battleships battleships  

```
You can now use `localhost:5000` to interact with the Api.

**Swagger:**
The swagger URL is at `http://localhost:5000/index.html`

**Postman:**
There is a postman collection included in the repository that contains the Api calls with the request pre-populated. Please import the JSON file into Postman app and try the Api out.


