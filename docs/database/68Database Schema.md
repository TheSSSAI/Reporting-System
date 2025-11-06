# 1 Title

Core Application Relational Database

# 2 Name

transformation_service_main_db

# 3 Db Type

- relational

# 4 Db Technology

PostgreSQL

# 5 Entities

## 5.1 User

### 5.1.1 Name

User

### 5.1.2 Description

Represents a system user. Manages credentials, roles, and personal information for auditing and access control.

### 5.1.3 Attributes

#### 5.1.3.1 UUID

##### 5.1.3.1.1 Name

userId

##### 5.1.3.1.2 Type

🔹 UUID

##### 5.1.3.1.3 Is Required

✅ Yes

##### 5.1.3.1.4 Is Primary Key

✅ Yes

##### 5.1.3.1.5 Size

0

##### 5.1.3.1.6 Is Unique

✅ Yes

##### 5.1.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.1.3.1.8 Precision

0

##### 5.1.3.1.9 Scale

0

##### 5.1.3.1.10 Is Foreign Key

❌ No

#### 5.1.3.2.0 VARCHAR

##### 5.1.3.2.1 Name

username

##### 5.1.3.2.2 Type

🔹 VARCHAR

##### 5.1.3.2.3 Is Required

✅ Yes

##### 5.1.3.2.4 Is Primary Key

❌ No

##### 5.1.3.2.5 Size

100

##### 5.1.3.2.6 Is Unique

✅ Yes

##### 5.1.3.2.7 Constraints

*No items available*

##### 5.1.3.2.8 Precision

0

##### 5.1.3.2.9 Scale

0

##### 5.1.3.2.10 Is Foreign Key

❌ No

#### 5.1.3.3.0 VARCHAR

##### 5.1.3.3.1 Name

email

##### 5.1.3.3.2 Type

🔹 VARCHAR

##### 5.1.3.3.3 Is Required

✅ Yes

##### 5.1.3.3.4 Is Primary Key

❌ No

##### 5.1.3.3.5 Size

255

##### 5.1.3.3.6 Is Unique

✅ Yes

##### 5.1.3.3.7 Constraints

*No items available*

##### 5.1.3.3.8 Precision

0

##### 5.1.3.3.9 Scale

0

##### 5.1.3.3.10 Is Foreign Key

❌ No

#### 5.1.3.4.0 VARCHAR

##### 5.1.3.4.1 Name

passwordHash

##### 5.1.3.4.2 Type

🔹 VARCHAR

##### 5.1.3.4.3 Is Required

✅ Yes

##### 5.1.3.4.4 Is Primary Key

❌ No

##### 5.1.3.4.5 Size

255

##### 5.1.3.4.6 Is Unique

❌ No

##### 5.1.3.4.7 Constraints

*No items available*

##### 5.1.3.4.8 Precision

0

##### 5.1.3.4.9 Scale

0

##### 5.1.3.4.10 Is Foreign Key

❌ No

#### 5.1.3.5.0 VARCHAR

##### 5.1.3.5.1 Name

twoFactorSecret

##### 5.1.3.5.2 Type

🔹 VARCHAR

##### 5.1.3.5.3 Is Required

❌ No

##### 5.1.3.5.4 Is Primary Key

❌ No

##### 5.1.3.5.5 Size

255

##### 5.1.3.5.6 Is Unique

❌ No

##### 5.1.3.5.7 Constraints

*No items available*

##### 5.1.3.5.8 Precision

0

##### 5.1.3.5.9 Scale

0

##### 5.1.3.5.10 Is Foreign Key

❌ No

#### 5.1.3.6.0 BOOLEAN

##### 5.1.3.6.1 Name

isTwoFactorEnabled

##### 5.1.3.6.2 Type

🔹 BOOLEAN

##### 5.1.3.6.3 Is Required

✅ Yes

##### 5.1.3.6.4 Is Primary Key

❌ No

##### 5.1.3.6.5 Size

0

##### 5.1.3.6.6 Is Unique

❌ No

##### 5.1.3.6.7 Constraints

- DEFAULT false

##### 5.1.3.6.8 Precision

0

##### 5.1.3.6.9 Scale

0

##### 5.1.3.6.10 Is Foreign Key

❌ No

#### 5.1.3.7.0 TIMESTAMPTZ

##### 5.1.3.7.1 Name

passwordLastChangedAt

##### 5.1.3.7.2 Type

🔹 TIMESTAMPTZ

##### 5.1.3.7.3 Is Required

✅ Yes

##### 5.1.3.7.4 Is Primary Key

❌ No

##### 5.1.3.7.5 Size

0

##### 5.1.3.7.6 Is Unique

❌ No

##### 5.1.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.7.8 Precision

0

##### 5.1.3.7.9 Scale

0

##### 5.1.3.7.10 Is Foreign Key

❌ No

#### 5.1.3.8.0 INT

##### 5.1.3.8.1 Name

failedLoginAttempts

##### 5.1.3.8.2 Type

🔹 INT

##### 5.1.3.8.3 Is Required

✅ Yes

##### 5.1.3.8.4 Is Primary Key

❌ No

##### 5.1.3.8.5 Size

0

##### 5.1.3.8.6 Is Unique

❌ No

##### 5.1.3.8.7 Constraints

- DEFAULT 0

##### 5.1.3.8.8 Precision

0

##### 5.1.3.8.9 Scale

0

##### 5.1.3.8.10 Is Foreign Key

❌ No

#### 5.1.3.9.0 BOOLEAN

##### 5.1.3.9.1 Name

isLocked

##### 5.1.3.9.2 Type

🔹 BOOLEAN

##### 5.1.3.9.3 Is Required

✅ Yes

##### 5.1.3.9.4 Is Primary Key

❌ No

##### 5.1.3.9.5 Size

0

##### 5.1.3.9.6 Is Unique

❌ No

##### 5.1.3.9.7 Constraints

- DEFAULT false

##### 5.1.3.9.8 Precision

0

##### 5.1.3.9.9 Scale

0

##### 5.1.3.9.10 Is Foreign Key

❌ No

#### 5.1.3.10.0 BOOLEAN

##### 5.1.3.10.1 Name

isActive

##### 5.1.3.10.2 Type

🔹 BOOLEAN

##### 5.1.3.10.3 Is Required

✅ Yes

##### 5.1.3.10.4 Is Primary Key

❌ No

##### 5.1.3.10.5 Size

0

##### 5.1.3.10.6 Is Unique

❌ No

##### 5.1.3.10.7 Constraints

- DEFAULT true

##### 5.1.3.10.8 Precision

0

##### 5.1.3.10.9 Scale

0

##### 5.1.3.10.10 Is Foreign Key

❌ No

#### 5.1.3.11.0 TIMESTAMPTZ

##### 5.1.3.11.1 Name

createdAt

##### 5.1.3.11.2 Type

🔹 TIMESTAMPTZ

##### 5.1.3.11.3 Is Required

✅ Yes

##### 5.1.3.11.4 Is Primary Key

❌ No

##### 5.1.3.11.5 Size

0

##### 5.1.3.11.6 Is Unique

❌ No

##### 5.1.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.11.8 Precision

0

##### 5.1.3.11.9 Scale

0

##### 5.1.3.11.10 Is Foreign Key

❌ No

#### 5.1.3.12.0 TIMESTAMPTZ

##### 5.1.3.12.1 Name

updatedAt

##### 5.1.3.12.2 Type

🔹 TIMESTAMPTZ

##### 5.1.3.12.3 Is Required

✅ Yes

##### 5.1.3.12.4 Is Primary Key

❌ No

##### 5.1.3.12.5 Size

0

##### 5.1.3.12.6 Is Unique

❌ No

##### 5.1.3.12.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.12.8 Precision

0

##### 5.1.3.12.9 Scale

0

##### 5.1.3.12.10 Is Foreign Key

❌ No

### 5.1.4.0.0 Primary Keys

- userId

### 5.1.5.0.0 Unique Constraints

#### 5.1.5.1.0 uq_user_username

##### 5.1.5.1.1 Name

uq_user_username

##### 5.1.5.1.2 Columns

- username

#### 5.1.5.2.0 uq_user_email

##### 5.1.5.2.1 Name

uq_user_email

##### 5.1.5.2.2 Columns

- email

### 5.1.6.0.0 Indexes

- {'name': 'idx_user_active_locked', 'columns': ['isActive', 'isLocked'], 'type': 'BTree'}

## 5.2.0.0.0 Role

### 5.2.1.0.0 Name

Role

### 5.2.2.0.0 Description

Defines a role for Role-Based Access Control (RBAC), such as 'Administrator' or 'Viewer'.

### 5.2.3.0.0 Attributes

#### 5.2.3.1.0 SERIAL

##### 5.2.3.1.1 Name

roleId

##### 5.2.3.1.2 Type

🔹 SERIAL

##### 5.2.3.1.3 Is Required

✅ Yes

##### 5.2.3.1.4 Is Primary Key

✅ Yes

##### 5.2.3.1.5 Size

0

##### 5.2.3.1.6 Is Unique

✅ Yes

##### 5.2.3.1.7 Constraints

*No items available*

##### 5.2.3.1.8 Precision

0

##### 5.2.3.1.9 Scale

0

##### 5.2.3.1.10 Is Foreign Key

❌ No

#### 5.2.3.2.0 VARCHAR

##### 5.2.3.2.1 Name

roleName

##### 5.2.3.2.2 Type

🔹 VARCHAR

##### 5.2.3.2.3 Is Required

✅ Yes

##### 5.2.3.2.4 Is Primary Key

❌ No

##### 5.2.3.2.5 Size

50

##### 5.2.3.2.6 Is Unique

✅ Yes

##### 5.2.3.2.7 Constraints

*No items available*

##### 5.2.3.2.8 Precision

0

##### 5.2.3.2.9 Scale

0

##### 5.2.3.2.10 Is Foreign Key

❌ No

#### 5.2.3.3.0 TEXT

##### 5.2.3.3.1 Name

description

##### 5.2.3.3.2 Type

🔹 TEXT

##### 5.2.3.3.3 Is Required

❌ No

##### 5.2.3.3.4 Is Primary Key

❌ No

##### 5.2.3.3.5 Size

0

##### 5.2.3.3.6 Is Unique

❌ No

##### 5.2.3.3.7 Constraints

*No items available*

##### 5.2.3.3.8 Precision

0

##### 5.2.3.3.9 Scale

0

##### 5.2.3.3.10 Is Foreign Key

❌ No

### 5.2.4.0.0 Primary Keys

- roleId

### 5.2.5.0.0 Unique Constraints

- {'name': 'uq_role_rolename', 'columns': ['roleName']}

### 5.2.6.0.0 Indexes

*No items available*

## 5.3.0.0.0 UserRole

### 5.3.1.0.0 Name

UserRole

### 5.3.2.0.0 Description

Junction table for the many-to-many relationship between Users and Roles. Cached in Redis to optimize auth checks.

### 5.3.3.0.0 Attributes

#### 5.3.3.1.0 UUID

##### 5.3.3.1.1 Name

userId

##### 5.3.3.1.2 Type

🔹 UUID

##### 5.3.3.1.3 Is Required

✅ Yes

##### 5.3.3.1.4 Is Primary Key

✅ Yes

##### 5.3.3.1.5 Size

0

##### 5.3.3.1.6 Is Unique

❌ No

##### 5.3.3.1.7 Constraints

*No items available*

##### 5.3.3.1.8 Precision

0

##### 5.3.3.1.9 Scale

0

##### 5.3.3.1.10 Is Foreign Key

✅ Yes

#### 5.3.3.2.0 INT

##### 5.3.3.2.1 Name

roleId

##### 5.3.3.2.2 Type

🔹 INT

##### 5.3.3.2.3 Is Required

✅ Yes

##### 5.3.3.2.4 Is Primary Key

✅ Yes

##### 5.3.3.2.5 Size

0

##### 5.3.3.2.6 Is Unique

❌ No

##### 5.3.3.2.7 Constraints

*No items available*

##### 5.3.3.2.8 Precision

0

##### 5.3.3.2.9 Scale

0

##### 5.3.3.2.10 Is Foreign Key

✅ Yes

### 5.3.4.0.0 Primary Keys

- userId
- roleId

### 5.3.5.0.0 Unique Constraints

*No items available*

### 5.3.6.0.0 Indexes

- {'name': 'idx_userrole_roleid', 'columns': ['roleId'], 'type': 'BTree'}

## 5.4.0.0.0 TransformationScript

### 5.4.1.0.0 Name

TransformationScript

### 5.4.2.0.0 Description

Stores the master record for a transformation script. Each script can have multiple versions, with one designated as active.

### 5.4.3.0.0 Attributes

#### 5.4.3.1.0 UUID

##### 5.4.3.1.1 Name

transformationScriptId

##### 5.4.3.1.2 Type

🔹 UUID

##### 5.4.3.1.3 Is Required

✅ Yes

##### 5.4.3.1.4 Is Primary Key

✅ Yes

##### 5.4.3.1.5 Size

0

##### 5.4.3.1.6 Is Unique

✅ Yes

##### 5.4.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.4.3.1.8 Precision

0

##### 5.4.3.1.9 Scale

0

##### 5.4.3.1.10 Is Foreign Key

❌ No

#### 5.4.3.2.0 VARCHAR

##### 5.4.3.2.1 Name

name

##### 5.4.3.2.2 Type

🔹 VARCHAR

##### 5.4.3.2.3 Is Required

✅ Yes

##### 5.4.3.2.4 Is Primary Key

❌ No

##### 5.4.3.2.5 Size

255

##### 5.4.3.2.6 Is Unique

✅ Yes

##### 5.4.3.2.7 Constraints

*No items available*

##### 5.4.3.2.8 Precision

0

##### 5.4.3.2.9 Scale

0

##### 5.4.3.2.10 Is Foreign Key

❌ No

#### 5.4.3.3.0 TEXT

##### 5.4.3.3.1 Name

description

##### 5.4.3.3.2 Type

🔹 TEXT

##### 5.4.3.3.3 Is Required

❌ No

##### 5.4.3.3.4 Is Primary Key

❌ No

##### 5.4.3.3.5 Size

0

##### 5.4.3.3.6 Is Unique

❌ No

##### 5.4.3.3.7 Constraints

*No items available*

##### 5.4.3.3.8 Precision

0

##### 5.4.3.3.9 Scale

0

##### 5.4.3.3.10 Is Foreign Key

❌ No

#### 5.4.3.4.0 UUID

##### 5.4.3.4.1 Name

activeVersionId

##### 5.4.3.4.2 Type

🔹 UUID

##### 5.4.3.4.3 Is Required

❌ No

##### 5.4.3.4.4 Is Primary Key

❌ No

##### 5.4.3.4.5 Size

0

##### 5.4.3.4.6 Is Unique

❌ No

##### 5.4.3.4.7 Constraints

*No items available*

##### 5.4.3.4.8 Precision

0

##### 5.4.3.4.9 Scale

0

##### 5.4.3.4.10 Is Foreign Key

✅ Yes

#### 5.4.3.5.0 UUID

##### 5.4.3.5.1 Name

createdByUserId

##### 5.4.3.5.2 Type

🔹 UUID

##### 5.4.3.5.3 Is Required

✅ Yes

##### 5.4.3.5.4 Is Primary Key

❌ No

##### 5.4.3.5.5 Size

0

##### 5.4.3.5.6 Is Unique

❌ No

##### 5.4.3.5.7 Constraints

*No items available*

##### 5.4.3.5.8 Precision

0

##### 5.4.3.5.9 Scale

0

##### 5.4.3.5.10 Is Foreign Key

✅ Yes

#### 5.4.3.6.0 UUID

##### 5.4.3.6.1 Name

updatedByUserId

##### 5.4.3.6.2 Type

🔹 UUID

##### 5.4.3.6.3 Is Required

✅ Yes

##### 5.4.3.6.4 Is Primary Key

❌ No

##### 5.4.3.6.5 Size

0

##### 5.4.3.6.6 Is Unique

❌ No

##### 5.4.3.6.7 Constraints

*No items available*

##### 5.4.3.6.8 Precision

0

##### 5.4.3.6.9 Scale

0

##### 5.4.3.6.10 Is Foreign Key

✅ Yes

#### 5.4.3.7.0 TIMESTAMPTZ

##### 5.4.3.7.1 Name

createdAt

##### 5.4.3.7.2 Type

🔹 TIMESTAMPTZ

##### 5.4.3.7.3 Is Required

✅ Yes

##### 5.4.3.7.4 Is Primary Key

❌ No

##### 5.4.3.7.5 Size

0

##### 5.4.3.7.6 Is Unique

❌ No

##### 5.4.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.7.8 Precision

0

##### 5.4.3.7.9 Scale

0

##### 5.4.3.7.10 Is Foreign Key

❌ No

#### 5.4.3.8.0 TIMESTAMPTZ

##### 5.4.3.8.1 Name

updatedAt

##### 5.4.3.8.2 Type

🔹 TIMESTAMPTZ

##### 5.4.3.8.3 Is Required

✅ Yes

##### 5.4.3.8.4 Is Primary Key

❌ No

##### 5.4.3.8.5 Size

0

##### 5.4.3.8.6 Is Unique

❌ No

##### 5.4.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.8.8 Precision

0

##### 5.4.3.8.9 Scale

0

##### 5.4.3.8.10 Is Foreign Key

❌ No

### 5.4.4.0.0 Primary Keys

- transformationScriptId

### 5.4.5.0.0 Unique Constraints

- {'name': 'uq_transformationscript_name', 'columns': ['name']}

### 5.4.6.0.0 Indexes

- {'name': 'idx_transformationscript_activeversionid', 'columns': ['activeVersionId'], 'type': 'BTree'}

## 5.5.0.0.0 TransformationScriptVersion

### 5.5.1.0.0 Name

TransformationScriptVersion

### 5.5.2.0.0 Description

Stores a specific, immutable version of a script's content. Content is encrypted at rest.

### 5.5.3.0.0 Attributes

#### 5.5.3.1.0 UUID

##### 5.5.3.1.1 Name

transformationScriptVersionId

##### 5.5.3.1.2 Type

🔹 UUID

##### 5.5.3.1.3 Is Required

✅ Yes

##### 5.5.3.1.4 Is Primary Key

✅ Yes

##### 5.5.3.1.5 Size

0

##### 5.5.3.1.6 Is Unique

✅ Yes

##### 5.5.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.5.3.1.8 Precision

0

##### 5.5.3.1.9 Scale

0

##### 5.5.3.1.10 Is Foreign Key

❌ No

#### 5.5.3.2.0 UUID

##### 5.5.3.2.1 Name

transformationScriptId

##### 5.5.3.2.2 Type

🔹 UUID

##### 5.5.3.2.3 Is Required

✅ Yes

##### 5.5.3.2.4 Is Primary Key

❌ No

##### 5.5.3.2.5 Size

0

##### 5.5.3.2.6 Is Unique

❌ No

##### 5.5.3.2.7 Constraints

*No items available*

##### 5.5.3.2.8 Precision

0

##### 5.5.3.2.9 Scale

0

##### 5.5.3.2.10 Is Foreign Key

✅ Yes

#### 5.5.3.3.0 INT

##### 5.5.3.3.1 Name

versionNumber

##### 5.5.3.3.2 Type

🔹 INT

##### 5.5.3.3.3 Is Required

✅ Yes

##### 5.5.3.3.4 Is Primary Key

❌ No

##### 5.5.3.3.5 Size

0

##### 5.5.3.3.6 Is Unique

❌ No

##### 5.5.3.3.7 Constraints

*No items available*

##### 5.5.3.3.8 Precision

0

##### 5.5.3.3.9 Scale

0

##### 5.5.3.3.10 Is Foreign Key

❌ No

#### 5.5.3.4.0 BYTEA

##### 5.5.3.4.1 Name

scriptContentEncrypted

##### 5.5.3.4.2 Type

🔹 BYTEA

##### 5.5.3.4.3 Is Required

✅ Yes

##### 5.5.3.4.4 Is Primary Key

❌ No

##### 5.5.3.4.5 Size

0

##### 5.5.3.4.6 Is Unique

❌ No

##### 5.5.3.4.7 Constraints

*No items available*

##### 5.5.3.4.8 Precision

0

##### 5.5.3.4.9 Scale

0

##### 5.5.3.4.10 Is Foreign Key

❌ No

#### 5.5.3.5.0 TEXT

##### 5.5.3.5.1 Name

changeNotes

##### 5.5.3.5.2 Type

🔹 TEXT

##### 5.5.3.5.3 Is Required

❌ No

##### 5.5.3.5.4 Is Primary Key

❌ No

##### 5.5.3.5.5 Size

0

##### 5.5.3.5.6 Is Unique

❌ No

##### 5.5.3.5.7 Constraints

*No items available*

##### 5.5.3.5.8 Precision

0

##### 5.5.3.5.9 Scale

0

##### 5.5.3.5.10 Is Foreign Key

❌ No

#### 5.5.3.6.0 UUID

##### 5.5.3.6.1 Name

createdByUserId

##### 5.5.3.6.2 Type

🔹 UUID

##### 5.5.3.6.3 Is Required

✅ Yes

##### 5.5.3.6.4 Is Primary Key

❌ No

##### 5.5.3.6.5 Size

0

##### 5.5.3.6.6 Is Unique

❌ No

##### 5.5.3.6.7 Constraints

*No items available*

##### 5.5.3.6.8 Precision

0

##### 5.5.3.6.9 Scale

0

##### 5.5.3.6.10 Is Foreign Key

✅ Yes

#### 5.5.3.7.0 TIMESTAMPTZ

##### 5.5.3.7.1 Name

createdAt

##### 5.5.3.7.2 Type

🔹 TIMESTAMPTZ

##### 5.5.3.7.3 Is Required

✅ Yes

##### 5.5.3.7.4 Is Primary Key

❌ No

##### 5.5.3.7.5 Size

0

##### 5.5.3.7.6 Is Unique

❌ No

##### 5.5.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.5.3.7.8 Precision

0

##### 5.5.3.7.9 Scale

0

##### 5.5.3.7.10 Is Foreign Key

❌ No

### 5.5.4.0.0 Primary Keys

- transformationScriptVersionId

### 5.5.5.0.0 Unique Constraints

- {'name': 'uq_scriptversion_script_version', 'columns': ['transformationScriptId', 'versionNumber']}

### 5.5.6.0.0 Indexes

- {'name': 'idx_scriptversion_scriptid_version_desc', 'columns': ['transformationScriptId', 'versionNumber DESC'], 'type': 'BTree'}

## 5.6.0.0.0 ReportConfiguration

### 5.6.1.0.0 Name

ReportConfiguration

### 5.6.2.0.0 Description

Defines a complete report job, linking a connector, an optional transformation script, an output format, a template, delivery destinations, and a schedule.

### 5.6.3.0.0 Attributes

#### 5.6.3.1.0 UUID

##### 5.6.3.1.1 Name

reportConfigurationId

##### 5.6.3.1.2 Type

🔹 UUID

##### 5.6.3.1.3 Is Required

✅ Yes

##### 5.6.3.1.4 Is Primary Key

✅ Yes

##### 5.6.3.1.5 Size

0

##### 5.6.3.1.6 Is Unique

✅ Yes

##### 5.6.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.6.3.1.8 Precision

0

##### 5.6.3.1.9 Scale

0

##### 5.6.3.1.10 Is Foreign Key

❌ No

#### 5.6.3.2.0 VARCHAR

##### 5.6.3.2.1 Name

name

##### 5.6.3.2.2 Type

🔹 VARCHAR

##### 5.6.3.2.3 Is Required

✅ Yes

##### 5.6.3.2.4 Is Primary Key

❌ No

##### 5.6.3.2.5 Size

255

##### 5.6.3.2.6 Is Unique

❌ No

##### 5.6.3.2.7 Constraints

*No items available*

##### 5.6.3.2.8 Precision

0

##### 5.6.3.2.9 Scale

0

##### 5.6.3.2.10 Is Foreign Key

❌ No

#### 5.6.3.3.0 TEXT

##### 5.6.3.3.1 Name

description

##### 5.6.3.3.2 Type

🔹 TEXT

##### 5.6.3.3.3 Is Required

❌ No

##### 5.6.3.3.4 Is Primary Key

❌ No

##### 5.6.3.3.5 Size

0

##### 5.6.3.3.6 Is Unique

❌ No

##### 5.6.3.3.7 Constraints

*No items available*

##### 5.6.3.3.8 Precision

0

##### 5.6.3.3.9 Scale

0

##### 5.6.3.3.10 Is Foreign Key

❌ No

#### 5.6.3.4.0 UUID

##### 5.6.3.4.1 Name

connectorConfigurationId

##### 5.6.3.4.2 Type

🔹 UUID

##### 5.6.3.4.3 Is Required

✅ Yes

##### 5.6.3.4.4 Is Primary Key

❌ No

##### 5.6.3.4.5 Size

0

##### 5.6.3.4.6 Is Unique

❌ No

##### 5.6.3.4.7 Constraints

*No items available*

##### 5.6.3.4.8 Precision

0

##### 5.6.3.4.9 Scale

0

##### 5.6.3.4.10 Is Foreign Key

✅ Yes

#### 5.6.3.5.0 UUID

##### 5.6.3.5.1 Name

transformationScriptVersionId

##### 5.6.3.5.2 Type

🔹 UUID

##### 5.6.3.5.3 Is Required

❌ No

##### 5.6.3.5.4 Is Primary Key

❌ No

##### 5.6.3.5.5 Size

0

##### 5.6.3.5.6 Is Unique

❌ No

##### 5.6.3.5.7 Constraints

*No items available*

##### 5.6.3.5.8 Precision

0

##### 5.6.3.5.9 Scale

0

##### 5.6.3.5.10 Is Foreign Key

✅ Yes

#### 5.6.3.6.0 VARCHAR

##### 5.6.3.6.1 Name

outputFormat

##### 5.6.3.6.2 Type

🔹 VARCHAR

##### 5.6.3.6.3 Is Required

✅ Yes

##### 5.6.3.6.4 Is Primary Key

❌ No

##### 5.6.3.6.5 Size

10

##### 5.6.3.6.6 Is Unique

❌ No

##### 5.6.3.6.7 Constraints

- CHECK (outputFormat IN ('HTML', 'PDF', 'JSON', 'CSV', 'TXT'))

##### 5.6.3.6.8 Precision

0

##### 5.6.3.6.9 Scale

0

##### 5.6.3.6.10 Is Foreign Key

❌ No

#### 5.6.3.7.0 VARCHAR

##### 5.6.3.7.1 Name

scheduleCronExpression

##### 5.6.3.7.2 Type

🔹 VARCHAR

##### 5.6.3.7.3 Is Required

❌ No

##### 5.6.3.7.4 Is Primary Key

❌ No

##### 5.6.3.7.5 Size

100

##### 5.6.3.7.6 Is Unique

❌ No

##### 5.6.3.7.7 Constraints

*No items available*

##### 5.6.3.7.8 Precision

0

##### 5.6.3.7.9 Scale

0

##### 5.6.3.7.10 Is Foreign Key

❌ No

#### 5.6.3.8.0 BOOLEAN

##### 5.6.3.8.1 Name

isActive

##### 5.6.3.8.2 Type

🔹 BOOLEAN

##### 5.6.3.8.3 Is Required

✅ Yes

##### 5.6.3.8.4 Is Primary Key

❌ No

##### 5.6.3.8.5 Size

0

##### 5.6.3.8.6 Is Unique

❌ No

##### 5.6.3.8.7 Constraints

- DEFAULT true

##### 5.6.3.8.8 Precision

0

##### 5.6.3.8.9 Scale

0

##### 5.6.3.8.10 Is Foreign Key

❌ No

#### 5.6.3.9.0 UUID

##### 5.6.3.9.1 Name

createdByUserId

##### 5.6.3.9.2 Type

🔹 UUID

##### 5.6.3.9.3 Is Required

✅ Yes

##### 5.6.3.9.4 Is Primary Key

❌ No

##### 5.6.3.9.5 Size

0

##### 5.6.3.9.6 Is Unique

❌ No

##### 5.6.3.9.7 Constraints

*No items available*

##### 5.6.3.9.8 Precision

0

##### 5.6.3.9.9 Scale

0

##### 5.6.3.9.10 Is Foreign Key

✅ Yes

#### 5.6.3.10.0 UUID

##### 5.6.3.10.1 Name

updatedByUserId

##### 5.6.3.10.2 Type

🔹 UUID

##### 5.6.3.10.3 Is Required

✅ Yes

##### 5.6.3.10.4 Is Primary Key

❌ No

##### 5.6.3.10.5 Size

0

##### 5.6.3.10.6 Is Unique

❌ No

##### 5.6.3.10.7 Constraints

*No items available*

##### 5.6.3.10.8 Precision

0

##### 5.6.3.10.9 Scale

0

##### 5.6.3.10.10 Is Foreign Key

✅ Yes

#### 5.6.3.11.0 TIMESTAMPTZ

##### 5.6.3.11.1 Name

createdAt

##### 5.6.3.11.2 Type

🔹 TIMESTAMPTZ

##### 5.6.3.11.3 Is Required

✅ Yes

##### 5.6.3.11.4 Is Primary Key

❌ No

##### 5.6.3.11.5 Size

0

##### 5.6.3.11.6 Is Unique

❌ No

##### 5.6.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.11.8 Precision

0

##### 5.6.3.11.9 Scale

0

##### 5.6.3.11.10 Is Foreign Key

❌ No

#### 5.6.3.12.0 TIMESTAMPTZ

##### 5.6.3.12.1 Name

updatedAt

##### 5.6.3.12.2 Type

🔹 TIMESTAMPTZ

##### 5.6.3.12.3 Is Required

✅ Yes

##### 5.6.3.12.4 Is Primary Key

❌ No

##### 5.6.3.12.5 Size

0

##### 5.6.3.12.6 Is Unique

❌ No

##### 5.6.3.12.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.12.8 Precision

0

##### 5.6.3.12.9 Scale

0

##### 5.6.3.12.10 Is Foreign Key

❌ No

### 5.6.4.0.0 Primary Keys

- reportConfigurationId

### 5.6.5.0.0 Unique Constraints

*No items available*

### 5.6.6.0.0 Indexes

#### 5.6.6.1.0 BTree

##### 5.6.6.1.1 Name

idx_reportconfiguration_name

##### 5.6.6.1.2 Columns

- name

##### 5.6.6.1.3 Type

🔹 BTree

#### 5.6.6.2.0 BTree

##### 5.6.6.2.1 Name

idx_reportconfiguration_active_schedule

##### 5.6.6.2.2 Columns

- isActive
- scheduleCronExpression

##### 5.6.6.2.3 Type

🔹 BTree

## 5.7.0.0.0 JobExecutionLog

### 5.7.1.0.0 Name

JobExecutionLog

### 5.7.2.0.0 Description

Records the history of every report generation attempt, including its status, start/end times, execution logs, and a link to the generated artifact.

### 5.7.3.0.0 Attributes

#### 5.7.3.1.0 UUID

##### 5.7.3.1.1 Name

jobExecutionLogId

##### 5.7.3.1.2 Type

🔹 UUID

##### 5.7.3.1.3 Is Required

✅ Yes

##### 5.7.3.1.4 Is Primary Key

✅ Yes

##### 5.7.3.1.5 Size

0

##### 5.7.3.1.6 Is Unique

✅ Yes

##### 5.7.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.7.3.1.8 Precision

0

##### 5.7.3.1.9 Scale

0

##### 5.7.3.1.10 Is Foreign Key

❌ No

#### 5.7.3.2.0 UUID

##### 5.7.3.2.1 Name

reportConfigurationId

##### 5.7.3.2.2 Type

🔹 UUID

##### 5.7.3.2.3 Is Required

✅ Yes

##### 5.7.3.2.4 Is Primary Key

❌ No

##### 5.7.3.2.5 Size

0

##### 5.7.3.2.6 Is Unique

❌ No

##### 5.7.3.2.7 Constraints

*No items available*

##### 5.7.3.2.8 Precision

0

##### 5.7.3.2.9 Scale

0

##### 5.7.3.2.10 Is Foreign Key

✅ Yes

#### 5.7.3.3.0 UUID

##### 5.7.3.3.1 Name

transformationScriptVersionId

##### 5.7.3.3.2 Type

🔹 UUID

##### 5.7.3.3.3 Is Required

❌ No

##### 5.7.3.3.4 Is Primary Key

❌ No

##### 5.7.3.3.5 Size

0

##### 5.7.3.3.6 Is Unique

❌ No

##### 5.7.3.3.7 Constraints

*No items available*

##### 5.7.3.3.8 Precision

0

##### 5.7.3.3.9 Scale

0

##### 5.7.3.3.10 Is Foreign Key

✅ Yes

#### 5.7.3.4.0 VARCHAR

##### 5.7.3.4.1 Name

status

##### 5.7.3.4.2 Type

🔹 VARCHAR

##### 5.7.3.4.3 Is Required

✅ Yes

##### 5.7.3.4.4 Is Primary Key

❌ No

##### 5.7.3.4.5 Size

50

##### 5.7.3.4.6 Is Unique

❌ No

##### 5.7.3.4.7 Constraints

- CHECK (status IN ('Queued', 'Running', 'Completed', 'Failed', 'Cancelled'))

##### 5.7.3.4.8 Precision

0

##### 5.7.3.4.9 Scale

0

##### 5.7.3.4.10 Is Foreign Key

❌ No

#### 5.7.3.5.0 TEXT

##### 5.7.3.5.1 Name

statusMessage

##### 5.7.3.5.2 Type

🔹 TEXT

##### 5.7.3.5.3 Is Required

❌ No

##### 5.7.3.5.4 Is Primary Key

❌ No

##### 5.7.3.5.5 Size

0

##### 5.7.3.5.6 Is Unique

❌ No

##### 5.7.3.5.7 Constraints

*No items available*

##### 5.7.3.5.8 Precision

0

##### 5.7.3.5.9 Scale

0

##### 5.7.3.5.10 Is Foreign Key

❌ No

#### 5.7.3.6.0 TIMESTAMPTZ

##### 5.7.3.6.1 Name

queuedAt

##### 5.7.3.6.2 Type

🔹 TIMESTAMPTZ

##### 5.7.3.6.3 Is Required

✅ Yes

##### 5.7.3.6.4 Is Primary Key

❌ No

##### 5.7.3.6.5 Size

0

##### 5.7.3.6.6 Is Unique

❌ No

##### 5.7.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.7.3.6.8 Precision

0

##### 5.7.3.6.9 Scale

0

##### 5.7.3.6.10 Is Foreign Key

❌ No

#### 5.7.3.7.0 TIMESTAMPTZ

##### 5.7.3.7.1 Name

startedAt

##### 5.7.3.7.2 Type

🔹 TIMESTAMPTZ

##### 5.7.3.7.3 Is Required

❌ No

##### 5.7.3.7.4 Is Primary Key

❌ No

##### 5.7.3.7.5 Size

0

##### 5.7.3.7.6 Is Unique

❌ No

##### 5.7.3.7.7 Constraints

*No items available*

##### 5.7.3.7.8 Precision

0

##### 5.7.3.7.9 Scale

0

##### 5.7.3.7.10 Is Foreign Key

❌ No

#### 5.7.3.8.0 TIMESTAMPTZ

##### 5.7.3.8.1 Name

completedAt

##### 5.7.3.8.2 Type

🔹 TIMESTAMPTZ

##### 5.7.3.8.3 Is Required

❌ No

##### 5.7.3.8.4 Is Primary Key

❌ No

##### 5.7.3.8.5 Size

0

##### 5.7.3.8.6 Is Unique

❌ No

##### 5.7.3.8.7 Constraints

*No items available*

##### 5.7.3.8.8 Precision

0

##### 5.7.3.8.9 Scale

0

##### 5.7.3.8.10 Is Foreign Key

❌ No

### 5.7.4.0.0 Primary Keys

- jobExecutionLogId

### 5.7.5.0.0 Unique Constraints

*No items available*

### 5.7.6.0.0 Indexes

#### 5.7.6.1.0 BTree

##### 5.7.6.1.1 Name

idx_jobexecutionlog_status_queuedat

##### 5.7.6.1.2 Columns

- status
- queuedAt

##### 5.7.6.1.3 Type

🔹 BTree

#### 5.7.6.2.0 BTree

##### 5.7.6.2.1 Name

idx_jobexecutionlog_reportconfig_completedat

##### 5.7.6.2.2 Columns

- reportConfigurationId
- completedAt DESC

##### 5.7.6.2.3 Type

🔹 BTree

## 5.8.0.0.0 ApplicationConfiguration

### 5.8.1.0.0 Name

ApplicationConfiguration

### 5.8.2.0.0 Description

A key-value store for system-wide settings, such as timeouts and feature flags. Values are cached in Redis to reduce database load.

### 5.8.3.0.0 Attributes

#### 5.8.3.1.0 VARCHAR

##### 5.8.3.1.1 Name

configurationKey

##### 5.8.3.1.2 Type

🔹 VARCHAR

##### 5.8.3.1.3 Is Required

✅ Yes

##### 5.8.3.1.4 Is Primary Key

✅ Yes

##### 5.8.3.1.5 Size

255

##### 5.8.3.1.6 Is Unique

✅ Yes

##### 5.8.3.1.7 Constraints

*No items available*

##### 5.8.3.1.8 Precision

0

##### 5.8.3.1.9 Scale

0

##### 5.8.3.1.10 Is Foreign Key

❌ No

#### 5.8.3.2.0 TEXT

##### 5.8.3.2.1 Name

configurationValue

##### 5.8.3.2.2 Type

🔹 TEXT

##### 5.8.3.2.3 Is Required

✅ Yes

##### 5.8.3.2.4 Is Primary Key

❌ No

##### 5.8.3.2.5 Size

0

##### 5.8.3.2.6 Is Unique

❌ No

##### 5.8.3.2.7 Constraints

*No items available*

##### 5.8.3.2.8 Precision

0

##### 5.8.3.2.9 Scale

0

##### 5.8.3.2.10 Is Foreign Key

❌ No

#### 5.8.3.3.0 TEXT

##### 5.8.3.3.1 Name

description

##### 5.8.3.3.2 Type

🔹 TEXT

##### 5.8.3.3.3 Is Required

❌ No

##### 5.8.3.3.4 Is Primary Key

❌ No

##### 5.8.3.3.5 Size

0

##### 5.8.3.3.6 Is Unique

❌ No

##### 5.8.3.3.7 Constraints

*No items available*

##### 5.8.3.3.8 Precision

0

##### 5.8.3.3.9 Scale

0

##### 5.8.3.3.10 Is Foreign Key

❌ No

#### 5.8.3.4.0 BOOLEAN

##### 5.8.3.4.1 Name

isSensitive

##### 5.8.3.4.2 Type

🔹 BOOLEAN

##### 5.8.3.4.3 Is Required

✅ Yes

##### 5.8.3.4.4 Is Primary Key

❌ No

##### 5.8.3.4.5 Size

0

##### 5.8.3.4.6 Is Unique

❌ No

##### 5.8.3.4.7 Constraints

- DEFAULT false

##### 5.8.3.4.8 Precision

0

##### 5.8.3.4.9 Scale

0

##### 5.8.3.4.10 Is Foreign Key

❌ No

#### 5.8.3.5.0 TIMESTAMPTZ

##### 5.8.3.5.1 Name

updatedAt

##### 5.8.3.5.2 Type

🔹 TIMESTAMPTZ

##### 5.8.3.5.3 Is Required

✅ Yes

##### 5.8.3.5.4 Is Primary Key

❌ No

##### 5.8.3.5.5 Size

0

##### 5.8.3.5.6 Is Unique

❌ No

##### 5.8.3.5.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.8.3.5.8 Precision

0

##### 5.8.3.5.9 Scale

0

##### 5.8.3.5.10 Is Foreign Key

❌ No

### 5.8.4.0.0 Primary Keys

- configurationKey

### 5.8.5.0.0 Unique Constraints

*No items available*

### 5.8.6.0.0 Indexes

*No items available*

