```
███████╗██████╗  ██████╗ ███╗   ██╗████████╗███████╗ █████╗ ███████╗███████╗
██╔════╝██╔══██╗██╔═══██╗████╗  ██║╚══██╔══╝██╔════╝██╔══██╗██╔════╝██╔════╝
█████╗  ██████╔╝██║   ██║██╔██╗ ██║   ██║   █████╗  ███████║███████╗█████╗  
██╔══╝  ██╔══██╗██║   ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██║╚════██║██╔══╝  
██║     ██║  ██║╚██████╔╝██║ ╚████║   ██║   ███████╗██║  ██║███████║███████╗
╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝
```


# 🎯 frontEASE - Frontend for Effortless Algorithmic Solution Evolution

🚀 **frontEASE** is the frontend component of the [EASE framework](https://github.com/TBU-AILab/EASE), providing a user-friendly interface to interact with the backend services. It is built using C# and integrates seamlessly with the backend to offer a cohesive user experience.

### Disclaimer

This is an open beta version of the framework.

<!--
## Features

- **Responsive Interface**: Built with HTML and CSS to ensure a responsive and intuitive user experience.
- **Seamless Backend Integration**: Communicates effectively with the EASE backend for real-time data exchange.
- **Modular Architecture**: Organized codebase facilitating easy maintenance and scalability.

## Project Structure

```
frontEASE/
├── src/                        # Source code directory
│   ├── Components/             # Reusable UI components
│   ├── Pages/                  # Application pages
│   ├── Services/               # Backend service integrations
│   └── frontEASE.csproj        # Project file
├── .editorconfig               # Editor configuration
├── .gitattributes              # Git attributes
├── .gitignore                  # Git ignore rules
├── .gitmodules                 # Git submodules configuration
├── FoP_IMT.sln                 # Solution file
├── dev-docker-entrypoint.sh    # Development Docker entrypoint script
├── dev.Dockerfile              # Development Dockerfile
├── docker-compose.override.yml # Docker Compose override for development
├── docker-compose.yml          # Docker Compose configuration
├── docker-entrypoint.sh        # Production Docker entrypoint script
└── prod.Dockerfile             # Production Dockerfile
```
-->

## How to run it - Windows

### Prerequisites

- [Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) installed to clone this repository.
- [Docker](https://www.docker.com/get-started) installed for containerized deployment.

### Installation
1. **Clone the Repository**:

   From the command line at the location where you want to have the project (e.g. _C:\Users\JohnDoe_)
   ```sh
   git clone --recurse-submodules https://github.com/TBU-AILab/frontEASE.git
   ```
2. **Go into the project directory**
   
   ```sh
   cd frontEASE
   ```

1. **Create .env file**
    In the root folder (where readme file is located), create a file named `.env` - the file must have the following structure:
    ```sh
    # If you change the values for user and password, you will need to change
    # the connection string in appsettings.json and appsettings.Development.json
    POSTGRES_USER="postgres" # required
    POSTGRES_PASSWORD="root" # required

    # Options: true | false
    # If true, the database will be deleted and re-created with the seed data
    # If false, the database will be used as is
    SEED_DB="false" # optional, defaults to false
    ```

3. **Compose docker containers**

   >The docker has to be running for this step.
   
   - If running locally for development, use:
   ```sh
   docker compose up -d
   ```
   - If running in production, use:
   ```sh
   docker compose -f .\docker-compose.yml up -d
   ```

4. **Profit**

   Go to:
   ```sh
   http://localhost:5235
   ```

   **Default Login**
   
   _username_: BigJoe
   
   _password_: root1234
   
<!--
## Running the Application - Windows

### Development Mode

To run frontEASE in development mode with live reloading:

```sh
docker compose -f docker-compose.override.yml up #windows
docker-compose -f docker-compose.override.yml up #unix
```

This command builds and starts the application using the `dev.Dockerfile` and applies the development-specific configurations.

### Production Mode

To run frontEASE in a production environment:

```sh
docker compose up -d #windows
docker-compose up -d #unix
```

This command uses the `prod.Dockerfile` to build and start the application in detached mode.

## Backend Integration

frontEASE is designed to work in tandem with the EASE backend. Ensure the backend services are running and accessible. The frontend communicates with the backend via predefined API endpoints.


## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new feature branch.
3. Commit your changes.
4. Submit a pull request.
-->
## License

This work is licensed under a [Creative Commons Attribution 4.0 International License](https://creativecommons.org/licenses/by/4.0/).

## Contact

For inquiries, please contact [ailab@fai.utb.cz](mailto:ailab@fai.utb.cz).
