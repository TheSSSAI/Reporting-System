# 1 Entities

## 1.1 TransformationScript

### 1.1.1 Name

TransformationScript

### 1.1.2 Description

Stores the master record for a transformation script. Each script can have multiple versions, with one designated as the active version. Fulfills REQ-FUNC-DTR-004.

### 1.1.3 Attributes

#### 1.1.3.1 Guid

##### 1.1.3.1.1 Name

transformationScriptId

##### 1.1.3.1.2 Type

🔹 Guid

##### 1.1.3.1.3 Is Required

✅ Yes

##### 1.1.3.1.4 Is Primary Key

✅ Yes

##### 1.1.3.1.5 Is Unique

✅ Yes

##### 1.1.3.1.6 Index Type

UniqueIndex

##### 1.1.3.1.7 Size

0

##### 1.1.3.1.8 Constraints

*No items available*

##### 1.1.3.1.9 Default Value



##### 1.1.3.1.10 Is Foreign Key

❌ No

##### 1.1.3.1.11 Precision

0

##### 1.1.3.1.12 Scale

0

#### 1.1.3.2.0 VARCHAR

##### 1.1.3.2.1 Name

name

##### 1.1.3.2.2 Type

🔹 VARCHAR

##### 1.1.3.2.3 Is Required

✅ Yes

##### 1.1.3.2.4 Is Primary Key

❌ No

##### 1.1.3.2.5 Is Unique

✅ Yes

##### 1.1.3.2.6 Index Type

UniqueIndex

##### 1.1.3.2.7 Size

255

##### 1.1.3.2.8 Constraints

*No items available*

##### 1.1.3.2.9 Default Value



##### 1.1.3.2.10 Is Foreign Key

❌ No

##### 1.1.3.2.11 Precision

0

##### 1.1.3.2.12 Scale

0

#### 1.1.3.3.0 TEXT

##### 1.1.3.3.1 Name

description

##### 1.1.3.3.2 Type

🔹 TEXT

##### 1.1.3.3.3 Is Required

❌ No

##### 1.1.3.3.4 Is Primary Key

❌ No

##### 1.1.3.3.5 Is Unique

❌ No

##### 1.1.3.3.6 Index Type

None

##### 1.1.3.3.7 Size

0

##### 1.1.3.3.8 Constraints

*No items available*

##### 1.1.3.3.9 Default Value



##### 1.1.3.3.10 Is Foreign Key

❌ No

##### 1.1.3.3.11 Precision

0

##### 1.1.3.3.12 Scale

0

#### 1.1.3.4.0 Guid

##### 1.1.3.4.1 Name

activeVersionId

##### 1.1.3.4.2 Type

🔹 Guid

##### 1.1.3.4.3 Is Required

❌ No

##### 1.1.3.4.4 Is Primary Key

❌ No

##### 1.1.3.4.5 Is Unique

❌ No

##### 1.1.3.4.6 Index Type

Index

##### 1.1.3.4.7 Size

0

##### 1.1.3.4.8 Constraints

*No items available*

##### 1.1.3.4.9 Default Value



##### 1.1.3.4.10 Is Foreign Key

✅ Yes

##### 1.1.3.4.11 Precision

0

##### 1.1.3.4.12 Scale

0

#### 1.1.3.5.0 Guid

##### 1.1.3.5.1 Name

createdByUserId

##### 1.1.3.5.2 Type

🔹 Guid

##### 1.1.3.5.3 Is Required

✅ Yes

##### 1.1.3.5.4 Is Primary Key

❌ No

##### 1.1.3.5.5 Is Unique

❌ No

##### 1.1.3.5.6 Index Type

Index

##### 1.1.3.5.7 Size

0

##### 1.1.3.5.8 Constraints

*No items available*

##### 1.1.3.5.9 Default Value



##### 1.1.3.5.10 Is Foreign Key

✅ Yes

##### 1.1.3.5.11 Precision

0

##### 1.1.3.5.12 Scale

0

#### 1.1.3.6.0 Guid

##### 1.1.3.6.1 Name

updatedByUserId

##### 1.1.3.6.2 Type

🔹 Guid

##### 1.1.3.6.3 Is Required

✅ Yes

##### 1.1.3.6.4 Is Primary Key

❌ No

##### 1.1.3.6.5 Is Unique

❌ No

##### 1.1.3.6.6 Index Type

Index

##### 1.1.3.6.7 Size

0

##### 1.1.3.6.8 Constraints

*No items available*

##### 1.1.3.6.9 Default Value



##### 1.1.3.6.10 Is Foreign Key

✅ Yes

##### 1.1.3.6.11 Precision

0

##### 1.1.3.6.12 Scale

0

#### 1.1.3.7.0 DateTime

##### 1.1.3.7.1 Name

createdAt

##### 1.1.3.7.2 Type

🔹 DateTime

##### 1.1.3.7.3 Is Required

✅ Yes

##### 1.1.3.7.4 Is Primary Key

❌ No

##### 1.1.3.7.5 Is Unique

❌ No

##### 1.1.3.7.6 Index Type

Index

##### 1.1.3.7.7 Size

0

##### 1.1.3.7.8 Constraints

*No items available*

##### 1.1.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.7.10 Is Foreign Key

❌ No

##### 1.1.3.7.11 Precision

0

##### 1.1.3.7.12 Scale

0

#### 1.1.3.8.0 DateTime

##### 1.1.3.8.1 Name

updatedAt

##### 1.1.3.8.2 Type

🔹 DateTime

##### 1.1.3.8.3 Is Required

✅ Yes

##### 1.1.3.8.4 Is Primary Key

❌ No

##### 1.1.3.8.5 Is Unique

❌ No

##### 1.1.3.8.6 Index Type

None

##### 1.1.3.8.7 Size

0

##### 1.1.3.8.8 Constraints

*No items available*

##### 1.1.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.8.10 Is Foreign Key

❌ No

##### 1.1.3.8.11 Precision

0

##### 1.1.3.8.12 Scale

0

#### 1.1.3.9.0 BOOLEAN

##### 1.1.3.9.1 Name

isDeleted

##### 1.1.3.9.2 Type

🔹 BOOLEAN

##### 1.1.3.9.3 Is Required

✅ Yes

##### 1.1.3.9.4 Is Primary Key

❌ No

##### 1.1.3.9.5 Is Unique

❌ No

##### 1.1.3.9.6 Index Type

Index

##### 1.1.3.9.7 Size

0

##### 1.1.3.9.8 Constraints

*No items available*

##### 1.1.3.9.9 Default Value

false

##### 1.1.3.9.10 Is Foreign Key

❌ No

##### 1.1.3.9.11 Precision

0

##### 1.1.3.9.12 Scale

0

### 1.1.4.0.0 Primary Keys

- transformationScriptId

### 1.1.5.0.0 Unique Constraints

- {'name': 'UC_TransformationScript_Name', 'columns': ['name']}

### 1.1.6.0.0 Indexes

#### 1.1.6.1.0 BTree

##### 1.1.6.1.1 Name

IX_TransformationScript_ActiveVersion

##### 1.1.6.1.2 Columns

- activeVersionId

##### 1.1.6.1.3 Type

🔹 BTree

#### 1.1.6.2.0 BTree

##### 1.1.6.2.1 Name

IX_TransformationScript_CreatedBy

##### 1.1.6.2.2 Columns

- createdByUserId

##### 1.1.6.2.3 Type

🔹 BTree

#### 1.1.6.3.0 BTree

##### 1.1.6.3.1 Name

IX_TransformationScript_UpdatedBy

##### 1.1.6.3.2 Columns

- updatedByUserId

##### 1.1.6.3.3 Type

🔹 BTree

#### 1.1.6.4.0 BTree

##### 1.1.6.4.1 Name

IX_TransformationScript_Active

##### 1.1.6.4.2 Columns

- isDeleted
- name

##### 1.1.6.4.3 Type

🔹 BTree

## 1.2.0.0.0 TransformationScriptVersion

### 1.2.1.0.0 Name

TransformationScriptVersion

### 1.2.2.0.0 Description

Stores a specific, immutable version of a transformation script's content. Fulfills REQ-FUNC-DTR-005 and REQ-SEC-DTR-003.

### 1.2.3.0.0 Attributes

#### 1.2.3.1.0 Guid

##### 1.2.3.1.1 Name

transformationScriptVersionId

##### 1.2.3.1.2 Type

🔹 Guid

##### 1.2.3.1.3 Is Required

✅ Yes

##### 1.2.3.1.4 Is Primary Key

✅ Yes

##### 1.2.3.1.5 Is Unique

✅ Yes

##### 1.2.3.1.6 Index Type

UniqueIndex

##### 1.2.3.1.7 Size

0

##### 1.2.3.1.8 Constraints

*No items available*

##### 1.2.3.1.9 Default Value



##### 1.2.3.1.10 Is Foreign Key

❌ No

##### 1.2.3.1.11 Precision

0

##### 1.2.3.1.12 Scale

0

#### 1.2.3.2.0 Guid

##### 1.2.3.2.1 Name

transformationScriptId

##### 1.2.3.2.2 Type

🔹 Guid

##### 1.2.3.2.3 Is Required

✅ Yes

##### 1.2.3.2.4 Is Primary Key

❌ No

##### 1.2.3.2.5 Is Unique

❌ No

##### 1.2.3.2.6 Index Type

Index

##### 1.2.3.2.7 Size

0

##### 1.2.3.2.8 Constraints

*No items available*

##### 1.2.3.2.9 Default Value



##### 1.2.3.2.10 Is Foreign Key

✅ Yes

##### 1.2.3.2.11 Precision

0

##### 1.2.3.2.12 Scale

0

#### 1.2.3.3.0 INT

##### 1.2.3.3.1 Name

versionNumber

##### 1.2.3.3.2 Type

🔹 INT

##### 1.2.3.3.3 Is Required

✅ Yes

##### 1.2.3.3.4 Is Primary Key

❌ No

##### 1.2.3.3.5 Is Unique

❌ No

##### 1.2.3.3.6 Index Type

Index

##### 1.2.3.3.7 Size

0

##### 1.2.3.3.8 Constraints

*No items available*

##### 1.2.3.3.9 Default Value



##### 1.2.3.3.10 Is Foreign Key

❌ No

##### 1.2.3.3.11 Precision

0

##### 1.2.3.3.12 Scale

0

#### 1.2.3.4.0 TEXT

##### 1.2.3.4.1 Name

scriptContent

##### 1.2.3.4.2 Type

🔹 TEXT

##### 1.2.3.4.3 Is Required

✅ Yes

##### 1.2.3.4.4 Is Primary Key

❌ No

##### 1.2.3.4.5 Is Unique

❌ No

##### 1.2.3.4.6 Index Type

None

##### 1.2.3.4.7 Size

0

##### 1.2.3.4.8 Constraints

- ENCRYPTED_AT_REST

##### 1.2.3.4.9 Default Value



##### 1.2.3.4.10 Is Foreign Key

❌ No

##### 1.2.3.4.11 Precision

0

##### 1.2.3.4.12 Scale

0

#### 1.2.3.5.0 TEXT

##### 1.2.3.5.1 Name

changeNotes

##### 1.2.3.5.2 Type

🔹 TEXT

##### 1.2.3.5.3 Is Required

❌ No

##### 1.2.3.5.4 Is Primary Key

❌ No

##### 1.2.3.5.5 Is Unique

❌ No

##### 1.2.3.5.6 Index Type

None

##### 1.2.3.5.7 Size

0

##### 1.2.3.5.8 Constraints

*No items available*

##### 1.2.3.5.9 Default Value



##### 1.2.3.5.10 Is Foreign Key

❌ No

##### 1.2.3.5.11 Precision

0

##### 1.2.3.5.12 Scale

0

#### 1.2.3.6.0 Guid

##### 1.2.3.6.1 Name

createdByUserId

##### 1.2.3.6.2 Type

🔹 Guid

##### 1.2.3.6.3 Is Required

✅ Yes

##### 1.2.3.6.4 Is Primary Key

❌ No

##### 1.2.3.6.5 Is Unique

❌ No

##### 1.2.3.6.6 Index Type

Index

##### 1.2.3.6.7 Size

0

##### 1.2.3.6.8 Constraints

*No items available*

##### 1.2.3.6.9 Default Value



##### 1.2.3.6.10 Is Foreign Key

✅ Yes

##### 1.2.3.6.11 Precision

0

##### 1.2.3.6.12 Scale

0

#### 1.2.3.7.0 DateTime

##### 1.2.3.7.1 Name

createdAt

##### 1.2.3.7.2 Type

🔹 DateTime

##### 1.2.3.7.3 Is Required

✅ Yes

##### 1.2.3.7.4 Is Primary Key

❌ No

##### 1.2.3.7.5 Is Unique

❌ No

##### 1.2.3.7.6 Index Type

Index

##### 1.2.3.7.7 Size

0

##### 1.2.3.7.8 Constraints

*No items available*

##### 1.2.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.2.3.7.10 Is Foreign Key

❌ No

##### 1.2.3.7.11 Precision

0

##### 1.2.3.7.12 Scale

0

### 1.2.4.0.0 Primary Keys

- transformationScriptVersionId

### 1.2.5.0.0 Unique Constraints

- {'name': 'UC_Version_Script_Number', 'columns': ['transformationScriptId', 'versionNumber']}

### 1.2.6.0.0 Indexes

#### 1.2.6.1.0 BTree

##### 1.2.6.1.1 Name

IX_Version_ScriptId_CreatedAt

##### 1.2.6.1.2 Columns

- transformationScriptId
- createdAt

##### 1.2.6.1.3 Type

🔹 BTree

#### 1.2.6.2.0 BTree

##### 1.2.6.2.1 Name

IX_Version_CreatedBy

##### 1.2.6.2.2 Columns

- createdByUserId

##### 1.2.6.2.3 Type

🔹 BTree

### 1.2.7.0.0 Notes

- The unique constraint on (transformationScriptId, versionNumber) serves as an efficient index. Queries for a specific version should use both columns in the WHERE clause for optimal performance.

## 1.3.0.0.0 ReportConfiguration

### 1.3.1.0.0 Name

ReportConfiguration

### 1.3.2.0.0 Description

Defines a report, including its association with a data connector and an optional transformation script. Fulfills REQ-FUNC-DTR-006.

### 1.3.3.0.0 Attributes

#### 1.3.3.1.0 Guid

##### 1.3.3.1.1 Name

reportConfigurationId

##### 1.3.3.1.2 Type

🔹 Guid

##### 1.3.3.1.3 Is Required

✅ Yes

##### 1.3.3.1.4 Is Primary Key

✅ Yes

##### 1.3.3.1.5 Is Unique

✅ Yes

##### 1.3.3.1.6 Index Type

UniqueIndex

##### 1.3.3.1.7 Size

0

##### 1.3.3.1.8 Constraints

*No items available*

##### 1.3.3.1.9 Default Value



##### 1.3.3.1.10 Is Foreign Key

❌ No

##### 1.3.3.1.11 Precision

0

##### 1.3.3.1.12 Scale

0

#### 1.3.3.2.0 VARCHAR

##### 1.3.3.2.1 Name

name

##### 1.3.3.2.2 Type

🔹 VARCHAR

##### 1.3.3.2.3 Is Required

✅ Yes

##### 1.3.3.2.4 Is Primary Key

❌ No

##### 1.3.3.2.5 Is Unique

❌ No

##### 1.3.3.2.6 Index Type

Index

##### 1.3.3.2.7 Size

255

##### 1.3.3.2.8 Constraints

*No items available*

##### 1.3.3.2.9 Default Value



##### 1.3.3.2.10 Is Foreign Key

❌ No

##### 1.3.3.2.11 Precision

0

##### 1.3.3.2.12 Scale

0

#### 1.3.3.3.0 Guid

##### 1.3.3.3.1 Name

transformationScriptId

##### 1.3.3.3.2 Type

🔹 Guid

##### 1.3.3.3.3 Is Required

❌ No

##### 1.3.3.3.4 Is Primary Key

❌ No

##### 1.3.3.3.5 Is Unique

❌ No

##### 1.3.3.3.6 Index Type

Index

##### 1.3.3.3.7 Size

0

##### 1.3.3.3.8 Constraints

*No items available*

##### 1.3.3.3.9 Default Value



##### 1.3.3.3.10 Is Foreign Key

✅ Yes

##### 1.3.3.3.11 Precision

0

##### 1.3.3.3.12 Scale

0

#### 1.3.3.4.0 TEXT

##### 1.3.3.4.1 Name

outputJsonSchema

##### 1.3.3.4.2 Type

🔹 TEXT

##### 1.3.3.4.3 Is Required

❌ No

##### 1.3.3.4.4 Is Primary Key

❌ No

##### 1.3.3.4.5 Is Unique

❌ No

##### 1.3.3.4.6 Index Type

None

##### 1.3.3.4.7 Size

0

##### 1.3.3.4.8 Constraints

- MUST_BE_VALID_JSON_SCHEMA

##### 1.3.3.4.9 Default Value



##### 1.3.3.4.10 Is Foreign Key

❌ No

##### 1.3.3.4.11 Precision

0

##### 1.3.3.4.12 Scale

0

#### 1.3.3.5.0 Guid

##### 1.3.3.5.1 Name

createdByUserId

##### 1.3.3.5.2 Type

🔹 Guid

##### 1.3.3.5.3 Is Required

✅ Yes

##### 1.3.3.5.4 Is Primary Key

❌ No

##### 1.3.3.5.5 Is Unique

❌ No

##### 1.3.3.5.6 Index Type

Index

##### 1.3.3.5.7 Size

0

##### 1.3.3.5.8 Constraints

*No items available*

##### 1.3.3.5.9 Default Value



##### 1.3.3.5.10 Is Foreign Key

✅ Yes

##### 1.3.3.5.11 Precision

0

##### 1.3.3.5.12 Scale

0

#### 1.3.3.6.0 Guid

##### 1.3.3.6.1 Name

updatedByUserId

##### 1.3.3.6.2 Type

🔹 Guid

##### 1.3.3.6.3 Is Required

✅ Yes

##### 1.3.3.6.4 Is Primary Key

❌ No

##### 1.3.3.6.5 Is Unique

❌ No

##### 1.3.3.6.6 Index Type

Index

##### 1.3.3.6.7 Size

0

##### 1.3.3.6.8 Constraints

*No items available*

##### 1.3.3.6.9 Default Value



##### 1.3.3.6.10 Is Foreign Key

✅ Yes

##### 1.3.3.6.11 Precision

0

##### 1.3.3.6.12 Scale

0

#### 1.3.3.7.0 DateTime

##### 1.3.3.7.1 Name

createdAt

##### 1.3.3.7.2 Type

🔹 DateTime

##### 1.3.3.7.3 Is Required

✅ Yes

##### 1.3.3.7.4 Is Primary Key

❌ No

##### 1.3.3.7.5 Is Unique

❌ No

##### 1.3.3.7.6 Index Type

Index

##### 1.3.3.7.7 Size

0

##### 1.3.3.7.8 Constraints

*No items available*

##### 1.3.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.7.10 Is Foreign Key

❌ No

##### 1.3.3.7.11 Precision

0

##### 1.3.3.7.12 Scale

0

#### 1.3.3.8.0 DateTime

##### 1.3.3.8.1 Name

updatedAt

##### 1.3.3.8.2 Type

🔹 DateTime

##### 1.3.3.8.3 Is Required

✅ Yes

##### 1.3.3.8.4 Is Primary Key

❌ No

##### 1.3.3.8.5 Is Unique

❌ No

##### 1.3.3.8.6 Index Type

None

##### 1.3.3.8.7 Size

0

##### 1.3.3.8.8 Constraints

*No items available*

##### 1.3.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.8.10 Is Foreign Key

❌ No

##### 1.3.3.8.11 Precision

0

##### 1.3.3.8.12 Scale

0

#### 1.3.3.9.0 BOOLEAN

##### 1.3.3.9.1 Name

isDeleted

##### 1.3.3.9.2 Type

🔹 BOOLEAN

##### 1.3.3.9.3 Is Required

✅ Yes

##### 1.3.3.9.4 Is Primary Key

❌ No

##### 1.3.3.9.5 Is Unique

❌ No

##### 1.3.3.9.6 Index Type

Index

##### 1.3.3.9.7 Size

0

##### 1.3.3.9.8 Constraints

*No items available*

##### 1.3.3.9.9 Default Value

false

##### 1.3.3.9.10 Is Foreign Key

❌ No

##### 1.3.3.9.11 Precision

0

##### 1.3.3.9.12 Scale

0

### 1.3.4.0.0 Primary Keys

- reportConfigurationId

### 1.3.5.0.0 Unique Constraints

*No items available*

### 1.3.6.0.0 Indexes

#### 1.3.6.1.0 BTree

##### 1.3.6.1.1 Name

IX_ReportConfig_TransformationScript

##### 1.3.6.1.2 Columns

- transformationScriptId

##### 1.3.6.1.3 Type

🔹 BTree

#### 1.3.6.2.0 BTree

##### 1.3.6.2.1 Name

IX_ReportConfig_Active

##### 1.3.6.2.2 Columns

- isDeleted
- name

##### 1.3.6.2.3 Type

🔹 BTree

## 1.4.0.0.0 ReportJob

### 1.4.1.0.0 Name

ReportJob

### 1.4.2.0.0 Description

Represents a single execution of a report generation task, tracking its status and outcome. Fulfills REQ-REL-DTR-001.

### 1.4.3.0.0 Attributes

#### 1.4.3.1.0 Guid

##### 1.4.3.1.1 Name

reportJobId

##### 1.4.3.1.2 Type

🔹 Guid

##### 1.4.3.1.3 Is Required

✅ Yes

##### 1.4.3.1.4 Is Primary Key

✅ Yes

##### 1.4.3.1.5 Is Unique

✅ Yes

##### 1.4.3.1.6 Index Type

UniqueIndex

##### 1.4.3.1.7 Size

0

##### 1.4.3.1.8 Constraints

*No items available*

##### 1.4.3.1.9 Default Value



##### 1.4.3.1.10 Is Foreign Key

❌ No

##### 1.4.3.1.11 Precision

0

##### 1.4.3.1.12 Scale

0

#### 1.4.3.2.0 Guid

##### 1.4.3.2.1 Name

reportConfigurationId

##### 1.4.3.2.2 Type

🔹 Guid

##### 1.4.3.2.3 Is Required

✅ Yes

##### 1.4.3.2.4 Is Primary Key

❌ No

##### 1.4.3.2.5 Is Unique

❌ No

##### 1.4.3.2.6 Index Type

Index

##### 1.4.3.2.7 Size

0

##### 1.4.3.2.8 Constraints

*No items available*

##### 1.4.3.2.9 Default Value



##### 1.4.3.2.10 Is Foreign Key

✅ Yes

##### 1.4.3.2.11 Precision

0

##### 1.4.3.2.12 Scale

0

#### 1.4.3.3.0 Guid

##### 1.4.3.3.1 Name

transformationScriptVersionId

##### 1.4.3.3.2 Type

🔹 Guid

##### 1.4.3.3.3 Is Required

❌ No

##### 1.4.3.3.4 Is Primary Key

❌ No

##### 1.4.3.3.5 Is Unique

❌ No

##### 1.4.3.3.6 Index Type

Index

##### 1.4.3.3.7 Size

0

##### 1.4.3.3.8 Constraints

*No items available*

##### 1.4.3.3.9 Default Value



##### 1.4.3.3.10 Is Foreign Key

✅ Yes

##### 1.4.3.3.11 Precision

0

##### 1.4.3.3.12 Scale

0

#### 1.4.3.4.0 VARCHAR

##### 1.4.3.4.1 Name

reportConfigurationName

##### 1.4.3.4.2 Type

🔹 VARCHAR

##### 1.4.3.4.3 Is Required

❌ No

##### 1.4.3.4.4 Is Primary Key

❌ No

##### 1.4.3.4.5 Is Unique

❌ No

##### 1.4.3.4.6 Index Type

None

##### 1.4.3.4.7 Size

255

##### 1.4.3.4.8 Constraints

*No items available*

##### 1.4.3.4.9 Default Value



##### 1.4.3.4.10 Is Foreign Key

❌ No

##### 1.4.3.4.11 Precision

0

##### 1.4.3.4.12 Scale

0

##### 1.4.3.4.13 Notes

- Denormalized for historical reporting and UI performance.

#### 1.4.3.5.0 VARCHAR

##### 1.4.3.5.1 Name

transformationScriptName

##### 1.4.3.5.2 Type

🔹 VARCHAR

##### 1.4.3.5.3 Is Required

❌ No

##### 1.4.3.5.4 Is Primary Key

❌ No

##### 1.4.3.5.5 Is Unique

❌ No

##### 1.4.3.5.6 Index Type

None

##### 1.4.3.5.7 Size

255

##### 1.4.3.5.8 Constraints

*No items available*

##### 1.4.3.5.9 Default Value



##### 1.4.3.5.10 Is Foreign Key

❌ No

##### 1.4.3.5.11 Precision

0

##### 1.4.3.5.12 Scale

0

##### 1.4.3.5.13 Notes

- Denormalized for historical reporting and UI performance.

#### 1.4.3.6.0 VARCHAR

##### 1.4.3.6.1 Name

status

##### 1.4.3.6.2 Type

🔹 VARCHAR

##### 1.4.3.6.3 Is Required

✅ Yes

##### 1.4.3.6.4 Is Primary Key

❌ No

##### 1.4.3.6.5 Is Unique

❌ No

##### 1.4.3.6.6 Index Type

Index

##### 1.4.3.6.7 Size

50

##### 1.4.3.6.8 Constraints

- CHECK_CONSTRAINT_IN ('Queued', 'Running', 'Completed', 'Failed', 'Cancelled')

##### 1.4.3.6.9 Default Value

'Queued'

##### 1.4.3.6.10 Is Foreign Key

❌ No

##### 1.4.3.6.11 Precision

0

##### 1.4.3.6.12 Scale

0

#### 1.4.3.7.0 TEXT

##### 1.4.3.7.1 Name

statusMessage

##### 1.4.3.7.2 Type

🔹 TEXT

##### 1.4.3.7.3 Is Required

❌ No

##### 1.4.3.7.4 Is Primary Key

❌ No

##### 1.4.3.7.5 Is Unique

❌ No

##### 1.4.3.7.6 Index Type

None

##### 1.4.3.7.7 Size

0

##### 1.4.3.7.8 Constraints

*No items available*

##### 1.4.3.7.9 Default Value



##### 1.4.3.7.10 Is Foreign Key

❌ No

##### 1.4.3.7.11 Precision

0

##### 1.4.3.7.12 Scale

0

#### 1.4.3.8.0 DateTime

##### 1.4.3.8.1 Name

queuedAt

##### 1.4.3.8.2 Type

🔹 DateTime

##### 1.4.3.8.3 Is Required

✅ Yes

##### 1.4.3.8.4 Is Primary Key

❌ No

##### 1.4.3.8.5 Is Unique

❌ No

##### 1.4.3.8.6 Index Type

Index

##### 1.4.3.8.7 Size

0

##### 1.4.3.8.8 Constraints

*No items available*

##### 1.4.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.8.10 Is Foreign Key

❌ No

##### 1.4.3.8.11 Precision

0

##### 1.4.3.8.12 Scale

0

#### 1.4.3.9.0 DateTime

##### 1.4.3.9.1 Name

startedAt

##### 1.4.3.9.2 Type

🔹 DateTime

##### 1.4.3.9.3 Is Required

❌ No

##### 1.4.3.9.4 Is Primary Key

❌ No

##### 1.4.3.9.5 Is Unique

❌ No

##### 1.4.3.9.6 Index Type

Index

##### 1.4.3.9.7 Size

0

##### 1.4.3.9.8 Constraints

*No items available*

##### 1.4.3.9.9 Default Value



##### 1.4.3.9.10 Is Foreign Key

❌ No

##### 1.4.3.9.11 Precision

0

##### 1.4.3.9.12 Scale

0

#### 1.4.3.10.0 DateTime

##### 1.4.3.10.1 Name

completedAt

##### 1.4.3.10.2 Type

🔹 DateTime

##### 1.4.3.10.3 Is Required

❌ No

##### 1.4.3.10.4 Is Primary Key

❌ No

##### 1.4.3.10.5 Is Unique

❌ No

##### 1.4.3.10.6 Index Type

Index

##### 1.4.3.10.7 Size

0

##### 1.4.3.10.8 Constraints

*No items available*

##### 1.4.3.10.9 Default Value



##### 1.4.3.10.10 Is Foreign Key

❌ No

##### 1.4.3.10.11 Precision

0

##### 1.4.3.10.12 Scale

0

### 1.4.4.0.0 Primary Keys

- reportJobId

### 1.4.5.0.0 Unique Constraints

*No items available*

### 1.4.6.0.0 Indexes

#### 1.4.6.1.0 BTree

##### 1.4.6.1.1 Name

IX_ReportJob_Status_QueuedAt

##### 1.4.6.1.2 Columns

- status
- queuedAt

##### 1.4.6.1.3 Type

🔹 BTree

#### 1.4.6.2.0 BTree

##### 1.4.6.2.1 Name

IX_ReportJob_ReportConfig_CompletedAt

##### 1.4.6.2.2 Columns

- reportConfigurationId
- completedAt

##### 1.4.6.2.3 Type

🔹 BTree

#### 1.4.6.3.0 BTree

##### 1.4.6.3.1 Name

IX_ReportJob_Covering_Status_Completed_Config

##### 1.4.6.3.2 Columns

- status
- completedAt
- reportConfigurationId

##### 1.4.6.3.3 Type

🔹 BTree

### 1.4.7.0.0 Partitioning

| Property | Value |
|----------|-------|
| Strategy | Range |
| Column | completedAt |
| Interval | Monthly |

## 1.5.0.0.0 AuditLog

### 1.5.1.0.0 Name

AuditLog

### 1.5.2.0.0 Description

Stores an immutable record of security-relevant events, such as script modifications and sandbox violations. Fulfills REQ-SEC-DTR-002, REQ-SEC-DTR-004, REQ-COMP-DTR-001.

### 1.5.3.0.0 Attributes

#### 1.5.3.1.0 BIGINT

##### 1.5.3.1.1 Name

auditLogId

##### 1.5.3.1.2 Type

🔹 BIGINT

##### 1.5.3.1.3 Is Required

✅ Yes

##### 1.5.3.1.4 Is Primary Key

✅ Yes

##### 1.5.3.1.5 Is Unique

✅ Yes

##### 1.5.3.1.6 Index Type

UniqueIndex

##### 1.5.3.1.7 Size

0

##### 1.5.3.1.8 Constraints

- AUTO_INCREMENT

##### 1.5.3.1.9 Default Value



##### 1.5.3.1.10 Is Foreign Key

❌ No

##### 1.5.3.1.11 Precision

0

##### 1.5.3.1.12 Scale

0

#### 1.5.3.2.0 DateTimeOffset

##### 1.5.3.2.1 Name

timestamp

##### 1.5.3.2.2 Type

🔹 DateTimeOffset

##### 1.5.3.2.3 Is Required

✅ Yes

##### 1.5.3.2.4 Is Primary Key

❌ No

##### 1.5.3.2.5 Is Unique

❌ No

##### 1.5.3.2.6 Index Type

Index

##### 1.5.3.2.7 Size

0

##### 1.5.3.2.8 Constraints

*No items available*

##### 1.5.3.2.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.2.10 Is Foreign Key

❌ No

##### 1.5.3.2.11 Precision

0

##### 1.5.3.2.12 Scale

0

#### 1.5.3.3.0 Guid

##### 1.5.3.3.1 Name

userId

##### 1.5.3.3.2 Type

🔹 Guid

##### 1.5.3.3.3 Is Required

❌ No

##### 1.5.3.3.4 Is Primary Key

❌ No

##### 1.5.3.3.5 Is Unique

❌ No

##### 1.5.3.3.6 Index Type

Index

##### 1.5.3.3.7 Size

0

##### 1.5.3.3.8 Constraints

*No items available*

##### 1.5.3.3.9 Default Value



##### 1.5.3.3.10 Is Foreign Key

✅ Yes

##### 1.5.3.3.11 Precision

0

##### 1.5.3.3.12 Scale

0

#### 1.5.3.4.0 VARCHAR

##### 1.5.3.4.1 Name

eventType

##### 1.5.3.4.2 Type

🔹 VARCHAR

##### 1.5.3.4.3 Is Required

✅ Yes

##### 1.5.3.4.4 Is Primary Key

❌ No

##### 1.5.3.4.5 Is Unique

❌ No

##### 1.5.3.4.6 Index Type

Index

##### 1.5.3.4.7 Size

100

##### 1.5.3.4.8 Constraints

*No items available*

##### 1.5.3.4.9 Default Value



##### 1.5.3.4.10 Is Foreign Key

❌ No

##### 1.5.3.4.11 Precision

0

##### 1.5.3.4.12 Scale

0

#### 1.5.3.5.0 VARCHAR

##### 1.5.3.5.1 Name

entityType

##### 1.5.3.5.2 Type

🔹 VARCHAR

##### 1.5.3.5.3 Is Required

❌ No

##### 1.5.3.5.4 Is Primary Key

❌ No

##### 1.5.3.5.5 Is Unique

❌ No

##### 1.5.3.5.6 Index Type

Index

##### 1.5.3.5.7 Size

100

##### 1.5.3.5.8 Constraints

*No items available*

##### 1.5.3.5.9 Default Value



##### 1.5.3.5.10 Is Foreign Key

❌ No

##### 1.5.3.5.11 Precision

0

##### 1.5.3.5.12 Scale

0

#### 1.5.3.6.0 VARCHAR

##### 1.5.3.6.1 Name

entityId

##### 1.5.3.6.2 Type

🔹 VARCHAR

##### 1.5.3.6.3 Is Required

❌ No

##### 1.5.3.6.4 Is Primary Key

❌ No

##### 1.5.3.6.5 Is Unique

❌ No

##### 1.5.3.6.6 Index Type

Index

##### 1.5.3.6.7 Size

255

##### 1.5.3.6.8 Constraints

*No items available*

##### 1.5.3.6.9 Default Value



##### 1.5.3.6.10 Is Foreign Key

❌ No

##### 1.5.3.6.11 Precision

0

##### 1.5.3.6.12 Scale

0

#### 1.5.3.7.0 JSONB

##### 1.5.3.7.1 Name

details

##### 1.5.3.7.2 Type

🔹 JSONB

##### 1.5.3.7.3 Is Required

✅ Yes

##### 1.5.3.7.4 Is Primary Key

❌ No

##### 1.5.3.7.5 Is Unique

❌ No

##### 1.5.3.7.6 Index Type

None

##### 1.5.3.7.7 Size

0

##### 1.5.3.7.8 Constraints

*No items available*

##### 1.5.3.7.9 Default Value

{}

##### 1.5.3.7.10 Is Foreign Key

❌ No

##### 1.5.3.7.11 Precision

0

##### 1.5.3.7.12 Scale

0

### 1.5.4.0.0 Primary Keys

- auditLogId

### 1.5.5.0.0 Unique Constraints

*No items available*

### 1.5.6.0.0 Indexes

#### 1.5.6.1.0 BTree

##### 1.5.6.1.1 Name

IX_AuditLog_Timestamp

##### 1.5.6.1.2 Columns

- timestamp

##### 1.5.6.1.3 Type

🔹 BTree

#### 1.5.6.2.0 BTree

##### 1.5.6.2.1 Name

IX_AuditLog_User

##### 1.5.6.2.2 Columns

- userId

##### 1.5.6.2.3 Type

🔹 BTree

#### 1.5.6.3.0 BTree

##### 1.5.6.3.1 Name

IX_AuditLog_EventType

##### 1.5.6.3.2 Columns

- eventType

##### 1.5.6.3.3 Type

🔹 BTree

#### 1.5.6.4.0 BTree

##### 1.5.6.4.1 Name

IX_AuditLog_Entity

##### 1.5.6.4.2 Columns

- entityType
- entityId

##### 1.5.6.4.3 Type

🔹 BTree

#### 1.5.6.5.0 GIN

##### 1.5.6.5.1 Name

IX_AuditLog_Details_GIN

##### 1.5.6.5.2 Columns

- details

##### 1.5.6.5.3 Type

🔹 GIN

### 1.5.7.0.0 Partitioning

| Property | Value |
|----------|-------|
| Strategy | Range |
| Column | timestamp |
| Interval | Monthly |

## 1.6.0.0.0 User

### 1.6.1.0.0 Name

User

### 1.6.2.0.0 Description

Represents a system user with credentials and role associations for RBAC.

### 1.6.3.0.0 Attributes

#### 1.6.3.1.0 Guid

##### 1.6.3.1.1 Name

userId

##### 1.6.3.1.2 Type

🔹 Guid

##### 1.6.3.1.3 Is Required

✅ Yes

##### 1.6.3.1.4 Is Primary Key

✅ Yes

##### 1.6.3.1.5 Is Unique

✅ Yes

##### 1.6.3.1.6 Index Type

UniqueIndex

##### 1.6.3.1.7 Size

0

##### 1.6.3.1.8 Constraints

*No items available*

##### 1.6.3.1.9 Default Value



##### 1.6.3.1.10 Is Foreign Key

❌ No

##### 1.6.3.1.11 Precision

0

##### 1.6.3.1.12 Scale

0

#### 1.6.3.2.0 VARCHAR

##### 1.6.3.2.1 Name

username

##### 1.6.3.2.2 Type

🔹 VARCHAR

##### 1.6.3.2.3 Is Required

✅ Yes

##### 1.6.3.2.4 Is Primary Key

❌ No

##### 1.6.3.2.5 Is Unique

✅ Yes

##### 1.6.3.2.6 Index Type

UniqueIndex

##### 1.6.3.2.7 Size

100

##### 1.6.3.2.8 Constraints

*No items available*

##### 1.6.3.2.9 Default Value



##### 1.6.3.2.10 Is Foreign Key

❌ No

##### 1.6.3.2.11 Precision

0

##### 1.6.3.2.12 Scale

0

#### 1.6.3.3.0 VARCHAR

##### 1.6.3.3.1 Name

email

##### 1.6.3.3.2 Type

🔹 VARCHAR

##### 1.6.3.3.3 Is Required

✅ Yes

##### 1.6.3.3.4 Is Primary Key

❌ No

##### 1.6.3.3.5 Is Unique

✅ Yes

##### 1.6.3.3.6 Index Type

UniqueIndex

##### 1.6.3.3.7 Size

255

##### 1.6.3.3.8 Constraints

*No items available*

##### 1.6.3.3.9 Default Value



##### 1.6.3.3.10 Is Foreign Key

❌ No

##### 1.6.3.3.11 Precision

0

##### 1.6.3.3.12 Scale

0

#### 1.6.3.4.0 VARCHAR

##### 1.6.3.4.1 Name

passwordHash

##### 1.6.3.4.2 Type

🔹 VARCHAR

##### 1.6.3.4.3 Is Required

✅ Yes

##### 1.6.3.4.4 Is Primary Key

❌ No

##### 1.6.3.4.5 Is Unique

❌ No

##### 1.6.3.4.6 Index Type

None

##### 1.6.3.4.7 Size

255

##### 1.6.3.4.8 Constraints

*No items available*

##### 1.6.3.4.9 Default Value



##### 1.6.3.4.10 Is Foreign Key

❌ No

##### 1.6.3.4.11 Precision

0

##### 1.6.3.4.12 Scale

0

#### 1.6.3.5.0 BOOLEAN

##### 1.6.3.5.1 Name

isActive

##### 1.6.3.5.2 Type

🔹 BOOLEAN

##### 1.6.3.5.3 Is Required

✅ Yes

##### 1.6.3.5.4 Is Primary Key

❌ No

##### 1.6.3.5.5 Is Unique

❌ No

##### 1.6.3.5.6 Index Type

Index

##### 1.6.3.5.7 Size

0

##### 1.6.3.5.8 Constraints

*No items available*

##### 1.6.3.5.9 Default Value

true

##### 1.6.3.5.10 Is Foreign Key

❌ No

##### 1.6.3.5.11 Precision

0

##### 1.6.3.5.12 Scale

0

#### 1.6.3.6.0 DateTime

##### 1.6.3.6.1 Name

createdAt

##### 1.6.3.6.2 Type

🔹 DateTime

##### 1.6.3.6.3 Is Required

✅ Yes

##### 1.6.3.6.4 Is Primary Key

❌ No

##### 1.6.3.6.5 Is Unique

❌ No

##### 1.6.3.6.6 Index Type

Index

##### 1.6.3.6.7 Size

0

##### 1.6.3.6.8 Constraints

*No items available*

##### 1.6.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.6.3.6.10 Is Foreign Key

❌ No

##### 1.6.3.6.11 Precision

0

##### 1.6.3.6.12 Scale

0

#### 1.6.3.7.0 DateTime

##### 1.6.3.7.1 Name

updatedAt

##### 1.6.3.7.2 Type

🔹 DateTime

##### 1.6.3.7.3 Is Required

✅ Yes

##### 1.6.3.7.4 Is Primary Key

❌ No

##### 1.6.3.7.5 Is Unique

❌ No

##### 1.6.3.7.6 Index Type

None

##### 1.6.3.7.7 Size

0

##### 1.6.3.7.8 Constraints

*No items available*

##### 1.6.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.6.3.7.10 Is Foreign Key

❌ No

##### 1.6.3.7.11 Precision

0

##### 1.6.3.7.12 Scale

0

#### 1.6.3.8.0 BOOLEAN

##### 1.6.3.8.1 Name

isDeleted

##### 1.6.3.8.2 Type

🔹 BOOLEAN

##### 1.6.3.8.3 Is Required

✅ Yes

##### 1.6.3.8.4 Is Primary Key

❌ No

##### 1.6.3.8.5 Is Unique

❌ No

##### 1.6.3.8.6 Index Type

Index

##### 1.6.3.8.7 Size

0

##### 1.6.3.8.8 Constraints

*No items available*

##### 1.6.3.8.9 Default Value

false

##### 1.6.3.8.10 Is Foreign Key

❌ No

##### 1.6.3.8.11 Precision

0

##### 1.6.3.8.12 Scale

0

### 1.6.4.0.0 Primary Keys

- userId

### 1.6.5.0.0 Unique Constraints

#### 1.6.5.1.0 UC_User_Username

##### 1.6.5.1.1 Name

UC_User_Username

##### 1.6.5.1.2 Columns

- username

#### 1.6.5.2.0 UC_User_Email

##### 1.6.5.2.1 Name

UC_User_Email

##### 1.6.5.2.2 Columns

- email

### 1.6.6.0.0 Indexes

- {'name': 'IX_User_Active', 'columns': ['isActive', 'isDeleted'], 'type': 'BTree'}

## 1.7.0.0.0 Role

### 1.7.1.0.0 Name

Role

### 1.7.2.0.0 Description

Defines a role for RBAC, such as 'Administrator' or 'ScriptManager'.

### 1.7.3.0.0 Attributes

#### 1.7.3.1.0 INT

##### 1.7.3.1.1 Name

roleId

##### 1.7.3.1.2 Type

🔹 INT

##### 1.7.3.1.3 Is Required

✅ Yes

##### 1.7.3.1.4 Is Primary Key

✅ Yes

##### 1.7.3.1.5 Is Unique

✅ Yes

##### 1.7.3.1.6 Index Type

UniqueIndex

##### 1.7.3.1.7 Size

0

##### 1.7.3.1.8 Constraints

- AUTO_INCREMENT

##### 1.7.3.1.9 Default Value



##### 1.7.3.1.10 Is Foreign Key

❌ No

##### 1.7.3.1.11 Precision

0

##### 1.7.3.1.12 Scale

0

#### 1.7.3.2.0 VARCHAR

##### 1.7.3.2.1 Name

roleName

##### 1.7.3.2.2 Type

🔹 VARCHAR

##### 1.7.3.2.3 Is Required

✅ Yes

##### 1.7.3.2.4 Is Primary Key

❌ No

##### 1.7.3.2.5 Is Unique

✅ Yes

##### 1.7.3.2.6 Index Type

UniqueIndex

##### 1.7.3.2.7 Size

50

##### 1.7.3.2.8 Constraints

*No items available*

##### 1.7.3.2.9 Default Value



##### 1.7.3.2.10 Is Foreign Key

❌ No

##### 1.7.3.2.11 Precision

0

##### 1.7.3.2.12 Scale

0

#### 1.7.3.3.0 TEXT

##### 1.7.3.3.1 Name

description

##### 1.7.3.3.2 Type

🔹 TEXT

##### 1.7.3.3.3 Is Required

❌ No

##### 1.7.3.3.4 Is Primary Key

❌ No

##### 1.7.3.3.5 Is Unique

❌ No

##### 1.7.3.3.6 Index Type

None

##### 1.7.3.3.7 Size

0

##### 1.7.3.3.8 Constraints

*No items available*

##### 1.7.3.3.9 Default Value



##### 1.7.3.3.10 Is Foreign Key

❌ No

##### 1.7.3.3.11 Precision

0

##### 1.7.3.3.12 Scale

0

### 1.7.4.0.0 Primary Keys

- roleId

### 1.7.5.0.0 Unique Constraints

- {'name': 'UC_Role_RoleName', 'columns': ['roleName']}

### 1.7.6.0.0 Indexes

*No items available*

## 1.8.0.0.0 UserRole

### 1.8.1.0.0 Name

UserRole

### 1.8.2.0.0 Description

Junction table for the many-to-many relationship between Users and Roles.

### 1.8.3.0.0 Attributes

#### 1.8.3.1.0 Guid

##### 1.8.3.1.1 Name

userId

##### 1.8.3.1.2 Type

🔹 Guid

##### 1.8.3.1.3 Is Required

✅ Yes

##### 1.8.3.1.4 Is Primary Key

✅ Yes

##### 1.8.3.1.5 Is Unique

❌ No

##### 1.8.3.1.6 Index Type

Index

##### 1.8.3.1.7 Size

0

##### 1.8.3.1.8 Constraints

*No items available*

##### 1.8.3.1.9 Default Value



##### 1.8.3.1.10 Is Foreign Key

✅ Yes

##### 1.8.3.1.11 Precision

0

##### 1.8.3.1.12 Scale

0

#### 1.8.3.2.0 INT

##### 1.8.3.2.1 Name

roleId

##### 1.8.3.2.2 Type

🔹 INT

##### 1.8.3.2.3 Is Required

✅ Yes

##### 1.8.3.2.4 Is Primary Key

✅ Yes

##### 1.8.3.2.5 Is Unique

❌ No

##### 1.8.3.2.6 Index Type

Index

##### 1.8.3.2.7 Size

0

##### 1.8.3.2.8 Constraints

*No items available*

##### 1.8.3.2.9 Default Value



##### 1.8.3.2.10 Is Foreign Key

✅ Yes

##### 1.8.3.2.11 Precision

0

##### 1.8.3.2.12 Scale

0

### 1.8.4.0.0 Primary Keys

- userId
- roleId

### 1.8.5.0.0 Unique Constraints

*No items available*

### 1.8.6.0.0 Indexes

- {'name': 'IX_UserRole_RoleId', 'columns': ['roleId'], 'type': 'BTree'}

### 1.8.7.0.0 Notes

- High Priority: Cache user role and permission sets in a distributed cache (like Redis) keyed by userId to eliminate database joins for RBAC checks on every API request. Requires robust cache invalidation logic.

## 1.9.0.0.0 ApplicationConfiguration

### 1.9.1.0.0 Name

ApplicationConfiguration

### 1.9.2.0.0 Description

A key-value store for system-wide settings, such as timeouts, limits, and feature flags. Fulfills REQ-SEC-DTR-001, REQ-OPER-IMP-001.

### 1.9.3.0.0 Attributes

#### 1.9.3.1.0 VARCHAR

##### 1.9.3.1.1 Name

configurationKey

##### 1.9.3.1.2 Type

🔹 VARCHAR

##### 1.9.3.1.3 Is Required

✅ Yes

##### 1.9.3.1.4 Is Primary Key

✅ Yes

##### 1.9.3.1.5 Is Unique

✅ Yes

##### 1.9.3.1.6 Index Type

UniqueIndex

##### 1.9.3.1.7 Size

255

##### 1.9.3.1.8 Constraints

*No items available*

##### 1.9.3.1.9 Default Value



##### 1.9.3.1.10 Is Foreign Key

❌ No

##### 1.9.3.1.11 Precision

0

##### 1.9.3.1.12 Scale

0

#### 1.9.3.2.0 TEXT

##### 1.9.3.2.1 Name

configurationValue

##### 1.9.3.2.2 Type

🔹 TEXT

##### 1.9.3.2.3 Is Required

✅ Yes

##### 1.9.3.2.4 Is Primary Key

❌ No

##### 1.9.3.2.5 Is Unique

❌ No

##### 1.9.3.2.6 Index Type

None

##### 1.9.3.2.7 Size

0

##### 1.9.3.2.8 Constraints

*No items available*

##### 1.9.3.2.9 Default Value



##### 1.9.3.2.10 Is Foreign Key

❌ No

##### 1.9.3.2.11 Precision

0

##### 1.9.3.2.12 Scale

0

#### 1.9.3.3.0 TEXT

##### 1.9.3.3.1 Name

description

##### 1.9.3.3.2 Type

🔹 TEXT

##### 1.9.3.3.3 Is Required

❌ No

##### 1.9.3.3.4 Is Primary Key

❌ No

##### 1.9.3.3.5 Is Unique

❌ No

##### 1.9.3.3.6 Index Type

None

##### 1.9.3.3.7 Size

0

##### 1.9.3.3.8 Constraints

*No items available*

##### 1.9.3.3.9 Default Value



##### 1.9.3.3.10 Is Foreign Key

❌ No

##### 1.9.3.3.11 Precision

0

##### 1.9.3.3.12 Scale

0

#### 1.9.3.4.0 BOOLEAN

##### 1.9.3.4.1 Name

isSensitive

##### 1.9.3.4.2 Type

🔹 BOOLEAN

##### 1.9.3.4.3 Is Required

✅ Yes

##### 1.9.3.4.4 Is Primary Key

❌ No

##### 1.9.3.4.5 Is Unique

❌ No

##### 1.9.3.4.6 Index Type

None

##### 1.9.3.4.7 Size

0

##### 1.9.3.4.8 Constraints

*No items available*

##### 1.9.3.4.9 Default Value

false

##### 1.9.3.4.10 Is Foreign Key

❌ No

##### 1.9.3.4.11 Precision

0

##### 1.9.3.4.12 Scale

0

#### 1.9.3.5.0 DateTime

##### 1.9.3.5.1 Name

updatedAt

##### 1.9.3.5.2 Type

🔹 DateTime

##### 1.9.3.5.3 Is Required

✅ Yes

##### 1.9.3.5.4 Is Primary Key

❌ No

##### 1.9.3.5.5 Is Unique

❌ No

##### 1.9.3.5.6 Index Type

None

##### 1.9.3.5.7 Size

0

##### 1.9.3.5.8 Constraints

*No items available*

##### 1.9.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.9.3.5.10 Is Foreign Key

❌ No

##### 1.9.3.5.11 Precision

0

##### 1.9.3.5.12 Scale

0

### 1.9.4.0.0 Primary Keys

- configurationKey

### 1.9.5.0.0 Unique Constraints

*No items available*

### 1.9.6.0.0 Indexes

*No items available*

### 1.9.7.0.0 Notes

- High Priority: Implement a write-through or read-through caching strategy (e.g., using Redis or in-memory cache) for all non-sensitive configuration values to reduce database load. Invalidate cache on update.

## 1.10.0.0.0 ReportingDashboardMV

### 1.10.1.0.0 Name

ReportingDashboardMV

### 1.10.2.0.0 Description

Materialized view that pre-aggregates key metrics, such as hourly/daily counts of jobs by status ('Completed', 'Failed') and average execution time per report configuration. Refresh on a schedule (e.g., every 5 minutes).

### 1.10.3.0.0 Attributes

#### 1.10.3.1.0 DATE

##### 1.10.3.1.1 Name

aggregationDate

##### 1.10.3.1.2 Type

🔹 DATE

##### 1.10.3.1.3 Is Required

✅ Yes

##### 1.10.3.1.4 Is Primary Key

✅ Yes

##### 1.10.3.1.5 Is Unique

❌ No

##### 1.10.3.1.6 Index Type

Index

##### 1.10.3.1.7 Size

0

##### 1.10.3.1.8 Constraints

*No items available*

##### 1.10.3.1.9 Default Value



##### 1.10.3.1.10 Is Foreign Key

❌ No

##### 1.10.3.1.11 Precision

0

##### 1.10.3.1.12 Scale

0

#### 1.10.3.2.0 Guid

##### 1.10.3.2.1 Name

reportConfigurationId

##### 1.10.3.2.2 Type

🔹 Guid

##### 1.10.3.2.3 Is Required

✅ Yes

##### 1.10.3.2.4 Is Primary Key

✅ Yes

##### 1.10.3.2.5 Is Unique

❌ No

##### 1.10.3.2.6 Index Type

Index

##### 1.10.3.2.7 Size

0

##### 1.10.3.2.8 Constraints

*No items available*

##### 1.10.3.2.9 Default Value



##### 1.10.3.2.10 Is Foreign Key

❌ No

##### 1.10.3.2.11 Precision

0

##### 1.10.3.2.12 Scale

0

#### 1.10.3.3.0 VARCHAR

##### 1.10.3.3.1 Name

reportConfigurationName

##### 1.10.3.3.2 Type

🔹 VARCHAR

##### 1.10.3.3.3 Is Required

✅ Yes

##### 1.10.3.3.4 Is Primary Key

❌ No

##### 1.10.3.3.5 Is Unique

❌ No

##### 1.10.3.3.6 Index Type

Index

##### 1.10.3.3.7 Size

255

##### 1.10.3.3.8 Constraints

*No items available*

##### 1.10.3.3.9 Default Value



##### 1.10.3.3.10 Is Foreign Key

❌ No

##### 1.10.3.3.11 Precision

0

##### 1.10.3.3.12 Scale

0

#### 1.10.3.4.0 BIGINT

##### 1.10.3.4.1 Name

completedJobsCount

##### 1.10.3.4.2 Type

🔹 BIGINT

##### 1.10.3.4.3 Is Required

✅ Yes

##### 1.10.3.4.4 Is Primary Key

❌ No

##### 1.10.3.4.5 Is Unique

❌ No

##### 1.10.3.4.6 Index Type

None

##### 1.10.3.4.7 Size

0

##### 1.10.3.4.8 Constraints

*No items available*

##### 1.10.3.4.9 Default Value

0

##### 1.10.3.4.10 Is Foreign Key

❌ No

##### 1.10.3.4.11 Precision

0

##### 1.10.3.4.12 Scale

0

#### 1.10.3.5.0 BIGINT

##### 1.10.3.5.1 Name

failedJobsCount

##### 1.10.3.5.2 Type

🔹 BIGINT

##### 1.10.3.5.3 Is Required

✅ Yes

##### 1.10.3.5.4 Is Primary Key

❌ No

##### 1.10.3.5.5 Is Unique

❌ No

##### 1.10.3.5.6 Index Type

None

##### 1.10.3.5.7 Size

0

##### 1.10.3.5.8 Constraints

*No items available*

##### 1.10.3.5.9 Default Value

0

##### 1.10.3.5.10 Is Foreign Key

❌ No

##### 1.10.3.5.11 Precision

0

##### 1.10.3.5.12 Scale

0

#### 1.10.3.6.0 DECIMAL

##### 1.10.3.6.1 Name

averageExecutionTimeSeconds

##### 1.10.3.6.2 Type

🔹 DECIMAL

##### 1.10.3.6.3 Is Required

❌ No

##### 1.10.3.6.4 Is Primary Key

❌ No

##### 1.10.3.6.5 Is Unique

❌ No

##### 1.10.3.6.6 Index Type

None

##### 1.10.3.6.7 Size

0

##### 1.10.3.6.8 Constraints

*No items available*

##### 1.10.3.6.9 Default Value



##### 1.10.3.6.10 Is Foreign Key

❌ No

##### 1.10.3.6.11 Precision

10

##### 1.10.3.6.12 Scale

2

### 1.10.4.0.0 Primary Keys

- aggregationDate
- reportConfigurationId

### 1.10.5.0.0 Unique Constraints

*No items available*

### 1.10.6.0.0 Indexes

- {'name': 'IX_ReportingDashboardMV_Name', 'columns': ['reportConfigurationName'], 'type': 'BTree'}

### 1.10.7.0.0 Notes

- This represents a database object (Materialized View), not a standard table. It serves pre-computed data to analytical dashboards for near-instant load times.

# 2.0.0.0.0 Relations

## 2.1.0.0.0 OneToMany

### 2.1.1.0.0 Name

TransformationScriptVersions

### 2.1.2.0.0 Id

REL_TRANSFORMATIONSCRIPT_TRANSFORMATIONSCRIPTVERSION_001

### 2.1.3.0.0 Source Entity

TransformationScript

### 2.1.4.0.0 Target Entity

TransformationScriptVersion

### 2.1.5.0.0 Type

🔹 OneToMany

### 2.1.6.0.0 Source Multiplicity

1

### 2.1.7.0.0 Target Multiplicity

0..*

### 2.1.8.0.0 Cascade Delete

✅ Yes

### 2.1.9.0.0 Is Identifying

❌ No

### 2.1.10.0.0 On Delete

Cascade

### 2.1.11.0.0 On Update

Cascade

## 2.2.0.0.0 OneToOne

### 2.2.1.0.0 Name

TransformationScriptActiveVersion

### 2.2.2.0.0 Id

REL_TRANSFORMATIONSCRIPT_TRANSFORMATIONSCRIPTVERSION_002

### 2.2.3.0.0 Source Entity

TransformationScript

### 2.2.4.0.0 Target Entity

TransformationScriptVersion

### 2.2.5.0.0 Type

🔹 OneToOne

### 2.2.6.0.0 Source Multiplicity

1

### 2.2.7.0.0 Target Multiplicity

0..1

### 2.2.8.0.0 Cascade Delete

❌ No

### 2.2.9.0.0 Is Identifying

❌ No

### 2.2.10.0.0 On Delete

SetNull

### 2.2.11.0.0 On Update

Cascade

## 2.3.0.0.0 OneToMany

### 2.3.1.0.0 Name

TransformationScriptReportConfigurations

### 2.3.2.0.0 Id

REL_TRANSFORMATIONSCRIPT_REPORTCONFIGURATION_001

### 2.3.3.0.0 Source Entity

TransformationScript

### 2.3.4.0.0 Target Entity

ReportConfiguration

### 2.3.5.0.0 Type

🔹 OneToMany

### 2.3.6.0.0 Source Multiplicity

1

### 2.3.7.0.0 Target Multiplicity

0..*

### 2.3.8.0.0 Cascade Delete

❌ No

### 2.3.9.0.0 Is Identifying

❌ No

### 2.3.10.0.0 On Delete

SetNull

### 2.3.11.0.0 On Update

Cascade

## 2.4.0.0.0 OneToMany

### 2.4.1.0.0 Name

ReportConfigurationJobs

### 2.4.2.0.0 Id

REL_REPORTCONFIGURATION_REPORTJOB_001

### 2.4.3.0.0 Source Entity

ReportConfiguration

### 2.4.4.0.0 Target Entity

ReportJob

### 2.4.5.0.0 Type

🔹 OneToMany

### 2.4.6.0.0 Source Multiplicity

1

### 2.4.7.0.0 Target Multiplicity

0..*

### 2.4.8.0.0 Cascade Delete

❌ No

### 2.4.9.0.0 Is Identifying

❌ No

### 2.4.10.0.0 On Delete

SetNull

### 2.4.11.0.0 On Update

Cascade

## 2.5.0.0.0 OneToMany

### 2.5.1.0.0 Name

TransformationScriptVersionJobs

### 2.5.2.0.0 Id

REL_TRANSFORMATIONSCRIPTVERSION_REPORTJOB_001

### 2.5.3.0.0 Source Entity

TransformationScriptVersion

### 2.5.4.0.0 Target Entity

ReportJob

### 2.5.5.0.0 Type

🔹 OneToMany

### 2.5.6.0.0 Source Multiplicity

1

### 2.5.7.0.0 Target Multiplicity

0..*

### 2.5.8.0.0 Cascade Delete

❌ No

### 2.5.9.0.0 Is Identifying

❌ No

### 2.5.10.0.0 On Delete

SetNull

### 2.5.11.0.0 On Update

Cascade

## 2.6.0.0.0 OneToMany

### 2.6.1.0.0 Name

UserCreatedTransformationScripts

### 2.6.2.0.0 Id

REL_USER_TRANSFORMATIONSCRIPT_001

### 2.6.3.0.0 Source Entity

User

### 2.6.4.0.0 Target Entity

TransformationScript

### 2.6.5.0.0 Type

🔹 OneToMany

### 2.6.6.0.0 Source Multiplicity

1

### 2.6.7.0.0 Target Multiplicity

0..*

### 2.6.8.0.0 Cascade Delete

❌ No

### 2.6.9.0.0 Is Identifying

❌ No

### 2.6.10.0.0 On Delete

SetNull

### 2.6.11.0.0 On Update

Cascade

## 2.7.0.0.0 OneToMany

### 2.7.1.0.0 Name

UserUpdatedTransformationScripts

### 2.7.2.0.0 Id

REL_USER_TRANSFORMATIONSCRIPT_002

### 2.7.3.0.0 Source Entity

User

### 2.7.4.0.0 Target Entity

TransformationScript

### 2.7.5.0.0 Type

🔹 OneToMany

### 2.7.6.0.0 Source Multiplicity

1

### 2.7.7.0.0 Target Multiplicity

0..*

### 2.7.8.0.0 Cascade Delete

❌ No

### 2.7.9.0.0 Is Identifying

❌ No

### 2.7.10.0.0 On Delete

SetNull

### 2.7.11.0.0 On Update

Cascade

## 2.8.0.0.0 OneToMany

### 2.8.1.0.0 Name

UserCreatedTransformationScriptVersions

### 2.8.2.0.0 Id

REL_USER_TRANSFORMATIONSCRIPTVERSION_001

### 2.8.3.0.0 Source Entity

User

### 2.8.4.0.0 Target Entity

TransformationScriptVersion

### 2.8.5.0.0 Type

🔹 OneToMany

### 2.8.6.0.0 Source Multiplicity

1

### 2.8.7.0.0 Target Multiplicity

0..*

### 2.8.8.0.0 Cascade Delete

❌ No

### 2.8.9.0.0 Is Identifying

❌ No

### 2.8.10.0.0 On Delete

SetNull

### 2.8.11.0.0 On Update

Cascade

## 2.9.0.0.0 OneToMany

### 2.9.1.0.0 Name

UserCreatedReportConfigurations

### 2.9.2.0.0 Id

REL_USER_REPORTCONFIGURATION_001

### 2.9.3.0.0 Source Entity

User

### 2.9.4.0.0 Target Entity

ReportConfiguration

### 2.9.5.0.0 Type

🔹 OneToMany

### 2.9.6.0.0 Source Multiplicity

1

### 2.9.7.0.0 Target Multiplicity

0..*

### 2.9.8.0.0 Cascade Delete

❌ No

### 2.9.9.0.0 Is Identifying

❌ No

### 2.9.10.0.0 On Delete

SetNull

### 2.9.11.0.0 On Update

Cascade

## 2.10.0.0.0 OneToMany

### 2.10.1.0.0 Name

UserUpdatedReportConfigurations

### 2.10.2.0.0 Id

REL_USER_REPORTCONFIGURATION_002

### 2.10.3.0.0 Source Entity

User

### 2.10.4.0.0 Target Entity

ReportConfiguration

### 2.10.5.0.0 Type

🔹 OneToMany

### 2.10.6.0.0 Source Multiplicity

1

### 2.10.7.0.0 Target Multiplicity

0..*

### 2.10.8.0.0 Cascade Delete

❌ No

### 2.10.9.0.0 Is Identifying

❌ No

### 2.10.10.0.0 On Delete

SetNull

### 2.10.11.0.0 On Update

Cascade

## 2.11.0.0.0 OneToMany

### 2.11.1.0.0 Name

UserAuditLogs

### 2.11.2.0.0 Id

REL_USER_AUDITLOG_001

### 2.11.3.0.0 Source Entity

User

### 2.11.4.0.0 Target Entity

AuditLog

### 2.11.5.0.0 Type

🔹 OneToMany

### 2.11.6.0.0 Source Multiplicity

1

### 2.11.7.0.0 Target Multiplicity

0..*

### 2.11.8.0.0 Cascade Delete

❌ No

### 2.11.9.0.0 Is Identifying

❌ No

### 2.11.10.0.0 On Delete

SetNull

### 2.11.11.0.0 On Update

Cascade

## 2.12.0.0.0 OneToMany

### 2.12.1.0.0 Name

UserRoleAssignments

### 2.12.2.0.0 Id

REL_USER_USERROLE_001

### 2.12.3.0.0 Source Entity

User

### 2.12.4.0.0 Target Entity

UserRole

### 2.12.5.0.0 Type

🔹 OneToMany

### 2.12.6.0.0 Source Multiplicity

1

### 2.12.7.0.0 Target Multiplicity

0..*

### 2.12.8.0.0 Cascade Delete

✅ Yes

### 2.12.9.0.0 Is Identifying

✅ Yes

### 2.12.10.0.0 On Delete

Cascade

### 2.12.11.0.0 On Update

Cascade

## 2.13.0.0.0 OneToMany

### 2.13.1.0.0 Name

RoleUserAssignments

### 2.13.2.0.0 Id

REL_ROLE_USERROLE_001

### 2.13.3.0.0 Source Entity

Role

### 2.13.4.0.0 Target Entity

UserRole

### 2.13.5.0.0 Type

🔹 OneToMany

### 2.13.6.0.0 Source Multiplicity

1

### 2.13.7.0.0 Target Multiplicity

0..*

### 2.13.8.0.0 Cascade Delete

✅ Yes

### 2.13.9.0.0 Is Identifying

✅ Yes

### 2.13.10.0.0 On Delete

Cascade

### 2.13.11.0.0 On Update

Cascade

