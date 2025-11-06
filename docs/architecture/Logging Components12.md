# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- Serilog
- PostgreSQL
- Jint
- prometheus-net

## 1.3 Monitoring Requirements

- REQ-SEC-DTR-002
- REQ-SEC-DTR-004
- REQ-PERF-DTR-001
- REQ-FUNC-DTR-003
- REQ-REL-DTR-001
- REQ-COMP-DTR-001
- REQ-OPER-DTR-001

## 1.4 System Architecture

ModularMonolith with CleanArchitecture

## 1.5 Environment

production

# 2.0 Log Level And Category Strategy

## 2.1 Default Log Level

Information

## 2.2 Environment Specific Levels

### 2.2.1 Environment

#### 2.2.1.1 Environment

Production

#### 2.2.1.2 Log Level

Information

#### 2.2.1.3 Justification

Provides a balance of operational insight and performance, capturing key events without excessive verbosity.

### 2.2.2.0 Environment

#### 2.2.2.1 Environment

Staging

#### 2.2.2.2 Log Level

Debug

#### 2.2.2.3 Justification

Enables detailed tracing for pre-production testing and troubleshooting.

### 2.2.3.0 Environment

#### 2.2.3.1 Environment

Development

#### 2.2.3.2 Log Level

Debug

#### 2.2.3.3 Justification

Provides maximum detail for developers during active feature development.

## 2.3.0.0 Component Categories

### 2.3.1.0 Component

#### 2.3.1.1 Component

API/Web Layer

#### 2.3.1.2 Category

🔹 Api

#### 2.3.1.3 Log Level

Information

#### 2.3.1.4 Verbose Logging

❌ No

#### 2.3.1.5 Justification

Logs request/response lifecycle information, authentication, and authorization events.

### 2.3.2.0 Component

#### 2.3.2.1 Component

Application Layer

#### 2.3.2.2 Category

🔹 Application

#### 2.3.2.3 Log Level

Information

#### 2.3.2.4 Verbose Logging

❌ No

#### 2.3.2.5 Justification

Logs the start and end of business use cases and key decisions.

### 2.3.3.0 Component

#### 2.3.3.1 Component

JintTransformationEngine

#### 2.3.3.2 Category

🔹 Infrastructure.Jint

#### 2.3.3.3 Log Level

Information

#### 2.3.3.4 Verbose Logging

✅ Yes

#### 2.3.3.5 Justification

Critical component that requires the ability to enable verbose (Debug) logging in production to diagnose complex script execution issues. Default level remains Information.

### 2.3.4.0 Component

#### 2.3.4.1 Component

Infrastructure Layer (Data)

#### 2.3.4.2 Category

🔹 Infrastructure.Data

#### 2.3.4.3 Log Level

Warning

#### 2.3.4.4 Verbose Logging

❌ No

#### 2.3.4.5 Justification

Logs only significant data access issues, such as slow queries or connection problems, to avoid high-volume logging. EF Core's built-in logging can be enabled at Debug level for troubleshooting.

## 2.4.0.0 Sampling Strategies

*No items available*

## 2.5.0.0 Logging Approach

### 2.5.1.0 Structured

✅ Yes

### 2.5.2.0 Format

JSON

### 2.5.3.0 Standard Fields

- Timestamp
- Level
- MessageTemplate
- Message
- Exception
- CorrelationId
- SourceContext

### 2.5.4.0 Custom Fields

- UserId
- ScriptId
- ReportJobId
- EventType
- RequestPath
- RequestMethod

# 3.0.0.0 Log Aggregation Architecture

## 3.1.0.0 Collection Mechanism

### 3.1.1.0 Type

🔹 library

### 3.1.2.0 Technology

Serilog

### 3.1.3.0 Configuration

#### 3.1.3.1 Sinks

- Console (JSON Formatter)

#### 3.1.3.2 Enrichers

- FromLogContext
- WithMachineName
- WithThreadId

### 3.1.4.0 Justification

Serilog is specified in the architecture and is the standard for structured logging in .NET. Logging to the console is best practice for containerized environments, delegating collection to the container runtime.

## 3.2.0.0 Strategy

| Property | Value |
|----------|-------|
| Approach | centralized |
| Reasoning | Required for effective troubleshooting, security a... |
| Local Retention | None. Logs are streamed directly from stdout by th... |

## 3.3.0.0 Shipping Methods

- {'protocol': 'stdout', 'destination': 'Central Log Aggregator (e.g., Loki, Elasticsearch, Splunk)', 'reliability': 'at-least-once', 'compression': True}

## 3.4.0.0 Buffering And Batching

| Property | Value |
|----------|-------|
| Buffer Size | 1000 events |
| Batch Size | 100 |
| Flush Interval | 5s |
| Backpressure Handling | Handled by the container runtime's logging driver ... |

## 3.5.0.0 Transformation And Enrichment

### 3.5.1.0 Transformation

#### 3.5.1.1 Transformation

Add Correlation ID

#### 3.5.1.2 Purpose

To trace a single request across all application components and logs.

#### 3.5.1.3 Stage

collection

### 3.5.2.0 Transformation

#### 3.5.2.1 Transformation

Add User ID

#### 3.5.2.2 Purpose

To attribute actions to a specific user, as required by REQ-SEC-DTR-004.

#### 3.5.2.3 Stage

collection

## 3.6.0.0 High Availability

| Property | Value |
|----------|-------|
| Required | ✅ |
| Redundancy | Required for the central log aggregation platform,... |
| Failover Strategy | Managed by the log aggregation platform (e.g., mul... |

# 4.0.0.0 Retention Policy Design

## 4.1.0.0 Retention Periods

### 4.1.1.0 Log Type

#### 4.1.1.1 Log Type

AuditLog

#### 4.1.1.2 Retention Period

365 days

#### 4.1.1.3 Justification

Directly fulfills REQ-COMP-DTR-001 for SOC 2 and ISO 27001 compliance.

#### 4.1.1.4 Compliance Requirement

SOC 2 / ISO 27001

### 4.1.2.0 Log Type

#### 4.1.2.1 Log Type

ApplicationLog

#### 4.1.2.2 Retention Period

90 days

#### 4.1.2.3 Justification

Provides a reasonable window for troubleshooting operational issues without incurring excessive storage costs.

#### 4.1.2.4 Compliance Requirement

None

### 4.1.3.0 Log Type

#### 4.1.3.1 Log Type

DebugLog

#### 4.1.3.2 Retention Period

14 days

#### 4.1.3.3 Justification

Short-term retention for detailed logs generated in non-production environments.

#### 4.1.3.4 Compliance Requirement

None

## 4.2.0.0 Compliance Requirements

- {'regulation': 'SOC 2 / ISO 27001', 'applicableLogTypes': ['AuditLog'], 'minimumRetention': '365 days', 'specialHandling': 'Logs must be stored in a way that ensures integrity and immutability. Access must be audited.'}

## 4.3.0.0 Volume Impact Analysis

| Property | Value |
|----------|-------|
| Estimated Daily Volume | Dependent on usage, but audit logs are expected to... |
| Storage Cost Projection | Tiered storage is essential to manage costs for th... |
| Compression Ratio | Expected 5:1 to 10:1 for text-based JSON logs. |

## 4.4.0.0 Storage Tiering

### 4.4.1.0 Hot Storage

| Property | Value |
|----------|-------|
| Duration | 30 days |
| Accessibility | immediate |
| Cost | high |

### 4.4.2.0 Warm Storage

| Property | Value |
|----------|-------|
| Duration | 90 days |
| Accessibility | minutes |
| Cost | medium |

### 4.4.3.0 Cold Storage

| Property | Value |
|----------|-------|
| Duration | > 90 days (up to 365 for Audit) |
| Accessibility | hours |
| Cost | low |

## 4.5.0.0 Compression Strategy

| Property | Value |
|----------|-------|
| Algorithm | LZ4 or Zstandard |
| Compression Level | Standard |
| Expected Ratio | 8:1 |

## 4.6.0.0 Anonymization Requirements

*No items available*

# 5.0.0.0 Search Capability Requirements

## 5.1.0.0 Essential Capabilities

### 5.1.1.0 Capability

#### 5.1.1.1 Capability

Filtering by key indexed fields (CorrelationId, UserId, ScriptId, Level, Timestamp).

#### 5.1.1.2 Performance Requirement

< 2 seconds

#### 5.1.1.3 Justification

Essential for rapid troubleshooting and incident response.

### 5.1.2.0 Capability

#### 5.1.2.1 Capability

Full-text search on message and exception fields.

#### 5.1.2.2 Performance Requirement

< 5 seconds

#### 5.1.2.3 Justification

Required to find specific error messages or stack traces.

## 5.2.0.0 Performance Characteristics

| Property | Value |
|----------|-------|
| Search Latency | p95 < 5 seconds for common queries |
| Concurrent Users | 10 |
| Query Complexity | complex |
| Indexing Strategy | Index key fields for fast filtering; full-text ind... |

## 5.3.0.0 Indexed Fields

### 5.3.1.0 Field

#### 5.3.1.1 Field

Timestamp

#### 5.3.1.2 Index Type

B-Tree

#### 5.3.1.3 Search Pattern

Time range queries

#### 5.3.1.4 Frequency

high

### 5.3.2.0 Field

#### 5.3.2.1 Field

Level

#### 5.3.2.2 Index Type

Keyword

#### 5.3.2.3 Search Pattern

Exact match filter

#### 5.3.2.4 Frequency

high

### 5.3.3.0 Field

#### 5.3.3.1 Field

CorrelationId

#### 5.3.3.2 Index Type

Keyword

#### 5.3.3.3 Search Pattern

Exact match filter

#### 5.3.3.4 Frequency

high

### 5.3.4.0 Field

#### 5.3.4.1 Field

UserId

#### 5.3.4.2 Index Type

Keyword

#### 5.3.4.3 Search Pattern

Exact match filter

#### 5.3.4.4 Frequency

medium

### 5.3.5.0 Field

#### 5.3.5.1 Field

ScriptId

#### 5.3.5.2 Index Type

Keyword

#### 5.3.5.3 Search Pattern

Exact match filter

#### 5.3.5.4 Frequency

medium

### 5.3.6.0 Field

#### 5.3.6.1 Field

EventType

#### 5.3.6.2 Index Type

Keyword

#### 5.3.6.3 Search Pattern

Exact match filter for audit logs

#### 5.3.6.4 Frequency

low

## 5.4.0.0 Full Text Search

### 5.4.1.0 Required

✅ Yes

### 5.4.2.0 Fields

- Message
- Exception

### 5.4.3.0 Search Engine

Handled by the central log aggregator (e.g., Lucene in Elasticsearch).

### 5.4.4.0 Relevance Scoring

✅ Yes

## 5.5.0.0 Correlation And Tracing

### 5.5.1.0 Correlation Ids

- CorrelationId

### 5.5.2.0 Trace Id Propagation

A Correlation ID will be generated by API/Web Layer middleware for each incoming request and attached to the Serilog LogContext. This ID will be passed to all subsequent services.

### 5.5.3.0 Span Correlation

❌ No

### 5.5.4.0 Cross Service Tracing

❌ No

## 5.6.0.0 Dashboard Requirements

### 5.6.1.0 Dashboard

#### 5.6.1.1 Dashboard

Application Error Rate

#### 5.6.1.2 Purpose

To visualize the number of errors (WARN, ERROR, FATAL) over time.

#### 5.6.1.3 Refresh Interval

1 minute

#### 5.6.1.4 Audience

Operators, Developers

### 5.6.2.0 Dashboard

#### 5.6.2.1 Dashboard

Security Audit Events

#### 5.6.2.2 Purpose

To monitor and review security-sensitive events like script modifications and sandbox violations.

#### 5.6.2.3 Refresh Interval

5 minutes

#### 5.6.2.4 Audience

Security Analysts, Auditors

# 6.0.0.0 Storage Solution Selection

## 6.1.0.0 Selected Technology

### 6.1.1.0 Primary

Time-series document database (e.g., Elasticsearch, Loki)

### 6.1.2.0 Reasoning

These technologies are purpose-built for high-volume ingestion, storage, and fast querying of log data.

### 6.1.3.0 Alternatives

- Cloud-native logging services (e.g., AWS CloudWatch Logs, Azure Monitor Logs)

## 6.2.0.0 Scalability Requirements

| Property | Value |
|----------|-------|
| Expected Growth Rate | Proportional to application usage. |
| Peak Load Handling | Must handle bursts of logs during high traffic or ... |
| Horizontal Scaling | ✅ |

## 6.3.0.0 Cost Performance Analysis

### 6.3.1.0 Solution

#### 6.3.1.1 Solution

Self-hosted ELK/Loki

#### 6.3.1.2 Cost Per Gb

Low to medium (hardware/VM costs)

#### 6.3.1.3 Query Performance

High

#### 6.3.1.4 Operational Complexity

high

### 6.3.2.0 Solution

#### 6.3.2.1 Solution

SaaS Logging Platform

#### 6.3.2.2 Cost Per Gb

High

#### 6.3.2.3 Query Performance

High

#### 6.3.2.4 Operational Complexity

low

## 6.4.0.0 Backup And Recovery

| Property | Value |
|----------|-------|
| Backup Frequency | Daily snapshots |
| Recovery Time Objective | 4 hours |
| Recovery Point Objective | 24 hours |
| Testing Frequency | Annually |

## 6.5.0.0 Geo Distribution

### 6.5.1.0 Required

❌ No

### 6.5.2.0 Regions

*No items available*

### 6.5.3.0 Replication Strategy



## 6.6.0.0 Data Sovereignty

*No items available*

# 7.0.0.0 Access Control And Compliance

## 7.1.0.0 Access Control Requirements

### 7.1.1.0 Role

#### 7.1.1.1 Role

Developer

#### 7.1.1.2 Permissions

- read

#### 7.1.1.3 Log Types

- ApplicationLog
- DebugLog

#### 7.1.1.4 Justification

Required for troubleshooting application behavior in dev and staging.

### 7.1.2.0 Role

#### 7.1.2.1 Role

Operator

#### 7.1.2.2 Permissions

- read

#### 7.1.2.3 Log Types

- ApplicationLog

#### 7.1.2.4 Justification

Required for monitoring and responding to production incidents.

### 7.1.3.0 Role

#### 7.1.3.1 Role

SecurityAnalyst

#### 7.1.3.2 Permissions

- read

#### 7.1.3.3 Log Types

- AuditLog
- ApplicationLog

#### 7.1.3.4 Justification

Required for security investigations and compliance monitoring.

## 7.2.0.0 Sensitive Data Handling

### 7.2.1.0 Data Type

#### 7.2.1.1 Data Type

Full Script Content

#### 7.2.1.2 Handling Strategy

exclude

#### 7.2.1.3 Fields

*No items available*

#### 7.2.1.4 Compliance Requirement

To avoid leaking potentially sensitive business logic into logs.

### 7.2.2.0 Data Type

#### 7.2.2.1 Data Type

Personally Identifiable Information (PII)

#### 7.2.2.2 Handling Strategy

mask

#### 7.2.2.3 Fields

*No items available*

#### 7.2.2.4 Compliance Requirement

General data protection best practice. Any PII accidentally captured in exception messages should be masked by the logging pipeline if possible.

## 7.3.0.0 Encryption Requirements

### 7.3.1.0 In Transit

| Property | Value |
|----------|-------|
| Required | ✅ |
| Protocol | TLS 1.2+ |
| Certificate Management | Handled by the container orchestrator and log aggr... |

### 7.3.2.0 At Rest

| Property | Value |
|----------|-------|
| Required | ✅ |
| Algorithm | AES-256 |
| Key Management | Handled by the log aggregation platform's storage ... |

## 7.4.0.0 Audit Trail

| Property | Value |
|----------|-------|
| Log Access | ✅ |
| Retention Period | 365 days |
| Audit Log Location | Within the log aggregation platform itself. |
| Compliance Reporting | ✅ |

## 7.5.0.0 Regulatory Compliance

- {'regulation': 'SOC2', 'applicableComponents': ['AuditLog'], 'specificRequirements': ['Log integrity, immutability, and 1-year retention as per REQ-COMP-DTR-001.'], 'evidenceCollection': 'Exportable reports from the central log aggregator.'}

## 7.6.0.0 Data Protection Measures

- {'measure': 'Role-Based Access Control on logs', 'implementation': 'Configuration within the central log aggregation platform.', 'monitoringRequired': True}

# 8.0.0.0 Project Specific Logging Config

## 8.1.0.0 Logging Config

### 8.1.1.0 Level

🔹 Information

### 8.1.2.0 Retention

365 days (Audit), 90 days (Application)

### 8.1.3.0 Aggregation

Centralized via container stdout

### 8.1.4.0 Storage

Time-series document database

### 8.1.5.0 Configuration

| Property | Value |
|----------|-------|
| Serilog.minimum Level.default | Information |
| Serilog.minimum Level.override.microsoft | Warning |
| Serilog.minimum Level.override.system | Warning |
| Serilog.write To.console.formatter | Serilog.Formatting.Json.JsonFormatter, Serilog |

## 8.2.0.0 Component Configurations

- {'component': 'JintTransformationEngine', 'logLevel': 'Information', 'outputFormat': 'JSON', 'destinations': ['Console'], 'sampling': {'enabled': False, 'rate': ''}, 'customFields': ['ScriptId', 'ReportJobId', 'ExecutionTimeMs', 'MemoryUsageMb']}

## 8.3.0.0 Metrics

### 8.3.1.0 Custom Metrics

*No data available*

## 8.4.0.0 Alert Rules

### 8.4.1.0 Jint Engine Internal Failure

#### 8.4.1.1 Name

Jint Engine Internal Failure

#### 8.4.1.2 Condition

count_over_time(logs{level="fatal", SourceContext="Infrastructure.Jint"}[5m]) > 0

#### 8.4.1.3 Severity

Critical

#### 8.4.1.4 Actions

- {'type': 'PagerDuty', 'target': 'on-call-devops', 'configuration': {}}

#### 8.4.1.5 Suppression Rules

*No items available*

#### 8.4.1.6 Escalation Path

*No items available*

### 8.4.2.0 High Rate of Script Sandbox Violations

#### 8.4.2.1 Name

High Rate of Script Sandbox Violations

#### 8.4.2.2 Condition

sum(rate(logs{level="error", EventType="SandboxViolation"}[5m])) > 5

#### 8.4.2.3 Severity

High

#### 8.4.2.4 Actions

- {'type': 'Slack', 'target': '#alerts-security', 'configuration': {}}

#### 8.4.2.5 Suppression Rules

*No items available*

#### 8.4.2.6 Escalation Path

*No items available*

# 9.0.0.0 Implementation Priority

## 9.1.0.0 Component

### 9.1.1.0 Component

Base Serilog Configuration

### 9.1.2.0 Priority

🔴 high

### 9.1.3.0 Dependencies

*No items available*

### 9.1.4.0 Estimated Effort

Low

### 9.1.5.0 Risk Level

low

## 9.2.0.0 Component

### 9.2.1.0 Component

Correlation ID Middleware

### 9.2.2.0 Priority

🔴 high

### 9.2.3.0 Dependencies

- Base Serilog Configuration

### 9.2.4.0 Estimated Effort

Low

### 9.2.5.0 Risk Level

low

## 9.3.0.0 Component

### 9.3.1.0 Component

Specific Audit Logging for CRUD & Violations

### 9.3.2.0 Priority

🔴 high

### 9.3.3.0 Dependencies

- Base Serilog Configuration

### 9.3.4.0 Estimated Effort

Medium

### 9.3.5.0 Risk Level

medium

## 9.4.0.0 Component

### 9.4.1.0 Component

Log Aggregator Configuration

### 9.4.2.0 Priority

🟡 medium

### 9.4.3.0 Dependencies

*No items available*

### 9.4.4.0 Estimated Effort

High

### 9.4.5.0 Risk Level

medium

# 10.0.0.0 Risk Assessment

## 10.1.0.0 Risk

### 10.1.1.0 Risk

Failure to meet audit log retention and immutability requirements.

### 10.1.2.0 Impact

high

### 10.1.3.0 Probability

medium

### 10.1.4.0 Mitigation

The primary audit trail is in the database (AuditLog table). Centralized logs are a searchable replica. Ensure the database backup/recovery (REQ-OPER-DTR-002) and retention policies are correctly implemented and tested.

### 10.1.5.0 Contingency Plan

Rely on database backups for audit trail recovery in case of logging system failure.

## 10.2.0.0 Risk

### 10.2.1.0 Risk

Excessive logging volume impacts application performance or incurs high costs.

### 10.2.2.0 Impact

medium

### 10.2.3.0 Probability

high

### 10.2.4.0 Mitigation

Set default log level to Information. Implement dynamic log level configuration to allow for temporary debugging without redeployment. Monitor log ingestion volume and costs.

### 10.2.5.0 Contingency Plan

Temporarily raise the minimum log level to Warning at the aggregator level to shed load.

# 11.0.0.0 Recommendations

## 11.1.0.0 Category

### 11.1.1.0 Category

🔹 Configuration

### 11.1.2.0 Recommendation

Implement structured logging with Serilog from the start of the project. Do not use unstructured text logging.

### 11.1.3.0 Justification

Structured logging is essential for meeting the filtering and analysis requirements for security (REQ-SEC-DTR-002) and troubleshooting (REQ-FUNC-DTR-003).

### 11.1.4.0 Priority

🔴 high

### 11.1.5.0 Implementation Notes

Define a base logging configuration shared across all services.

## 11.2.0.0 Category

### 11.2.1.0 Category

🔹 Observability

### 11.2.2.0 Recommendation

Ensure the Correlation ID is added to the log context at the beginning of every API request and is included in every log message for that request's lifecycle.

### 11.2.3.0 Justification

This is the single most important practice for enabling effective troubleshooting in a distributed or microservices-style architecture, and remains critical in a scaled monolith.

### 11.2.4.0 Priority

🔴 high

### 11.2.5.0 Implementation Notes

Create a simple ASP.NET Core middleware to manage the Correlation ID.

## 11.3.0.0 Category

### 11.3.1.0 Category

🔹 Security

### 11.3.2.0 Recommendation

Differentiate between the immutable database audit trail (source of truth) and the centralized logging system (searchable replica). Log security events to both.

### 11.3.3.0 Justification

This layered approach satisfies compliance requirements for integrity (database) while providing operational utility (searchable logs), fulfilling REQ-COMP-DTR-001.

### 11.3.4.0 Priority

🟡 medium

### 11.3.5.0 Implementation Notes

The SecurityAuditService should write to the AuditLog table and also write a structured log event via Serilog.

