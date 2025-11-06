# 1 Pipelines

## 1.1 Pull Request Validation

### 1.1.1 Id

pipeline-pr-validation-01

### 1.1.2 Name

Pull Request Validation

### 1.1.3 Description

Runs on every pull request to validate code quality, test coverage, and security before merging. Fulfills requirements 3.4 and 8.4.

### 1.1.4 Stages

#### 1.1.4.1 Build

##### 1.1.4.1.1 Name

Build

##### 1.1.4.1.2 Steps

- dotnet restore
- npm install --prefix Frontend
- dotnet build --no-restore

##### 1.1.4.1.3 Environment

###### 1.1.4.1.3.1 Dotnet Version

8.0

###### 1.1.4.1.3.2 Node Version

20

##### 1.1.4.1.4.0 Quality Gates

*No items available*

#### 1.1.4.2.0.0 Unit Testing & Coverage

##### 1.1.4.2.1.0 Name

Unit Testing & Coverage

##### 1.1.4.2.2.0 Steps

- dotnet test --collect:"XPlat Code Coverage"
- npm test --prefix Frontend -- --coverage
- report-coverage --backend-coverage results.xml --frontend-coverage Frontend/coverage/cobertura-coverage.xml

##### 1.1.4.2.3.0 Environment

*No data available*

##### 1.1.4.2.4.0 Quality Gates

###### 1.1.4.2.4.1 Test Pass Rate

####### 1.1.4.2.4.1.1 Name

Test Pass Rate

####### 1.1.4.2.4.1.2 Criteria

- all tests pass

####### 1.1.4.2.4.1.3 Blocking

✅ Yes

###### 1.1.4.2.4.2.0 Code Coverage Check

####### 1.1.4.2.4.2.1 Name

Code Coverage Check

####### 1.1.4.2.4.2.2 Criteria

- backend coverage >= 80%
- frontend coverage >= 80%

####### 1.1.4.2.4.2.3 Blocking

✅ Yes

#### 1.1.4.3.0.0.0 Dependency Security Scan

##### 1.1.4.3.1.0.0 Name

Dependency Security Scan

##### 1.1.4.3.2.0.0 Steps

- dotnet list package --vulnerable
- npm audit --prefix Frontend --audit-level=critical

##### 1.1.4.3.3.0.0 Environment

*No data available*

##### 1.1.4.3.4.0.0 Quality Gates

- {'name': 'Vulnerability Check', 'criteria': ['zero critical CVEs found in dependencies'], 'blocking': True}

## 1.2.0.0.0.0.0 Release MSI Package

### 1.2.1.0.0.0.0 Id

pipeline-release-msi-02

### 1.2.2.0.0.0.0 Name

Release MSI Package

### 1.2.3.0.0.0.0 Description

Builds, tests, and packages the application into a versioned MSI installer upon merge to the main branch. Fulfills requirements 3.2, 3.4, and 6.7.

### 1.2.4.0.0.0.0 Stages

#### 1.2.4.1.0.0.0 Build & Test

##### 1.2.4.1.1.0.0 Name

Build & Test

##### 1.2.4.1.2.0.0 Steps

- dotnet restore
- npm install --prefix Frontend
- dotnet build -c Release
- dotnet test -c Release --collect:"XPlat Code Coverage"
- npm test --prefix Frontend -- --coverage

##### 1.2.4.1.3.0.0 Environment

###### 1.2.4.1.3.1.0 Dotnet Version

8.0

###### 1.2.4.1.3.2.0 Node Version

20

##### 1.2.4.1.4.0.0 Quality Gates

- {'name': 'Release Quality Checks', 'criteria': ['all tests pass', 'backend coverage >= 80%', 'frontend coverage >= 80%'], 'blocking': True}

#### 1.2.4.2.0.0.0 Dependency Security Scan

##### 1.2.4.2.1.0.0 Name

Dependency Security Scan

##### 1.2.4.2.2.0.0 Steps

- dotnet list package --vulnerable --include-transitive
- npm audit --prefix Frontend --audit-level=critical

##### 1.2.4.2.3.0.0 Environment

*No data available*

##### 1.2.4.2.4.0.0 Quality Gates

- {'name': 'Vulnerability Check', 'criteria': ['zero critical CVEs found in dependencies'], 'blocking': True}

#### 1.2.4.3.0.0.0 Package Installer

##### 1.2.4.3.1.0.0 Name

Package Installer

##### 1.2.4.3.2.0.0 Steps

- dotnet publish -c Release -o ./publish
- wix build Installer/Product.wxs -o ReportingSystem-v${VERSION}.msi

##### 1.2.4.3.3.0.0 Environment

###### 1.2.4.3.3.1.0 Wix Toolset Version

4.0

##### 1.2.4.3.4.0.0 Quality Gates

*No items available*

#### 1.2.4.4.0.0.0 Publish Artifacts

##### 1.2.4.4.1.0.0 Name

Publish Artifacts

##### 1.2.4.4.2.0.0 Steps

- publish-artifact --name ReportingSystem-v${VERSION}.msi --path ReportingSystem-v${VERSION}.msi
- publish-artifact --name openapi-spec.json --path ./publish/swagger/v1/swagger.json

##### 1.2.4.4.3.0.0 Environment

*No data available*

##### 1.2.4.4.4.0.0 Quality Gates

*No items available*

# 2.0.0.0.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Artifact Repository | GitHub Actions Artifacts |
| Default Branch | main |
| Retention Policy | 90d |
| Notification Channel | email on failure |

