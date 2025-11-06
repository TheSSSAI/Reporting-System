# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-03-SHARED-CONTRACTS |
| Extraction Timestamp | 2024-07-28T10:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Fully Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

US-089

#### 1.2.1.2 Requirement Text

As an API User (System Integrator or Administrator), I want to perform full Create, Read, Update, and Delete (CRUD) operations on all system configurations via a secure RESTful API, so that I can automate the setup, management, and integration of the reporting system.

#### 1.2.1.3 Validation Criteria

- The API must correctly list, create, retrieve, update, and delete connector configurations.
- The API must correctly manage user accounts. GET responses must never include password hashes.

#### 1.2.1.4 Implementation Implications

- This repository must define Data Transfer Objects (DTOs) for all entities involved in CRUD operations (Users, Connectors, Reports, Transformations, etc.).
- Separate DTOs for request (Create/Update) and response (Read) models are required to control data exposure, e.g., omitting password hashes from UserResponseDTO.

#### 1.2.1.5 Extraction Reasoning

This requirement is the primary driver for creating a comprehensive set of DTOs for all system entities. The repository's main purpose is to house these contract definitions.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

US-087

#### 1.2.2.2 Requirement Text

As an API User (representing an external system or script), I want to submit my username and password to a secure authentication endpoint so that I receive a valid JSON Web Token (JWT) that I can use to authorize subsequent API requests.

#### 1.2.2.3 Validation Criteria

- The system responds with an HTTP 200 OK status code.
- The response body contains a JSON object with 'accessToken' (string), 'tokenType' (string, fixed to 'Bearer'), 'expiresIn' (integer, seconds), 'refreshToken' (string).

#### 1.2.2.4 Implementation Implications

- This repository must define a `LoginRequest` DTO with `username` and `password` fields.
- This repository must define a `LoginResponse` DTO with `accessToken`, `refreshToken`, and `expiresIn` fields.

#### 1.2.2.5 Extraction Reasoning

This requirement directly defines the data contract for the API's primary authentication workflow, which must be specified in this shared contracts repository.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

US-094

#### 1.2.3.2 Requirement Text

As an API User, I want to trigger a long-running report via an API call and immediately receive a 202 Accepted response with a job ID and a status URL.

#### 1.2.3.3 Validation Criteria

- The system MUST respond with an HTTP 202 Accepted status code.
- The response body MUST be a JSON object containing a non-null string 'jobId' and a 'statusUrl'.

#### 1.2.3.4 Implementation Implications

- A specific DTO, e.g., `AsyncJobSubmissionResponse`, must be defined in this repository to standardize the response for initiating asynchronous operations.

#### 1.2.3.5 Extraction Reasoning

This requirement specifies a key asynchronous API response contract (DTO) that must be defined in this shared library to ensure consistency between the API and its consumers.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-FUNC-DTR-003

#### 1.2.4.2 Requirement Text

The transformation engine shall catch exceptions from user scripts, log the full error details, and return a structured error object in the API response.

#### 1.2.4.3 Validation Criteria

- The API response body contains a JSON object with the structure {"error": {"message": "...", "stackTrace": "...", "lineNumber": ...}}.

#### 1.2.4.4 Implementation Implications

- A structured error DTO, e.g., `ApiErrorResponse` and a nested `ScriptErrorDetailDto`, must be defined to standardize how script execution errors are communicated via the API.

#### 1.2.4.5 Extraction Reasoning

This requirement defines a specific, structured error DTO for a key API feature (script preview), which must be included in the shared contracts to ensure a consistent error handling contract.

## 1.3.0.0 Relevant Components

- {'component_name': 'Data Transfer Objects (DTOs)', 'component_specification': 'A collection of plain C# objects (POCOs), primarily implemented as `record` types, that define the data structures for all API requests and responses. They act as the definitive contract between the backend API and any client. These objects contain properties and data annotations for validation but no business logic.', 'implementation_requirements': ['Must be simple POCOs/records without behavior or dependencies on frameworks like Entity Framework Core.', 'Must be optimized for serialization using System.Text.Json, using attributes like `[JsonPropertyName]` to control casing and naming.', 'Response DTOs (e.g., `UserDto`) must be designed to explicitly exclude sensitive data like password hashes.', 'Request DTOs (e.g., `UserCreateRequest`) can use data annotations (e.g., `[Required]`, `[MaxLength]`) for automatic model validation in ASP.NET Core.'], 'architectural_context': 'These objects constitute the entirety of the ReportingSystem.Shared.Contracts repository. They define the data contract for the API/Web Layer of the main application.', 'extraction_reasoning': 'This is the primary and sole type of component within this repository, as per its description and decomposition rationale. All relevant requirements map to the creation of these DTOs.'}

## 1.4.0.0 Architectural Layers

- {'layer_name': 'API Contracts Layer', 'layer_responsibilities': ["This layer's sole responsibility is to define the technology-agnostic data contracts (DTOs) and enumerations for the system's external RESTful API.", "It provides a stable, shared understanding of the data shapes for requests and responses, decoupling the presentation layer from the backend's internal domain models."], 'layer_constraints': ['Must not contain any business logic.', 'Must not have dependencies on any other application layers (e.g., Application, Domain, Infrastructure).', 'Objects must be simple data containers (POCOs/records).'], 'implementation_patterns': ['Data Transfer Object (DTO)', 'API Contract-First (Conceptual)'], 'extraction_reasoning': "The repository definition explicitly describes its role as the 'definitive, shared contract' that 'completely decouples the presentation layer from the backend's internal domain model', which perfectly defines its function as a dedicated API Contracts Layer in the architecture."}

## 1.5.0.0 Dependency Interfaces

*No items available*

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

Authentication DTOs

#### 1.6.1.2 Consumer Repositories

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

#### 1.6.1.3 Method Contracts

*No items available*

#### 1.6.1.4 Service Level Requirements

- All DTOs must be serializable to/from JSON.

#### 1.6.1.5 Implementation Constraints

- Defines the contract for API authentication as per US-087 and US-088.
- Includes `LoginRequest`, `LoginResponse`, and `RefreshTokenRequest` DTOs.

#### 1.6.1.6 Extraction Reasoning

These DTOs form the entry point for all secure API interactions. `REPO-08-SERVICE-HOST` uses them in its authentication controller. `REPO-09-WEB-UI` uses the contract (via generated TypeScript types) to build its login form and handle the returned JWTs.

### 1.6.2.0 Interface Name

#### 1.6.2.1 Interface Name

CRUD DTOs

#### 1.6.2.2 Consumer Repositories

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

#### 1.6.2.3 Method Contracts

*No items available*

#### 1.6.2.4 Service Level Requirements

- All DTOs must be serializable to/from JSON.

#### 1.6.2.5 Implementation Constraints

- Define separate models for Create, Update, and Response (e.g., `UserCreateRequest`, `UserUpdateRequest`, `UserDto`) to control data flow and prevent over-posting vulnerabilities.
- Response DTOs like `UserDto` MUST omit sensitive fields such as password hashes to prevent data leakage, fulfilling US-089.

#### 1.6.2.6 Extraction Reasoning

Based on US-089, this repository must expose a comprehensive set of DTOs for all configurable entities (Users, Connectors, Reports, etc.) to enable full automation and management via the API. `REPO-08-SERVICE-HOST` uses these for its management controllers, and `REPO-09-WEB-UI` consumes the contract to build management UIs.

### 1.6.3.0 Interface Name

#### 1.6.3.1 Interface Name

Asynchronous Job DTOs

#### 1.6.3.2 Consumer Repositories

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

#### 1.6.3.3 Method Contracts

*No items available*

#### 1.6.3.4 Service Level Requirements

*No items available*

#### 1.6.3.5 Implementation Constraints

- DTOs must be designed to support the asynchronous polling pattern described in US-094 and US-095.
- The `JobStatusResponse` DTO must include nullable fields for `endTime` and `resultUrl` to represent an in-progress job.

#### 1.6.3.6 Extraction Reasoning

Based on US-094 and US-095, this repository must expose DTOs that define the contract for initiating and polling asynchronous report generation jobs, which is a key API workflow consumed by both `REPO-08` and any client like `REPO-09`.

### 1.6.4.0 Interface Name

#### 1.6.4.1 Interface Name

Standardized Error DTOs

#### 1.6.4.2 Consumer Repositories

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

#### 1.6.4.3 Method Contracts

*No items available*

#### 1.6.4.4 Service Level Requirements

*No items available*

#### 1.6.4.5 Implementation Constraints

- A generic `ApiErrorResponse` DTO should be defined for common errors (e.g., validation failures).
- Specific error detail DTOs, like `ScriptErrorDetailDto`, must be defined for specialized error reporting as per REQ-FUNC-DTR-003.

#### 1.6.4.6 Extraction Reasoning

As specified in REQ-FUNC-DTR-003 and implied by the overall API design, this repository is responsible for defining the consistent structure of error messages returned by the API, ensuring a predictable experience for clients like `REPO-09-WEB-UI`.

## 1.7.0.0 Technology Context

### 1.7.1.0 Framework Requirements

The repository must be implemented as a .NET 8 Class Library, containing only plain C# objects (POCOs), preferably using C# `record` types for immutability.

### 1.7.2.0 Integration Technologies

- System.Text.Json: Used for serialization attributes (e.g., [JsonPropertyName]) to control the final JSON structure.
- System.ComponentModel.DataAnnotations: Used for basic validation attributes (e.g., [Required], [MaxLength]) on request DTOs, which are automatically enforced by ASP.NET Core.

### 1.7.3.0 Performance Constraints

DTOs should be lightweight and designed for efficient JSON serialization and deserialization to minimize API latency.

### 1.7.4.0 Security Requirements

Response DTOs must be carefully designed to omit any sensitive information, such as password hashes, secret keys, or internal system paths. This forms a critical part of the API's security boundary.

## 1.8.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | The repository's purpose is fully mapped. It exclu... |
| Cross Reference Validation | The repository's role as a dependency for the API ... |
| Implementation Readiness Assessment | Implementation readiness is high. The technology s... |
| Quality Assurance Confirmation | The extracted context is complete and internally c... |

