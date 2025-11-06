# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- GitHub Actions
- .NET 8
- React 18 / Vite
- WiX Toolset
- xUnit
- Jest
- Docker (for build environment)

## 1.3 Architecture Patterns

- Modular Monolith
- Single Build Pipeline for Backend & Frontend

## 1.4 Data Sensitivity

- Source Code (Confidential)
- Build Secrets & Credentials (Restricted)
- Test Data (Internal)
- Build Artifacts (Internal)

## 1.5 Regulatory Considerations

- OWASP Top 10 (via SAST)
- Software Bill of Materials (SBOM)
- Dependency Vulnerability Management

## 1.6 System Criticality

business-critical

# 2.0 Data Classification And Protection Requirements

## 2.1 Sensitive Data Components

### 2.1.1 Data Type

#### 2.1.1.1 Data Type

Source Code

#### 2.1.1.2 Location

GitHub Repository

#### 2.1.1.3 Volume

Medium

#### 2.1.1.4 Sensitivity

confidential

#### 2.1.1.5 Regulatory Requirements

*No items available*

#### 2.1.1.6 Access Patterns

Controlled via branch protection rules and CODEOWNERS.

### 2.1.2.0 Data Type

#### 2.1.2.1 Data Type

Build Secrets

#### 2.1.2.2 Location

GitHub Actions Encrypted Secrets

#### 2.1.2.3 Volume

Low

#### 2.1.2.4 Sensitivity

high

#### 2.1.2.5 Regulatory Requirements

*No items available*

#### 2.1.2.6 Access Patterns

Injected into pipeline runtime, never logged.

## 2.2.0.0 Regulatory Compliance

- {'regulation': 'OWASP Top 10', 'applicableData': ['Source Code'], 'retentionRequirements': 'Scan results retained with build logs for 90 days.', 'encryptionMandatory': False, 'auditRequirements': ['All pipeline runs must be logged and auditable.'], 'breachNotificationTime': 'N/A'}

## 2.3.0.0 Data Sensitivity Levels

- {'level': 'Vulnerability Severity (Critical/High)', 'description': 'Severity level of findings from SAST and dependency scanners.', 'handlingRequirements': ['Must fail the build if Critical or High vulnerabilities are found.'], 'backupRequirements': 'Scan reports are stored as build artifacts.', 'recoveryPriority': 0}

## 2.4.0.0 Data Location Mapping

- {'dataCategory': 'Build Artifacts (MSI)', 'primaryLocation': 'GitHub Actions Artifacts', 'backupLocations': ['GitHub Releases (for tagged versions)'], 'replicationStatus': 'Manual Promotion', 'encryptionStatus': 'Encrypted in transit and at rest by GitHub.'}

## 2.5.0.0 Critical System Configurations

- {'component': 'CI/CD Pipeline Definition', 'configurationType': 'application', 'backupFrequency': 'On every commit to source control.', 'recoveryPriority': 'critical', 'dependencies': ['.github/workflows/main.yml']}

## 2.6.0.0 Recovery Prioritization

- {'dataCategory': 'Broken Main Branch Build', 'priority': 1, 'justification': 'A broken main branch blocks all development and releases.', 'dependencies': ['Source Code'], 'rtoRequirement': '< 1 hour to fix or revert.'}

## 2.7.0.0 Backup Verification Requirements

- {'dataType': 'MSI Installer Artifact', 'verificationMethod': 'code-signing', 'frequency': 'On every release build.', 'complianceRequirement': 'Ensures artifact integrity and authenticity.'}

# 3.0.0.0 Backup Strategy Design

## 3.1.0.0 Backup Types

### 3.1.1.0 Continuous Integration Build

#### 3.1.1.1 Type

🔹 Continuous Integration Build

#### 3.1.1.2 Applicable Data

- Source Code

#### 3.1.1.3 Frequency

On every push to a feature branch or PR.

#### 3.1.1.4 Retention

Build logs and artifacts retained for 30 days.

#### 3.1.1.5 Storage Location

GitHub Actions

#### 3.1.1.6 Justification

Provides rapid feedback to developers on code changes.

### 3.1.2.0 Release Candidate Build

#### 3.1.2.1 Type

🔹 Release Candidate Build

#### 3.1.2.2 Applicable Data

- Source Code

#### 3.1.2.3 Frequency

On every push or merge to the main branch.

#### 3.1.2.4 Retention

Build logs and artifacts retained for 90 days.

#### 3.1.2.5 Storage Location

GitHub Actions

#### 3.1.2.6 Justification

Creates a stable, testable artifact for QA and potential release.

### 3.1.3.0 Official Release Build

#### 3.1.3.1 Type

🔹 Official Release Build

#### 3.1.3.2 Applicable Data

- Source Code

#### 3.1.3.3 Frequency

On creation of a version tag (e.g., 'v1.2.3').

#### 3.1.3.4 Retention

Indefinite.

#### 3.1.3.5 Storage Location

GitHub Releases

#### 3.1.3.6 Justification

Creates a permanent, versioned, and signed official release artifact for distribution.

## 3.2.0.0 Backup Frequency

- {'dataCategory': 'Source Code Build', 'frequency': 'continuous', 'schedule': 'Triggered by git push events.', 'timezone': 'UTC', 'maintenanceWindow': 'N/A'}

## 3.3.0.0 Rotation And Retention

- {'backupType': 'Build Artifacts', 'rotationScheme': 'simple', 'retentionPeriod': '30 days (CI), 90 days (RC), Indefinite (Release)', 'archivalPolicy': 'Official releases are archived permanently in GitHub Releases.', 'deletionPolicy': 'Older CI/RC artifacts are automatically pruned by GitHub Actions retention policies.'}

## 3.4.0.0 Storage Requirements

- {'storageType': 'cloud', 'location': 'GitHub Actions Artifacts & GitHub Releases', 'redundancy': 'geo-replication', 'accessTime': 'immediate', 'costTier': 'hot'}

## 3.5.0.0 Backup Handling Procedures

- {'procedure': 'Uploading MSI to GitHub Release', 'chainOfCustody': 'Logged via GitHub Actions audit trail.', 'accessControls': ['Pipeline service principal only'], 'transportSecurity': 'HTTPS/TLS 1.2+', 'handlingPersonnel': ['Automated Process']}

## 3.6.0.0 Verification Processes

- {'verificationType': 'Automated Testing (Unit & Integration)', 'schedule': 'On every build.', 'automationLevel': 'automated', 'successCriteria': ['All tests must pass.', 'Code coverage must meet or exceed 80%.']}

## 3.7.0.0 Catalog And Indexing

### 3.7.1.0 Catalog System

GitHub Releases

### 3.7.2.0 Indexing Strategy

By semantic version tag.

### 3.7.3.0 Search Capabilities

- Search by tag name
- Browse release history

### 3.7.4.0 Metadata Tracking

- Release notes
- Commit SHA
- Build date

## 3.8.0.0 Database Specific Backups

*No items available*

# 4.0.0.0 Recovery Objectives Definition

## 4.1.0.0 Recovery Point Objectives

- {'dataCategory': 'Source Code', 'rpo': '0 seconds', 'justification': 'All code is stored in a distributed version control system (Git).', 'businessImpact': 'N/A', 'complianceRequirement': 'N/A'}

## 4.2.0.0 Recovery Time Objectives

- {'systemComponent': 'CI/CD Pipeline', 'rto': '< 4 hours', 'justification': 'In case of a catastrophic failure of the CI/CD service, the pipeline definition is in source control and can be re-established on a new runner or service.', 'criticality': 'business-critical', 'dependencies': ['Git repository', 'CI/CD provider (GitHub Actions)']}

## 4.3.0.0 Recovery Prioritization

### 4.3.1.0 Sequence

#### 4.3.1.1 Sequence

1

#### 4.3.1.2 Component

Previous successful release artifact (MSI)

#### 4.3.1.3 Reason

Immediate rollback mechanism is to redeploy the last known good version.

#### 4.3.1.4 Parallel Recovery

❌ No

#### 4.3.1.5 Prerequisites

- Access to GitHub Releases

### 4.3.2.0 Sequence

#### 4.3.2.1 Sequence

2

#### 4.3.2.2 Component

Source Code Revert

#### 4.3.2.3 Reason

If a new artifact is not available, revert the problematic commit and trigger a new release build.

#### 4.3.2.4 Parallel Recovery

❌ No

#### 4.3.2.5 Prerequisites

- Git access
- Functional CI/CD pipeline

## 4.4.0.0 Recovery Verification Procedures

- {'component': 'New Release Build', 'verificationSteps': ['Verify all pipeline stages passed.', 'Manually download the MSI and perform a smoke test installation.'], 'acceptanceCriteria': ['Application installs and starts successfully.'], 'testingRequired': True}

## 4.5.0.0 Recovery Testing Schedule

*No items available*

## 4.6.0.0 Recovery Team Roles

*No items available*

## 4.7.0.0 Recovery Runbooks

- {'scenario': 'Deployment of a faulty MSI', 'procedures': ['Communicate issue to IT Support.', 'IT Support uninstalls the faulty version from affected machines.', 'IT Support restores host machines from pre-cutover backup as per fallback plan (SRS 7.4).', 'DevOps team identifies the last known good release from GitHub Releases and provides it to IT Support for redeployment.'], 'decisionPoints': ['Is the issue widespread or isolated?', 'Is a hotfix build feasible or is rollback faster?'], 'escalationCriteria': ['If rollback fails or no good version is available.'], 'updateFrequency': 'Annually'}

## 4.8.0.0 Alternate Site Capabilities

*No items available*

# 5.0.0.0 Encryption And Security Design

## 5.1.0.0 Backup Encryption Requirements

- {'dataType': 'Build Artifact (MSI)', 'encryptionAlgorithm': 'RSA-4096', 'keyLength': '4096', 'complianceStandard': 'Authenticode', 'mandatoryFields': []}

## 5.2.0.0 Key Management Procedures

### 5.2.1.0 Key Type

#### 5.2.1.1 Key Type

Code Signing Certificate

#### 5.2.1.2 Key Rotation Frequency

Annually

#### 5.2.1.3 Key Escrow Policy

Managed by a secure secret store (e.g., Azure Key Vault, HashiCorp Vault).

#### 5.2.1.4 Key Recovery Procedure

As per secret store's documented recovery process.

#### 5.2.1.5 Access Controls

- Restricted to CI/CD service principal during release builds.

### 5.2.2.0 Key Type

#### 5.2.2.1 Key Type

Build Secrets

#### 5.2.2.2 Key Rotation Frequency

As needed, minimum annually.

#### 5.2.2.3 Key Escrow Policy

N/A

#### 5.2.2.4 Key Recovery Procedure

Secrets must be regenerated if lost.

#### 5.2.2.5 Access Controls

- Managed via GitHub Actions environment secrets.

## 5.3.0.0 Backup Access Controls

- {'accessLevel': 'read-only', 'authentication': 'token', 'authorization': 'rbac', 'auditLogging': True}

## 5.4.0.0 Secure Transport

- {'transportMethod': 'https', 'encryptionInTransit': True, 'certificateValidation': True, 'integrityChecking': True}

## 5.5.0.0 Backup Audit Logging

- {'eventType': 'access', 'logDetail': 'full', 'retentionPeriod': '2 years', 'logProtection': 'Immutable'}

## 5.6.0.0 Chain Of Custody Procedures

- {'handoffPoint': 'Build to Release', 'documentation': 'GitHub Actions logs and release notes.', 'verification': 'Commit SHA matching.', 'responsibility': 'Automated CI/CD process.'}

## 5.7.0.0 Secure Erasure Procedures

*No items available*

# 6.0.0.0 Disaster Recovery Planning

## 6.1.0.0 Disaster Scenarios

### 6.1.1.0 Scenario

#### 6.1.1.1 Scenario

Critical security vulnerability discovered in a dependency post-release.

#### 6.1.1.2 Impact

Requires immediate hotfix release.

#### 6.1.1.3 Likelihood

medium

#### 6.1.1.4 Response Strategy

1. Update dependency. 2. Trigger emergency hotfix release pipeline. 3. Distribute new MSI.

### 6.1.2.0 Scenario

#### 6.1.2.1 Scenario

Build agent/runner infrastructure is unavailable.

#### 6.1.2.2 Impact

No new builds or releases can be created.

#### 6.1.2.3 Likelihood

low

#### 6.1.2.4 Response Strategy

Use alternative runner pool or wait for infrastructure restoration. Pipeline definition is safe in Git.

## 6.2.0.0 System Recovery Procedures

- {'failureType': 'Failed deployment validation', 'recoveryApproach': 'Do not release artifact. Revert offending commit. Trigger new build.', 'estimatedTime': '< 2 hours', 'resourceRequirements': ['Developer time']}

## 6.3.0.0 Alternate Processing Capabilities

*No items available*

## 6.4.0.0 Communication Procedures

- {'audience': 'internal', 'communicationMethod': 'slack', 'messageTemplates': ['CI/CD Pipeline Failure Alert: Build for branch {branch} failed at stage {stage}. Link: {url}'], 'escalationPath': ['Developer -> Lead Developer -> DevOps Team']}

## 6.5.0.0 Recovery Sequence And Dependencies

*No items available*

## 6.6.0.0 Data Synchronization

*No items available*

## 6.7.0.0 Disaster Declaration Criteria

*No items available*

## 6.8.0.0 Post Recovery Validation

*No items available*

# 7.0.0.0 Testing And Validation Strategy

## 7.1.0.0 Recovery Testing Procedures

### 7.1.1.0 Test Type

#### 7.1.1.1 Test Type

Unit Test Execution

#### 7.1.1.2 Frequency

On every build.

#### 7.1.1.3 Scope

- Backend C# code
- Frontend TypeScript code

#### 7.1.1.4 Environment

isolated

#### 7.1.1.5 Impact Minimization

*No items available*

### 7.1.2.0 Test Type

#### 7.1.2.1 Test Type

Integration Test Execution

#### 7.1.2.2 Frequency

On every build.

#### 7.1.2.3 Scope

- Full data pipeline validation for built-in connectors

#### 7.1.2.4 Environment

isolated

#### 7.1.2.5 Impact Minimization

*No items available*

## 7.2.0.0 Backup Verification Schedule

- {'verificationType': 'Code Signing', 'frequency': 'On every release build.', 'automationLevel': 'automated', 'reportingRequired': True}

## 7.3.0.0 Recovery Testing Success Criteria

- {'testType': 'Quality Gate', 'criteria': ['All unit and integration tests passed.', 'Code coverage for both frontend and backend is >= 80%.', 'No Critical or High severity issues found by security scanners.'], 'acceptableTolerances': [], 'escalationTriggers': ['Any failure of these criteria will fail the build.']}

## 7.4.0.0 Tabletop Exercises

- {'scenario': 'Manual UAT Approval', 'frequency': 'On every release candidate build.', 'participants': ['QA Team', 'Product Owner'], 'objectives': ['Verify the release candidate MSI meets business requirements before creating an official release.'], 'improvementTracking': True}

## 7.5.0.0 Backup Integrity Validation

*No items available*

## 7.6.0.0 Testing Documentation Requirements

- {'documentType': 'test-results', 'template': 'JUnit XML, Cobertura XML', 'retention': 'Stored with build artifacts.', 'distribution': ['Linked in build summary']}

## 7.7.0.0 Continuous Improvement Process

*No items available*

# 8.0.0.0 Project Specific Backup Strategy

## 8.1.0.0 Strategy

### 8.1.1.0 Id

main-ci-cd-pipeline

### 8.1.2.0 Type

🔹 Hybrid

### 8.1.3.0 Schedule

On git push events to branches, PRs, and version tags.

### 8.1.4.0 Retention Period Days

90

### 8.1.5.0 Backup Locations

- GitHub Actions Artifacts
- GitHub Releases

### 8.1.6.0 Configuration

| Property | Value |
|----------|-------|
| Backup Window | N/A |
| Compression | enabled |
| Verification | automated |
| Throttling | N/A |
| Priority | high |
| Max Concurrent Transfers | N/A |
| Parallelism | 3 |
| Checksum Validation | ❌ |

### 8.1.7.0 Encryption

#### 8.1.7.1 Enabled

✅ Yes

#### 8.1.7.2 Algorithm

RSA-4096

#### 8.1.7.3 Key Management Service

GitHub Secrets

#### 8.1.7.4 Encrypted Fields

*No items available*

#### 8.1.7.5 Configuration

| Property | Value |
|----------|-------|
| Key Rotation | Annually |
| Access Policy | strict |
| Key Identifier | CODE_SIGNING_CERT |
| Multi Factor | disabled |

#### 8.1.7.6 Transit Encryption

✅ Yes

#### 8.1.7.7 At Rest Encryption

✅ Yes

## 8.2.0.0 Component Specific Strategies

### 8.2.1.0 Component

#### 8.2.1.1 Component

Backend (.NET)

#### 8.2.1.2 Backup Type

Build & Test

#### 8.2.1.3 Frequency

On every build.

#### 8.2.1.4 Retention

N/A

#### 8.2.1.5 Special Requirements

- Run xUnit tests.
- Generate Cobertura code coverage report.
- Publish artifacts for packaging.

### 8.2.2.0 Component

#### 8.2.2.1 Component

Frontend (React)

#### 8.2.2.2 Backup Type

Build & Test

#### 8.2.2.3 Frequency

On every build.

#### 8.2.2.4 Retention

N/A

#### 8.2.2.5 Special Requirements

- Run Jest tests.
- Generate Cobertura code coverage report.
- Build static assets with Vite.

### 8.2.3.0 Component

#### 8.2.3.1 Component

Security Scans

#### 8.2.3.2 Backup Type

Analysis

#### 8.2.3.3 Frequency

On every build.

#### 8.2.3.4 Retention

N/A

#### 8.2.3.5 Special Requirements

- Run SAST scan on code.
- Run dependency vulnerability scan.

## 8.3.0.0 Configuration

### 8.3.1.0 Rpo

N/A

### 8.3.2.0 Rto

20 minutes (for full build and test cycle)

### 8.3.3.0 Backup Verification

Automated via quality gates.

### 8.3.4.0 Disaster Recovery Site

N/A

### 8.3.5.0 Compliance Standard

OWASP

### 8.3.6.0 Audit Logging

enabled

### 8.3.7.0 Test Restore Frequency

N/A

### 8.3.8.0 Notification Channel

slack

### 8.3.9.0 Alert Thresholds

1 build failure

### 8.3.10.0 Retry Policy

1 retry on transient network errors

### 8.3.11.0 Backup Admin

DevOps Team

### 8.3.12.0 Escalation Path

On-call developer

### 8.3.13.0 Reporting Schedule

On every build completion.

### 8.3.14.0 Cost Optimization

disabled

### 8.3.15.0 Maintenance Window

N/A

### 8.3.16.0 Environment Specific

#### 8.3.16.1 Production

| Property | Value |
|----------|-------|
| Rpo | N/A |
| Rto | N/A |
| Testing Frequency | N/A |

#### 8.3.16.2 Staging

| Property | Value |
|----------|-------|
| Rpo | N/A |
| Rto | N/A |
| Testing Frequency | N/A |

#### 8.3.16.3 Development

| Property | Value |
|----------|-------|
| Rpo | N/A |
| Rto | N/A |
| Testing Frequency | N/A |

# 9.0.0.0 Implementation Priority

## 9.1.0.0 Component

### 9.1.1.0 Component

CI Pipeline (Build & Test Automation)

### 9.1.2.0 Priority

🔴 high

### 9.1.3.0 Dependencies

*No items available*

### 9.1.4.0 Estimated Effort

Medium

### 9.1.5.0 Risk Level

low

## 9.2.0.0 Component

### 9.2.1.0 Component

Quality Gates (Coverage & Security)

### 9.2.2.0 Priority

🔴 high

### 9.2.3.0 Dependencies

- CI Pipeline

### 9.2.4.0 Estimated Effort

Medium

### 9.2.5.0 Risk Level

medium

## 9.3.0.0 Component

### 9.3.1.0 Component

CD Pipeline (Release & Signing)

### 9.3.2.0 Priority

🟡 medium

### 9.3.3.0 Dependencies

- CI Pipeline

### 9.3.4.0 Estimated Effort

Low

### 9.3.5.0 Risk Level

low

# 10.0.0.0 Risk Assessment

## 10.1.0.0 Risk

### 10.1.1.0 Risk

Flaky tests causing intermittent build failures.

### 10.1.2.0 Impact

medium

### 10.1.3.0 Probability

high

### 10.1.4.0 Mitigation

Implement a strict policy for test reliability. Quarantine flaky tests and require developers to fix them.

### 10.1.5.0 Contingency Plan

Allow manual override of the quality gate by a lead developer with documented justification.

## 10.2.0.0 Risk

### 10.2.1.0 Risk

Build secrets are compromised.

### 10.2.2.0 Impact

high

### 10.2.3.0 Probability

low

### 10.2.4.0 Mitigation

Use GitHub's encrypted secrets, limit access, and rotate secrets regularly. Never print secrets to logs.

### 10.2.5.0 Contingency Plan

Immediately revoke the compromised secret and issue a new one. Audit for malicious use.

# 11.0.0.0 Recommendations

## 11.1.0.0 Category

### 11.1.1.0 Category

🔹 Performance

### 11.1.2.0 Recommendation

Use dependency caching for both .NET (NuGet) and Node.js (npm) packages.

### 11.1.3.0 Justification

Significantly reduces build times by avoiding repeated downloads of unchanged dependencies, leading to faster feedback for developers.

### 11.1.4.0 Priority

🔴 high

### 11.1.5.0 Implementation Notes

Utilize the `actions/cache` action in GitHub Actions.

## 11.2.0.0 Category

### 11.2.1.0 Category

🔹 Security

### 11.2.2.0 Recommendation

Implement artifact signing for all official release builds.

### 11.2.3.0 Justification

Provides integrity and authenticity for the distributed MSI installer, assuring users that the software has not been tampered with and originates from a trusted source.

### 11.2.4.0 Priority

🔴 high

### 11.2.5.0 Implementation Notes

Store the code signing certificate securely and inject it into the pipeline only during tagged release builds.

## 11.3.0.0 Category

### 11.3.1.0 Category

🔹 Workflow

### 11.3.2.0 Recommendation

Use reusable workflows in GitHub Actions to define jobs like 'build-and-test-dotnet' and 'build-and-test-react'.

### 11.3.3.0 Justification

Reduces duplication in the pipeline definition file, improves maintainability, and allows for consistent execution of common tasks across different workflows (e.g., CI vs. Release).

### 11.3.4.0 Priority

🟡 medium

### 11.3.5.0 Implementation Notes

Create separate YAML files in the `.github/workflows` directory for each reusable workflow.

