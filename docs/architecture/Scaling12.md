# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- SQLite
- Quartz.NET
- React 18
- Windows Server

## 1.3 Architecture Patterns

- Hybrid, Modular Monolith
- On-Premise
- Single-Instance Deployment

## 1.4 Resource Needs

- CPU for report generation and JS transformation
- Memory for in-memory data transformation
- Disk I/O for SQLite database and report storage

## 1.5 Performance Expectations

Process 1M records within 5 minutes; P95 API latency < 200ms for up to 100 concurrent users.

## 1.6 Data Processing Volumes

Up to 1,000 scheduled reports per day; datasets up to 1 million records.

# 2.0 Workload Characterization

## 2.1 Processing Resource Consumption

- {'operation': 'Report Generation (with transformation)', 'cpuPattern': 'bursty', 'cpuUtilization': {'baseline': '< 10%', 'peak': '50-90%', 'average': '20%'}, 'memoryPattern': 'fluctuating', 'memoryRequirements': {'baseline': '< 1GB', 'peak': '4GB+', 'growth': 'Proportional to dataset size'}, 'ioCharacteristics': {'diskIOPS': 'Moderate (SQLite writes for logs, report file creation)', 'networkThroughput': 'High during data ingestion/delivery', 'ioPattern': 'mixed'}}

## 2.2 Concurrency Requirements

### 2.2.1 Operation

#### 2.2.1.1 Operation

Scheduled Report Jobs

#### 2.2.1.2 Max Concurrent Jobs

10

#### 2.2.1.3 Thread Pool Size

10

#### 2.2.1.4 Connection Pool Size

0

#### 2.2.1.5 Queue Depth

1,000

### 2.2.2.0 Operation

#### 2.2.2.1 Operation

Web UI Users

#### 2.2.2.2 Max Concurrent Jobs

100

#### 2.2.2.3 Thread Pool Size

100

#### 2.2.2.4 Connection Pool Size

0

#### 2.2.2.5 Queue Depth

0

## 2.3.0.0 Database Access Patterns

- {'accessType': 'mixed', 'connectionRequirements': 'Embedded (SQLite file access)', 'queryComplexity': 'simple', 'transactionVolume': 'High (job logs, audit logs)', 'cacheHitRatio': 'N/A'}

## 2.4.0.0 Frontend Resource Demands

- {'component': 'Control Panel / Report Viewer', 'renderingLoad': 'moderate', 'staticContentSize': '< 10MB', 'dynamicContentVolume': 'High (API calls for dashboards, logs)', 'userConcurrency': 'Up to 100 users'}

## 2.5.0.0 Load Patterns

- {'pattern': 'event-driven', 'description': 'Workload is primarily driven by CRON schedules for report generation, which can cause predictable spikes in resource usage.', 'frequency': 'Varies (per-minute to daily)', 'magnitude': 'High CPU/Memory bursts during job execution', 'predictability': 'high'}

# 3.0.0.0 Scaling Strategy Design

## 3.1.0.0 Scaling Approaches

- {'component': 'Windows Service (Monolith)', 'primaryStrategy': 'vertical', 'justification': "Requirement 6.2 explicitly states 'The primary scaling mechanism shall be vertical scaling (increasing CPU/RAM of the host machine).' The system is designed as a single deployment unit.", 'limitations': ['Single Point of Failure (SPOF)', 'Finite scaling ceiling per host', 'Requires downtime for scaling operations'], 'implementation': 'Manually resizing the underlying Virtual Machine (e.g., changing instance type in a cloud provider or reallocating resources in a hypervisor).'}

## 3.2.0.0 Instance Specifications

- {'workloadType': 'General Purpose / Reporting', 'instanceFamily': 'Balanced (e.g., AWS m5, Azure Dsv4)', 'instanceSize': "Start with 4 vCPUs, 16GB RAM as per 'recommended' hardware.", 'vCPUs': 4, 'memoryGB': 16, 'storageType': 'General Purpose SSD', 'networkPerformance': 'Moderate', 'optimization': 'balanced'}

## 3.3.0.0 Multithreading Considerations

- {'component': 'Quartz.NET Job Scheduler', 'threadingModel': 'multi', 'optimalThreads': 10, 'scalingCharacteristics': 'sublinear', 'bottlenecks': ['CPU during parallel transformations', 'Memory if multiple large datasets are transformed simultaneously', 'Disk I/O for SQLite contention']}

## 3.4.0.0 Specialized Hardware

*No items available*

## 3.5.0.0 Storage Scaling

- {'storageType': 'file', 'scalingMethod': 'vertical', 'performance': 'Dependent on underlying disk (SSD recommended)', 'consistency': 'N/A'}

## 3.6.0.0 Licensing Implications

- {'software': 'Windows Server', 'licensingModel': 'per-core', 'scalingImpact': 'Cost increases with vertical scaling (more vCPUs).', 'costOptimization': 'N/A'}

# 4.0.0.0 Auto Scaling Trigger Metrics

## 4.1.0.0 Cpu Utilization Triggers

- {'component': 'Host VM', 'scaleUpThreshold': 80, 'scaleDownThreshold': 0, 'evaluationPeriods': 1, 'dataPoints': 1, 'justification': 'A sustained CPU utilization above 80% for 30 minutes indicates the machine is consistently overloaded and is a primary trigger for a manual vertical scaling review.'}

## 4.2.0.0 Memory Consumption Triggers

- {'component': 'Host VM', 'scaleUpThreshold': 85, 'scaleDownThreshold': 0, 'evaluationPeriods': 1, 'triggerCondition': 'used', 'justification': 'Sustained high memory usage indicates frequent swapping and performance degradation, triggering a manual vertical scaling review.'}

## 4.3.0.0 Database Connection Triggers

*No items available*

## 4.4.0.0 Queue Length Triggers

- {'queueType': 'job', 'scaleUpThreshold': 50, 'scaleDownThreshold': 0, 'ageThreshold': '10m', 'priority': 'normal'}

## 4.5.0.0 Response Time Triggers

- {'endpoint': '/api/v1/*', 'p95Threshold': '500ms', 'p99Threshold': '1000ms', 'evaluationWindow': '5m', 'userImpact': 'Sustained high API latency indicates system overload and poor user experience, triggering a manual vertical scaling review.'}

## 4.6.0.0 Custom Metric Triggers

*No items available*

## 4.7.0.0 Disk Iotriggers

*No items available*

# 5.0.0.0 Scaling Limits And Safeguards

## 5.1.0.0 Instance Limits

- {'component': 'Windows Service (Monolith)', 'minInstances': 1, 'maxInstances': 1, 'justification': 'The architecture is a single, non-clustered monolith. Horizontal scaling is explicitly out of scope for the initial release.', 'costImplication': 'Predictable, single-instance cost.'}

## 5.2.0.0 Cooldown Periods

*No items available*

## 5.3.0.0 Scaling Step Sizes

*No items available*

## 5.4.0.0 Runaway Protection

*No items available*

## 5.5.0.0 Graceful Degradation

- {'scenario': 'Sustained resource exhaustion (high CPU/memory)', 'strategy': 'Report jobs will queue and experience delays. API response times will increase.', 'implementation': 'The Quartz.NET scheduler and .NET thread pool will naturally queue work, preventing system crashes but degrading performance.', 'userImpact': 'Slow UI, delayed report delivery.'}

## 5.6.0.0 Resource Quotas

*No items available*

## 5.7.0.0 Workload Prioritization

*No items available*

# 6.0.0.0 Cost Optimization Strategy

## 6.1.0.0 Instance Right Sizing

- {'component': 'Host VM', 'currentSize': '4 vCPU, 16 GB RAM (Recommended Start)', 'recommendedSize': 'Monitor and adjust based on real-world utilization.', 'utilizationTarget': '60-70% average CPU/Memory during peak hours', 'costSavings': 'Potential savings by starting smaller if initial load is low, or avoiding performance issues by starting at recommended size.'}

## 6.2.0.0 Time Based Scaling

- {'schedule': 'N/A', 'timezone': 'N/A', 'scaleAction': 'N/A', 'instanceCount': 0, 'justification': 'Automatic time-based vertical scaling is not feasible as it requires instance reboots. However, administrators should schedule resource-intensive reports for off-peak hours.'}

## 6.3.0.0 Instance Termination Policies

*No items available*

## 6.4.0.0 Spot Instance Strategies

*No items available*

## 6.5.0.0 Reserved Instance Planning

- {'instanceType': 'General Purpose (Balanced)', 'reservationTerm': '1-year', 'utilizationForecast': 'Continuous (always on)', 'baselineInstances': 1, 'paymentOption': 'partial-upfront'}

## 6.6.0.0 Resource Tracking

*No items available*

## 6.7.0.0 Cleanup Policies

*No items available*

# 7.0.0.0 Load Testing And Validation

## 7.1.0.0 Baseline Metrics

### 7.1.1.0 Metric

#### 7.1.1.1 Metric

Report Generation Time (1M records)

#### 7.1.1.2 Baseline Value

< 5 minutes

#### 7.1.1.3 Acceptable Variation

10%

#### 7.1.1.4 Measurement Method

Integration Test Suite

### 7.1.2.0 Metric

#### 7.1.2.1 Metric

API P95 Latency (100 users)

#### 7.1.2.2 Baseline Value

< 200ms

#### 7.1.2.3 Acceptable Variation

20%

#### 7.1.2.4 Measurement Method

Load Testing Tool (k6, JMeter)

## 7.2.0.0 Validation Procedures

- {'procedure': 'Post-scaling validation', 'frequency': 'After every manual vertical scaling event', 'successCriteria': ['Baseline performance metrics are met or exceeded', 'System stability is confirmed under load'], 'failureActions': ['Revert to previous instance size', 'Investigate performance bottleneck']}

## 7.3.0.0 Synthetic Load Scenarios

*No items available*

## 7.4.0.0 Scaling Event Monitoring

*No items available*

## 7.5.0.0 Policy Refinement

*No items available*

## 7.6.0.0 Effectiveness Kpis

*No items available*

## 7.7.0.0 Feedback Mechanisms

*No items available*

# 8.0.0.0 Project Specific Scaling Policies

## 8.1.0.0 Policies

- {'id': 'manual-vertical-scaling-policy-01', 'type': 'Manual', 'component': 'Windows Service Host VM', 'rules': [{'metric': 'cpu_utilization_percentage', 'threshold': 80, 'operator': 'GREATER_THAN', 'scaleChange': 0, 'cooldown': {'scaleUpSeconds': 0, 'scaleDownSeconds': 0}, 'evaluationPeriods': 6, 'dataPointsToAlarm': 5}, {'metric': 'memory_utilization_percentage', 'threshold': 85, 'operator': 'GREATER_THAN', 'scaleChange': 0, 'cooldown': {'scaleUpSeconds': 0, 'scaleDownSeconds': 0}, 'evaluationPeriods': 6, 'dataPointsToAlarm': 5}], 'safeguards': {'minInstances': 1, 'maxInstances': 1, 'maxScalingRate': 'N/A', 'costThreshold': 'N/A'}, 'schedule': {'enabled': False, 'timezone': 'UTC', 'rules': []}}

## 8.2.0.0 Configuration

### 8.2.1.0 Min Instances

1

### 8.2.2.0 Max Instances

1

### 8.2.3.0 Default Timeout

N/A

### 8.2.4.0 Region

Customer-defined

### 8.2.5.0 Resource Group

Customer-defined

### 8.2.6.0 Notification Endpoint

Administrator email list

### 8.2.7.0 Logging Level

Information

### 8.2.8.0 Vpc Id

Customer-defined

### 8.2.9.0 Instance Type

Balanced/General Purpose

### 8.2.10.0 Enable Detailed Monitoring

true

### 8.2.11.0 Scaling Mode

reactive

### 8.2.12.0 Cost Optimization

#### 8.2.12.1 Spot Instances Enabled

❌ No

#### 8.2.12.2 Reserved Instances Planned

✅ Yes

### 8.2.13.0 Performance Targets

| Property | Value |
|----------|-------|
| Response Time | < 200ms (P95) |
| Throughput | 1000 jobs/day |
| Availability | 99.9% |

## 8.3.0.0 Environment Specific Policies

- {'environment': 'production', 'scalingEnabled': True, 'aggressiveness': 'conservative', 'costPriority': 'performance'}

# 9.0.0.0 Implementation Priority

- {'component': 'Host Monitoring & Alerting Setup', 'priority': 'high', 'dependencies': [], 'estimatedEffort': 'Low', 'riskLevel': 'low'}

# 10.0.0.0 Risk Assessment

## 10.1.0.0 Risk

### 10.1.1.0 Risk

Single Point of Failure (SPOF)

### 10.1.2.0 Impact

high

### 10.1.3.0 Probability

medium

### 10.1.4.0 Mitigation

The SRS specifies a single instance. Mitigation relies on robust monitoring for proactive issue detection and a well-documented, tested backup and recovery procedure (as required by SRS 6.3).

### 10.1.5.0 Contingency Plan

Execute disaster recovery plan: provision a new VM, reinstall the application via MSI, and restore the configuration from the latest backup. RTO is 4 hours.

## 10.2.0.0 Risk

### 10.2.1.0 Risk

Vertical scaling ceiling reached

### 10.2.2.0 Impact

high

### 10.2.3.0 Probability

low

### 10.2.4.0 Mitigation

Right-size the initial instance based on load testing. For future growth, this would necessitate an architectural change to support horizontal scaling, which is out of scope for the initial release.

### 10.2.5.0 Con શરીplan

Optimize resource-intensive reports and transformations. Plan for architectural redesign to introduce horizontal scaling.

# 11.0.0.0 Recommendations

## 11.1.0.0 Category

### 11.1.1.0 Category

🔹 Reliability

### 11.1.2.0 Recommendation

Implement a comprehensive monitoring and alerting dashboard for the single host VM.

### 11.1.3.0 Justification

Since vertical scaling is a manual process, proactive alerting on key metrics (CPU, Memory, API latency) is the only way for IT Support to know when a scaling operation is required to prevent performance degradation or an outage.

### 11.1.4.0 Priority

🔴 high

### 11.1.5.0 Implementation Notes

Use standard tools like Prometheus with Node Exporter and Grafana, or a cloud provider's native monitoring service (e.g., AWS CloudWatch, Azure Monitor).

## 11.2.0.0 Category

### 11.2.1.0 Category

🔹 Operations

### 11.2.2.0 Recommendation

Automate and regularly test the backup and restore procedure.

### 11.2.3.0 Justification

Given the single-instance architecture, the backup and restore process is the primary disaster recovery mechanism. Its reliability is critical to meeting the RPO/RTO defined in SRS 6.3.

### 11.2.4.0 Priority

🔴 high

### 11.2.5.0 Implementation Notes

Create a scheduled task or script to perform daily backups of the application's data directory (which includes the SQLite database) to a separate, durable storage location.

