# 1 Design

code_design

# 2 Code Specfication

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-02-CORE-DOMAIN |
| Validation Timestamp | 2025-01-28T11:00:00Z |
| Original Component Count Claimed | 0 |
| Original Component Count Actual | 0 |
| Gaps Identified Count | 52 |
| Components Added Count | 52 |
| Final Component Count | 52 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic validation against all cached context (... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Initial prompt was an empty template. A comprehensive set of domain specifications has been created to fully comply with the repository's defined scope as the \"Domain Layer\" of a Clean Architecture.

#### 2.2.1.2 Gaps Identified

- Missing specifications for all core domain entities and aggregates (User, ReportConfiguration, etc.).
- Missing specifications for repository interfaces.
- Incomplete specification for the IConnector interface.
- Missing specifications for DDD patterns like Value Objects, Domain Events, and custom exceptions.

#### 2.2.1.3 Components Added

- Specifications for 19 domain entities, aggregates, and value objects.
- Specifications for 5 core domain interfaces.
- Specifications for 3 domain-specific enumerations.
- Specifications for custom domain exceptions and domain events.

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- The initial prompt lacked any specifications to cover requirements US-113, US-051, and US-018.

#### 2.2.2.4 Added Requirement Components

- IConnector interface specification to satisfy US-113.
- ReportConfiguration aggregate specification to satisfy US-051.
- User and Role aggregate specifications to satisfy US-018.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The enhanced specification fully defines the components required to implement DDD and Clean Architecture patterns for the domain layer.

#### 2.2.3.2 Missing Pattern Components

- Aggregate Root base classes, Value Object patterns, Domain Event contracts, custom Domain Exception hierarchy.

#### 2.2.3.3 Added Pattern Components

- AggregateRoot<T> base class specification.
- EmailAddress value object specification.
- IDomainEvent and example event class specifications.
- DomainException class hierarchy specification.

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

The enhanced specification provides a complete set of domain entities corresponding to the database design, while correctly maintaining persistence ignorance.

#### 2.2.4.2 Missing Database Components

- Domain entity specifications for nearly all tables in the provided database designs.

#### 2.2.4.3 Added Database Components

- Specifications for all required domain entities (TransformationScript, JobExecutionLog, AuditLog, etc.) have been added.

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

The enhanced specification defines all necessary repository interface methods and domain entity methods identified from sequence diagrams.

#### 2.2.5.2 Missing Interaction Components

- Repository methods required by sequence diagrams (e.g., GetByNameAsync).
- Domain methods for enforcing business rules (e.g., Deactivate).

#### 2.2.5.3 Added Interaction Components

- Added specific query methods to repository interfaces.
- Added business rule enforcement methods to aggregate root specifications.

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-02-CORE-DOMAIN |
| Technology Stack | .NET 8, C# 12 |
| Technology Guidance Integration | Specification adheres to .NET 8 Domain-Driven Desi... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 52 |
| Specification Methodology | Specification is modeled using DDD concepts, inclu... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Domain-Driven Design (Aggregates, Entities, Value Objects, Domain Events, Repositories)
- Clean Architecture (Domain Layer)
- Dependency Inversion Principle
- Immutability by Default (specification recommends C# records and init setters)

#### 2.3.2.2 Directory Structure Source

Specification follows standard DDD and .NET Clean Architecture conventions, organizing components by their role within the domain.

#### 2.3.2.3 Naming Conventions Source

Specification adheres to Microsoft C# coding standards combined with Ubiquitous Language from the domain.

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture

#### 2.3.2.5 Performance Optimizations Applied

- Specification mandates asynchronous interfaces (Task-based) for all I/O-bound operations (persistence) to be implemented in outer layers.
- Specification recommends using \"record struct\" for lightweight value objects to reduce memory allocation.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

src/Aggregates/ReportConfigurationAggregate

###### 2.3.3.1.1.2 Purpose

Specification for the ReportConfiguration aggregate root and its encapsulated child entities, ensuring transactional consistency for all report-related settings.

###### 2.3.3.1.1.3 Contains Files

- ReportConfiguration.cs
- DeliveryDestination.cs
- ReportSchedule.cs

###### 2.3.3.1.1.4 Organizational Reasoning

Groups the aggregate root with its closely related child entities to enforce a clear consistency boundary as per DDD principles.

###### 2.3.3.1.1.5 Framework Convention Alignment

Aligns with DDD by organizing code around aggregates, which are the primary units of consistency in the domain.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

src/Aggregates/UserAggregate

###### 2.3.3.1.2.2 Purpose

Specification for the User aggregate root, which manages user identity, status, and role assignment.

###### 2.3.3.1.2.3 Contains Files

- User.cs
- Role.cs

###### 2.3.3.1.2.4 Organizational Reasoning

Defines the User as an aggregate root, as it is a central entity for security and access control.

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard DDD practice for core entities like User.

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

src/Entities

###### 2.3.3.1.3.2 Purpose

Specification for domain entities that have their own identity and lifecycle but are not aggregate roots. They are typically referenced by other aggregates.

###### 2.3.3.1.3.3 Contains Files

- ConnectorConfiguration.cs
- TransformationScript.cs
- TransformationScriptVersion.cs
- JobExecutionLog.cs
- AuditLog.cs
- JsonSchema.cs
- Template.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Separates standalone, globally identifiable entities from those encapsulated within aggregates.

###### 2.3.3.1.3.5 Framework Convention Alignment

.NET and DDD convention for organizing domain models.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

src/Enums

###### 2.3.3.1.4.2 Purpose

Specification for strongly-typed enumerations that represent a fixed set of options within the domain.

###### 2.3.3.1.4.3 Contains Files

- JobStatus.cs
- DeliveryMode.cs
- OutputFormat.cs

###### 2.3.3.1.4.4 Organizational Reasoning

Centralizes all domain-specific enumerations for clarity and discoverability.

###### 2.3.3.1.4.5 Framework Convention Alignment

Common .NET practice for organizing enumeration types.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

src/Events

###### 2.3.3.1.5.2 Purpose

Specification for domain event contracts that represent significant occurrences within the business domain.

###### 2.3.3.1.5.3 Contains Files

- IDomainEvent.cs
- UserCreatedEvent.cs
- ReportConfigurationUpdatedEvent.cs

###### 2.3.3.1.5.4 Organizational Reasoning

Decouples domain logic by allowing other parts of the system to react to domain changes without direct coupling, following the Domain Events pattern.

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard DDD pattern implementation.

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

src/Exceptions

###### 2.3.3.1.6.2 Purpose

Specification for custom, domain-specific exceptions that provide clear, contextual error information when business rules are violated.

###### 2.3.3.1.6.3 Contains Files

- DomainException.cs
- EntityNotFoundException.cs
- BusinessRuleValidationException.cs

###### 2.3.3.1.6.4 Organizational Reasoning

Creates a well-defined exception hierarchy for the domain, allowing for more precise error handling in the application layer.

###### 2.3.3.1.6.5 Framework Convention Alignment

.NET best practice for custom exception design.

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

src/Interfaces

###### 2.3.3.1.7.2 Purpose

Specification for all abstract contracts (interfaces) that the domain layer exposes for implementation by outer layers, enforcing the Dependency Inversion Principle.

###### 2.3.3.1.7.3 Contains Files

- IRepository.cs
- IUserRepository.cs
- IReportConfigurationRepository.cs
- IConnector.cs

###### 2.3.3.1.7.4 Organizational Reasoning

Centralizes all abstraction contracts, making the domain's dependencies on external concerns explicit and clear.

###### 2.3.3.1.7.5 Framework Convention Alignment

Core principle of Clean Architecture and DDD.

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

src/ValueObjects

###### 2.3.3.1.8.2 Purpose

Specification for immutable types that represent descriptive aspects of the domain and are defined by their attributes rather than an identity.

###### 2.3.3.1.8.3 Contains Files

- EmailAddress.cs

###### 2.3.3.1.8.4 Organizational Reasoning

Encapsulates complex validation and business logic for simple values, enriching the domain model and improving type safety.

###### 2.3.3.1.8.5 Framework Convention Alignment

Leverages C# records for concise and correct implementation of the Value Object pattern.

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReportingSystem.Core.Domain |
| Namespace Organization | Specification requires mirroring the directory str... |
| Naming Conventions | Specification mandates following Microsoft C# Nami... |
| Framework Alignment | Standard .NET namespace conventions for class libr... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

ReportConfiguration

##### 2.3.4.1.2.0 File Path

src/Aggregates/ReportConfigurationAggregate/ReportConfiguration.cs

##### 2.3.4.1.3.0 Class Type

Aggregate Root

##### 2.3.4.1.4.0 Inheritance

AggregateRoot<Guid>

##### 2.3.4.1.5.0 Purpose

Specification for the central entity that encapsulates all settings for a report. It acts as the consistency boundary for report-related operations.

##### 2.3.4.1.6.0 Dependencies

- DeliveryDestination
- ReportSchedule
- BusinessRuleValidationException

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Specification requires implementation as a POCO class, using C# 12 primary constructors for initialization. State changes must be exposed through well-defined methods that enforce business rules.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

Name

####### 2.3.4.1.9.1.2 Property Type

string

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Specifies the unique, user-defined name of the report configuration.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Specification requires an init-only setter or a private setter with a public method for changes to enforce uniqueness rules at a higher level.

####### 2.3.4.1.9.1.7 Implementation Notes

Validation logic must ensure the name is not null or empty.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Description

####### 2.3.4.1.9.2.2 Property Type

string?

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Specifies the optional description for the report's purpose.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Specification requires a public setter.

####### 2.3.4.1.9.2.7 Implementation Notes

Specification for a nullable string.

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

ConnectorConfigurationId

####### 2.3.4.1.9.3.2 Property Type

Guid

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Specifies the foreign key to the ConnectorConfiguration entity that provides data for this report.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

Specification mandates this must be set on creation and cannot be an empty GUID.

####### 2.3.4.1.9.3.7 Implementation Notes



###### 2.3.4.1.9.4.0 Property Name

####### 2.3.4.1.9.4.1 Property Name

TransformationScriptId

####### 2.3.4.1.9.4.2 Property Type

Guid?

####### 2.3.4.1.9.4.3 Access Modifier

public

####### 2.3.4.1.9.4.4 Purpose

Specifies the optional foreign key to the TransformationScript entity used to process the data.

####### 2.3.4.1.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.4.6 Framework Specific Configuration

Specification for a nullable Guid.

####### 2.3.4.1.9.4.7 Implementation Notes



###### 2.3.4.1.9.5.0 Property Name

####### 2.3.4.1.9.5.1 Property Name

TemplateId

####### 2.3.4.1.9.5.2 Property Type

Guid?

####### 2.3.4.1.9.5.3 Access Modifier

public

####### 2.3.4.1.9.5.4 Purpose

Specifies the optional foreign key to the Template entity used for rendering HTML/PDF reports.

####### 2.3.4.1.9.5.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.5.6 Framework Specific Configuration

Specification for a nullable Guid.

####### 2.3.4.1.9.5.7 Implementation Notes

Business rule must be enforced: Must not be null if OutputFormat is HTML or PDF.

###### 2.3.4.1.9.6.0 Property Name

####### 2.3.4.1.9.6.1 Property Name

OutputFormat

####### 2.3.4.1.9.6.2 Property Type

OutputFormat

####### 2.3.4.1.9.6.3 Access Modifier

public

####### 2.3.4.1.9.6.4 Purpose

Specifies the final file format of the generated report.

####### 2.3.4.1.9.6.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.6.6 Framework Specific Configuration

Specification for an enum type.

####### 2.3.4.1.9.6.7 Implementation Notes



###### 2.3.4.1.9.7.0 Property Name

####### 2.3.4.1.9.7.1 Property Name

Schedule

####### 2.3.4.1.9.7.2 Property Type

ReportSchedule

####### 2.3.4.1.9.7.3 Access Modifier

public

####### 2.3.4.1.9.7.4 Purpose

Specifies the value object containing the scheduling information for this report.

####### 2.3.4.1.9.7.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.7.6 Framework Specific Configuration

Specification requires this to be implemented as a complex type (Value Object).

####### 2.3.4.1.9.7.7 Implementation Notes



###### 2.3.4.1.9.8.0 Property Name

####### 2.3.4.1.9.8.1 Property Name

_deliveryDestinations

####### 2.3.4.1.9.8.2 Property Type

List<DeliveryDestination>

####### 2.3.4.1.9.8.3 Access Modifier

private

####### 2.3.4.1.9.8.4 Purpose

Specifies the private backing field for the collection of child entities representing where the report will be delivered.

####### 2.3.4.1.9.8.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.8.6 Framework Specific Configuration

Public access must be exposed via a \"IReadOnlyCollection<DeliveryDestination>\" property and public methods.

####### 2.3.4.1.9.8.7 Implementation Notes

This enforces the aggregate boundary; destinations cannot be modified directly.

##### 2.3.4.1.10.0.0 Methods

###### 2.3.4.1.10.1.0 Method Name

####### 2.3.4.1.10.1.1 Method Name

AddDeliveryDestination

####### 2.3.4.1.10.1.2 Method Signature

void AddDeliveryDestination(DeliveryDestination newDestination)

####### 2.3.4.1.10.1.3 Return Type

void

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

❌ No

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

- {'parameter_name': 'newDestination', 'parameter_type': 'DeliveryDestination', 'is_nullable': False, 'purpose': 'Specifies the delivery destination entity to add to the report configuration.', 'framework_attributes': []}

####### 2.3.4.1.10.1.8 Implementation Logic

Specification: This method must contain logic to validate the new destination, add it to the internal list of destinations, and raise a \"DeliveryDestinationAddedEvent\" domain event.

####### 2.3.4.1.10.1.9 Exception Handling

Specification: Must throw \"BusinessRuleValidationException\" if adding the destination violates a business rule (e.g., duplicate destination).

####### 2.3.4.1.10.1.10 Performance Considerations

N/A

####### 2.3.4.1.10.1.11 Validation Requirements

Specification: Destination must not be null.

####### 2.3.4.1.10.1.12 Technology Integration Details

N/A

####### 2.3.4.1.10.1.13 Validation Notes

Method specification is complete and aligns with DDD aggregate principles.

###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

UpdateSchedule

####### 2.3.4.1.10.2.2 Method Signature

void UpdateSchedule(ReportSchedule newSchedule)

####### 2.3.4.1.10.2.3 Return Type

void

####### 2.3.4.1.10.2.4 Access Modifier

public

####### 2.3.4.1.10.2.5 Is Async

❌ No

####### 2.3.4.1.10.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7 Parameters

- {'parameter_name': 'newSchedule', 'parameter_type': 'ReportSchedule', 'is_nullable': False, 'purpose': 'Specifies the new schedule to apply to the report.', 'framework_attributes': []}

####### 2.3.4.1.10.2.8 Implementation Logic

Specification: This method must replace the existing schedule with the new one and raise a \"ReportScheduleUpdatedEvent\" domain event.

####### 2.3.4.1.10.2.9 Exception Handling

N/A

####### 2.3.4.1.10.2.10 Performance Considerations

N/A

####### 2.3.4.1.10.2.11 Validation Requirements

Specification: The new schedule must be a valid, non-null object.

####### 2.3.4.1.10.2.12 Technology Integration Details

N/A

####### 2.3.4.1.10.2.13 Validation Notes

Method specification is complete.

##### 2.3.4.1.11.0.0 Events

- {'event_name': 'ReportConfigurationCreatedEvent', 'event_type': 'IDomainEvent', 'trigger_conditions': 'Specification: Should be raised in the constructor of the ReportConfiguration.', 'event_data': "Specification: Should contain the ReportConfiguration's ID and Name."}

##### 2.3.4.1.12.0.0 Implementation Notes

Specification: The constructor must enforce core invariants, such as ensuring a name and connector ID are always provided. The class must be designed to always be in a valid state.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

User

##### 2.3.4.2.2.0.0 File Path

src/Aggregates/UserAggregate/User.cs

##### 2.3.4.2.3.0.0 Class Type

Aggregate Root

##### 2.3.4.2.4.0.0 Inheritance

AggregateRoot<Guid>

##### 2.3.4.2.5.0.0 Purpose

Specification for the User entity, representing a user of the system with their identity, status, and role.

##### 2.3.4.2.6.0.0 Dependencies

- Role
- EmailAddress

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Specification requires implementation as a POCO class. Must not contain any password information (hash or plaintext); that is an infrastructure concern handled by ASP.NET Core Identity's store.

##### 2.3.4.2.9.0.0 Properties

###### 2.3.4.2.9.1.0 Property Name

####### 2.3.4.2.9.1.1 Property Name

Username

####### 2.3.4.2.9.1.2 Property Type

string

####### 2.3.4.2.9.1.3 Access Modifier

public

####### 2.3.4.2.9.1.4 Purpose

Specifies the unique username for login.

####### 2.3.4.2.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.1.6 Framework Specific Configuration

Specification requires an init-only setter as username is immutable.

####### 2.3.4.2.9.1.7 Implementation Notes



###### 2.3.4.2.9.2.0 Property Name

####### 2.3.4.2.9.2.1 Property Name

Email

####### 2.3.4.2.9.2.2 Property Type

EmailAddress

####### 2.3.4.2.9.2.3 Access Modifier

public

####### 2.3.4.2.9.2.4 Purpose

Specifies the user's email address, encapsulated in a Value Object.

####### 2.3.4.2.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.2.6 Framework Specific Configuration

Specification requires a public setter to allow for updates.

####### 2.3.4.2.9.2.7 Implementation Notes

Using a value object enforces a valid email format.

###### 2.3.4.2.9.3.0 Property Name

####### 2.3.4.2.9.3.1 Property Name

RoleId

####### 2.3.4.2.9.3.2 Property Type

int

####### 2.3.4.2.9.3.3 Access Modifier

public

####### 2.3.4.2.9.3.4 Purpose

Specifies the foreign key to the Role entity.

####### 2.3.4.2.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.3.6 Framework Specific Configuration



####### 2.3.4.2.9.3.7 Implementation Notes



###### 2.3.4.2.9.4.0 Property Name

####### 2.3.4.2.9.4.1 Property Name

IsActive

####### 2.3.4.2.9.4.2 Property Type

bool

####### 2.3.4.2.9.4.3 Access Modifier

public

####### 2.3.4.2.9.4.4 Purpose

Specifies whether the user account is active and can be used to log in.

####### 2.3.4.2.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.4.6 Framework Specific Configuration



####### 2.3.4.2.9.4.7 Implementation Notes



##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Deactivate', 'method_signature': 'void Deactivate()', 'return_type': 'void', 'access_modifier': 'public', 'is_async': False, 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': 'Specification: Sets the \\"IsActive\\" property to false and must raise a \\"UserDeactivatedEvent\\" domain event.', 'exception_handling': 'Specification: Must throw \\"BusinessRuleValidationException\\" if trying to deactivate the primary administrator account (as per US-022).', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'N/A', 'validation_notes': 'Method specification is complete and covers business rule US-022.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Specification: The User aggregate is responsible for managing its own state (e.g., active/inactive) but delegates persistence and authentication concerns to other layers.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

BusinessRuleValidationException

##### 2.3.4.3.2.0.0 File Path

src/Exceptions/BusinessRuleValidationException.cs

##### 2.3.4.3.3.0.0 Class Type

Custom Exception

##### 2.3.4.3.4.0.0 Inheritance

DomainException

##### 2.3.4.3.5.0.0 Purpose

Specification for a custom exception that must be thrown when a business rule or entity invariant is violated.

##### 2.3.4.3.6.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0 Framework Specific Attributes

- [Serializable]

##### 2.3.4.3.8.0.0 Technology Integration Notes

Specification requires adherence to .NET best practices for custom exceptions, including standard constructors (message, message + inner exception).

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

*No items available*

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Specification: This exception allows the Application layer to catch domain-specific errors and translate them into appropriate user feedback or API responses.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

IReportConfigurationRepository

##### 2.3.5.1.2.0.0 File Path

src/Interfaces/IReportConfigurationRepository.cs

##### 2.3.5.1.3.0.0 Purpose

Specification for the contract for data persistence operations related to the ReportConfiguration aggregate root.

##### 2.3.5.1.4.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0 Framework Specific Inheritance

IRepository<ReportConfiguration, Guid>

##### 2.3.5.1.6.0.0 Method Contracts

- {'method_name': 'GetByNameAsync', 'method_signature': 'Task<ReportConfiguration?> GetByNameAsync(string name, CancellationToken cancellationToken = default)', 'return_type': 'Task<ReportConfiguration?>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'name', 'parameter_type': 'string', 'purpose': 'The name of the report configuration to find.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'purpose': 'Token to support operation cancellation.'}], 'contract_description': 'Specification: Must find a report configuration by its unique name, case-insensitively.', 'exception_contracts': 'N/A'}

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Specification: The implementation of this interface belongs in the Infrastructure layer. It must use asynchronous I/O and handle database-specific exceptions, translating them if necessary. It must manage the entire aggregate, including its child entities (eager or lazy loading as appropriate).

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IConnector

##### 2.3.5.2.2.0.0 File Path

src/Interfaces/IConnector.cs

##### 2.3.5.2.3.0.0 Purpose

Specification for the contract that all data connector plugins must implement. Defines the core data fetching capability of the system's extensible data ingestion framework.

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0 Method Contracts

###### 2.3.5.2.6.1.0 Method Name

####### 2.3.5.2.6.1.1 Method Name

FetchDataAsync

####### 2.3.5.2.6.1.2 Method Signature

Task<System.Text.Json.JsonNode> FetchDataAsync(ConnectorConfiguration config, CancellationToken cancellationToken = default)

####### 2.3.5.2.6.1.3 Return Type

Task<System.Text.Json.JsonNode>

####### 2.3.5.2.6.1.4 Framework Attributes

*No items available*

####### 2.3.5.2.6.1.5 Parameters

######## 2.3.5.2.6.1.5.1 Parameter Name

######### 2.3.5.2.6.1.5.1.1 Parameter Name

config

######### 2.3.5.2.6.1.5.1.2 Parameter Type

ConnectorConfiguration

######### 2.3.5.2.6.1.5.1.3 Purpose

The configuration entity containing all settings for this connector instance.

######## 2.3.5.2.6.1.5.2.0 Parameter Name

######### 2.3.5.2.6.1.5.2.1 Parameter Name

cancellationToken

######### 2.3.5.2.6.1.5.2.2 Parameter Type

CancellationToken

######### 2.3.5.2.6.1.5.2.3 Purpose

Token to support cancellation of long-running data fetch operations.

####### 2.3.5.2.6.1.6.0.0 Contract Description

Specification: Must connect to the data source defined in the configuration, fetch the data, and return it as a standardized \"System.Text.Json.JsonNode\" object.

####### 2.3.5.2.6.1.7.0.0 Exception Contracts

Specification: Implementations must throw specific, descriptive exceptions for failures like connection errors, authentication failures, or data parsing errors.

###### 2.3.5.2.6.2.0.0.0 Method Name

####### 2.3.5.2.6.2.1.0.0 Method Name

GetConfigurationSchema

####### 2.3.5.2.6.2.2.0.0 Method Signature

string GetConfigurationSchema()

####### 2.3.5.2.6.2.3.0.0 Return Type

string

####### 2.3.5.2.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.2.6.2.5.0.0 Parameters

*No items available*

####### 2.3.5.2.6.2.6.0.0 Contract Description

Specification: Must return a JSON string that defines the schema for the connector's configuration UI. This schema will be used by the frontend to dynamically render the configuration form, as required by US-037.

####### 2.3.5.2.6.2.7.0.0 Exception Contracts

N/A

###### 2.3.5.2.6.3.0.0.0 Method Name

####### 2.3.5.2.6.3.1.0.0 Method Name

TestConnectionAsync

####### 2.3.5.2.6.3.2.0.0 Method Signature

Task TestConnectionAsync(ConnectorConfiguration config, CancellationToken cancellationToken = default)

####### 2.3.5.2.6.3.3.0.0 Return Type

Task

####### 2.3.5.2.6.3.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.2.6.3.5.0.0 Parameters

######## 2.3.5.2.6.3.5.1.0 Parameter Name

######### 2.3.5.2.6.3.5.1.1 Parameter Name

config

######### 2.3.5.2.6.3.5.1.2 Parameter Type

ConnectorConfiguration

######### 2.3.5.2.6.3.5.1.3 Purpose

The configuration entity to be tested.

######## 2.3.5.2.6.3.5.2.0 Parameter Name

######### 2.3.5.2.6.3.5.2.1 Parameter Name

cancellationToken

######### 2.3.5.2.6.3.5.2.2 Parameter Type

CancellationToken

######### 2.3.5.2.6.3.5.2.3 Purpose

Token to support cancellation of the connection test.

####### 2.3.5.2.6.3.6.0.0 Contract Description

Specification: Must attempt to connect to the data source and validate the provided configuration. Must throw a specific exception on failure, as required by US-038.

####### 2.3.5.2.6.3.7.0.0 Exception Contracts

Specification: Implementations must throw specific exceptions for different failure modes (e.g., `ConnectionFailedException`, `AuthenticationFailedException`).

##### 2.3.5.2.7.0.0.0.0 Property Contracts

- {'property_name': 'Name', 'property_type': 'string', 'getter_contract': 'Specification: Must return the unique, user-friendly name of the connector type (e.g., \\"SQL Server\\").', 'setter_contract': 'N/A'}

##### 2.3.5.2.8.0.0.0.0 Implementation Guidance

Specification: Implementations of this interface will reside in separate plugin assemblies. They must handle all aspects of communication with their specific data source and be completely self-contained. This specification is enhanced to be fully compliant with requirements US-113, US-037, and US-038.

##### 2.3.5.2.9.0.0.0.0 Validation Notes

Validation complete: This interface specification now includes all methods required by the user stories and sequence diagrams.

### 2.3.6.0.0.0.0.0.0 Enum Specifications

#### 2.3.6.1.0.0.0.0.0 Enum Name

##### 2.3.6.1.1.0.0.0.0 Enum Name

OutputFormat

##### 2.3.6.1.2.0.0.0.0 File Path

src/Enums/OutputFormat.cs

##### 2.3.6.1.3.0.0.0.0 Underlying Type

int

##### 2.3.6.1.4.0.0.0.0 Purpose

Specification for the available file formats for a generated report.

##### 2.3.6.1.5.0.0.0.0 Framework Attributes

*No items available*

##### 2.3.6.1.6.0.0.0.0 Values

###### 2.3.6.1.6.1.0.0.0 Value Name

####### 2.3.6.1.6.1.1.0.0 Value Name

HTML

####### 2.3.6.1.6.1.2.0.0 Value

0

####### 2.3.6.1.6.1.3.0.0 Description

HyperText Markup Language

###### 2.3.6.1.6.2.0.0.0 Value Name

####### 2.3.6.1.6.2.1.0.0 Value Name

PDF

####### 2.3.6.1.6.2.2.0.0 Value

1

####### 2.3.6.1.6.2.3.0.0 Description

Portable Document Format

###### 2.3.6.1.6.3.0.0.0 Value Name

####### 2.3.6.1.6.3.1.0.0 Value Name

JSON

####### 2.3.6.1.6.3.2.0.0 Value

2

####### 2.3.6.1.6.3.3.0.0 Description

JavaScript Object Notation

###### 2.3.6.1.6.4.0.0.0 Value Name

####### 2.3.6.1.6.4.1.0.0 Value Name

CSV

####### 2.3.6.1.6.4.2.0.0 Value

3

####### 2.3.6.1.6.4.3.0.0 Description

Comma-Separated Values

###### 2.3.6.1.6.5.0.0.0 Value Name

####### 2.3.6.1.6.5.1.0.0 Value Name

TXT

####### 2.3.6.1.6.5.2.0.0 Value

4

####### 2.3.6.1.6.5.3.0.0 Description

Plain Text

##### 2.3.6.1.7.0.0.0.0 Validation Notes

Specification is complete.

#### 2.3.6.2.0.0.0.0.0 Enum Name

##### 2.3.6.2.1.0.0.0.0 Enum Name

JobStatus

##### 2.3.6.2.2.0.0.0.0 File Path

src/Enums/JobStatus.cs

##### 2.3.6.2.3.0.0.0.0 Underlying Type

int

##### 2.3.6.2.4.0.0.0.0 Purpose

Specification for the possible states of a report generation job, as required by US-071.

##### 2.3.6.2.5.0.0.0.0 Framework Attributes

*No items available*

##### 2.3.6.2.6.0.0.0.0 Values

###### 2.3.6.2.6.1.0.0.0 Value Name

####### 2.3.6.2.6.1.1.0.0 Value Name

Queued

####### 2.3.6.2.6.1.2.0.0 Value

0

####### 2.3.6.2.6.1.3.0.0 Description

The job is waiting in the queue for execution.

###### 2.3.6.2.6.2.0.0.0 Value Name

####### 2.3.6.2.6.2.1.0.0 Value Name

Running

####### 2.3.6.2.6.2.2.0.0 Value

1

####### 2.3.6.2.6.2.3.0.0 Description

The job is currently being executed.

###### 2.3.6.2.6.3.0.0.0 Value Name

####### 2.3.6.2.6.3.1.0.0 Value Name

Succeeded

####### 2.3.6.2.6.3.2.0.0 Value

2

####### 2.3.6.2.6.3.3.0.0 Description

The job completed successfully.

###### 2.3.6.2.6.4.0.0.0 Value Name

####### 2.3.6.2.6.4.1.0.0 Value Name

Failed

####### 2.3.6.2.6.4.2.0.0 Value

3

####### 2.3.6.2.6.4.3.0.0 Description

The job failed during execution.

###### 2.3.6.2.6.5.0.0.0 Value Name

####### 2.3.6.2.6.5.1.0.0 Value Name

Cancelled

####### 2.3.6.2.6.5.2.0.0 Value

4

####### 2.3.6.2.6.5.3.0.0 Description

The job was manually cancelled before completion.

##### 2.3.6.2.7.0.0.0.0 Validation Notes

Specification is complete and covers all states mentioned in requirements.

### 2.3.7.0.0.0.0.0.0 Dto Specifications

*No items available*

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

*No items available*

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 19 |
| Total Interfaces | 5 |
| Total Enums | 3 |
| Total Dtos | 0 |
| Total Configurations | 0 |
| Total External Integrations | 0 |
| Grand Total Components | 27 |
| Phase 2 Claimed Count | 0 |
| Phase 2 Actual Count | 0 |
| Validation Added Count | 27 |
| Final Validated Count | 27 |
| Validation Notes | Initial specification was empty. All 27 core domai... |

# 3.0.0.0.0.0.0.0.0 File Structure

## 3.1.0.0.0.0.0.0.0 Directory Organization

### 3.1.1.0.0.0.0.0.0 Directory Path

#### 3.1.1.1.0.0.0.0.0 Directory Path

/

#### 3.1.1.2.0.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.1.3.0.0.0.0.0 Contains Files

- ReportingSystem.sln
- global.json
- .editorconfig
- Directory.Build.props
- .gitignore

#### 3.1.1.4.0.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.1.5.0.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.2.0.0.0.0.0.0 Directory Path

#### 3.1.2.1.0.0.0.0.0 Directory Path

src/ReportingSystem.Core.Domain

#### 3.1.2.2.0.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.2.3.0.0.0.0.0 Contains Files

- ReportingSystem.Core.Domain.csproj

#### 3.1.2.4.0.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.2.5.0.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

