### **Software Requirements Specification**

#### **1. Introduction**

**1.1. Project Scope**
The system's scope is defined by the following functional capabilities and explicit limitations.

**1.1.1. In-Scope Capabilities**
*   **Deployment:** The system shall provide a locally deployable Windows service for automated report generation.
*   **Licensing:** The system shall provide a cloud-based activation key mechanism for licensing.
*   **Data Ingestion:** The system shall provide data ingestion from databases (via JDBC/ODBC), local/network file systems, and OPC UA servers.
*   **Extensibility:** The system shall provide an extensible plug-in architecture for custom data connectors, with provided examples for FHIR and HL7.
*   **Data Transformation:** The system shall provide a data transformation engine using JSON-based configurations and user-defined JavaScript scripts.
*   **Report Generation:** The system shall provide report generation using Handlebars templates into human-readable formats (HTML, PDF). It shall also support direct data export to machine-readable formats (JSON, CSV, TXT).
*   **Report Delivery:** The system shall provide report delivery to cloud storage (Amazon S3, Azure Blob), email (SMTP), local/network storage, and FTP/SFTP. It shall also support direct data delivery via a configurable, secure RESTful API call.
*   **Management Interface:** The system shall provide a web-based Control Panel for system configuration, user management, and job monitoring.
*   **Report Access:** The system shall provide a web-based Report Viewer for browsing and accessing generated reports stored by the system.
*   **On-Demand API:** The system shall provide a secure RESTful API for on-demand report generation and system management.

**1.1.2. Out-of-Scope Limitations**
*   The system shall not provide direct integration with Business Intelligence (BI) tools such as Power BI or Tableau.
*   The system shall not provide a native mobile application for report viewing.
*   The system shall not provide advanced analytics or machine learning capabilities.
*   The system shall not provide support for operating systems other than Windows.
*   The system shall not provide support for report output formats other than HTML, PDF, JSON, CSV, and TXT.
*   The cloud-based licensing service is a dependency and is not part of the deliverable system.
*   The system shall not provide automated data migration tools for migrating configurations from other reporting systems.

**1.2. Product Perspective**
*   The system shall be a standalone, self-hosted Windows service designed for on-premise deployment.
*   The system shall follow a Hybrid, Modular Monolith architecture.
*   The system's core processing engine shall operate entirely on the customer's local network to ensure data privacy.
*   The system shall make a minimal, secure connection to a cloud service exclusively for license validation.

#### **2. Overall Description**

**2.1. Product Features**
The system shall provide the following features, prioritized as indicated:
*   System Licensing & Activation (Priority: High)
*   Data Ingestion Framework (Priority: High)
*   Data Transformation Engine (Priority: High)
*   Report Configuration & Scheduling (Priority: High)
*   Report Generation & Delivery (Priority: High)
*   System Administration via a Control Panel (Priority: High)
*   User & Access Management (Priority: High)
*   Configuration Versioning & Rollback (Priority: Medium)
*   Report Viewing & Access (Priority: Medium)
*   Job Monitoring & Management (Priority: Medium)
*   Secure External API (Priority: Medium)

**2.2. User Classes and Characteristics**
*   **Administrator:** This user class shall have full Create, Read, Update, and Delete (CRUD) access to all system configurations via the Control Panel and API.
*   **End-User (Viewer):** This user class shall have read-only access to the Report Viewer to browse and download generated reports, subject to role-based access controls.
*   **System Integrator:** This user class interacts with the system by developing custom connector plug-ins against a provided Plug-in Development Kit (PDK).
*   **IT Support:** This user class has filesystem and OS-level access to the host machine for installation, maintenance, and backup procedures.

**2.3. Operating Environment**
*   The system shall operate on Windows Server 2019/2022 or Windows 10/11 (64-bit).
*   The system shall be deployed as a local installation via a Windows Installer (MSI) package.
*   The system installer shall include the .NET 8 Runtime.
*   The system shall have minimum hardware requirements of a 2-core CPU, 4 GB RAM, and 20 GB free disk space.
*   The system shall have recommended hardware requirements of a 4+ core CPU, 16 GB RAM, and 100 GB free disk space for large datasets.
*   The system shall require database drivers (e.g., ODBC, JDBC) to be installed on the host machine for specific database connectors.
*   The system shall require outbound internet access on port 443 for cloud-based activation key validation.
*   The system shall require network access from the host machine to all configured data sources and delivery destinations.
*   The Control Panel and Report Viewer shall be accessible via the latest stable versions of Google Chrome, Mozilla Firefox, or Microsoft Edge.

**2.4. Assumptions and Dependencies**
*   The system shall assume users have stable internet access for initial activation and periodic validation.
*   The system shall assume data sources provide consistent and well-structured data.
*   The system shall assume administrators are trained to configure connectors and templates.
*   The system shall depend on the availability of the cloud-based licensing server for activation.
*   The system shall depend on the availability of third-party data sources and delivery endpoints.
*   The system shall depend on the customer's IT infrastructure (network, firewalls) being configured to allow the required connectivity.

#### **3. System Architecture and Technology**

**3.1. High-Level Architecture**
*   The system shall employ a Hybrid, Modular Monolith pattern.
*   The system shall be deployed as a single, self-contained Windows Service.
*   The Windows Service shall host an embedded ASP.NET Core web server to serve the Control Panel, Report Viewer, and RESTful API.

**3.2. Technology Stack**
*   **Backend:**
    *   **Language & Framework:** C# on .NET 8 with ASP.NET Core 8 for web and API hosting.
    *   **Job Scheduling:** Quartz.NET 3.x for managing scheduled report generation.
    *   **Data Transformation:** Jint for executing user-defined JavaScript transformation scripts.
    *   **Resiliency:** Polly for implementing transient-fault handling and resiliency patterns (retry, circuit breaker) for external calls.
    *   **Logging:** Serilog for structured, configurable logging to rolling files in JSON format.
*   **Database & Data Access:**
    *   **Configuration Storage:** SQLite for local, embedded storage of system configuration, users, and job metadata.
    *   **Data Access Layer:** Entity Framework Core 8 (EF Core) shall be used with the `Microsoft.Data.Sqlite` provider for simplified data access, schema management, and migrations.
*   **Frontend:**
    *   **Framework:** React 18 with TypeScript for building a strongly-typed, component-based user interface.
    *   **Build Tool:** Vite for a fast development server and optimized production builds.
    *   **Styling:** MUI v5 shall be used to ensure a consistent, accessible (WCAG 2.1 AA), and responsive UI.
    *   **State Management:** Zustand shall be used for lightweight, simple global state management.
*   **Report Generation & Templating:**
    *   **PDF Generation:** Puppeteer Sharp for high-fidelity HTML to PDF rendering via an embedded, sandboxed headless Chromium instance.
    *   **HTML Templating:** Handlebars.Net shall be used on the backend for its performance and native .NET integration to process data into HTML templates.
*   **Installer:**
    *   **Package Format:** WiX Toolset for creating a standard Windows Installer (MSI) package.

**3.2.1. Core Data Entities**
The system's configuration database shall model the following core entities and their relationships:
*   **User:** Stores user account details, credentials, and role assignments.
*   **Role:** Defines a set of permissions (Administrator, Viewer).
*   **ConnectorConfiguration:** Stores the definition for a data source, including its type, connection string, credentials, and other parameters. Each configuration shall have a version history.
*   **TransformationScript:** Stores a user-defined JavaScript for data manipulation. Each script shall have a version history.
*   **ReportConfiguration:** Defines a complete report job, linking a Connector, an optional Transformation, an output format, a template (if applicable), delivery destinations, and a schedule. Each configuration shall have a version history.
*   **JobExecutionLog:** Records the history of every report generation attempt, including its status, start/end times, execution logs, and a link to the generated artifact.
*   **AuditLog:** Stores a tamper-evident record of security-sensitive actions performed within the system.

**3.3. Security and Authentication**
*   **User Management:** ASP.NET Core Identity shall be used for managing user accounts, password hashing, and role-based access control (RBAC).
*   **API Authentication:** The RESTful API shall be secured using JWT (JSON Web Tokens). The server will issue tokens with a configurable expiration (default: 60 minutes) upon successful login. Clients must present a valid token in the `Authorization: Bearer <token>` header for subsequent requests. The system shall support a token refresh mechanism.
*   **Secret Management:** Sensitive configuration data (delivery endpoint credentials, API keys) shall be managed using the .NET Configuration Provider model, integrating with the .NET Secret Manager for local development and the Windows Certificate Store for production environments to avoid storing secrets in plaintext configuration files.
*   **Data Encryption:** The entire SQLite database file containing system configuration and user data shall be encrypted at rest using platform-specific mechanisms like DPAPI to protect against unauthorized offline access.

**3.4. Development, Testing, and Operations**
*   **API Documentation:** Swashbuckle.AspNetCore shall be used to automatically generate an OpenAPI 3.0 specification from the API controllers, providing interactive API documentation.
*   **Backend Testing:** The backend codebase shall be tested using xUnit as the test runner and Moq for creating mock objects to achieve a minimum of 80% unit test coverage.
*   **Frontend Testing:** The frontend codebase shall be tested using Jest as the test runner and React Testing Library for component-level testing to achieve a minimum of 80% unit test coverage.
*   **Integration and End-to-End Testing:** A dedicated suite of integration tests shall validate the entire data pipeline, from data ingestion through transformation, generation, and delivery for all built-in connectors and output formats.
*   **CI/CD:** A continuous integration and delivery pipeline shall be established using GitHub Actions to automate building, testing, packaging of the application into its MSI installer, and running security vulnerability scans on third-party dependencies.

**3.5. Design and Implementation Constraints**
*   The application shall run as a self-contained Windows service.
*   The activation key shall be validated via a secure cloud API.
*   All data processing and report generation shall occur locally.
*   Raw or intermediate data from customer sources shall never be transmitted outside the local network environment.
*   The embedded Chromium instance for PDF generation shall be configured to block all outbound network requests to ensure data privacy.
*   Custom connectors shall be implemented as .NET DLLs following a defined plug-in specification.
*   The system shall use a local, embedded SQLite database for configuration and user storage.

#### **4. Functional Requirements**

**4.1. System Activation and Licensing**
*   An administrator shall be able to activate the system using a cloud-issued activation key via the Control Panel.
*   An administrator shall be able to manually trigger a re-validation of the current license key.
*   Upon successful validation of a key, the system shall transition to an 'Active' state and unlock all features.
*   The system shall display an error message if an invalid or expired key is entered.
*   The system shall periodically re-validate the active key with the cloud licensing service. The validation interval shall be 30 days.
*   If no valid activation key is present, the system shall operate in a functionally limited Trial Mode.
    *   In Trial Mode, all generated reports shall contain a 'Trial Version' watermark.
    *   In Trial Mode, an administrator shall be able to configure a maximum of 3 active report schedules.
    *   In Trial Mode, an administrator shall be able to configure a maximum of 3 active connectors in total, regardless of type.
    *   In Trial Mode, custom connectors shall be disabled.
    *   The Control Panel UI shall display a persistent, visible indicator that the system is operating in Trial Mode.
*   If a periodic license validation fails, the system shall enter a Grace Period of 7 days.
    *   The system shall remain fully functional during the Grace Period.
    *   The Control Panel shall display a prominent notification indicating the system is in a Grace Period and the number of days remaining.
*   If the key is successfully validated before the Grace Period ends, the system shall return to the 'Active' state.
*   If the Grace Period expires without a successful validation, the system shall revert to Trial Mode.

**4.2. User Management and Access Control**
*   Administrators shall be able to perform full CRUD operations on user accounts and assign them to roles (Administrator, Viewer).
*   The system shall support configurable two-factor authentication (2FA) via TOTP authenticator apps.
*   The system shall enforce a configurable password policy. Default settings shall be: minimum 12 characters, 1 uppercase, 1 lowercase, 1 number, 1 special character, expiration after 90 days, and prevention of reuse of the last 5 passwords.
*   Administrators shall be able to initiate a password reset for a user, generating a temporary, one-time-use password. The user shall be forced to set a new password upon their next login.
*   A user's account shall be locked after 5 consecutive failed login attempts. An Administrator shall be able to manually unlock a locked account.
*   User sessions in the Control Panel and Report Viewer shall time out after 15 minutes of inactivity, after which the user shall be logged out. This duration shall be configurable.
*   The primary administrator account created during installation shall not be deletable.

**4.3. Data Input Connectors**
*   **Built-in Connectors:** The system shall provide built-in connectors for:
    *   Relational Databases: SQL Server, MySQL, and PostgreSQL. The system will use standard .NET data providers which may depend on ODBC/JDBC drivers installed on the host machine.
    *   File Systems: Reading and parsing CSV, JSON, fixed-width text, and XML files from local or network (UNC) paths.
    *   Industrial Data: A dedicated connector for OPC UA servers.
*   **Data Standardization:** All data ingested from any connector shall be converted into a standardized JSON format for internal processing.
*   **Custom Connector Architecture:**
    *   The system shall support a plug-in architecture for custom-developed connectors.
    *   The system shall dynamically discover and load custom connector .NET DLLs from a designated plug-in directory at runtime.
    *   Custom connectors must implement a defined `IConnector` interface, which shall expose methods for `GetConfigurationSchema()`, `ValidateConfiguration(configJson)`, `TestConnection(configJson)`, and `FetchData(configJson)`.
*   **Configuration and Testing:**
    *   The Control Panel shall dynamically render configuration UI elements for discovered connectors based on the schema returned by `GetConfigurationSchema()`.
    *   The connector configuration UI shall provide a 'Test Connection' button that validates network connectivity, credentials, and required permissions.
    *   A connector configuration shall not be permitted to be saved unless the 'Test Connection' has been executed successfully at least once within the current session. The UI must display a clear success or failure message with diagnostic information.
*   **Data Quality and Error Handling:**
    *   The system shall provide a configurable error handling strategy for data ingestion.
    *   For file-based sources, administrators shall be able to configure the job to either 'Fail on First Error' or 'Skip Erroneous Rows and Continue'.
    *   When skipping rows, the system shall log the row number and a description of the error (e.g., incorrect column count, data type mismatch) in the job execution log.

**4.4. Data Transformation**
*   The system shall provide an engine to manipulate intermediate JSON data using JavaScript, executed securely via the Jint library.
*   The JavaScript execution context shall provide a library of common helper functions for tasks such as date formatting, string manipulation, and mathematical calculations.
*   For reports utilizing a transformation script, the system shall buffer the entire dataset into memory as a JSON object. The Control Panel UI shall display a performance warning to the administrator when a transformation script is added to a report configuration, advising that this may increase memory usage and processing time for large datasets.
*   The Control Panel shall provide a UI for writing, managing, and validating transformation scripts for syntax errors.
*   Administrators shall be able to preview the output of a transformation script using either user-provided sample JSON data or live data from a selected connector.
*   The preview function shall have a 15-second timeout to prevent long-running scripts from freezing the UI.

**4.5. Report Configuration**
*   The Control Panel shall provide a guided, multi-step interface for creating and editing report configurations.
*   The configuration process shall include:
    1.  Defining basic report metadata (name, description).
    2.  Selecting a configured data connector.
    3.  Optionally selecting a data transformation script.
    4.  Configuring the output, including a mandatory 'Output Format' (`HTML`, `PDF`, `JSON`, `CSV`, `TXT`).
    5.  Selecting a Handlebars template (required only for `HTML` or `PDF` formats).
    6.  Configuring one or more delivery destinations.
    7.  Defining a schedule for automated execution using a CRON expression builder.
*   The backend data model for report configurations shall store all selected parameters.

**4.6. Report Generation**
*   The report generation engine shall select a processing pipeline based on the configured output format.
*   **Templated Generation:** For `HTML` and `PDF` formats, the engine shall process data using a Handlebars template to produce an HTML document. For `PDF`, the HTML is then rendered using an embedded Chromium engine.
*   **Direct Data Serialization:** For `JSON`, `CSV`, and `TXT` formats, the engine shall bypass templating and directly serialize the transformed data into the selected format.
*   The system shall handle and log errors gracefully during generation, such as invalid templates or data serialization failures, and mark the job as 'Failed'.
*   **Acceptance Criteria:**
    *   JSON output must be well-formed.
    *   CSV output must be RFC 4180 compliant with a header row.
    *   TXT output format (delimiters, fixed-width columns) shall be configurable.

**4.7. Report Delivery**
*   The system shall support multiple delivery targets per report, including a new target named 'API Response'.
*   **Synchronous Mode (API Response):** For quick reports, the API call shall hold the connection and return the report content in the HTTP response body. A 30-second timeout shall be implemented, returning an HTTP 408 error if exceeded.
    *   The system shall prevent reports estimated to exceed the synchronous timeout from being executed in this mode. If a synchronous request is made for such a report, the API shall return an HTTP 409 Conflict error with a message directing the client to use the asynchronous method.
*   **Asynchronous Mode (API Response):** For long-running reports, the initial API call shall immediately return an HTTP 202 Accepted response with a `jobId` and a status polling URL. The client can then poll for status and retrieve the result from a separate URL upon completion.
*   The choice between synchronous and asynchronous behavior shall be configurable at the report definition level for the 'API Response' delivery target.

**4.8. Configuration Management**
*   The system shall automatically create a new, versioned copy of a configuration item (Connector, Transformation, ReportConfiguration) every time it is saved.
*   The system shall maintain a version history for these critical configurations.
*   The Control Panel shall provide a UI to view the version history for a configuration item, showing the version number, who made the change, and when.
*   The UI shall allow an administrator to view a side-by-side 'diff' of changes between any two selected versions.
*   The UI shall allow an administrator to select a previous version and restore it as the current active version.

**4.9. Job Monitoring and Management**
*   The Control Panel shall include a dashboard to view the status of all report generation and delivery jobs in real-time.
*   The dashboard shall list recent and running jobs with their name, status (Queued, Running, Succeeded, Failed), and start/end times.
*   Administrators shall be able to view detailed, timestamped execution logs for each stage of any job (ingestion, transformation, generation, delivery).
*   All errors shall be logged with detailed context and stack traces.
*   Administrators shall be able to manually cancel jobs in the 'Queued' or 'Running' state and retry jobs in the 'Failed' state from the dashboard.
*   The system shall send an email notification to a configurable list of administrator emails upon job failure.

#### **5. Interface Requirements**

**5.1. User Interfaces**
*   **General:** The UI shall be built with React 18 and TypeScript. It shall be clean, intuitive, responsive for resolutions 1280px wide and above, and adhere to WCAG 2.1 Level AA. A single, standard light theme shall be provided.
*   **Internationalization Readiness:** All user-facing strings in the UI shall be stored in JSON resource files to facilitate future localization. The initial release shall only provide English (en-US).
*   **Login:** The system shall provide a login screen with inputs for username and password, and support for 2FA codes if enabled.
*   **Control Panel:** The Control Panel shall use a navigation sidebar for accessing different sections: Dashboard, Reports, Connectors, Transformations, Templates, Users, and System Settings.
*   **Report Viewer:**
    *   The viewer shall require user authentication and display a list of generated reports based on the user's permissions.
    *   The report list shall be searchable by report name and filterable by generation date range and status.
    *   The viewer shall render HTML reports in the browser and provide download links for other formats.
    *   The viewer shall support bulk operations (delete, re-run delivery) on multiple reports selected via checkboxes.
    *   Administrators shall be able to configure access permissions for reports, granting or denying access to specific user roles.

**5.2. API Interfaces**
*   **General:** The system's RESTful API shall be documented in an OpenAPI 3.0 specification.
*   **Authentication:** All API endpoints (except login) shall require a valid JWT in the `Authorization: Bearer <token>` header.
*   **Configuration Endpoints:**
    *   The API shall provide full CRUD endpoints for managing Reports, Connectors, Transformations, Templates, and Users.
    *   `POST /api/v1/reports` and `PUT /api/v1/reports/{id}` data models shall be updated to include a mandatory `outputFormat` field (`HTML`, `PDF`, `JSON`, `CSV`, `TXT`).
    *   Delivery configuration models shall be updated to include a new delivery type, `API_RESPONSE`, with associated parameters for synchronous timeout and asynchronous mode selection.
*   **Generation Endpoints:**
    *   `POST /api/v1/reports/{id}/generate`: Triggers on-demand report generation.
        *   Synchronous Success Response: HTTP 200 OK with report content.
        *   Synchronous Timeout Response: HTTP 408 Request Timeout.
        *   Synchronous Rejection Response: HTTP 409 Conflict with a JSON body `{\"error\": \"Report is too large for synchronous generation. Use asynchronous mode.\"}`.
        *   Asynchronous Initiated Response: HTTP 202 Accepted with a JSON body containing `{\"jobId\": \"...\", \"statusUrl\": \"...\"}`.
    *   `GET /api/v1/jobs/{jobId}`: Polls the status of an asynchronous job. Returns job status, timestamps, and logs.
    *   `GET /api/v1/jobs/{jobId}/result`: Retrieves the output of a completed asynchronous job.

**5.3. Communication Interfaces**
*   The Control Panel and Report Viewer shall be served over HTTPS.
*   Communication with the cloud licensing service shall use HTTPS over TCP port 443.
*   Email delivery shall use SMTP/SMTPS protocols.
*   File transfer delivery shall use FTP/SFTP protocols.
*   Cloud storage delivery shall use HTTPS protocol.

#### **6. Non-Functional Requirements**

**6.1. Performance**
*   The system shall process datasets of up to 1 million records from a data source into an intermediate JSON file within 5 minutes on recommended hardware.
*   Connectors shall use data streaming where feasible. For reports that do not involve a data transformation script, entire datasets shall not be loaded into memory.
*   The data transformation engine's memory consumption shall not exceed 4 GB when processing a 1 million record dataset on recommended hardware.
*   Report generation (HTML to PDF conversion) shall complete within 10 seconds for reports based on datasets under 10,000 records.
*   All Control Panel and Report Viewer API responses shall have a P95 latency of under 200ms under normal load (up to 100 concurrent users).
*   UI pages shall achieve a Largest Contentful Paint (LCP) of under 2.5 seconds.
*   The Windows service shall consume less than 256 MB of RAM while idle.

**6.2. Scalability**
*   The system shall support up to 100 concurrent users accessing the Control Panel or Report Viewer.
*   The system shall handle up to 1,000 scheduled report generations per day without performance degradation.
*   The primary scaling mechanism shall be vertical scaling (increasing CPU/RAM of the host machine).
*   The Quartz.NET scheduler shall be configured with a thread pool of 10 concurrent jobs. This value shall be configurable.

**6.3. Reliability**
*   The Windows service shall achieve 99.9% uptime, excluding scheduled maintenance.
*   The Windows service shall be configured to restart automatically upon crashing.
*   The system shall resume any queued or interrupted tasks where feasible upon restart.
*   **Backup and Recovery:**
    *   The system shall have a Recovery Point Objective (RPO) of 24 hours for all configuration data.
    *   The system shall have a Recovery Time Objective (RTO) of 4 hours, assuming a valid backup and required hardware are available.
    *   The Control Panel shall provide a function for Administrators to trigger an on-demand backup of the entire configuration database to a specified file path.
    *   The Control Panel shall provide a function for Administrators to restore the system's configuration from a previously created backup file.
*   Disaster recovery shall be achieved by reinstalling the application and restoring the application's data directory from a backup. The backup and restore procedure shall be documented for the IT Support user class.

**6.4. Security**
*   All external data transmissions (licensing, delivery) shall use HTTPS/TLS 1.2+ encryption.
*   All local web traffic (Control Panel, API) shall use HTTPS/TLS 1.2+ encryption, configured during installation.
*   Sensitive configuration data stored in the SQLite database shall be encrypted using .NET's Data Protection APIs (DPAPI).
*   The system shall implement Role-Based Access Control (RBAC) for all functions and data access.
*   **Auditability:**
    *   Audit logs shall track all security-sensitive events, including: user logins (success and failure), password changes and resets, user/role CRUD operations, changes to system-level settings, and all CRUD operations on report, connector, and transformation configurations.
    *   Audit logs shall include a timestamp, the responsible user, the source IP address, the action performed, and the outcome.
    *   Audit logs shall be stored in a tamper-evident format and be exportable by an administrator in CSV and JSON formats.
*   The system shall be architecturally designed to prevent the transmission of raw or intermediate data from customer data sources over any external network.

**6.5. Logging and Monitoring**
*   All system logs shall be written in a structured JSON format using the Serilog library.
*   Logs shall be written to rolling files in a dedicated `logs` subdirectory, rotated daily.
*   Log files shall be retained for 30 days. This retention period shall be configurable.
*   The logging level (Verbose, Debug, Information, Warning, Error, Fatal) shall be configurable at runtime without requiring a service restart.
*   Logs shall capture detailed information for all system activities, including job execution steps, API requests, user actions, and errors with full stack traces.
*   The Job Monitoring Dashboard shall serve as the primary real-time monitoring interface.
*   **System Health Monitoring and Alerting:** The system shall monitor its own health, including host CPU usage, memory consumption, and available disk space. An administrator shall be able to configure thresholds for these metrics. If a threshold is breached for a configurable duration, the system shall log a critical error and send an alert to the administrator email list.

**6.6. Maintainability and Quality**
*   All code shall adhere to defined coding standards for C# and TypeScript.
*   All code shall have a minimum unit test coverage of 80%.
*   The system shall be delivered with comprehensive documentation, including:
    *   An Installation and Administration Guide for IT Support.
    *   A User Guide for Administrators and End-Users.
    *   A Plug-in Development Kit (PDK) with documentation and example projects for System Integrators.
    *   An API Reference Guide, generated from the OpenAPI specification, for external system integration.

**6.7. Support and Maintenance**
*   **Update Procedure:** The Windows Installer (MSI) package shall support in-place upgrades. The upgrade process must preserve all existing user data, configurations, logs, and generated reports.
*   **Maintenance Windows:** The system shall not require explicit maintenance windows for normal operation. Service restarts for updates should be planned by the IT Support user.
*   **Incident Handling:** The Control Panel shall provide a feature for Administrators to generate and download a "Support Bundle," which is a ZIP archive containing all system logs, configuration files (with secrets redacted), and system health information for a specified time period. This bundle can be provided to support personnel for troubleshooting.

#### **7. Transition Requirements**

**7.1. Implementation Strategy**
*   **Initial Deployment:** The system shall be deployed via a "Big Bang" approach for new installations, where the entire system is made operational at once.
*   **Upgrades:** Future updates shall be deployed as in-place upgrades using the provided MSI installer, following a rolling update model if multiple instances are deployed for high availability (not in scope for initial release).

**7.2. Data and Configuration Migration**
*   The system does not provide automated tools for migrating configurations from other reporting systems.
*   All system configurations (Connectors, Reports, Users, etc.) must be manually created using the Control Panel or API.
*   The Installation and Administration Guide shall include a chapter on "Initial System Configuration" outlining the recommended sequence for setting up the system from a clean state.

**7.3. User Training**
*   **Administrator & IT Support:** Training shall be provided via comprehensive documentation (User Guide, Installation Guide) and a series of pre-recorded video tutorials covering installation, configuration, user management, and backup/restore procedures.
*   **End-User (Viewer):** Training shall be provided via a concise Quick Start Guide and in-application tooltips explaining the features of the Report Viewer.
*   **System Integrator:** Training shall be provided through the Plug-in Development Kit (PDK), which includes detailed documentation, code examples for FHIR and HL7 connectors, and a reference implementation.

**7.4. System Cutover**
*   The IT Support user is responsible for executing the cutover plan. The plan shall consist of the following phases documented in the Installation Guide:
    *   **Pre-Cutover:** Verify all hardware, software, and network prerequisites are met. Perform a full backup of the host machine.
    *   **Installation:** Run the MSI installer and complete the initial setup wizard, including creating the primary administrator account and configuring HTTPS.
    *   **Configuration:** Manually configure all required connectors, report definitions, users, and roles.
    *   **Validation:** Execute all report jobs manually to validate data ingestion, transformation, generation, and delivery. Verify user access and permissions.
    *   **Go-Live:** Activate the scheduled jobs and notify end-users that the system is operational.
    *   **Fallback Plan:** In case of critical failure during validation, the fallback procedure is to uninstall the software and restore the host machine from the pre-cutover backup.

**7.5. Legacy System Decommissioning**
*   The customer is solely responsible for the decommissioning of any legacy reporting systems.
*   The system shall operate in parallel with legacy systems during the configuration and validation phase.
*   The decommissioning plan for the legacy system, including data archival, should be executed by the customer after the new system's successful go-live.

#### **8. Business Rules and Compliance**

**8.1. General Business Rules**
*   A report job shall be marked as 'Failed' if any of its constituent steps (ingestion, transformation, generation, or any single delivery) fails.
*   If a data source is unavailable during a scheduled run, the system shall retry the connection based on the Polly resiliency policy (default: 3 retries with exponential backoff) before failing the job.
*   CRON expressions used for scheduling must be validated for correctness upon saving a report configuration. The system shall not support expressions that schedule jobs more frequently than once per minute.

**8.2. Regulatory Compliance**
*   **HIPAA:** For deployments in healthcare environments handling Protected Health Information (PHI), the system provides features to support HIPAA compliance. The customer is responsible for ensuring their configuration and use of the system complies with HIPAA regulations. The system's supporting features include:
    *   **Access Control:** Role-Based Access Control (RBAC) to enforce the principle of least privilege.
    *   **Audit Controls:** Tamper-evident audit logs to record access and modifications to PHI-related configurations.
    *   **Data Integrity:** Use of secure, encrypted communication channels (HTTPS/TLS 1.2+).
    *   **Data Privacy:** All data processing occurs on-premise, and data at rest (configuration database) is encrypted.

**8.3. Legal and Licensing Constraints**
*   **End-User License Agreement (EULA):** Use of the software is governed by a EULA that must be accepted during installation.
*   **Data Ownership:** The customer retains full ownership of all their data, including source data, configuration data, and generated reports.
*   **Intellectual Property:** The software, its architecture, and the provided documentation are the intellectual property of the vendor. Custom plug-ins developed by the System Integrator are the intellectual property of their creator.

**8.4. Adherence to Industry Standards**
*   **Web Security:** The system shall be developed with security best practices in mind to mitigate risks outlined in the OWASP Top 10, including prevention of injection attacks, broken authentication, and cross-site scripting (XSS).
*   **Accessibility:** The web interfaces (Control Panel, Report Viewer) shall conform to the Web Content Accessibility Guidelines (WCAG) 2.1 at Level AA.

**8.5. Organizational Policy Alignment**
*   The system shall provide configuration options to allow administrators to align its operation with internal organizational policies.
*   Configurable areas include, but are not limited to: password policies, session timeout durations, log retention periods, and system health monitoring thresholds.