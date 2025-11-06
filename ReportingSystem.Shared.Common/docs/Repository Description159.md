# 1 Id

REPO-04-SHARED-COMMON

# 2 Name

ReportingSystem.Shared.Common

# 3 Description

This repository is a shared utility library containing reusable helper methods, extension methods, and common patterns that are used across multiple backend projects but are not part of the core business domain. Examples include custom extension methods for string manipulation, date handling, or perhaps a base class for specific service types. It was extracted from the original monorepo to avoid code duplication and provide a centralized location for generic, low-level utilities. It has minimal to no dependencies on other project libraries, making it a highly reusable and stable component.

# 4 Type

🔹 Utility Library

# 5 Namespace

ReportingSystem.Shared.Common

# 6 Output Path

src/libs/ReportingSystem.Shared.Common

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- shared-layer

# 12 Dependencies

*No items available*

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

N/A

# 17 Architecture Map

- shared-concerns

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Created to adhere to the Don't Repeat Yourself (DRY) principle. By centralizing common, non-domain-specific helper code, it reduces code duplication, simplifies maintenance, and ensures consistency across the backend codebase.

## 20.4 Extracted Responsibilities

- String and DateTime extension methods.
- Common constants or configuration models.
- Generic helper classes for tasks like reflection or serialization.

## 20.5 Reusability Scope

- Referenced by nearly all other backend repositories (Core, Infrastructure, Service) for common utilities.

## 20.6 Development Benefits

- Reduces boilerplate code in feature development.
- Provides a single place to maintain and optimize common helper functions.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

- {'interface': 'StringExtensions (Static Class)', 'methods': ['string ToSnakeCase(this string input)'], 'events': [], 'properties': [], 'consumers': ['REPO-02-CORE-DOMAIN', 'REPO-05-INFRASTRUCTURE', 'REPO-08-SERVICE-HOST']}

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A - Primarily static helper classes. |
| Event Communication | N/A |
| Data Flow | N/A |
| Error Handling | N/A |
| Async Patterns | N/A |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Code should be generic and not tied to any specifi... |
| Performance Considerations | Utilities should be written with performance in mi... |
| Security Considerations | Input to helper methods should be properly validat... |
| Testing Approach | Thorough unit testing is critical for this shared ... |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- Highly reusable, generic utility code.

## 25.2 Must Not Implement

- Any business domain logic.
- Any I/O operations or infrastructure-specific code.

## 25.3 Extension Points

- New helper classes and extension methods can be added.

## 25.4 Validation Rules

*No items available*

