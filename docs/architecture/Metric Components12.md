# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- PostgreSQL 16
- React 18
- Jint
- Redis

## 1.3 Monitoring Components

- prometheus-net
- Serilog
- AspNetCore.HealthChecks

## 1.4 Requirements

- REQ-OPER-DTR-001
- REQ-PERF-DTR-002
- REQ-SEC-DTR-002
- REQ-REL-DTR-001
- REQ-OPER-DTR-003

## 1.5 Environment

production

# 2.0 Standard System Metrics Selection

## 2.1 Hardware Utilization Metrics

### 2.1.1 counter

#### 2.1.1.1 Name

node_cpu_seconds_total

#### 2.1.1.2 Type

🔹 counter

#### 2.1.1.3 Unit

seconds

#### 2.1.1.4 Description

Total CPU time spent in seconds, aggregated by mode (user, system, idle). Essential for capacity planning and detecting CPU-bound performance issues.

#### 2.1.1.5 Collection

##### 2.1.1.5.1 Interval

30s

##### 2.1.1.5.2 Method

Prometheus Node Exporter

#### 2.1.1.6.0 Thresholds

##### 2.1.1.6.1 Warning

rate > 80% for 5m

##### 2.1.1.6.2 Critical

rate > 95% for 5m

#### 2.1.1.7.0 Justification

Provides fundamental health monitoring for the host system to ensure performance benchmarks (REQ-PERF-DTR-002) can be met.

### 2.1.2.0.0 gauge

#### 2.1.2.1.0 Name

node_memory_MemAvailable_bytes

#### 2.1.2.2.0 Type

🔹 gauge

#### 2.1.2.3.0 Unit

bytes

#### 2.1.2.4.0 Description

Available memory on the host system. Crucial for preventing OutOfMemory errors at the OS level, which would impact service availability (REQ-OPER-DTR-003).

#### 2.1.2.5.0 Collection

##### 2.1.2.5.1 Interval

30s

##### 2.1.2.5.2 Method

Prometheus Node Exporter

#### 2.1.2.6.0 Thresholds

##### 2.1.2.6.1 Warning

< 15% of total

##### 2.1.2.6.2 Critical

< 5% of total

#### 2.1.2.7.0 Justification

Ensures the underlying host has sufficient resources for the application and the Jint engine's memory demands.

## 2.2.0.0.0 Runtime Metrics

### 2.2.1.0.0 gauge

#### 2.2.1.1.0 Name

dotnet_gc_heap_size_bytes

#### 2.2.1.2.0 Type

🔹 gauge

#### 2.2.1.3.0 Unit

bytes

#### 2.2.1.4.0 Description

Total .NET garbage collection heap size. Monitors the application's memory footprint to diagnose memory leaks or inefficient memory usage.

#### 2.2.1.5.0 Technology

.NET

#### 2.2.1.6.0 Collection

##### 2.2.1.6.1 Interval

15s

##### 2.2.1.6.2 Method

prometheus-net built-in

#### 2.2.1.7.0 Criticality

medium

### 2.2.2.0.0 gauge

#### 2.2.2.1.0 Name

dotnet_threadpool_queue_length

#### 2.2.2.2.0 Type

🔹 gauge

#### 2.2.2.3.0 Unit

count

#### 2.2.2.4.0 Description

Number of work items currently queued in the .NET thread pool. A consistently high number indicates thread pool starvation and potential performance bottlenecks.

#### 2.2.2.5.0 Technology

.NET

#### 2.2.2.6.0 Collection

##### 2.2.2.6.1 Interval

15s

##### 2.2.2.6.2 Method

prometheus-net built-in

#### 2.2.2.7.0 Criticality

high

## 2.3.0.0.0 Request Response Metrics

- {'name': 'http_server_request_duration_seconds', 'type': 'histogram', 'unit': 'seconds', 'description': 'Latency of HTTP requests to the API, essential for monitoring overall application responsiveness and UI performance.', 'dimensions': ['method', 'endpoint', 'status_code'], 'percentiles': ['p95', 'p99'], 'collection': {'interval': 'real-time', 'method': 'prometheus-net ASP.NET Core middleware'}}

## 2.4.0.0.0 Availability Metrics

- {'name': 'service_uptime_status', 'type': 'gauge', 'unit': 'boolean', 'description': 'Binary gauge (1 for up, 0 for down) based on health check results. Used to calculate overall availability.', 'calculation': 'Derived from the /healthz endpoint status.', 'slaTarget': '99.9% (REQ-OPER-DTR-003)'}

## 2.5.0.0.0 Scalability Metrics

*No items available*

# 3.0.0.0.0 Application Specific Metrics Design

## 3.1.0.0.0 Transaction Metrics

### 3.1.1.0.0 histogram

#### 3.1.1.1.0 Name

transformation_script_execution_seconds

#### 3.1.1.2.0 Type

🔹 histogram

#### 3.1.1.3.0 Unit

seconds

#### 3.1.1.4.0 Description

Duration of individual transformation script executions. Directly required by REQ-OPER-DTR-001 for performance monitoring and to validate the benchmark in REQ-PERF-DTR-002.

#### 3.1.1.5.0 Business Context

Data Transformation Engine

#### 3.1.1.6.0 Dimensions

- script_id

#### 3.1.1.7.0 Collection

##### 3.1.1.7.1 Interval

real-time

##### 3.1.1.7.2 Method

Custom instrumentation in JintTransformationEngine

#### 3.1.1.8.0 Aggregation

##### 3.1.1.8.1 Functions

- avg
- max
- p95

##### 3.1.1.8.2 Window

1m, 5m, 15m

### 3.1.2.0.0 histogram

#### 3.1.2.1.0 Name

transformation_script_memory_usage_bytes

#### 3.1.2.2.0 Type

🔹 histogram

#### 3.1.2.3.0 Unit

bytes

#### 3.1.2.4.0 Description

Memory allocated by the Jint engine during a script execution. Directly required by REQ-OPER-DTR-001.

#### 3.1.2.5.0 Business Context

Data Transformation Engine

#### 3.1.2.6.0 Dimensions

- script_id

#### 3.1.2.7.0 Collection

##### 3.1.2.7.1 Interval

real-time

##### 3.1.2.7.2 Method

Custom instrumentation in JintTransformationEngine

#### 3.1.2.8.0 Aggregation

##### 3.1.2.8.1 Functions

- avg
- max

##### 3.1.2.8.2 Window

5m

## 3.2.0.0.0 Cache Performance Metrics

- {'name': 'cache_hits_misses_total', 'type': 'counter', 'unit': 'count', 'description': 'Tracks the number of hits and misses for the Redis cache, which is critical for understanding the effectiveness of the caching strategy for configuration and roles.', 'cacheType': 'Redis', 'hitRatioTarget': '> 95%'}

## 3.3.0.0.0 External Dependency Metrics

*No items available*

## 3.4.0.0.0 Error Metrics

- {'name': 'transformation_script_events_total', 'type': 'counter', 'unit': 'count', 'description': 'A single counter for all significant events related to script execution. Directly fulfills the requirement in REQ-OPER-DTR-001 to count timeouts, constraint violations, and script errors.', 'errorTypes': ['execution_started', 'execution_completed', 'timeout_violation', 'memory_violation', 'statement_violation', 'user_script_error', 'engine_failure'], 'dimensions': ['script_id', 'event_type'], 'alertThreshold': 'rate(engine_failure) > 0 for 5m'}

## 3.5.0.0.0 Throughput And Latency Metrics

*No items available*

# 4.0.0.0.0 Business Kpi Identification

## 4.1.0.0.0 Critical Business Metrics

- {'name': 'report_jobs_processed_total', 'type': 'counter', 'unit': 'count', 'description': 'Total number of report generation jobs processed, categorized by their final status. This is the primary operational KPI for the reporting system.', 'businessOwner': 'Reporting Team', 'calculation': "Incremented when a ReportJob row is updated to a terminal status ('Completed', 'Failed').", 'reportingFrequency': 'real-time', 'target': 'failure_rate < 1%'}

## 4.2.0.0.0 User Engagement Metrics

*No items available*

## 4.3.0.0.0 Conversion Metrics

*No items available*

## 4.4.0.0.0 Operational Efficiency Kpis

*No items available*

## 4.5.0.0.0 Revenue And Cost Metrics

*No items available*

## 4.6.0.0.0 Customer Satisfaction Indicators

*No items available*

# 5.0.0.0.0 Collection Interval Optimization

## 5.1.0.0.0 Sampling Frequencies

### 5.1.1.0.0 Metric Category

#### 5.1.1.1.0 Metric Category

API Request Metrics

#### 5.1.1.2.0 Interval

15s

#### 5.1.1.3.0 Justification

Provides a good balance between timely feedback on API performance and resource usage.

#### 5.1.1.4.0 Resource Impact

low

### 5.1.2.0.0 Metric Category

#### 5.1.2.1.0 Metric Category

Hardware Utilization

#### 5.1.2.2.0 Interval

30s

#### 5.1.2.3.0 Justification

Sufficient for detecting sustained resource pressure without excessive data generation.

#### 5.1.2.4.0 Resource Impact

low

### 5.1.3.0.0 Metric Category

#### 5.1.3.1.0 Metric Category

Custom Application Metrics

#### 5.1.3.2.0 Interval

real-time (on event)

#### 5.1.3.3.0 Justification

Events like script execution are captured as they happen to ensure no data is lost.

#### 5.1.3.4.0 Resource Impact

low

## 5.2.0.0.0 High Frequency Metrics

*No items available*

## 5.3.0.0.0 Cardinality Considerations

- {'metricName': 'transformation_script_execution_seconds', 'estimatedCardinality': 'Potentially high if there are thousands of unique scripts.', 'dimensionStrategy': 'Use `script_id` as a label.', 'mitigationApproach': 'Ensure monitoring system (Prometheus) is configured to handle the expected cardinality. If it becomes an issue, an aggregation layer or a lower-cardinality label (e.g., `script_group`) could be introduced.'}

## 5.4.0.0.0 Aggregation Periods

- {'metricType': 'Rates and Percentiles', 'periods': ['1m', '5m', '1h'], 'retentionStrategy': 'Store pre-aggregated metrics in Prometheus using recording rules.'}

## 5.5.0.0.0 Collection Methods

*No items available*

# 6.0.0.0.0 Aggregation Method Selection

## 6.1.0.0.0 Statistical Aggregations

- {'metricName': 'transformation_script_execution_seconds', 'aggregationFunctions': ['rate', 'sum', 'avg', 'max'], 'windows': ['1m', '5m'], 'justification': 'Provides a comprehensive view of script performance over time, as required by REQ-OPER-DTR-001.'}

## 6.2.0.0.0 Histogram Requirements

- {'metricName': 'transformation_script_execution_seconds', 'buckets': ['0.01', '0.05', '0.1', '0.25', '0.5', '1.0', '2.5', '5.0', '10.0', '30.0'], 'percentiles': ['p95', 'p99'], 'accuracy': 'High accuracy is needed for the buckets around the 10-second benchmark from REQ-PERF-DTR-002.'}

## 6.3.0.0.0 Percentile Calculations

*No items available*

## 6.4.0.0.0 Metric Types

### 6.4.1.0.0 transformation_script_events_total

#### 6.4.1.1.0 Name

transformation_script_events_total

#### 6.4.1.2.0 Implementation

counter

#### 6.4.1.3.0 Reasoning

This is a monotonically increasing value representing distinct events. A counter is the correct type.

#### 6.4.1.4.0 Resets Handling

Handled by the `rate()` function in PromQL.

### 6.4.2.0.0 dotnet_gc_heap_size_bytes

#### 6.4.2.1.0 Name

dotnet_gc_heap_size_bytes

#### 6.4.2.2.0 Implementation

gauge

#### 6.4.2.3.0 Reasoning

Represents a point-in-time value that can go up or down.

#### 6.4.2.4.0 Resets Handling

N/A

## 6.5.0.0.0 Dimensional Aggregation

*No items available*

## 6.6.0.0.0 Derived Metrics

- {'name': 'job_failure_rate', 'calculation': "sum(rate(report_jobs_processed_total{status='Failed'}[5m])) / sum(rate(report_jobs_processed_total[5m]))", 'sourceMetrics': ['report_jobs_processed_total'], 'updateFrequency': 'Calculated at query time in Prometheus.'}

# 7.0.0.0.0 Storage Requirements Planning

## 7.1.0.0.0 Retention Periods

### 7.1.1.0.0 Metric Type

#### 7.1.1.1.0 Metric Type

High-resolution metrics (raw scrapes)

#### 7.1.1.2.0 Retention Period

14 days

#### 7.1.1.3.0 Justification

Allows for detailed analysis of recent incidents.

#### 7.1.1.4.0 Compliance Requirement

None

### 7.1.2.0.0 Metric Type

#### 7.1.2.1.0 Metric Type

Aggregated metrics (1-hour resolution)

#### 7.1.2.2.0 Retention Period

1 year

#### 7.1.2.3.0 Justification

Supports long-term capacity planning and trend analysis.

#### 7.1.2.4.0 Compliance Requirement

None

## 7.2.0.0.0 Data Resolution

*No items available*

## 7.3.0.0.0 Downsampling Strategies

- {'sourceResolution': '15s', 'targetResolution': '1h', 'aggregationMethod': 'avg, max, sum', 'triggerCondition': 'After 14 days'}

## 7.4.0.0.0 Storage Performance

| Property | Value |
|----------|-------|
| Write Latency | < 100ms |
| Query Latency | < 2s for typical dashboard queries |
| Throughput Requirements | Handle metrics from all horizontally scaled applic... |
| Scalability Needs | Storage should scale with the number of applicatio... |

## 7.5.0.0.0 Query Optimization

*No items available*

## 7.6.0.0.0 Cost Optimization

*No items available*

# 8.0.0.0.0 Project Specific Metrics Config

## 8.1.0.0.0 Standard Metrics

*No items available*

## 8.2.0.0.0 Custom Metrics

*No items available*

## 8.3.0.0.0 Dashboard Metrics

*No items available*

# 9.0.0.0.0 Implementation Priority

*No items available*

# 10.0.0.0.0 Risk Assessment

*No items available*

# 11.0.0.0.0 Recommendations

*No items available*

