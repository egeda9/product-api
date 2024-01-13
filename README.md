# Development of Web API for Product Management

## 1. Database

### Product Table

Create a database to store product information. The product table should have the following structure:

- **ProductID (Primary Key):** Unique identifier for each product.
- **Name:** The name of the product.
- **Price:** The price of the product.

### User Table

Create a user table to store user information. This table should have at least the following fields:

- **UserID (Primary Key):** Unique identifier for each user.
- **Username:** The username of the user.
- **Password:** The hashed password of the user.

## 2. API

### Product Endpoints

#### Create a Product

- **Endpoint:** `POST /api/product`
- **Parameters:**
  - `Name` (string): The name of the product.
  - `Price` (decimal): The price of the product.
- **Return Value:** The created product with its unique identifier.

#### Read Products

- **Endpoint:** `GET /api/product`
- **Return Value:** List of all products.

#### Update a Product

- **Endpoint:** `PUT /api/product/{productId}`
- **Parameters:**
  - `Name` (string): The updated name of the product.
  - `Price` (decimal): The updated price of the product.
- **Return Value:** The updated product.

#### Delete a Product

- **Endpoint:** `DELETE /api/product/{productId}`
- **Return Value:** Status indicating the success of the operation.

### User Endpoints

#### Create a User

- **Endpoint:** `POST /api/user`
- **Parameters:**
  - `Username` (string): The username of the new user.
  - `Password` (string): The password of the new user.
- **Return Value:** The created user with its unique identifier.

#### User Login

- **Endpoint:** `POST /api/user/login`
- **Parameters:**
  - `Username` (string): The username of the user.
  - `Password` (string): The password of the user.
- **Return Value:** Token for authentication.

## 3. Clean Architecture 

This project follows the principles of Clean Architecture to ensure a modular, maintainable, and scalable design. Clean Architecture emphasizes the separation of concerns and the independence of components, allowing for flexibility and ease of testing.

## Principles

1. **Separation of Concerns:**
   - Clear distinction between the core business logic, interfaces, and external dependencies.
   - Each layer has a specific responsibility and does not depend on the details of other layers.

2. **Independence of Frameworks:**
   - The core business logic is independent of any external framework, database, or UI.
   - Frameworks are used on the outer layers, allowing for easy replacement or upgrades.

3. **Test-Driven Development (TDD):**
   - Unit tests are an integral part of the development process, ensuring that each component is thoroughly tested in isolation.
   - Tests validate the correctness of business logic, data access, and API functionality.

4. **Isolation of Business Logic:**
   - Business rules and validation are encapsulated in a dedicated business logic layer.
   - The business logic layer is decoupled from the data access layer and the API, promoting maintainability.

## Project Structure

The project is organized into distinct layers, each serving a specific purpose:

- **Database Layer:** Manages data storage and retrieval.
- **Data Access Layer:** Interacts with the database and provides CRUD operations.
- **Business Logic Layer:** Enforces business rules and validation independently of data access and API concerns.
- **API Layer:** Exposes endpoints for product management and user actions.
- **Unit Tests:** Ensures the integrity and functionality of each layer through comprehensive test coverage.

## 4. Docker

This project leverages Docker for containerization, providing a consistent and reproducible environment for seamless deployment across different systems. Docker enables developers to package the application along with its dependencies, ensuring portability and ease of deployment.

## Docker Usage

### Local Development

1. **Containerized Environment:**
   - The application can be run locally in a containerized environment using Docker.
   - Dependencies, including the database or data storage, are encapsulated within Docker containers.

2. **Development Image:**
   - A Docker image is provided for local development, enabling developers to work in an isolated environment.
   - Developers can spin up the application and its dependencies with a single command.

### Deployment

1. **Consistent Deployment:**
   - Docker ensures consistent deployment across different environments, minimizing potential issues related to dependencies or configurations.

2. **Docker Compose:**
   - Docker Compose is utilized for multi-container deployments.
   - The `docker-compose.yml` file defines the services and configurations needed to run the entire application stack.

3. **Database Deployment:**
   - The database or data storage can be deployed as a separate container, ensuring a decoupled and scalable architecture.


##  5. Swagger Usage: http://localhost/swagger

This project incorporates Swagger for API documentation and testing. Swagger provides a dynamic and interactive documentation interface that allows developers to understand, test, and interact with the API endpoints seamlessly.

### API Documentation

1. **Interactive API Documentation:**
   - Swagger generates interactive API documentation, making it easy for developers to explore available endpoints and understand request and response formats.

2. **Endpoint Details:**
   - Each API endpoint is documented with details such as HTTP methods, parameters, request bodies, and response structures.

### API Testing

1. **In-Browser Testing:**
   - Swagger enables in-browser testing of API endpoints, allowing developers to send requests and view responses directly from the Swagger UI.

2. **Request Samples:**
   - Sample requests can be generated and executed within the Swagger UI, facilitating testing and validation of different scenarios.

### Integration with Development Workflow

1. **Code Generation:**
   - Swagger supports code generation, allowing developers to generate client SDKs in various programming languages based on the API specification.

2. **Collaboration:**
   - The Swagger specification serves as a central source of truth for the API, fostering collaboration between development and documentation teams.