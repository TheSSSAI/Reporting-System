# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Architecture Type

Modular Monolith with Clean Architecture

## 1.3 Technology Stack

- .NET 8
- ASP.NET Core 8
- Entity Framework Core 8
- PostgreSQL

## 1.4 Bounded Contexts

- Reporting System

# 2.0 Project Specific Events

## 2.1 Event Id

### 2.1.1 Event Id

EVT-AUD-001

### 2.1.2 Event Name

ScriptOperationAudited

### 2.1.3 Event Type

domain

### 2.1.4 Category

🔹 Security Audit

### 2.1.5 Description

Fired after any CRUD operation on a TransformationScript or its association with a ReportConfiguration. Fulfills REQ-SEC-DTR-004.

### 2.1.6 Trigger Condition

Successful creation, update, or deletion of a TransformationScript, or change in a ReportConfiguration's script association.

### 2.1.7 Source Context

Reporting System

### 2.1.8 Target Contexts

- Reporting System

### 2.1.9 Payload

#### 2.1.9.1 Schema

| Property | Value |
|----------|-------|
| Event Type | string (e.g., 'ScriptCreated', 'ScriptUpdated', 'S... |
| Entity Type | string (e.g., 'TransformationScript', 'ReportConfi... |
| Entity Id | string |
| User Id | string |
| Timestamp | string (ISO 8601) |
| Details | object (Changes made or context of the operation) |

#### 2.1.9.2 Required Fields

- eventType
- entityType
- entityId
- userId
- timestamp

#### 2.1.9.3 Optional Fields

- details

### 2.1.10.0 Frequency

medium

### 2.1.11.0 Business Criticality

critical

### 2.1.12.0 Data Source

| Property | Value |
|----------|-------|
| Database | PostgreSQL |
| Table | TransformationScript, ReportConfiguration |
| Operation | create\|update\|delete |

### 2.1.13.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | ScriptOperationAudited.v1 |
| Exchange | InProcessMediator |
| Queue | N/A (In-process handling) |

### 2.1.14.0 Consumers

- {'service': 'SecurityAuditService', 'handler': 'AuditLogEventHandler', 'processingType': 'async'}

### 2.1.15.0 Dependencies

*No items available*

### 2.1.16.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | 3 retries with exponential backoff |
| Dead Letter Queue | Log to system's error log (Serilog) after retries ... |
| Timeout Ms | 5000 |

## 2.2.0.0 Event Id

### 2.2.1.0 Event Id

EVT-AUD-002

### 2.2.2.0 Event Name

SandboxConstraintViolated

### 2.2.3.0 Event Type

domain

### 2.2.4.0 Category

🔹 Security Audit

### 2.2.5.0 Description

Fired when a script execution violates a configured sandbox constraint (timeout, memory, statement count). Fulfills REQ-SEC-DTR-002.

### 2.2.6.0 Trigger Condition

A Jint engine constraint is breached during script execution.

### 2.2.7.0 Source Context

Reporting System

### 2.2.8.0 Target Contexts

- Reporting System

### 2.2.9.0 Payload

#### 2.2.9.1 Schema

| Property | Value |
|----------|-------|
| Violation Type | string (e.g., 'Timeout', 'MemoryLimit', 'Statement... |
| Script Id | string |
| Report Job Id | string (if applicable) |
| Limit Value | string |
| Timestamp | string (ISO 8601) |

#### 2.2.9.2 Required Fields

- violationType
- scriptId
- limitValue
- timestamp

#### 2.2.9.3 Optional Fields

- reportJobId

### 2.2.10.0 Frequency

low

### 2.2.11.0 Business Criticality

important

### 2.2.12.0 Data Source

| Property | Value |
|----------|-------|
| Database | N/A |
| Table | N/A |
| Operation | read |

### 2.2.13.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | SandboxConstraintViolated.v1 |
| Exchange | InProcessMediator |
| Queue | N/A (In-process handling) |

### 2.2.14.0 Consumers

- {'service': 'SecurityAuditService', 'handler': 'AuditLogEventHandler', 'processingType': 'async'}

### 2.2.15.0 Dependencies

*No items available*

### 2.2.16.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | 3 retries with exponential backoff |
| Dead Letter Queue | Log to system's error log (Serilog) after retries ... |
| Timeout Ms | 5000 |

# 3.0.0.0 Event Types And Schema Design

## 3.1.0.0 Essential Event Types

### 3.1.1.0 Event Name

#### 3.1.1.1 Event Name

ScriptOperationAudited

#### 3.1.1.2 Category

🔹 domain

#### 3.1.1.3 Description

Captures all management actions on scripts for the immutable audit trail.

#### 3.1.1.4 Priority

🔴 high

### 3.1.2.0 Event Name

#### 3.1.2.1 Event Name

SandboxConstraintViolated

#### 3.1.2.2 Category

🔹 domain

#### 3.1.2.3 Description

Captures all runtime security violations for the immutable audit trail.

#### 3.1.2.4 Priority

🔴 high

## 3.2.0.0 Schema Design

| Property | Value |
|----------|-------|
| Format | JSON |
| Reasoning | Native to the .NET and web stack, easily serializa... |
| Consistency Approach | Use a base event class with common fields like Eve... |

## 3.3.0.0 Schema Evolution

| Property | Value |
|----------|-------|
| Backward Compatibility | ✅ |
| Forward Compatibility | ❌ |
| Strategy | Additive changes only. New event properties must b... |

## 3.4.0.0 Event Structure

### 3.4.1.0 Standard Fields

- eventId
- eventTimestamp
- correlationId

### 3.4.2.0 Metadata Requirements

- The correlation ID from the originating API request must be passed through to the event to enable end-to-end tracing within logs.

# 4.0.0.0 Event Routing And Processing

## 4.1.0.0 Routing Mechanisms

- {'type': 'In-process Mediator (e.g., MediatR library)', 'description': 'Events are published and handled within the same process. This decouples components (e.g., ScriptManagementService from SecurityAuditService) without the overhead of an external message broker.', 'useCase': 'Handling all internal domain events for auditing, fitting the Modular Monolith architecture perfectly.'}

## 4.2.0.0 Processing Patterns

- {'pattern': 'sequential', 'applicableScenarios': ['All audit events.'], 'implementation': 'An asynchronous, non-blocking handler (`async Task Handle(TEvent e)`) is invoked by the mediator. The original request thread is not blocked, but each event is processed independently.'}

## 4.3.0.0 Filtering And Subscription

### 4.3.1.0 Filtering Mechanism

Type-based subscription

### 4.3.2.0 Subscription Model

Handlers implement a generic interface (e.g., `IEventHandler<TEvent>`) and are automatically registered via Dependency Injection. The mediator dispatches events to handlers subscribed to that specific event type.

### 4.3.3.0 Routing Keys

*No items available*

## 4.4.0.0 Handler Isolation

| Property | Value |
|----------|-------|
| Required | ❌ |
| Approach | N/A |
| Reasoning | The system is a monolith. All handlers run within ... |

## 4.5.0.0 Delivery Guarantees

| Property | Value |
|----------|-------|
| Level | at-least-once |
| Justification | Audit events are critical and must not be lost due... |
| Implementation | The event handler will implement a retry policy (e... |

# 5.0.0.0 Event Storage And Replay

## 5.1.0.0 Persistence Requirements

| Property | Value |
|----------|-------|
| Required | ❌ |
| Duration | N/A |
| Reasoning | The events themselves are transient messages. The ... |

## 5.2.0.0 Event Sourcing

### 5.2.1.0 Necessary

❌ No

### 5.2.2.0 Justification

The architecture is a classic state-based model using a relational database, not event sourcing. Introducing it would be a significant and unnecessary architectural change.

### 5.2.3.0 Scope

*No items available*

## 5.3.0.0 Technology Options

*No items available*

## 5.4.0.0 Replay Capabilities

### 5.4.1.0 Required

❌ No

### 5.4.2.0 Scenarios

*No items available*

### 5.4.3.0 Implementation

N/A. System recovery relies on database backups, not event replay.

## 5.5.0.0 Retention Policy

| Property | Value |
|----------|-------|
| Strategy | N/A |
| Duration | N/A |
| Archiving Approach | N/A for events. The AuditLog table itself has a re... |

# 6.0.0.0 Dead Letter Queue And Error Handling

## 6.1.0.0 Dead Letter Strategy

| Property | Value |
|----------|-------|
| Approach | Retry and Log |
| Queue Configuration | Not a formal DLQ. After exhausting retries, the ha... |
| Processing Logic | Manual intervention is required based on critical ... |

## 6.2.0.0 Retry Policies

- {'errorType': 'Transient database or network errors', 'maxRetries': 3, 'backoffStrategy': 'exponential', 'delayConfiguration': 'Start with 100ms, then 200ms, then 400ms.'}

## 6.3.0.0 Poison Message Handling

| Property | Value |
|----------|-------|
| Detection Mechanism | A message is considered poison if it fails consist... |
| Handling Strategy | The handler will catch the final exception, log it... |
| Alerting Required | ✅ |

## 6.4.0.0 Error Notification

### 6.4.1.0 Channels

- Logging System (Serilog)
- Monitoring System (Prometheus Alerts)

### 6.4.2.0 Severity

critical

### 6.4.3.0 Recipients

- On-call DevOps/SRE team

## 6.5.0.0 Recovery Procedures

- {'scenario': 'Failed Audit Event', 'procedure': '1. DevOps receives alert for failed event handler. 2. Investigate the critical log entry containing the event payload. 3. Manually insert the corresponding record into the AuditLog table if necessary. 4. Resolve the root cause (e.g., database issue, bug in handler).', 'automationLevel': 'manual'}

# 7.0.0.0 Event Versioning Strategy

## 7.1.0.0 Schema Evolution Approach

| Property | Value |
|----------|-------|
| Strategy | Additive Changes Only. Strictly avoid breaking cha... |
| Versioning Scheme | Semantic versioning on the event name (e.g., Scrip... |
| Migration Strategy | Since publisher and consumers are deployed togethe... |

## 7.2.0.0 Compatibility Requirements

| Property | Value |
|----------|-------|
| Backward Compatible | ✅ |
| Forward Compatible | ❌ |
| Reasoning | New optional fields can be added to events without... |

## 7.3.0.0 Version Identification

| Property | Value |
|----------|-------|
| Mechanism | By C# class type. |
| Location | payload |
| Format | The fully qualified .NET type name implicitly vers... |

## 7.4.0.0 Consumer Upgrade Strategy

| Property | Value |
|----------|-------|
| Approach | N/A |
| Rollout Strategy | N/A |
| Rollback Procedure | The entire application is deployed or rolled back ... |

## 7.5.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | Schema is governed by the C# class definitions wit... |

# 8.0.0.0 Event Monitoring And Observability

## 8.1.0.0 Monitoring Capabilities

- {'capability': 'Event Handler Execution Metrics', 'justification': 'To monitor the health and performance of the asynchronous auditing process.', 'implementation': 'Use prometheus-net to expose counters for events processed/failed and a histogram for handler execution duration.'}

## 8.2.0.0 Tracing And Correlation

| Property | Value |
|----------|-------|
| Tracing Required | ✅ |
| Correlation Strategy | A unique Correlation ID is generated at the API/We... |
| Trace Id Propagation | The Correlation ID is passed from the initial serv... |

## 8.3.0.0 Performance Metrics

### 8.3.1.0 Metric

#### 8.3.1.1 Metric

event_handler_execution_duration_seconds

#### 8.3.1.2 Threshold

p99 < 500ms

#### 8.3.1.3 Alerting

✅ Yes

### 8.3.2.0 Metric

#### 8.3.2.1 Metric

event_handler_failures_total

#### 8.3.2.2 Threshold

Increases by > 0 in 5 minutes

#### 8.3.2.3 Alerting

✅ Yes

## 8.4.0.0 Event Flow Visualization

| Property | Value |
|----------|-------|
| Required | ❌ |
| Tooling | N/A |
| Scope | The event flow is extremely simple (Service -> Med... |

## 8.5.0.0 Alerting Requirements

- {'condition': 'Rate of event handler failures is > 0.', 'severity': 'critical', 'responseTime': '15 minutes', 'escalationPath': ['On-call DevOps Engineer', 'Lead Engineer']}

# 9.0.0.0 Implementation Priority

## 9.1.0.0 Component

### 9.1.1.0 Component

In-process Mediator Library Integration

### 9.1.2.0 Priority

🔴 high

### 9.1.3.0 Dependencies

*No items available*

### 9.1.4.0 Estimated Effort

Low

## 9.2.0.0 Component

### 9.2.1.0 Component

Audit Event Definitions & Publishers

### 9.2.2.0 Priority

🔴 high

### 9.2.3.0 Dependencies

- In-process Mediator Library Integration

### 9.2.4.0 Estimated Effort

Medium

## 9.3.0.0 Component

### 9.3.1.0 Component

Audit Event Handler with Retry/Error Logic

### 9.3.2.0 Priority

🔴 high

### 9.3.3.0 Dependencies

- Audit Event Definitions & Publishers

### 9.3.4.0 Estimated Effort

Medium

## 9.4.0.0 Component

### 9.4.1.0 Component

Monitoring & Alerting for Event Handlers

### 9.4.2.0 Priority

🟡 medium

### 9.4.3.0 Dependencies

- Audit Event Handler with Retry/Error Logic

### 9.4.4.0 Estimated Effort

Low

# 10.0.0.0 Risk Assessment

## 10.1.0.0 Risk

### 10.1.1.0 Risk

Over-engineering with an external message broker.

### 10.1.2.0 Impact

high

### 10.1.3.0 Probability

medium

### 10.1.4.0 Mitigation

Strictly adhere to the recommendation of using an in-process mediator pattern which is appropriate for a Modular Monolith architecture. Document this decision and its rationale clearly.

## 10.2.0.0 Risk

### 10.2.1.0 Risk

Loss of audit data due to unhandled exceptions in the event handler.

### 10.2.2.0 Impact

high

### 10.2.3.0 Probability

low

### 10.2.4.0 Mitigation

Implement robust retry logic for transient errors and a reliable 'Retry and Log' dead-letter strategy for poison messages, coupled with critical alerting.

## 10.3.0.0 Risk

### 10.3.1.0 Risk

Asynchronous processing complicates the database transaction scope.

### 10.3.2.0 Impact

medium

### 10.3.3.0 Probability

medium

### 10.3.4.0 Mitigation

Adopt the pattern of publishing events only *after* the primary database transaction has been successfully committed. This prevents handlers from processing events related to rolled-back operations.

# 11.0.0.0 Recommendations

## 11.1.0.0 Category

### 11.1.1.0 Category

🔹 Architecture

### 11.1.2.0 Recommendation

Utilize an in-process messaging library like MediatR for handling domain events. Do not introduce an external message broker (e.g., RabbitMQ, Kafka).

### 11.1.3.0 Justification

This approach provides the desired decoupling between components within the monolith without adding significant operational complexity, infrastructure cost, or latency, which aligns perfectly with the existing Modular Monolith architecture.

### 11.1.4.0 Priority

🔴 high

## 11.2.0.0 Category

### 11.2.1.0 Category

🔹 Reliability

### 11.2.2.0 Recommendation

Implement a standardized retry policy (e.g., using Polly) and a consistent 'log-and-drop' dead-letter strategy within a base event handler class.

### 11.2.3.0 Justification

This ensures all asynchronous handlers are resilient to transient failures and handle poison messages gracefully, preventing data loss and system instability as required by the audit-related requirements.

### 11.2.4.0 Priority

🔴 high

## 11.3.0.0 Category

### 11.3.1.0 Category

🔹 Observability

### 11.3.2.0 Recommendation

Ensure the Correlation ID from the originating HTTP request is propagated through to all published events and used in all subsequent logging from event handlers.

### 11.3.3.0 Justification

This is a low-effort, high-impact change that enables end-to-end tracing of operations through the system's logs, which is invaluable for debugging and auditing purposes.

### 11.3.4.0 Priority

🟡 medium

