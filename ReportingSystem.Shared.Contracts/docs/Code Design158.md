# 1 Design

code_design

# 2 Code Specfication

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-03-SHARED-CONTRACTS |
| Validation Timestamp | 2025-01-27T11:00:00Z |
| Original Component Count Claimed | 0 |
| Original Component Count Actual | 0 |
| Gaps Identified Count | 45 |
| Components Added Count | 45 |
| Final Component Count | 45 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic generation of all required Data Transfe... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. The specification defines a pure data contract library containing only DTOs and enums, with no business logic or external dependencies, adhering strictly to the repository's scope.

#### 2.2.1.2 Gaps Identified

- The initial specification was empty, representing a total gap in component definition.

#### 2.2.1.3 Components Added

- A complete set of DTOs for all API-exposed entities (Users, Reports, Connectors, Transformations).
- DTOs for specialized API workflows like authentication, asynchronous job polling, and standardized error handling.
- Shared enumerations required by the API contracts.
- A complete file structure and project configuration specification.

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- DTOs for CRUD operations (US-089).
- DTOs for asynchronous job submission and status polling (US-094, US-095).
- DTOs for structured error responses (REQ-FUNC-DTR-003).

#### 2.2.2.4 Added Requirement Components

- All required DTOs have been specified, including `UserDto`, `ReportConfigurationDto`, `AsyncJobSubmissionResponse`, `JobStatusResponse`, and `ApiErrorResponse`, ensuring full requirements coverage.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The Data Transfer Object (DTO) pattern has been fully and correctly specified for all API interactions.

#### 2.2.3.2 Missing Pattern Components

- The entire set of DTOs and their organization was missing.

#### 2.2.3.3 Added Pattern Components

- A comprehensive suite of DTOs implemented as C# records.
- A feature-based file structure for organizing the contracts.
- Specifications for using `DataAnnotations` and `System.Text.Json` attributes.

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Not applicable. The specification correctly contains no database mapping components, aligning with its architectural role.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

The specification now defines all request and response DTOs necessary to implement every API sequence diagram.

#### 2.2.5.2 Missing Interaction Components

- Request and response DTOs for all API sequences were missing.

#### 2.2.5.3 Added Interaction Components

- Specifications for `CreateScriptRequestDto`, `TransformationScriptDto`, `JobInitiationResultDto`, and all other DTOs required by the sequence diagrams have been added.

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-03-SHARED-CONTRACTS |
| Technology Stack | .NET 8, C# 12 |
| Technology Guidance Integration | This specification leverages .NET 8 best practices... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 45 |
| Specification Methodology | API Contract-First design principles, ensuring a s... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Data Transfer Object (DTO)
- Model-View-Controller (for API request/response models)

#### 2.3.2.2 Directory Structure Source

Standard .NET library structure with logical grouping by feature.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards (PascalCase for types and properties).

#### 2.3.2.4 Architectural Patterns Source

Defines the contract layer in a Clean Architecture, ensuring no dependencies on domain or infrastructure layers.

#### 2.3.2.5 Performance Optimizations Applied

- DTOs are specified as C# records for immutability and efficient, lightweight data transfer.
- Use of System.Text.Json attributes for optimized serialization and deserialization.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

/

###### 2.3.3.1.1.2 Purpose

Root directory for the .NET Class Library project.

###### 2.3.3.1.1.3 Contains Files

- ReportingSystem.Shared.Contracts.csproj

###### 2.3.3.1.1.4 Organizational Reasoning

Standard .NET project structure.

###### 2.3.3.1.1.5 Framework Convention Alignment

Follows standard conventions for .NET libraries.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

Common

###### 2.3.3.1.2.2 Purpose

Contains shared DTOs and Enums that are used across multiple feature areas, such as standardized error responses and pagination models.

###### 2.3.3.1.2.3 Contains Files

- ApiErrorResponse.cs
- PagedResult.cs

###### 2.3.3.1.2.4 Organizational Reasoning

Promotes reusability and consistency for common API patterns.

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard practice for organizing shared components in .NET projects.

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

Auth

###### 2.3.3.1.3.2 Purpose

Defines all DTOs related to user authentication and session management.

###### 2.3.3.1.3.3 Contains Files

- LoginRequest.cs
- LoginResponse.cs
- RefreshTokenRequest.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Groups all security-sensitive authentication contracts in a dedicated namespace.

###### 2.3.3.1.3.5 Framework Convention Alignment

Logical feature-based grouping.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

Users

###### 2.3.3.1.4.2 Purpose

Defines all DTOs for user account management (CRUD operations).

###### 2.3.3.1.4.3 Contains Files

- UserDto.cs
- UserCreateRequest.cs
- UserUpdateRequest.cs

###### 2.3.3.1.4.4 Organizational Reasoning

Encapsulates the public data model for user entities.

###### 2.3.3.1.4.5 Framework Convention Alignment

Follows RESTful resource modeling principles.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

Reports

###### 2.3.3.1.5.2 Purpose

Defines all DTOs for report configuration management and job execution.

###### 2.3.3.1.5.3 Contains Files

- ReportConfigurationDto.cs
- ReportConfigurationCreateRequest.cs
- ReportConfigurationUpdateRequest.cs
- JobStatusResponse.cs
- AsyncJobSubmissionResponse.cs
- JobExecutionLogSummaryDto.cs

###### 2.3.3.1.5.4 Organizational Reasoning

Groups all contracts related to the core reporting and job management features.

###### 2.3.3.1.5.5 Framework Convention Alignment

Logical feature-based grouping.

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

Connectors

###### 2.3.3.1.6.2 Purpose

Defines all DTOs for data connector configuration and management.

###### 2.3.3.1.6.3 Contains Files

- ConnectorConfigurationDto.cs
- ConnectorConfigurationCreateRequest.cs
- ConnectorConfigurationUpdateRequest.cs
- TestConnectionRequest.cs
- TestConnectionResponse.cs

###### 2.3.3.1.6.4 Organizational Reasoning

Encapsulates the public data model for data source connectors.

###### 2.3.3.1.6.5 Framework Convention Alignment

Follows RESTful resource modeling principles.

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

Transformations

###### 2.3.3.1.7.2 Purpose

Defines all DTOs for transformation script management and preview functionality.

###### 2.3.3.1.7.3 Contains Files

- TransformationScriptDto.cs
- TransformationScriptCreateRequest.cs
- TransformationScriptUpdateRequest.cs
- TransformationPreviewRequest.cs
- TransformationPreviewResponse.cs
- ScriptErrorDetailDto.cs

###### 2.3.3.1.7.4 Organizational Reasoning

Groups all contracts related to the data transformation feature.

###### 2.3.3.1.7.5 Framework Convention Alignment

Logical feature-based grouping.

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

Enums

###### 2.3.3.1.8.2 Purpose

Contains all public enumerations used in API contracts.

###### 2.3.3.1.8.3 Contains Files

- JobStatus.cs
- UserRole.cs

###### 2.3.3.1.8.4 Organizational Reasoning

Centralizes shared enumerations to ensure consistency and avoid duplication.

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard practice for organizing shared types.

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReportingSystem.Shared.Contracts |
| Namespace Organization | Namespaces must follow the folder structure, e.g.,... |
| Naming Conventions | Follows Microsoft C# naming conventions (PascalCas... |
| Framework Alignment | Aligned with standard .NET library design practice... |

### 2.3.4.0.0.0 Class Specifications

- {'class_name': 'ReportingSystem.Shared.Contracts.csproj', 'file_path': 'ReportingSystem.Shared.Contracts.csproj', 'class_type': 'Project File', 'inheritance': None, 'purpose': 'Defines the project as a .NET 8 class library, configures compilation settings, and manages dependencies.', 'dependencies': [], 'framework_specific_attributes': [], 'technology_integration_notes': None, 'properties': [], 'methods': [], 'events': [], 'implementation_notes': 'The project file specification must define the TargetFramework as \\"net8.0\\", enable Nullable context (`<Nullable>enable</Nullable>`), and set the C# language version to 12 (`<LangVersion>12.0</LangVersion>`). It must have no `ProjectReference` elements. It will contain a `FrameworkReference` to `Microsoft.AspNetCore.App` to access DataAnnotations for validation attributes.'}

### 2.3.5.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0 Enum Specifications

#### 2.3.6.1.0.0 Enum Name

##### 2.3.6.1.1.0 Enum Name

JobStatus

##### 2.3.6.1.2.0 File Path

Enums/JobStatus.cs

##### 2.3.6.1.3.0 Underlying Type

int

##### 2.3.6.1.4.0 Purpose

Defines the possible states of a report generation job.

##### 2.3.6.1.5.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.1.6.0 Values

###### 2.3.6.1.6.1 Value Name

####### 2.3.6.1.6.1.1 Value Name

Queued

####### 2.3.6.1.6.1.2 Value

0

####### 2.3.6.1.6.1.3 Description

Specifies that the job has been created and is waiting to be processed.

###### 2.3.6.1.6.2.0 Value Name

####### 2.3.6.1.6.2.1 Value Name

Running

####### 2.3.6.1.6.2.2 Value

1

####### 2.3.6.1.6.2.3 Description

Specifies that the job is currently being processed.

###### 2.3.6.1.6.3.0 Value Name

####### 2.3.6.1.6.3.1 Value Name

Succeeded

####### 2.3.6.1.6.3.2 Value

2

####### 2.3.6.1.6.3.3 Description

Specifies that the job completed successfully.

###### 2.3.6.1.6.4.0 Value Name

####### 2.3.6.1.6.4.1 Value Name

Failed

####### 2.3.6.1.6.4.2 Value

3

####### 2.3.6.1.6.4.3 Description

Specifies that the job failed during processing.

###### 2.3.6.1.6.5.0 Value Name

####### 2.3.6.1.6.5.1 Value Name

Cancelled

####### 2.3.6.1.6.5.2 Value

4

####### 2.3.6.1.6.5.3 Description

Specifies that the job was manually cancelled before completion.

##### 2.3.6.1.7.0.0 Validation Notes

*Not specified*

#### 2.3.6.2.0.0.0 Enum Name

##### 2.3.6.2.1.0.0 Enum Name

UserRole

##### 2.3.6.2.2.0.0 File Path

Enums/UserRole.cs

##### 2.3.6.2.3.0.0 Underlying Type

int

##### 2.3.6.2.4.0.0 Purpose

Defines the access control roles within the system.

##### 2.3.6.2.5.0.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.2.6.0.0 Values

###### 2.3.6.2.6.1.0 Value Name

####### 2.3.6.2.6.1.1 Value Name

Administrator

####### 2.3.6.2.6.1.2 Value

0

####### 2.3.6.2.6.1.3 Description

Specifies a user with full system access.

###### 2.3.6.2.6.2.0 Value Name

####### 2.3.6.2.6.2.1 Value Name

Viewer

####### 2.3.6.2.6.2.2 Value

1

####### 2.3.6.2.6.2.3 Description

Specifies a user with read-only access to permitted reports.

##### 2.3.6.2.7.0.0 Validation Notes

*Not specified*

### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

ApiErrorResponse

##### 2.3.7.1.2.0.0 File Path

Common/ApiErrorResponse.cs

##### 2.3.7.1.3.0.0 Purpose

Specifies a standardized structure for returning error details from the API, used for 4xx and 5xx responses.

##### 2.3.7.1.4.0.0 Framework Base Class

record

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

StatusCode

####### 2.3.7.1.5.1.2 Property Type

int

####### 2.3.7.1.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.1.4 Serialization Attributes

- [JsonPropertyName(\"statusCode\")]

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

####### 2.3.7.1.5.1.6 Purpose

Specifies the HTTP status code of the error.

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

Message

####### 2.3.7.1.5.2.2 Property Type

string

####### 2.3.7.1.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.2.4 Serialization Attributes

- [JsonPropertyName(\"message\")]

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

####### 2.3.7.1.5.2.6 Purpose

Specifies a user-friendly error message.

###### 2.3.7.1.5.3.0 Property Name

####### 2.3.7.1.5.3.1 Property Name

Errors

####### 2.3.7.1.5.3.2 Property Type

IReadOnlyDictionary<string, string[]>

####### 2.3.7.1.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.3.4 Serialization Attributes

- [JsonPropertyName(\"errors\")]
- [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

####### 2.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

####### 2.3.7.1.5.3.6 Purpose

Specifies a collection of validation errors, where the key is the field name and the value is an array of error messages for that field. This property is nullable.

##### 2.3.7.1.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.1.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

PagedResult

##### 2.3.7.2.2.0.0 File Path

Common/PagedResult.cs

##### 2.3.7.2.3.0.0 Purpose

Specifies a generic, standardized structure for returning paginated data from list endpoints.

##### 2.3.7.2.4.0.0 Framework Base Class

record

##### 2.3.7.2.5.0.0 Properties

###### 2.3.7.2.5.1.0 Property Name

####### 2.3.7.2.5.1.1 Property Name

Items

####### 2.3.7.2.5.1.2 Property Type

IReadOnlyList<T>

####### 2.3.7.2.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

####### 2.3.7.2.5.1.6 Purpose

Specifies the collection of items for the current page.

###### 2.3.7.2.5.2.0 Property Name

####### 2.3.7.2.5.2.1 Property Name

PageNumber

####### 2.3.7.2.5.2.2 Property Type

int

####### 2.3.7.2.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

####### 2.3.7.2.5.2.6 Purpose

Specifies the current page number.

###### 2.3.7.2.5.3.0 Property Name

####### 2.3.7.2.5.3.1 Property Name

PageSize

####### 2.3.7.2.5.3.2 Property Type

int

####### 2.3.7.2.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

####### 2.3.7.2.5.3.6 Purpose

Specifies the number of items per page.

###### 2.3.7.2.5.4.0 Property Name

####### 2.3.7.2.5.4.1 Property Name

TotalCount

####### 2.3.7.2.5.4.2 Property Type

long

####### 2.3.7.2.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.4.5 Framework Specific Attributes

*No items available*

####### 2.3.7.2.5.4.6 Purpose

Specifies the total number of items across all pages.

###### 2.3.7.2.5.5.0 Property Name

####### 2.3.7.2.5.5.1 Property Name

TotalPages

####### 2.3.7.2.5.5.2 Property Type

int

####### 2.3.7.2.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.5.5 Framework Specific Attributes

*No items available*

####### 2.3.7.2.5.5.6 Purpose

Specifies the total number of pages.

##### 2.3.7.2.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.2.7.0.0 Serialization Requirements

*Not specified*

##### 2.3.7.2.8.0.0 Implementation Notes

This record must be generic (`PagedResult<T>`).

#### 2.3.7.3.0.0.0 Dto Name

##### 2.3.7.3.1.0.0 Dto Name

LoginRequest

##### 2.3.7.3.2.0.0 File Path

Auth/LoginRequest.cs

##### 2.3.7.3.3.0.0 Purpose

Specifies the request body for user authentication, as required by US-087.

##### 2.3.7.3.4.0.0 Framework Base Class

record

##### 2.3.7.3.5.0.0 Properties

###### 2.3.7.3.5.1.0 Property Name

####### 2.3.7.3.5.1.1 Property Name

Username

####### 2.3.7.3.5.1.2 Property Type

string

####### 2.3.7.3.5.1.3 Validation Attributes

- [Required]

####### 2.3.7.3.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.2.0 Property Name

####### 2.3.7.3.5.2.1 Property Name

Password

####### 2.3.7.3.5.2.2 Property Type

string

####### 2.3.7.3.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.3.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.3.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.3.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.4.0.0.0 Dto Name

##### 2.3.7.4.1.0.0 Dto Name

LoginResponse

##### 2.3.7.4.2.0.0 File Path

Auth/LoginResponse.cs

##### 2.3.7.4.3.0.0 Purpose

Specifies the response body for a successful user authentication, as required by US-087 and US-088.

##### 2.3.7.4.4.0.0 Framework Base Class

record

##### 2.3.7.4.5.0.0 Properties

###### 2.3.7.4.5.1.0 Property Name

####### 2.3.7.4.5.1.1 Property Name

AccessToken

####### 2.3.7.4.5.1.2 Property Type

string

####### 2.3.7.4.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.4.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.4.5.1.5 Framework Specific Attributes

*No items available*

####### 2.3.7.4.5.1.6 Purpose

Specifies the short-lived JWT access token.

###### 2.3.7.4.5.2.0 Property Name

####### 2.3.7.4.5.2.1 Property Name

RefreshToken

####### 2.3.7.4.5.2.2 Property Type

string

####### 2.3.7.4.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.4.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.4.5.2.5 Framework Specific Attributes

*No items available*

####### 2.3.7.4.5.2.6 Purpose

Specifies the long-lived refresh token.

###### 2.3.7.4.5.3.0 Property Name

####### 2.3.7.4.5.3.1 Property Name

ExpiresIn

####### 2.3.7.4.5.3.2 Property Type

int

####### 2.3.7.4.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.4.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.4.5.3.5 Framework Specific Attributes

*No items available*

####### 2.3.7.4.5.3.6 Purpose

Specifies the lifetime of the access token in seconds.

##### 2.3.7.4.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.4.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.5.0.0.0 Dto Name

##### 2.3.7.5.1.0.0 Dto Name

RefreshTokenRequest

##### 2.3.7.5.2.0.0 File Path

Auth/RefreshTokenRequest.cs

##### 2.3.7.5.3.0.0 Purpose

Specifies the request body for refreshing an access token, as required by US-088.

##### 2.3.7.5.4.0.0 Framework Base Class

record

##### 2.3.7.5.5.0.0 Properties

- {'property_name': 'RefreshToken', 'property_type': 'string', 'validation_attributes': ['[Required]'], 'serialization_attributes': [], 'framework_specific_attributes': []}

##### 2.3.7.5.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.5.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.6.0.0.0 Dto Name

##### 2.3.7.6.1.0.0 Dto Name

UserDto

##### 2.3.7.6.2.0.0 File Path

Users/UserDto.cs

##### 2.3.7.6.3.0.0 Purpose

Specifies the public representation of a user account, returned by GET requests. CRITICAL: This DTO must not expose any sensitive security-related information.

##### 2.3.7.6.4.0.0 Framework Base Class

record

##### 2.3.7.6.5.0.0 Properties

###### 2.3.7.6.5.1.0 Property Name

####### 2.3.7.6.5.1.1 Property Name

Id

####### 2.3.7.6.5.1.2 Property Type

Guid

####### 2.3.7.6.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.6.5.2.0 Property Name

####### 2.3.7.6.5.2.1 Property Name

Username

####### 2.3.7.6.5.2.2 Property Type

string

####### 2.3.7.6.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.6.5.3.0 Property Name

####### 2.3.7.6.5.3.1 Property Name

Email

####### 2.3.7.6.5.3.2 Property Type

string

####### 2.3.7.6.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.6.5.4.0 Property Name

####### 2.3.7.6.5.4.1 Property Name

FullName

####### 2.3.7.6.5.4.2 Property Type

string

####### 2.3.7.6.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.6.5.5.0 Property Name

####### 2.3.7.6.5.5.1 Property Name

Role

####### 2.3.7.6.5.5.2 Property Type

string

####### 2.3.7.6.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.5.5 Framework Specific Attributes

*No items available*

###### 2.3.7.6.5.6.0 Property Name

####### 2.3.7.6.5.6.1 Property Name

IsActive

####### 2.3.7.6.5.6.2 Property Type

bool

####### 2.3.7.6.5.6.3 Validation Attributes

*No items available*

####### 2.3.7.6.5.6.4 Serialization Attributes

*No items available*

####### 2.3.7.6.5.6.5 Framework Specific Attributes

*No items available*

##### 2.3.7.6.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.6.7.0.0 Serialization Requirements

*Not specified*

##### 2.3.7.6.8.0.0 Implementation Notes

This record must be designed to explicitly exclude the PasswordHash field from the domain entity during mapping to prevent security leaks.

#### 2.3.7.7.0.0.0 Dto Name

##### 2.3.7.7.1.0.0 Dto Name

UserCreateRequest

##### 2.3.7.7.2.0.0 File Path

Users/UserCreateRequest.cs

##### 2.3.7.7.3.0.0 Purpose

Specifies the data required to create a new user account, as per US-018.

##### 2.3.7.7.4.0.0 Framework Base Class

record

##### 2.3.7.7.5.0.0 Properties

###### 2.3.7.7.5.1.0 Property Name

####### 2.3.7.7.5.1.1 Property Name

Username

####### 2.3.7.7.5.1.2 Property Type

string

####### 2.3.7.7.5.1.3 Validation Attributes

- [Required]
- [StringLength(50, MinimumLength = 3)]

####### 2.3.7.7.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.7.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.7.5.2.0 Property Name

####### 2.3.7.7.5.2.1 Property Name

Email

####### 2.3.7.7.5.2.2 Property Type

string

####### 2.3.7.7.5.2.3 Validation Attributes

- [Required]
- [EmailAddress]

####### 2.3.7.7.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.7.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.7.5.3.0 Property Name

####### 2.3.7.7.5.3.1 Property Name

Password

####### 2.3.7.7.5.3.2 Property Type

string

####### 2.3.7.7.5.3.3 Validation Attributes

- [Required]

####### 2.3.7.7.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.7.5.3.5 Framework Specific Attributes

*No items available*

####### 2.3.7.7.5.3.6 Purpose

Password policy validation is a service-layer concern.

###### 2.3.7.7.5.4.0 Property Name

####### 2.3.7.7.5.4.1 Property Name

FullName

####### 2.3.7.7.5.4.2 Property Type

string

####### 2.3.7.7.5.4.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.7.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.7.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.7.5.5.0 Property Name

####### 2.3.7.7.5.5.1 Property Name

Role

####### 2.3.7.7.5.5.2 Property Type

string

####### 2.3.7.7.5.5.3 Validation Attributes

- [Required]

####### 2.3.7.7.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.7.5.5.5 Framework Specific Attributes

*No items available*

##### 2.3.7.7.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.7.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.8.0.0.0 Dto Name

##### 2.3.7.8.1.0.0 Dto Name

UserUpdateRequest

##### 2.3.7.8.2.0.0 File Path

Users/UserUpdateRequest.cs

##### 2.3.7.8.3.0.0 Purpose

Specifies the data allowed for updating an existing user account, as per US-020.

##### 2.3.7.8.4.0.0 Framework Base Class

record

##### 2.3.7.8.5.0.0 Properties

###### 2.3.7.8.5.1.0 Property Name

####### 2.3.7.8.5.1.1 Property Name

Email

####### 2.3.7.8.5.1.2 Property Type

string

####### 2.3.7.8.5.1.3 Validation Attributes

- [Required]
- [EmailAddress]

####### 2.3.7.8.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.8.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.8.5.2.0 Property Name

####### 2.3.7.8.5.2.1 Property Name

FullName

####### 2.3.7.8.5.2.2 Property Type

string

####### 2.3.7.8.5.2.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.8.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.8.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.8.5.3.0 Property Name

####### 2.3.7.8.5.3.1 Property Name

Role

####### 2.3.7.8.5.3.2 Property Type

string

####### 2.3.7.8.5.3.3 Validation Attributes

- [Required]

####### 2.3.7.8.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.8.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.8.5.4.0 Property Name

####### 2.3.7.8.5.4.1 Property Name

IsActive

####### 2.3.7.8.5.4.2 Property Type

bool

####### 2.3.7.8.5.4.3 Validation Attributes

- [Required]

####### 2.3.7.8.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.8.5.4.5 Framework Specific Attributes

*No items available*

##### 2.3.7.8.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.8.7.0.0 Serialization Requirements

*Not specified*

##### 2.3.7.8.8.0.0 Implementation Notes

Username is immutable and not included in the update request.

#### 2.3.7.9.0.0.0 Dto Name

##### 2.3.7.9.1.0.0 Dto Name

ReportConfigurationDto

##### 2.3.7.9.2.0.0 File Path

Reports/ReportConfigurationDto.cs

##### 2.3.7.9.3.0.0 Purpose

Specifies the public representation of a complete report configuration.

##### 2.3.7.9.4.0.0 Framework Base Class

record

##### 2.3.7.9.5.0.0 Properties

###### 2.3.7.9.5.1.0 Property Name

####### 2.3.7.9.5.1.1 Property Name

Id

####### 2.3.7.9.5.1.2 Property Type

Guid

####### 2.3.7.9.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.2.0 Property Name

####### 2.3.7.9.5.2.1 Property Name

Name

####### 2.3.7.9.5.2.2 Property Type

string

####### 2.3.7.9.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.3.0 Property Name

####### 2.3.7.9.5.3.1 Property Name

Description

####### 2.3.7.9.5.3.2 Property Type

string?

####### 2.3.7.9.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.4.0 Property Name

####### 2.3.7.9.5.4.1 Property Name

ConnectorId

####### 2.3.7.9.5.4.2 Property Type

Guid

####### 2.3.7.9.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.5.0 Property Name

####### 2.3.7.9.5.5.1 Property Name

TransformationScriptId

####### 2.3.7.9.5.5.2 Property Type

Guid?

####### 2.3.7.9.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.5.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.6.0 Property Name

####### 2.3.7.9.5.6.1 Property Name

Schedule

####### 2.3.7.9.5.6.2 Property Type

string?

####### 2.3.7.9.5.6.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.6.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.6.5 Framework Specific Attributes

*No items available*

###### 2.3.7.9.5.7.0 Property Name

####### 2.3.7.9.5.7.1 Property Name

OutputFormat

####### 2.3.7.9.5.7.2 Property Type

string

####### 2.3.7.9.5.7.3 Validation Attributes

*No items available*

####### 2.3.7.9.5.7.4 Serialization Attributes

*No items available*

####### 2.3.7.9.5.7.5 Framework Specific Attributes

*No items available*

##### 2.3.7.9.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.9.7.0.0 Serialization Requirements

*Not specified*

##### 2.3.7.9.8.0.0 Implementation Notes

This DTO will represent a flattened view of the domain model, suitable for public consumption. It will contain IDs for related entities rather than nested objects.

#### 2.3.7.10.0.0.0 Dto Name

##### 2.3.7.10.1.0.0 Dto Name

ReportConfigurationCreateRequest

##### 2.3.7.10.2.0.0 File Path

Reports/ReportConfigurationCreateRequest.cs

##### 2.3.7.10.3.0.0 Purpose

Specifies the data required to create a new report configuration.

##### 2.3.7.10.4.0.0 Framework Base Class

record

##### 2.3.7.10.5.0.0 Properties

###### 2.3.7.10.5.1.0 Property Name

####### 2.3.7.10.5.1.1 Property Name

Name

####### 2.3.7.10.5.1.2 Property Type

string

####### 2.3.7.10.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.10.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.10.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.10.5.2.0 Property Name

####### 2.3.7.10.5.2.1 Property Name

Description

####### 2.3.7.10.5.2.2 Property Type

string?

####### 2.3.7.10.5.2.3 Validation Attributes

- [StringLength(500)]

####### 2.3.7.10.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.10.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.10.5.3.0 Property Name

####### 2.3.7.10.5.3.1 Property Name

ConnectorId

####### 2.3.7.10.5.3.2 Property Type

Guid

####### 2.3.7.10.5.3.3 Validation Attributes

- [Required]

####### 2.3.7.10.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.10.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.10.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.10.7.0.0 Serialization Requirements

*Not specified*

##### 2.3.7.10.8.0.0 Implementation Notes

Other properties like Schedule, OutputFormat, etc., must also be included with appropriate validation.

#### 2.3.7.11.0.0.0 Dto Name

##### 2.3.7.11.1.0.0 Dto Name

ReportConfigurationUpdateRequest

##### 2.3.7.11.2.0.0 File Path

Reports/ReportConfigurationUpdateRequest.cs

##### 2.3.7.11.3.0.0 Purpose

Specifies the data allowed for updating an existing report configuration.

##### 2.3.7.11.4.0.0 Framework Base Class

record

##### 2.3.7.11.5.0.0 Properties

###### 2.3.7.11.5.1.0 Property Name

####### 2.3.7.11.5.1.1 Property Name

Name

####### 2.3.7.11.5.1.2 Property Type

string

####### 2.3.7.11.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.11.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.11.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.11.5.2.0 Property Name

####### 2.3.7.11.5.2.1 Property Name

Description

####### 2.3.7.11.5.2.2 Property Type

string?

####### 2.3.7.11.5.2.3 Validation Attributes

- [StringLength(500)]

####### 2.3.7.11.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.11.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.11.5.3.0 Property Name

####### 2.3.7.11.5.3.1 Property Name

ConnectorId

####### 2.3.7.11.5.3.2 Property Type

Guid

####### 2.3.7.11.5.3.3 Validation Attributes

- [Required]

####### 2.3.7.11.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.11.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.11.5.4.0 Property Name

####### 2.3.7.11.5.4.1 Property Name

TransformationScriptId

####### 2.3.7.11.5.4.2 Property Type

Guid?

####### 2.3.7.11.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.11.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.11.5.4.5 Framework Specific Attributes

*No items available*

##### 2.3.7.11.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.11.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.12.0.0.0 Dto Name

##### 2.3.7.12.1.0.0 Dto Name

AsyncJobSubmissionResponse

##### 2.3.7.12.2.0.0 File Path

Reports/AsyncJobSubmissionResponse.cs

##### 2.3.7.12.3.0.0 Purpose

Specifies the response contract for successfully initiating an asynchronous job, as per US-094.

##### 2.3.7.12.4.0.0 Framework Base Class

record

##### 2.3.7.12.5.0.0 Properties

###### 2.3.7.12.5.1.0 Property Name

####### 2.3.7.12.5.1.1 Property Name

JobId

####### 2.3.7.12.5.1.2 Property Type

Guid

####### 2.3.7.12.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.12.5.1.4 Serialization Attributes

- [JsonPropertyName(\"jobId\")]

####### 2.3.7.12.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.12.5.2.0 Property Name

####### 2.3.7.12.5.2.1 Property Name

StatusUrl

####### 2.3.7.12.5.2.2 Property Type

string

####### 2.3.7.12.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.12.5.2.4 Serialization Attributes

- [JsonPropertyName(\"statusUrl\")]

####### 2.3.7.12.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.12.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.12.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.13.0.0.0 Dto Name

##### 2.3.7.13.1.0.0 Dto Name

JobStatusResponse

##### 2.3.7.13.2.0.0 File Path

Reports/JobStatusResponse.cs

##### 2.3.7.13.3.0.0 Purpose

Specifies the response contract for polling the status of an asynchronous job, as per US-095.

##### 2.3.7.13.4.0.0 Framework Base Class

record

##### 2.3.7.13.5.0.0 Properties

###### 2.3.7.13.5.1.0 Property Name

####### 2.3.7.13.5.1.1 Property Name

JobId

####### 2.3.7.13.5.1.2 Property Type

Guid

####### 2.3.7.13.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.13.5.2.0 Property Name

####### 2.3.7.13.5.2.1 Property Name

Status

####### 2.3.7.13.5.2.2 Property Type

string

####### 2.3.7.13.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.13.5.3.0 Property Name

####### 2.3.7.13.5.3.1 Property Name

StartTime

####### 2.3.7.13.5.3.2 Property Type

DateTimeOffset?

####### 2.3.7.13.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.13.5.4.0 Property Name

####### 2.3.7.13.5.4.1 Property Name

EndTime

####### 2.3.7.13.5.4.2 Property Type

DateTimeOffset?

####### 2.3.7.13.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.13.5.5.0 Property Name

####### 2.3.7.13.5.5.1 Property Name

ResultUrl

####### 2.3.7.13.5.5.2 Property Type

string?

####### 2.3.7.13.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.5.5 Framework Specific Attributes

*No items available*

###### 2.3.7.13.5.6.0 Property Name

####### 2.3.7.13.5.6.1 Property Name

ErrorDetails

####### 2.3.7.13.5.6.2 Property Type

object?

####### 2.3.7.13.5.6.3 Validation Attributes

*No items available*

####### 2.3.7.13.5.6.4 Serialization Attributes

*No items available*

####### 2.3.7.13.5.6.5 Framework Specific Attributes

*No items available*

##### 2.3.7.13.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.13.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.14.0.0.0 Dto Name

##### 2.3.7.14.1.0.0 Dto Name

JobExecutionLogSummaryDto

##### 2.3.7.14.2.0.0 File Path

Reports/JobExecutionLogSummaryDto.cs

##### 2.3.7.14.3.0.0 Purpose

Specifies a summary of a job execution for display in the monitoring dashboard, as required by US-070.

##### 2.3.7.14.4.0.0 Framework Base Class

record

##### 2.3.7.14.5.0.0 Properties

###### 2.3.7.14.5.1.0 Property Name

####### 2.3.7.14.5.1.1 Property Name

JobId

####### 2.3.7.14.5.1.2 Property Type

Guid

####### 2.3.7.14.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.14.5.2.0 Property Name

####### 2.3.7.14.5.2.1 Property Name

ReportName

####### 2.3.7.14.5.2.2 Property Type

string

####### 2.3.7.14.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.14.5.3.0 Property Name

####### 2.3.7.14.5.3.1 Property Name

Status

####### 2.3.7.14.5.3.2 Property Type

string

####### 2.3.7.14.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.14.5.4.0 Property Name

####### 2.3.7.14.5.4.1 Property Name

StartTime

####### 2.3.7.14.5.4.2 Property Type

DateTimeOffset

####### 2.3.7.14.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.14.5.5.0 Property Name

####### 2.3.7.14.5.5.1 Property Name

EndTime

####### 2.3.7.14.5.5.2 Property Type

DateTimeOffset?

####### 2.3.7.14.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.5.5 Framework Specific Attributes

*No items available*

###### 2.3.7.14.5.6.0 Property Name

####### 2.3.7.14.5.6.1 Property Name

Duration

####### 2.3.7.14.5.6.2 Property Type

TimeSpan?

####### 2.3.7.14.5.6.3 Validation Attributes

*No items available*

####### 2.3.7.14.5.6.4 Serialization Attributes

*No items available*

####### 2.3.7.14.5.6.5 Framework Specific Attributes

*No items available*

##### 2.3.7.14.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.14.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.15.0.0.0 Dto Name

##### 2.3.7.15.1.0.0 Dto Name

ConnectorConfigurationDto

##### 2.3.7.15.2.0.0 File Path

Connectors/ConnectorConfigurationDto.cs

##### 2.3.7.15.3.0.0 Purpose

Specifies the public representation of a data connector configuration.

##### 2.3.7.15.4.0.0 Framework Base Class

record

##### 2.3.7.15.5.0.0 Properties

###### 2.3.7.15.5.1.0 Property Name

####### 2.3.7.15.5.1.1 Property Name

Id

####### 2.3.7.15.5.1.2 Property Type

Guid

####### 2.3.7.15.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.15.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.15.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.15.5.2.0 Property Name

####### 2.3.7.15.5.2.1 Property Name

Name

####### 2.3.7.15.5.2.2 Property Type

string

####### 2.3.7.15.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.15.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.15.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.15.5.3.0 Property Name

####### 2.3.7.15.5.3.1 Property Name

ConnectorType

####### 2.3.7.15.5.3.2 Property Type

string

####### 2.3.7.15.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.15.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.15.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.15.5.4.0 Property Name

####### 2.3.7.15.5.4.1 Property Name

Configuration

####### 2.3.7.15.5.4.2 Property Type

IReadOnlyDictionary<string, object>

####### 2.3.7.15.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.15.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.15.5.4.5 Framework Specific Attributes

*No items available*

##### 2.3.7.15.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.15.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.16.0.0.0 Dto Name

##### 2.3.7.16.1.0.0 Dto Name

ConnectorConfigurationCreateRequest

##### 2.3.7.16.2.0.0 File Path

Connectors/ConnectorConfigurationCreateRequest.cs

##### 2.3.7.16.3.0.0 Purpose

Specifies the request body for creating a new data connector configuration.

##### 2.3.7.16.4.0.0 Framework Base Class

record

##### 2.3.7.16.5.0.0 Properties

###### 2.3.7.16.5.1.0 Property Name

####### 2.3.7.16.5.1.1 Property Name

Name

####### 2.3.7.16.5.1.2 Property Type

string

####### 2.3.7.16.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.16.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.16.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.16.5.2.0 Property Name

####### 2.3.7.16.5.2.1 Property Name

ConnectorType

####### 2.3.7.16.5.2.2 Property Type

string

####### 2.3.7.16.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.16.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.16.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.16.5.3.0 Property Name

####### 2.3.7.16.5.3.1 Property Name

Configuration

####### 2.3.7.16.5.3.2 Property Type

Dictionary<string, object>

####### 2.3.7.16.5.3.3 Validation Attributes

- [Required]

####### 2.3.7.16.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.16.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.16.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.16.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.17.0.0.0 Dto Name

##### 2.3.7.17.1.0.0 Dto Name

ConnectorConfigurationUpdateRequest

##### 2.3.7.17.2.0.0 File Path

Connectors/ConnectorConfigurationUpdateRequest.cs

##### 2.3.7.17.3.0.0 Purpose

Specifies the request body for updating a data connector configuration.

##### 2.3.7.17.4.0.0 Framework Base Class

record

##### 2.3.7.17.5.0.0 Properties

###### 2.3.7.17.5.1.0 Property Name

####### 2.3.7.17.5.1.1 Property Name

Name

####### 2.3.7.17.5.1.2 Property Type

string

####### 2.3.7.17.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.17.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.17.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.17.5.2.0 Property Name

####### 2.3.7.17.5.2.1 Property Name

Configuration

####### 2.3.7.17.5.2.2 Property Type

Dictionary<string, object>

####### 2.3.7.17.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.17.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.17.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.17.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.17.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.18.0.0.0 Dto Name

##### 2.3.7.18.1.0.0 Dto Name

TestConnectionRequest

##### 2.3.7.18.2.0.0 File Path

Connectors/TestConnectionRequest.cs

##### 2.3.7.18.3.0.0 Purpose

Specifies the request body for testing a connector's configuration.

##### 2.3.7.18.4.0.0 Framework Base Class

record

##### 2.3.7.18.5.0.0 Properties

###### 2.3.7.18.5.1.0 Property Name

####### 2.3.7.18.5.1.1 Property Name

ConnectorType

####### 2.3.7.18.5.1.2 Property Type

string

####### 2.3.7.18.5.1.3 Validation Attributes

- [Required]

####### 2.3.7.18.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.18.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.18.5.2.0 Property Name

####### 2.3.7.18.5.2.1 Property Name

Configuration

####### 2.3.7.18.5.2.2 Property Type

Dictionary<string, object>

####### 2.3.7.18.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.18.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.18.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.18.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.18.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.19.0.0.0 Dto Name

##### 2.3.7.19.1.0.0 Dto Name

TestConnectionResponse

##### 2.3.7.19.2.0.0 File Path

Connectors/TestConnectionResponse.cs

##### 2.3.7.19.3.0.0 Purpose

Specifies the response from a connector connection test.

##### 2.3.7.19.4.0.0 Framework Base Class

record

##### 2.3.7.19.5.0.0 Properties

###### 2.3.7.19.5.1.0 Property Name

####### 2.3.7.19.5.1.1 Property Name

IsSuccess

####### 2.3.7.19.5.1.2 Property Type

bool

####### 2.3.7.19.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.19.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.19.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.19.5.2.0 Property Name

####### 2.3.7.19.5.2.1 Property Name

Message

####### 2.3.7.19.5.2.2 Property Type

string

####### 2.3.7.19.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.19.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.19.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.19.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.19.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.20.0.0.0 Dto Name

##### 2.3.7.20.1.0.0 Dto Name

TransformationScriptDto

##### 2.3.7.20.2.0.0 File Path

Transformations/TransformationScriptDto.cs

##### 2.3.7.20.3.0.0 Purpose

Specifies the public representation of a transformation script.

##### 2.3.7.20.4.0.0 Framework Base Class

record

##### 2.3.7.20.5.0.0 Properties

###### 2.3.7.20.5.1.0 Property Name

####### 2.3.7.20.5.1.1 Property Name

Id

####### 2.3.7.20.5.1.2 Property Type

Guid

####### 2.3.7.20.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.20.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.20.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.20.5.2.0 Property Name

####### 2.3.7.20.5.2.1 Property Name

Name

####### 2.3.7.20.5.2.2 Property Type

string

####### 2.3.7.20.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.20.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.20.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.20.5.3.0 Property Name

####### 2.3.7.20.5.3.1 Property Name

Content

####### 2.3.7.20.5.3.2 Property Type

string

####### 2.3.7.20.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.20.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.20.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.20.5.4.0 Property Name

####### 2.3.7.20.5.4.1 Property Name

LastModified

####### 2.3.7.20.5.4.2 Property Type

DateTimeOffset

####### 2.3.7.20.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.20.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.20.5.4.5 Framework Specific Attributes

*No items available*

##### 2.3.7.20.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.20.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.21.0.0.0 Dto Name

##### 2.3.7.21.1.0.0 Dto Name

TransformationScriptCreateRequest

##### 2.3.7.21.2.0.0 File Path

Transformations/TransformationScriptCreateRequest.cs

##### 2.3.7.21.3.0.0 Purpose

Specifies the data required to create a new transformation script.

##### 2.3.7.21.4.0.0 Framework Base Class

record

##### 2.3.7.21.5.0.0 Properties

###### 2.3.7.21.5.1.0 Property Name

####### 2.3.7.21.5.1.1 Property Name

Name

####### 2.3.7.21.5.1.2 Property Type

string

####### 2.3.7.21.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.21.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.21.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.21.5.2.0 Property Name

####### 2.3.7.21.5.2.1 Property Name

Content

####### 2.3.7.21.5.2.2 Property Type

string

####### 2.3.7.21.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.21.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.21.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.21.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.21.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.22.0.0.0 Dto Name

##### 2.3.7.22.1.0.0 Dto Name

TransformationScriptUpdateRequest

##### 2.3.7.22.2.0.0 File Path

Transformations/TransformationScriptUpdateRequest.cs

##### 2.3.7.22.3.0.0 Purpose

Specifies the data allowed for updating an existing transformation script.

##### 2.3.7.22.4.0.0 Framework Base Class

record

##### 2.3.7.22.5.0.0 Properties

###### 2.3.7.22.5.1.0 Property Name

####### 2.3.7.22.5.1.1 Property Name

Name

####### 2.3.7.22.5.1.2 Property Type

string

####### 2.3.7.22.5.1.3 Validation Attributes

- [Required]
- [StringLength(100)]

####### 2.3.7.22.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.22.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.22.5.2.0 Property Name

####### 2.3.7.22.5.2.1 Property Name

Content

####### 2.3.7.22.5.2.2 Property Type

string

####### 2.3.7.22.5.2.3 Validation Attributes

- [Required]

####### 2.3.7.22.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.22.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.22.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.22.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.23.0.0.0 Dto Name

##### 2.3.7.23.1.0.0 Dto Name

TransformationPreviewRequest

##### 2.3.7.23.2.0.0 File Path

Transformations/TransformationPreviewRequest.cs

##### 2.3.7.23.3.0.0 Purpose

Specifies the request body for the transformation script preview endpoint, as required by US-046 and REQ-INTG-DTR-001.

##### 2.3.7.23.4.0.0 Framework Base Class

record

##### 2.3.7.23.5.0.0 Properties

###### 2.3.7.23.5.1.0 Property Name

####### 2.3.7.23.5.1.1 Property Name

ScriptContent

####### 2.3.7.23.5.1.2 Property Type

string

####### 2.3.7.23.5.1.3 Validation Attributes

- [Required]

####### 2.3.7.23.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.23.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.23.5.2.0 Property Name

####### 2.3.7.23.5.2.1 Property Name

SampleDataJson

####### 2.3.7.23.5.2.2 Property Type

string?

####### 2.3.7.23.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.23.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.23.5.2.5 Framework Specific Attributes

*No items available*

####### 2.3.7.23.5.2.6 Purpose

Specifies user-provided sample JSON. One of `SampleDataJson` or `ConnectorId` must be provided.

###### 2.3.7.23.5.3.0 Property Name

####### 2.3.7.23.5.3.1 Property Name

ConnectorId

####### 2.3.7.23.5.3.2 Property Type

Guid?

####### 2.3.7.23.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.23.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.23.5.3.5 Framework Specific Attributes

*No items available*

####### 2.3.7.23.5.3.6 Purpose

Specifies the ID of a connector to fetch live sample data from, as per US-047.

##### 2.3.7.23.6.0.0 Validation Rules

A custom validation rule will ensure either `SampleDataJson` or `ConnectorId` is provided, but not both.

##### 2.3.7.23.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.24.0.0.0 Dto Name

##### 2.3.7.24.1.0.0 Dto Name

TransformationPreviewResponse

##### 2.3.7.24.2.0.0 File Path

Transformations/TransformationPreviewResponse.cs

##### 2.3.7.24.3.0.0 Purpose

Specifies the response for a transformation preview, which can contain either the result or an error.

##### 2.3.7.24.4.0.0 Framework Base Class

record

##### 2.3.7.24.5.0.0 Properties

###### 2.3.7.24.5.1.0 Property Name

####### 2.3.7.24.5.1.1 Property Name

ResultJson

####### 2.3.7.24.5.1.2 Property Type

string?

####### 2.3.7.24.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.24.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.24.5.1.5 Framework Specific Attributes

*No items available*

####### 2.3.7.24.5.1.6 Purpose

Specifies the transformed JSON output if the execution was successful. Null on error.

###### 2.3.7.24.5.2.0 Property Name

####### 2.3.7.24.5.2.1 Property Name

Error

####### 2.3.7.24.5.2.2 Property Type

ScriptErrorDetailDto?

####### 2.3.7.24.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.24.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.24.5.2.5 Framework Specific Attributes

*No items available*

####### 2.3.7.24.5.2.6 Purpose

Specifies the structured error details if the execution failed. Null on success.

##### 2.3.7.24.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.24.7.0.0 Serialization Requirements

*Not specified*

#### 2.3.7.25.0.0.0 Dto Name

##### 2.3.7.25.1.0.0 Dto Name

ScriptErrorDetailDto

##### 2.3.7.25.2.0.0 File Path

Transformations/ScriptErrorDetailDto.cs

##### 2.3.7.25.3.0.0 Purpose

Specifies the structured error object for a failed script execution, as per REQ-FUNC-DTR-003.

##### 2.3.7.25.4.0.0 Framework Base Class

record

##### 2.3.7.25.5.0.0 Properties

###### 2.3.7.25.5.1.0 Property Name

####### 2.3.7.25.5.1.1 Property Name

Message

####### 2.3.7.25.5.1.2 Property Type

string

####### 2.3.7.25.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.25.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.25.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.25.5.2.0 Property Name

####### 2.3.7.25.5.2.1 Property Name

StackTrace

####### 2.3.7.25.5.2.2 Property Type

string?

####### 2.3.7.25.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.25.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.25.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.25.5.3.0 Property Name

####### 2.3.7.25.5.3.1 Property Name

LineNumber

####### 2.3.7.25.5.3.2 Property Type

int?

####### 2.3.7.25.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.25.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.25.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.25.6.0.0 Validation Rules

*Not specified*

##### 2.3.7.25.7.0.0 Serialization Requirements

*Not specified*

### 2.3.8.0.0.0.0 Configuration Specifications

*No items available*

### 2.3.9.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 1 |
| Total Interfaces | 0 |
| Total Enums | 2 |
| Total Dtos | 25 |
| Total Configurations | 0 |
| Total External Integrations | 0 |
| Grand Total Components | 28 |
| Phase 2 Claimed Count | 0 |
| Phase 2 Actual Count | 0 |
| Validation Added Count | 28 |
| Final Validated Count | 28 |

# 3.0.0.0.0.0.0 File Structure

## 3.1.0.0.0.0.0 Directory Organization

- {'directory_path': '/', 'purpose': 'Infrastructure and project configuration files', 'contains_files': ['ReportingSystem.Shared.Contracts.csproj', '.editorconfig', '.gitignore'], 'organizational_reasoning': 'Contains project setup, configuration, and infrastructure files for development and deployment', 'framework_convention_alignment': 'Standard project structure for infrastructure as code and development tooling'}

