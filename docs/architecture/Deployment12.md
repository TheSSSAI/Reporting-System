# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- React 18
- SQLite
- Jint
- Puppeteer Sharp
- WiX Toolset

## 1.3 Architecture Patterns

- Hybrid, Modular Monolith
- On-Premise Deployment

## 1.4 Data Handling Needs

- Local data processing of sensitive information from customer data sources (DB, Files, OPC UA).
- Encrypted local storage for system configuration (SQLite via DPAPI).
- Strict data privacy requirement: customer data must not leave the local network.

## 1.5 Performance Expectations

Support for up to 100 concurrent users and 1,000 daily reports. Vertical scaling is the primary mechanism. Datasets up to 1 million records processed within 5 minutes on recommended hardware.

## 1.6 Regulatory Requirements

- HIPAA

# 2.0 Environment Strategy

## 2.1 Environment Types

### 2.1.1 Development

#### 2.1.1.1 Type

🔹 Development

#### 2.1.1.2 Purpose

Internal environment for developers to build and unit test features. Used for local debugging and rapid iteration.

#### 2.1.1.3 Usage Patterns

- Continuous integration builds
- Local debugging
- Feature branch testing

#### 2.1.1.4 Isolation Level

partial

#### 2.1.1.5 Data Policy

Mock data, developer-generated sample data, and anonymized test data only. No production data allowed.

#### 2.1.1.6 Lifecycle Management

Ephemeral; can be torn down and rebuilt on demand.

### 2.1.2.0 Testing

#### 2.1.2.1 Type

🔹 Testing

#### 2.1.2.2 Purpose

Internal environment for the QA team to perform integration, end-to-end, and regression testing on stable builds.

#### 2.1.2.3 Usage Patterns

- Manual and automated test suite execution
- Performance baseline testing
- User Acceptance Testing (UAT) for internal stakeholders

#### 2.1.2.4 Isolation Level

complete

#### 2.1.2.5 Data Policy

Curated, anonymized, and structured test datasets that cover a wide range of scenarios, including edge cases and large volumes.

#### 2.1.2.6 Lifecycle Management

Persistent, updated with builds that pass CI.

### 2.1.3.0 Staging

#### 2.1.3.1 Type

🔹 Staging

#### 2.1.3.2 Purpose

Internal pre-production environment that is a near-identical replica of the customer reference architecture. Used for final validation of the MSI installer and upgrade process.

#### 2.1.3.3 Usage Patterns

- Release candidate validation
- Full upgrade path testing
- Final security scans and performance validation

#### 2.1.3.4 Isolation Level

complete

#### 2.1.3.5 Data Policy

Anonymized, production-like datasets. Policies and configurations mirror production reference.

#### 2.1.3.6 Lifecycle Management

Persistent, mirrors production configuration, updated only with release candidates.

### 2.1.4.0 Production

#### 2.1.4.1 Type

🔹 Production

#### 2.1.4.2 Purpose

Live customer-hosted environment. The design provided is a reference architecture for customers to implement within their own infrastructure.

#### 2.1.4.3 Usage Patterns

- Automated report generation and delivery
- User access via Control Panel and Report Viewer
- API integrations

#### 2.1.4.4 Isolation Level

complete

#### 2.1.4.5 Data Policy

Live customer production data, subject to customer's internal data governance and security policies.

#### 2.1.4.6 Lifecycle Management

Long-lived, managed by the customer's IT Support team. Updates are applied via the provided MSI installer.

### 2.1.5.0 DR

#### 2.1.5.1 Type

🔹 DR

#### 2.1.5.2 Purpose

A customer-managed Disaster Recovery environment to be activated in case of a primary production host failure. Can be cold, warm, or hot standby based on customer RTO/RPO needs.

#### 2.1.5.3 Usage Patterns

- Failover target for production services
- Periodic restoration testing

#### 2.1.5.4 Isolation Level

complete

#### 2.1.5.5 Data Policy

Replicated or restored production data. Access is highly restricted.

#### 2.1.5.6 Lifecycle Management

Managed by the customer. Maintained in sync with production according to customer's DR plan.

## 2.2.0.0 Promotion Strategy

### 2.2.1.0 Workflow

Development -> Testing -> Staging -> Release. A signed-off MSI from Staging is the final artifact delivered to customers.

### 2.2.2.0 Approval Gates

- Code review and successful CI build for Dev->Test promotion.
- QA sign-off and passing all automated tests for Test->Staging promotion.
- Successful staging deployment, upgrade test, and final UAT for Staging->Release.

### 2.2.3.0 Automation Level

automated

### 2.2.4.0 Rollback Procedure

For internal environments, redeploy the previous successful build artifact. For customer production, uninstall the new version and reinstall the previous MSI, then restore the application data from a pre-upgrade backup.

## 2.3.0.0 Isolation Strategies

### 2.3.1.0 Environment

#### 2.3.1.1 Environment

Production (Customer)

#### 2.3.1.2 Isolation Type

complete

#### 2.3.1.3 Implementation

The application server is deployed on a dedicated VM or physical server within a secure, isolated network segment (private subnet) in the customer's datacenter.

#### 2.3.1.4 Justification

Required to enforce the strict data privacy constraint that no customer source data leaves their local network, and to minimize the attack surface.

### 2.3.2.0 Environment

#### 2.3.2.1 Environment

Testing vs. Development

#### 2.3.2.2 Isolation Type

network

#### 2.3.2.3 Implementation

Internal environments are hosted on separate VLANs or virtual networks with strict firewall rules preventing direct communication, except for authorized CI/CD systems.

#### 2.3.2.4 Justification

Prevents unstable code in Development from impacting the integrity of the QA testing environment.

## 2.4.0.0 Scaling Approaches

### 2.4.1.0 Environment

#### 2.4.1.1 Environment

Production

#### 2.4.1.2 Scaling Type

vertical

#### 2.4.1.3 Triggers

- Sustained high CPU or memory utilization during peak report generation hours.
- Increased dataset size or report complexity.

#### 2.4.1.4 Limits

Dependent on the customer's hypervisor or hardware capabilities.

### 2.4.2.0 Environment

#### 2.4.2.1 Environment

Testing

#### 2.4.2.2 Scaling Type

vertical

#### 2.4.2.3 Triggers

- Need to perform load and stress testing that exceeds baseline hardware.

#### 2.4.2.4 Limits

Scaled up on-demand for specific test runs, then scaled back down to conserve resources.

## 2.5.0.0 Provisioning Automation

| Property | Value |
|----------|-------|
| Tool | terraform |
| Templating | Modules for creating Windows Server VMs on a chose... |
| State Management | Remote state backend (e.g., Terraform Cloud, S3 wi... |
| Cicd Integration | ✅ |

# 3.0.0.0 Resource Requirements Analysis

## 3.1.0.0 Workload Analysis

### 3.1.1.0 Workload Type

#### 3.1.1.1 Workload Type

Report Generation (PDF)

#### 3.1.1.2 Expected Load

Can be bursty, based on schedules.

#### 3.1.1.3 Peak Capacity

Up to 10 concurrent jobs as per default Quartz.NET configuration.

#### 3.1.1.4 Resource Profile

cpu-intensive|memory-intensive

### 3.1.2.0 Workload Type

#### 3.1.2.1 Workload Type

Web UI / API Serving

#### 3.1.2.2 Expected Load

Up to 100 concurrent users.

#### 3.1.2.3 Peak Capacity

P95 latency under 200ms.

#### 3.1.2.4 Resource Profile

balanced

### 3.1.3.0 Workload Type

#### 3.1.3.1 Workload Type

Data Transformation

#### 3.1.3.2 Expected Load

Varies by report; can be significant for large datasets.

#### 3.1.3.3 Peak Capacity

Up to 4GB memory for a 1M record dataset.

#### 3.1.3.4 Resource Profile

memory-intensive

## 3.2.0.0 Compute Requirements

### 3.2.1.0 Environment

#### 3.2.1.1 Environment

Development

#### 3.2.1.2 Instance Type

Standard_D2s_v5 (Azure) or similar

#### 3.2.1.3 Cpu Cores

2

#### 3.2.1.4 Memory Gb

8

#### 3.2.1.5 Instance Count

1

#### 3.2.1.6 Auto Scaling

##### 3.2.1.6.1 Enabled

❌ No

##### 3.2.1.6.2 Min Instances

0

##### 3.2.1.6.3 Max Instances

0

##### 3.2.1.6.4 Scaling Triggers

*No items available*

#### 3.2.1.7.0 Justification

Sufficient for a single developer's workload and local debugging.

### 3.2.2.0.0 Environment

#### 3.2.2.1.0 Environment

Testing

#### 3.2.2.2.0 Instance Type

Standard_D4s_v5 (Azure) or similar

#### 3.2.2.3.0 Cpu Cores

4

#### 3.2.2.4.0 Memory Gb

16

#### 3.2.2.5.0 Instance Count

1

#### 3.2.2.6.0 Auto Scaling

##### 3.2.2.6.1 Enabled

❌ No

##### 3.2.2.6.2 Min Instances

0

##### 3.2.2.6.3 Max Instances

0

##### 3.2.2.6.4 Scaling Triggers

*No items available*

#### 3.2.2.7.0 Justification

Matches the recommended production specs to ensure valid performance testing.

### 3.2.3.0.0 Environment

#### 3.2.3.1.0 Environment

Production (Reference - Minimum)

#### 3.2.3.2.0 Instance Type

Customer-provisioned VM or physical server

#### 3.2.3.3.0 Cpu Cores

2

#### 3.2.3.4.0 Memory Gb

4

#### 3.2.3.5.0 Instance Count

1

#### 3.2.3.6.0 Auto Scaling

##### 3.2.3.6.1 Enabled

❌ No

##### 3.2.3.6.2 Min Instances

0

##### 3.2.3.6.3 Max Instances

0

##### 3.2.3.6.4 Scaling Triggers

*No items available*

#### 3.2.3.7.0 Justification

Based on explicit minimum hardware requirements in SRS 2.3.

### 3.2.4.0.0 Environment

#### 3.2.4.1.0 Environment

Production (Reference - Recommended)

#### 3.2.4.2.0 Instance Type

Customer-provisioned VM or physical server

#### 3.2.4.3.0 Cpu Cores

4

#### 3.2.4.4.0 Memory Gb

16

#### 3.2.4.5.0 Instance Count

1

#### 3.2.4.6.0 Auto Scaling

##### 3.2.4.6.1 Enabled

❌ No

##### 3.2.4.6.2 Min Instances

0

##### 3.2.4.6.3 Max Instances

0

##### 3.2.4.6.4 Scaling Triggers

*No items available*

#### 3.2.4.7.0 Justification

Based on explicit recommended hardware requirements in SRS 2.3 for large datasets.

## 3.3.0.0.0 Storage Requirements

### 3.3.1.0.0 Environment

#### 3.3.1.1.0 Environment

Production

#### 3.3.1.2.0 Storage Type

ssd

#### 3.3.1.3.0 Capacity

100 GB+ (as per SRS 2.3 recommendation)

#### 3.3.1.4.0 Iops Requirements

Standard performance tier.

#### 3.3.1.5.0 Throughput Requirements

N/A

#### 3.3.1.6.0 Redundancy

Handled by customer's underlying storage solution (e.g., SAN, RAID).

#### 3.3.1.7.0 Encryption

✅ Yes

### 3.3.2.0.0 Environment

#### 3.3.2.1.0 Environment

Testing

#### 3.3.2.2.0 Storage Type

ssd

#### 3.3.2.3.0 Capacity

100 GB

#### 3.3.2.4.0 Iops Requirements

Standard performance tier.

#### 3.3.2.5.0 Throughput Requirements

N/A

#### 3.3.2.6.0 Redundancy

Locally redundant storage is sufficient.

#### 3.3.2.7.0 Encryption

✅ Yes

## 3.4.0.0.0 Special Hardware Requirements

*No items available*

## 3.5.0.0.0 Scaling Strategies

- {'environment': 'Production', 'strategy': 'reactive', 'implementation': "Customer's IT Support team monitors resource utilization. If thresholds are consistently breached, they will vertically scale the host VM (increase CPU/RAM) during a planned maintenance window.", 'costOptimization': 'N/A (Customer managed).'}

# 4.0.0.0.0 Security Architecture

## 4.1.0.0.0 Authentication Controls

- {'method': 'sso', 'scope': 'Internal Environments (Dev, Test, Staging)', 'implementation': "Integration with company's Azure Active Directory for administrative access to the host VMs.", 'environment': 'Development|Testing|Staging'}

## 4.2.0.0.0 Authorization Controls

- {'model': 'rbac', 'implementation': 'Windows User Groups and Group Policy Objects (GPOs) to control OS-level access for IT Support users. The application itself implements RBAC for its own users.', 'granularity': 'coarse', 'environment': 'Production'}

## 4.3.0.0.0 Certificate Management

| Property | Value |
|----------|-------|
| Authority | hybrid |
| Rotation Policy | Annual rotation, or as per customer policy. |
| Automation | ✅ |
| Monitoring | ✅ |

## 4.4.0.0.0 Encryption Standards

### 4.4.1.0.0 Scope

#### 4.4.1.1.0 Scope

data-at-rest

#### 4.4.1.2.0 Algorithm

DPAPI (using AES-256)

#### 4.4.1.3.0 Key Management

Managed by the Windows OS, tied to the service account or machine.

#### 4.4.1.4.0 Compliance

- HIPAA

### 4.4.2.0.0 Scope

#### 4.4.2.1.0 Scope

data-in-transit

#### 4.4.2.2.0 Algorithm

TLS 1.2+

#### 4.4.2.3.0 Key Management

Customer-provided or internal CA-issued SSL/TLS certificates.

#### 4.4.2.4.0 Compliance

- HIPAA

## 4.5.0.0.0 Access Control Mechanisms

- {'type': 'iam', 'configuration': 'Principle of Least Privilege applied to service accounts running the Windows Service.', 'environment': 'Production', 'rules': ["Service account must have 'Log on as a service' rights.", 'Service account requires read/write access to its installation and data directories.', 'Service account requires permissions to read from source file shares if configured.']}

## 4.6.0.0.0 Data Protection Measures

- {'dataType': 'phi', 'protectionMethod': 'encryption', 'implementation': 'End-to-end encryption via TLS for data in transit and DPAPI for the configuration database at rest.', 'compliance': ['HIPAA']}

## 4.7.0.0.0 Network Security

- {'control': 'firewall', 'implementation': 'Windows Defender Firewall on the host, supplemented by network-level firewalls (Security Groups, ACLs).', 'rules': ['Default Deny All.', 'Allow inbound HTTPS from trusted internal subnets.', 'Allow outbound HTTPS to the specific IP range of the licensing service.', 'Allow outbound traffic on specific ports to specific internal data sources (e.g., TCP 1433 to a specific SQL Server IP).'], 'monitoring': True}

## 4.8.0.0.0 Security Monitoring

- {'type': 'siem', 'implementation': 'Customer is responsible for forwarding Windows Security Event Logs and application logs from the host to their central SIEM solution.', 'frequency': 'real-time', 'alerting': True}

## 4.9.0.0.0 Backup Security

| Property | Value |
|----------|-------|
| Encryption | ✅ |
| Access Control | Access to backup files must be restricted to autho... |
| Offline Storage | ✅ |
| Testing Frequency | Quarterly |

## 4.10.0.0.0 Compliance Frameworks

- {'framework': 'hipaa', 'applicableEnvironments': ['Production'], 'controls': ['Infrastructure must support technical safeguards such as unique user identification, access control (RBAC), audit controls (logging), data integrity (encryption), and transmission security (TLS).'], 'auditFrequency': "As per customer's internal audit schedule."}

# 5.0.0.0.0 Network Design

## 5.1.0.0.0 Network Segmentation

- {'environment': 'Production', 'segmentType': 'private', 'purpose': 'To host the application server, isolated from direct internet access.', 'isolation': 'logical'}

## 5.2.0.0.0 Subnet Strategy

- {'environment': 'Production', 'subnetType': 'private', 'cidrBlock': 'Customer-defined (e.g., 10.10.10.0/24)', 'availabilityZone': 'N/A (On-Premise)', 'routingTable': 'Routes to internal data sources and to a NAT gateway/proxy for outbound licensing calls.'}

## 5.3.0.0.0 Security Group Rules

### 5.3.1.0.0 Group Name

#### 5.3.1.1.0 Group Name

AppServer-SG

#### 5.3.1.2.0 Direction

inbound

#### 5.3.1.3.0 Protocol

tcp

#### 5.3.1.4.0 Port Range

443

#### 5.3.1.5.0 Source

Internal-User-Subnet-CIDR

#### 5.3.1.6.0 Purpose

Allow users to access the Control Panel and Report Viewer.

### 5.3.2.0.0 Group Name

#### 5.3.2.1.0 Group Name

AppServer-SG

#### 5.3.2.2.0 Direction

outbound

#### 5.3.2.3.0 Protocol

tcp

#### 5.3.2.4.0 Port Range

443

#### 5.3.2.5.0 Source

Licensing-Service-IP-Range

#### 5.3.2.6.0 Purpose

Allow activation and validation of the software license.

### 5.3.3.0.0 Group Name

#### 5.3.3.1.0 Group Name

AppServer-SG

#### 5.3.3.2.0 Direction

outbound

#### 5.3.3.3.0 Protocol

tcp

#### 5.3.3.4.0 Port Range

1433

#### 5.3.3.5.0 Source

Internal-SQL-Server-IP

#### 5.3.3.6.0 Purpose

Allow connection to a specific internal data source.

## 5.4.0.0.0 Connectivity Requirements

### 5.4.1.0.0 Source

#### 5.4.1.1.0 Source

Application Server

#### 5.4.1.2.0 Destination

Licensing Service

#### 5.4.1.3.0 Protocol

HTTPS

#### 5.4.1.4.0 Bandwidth

Low (<1 Mbps)

#### 5.4.1.5.0 Latency

Standard internet latency.

### 5.4.2.0.0 Source

#### 5.4.2.1.0 Source

Application Server

#### 5.4.2.2.0 Destination

Internal Data Sources

#### 5.4.2.3.0 Protocol

Varies (TCP 1433, 3306, 5432, 445)

#### 5.4.2.4.0 Bandwidth

High

#### 5.4.2.5.0 Latency

Low (<10ms)

## 5.5.0.0.0 Network Monitoring

- {'type': 'flow-logs', 'implementation': "Customer's existing network monitoring tools.", 'alerting': True, 'retention': 'As per customer policy.'}

## 5.6.0.0.0 Bandwidth Controls

*No items available*

## 5.7.0.0.0 Service Discovery

| Property | Value |
|----------|-------|
| Method | dns |
| Implementation | Internal DNS record (e.g., 'reports.customer.local... |
| Health Checks | ❌ |

## 5.8.0.0.0 Environment Communication

*No items available*

# 6.0.0.0.0 Data Management Strategy

## 6.1.0.0.0 Data Isolation

- {'environment': 'Production', 'isolationLevel': 'complete', 'method': "The entire system is self-contained on the customer's infrastructure, providing complete data isolation from the vendor and other customers.", 'justification': 'Core security and data privacy requirement.'}

## 6.2.0.0.0 Backup And Recovery

- {'environment': 'Production', 'backupFrequency': 'Daily', 'retentionPeriod': 'As per customer policy.', 'recoveryTimeObjective': '4 hours', 'recoveryPointObjective': '24 hours', 'testingSchedule': 'Quarterly'}

## 6.3.0.0.0 Data Masking Anonymization

- {'environment': 'Testing', 'dataType': 'All source data', 'maskingMethod': 'static', 'coverage': 'complete', 'compliance': ['HIPAA']}

## 6.4.0.0.0 Migration Processes

- {'sourceEnvironment': 'Staging', 'targetEnvironment': 'Production', 'migrationMethod': 'The MSI installer handles the application upgrade. Schema migrations for the SQLite database are handled automatically by EF Core Migrations on service startup.', 'validation': 'Post-upgrade, manually run key reports and verify system functionality.', 'rollbackPlan': 'Uninstall the new version, reinstall the old version, and restore the application data directory from a pre-upgrade backup.'}

## 6.5.0.0.0 Retention Policies

- {'environment': 'Production', 'dataType': 'Job Execution Logs and Generated Reports', 'retentionPeriod': 'Configurable within the application, as per customer policy.', 'archivalMethod': 'Manual or scripted export.', 'complianceRequirement': 'Customer-defined.'}

## 6.6.0.0.0 Data Classification

- {'classification': 'restricted', 'handlingRequirements': ['Must be encrypted at rest and in transit.', 'Access must be strictly controlled and audited.'], 'accessControls': ['RBAC'], 'environments': ['Production']}

## 6.7.0.0.0 Disaster Recovery

- {'environment': 'Production', 'drSite': "Customer's secondary datacenter or cloud region.", 'replicationMethod': 'snapshot', 'failoverTime': '< 4 hours (RTO)', 'testingFrequency': 'Annually'}

# 7.0.0.0.0 Monitoring And Observability

## 7.1.0.0.0 Monitoring Components

### 7.1.1.0.0 Component

#### 7.1.1.1.0 Component

infrastructure

#### 7.1.1.2.0 Tool

Windows Performance Monitor / Customer's existing tools (e.g., Nagios, Zabbix, Datadog)

#### 7.1.1.3.0 Implementation

Monitor CPU, Memory, Disk Space, and Network I/O of the host server.

#### 7.1.1.4.0 Environments

- Production
- Staging
- Testing

### 7.1.2.0.0 Component

#### 7.1.2.1.0 Component

logs

#### 7.1.2.2.0 Tool

Customer's existing centralized logging platform (e.g., Splunk, ELK, Graylog)

#### 7.1.2.3.0 Implementation

A log forwarder (e.g., Filebeat, Splunk Universal Forwarder) is installed on the host to collect structured JSON logs from the application's log directory.

#### 7.1.2.4.0 Environments

- Production
- Staging
- Testing
- Development

### 7.1.3.0.0 Component

#### 7.1.3.1.0 Component

alerting

#### 7.1.3.2.0 Tool

In-app Email Notifications + Customer's Monitoring Tools

#### 7.1.3.3.0 Implementation

The application's built-in health monitoring and job failure notifications are the primary alerting mechanism. Infrastructure alerts are handled by the customer's tools.

#### 7.1.3.4.0 Environments

- Production

## 7.2.0.0.0 Environment Specific Thresholds

### 7.2.1.0.0 Environment

#### 7.2.1.1.0 Environment

Production

#### 7.2.1.2.0 Metric

Host CPU Utilization

#### 7.2.1.3.0 Warning Threshold

> 80% for 15 minutes

#### 7.2.1.4.0 Critical Threshold

> 95% for 5 minutes

#### 7.2.1.5.0 Justification

Indicates sustained load that may require vertical scaling.

### 7.2.2.0.0 Environment

#### 7.2.2.1.0 Environment

Production

#### 7.2.2.2.0 Metric

Available Disk Space

#### 7.2.2.3.0 Warning Threshold

< 20%

#### 7.2.2.4.0 Critical Threshold

< 10%

#### 7.2.2.5.0 Justification

Prevents system failure due to insufficient space for logs or reports.

## 7.3.0.0.0 Metrics Collection

- {'category': 'application', 'metrics': ['Job Execution Status (Success, Failure)', 'Report Generation Duration'], 'collectionInterval': 'real-time', 'retention': 'Configurable in-app'}

## 7.4.0.0.0 Health Check Endpoints

- {'component': 'Reporting Service', 'endpoint': 'Application-internal check', 'checkType': 'liveness', 'timeout': '30s', 'frequency': '5 minutes'}

## 7.5.0.0.0 Logging Configuration

### 7.5.1.0.0 Environment

#### 7.5.1.1.0 Environment

Production

#### 7.5.1.2.0 Log Level

Information

#### 7.5.1.3.0 Destinations

- Rolling file (JSON format)

#### 7.5.1.4.0 Retention

30 days (configurable)

#### 7.5.1.5.0 Sampling

None

### 7.5.2.0.0 Environment

#### 7.5.2.1.0 Environment

Development

#### 7.5.2.2.0 Log Level

Debug

#### 7.5.2.3.0 Destinations

- Console
- Rolling file (JSON format)

#### 7.5.2.4.0 Retention

7 days

#### 7.5.2.5.0 Sampling

None

## 7.6.0.0.0 Escalation Policies

- {'environment': 'Production', 'severity': 'critical', 'escalationPath': ['Email to configured administrator list.', "Customer's internal on-call procedure."], 'timeouts': [], 'channels': ['email']}

## 7.7.0.0.0 Dashboard Configurations

- {'dashboardType': 'operational', 'audience': 'Administrator', 'refreshInterval': '30 seconds', 'metrics': ['In-app Job Monitoring Dashboard showing status of recent and running jobs.']}

# 8.0.0.0.0 Project Specific Environments

## 8.1.0.0.0 Environments

*No items available*

## 8.2.0.0.0 Configuration

*No data available*

## 8.3.0.0.0 Cross Environment Policies

*No items available*

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

Production Reference Architecture Documentation

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Medium

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Internal Staging Environment Build-out

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

*No items available*

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Automated Internal Environment Provisioning (Terraform)

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

*No items available*

### 9.3.4.0.0 Estimated Effort

High

### 9.3.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Customer misconfigures network security, exposing the application or violating data privacy rules.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Provide a comprehensive, clear, and prescriptive Installation and Administration Guide with explicit examples of firewall rules and network segmentation.

### 10.1.5.0.0 Contingency Plan

Support team assists customer in correcting the configuration. Application audit logs may help identify unauthorized access attempts.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Customer fails to back up the application data directory, leading to data loss.

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

medium

### 10.2.4.0.0 Mitigation

The Administration Guide must have a dedicated, prominent chapter on Backup and Recovery procedures. The Control Panel UI should display reminders if no recent backup has been triggered via the built-in function.

### 10.2.5.0.0 Contingency Plan

Data may be unrecoverable. Assist customer in rebuilding configuration from scratch.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Documentation

### 11.1.2.0.0 Recommendation

Create a 'Hardening Guide' for the customer's IT Support team, based on CIS Benchmarks for Windows Server, to ensure the host OS is secure.

### 11.1.3.0.0 Justification

Reduces the risk of OS-level vulnerabilities compromising the application, especially in environments handling sensitive data like PHI.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

This guide should cover service account permissions, GPO settings, firewall configuration, and logging/auditing setup.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Deployment

### 11.2.2.0.0 Recommendation

The MSI installer should perform pre-flight checks to validate prerequisites, such as .NET Runtime version, available disk space, and required OS permissions.

### 11.2.3.0.0 Justification

Improves the reliability of the installation process and provides immediate, actionable feedback to the installer, reducing support calls.

### 11.2.4.0.0 Priority

🟡 medium

### 11.2.5.0.0 Implementation Notes

These checks can be implemented as custom actions within the WiX Toolset configuration.

