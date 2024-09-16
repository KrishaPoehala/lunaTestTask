# lunaTestTask
This is an ASP.NET Core Web API project built with .NET 8 and PostgreSQL as the database. The project is containerized using Docker, and the setup instructions below will guide you through running it on your local machine.

## Prerequisites

Before you start, ensure you have the following installed on your machine:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Git](https://git-scm.com/)

## Cloning the Repository

To clone this repository, run the following command:
```bash
git clone https://github.com/yourusername/LunaTestTask.git
```
Navigate to the project directory:
``` bash
cd LunaTestTask
```
### Setting Up Environment Variables
Make sure you have a .env file in the root directory (same as your docker-compose.yml file). The .env file should look like this:

``` bash
DB_USER=postgres
DB_PASSWORD=yourpassword
DB_NAME=luna
DB_HOST=lunatesttask.database
DB_PORT=5438

JWT_SECRET=your_jwt_secret_key
```
Replace yourpassword and your_jwt_secret_key with appropriate values.
Before running the project, you need to create a volume to store PostgreSQL data persistently. Run the following command:

```bash
docker volume create pgdata
```
Now you can run the project using Docker Compose. This will build and start both the application and the PostgreSQL database.
```bash
docker-compose up --build
```
This command will:

Build the Docker image for the lunatesttask service.
Start the PostgreSQL container using the volume for persistent data storage.
Expose ports 5000 and 5001 for the API, and port 5432 for the PostgreSQL database.

## Accessing the Application
Once everything is up and running, you can access the API at:

HTTP: http://localhost:5000
HTTPS: https://localhost:5001
The PostgreSQL database is accessible on port 5432.
You can access the swagger you by going: https://localhost:5001/swagger/index.html

## Shutting Down the Services
To stop and remove the containers, run:

```bash
docker-compose down
```










