
<div align="center">

[//]: # (    <img src="static/logo.png" alt="frontEASE Logo" width="300">)

```
███████╗██████╗  ██████╗ ███╗   ██╗████████╗███████╗ █████╗ ███████╗███████╗
██╔════╝██╔══██╗██╔═══██╗████╗  ██║╚══██╔══╝██╔════╝██╔══██╗██╔════╝██╔════╝
█████╗  ██████╔╝██║   ██║██╔██╗ ██║   ██║   █████╗  ███████║███████╗█████╗  
██╔══╝  ██╔══██╗██║   ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██║╚════██║██╔══╝  
██║     ██║  ██║╚██████╔╝██║ ╚████║   ██║   ███████╗██║  ██║███████║███████╗
╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝
```
</div>

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

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [Docker](https://www.docker.com/get-started) installed for containerized deployment.

## Installation
-->

1. **Clone the Repository**:

   ```sh
   git clone --recurse-submodules https://github.com/TBU-AILab/frontEASE.git
   cd frontEASE
   ```
<!--
2. **Restore Dependencies**:

   ```sh
   dotnet restore
   ```
-->
## Running the Application

### Development Mode

To run frontEASE in development mode with live reloading:

```sh
docker-compose -f docker-compose.override.yml up
```

This command builds and starts the application using the `dev.Dockerfile` and applies the development-specific configurations.

### Production Mode

To run frontEASE in a production environment:

```sh
docker-compose up -d
```

This command uses the `prod.Dockerfile` to build and start the application in detached mode.

## Backend Integration

frontEASE is designed to work in tandem with the EASE backend. Ensure the backend services are running and accessible. The frontend communicates with the backend via predefined API endpoints.

<!--
## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new feature branch.
3. Commit your changes.
4. Submit a pull request.
-->
## License

This project is licensed under the [MIT License](LICENSE).

## Contact

For inquiries, please contact [ailab@fai.utb.cz](mailto:ailab@fai.utb.cz).