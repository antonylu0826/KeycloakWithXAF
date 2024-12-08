# KeycloakWithXAF

This repository demonstrates how to integrate **Keycloak** with **DevExpress XAF** to achieve Single Sign-On (SSO) functionality. It includes examples for both **Blazor** and **Web API Server** applications.

## Features

- Integration of Keycloak for secure SSO authentication.
- DevExpress XAF implementation with two modes:
  - **Blazor**: For building modern, interactive web applications.
  - **Web API**: For managing backend API services.
- Demonstrates seamless authentication and authorization mechanisms.

## Technology Stack

- **C#**
- **.NET 8**
- **DevExpress XAF**
- **Keycloak**

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/antonylu0826/KeycloakWithXAF.git

## Installation

1. Open the project in your preferred IDE (e.g., **Visual Studio** or **Rider**).
2. Restore the NuGet packages:
   ```bash
   dotnet restore
   ```
3. Configure the Keycloak settings:
   - Set up Keycloak with appropriate client credentials.
   - Update the configuration files in the project to match your Keycloak setup (e.g., client ID, secret, and authentication endpoints).
4. Build and run the project.

## Usage

### Blazor Application
- Start the **Blazor** project.
- Open your browser and navigate to the application's URL.
- Log in using your Keycloak credentials to test the SSO functionality.

### Web API
- Start the **Web API** project.
- Use tools like **Postman** or a custom client to test API endpoints.
- Ensure you include a valid authorization token in your requests.

## Contribution

Contributions are welcome! If you'd like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Description of your changes"
   ```
4. Push your changes:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request and provide details about your changes.

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for more information.

## Contact

If you have any questions or issues, please open an issue in the [GitHub Issues](https://github.com/antonylu0826/KeycloakWithXAF/issues) page.

## Screenshots

![image](https://github.com/antonylu0826/KeycloakWithXAF/blob/master/KeycloakWithXAF.gif)
