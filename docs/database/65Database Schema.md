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

Represents a system user or administrator who performs actions on scripts and reports.

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

*No items available*

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

fullName

##### 5.1.3.4.2 Type

🔹 VARCHAR

##### 5.1.3.4.3 Is Required

✅ Yes

##### 5.1.3.4.4 Is Primary Key

❌ No

##### 5.1.3.4.5 Size

200

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

passwordHash

##### 5.1.3.5.2 Type

🔹 VARCHAR

##### 5.1.3.5.3 Is Required

✅ Yes

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

isActive

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

- DEFAULT true

##### 5.1.3.6.8 Precision

0

##### 5.1.3.6.9 Scale

0

##### 5.1.3.6.10 Is Foreign Key

❌ No

#### 5.1.3.7.0 BOOLEAN

##### 5.1.3.7.1 Name

isDeleted

##### 5.1.3.7.2 Type

🔹 BOOLEAN

##### 5.1.3.7.3 Is Required

✅ Yes

##### 5.1.3.7.4 Is Primary Key

❌ No

##### 5.1.3.7.5 Size

0

##### 5.1.3.7.6 Is Unique

❌ No

##### 5.1.3.7.7 Constraints

- DEFAULT false

##### 5.1.3.7.8 Precision

0

##### 5.1.3.7.9 Scale

0

##### 5.1.3.7.10 Is Foreign Key

❌ No

#### 5.1.3.8.0 DateTimeOffset

##### 5.1.3.8.1 Name

createdAt

##### 5.1.3.8.2 Type

🔹 DateTimeOffset

##### 5.1.3.8.3 Is Required

✅ Yes

##### 5.1.3.8.4 Is Primary Key

❌ No

##### 5.1.3.8.5 Size

0

##### 5.1.3.8.6 Is Unique

❌ No

##### 5.1.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.8.8 Precision

0

##### 5.1.3.8.9 Scale

0

##### 5.1.3.8.10 Is Foreign Key

❌ No

#### 5.1.3.9.0 DateTimeOffset

##### 5.1.3.9.1 Name

updatedAt

##### 5.1.3.9.2 Type

🔹 DateTimeOffset

##### 5.1.3.9.3 Is Required

✅ Yes

##### 5.1.3.9.4 Is Primary Key

❌ No

##### 5.1.3.9.5 Size

0

##### 5.1.3.9.6 Is Unique

❌ No

##### 5.1.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.9.8 Precision

0

##### 5.1.3.9.9 Scale

0

##### 5.1.3.9.10 Is Foreign Key

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

- {'name': 'idx_user_active_not_deleted', 'columns': ['isActive', 'isDeleted'], 'type': 'BTree'}

## 5.2.0.0.0 TransformationScript

### 5.2.1.0.0 Name

TransformationScript

### 5.2.2.0.0 Description

Represents a user-defined transformation script. Each script has a history of versions, supporting REQ-DTR-007.

### 5.2.3.0.0 Attributes

#### 5.2.3.1.0 UUID

##### 5.2.3.1.1 Name

transformationScriptId

##### 5.2.3.1.2 Type

🔹 UUID

##### 5.2.3.1.3 Is Required

✅ Yes

##### 5.2.3.1.4 Is Primary Key

✅ Yes

##### 5.2.3.1.5 Size

0

##### 5.2.3.1.6 Is Unique

✅ Yes

##### 5.2.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.2.3.1.8 Precision

0

##### 5.2.3.1.9 Scale

0

##### 5.2.3.1.10 Is Foreign Key

❌ No

#### 5.2.3.2.0 VARCHAR

##### 5.2.3.2.1 Name

name

##### 5.2.3.2.2 Type

🔹 VARCHAR

##### 5.2.3.2.3 Is Required

✅ Yes

##### 5.2.3.2.4 Is Primary Key

❌ No

##### 5.2.3.2.5 Size

255

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

#### 5.2.3.4.0 UUID

##### 5.2.3.4.1 Name

activeScriptVersionId

##### 5.2.3.4.2 Type

🔹 UUID

##### 5.2.3.4.3 Is Required

❌ No

##### 5.2.3.4.4 Is Primary Key

❌ No

##### 5.2.3.4.5 Size

0

##### 5.2.3.4.6 Is Unique

❌ No

##### 5.2.3.4.7 Constraints

*No items available*

##### 5.2.3.4.8 Precision

0

##### 5.2.3.4.9 Scale

0

##### 5.2.3.4.10 Is Foreign Key

✅ Yes

#### 5.2.3.5.0 INT

##### 5.2.3.5.1 Name

activeVersionNumber

##### 5.2.3.5.2 Type

🔹 INT

##### 5.2.3.5.3 Is Required

❌ No

##### 5.2.3.5.4 Is Primary Key

❌ No

##### 5.2.3.5.5 Size

0

##### 5.2.3.5.6 Is Unique

❌ No

##### 5.2.3.5.7 Constraints

- Denormalized field to avoid joins when listing scripts

##### 5.2.3.5.8 Precision

0

##### 5.2.3.5.9 Scale

0

##### 5.2.3.5.10 Is Foreign Key

❌ No

#### 5.2.3.6.0 UUID

##### 5.2.3.6.1 Name

createdByUserId

##### 5.2.3.6.2 Type

🔹 UUID

##### 5.2.3.6.3 Is Required

✅ Yes

##### 5.2.3.6.4 Is Primary Key

❌ No

##### 5.2.3.6.5 Size

0

##### 5.2.3.6.6 Is Unique

❌ No

##### 5.2.3.6.7 Constraints

*No items available*

##### 5.2.3.6.8 Precision

0

##### 5.2.3.6.9 Scale

0

##### 5.2.3.6.10 Is Foreign Key

✅ Yes

#### 5.2.3.7.0 UUID

##### 5.2.3.7.1 Name

updatedByUserId

##### 5.2.3.7.2 Type

🔹 UUID

##### 5.2.3.7.3 Is Required

✅ Yes

##### 5.2.3.7.4 Is Primary Key

❌ No

##### 5.2.3.7.5 Size

0

##### 5.2.3.7.6 Is Unique

❌ No

##### 5.2.3.7.7 Constraints

*No items available*

##### 5.2.3.7.8 Precision

0

##### 5.2.3.7.9 Scale

0

##### 5.2.3.7.10 Is Foreign Key

✅ Yes

#### 5.2.3.8.0 BOOLEAN

##### 5.2.3.8.1 Name

isDeleted

##### 5.2.3.8.2 Type

🔹 BOOLEAN

##### 5.2.3.8.3 Is Required

✅ Yes

##### 5.2.3.8.4 Is Primary Key

❌ No

##### 5.2.3.8.5 Size

0

##### 5.2.3.8.6 Is Unique

❌ No

##### 5.2.3.8.7 Constraints

- DEFAULT false

##### 5.2.3.8.8 Precision

0

##### 5.2.3.8.9 Scale

0

##### 5.2.3.8.10 Is Foreign Key

❌ No

#### 5.2.3.9.0 DateTimeOffset

##### 5.2.3.9.1 Name

createdAt

##### 5.2.3.9.2 Type

🔹 DateTimeOffset

##### 5.2.3.9.3 Is Required

✅ Yes

##### 5.2.3.9.4 Is Primary Key

❌ No

##### 5.2.3.9.5 Size

0

##### 5.2.3.9.6 Is Unique

❌ No

##### 5.2.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.2.3.9.8 Precision

0

##### 5.2.3.9.9 Scale

0

##### 5.2.3.9.10 Is Foreign Key

❌ No

#### 5.2.3.10.0 DateTimeOffset

##### 5.2.3.10.1 Name

updatedAt

##### 5.2.3.10.2 Type

🔹 DateTimeOffset

##### 5.2.3.10.3 Is Required

✅ Yes

##### 5.2.3.10.4 Is Primary Key

❌ No

##### 5.2.3.10.5 Size

0

##### 5.2.3.10.6 Is Unique

❌ No

##### 5.2.3.10.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.2.3.10.8 Precision

0

##### 5.2.3.10.9 Scale

0

##### 5.2.3.10.10 Is Foreign Key

❌ No

### 5.2.4.0.0 Primary Keys

- transformationScriptId

### 5.2.5.0.0 Unique Constraints

- {'name': 'uq_transformation_script_name', 'columns': ['name']}

### 5.2.6.0.0 Indexes

#### 5.2.6.1.0 BTree

##### 5.2.6.1.1 Name

idx_transformationscript_activescriptversionid

##### 5.2.6.1.2 Columns

- activeScriptVersionId

##### 5.2.6.1.3 Type

🔹 BTree

#### 5.2.6.2.0 BTree

##### 5.2.6.2.1 Name

idx_transformationscript_isdeleted

##### 5.2.6.2.2 Columns

- isDeleted

##### 5.2.6.2.3 Type

🔹 BTree

## 5.3.0.0.0 ScriptVersion

### 5.3.1.0.0 Name

ScriptVersion

### 5.3.2.0.0 Description

Stores an immutable version of a script's content. A new version is created on each update, satisfying REQ-DTR-008. Content is encrypted at rest (REQ-DTR-010).

### 5.3.3.0.0 Attributes

#### 5.3.3.1.0 UUID

##### 5.3.3.1.1 Name

scriptVersionId

##### 5.3.3.1.2 Type

🔹 UUID

##### 5.3.3.1.3 Is Required

✅ Yes

##### 5.3.3.1.4 Is Primary Key

✅ Yes

##### 5.3.3.1.5 Size

0

##### 5.3.3.1.6 Is Unique

✅ Yes

##### 5.3.3.1.7 Constraints

- DEFAULT gen_random_uuid()

##### 5.3.3.1.8 Precision

0

##### 5.3.3.1.9 Scale

0

##### 5.3.3.1.10 Is Foreign Key

❌ No

#### 5.3.3.2.0 UUID

##### 5.3.3.2.1 Name

transformationScriptId

##### 5.3.3.2.2 Type

🔹 UUID

##### 5.3.3.2.3 Is Required

✅ Yes

##### 5.3.3.2.4 Is Primary Key

❌ No

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

#### 5.3.3.3.0 INT

##### 5.3.3.3.1 Name

versionNumber

##### 5.3.3.3.2 Type

🔹 INT

##### 5.3.3.3.3 Is Required

✅ Yes

##### 5.3.3.3.4 Is Primary Key

❌ No

##### 5.3.3.3.5 Size

0

##### 5.3.3.3.6 Is Unique

❌ No

##### 5.3.3.3.7 Constraints

*No items available*

##### 5.3.3.3.8 Precision

0

##### 5.3.3.3.9 Scale

0

##### 5.3.3.3.10 Is Foreign Key

❌ No

#### 5.3.3.4.0 BYTEA

##### 5.3.3.4.1 Name

scriptContentEncrypted

##### 5.3.3.4.2 Type

🔹 BYTEA

##### 5.3.3.4.3 Is Required

✅ Yes

##### 5.3.3.4.4 Is Primary Key

❌ No

##### 5.3.3.4.5 Size

0

##### 5.3.3.4.6 Is Unique

❌ No

##### 5.3.3.4.7 Constraints

- Ideal candidate for caching (e.g., Redis) to reduce DB load and decryption overhead.

##### 5.3.3.4.8 Precision

0

##### 5.3.3.4.9 Scale

0

##### 5.3.3.4.10 Is Foreign Key

❌ No

#### 5.3.3.5.0 TEXT

##### 5.3.3.5.1 Name

changeLog

##### 5.3.3.5.2 Type

🔹 TEXT

##### 5.3.3.5.3 Is Required

❌ No

##### 5.3.3.5.4 Is Primary Key

❌ No

##### 5.3.3.5.5 Size

0

##### 5.3.3.5.6 Is Unique

❌ No

##### 5.3.3.5.7 Constraints

*No items available*

##### 5.3.3.5.8 Precision

0

##### 5.3.3.5.9 Scale

0

##### 5.3.3.5.10 Is Foreign Key

❌ No

#### 5.3.3.6.0 UUID

##### 5.3.3.6.1 Name

createdByUserId

##### 5.3.3.6.2 Type

🔹 UUID

##### 5.3.3.6.3 Is Required

✅ Yes

##### 5.3.3.6.4 Is Primary Key

❌ No

##### 5.3.3.6.5 Size

0

##### 5.3.3.6.6 Is Unique

❌ No

##### 5.3.3.6.7 Constraints

*No items available*

##### 5.3.3.6.8 Precision

0

##### 5.3.3.6.9 Scale

0

##### 5.3.3.6.10 Is Foreign Key

✅ Yes

#### 5.3.3.7.0 DateTimeOffset

##### 5.3.3.7.1 Name

createdAt

##### 5.3.3.7.2 Type

🔹 DateTimeOffset

##### 5.3.3.7.3 Is Required

✅ Yes

##### 5.3.3.7.4 Is Primary Key

❌ No

##### 5.3.3.7.5 Size

0

##### 5.3.3.7.6 Is Unique

❌ No

##### 5.3.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.7.8 Precision

0

##### 5.3.3.7.9 Scale

0

##### 5.3.3.7.10 Is Foreign Key

❌ No

### 5.3.4.0.0 Primary Keys

- scriptVersionId

### 5.3.5.0.0 Unique Constraints

- {'name': 'uq_scriptversion_script_version', 'columns': ['transformationScriptId', 'versionNumber']}

### 5.3.6.0.0 Indexes

- {'name': 'idx_scriptversion_scriptid_version_desc', 'columns': ['transformationScriptId', 'versionNumber DESC'], 'type': 'BTree'}

## 5.4.0.0.0 ReportConfiguration

### 5.4.1.0.0 Name

ReportConfiguration

### 5.4.2.0.0 Description

Represents a report definition. Can be associated with a transformation script and a JSON Schema.

### 5.4.3.0.0 Attributes

#### 5.4.3.1.0 UUID

##### 5.4.3.1.1 Name

reportConfigurationId

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

❌ No

##### 5.4.3.2.7 Constraints

*No items available*

##### 5.4.3.2.8 Precision

0

##### 5.4.3.2.9 Scale

0

##### 5.4.3.2.10 Is Foreign Key

❌ No

#### 5.4.3.3.0 UUID

##### 5.4.3.3.1 Name

jsonSchemaId

##### 5.4.3.3.2 Type

🔹 UUID

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

✅ Yes

#### 5.4.3.4.0 UUID

##### 5.4.3.4.1 Name

createdByUserId

##### 5.4.3.4.2 Type

🔹 UUID

##### 5.4.3.4.3 Is Required

✅ Yes

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

updatedByUserId

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

#### 5.4.3.6.0 DateTimeOffset

##### 5.4.3.6.1 Name

createdAt

##### 5.4.3.6.2 Type

🔹 DateTimeOffset

##### 5.4.3.6.3 Is Required

✅ Yes

##### 5.4.3.6.4 Is Primary Key

❌ No

##### 5.4.3.6.5 Size

0

##### 5.4.3.6.6 Is Unique

❌ No

##### 5.4.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.6.8 Precision

0

##### 5.4.3.6.9 Scale

0

##### 5.4.3.6.10 Is Foreign Key

❌ No

#### 5.4.3.7.0 DateTimeOffset

##### 5.4.3.7.1 Name

updatedAt

##### 5.4.3.7.2 Type

🔹 DateTimeOffset

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

### 5.4.4.0.0 Primary Keys

- reportConfigurationId

### 5.4.5.0.0 Unique Constraints

*No items available*

### 5.4.6.0.0 Indexes

- {'name': 'idx_reportconfiguration_name', 'columns': ['name'], 'type': 'BTree'}

## 5.5.0.0.0 ReportTransformationScriptAssociation

### 5.5.1.0.0 Name

ReportTransformationScriptAssociation

### 5.5.2.0.0 Description

Junction table for the many-to-many relationship between reports and scripts (REQ-DTR-007).

### 5.5.3.0.0 Attributes

#### 5.5.3.1.0 UUID

##### 5.5.3.1.1 Name

reportConfigurationId

##### 5.5.3.1.2 Type

🔹 UUID

##### 5.5.3.1.3 Is Required

✅ Yes

##### 5.5.3.1.4 Is Primary Key

✅ Yes

##### 5.5.3.1.5 Size

0

##### 5.5.3.1.6 Is Unique

❌ No

##### 5.5.3.1.7 Constraints

*No items available*

##### 5.5.3.1.8 Precision

0

##### 5.5.3.1.9 Scale

0

##### 5.5.3.1.10 Is Foreign Key

✅ Yes

#### 5.5.3.2.0 UUID

##### 5.5.3.2.1 Name

transformationScriptId

##### 5.5.3.2.2 Type

🔹 UUID

##### 5.5.3.2.3 Is Required

✅ Yes

##### 5.5.3.2.4 Is Primary Key

✅ Yes

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

#### 5.5.3.3.0 UUID

##### 5.5.3.3.1 Name

associatedByUserId

##### 5.5.3.3.2 Type

🔹 UUID

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

✅ Yes

#### 5.5.3.4.0 DateTimeOffset

##### 5.5.3.4.1 Name

associatedAt

##### 5.5.3.4.2 Type

🔹 DateTimeOffset

##### 5.5.3.4.3 Is Required

✅ Yes

##### 5.5.3.4.4 Is Primary Key

❌ No

##### 5.5.3.4.5 Size

0

##### 5.5.3.4.6 Is Unique

❌ No

##### 5.5.3.4.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.5.3.4.8 Precision

0

##### 5.5.3.4.9 Scale

0

##### 5.5.3.4.10 Is Foreign Key

❌ No

### 5.5.4.0.0 Primary Keys

- reportConfigurationId
- transformationScriptId

### 5.5.5.0.0 Unique Constraints

*No items available*

### 5.5.6.0.0 Indexes

- {'name': 'idx_rtsa_transformationscriptid', 'columns': ['transformationScriptId'], 'type': 'BTree'}

## 5.6.0.0.0 JsonSchema

### 5.6.1.0.0 Name

JsonSchema

### 5.6.2.0.0 Description

Stores JSON Schema definitions used to validate transformation script outputs (REQ-DTR-017).

### 5.6.3.0.0 Attributes

#### 5.6.3.1.0 UUID

##### 5.6.3.1.1 Name

jsonSchemaId

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

✅ Yes

##### 5.6.3.2.7 Constraints

*No items available*

##### 5.6.3.2.8 Precision

0

##### 5.6.3.2.9 Scale

0

##### 5.6.3.2.10 Is Foreign Key

❌ No

#### 5.6.3.3.0 JSONB

##### 5.6.3.3.1 Name

schemaDefinition

##### 5.6.3.3.2 Type

🔹 JSONB

##### 5.6.3.3.3 Is Required

✅ Yes

##### 5.6.3.3.4 Is Primary Key

❌ No

##### 5.6.3.3.5 Size

0

##### 5.6.3.3.6 Is Unique

❌ No

##### 5.6.3.3.7 Constraints

- Must be a valid JSON object. Ideal candidate for in-memory or distributed caching.

##### 5.6.3.3.8 Precision

0

##### 5.6.3.3.9 Scale

0

##### 5.6.3.3.10 Is Foreign Key

❌ No

#### 5.6.3.4.0 UUID

##### 5.6.3.4.1 Name

createdByUserId

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

#### 5.6.3.5.0 DateTimeOffset

##### 5.6.3.5.1 Name

createdAt

##### 5.6.3.5.2 Type

🔹 DateTimeOffset

##### 5.6.3.5.3 Is Required

✅ Yes

##### 5.6.3.5.4 Is Primary Key

❌ No

##### 5.6.3.5.5 Size

0

##### 5.6.3.5.6 Is Unique

❌ No

##### 5.6.3.5.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.5.8 Precision

0

##### 5.6.3.5.9 Scale

0

##### 5.6.3.5.10 Is Foreign Key

❌ No

#### 5.6.3.6.0 DateTimeOffset

##### 5.6.3.6.1 Name

updatedAt

##### 5.6.3.6.2 Type

🔹 DateTimeOffset

##### 5.6.3.6.3 Is Required

✅ Yes

##### 5.6.3.6.4 Is Primary Key

❌ No

##### 5.6.3.6.5 Size

0

##### 5.6.3.6.6 Is Unique

❌ No

##### 5.6.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.6.8 Precision

0

##### 5.6.3.6.9 Scale

0

##### 5.6.3.6.10 Is Foreign Key

❌ No

### 5.6.4.0.0 Primary Keys

- jsonSchemaId

### 5.6.5.0.0 Unique Constraints

- {'name': 'uq_jsonschema_name', 'columns': ['name']}

### 5.6.6.0.0 Indexes

*No items available*

