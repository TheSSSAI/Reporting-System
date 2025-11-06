# 1 Entities

## 1.1 TransformationScript

### 1.1.1 Name

TransformationScript

### 1.1.2 Description

Represents a user-defined transformation script. Each script has a history of versions. REQ-1-013

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

activeScriptVersionId

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

#### 1.1.3.5.0 INT

##### 1.1.3.5.1 Name

activeVersionNumber

##### 1.1.3.5.2 Type

🔹 INT

##### 1.1.3.5.3 Is Required

❌ No

##### 1.1.3.5.4 Is Primary Key

❌ No

##### 1.1.3.5.5 Is Unique

❌ No

##### 1.1.3.5.6 Index Type

Index

##### 1.1.3.5.7 Size

0

##### 1.1.3.5.8 Constraints

- Denormalized field to avoid joins when listing scripts

##### 1.1.3.5.9 Default Value



##### 1.1.3.5.10 Is Foreign Key

❌ No

##### 1.1.3.5.11 Precision

0

##### 1.1.3.5.12 Scale

0

#### 1.1.3.6.0 Guid

##### 1.1.3.6.1 Name

createdByUserId

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

#### 1.1.3.7.0 Guid

##### 1.1.3.7.1 Name

updatedByUserId

##### 1.1.3.7.2 Type

🔹 Guid

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



##### 1.1.3.7.10 Is Foreign Key

✅ Yes

##### 1.1.3.7.11 Precision

0

##### 1.1.3.7.12 Scale

0

#### 1.1.3.8.0 BOOLEAN

##### 1.1.3.8.1 Name

isDeleted

##### 1.1.3.8.2 Type

🔹 BOOLEAN

##### 1.1.3.8.3 Is Required

✅ Yes

##### 1.1.3.8.4 Is Primary Key

❌ No

##### 1.1.3.8.5 Is Unique

❌ No

##### 1.1.3.8.6 Index Type

Index

##### 1.1.3.8.7 Size

0

##### 1.1.3.8.8 Constraints

*No items available*

##### 1.1.3.8.9 Default Value

false

##### 1.1.3.8.10 Is Foreign Key

❌ No

##### 1.1.3.8.11 Precision

0

##### 1.1.3.8.12 Scale

0

#### 1.1.3.9.0 DateTimeOffset

##### 1.1.3.9.1 Name

createdAt

##### 1.1.3.9.2 Type

🔹 DateTimeOffset

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

CURRENT_TIMESTAMP

##### 1.1.3.9.10 Is Foreign Key

❌ No

##### 1.1.3.9.11 Precision

0

##### 1.1.3.9.12 Scale

0

#### 1.1.3.10.0 DateTimeOffset

##### 1.1.3.10.1 Name

updatedAt

##### 1.1.3.10.2 Type

🔹 DateTimeOffset

##### 1.1.3.10.3 Is Required

✅ Yes

##### 1.1.3.10.4 Is Primary Key

❌ No

##### 1.1.3.10.5 Is Unique

❌ No

##### 1.1.3.10.6 Index Type

None

##### 1.1.3.10.7 Size

0

##### 1.1.3.10.8 Constraints

*No items available*

##### 1.1.3.10.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.10.10 Is Foreign Key

❌ No

##### 1.1.3.10.11 Precision

0

##### 1.1.3.10.12 Scale

0

### 1.1.4.0.0 Primary Keys

- transformationScriptId

### 1.1.5.0.0 Unique Constraints

- {'name': 'UC_TransformationScript_Name', 'columns': ['name']}

### 1.1.6.0.0 Indexes

#### 1.1.6.1.0 BTree

##### 1.1.6.1.1 Name

IX_TransformationScript_ActiveScriptVersionId

##### 1.1.6.1.2 Columns

- activeScriptVersionId

##### 1.1.6.1.3 Type

🔹 BTree

#### 1.1.6.2.0 BTree

##### 1.1.6.2.1 Name

IX_TransformationScript_IsDeleted

##### 1.1.6.2.2 Columns

- isDeleted

##### 1.1.6.2.3 Type

🔹 BTree

#### 1.1.6.3.0 BTree

##### 1.1.6.3.1 Name

IX_TransformationScript_ActiveVersionNumber

##### 1.1.6.3.2 Columns

- activeVersionNumber

##### 1.1.6.3.3 Type

🔹 BTree

## 1.2.0.0.0 ScriptVersion

### 1.2.1.0.0 Name

ScriptVersion

### 1.2.2.0.0 Description

Stores an immutable version of a transformation script's content. Created on each update. REQ-1-014, REQ-1-017

### 1.2.3.0.0 Attributes

#### 1.2.3.1.0 Guid

##### 1.2.3.1.1 Name

scriptVersionId

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

- Must be unique per transformationScriptId

##### 1.2.3.3.9 Default Value



##### 1.2.3.3.10 Is Foreign Key

❌ No

##### 1.2.3.3.11 Precision

0

##### 1.2.3.3.12 Scale

0

#### 1.2.3.4.0 VARBINARY(MAX)

##### 1.2.3.4.1 Name

scriptContentEncrypted

##### 1.2.3.4.2 Type

🔹 VARBINARY(MAX)

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

- Ideal candidate for caching (e.g., Redis) to reduce DB load.

##### 1.2.3.4.9 Default Value



##### 1.2.3.4.10 Is Foreign Key

❌ No

##### 1.2.3.4.11 Precision

0

##### 1.2.3.4.12 Scale

0

#### 1.2.3.5.0 TEXT

##### 1.2.3.5.1 Name

changeLog

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

#### 1.2.3.7.0 DateTimeOffset

##### 1.2.3.7.1 Name

createdAt

##### 1.2.3.7.2 Type

🔹 DateTimeOffset

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

- scriptVersionId

### 1.2.5.0.0 Unique Constraints

- {'name': 'UC_ScriptVersion_Script_Version', 'columns': ['transformationScriptId', 'versionNumber']}

### 1.2.6.0.0 Indexes

#### 1.2.6.1.0 BTree

##### 1.2.6.1.1 Name

IX_ScriptVersion_CreatedAt

##### 1.2.6.1.2 Columns

- createdAt

##### 1.2.6.1.3 Type

🔹 BTree

#### 1.2.6.2.0 BTree

##### 1.2.6.2.1 Name

IX_ScriptVersion_ScriptId_VersionDesc

##### 1.2.6.2.2 Columns

- transformationScriptId
- versionNumber DESC

##### 1.2.6.2.3 Type

🔹 BTree

### 1.2.7.0.0 Partitioning

#### 1.2.7.1.0 Type

🔹 Hash

#### 1.2.7.2.0 Columns

- transformationScriptId

#### 1.2.7.3.0 Strategy

For systems with extremely high volume of script updates, hash partitioning distributes versions for different scripts across physical partitions, reducing contention.

## 1.3.0.0.0 ReportConfiguration

### 1.3.1.0.0 Name

ReportConfiguration

### 1.3.2.0.0 Description

Represents a report definition in the system. Can be associated with a transformation script.

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

jsonSchemaId

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

#### 1.3.3.4.0 Guid

##### 1.3.3.4.1 Name

createdByUserId

##### 1.3.3.4.2 Type

🔹 Guid

##### 1.3.3.4.3 Is Required

✅ Yes

##### 1.3.3.4.4 Is Primary Key

❌ No

##### 1.3.3.4.5 Is Unique

❌ No

##### 1.3.3.4.6 Index Type

Index

##### 1.3.3.4.7 Size

0

##### 1.3.3.4.8 Constraints

*No items available*

##### 1.3.3.4.9 Default Value



##### 1.3.3.4.10 Is Foreign Key

✅ Yes

##### 1.3.3.4.11 Precision

0

##### 1.3.3.4.12 Scale

0

#### 1.3.3.5.0 Guid

##### 1.3.3.5.1 Name

updatedByUserId

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

#### 1.3.3.6.0 DateTimeOffset

##### 1.3.3.6.1 Name

createdAt

##### 1.3.3.6.2 Type

🔹 DateTimeOffset

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

CURRENT_TIMESTAMP

##### 1.3.3.6.10 Is Foreign Key

❌ No

##### 1.3.3.6.11 Precision

0

##### 1.3.3.6.12 Scale

0

#### 1.3.3.7.0 DateTimeOffset

##### 1.3.3.7.1 Name

updatedAt

##### 1.3.3.7.2 Type

🔹 DateTimeOffset

##### 1.3.3.7.3 Is Required

✅ Yes

##### 1.3.3.7.4 Is Primary Key

❌ No

##### 1.3.3.7.5 Is Unique

❌ No

##### 1.3.3.7.6 Index Type

None

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

### 1.3.4.0.0 Primary Keys

- reportConfigurationId

### 1.3.5.0.0 Unique Constraints

*No items available*

### 1.3.6.0.0 Indexes

- {'name': 'IX_ReportConfiguration_Name', 'columns': ['name'], 'type': 'BTree'}

## 1.4.0.0.0 ReportTransformationScriptAssociation

### 1.4.1.0.0 Name

ReportTransformationScriptAssociation

### 1.4.2.0.0 Description

Junction table to manage the many-to-many relationship between reports and scripts. REQ-1-013

### 1.4.3.0.0 Attributes

#### 1.4.3.1.0 Guid

##### 1.4.3.1.1 Name

reportConfigurationId

##### 1.4.3.1.2 Type

🔹 Guid

##### 1.4.3.1.3 Is Required

✅ Yes

##### 1.4.3.1.4 Is Primary Key

✅ Yes

##### 1.4.3.1.5 Is Unique

❌ No

##### 1.4.3.1.6 Index Type

UniqueIndex

##### 1.4.3.1.7 Size

0

##### 1.4.3.1.8 Constraints

*No items available*

##### 1.4.3.1.9 Default Value



##### 1.4.3.1.10 Is Foreign Key

✅ Yes

##### 1.4.3.1.11 Precision

0

##### 1.4.3.1.12 Scale

0

#### 1.4.3.2.0 Guid

##### 1.4.3.2.1 Name

transformationScriptId

##### 1.4.3.2.2 Type

🔹 Guid

##### 1.4.3.2.3 Is Required

✅ Yes

##### 1.4.3.2.4 Is Primary Key

✅ Yes

##### 1.4.3.2.5 Is Unique

❌ No

##### 1.4.3.2.6 Index Type

UniqueIndex

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

associatedByUserId

##### 1.4.3.3.2 Type

🔹 Guid

##### 1.4.3.3.3 Is Required

✅ Yes

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

#### 1.4.3.4.0 DateTimeOffset

##### 1.4.3.4.1 Name

associatedAt

##### 1.4.3.4.2 Type

🔹 DateTimeOffset

##### 1.4.3.4.3 Is Required

✅ Yes

##### 1.4.3.4.4 Is Primary Key

❌ No

##### 1.4.3.4.5 Is Unique

❌ No

##### 1.4.3.4.6 Index Type

None

##### 1.4.3.4.7 Size

0

##### 1.4.3.4.8 Constraints

*No items available*

##### 1.4.3.4.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.4.10 Is Foreign Key

❌ No

##### 1.4.3.4.11 Precision

0

##### 1.4.3.4.12 Scale

0

### 1.4.4.0.0 Primary Keys

- reportConfigurationId
- transformationScriptId

### 1.4.5.0.0 Unique Constraints

*No items available*

### 1.4.6.0.0 Indexes

- {'name': 'IX_RTSA_TransformationScriptId', 'columns': ['transformationScriptId'], 'type': 'BTree'}

## 1.5.0.0.0 JsonSchema

### 1.5.1.0.0 Name

JsonSchema

### 1.5.2.0.0 Description

Stores a JSON Schema definition that can be used to validate the output of a transformation script. REQ-1-025

### 1.5.3.0.0 Attributes

#### 1.5.3.1.0 Guid

##### 1.5.3.1.1 Name

jsonSchemaId

##### 1.5.3.1.2 Type

🔹 Guid

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

*No items available*

##### 1.5.3.1.9 Default Value



##### 1.5.3.1.10 Is Foreign Key

❌ No

##### 1.5.3.1.11 Precision

0

##### 1.5.3.1.12 Scale

0

#### 1.5.3.2.0 VARCHAR

##### 1.5.3.2.1 Name

name

##### 1.5.3.2.2 Type

🔹 VARCHAR

##### 1.5.3.2.3 Is Required

✅ Yes

##### 1.5.3.2.4 Is Primary Key

❌ No

##### 1.5.3.2.5 Is Unique

✅ Yes

##### 1.5.3.2.6 Index Type

UniqueIndex

##### 1.5.3.2.7 Size

255

##### 1.5.3.2.8 Constraints

*No items available*

##### 1.5.3.2.9 Default Value



##### 1.5.3.2.10 Is Foreign Key

❌ No

##### 1.5.3.2.11 Precision

0

##### 1.5.3.2.12 Scale

0

#### 1.5.3.3.0 TEXT

##### 1.5.3.3.1 Name

schemaDefinition

##### 1.5.3.3.2 Type

🔹 TEXT

##### 1.5.3.3.3 Is Required

✅ Yes

##### 1.5.3.3.4 Is Primary Key

❌ No

##### 1.5.3.3.5 Is Unique

❌ No

##### 1.5.3.3.6 Index Type

None

##### 1.5.3.3.7 Size

0

##### 1.5.3.3.8 Constraints

- Must be a valid JSON object
- Ideal candidate for in-memory or distributed caching.

##### 1.5.3.3.9 Default Value



##### 1.5.3.3.10 Is Foreign Key

❌ No

##### 1.5.3.3.11 Precision

0

##### 1.5.3.3.12 Scale

0

#### 1.5.3.4.0 Guid

##### 1.5.3.4.1 Name

createdByUserId

##### 1.5.3.4.2 Type

🔹 Guid

##### 1.5.3.4.3 Is Required

✅ Yes

##### 1.5.3.4.4 Is Primary Key

❌ No

##### 1.5.3.4.5 Is Unique

❌ No

##### 1.5.3.4.6 Index Type

Index

##### 1.5.3.4.7 Size

0

##### 1.5.3.4.8 Constraints

*No items available*

##### 1.5.3.4.9 Default Value



##### 1.5.3.4.10 Is Foreign Key

✅ Yes

##### 1.5.3.4.11 Precision

0

##### 1.5.3.4.12 Scale

0

#### 1.5.3.5.0 DateTimeOffset

##### 1.5.3.5.1 Name

createdAt

##### 1.5.3.5.2 Type

🔹 DateTimeOffset

##### 1.5.3.5.3 Is Required

✅ Yes

##### 1.5.3.5.4 Is Primary Key

❌ No

##### 1.5.3.5.5 Is Unique

❌ No

##### 1.5.3.5.6 Index Type

Index

##### 1.5.3.5.7 Size

0

##### 1.5.3.5.8 Constraints

*No items available*

##### 1.5.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.5.10 Is Foreign Key

❌ No

##### 1.5.3.5.11 Precision

0

##### 1.5.3.5.12 Scale

0

#### 1.5.3.6.0 DateTimeOffset

##### 1.5.3.6.1 Name

updatedAt

##### 1.5.3.6.2 Type

🔹 DateTimeOffset

##### 1.5.3.6.3 Is Required

✅ Yes

##### 1.5.3.6.4 Is Primary Key

❌ No

##### 1.5.3.6.5 Is Unique

❌ No

##### 1.5.3.6.6 Index Type

None

##### 1.5.3.6.7 Size

0

##### 1.5.3.6.8 Constraints

*No items available*

##### 1.5.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.6.10 Is Foreign Key

❌ No

##### 1.5.3.6.11 Precision

0

##### 1.5.3.6.12 Scale

0

### 1.5.4.0.0 Primary Keys

- jsonSchemaId

### 1.5.5.0.0 Unique Constraints

- {'name': 'UC_JsonSchema_Name', 'columns': ['name']}

### 1.5.6.0.0 Indexes

*No items available*

## 1.6.0.0.0 AuditLog

### 1.6.1.0.0 Name

AuditLog

### 1.6.2.0.0 Description

Records all script management activities (CRUD, association) for compliance and tracking. REQ-1-018, REQ-1-028

### 1.6.3.0.0 Attributes

#### 1.6.3.1.0 BIGINT

##### 1.6.3.1.1 Name

auditLogId

##### 1.6.3.1.2 Type

🔹 BIGINT

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

- IDENTITY(1,1)

##### 1.6.3.1.9 Default Value



##### 1.6.3.1.10 Is Foreign Key

❌ No

##### 1.6.3.1.11 Precision

0

##### 1.6.3.1.12 Scale

0

#### 1.6.3.2.0 Guid

##### 1.6.3.2.1 Name

userId

##### 1.6.3.2.2 Type

🔹 Guid

##### 1.6.3.2.3 Is Required

✅ Yes

##### 1.6.3.2.4 Is Primary Key

❌ No

##### 1.6.3.2.5 Is Unique

❌ No

##### 1.6.3.2.6 Index Type

Index

##### 1.6.3.2.7 Size

0

##### 1.6.3.2.8 Constraints

*No items available*

##### 1.6.3.2.9 Default Value



##### 1.6.3.2.10 Is Foreign Key

✅ Yes

##### 1.6.3.2.11 Precision

0

##### 1.6.3.2.12 Scale

0

#### 1.6.3.3.0 VARCHAR

##### 1.6.3.3.1 Name

actionType

##### 1.6.3.3.2 Type

🔹 VARCHAR

##### 1.6.3.3.3 Is Required

✅ Yes

##### 1.6.3.3.4 Is Primary Key

❌ No

##### 1.6.3.3.5 Is Unique

❌ No

##### 1.6.3.3.6 Index Type

Index

##### 1.6.3.3.7 Size

100

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

entityType

##### 1.6.3.4.2 Type

🔹 VARCHAR

##### 1.6.3.4.3 Is Required

✅ Yes

##### 1.6.3.4.4 Is Primary Key

❌ No

##### 1.6.3.4.5 Is Unique

❌ No

##### 1.6.3.4.6 Index Type

Index

##### 1.6.3.4.7 Size

100

##### 1.6.3.4.8 Constraints

*No items available*

##### 1.6.3.4.9 Default Value



##### 1.6.3.4.10 Is Foreign Key

❌ No

##### 1.6.3.4.11 Precision

0

##### 1.6.3.4.12 Scale

0

#### 1.6.3.5.0 Guid

##### 1.6.3.5.1 Name

entityId

##### 1.6.3.5.2 Type

🔹 Guid

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



##### 1.6.3.5.10 Is Foreign Key

❌ No

##### 1.6.3.5.11 Precision

0

##### 1.6.3.5.12 Scale

0

#### 1.6.3.6.0 JSONB

##### 1.6.3.6.1 Name

changeDetails

##### 1.6.3.6.2 Type

🔹 JSONB

##### 1.6.3.6.3 Is Required

❌ No

##### 1.6.3.6.4 Is Primary Key

❌ No

##### 1.6.3.6.5 Is Unique

❌ No

##### 1.6.3.6.6 Index Type

None

##### 1.6.3.6.7 Size

0

##### 1.6.3.6.8 Constraints

- Should contain a JSON object detailing the change. Indexed with GIN for efficient querying.

##### 1.6.3.6.9 Default Value



##### 1.6.3.6.10 Is Foreign Key

❌ No

##### 1.6.3.6.11 Precision

0

##### 1.6.3.6.12 Scale

0

#### 1.6.3.7.0 DateTimeOffset

##### 1.6.3.7.1 Name

timestamp

##### 1.6.3.7.2 Type

🔹 DateTimeOffset

##### 1.6.3.7.3 Is Required

✅ Yes

##### 1.6.3.7.4 Is Primary Key

❌ No

##### 1.6.3.7.5 Is Unique

❌ No

##### 1.6.3.7.6 Index Type

Index

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

#### 1.6.3.8.0 VARCHAR

##### 1.6.3.8.1 Name

sourceIpAddress

##### 1.6.3.8.2 Type

🔹 VARCHAR

##### 1.6.3.8.3 Is Required

❌ No

##### 1.6.3.8.4 Is Primary Key

❌ No

##### 1.6.3.8.5 Is Unique

❌ No

##### 1.6.3.8.6 Index Type

None

##### 1.6.3.8.7 Size

45

##### 1.6.3.8.8 Constraints

*No items available*

##### 1.6.3.8.9 Default Value



##### 1.6.3.8.10 Is Foreign Key

❌ No

##### 1.6.3.8.11 Precision

0

##### 1.6.3.8.12 Scale

0

### 1.6.4.0.0 Primary Keys

- auditLogId
- timestamp

### 1.6.5.0.0 Unique Constraints

*No items available*

### 1.6.6.0.0 Indexes

#### 1.6.6.1.0 BTree

##### 1.6.6.1.1 Name

IX_AuditLog_EntityType_EntityId

##### 1.6.6.1.2 Columns

- entityType
- entityId

##### 1.6.6.1.3 Type

🔹 BTree

#### 1.6.6.2.0 BTree

##### 1.6.6.2.1 Name

IX_AuditLog_UserId_Timestamp

##### 1.6.6.2.2 Columns

- userId
- timestamp

##### 1.6.6.2.3 Type

🔹 BTree

#### 1.6.6.3.0 BTree

##### 1.6.6.3.1 Name

IX_AuditLog_TimestampDesc_EntityType_ActionType

##### 1.6.6.3.2 Columns

- timestamp DESC
- entityType
- actionType

##### 1.6.6.3.3 Type

🔹 BTree

#### 1.6.6.4.0 GIN

##### 1.6.6.4.1 Name

IX_AuditLog_ChangeDetails_GIN

##### 1.6.6.4.2 Columns

- changeDetails

##### 1.6.6.4.3 Type

🔹 GIN

### 1.6.7.0.0 Partitioning

#### 1.6.7.1.0 Type

🔹 Range

#### 1.6.7.2.0 Columns

- timestamp

#### 1.6.7.3.0 Strategy

Implement monthly range partitioning to improve performance for time-bound searches and simplify data archival/purging.

## 1.7.0.0.0 SecurityViolationLog

### 1.7.1.0.0 Name

SecurityViolationLog

### 1.7.2.0.0 Description

Dedicated log for script executions that violate a configured sandbox constraint. REQ-1-004, REQ-1-028

### 1.7.3.0.0 Attributes

#### 1.7.3.1.0 BIGINT

##### 1.7.3.1.1 Name

securityViolationLogId

##### 1.7.3.1.2 Type

🔹 BIGINT

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

- IDENTITY(1,1)

##### 1.7.3.1.9 Default Value



##### 1.7.3.1.10 Is Foreign Key

❌ No

##### 1.7.3.1.11 Precision

0

##### 1.7.3.1.12 Scale

0

#### 1.7.3.2.0 Guid

##### 1.7.3.2.1 Name

transformationScriptId

##### 1.7.3.2.2 Type

🔹 Guid

##### 1.7.3.2.3 Is Required

✅ Yes

##### 1.7.3.2.4 Is Primary Key

❌ No

##### 1.7.3.2.5 Is Unique

❌ No

##### 1.7.3.2.6 Index Type

Index

##### 1.7.3.2.7 Size

0

##### 1.7.3.2.8 Constraints

*No items available*

##### 1.7.3.2.9 Default Value



##### 1.7.3.2.10 Is Foreign Key

✅ Yes

##### 1.7.3.2.11 Precision

0

##### 1.7.3.2.12 Scale

0

#### 1.7.3.3.0 Guid

##### 1.7.3.3.1 Name

scriptVersionId

##### 1.7.3.3.2 Type

🔹 Guid

##### 1.7.3.3.3 Is Required

✅ Yes

##### 1.7.3.3.4 Is Primary Key

❌ No

##### 1.7.3.3.5 Is Unique

❌ No

##### 1.7.3.3.6 Index Type

Index

##### 1.7.3.3.7 Size

0

##### 1.7.3.3.8 Constraints

*No items available*

##### 1.7.3.3.9 Default Value



##### 1.7.3.3.10 Is Foreign Key

✅ Yes

##### 1.7.3.3.11 Precision

0

##### 1.7.3.3.12 Scale

0

#### 1.7.3.4.0 VARCHAR

##### 1.7.3.4.1 Name

violationType

##### 1.7.3.4.2 Type

🔹 VARCHAR

##### 1.7.3.4.3 Is Required

✅ Yes

##### 1.7.3.4.4 Is Primary Key

❌ No

##### 1.7.3.4.5 Is Unique

❌ No

##### 1.7.3.4.6 Index Type

Index

##### 1.7.3.4.7 Size

50

##### 1.7.3.4.8 Constraints

- ENUM('TIMEOUT', 'MEMORY_LIMIT', 'STATEMENT_COUNT')

##### 1.7.3.4.9 Default Value



##### 1.7.3.4.10 Is Foreign Key

❌ No

##### 1.7.3.4.11 Precision

0

##### 1.7.3.4.12 Scale

0

#### 1.7.3.5.0 VARCHAR

##### 1.7.3.5.1 Name

violationValue

##### 1.7.3.5.2 Type

🔹 VARCHAR

##### 1.7.3.5.3 Is Required

✅ Yes

##### 1.7.3.5.4 Is Primary Key

❌ No

##### 1.7.3.5.5 Is Unique

❌ No

##### 1.7.3.5.6 Index Type

None

##### 1.7.3.5.7 Size

255

##### 1.7.3.5.8 Constraints

*No items available*

##### 1.7.3.5.9 Default Value



##### 1.7.3.5.10 Is Foreign Key

❌ No

##### 1.7.3.5.11 Precision

0

##### 1.7.3.5.12 Scale

0

#### 1.7.3.6.0 Guid

##### 1.7.3.6.1 Name

executionId

##### 1.7.3.6.2 Type

🔹 Guid

##### 1.7.3.6.3 Is Required

❌ No

##### 1.7.3.6.4 Is Primary Key

❌ No

##### 1.7.3.6.5 Is Unique

❌ No

##### 1.7.3.6.6 Index Type

Index

##### 1.7.3.6.7 Size

0

##### 1.7.3.6.8 Constraints

*No items available*

##### 1.7.3.6.9 Default Value



##### 1.7.3.6.10 Is Foreign Key

❌ No

##### 1.7.3.6.11 Precision

0

##### 1.7.3.6.12 Scale

0

#### 1.7.3.7.0 DateTimeOffset

##### 1.7.3.7.1 Name

timestamp

##### 1.7.3.7.2 Type

🔹 DateTimeOffset

##### 1.7.3.7.3 Is Required

✅ Yes

##### 1.7.3.7.4 Is Primary Key

❌ No

##### 1.7.3.7.5 Is Unique

❌ No

##### 1.7.3.7.6 Index Type

Index

##### 1.7.3.7.7 Size

0

##### 1.7.3.7.8 Constraints

*No items available*

##### 1.7.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.7.3.7.10 Is Foreign Key

❌ No

##### 1.7.3.7.11 Precision

0

##### 1.7.3.7.12 Scale

0

### 1.7.4.0.0 Primary Keys

- securityViolationLogId
- timestamp

### 1.7.5.0.0 Unique Constraints

*No items available*

### 1.7.6.0.0 Indexes

#### 1.7.6.1.0 BTree

##### 1.7.6.1.1 Name

IX_SecurityViolationLog_ScriptId_Timestamp

##### 1.7.6.1.2 Columns

- transformationScriptId
- timestamp

##### 1.7.6.1.3 Type

🔹 BTree

#### 1.7.6.2.0 BTree

##### 1.7.6.2.1 Name

IX_SecurityViolationLog_ViolationType_TimestampDesc

##### 1.7.6.2.2 Columns

- violationType
- timestamp DESC

##### 1.7.6.2.3 Type

🔹 BTree

### 1.7.7.0.0 Partitioning

#### 1.7.7.1.0 Type

🔹 Range

#### 1.7.7.2.0 Columns

- timestamp

#### 1.7.7.3.0 Strategy

Implement monthly or quarterly range partitioning to maintain query performance and manage data lifecycle.

## 1.8.0.0.0 User

### 1.8.1.0.0 Name

User

### 1.8.2.0.0 Description

Represents a system user or administrator who performs actions on scripts and reports.

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

✅ Yes

##### 1.8.3.1.6 Index Type

UniqueIndex

##### 1.8.3.1.7 Size

0

##### 1.8.3.1.8 Constraints

*No items available*

##### 1.8.3.1.9 Default Value



##### 1.8.3.1.10 Is Foreign Key

❌ No

##### 1.8.3.1.11 Precision

0

##### 1.8.3.1.12 Scale

0

#### 1.8.3.2.0 VARCHAR

##### 1.8.3.2.1 Name

username

##### 1.8.3.2.2 Type

🔹 VARCHAR

##### 1.8.3.2.3 Is Required

✅ Yes

##### 1.8.3.2.4 Is Primary Key

❌ No

##### 1.8.3.2.5 Is Unique

✅ Yes

##### 1.8.3.2.6 Index Type

UniqueIndex

##### 1.8.3.2.7 Size

100

##### 1.8.3.2.8 Constraints

*No items available*

##### 1.8.3.2.9 Default Value



##### 1.8.3.2.10 Is Foreign Key

❌ No

##### 1.8.3.2.11 Precision

0

##### 1.8.3.2.12 Scale

0

#### 1.8.3.3.0 VARCHAR

##### 1.8.3.3.1 Name

email

##### 1.8.3.3.2 Type

🔹 VARCHAR

##### 1.8.3.3.3 Is Required

✅ Yes

##### 1.8.3.3.4 Is Primary Key

❌ No

##### 1.8.3.3.5 Is Unique

✅ Yes

##### 1.8.3.3.6 Index Type

UniqueIndex

##### 1.8.3.3.7 Size

255

##### 1.8.3.3.8 Constraints

*No items available*

##### 1.8.3.3.9 Default Value



##### 1.8.3.3.10 Is Foreign Key

❌ No

##### 1.8.3.3.11 Precision

0

##### 1.8.3.3.12 Scale

0

#### 1.8.3.4.0 VARCHAR

##### 1.8.3.4.1 Name

fullName

##### 1.8.3.4.2 Type

🔹 VARCHAR

##### 1.8.3.4.3 Is Required

✅ Yes

##### 1.8.3.4.4 Is Primary Key

❌ No

##### 1.8.3.4.5 Is Unique

❌ No

##### 1.8.3.4.6 Index Type

Index

##### 1.8.3.4.7 Size

200

##### 1.8.3.4.8 Constraints

*No items available*

##### 1.8.3.4.9 Default Value



##### 1.8.3.4.10 Is Foreign Key

❌ No

##### 1.8.3.4.11 Precision

0

##### 1.8.3.4.12 Scale

0

#### 1.8.3.5.0 VARCHAR

##### 1.8.3.5.1 Name

passwordHash

##### 1.8.3.5.2 Type

🔹 VARCHAR

##### 1.8.3.5.3 Is Required

✅ Yes

##### 1.8.3.5.4 Is Primary Key

❌ No

##### 1.8.3.5.5 Is Unique

❌ No

##### 1.8.3.5.6 Index Type

None

##### 1.8.3.5.7 Size

255

##### 1.8.3.5.8 Constraints

*No items available*

##### 1.8.3.5.9 Default Value



##### 1.8.3.5.10 Is Foreign Key

❌ No

##### 1.8.3.5.11 Precision

0

##### 1.8.3.5.12 Scale

0

#### 1.8.3.6.0 BOOLEAN

##### 1.8.3.6.1 Name

isActive

##### 1.8.3.6.2 Type

🔹 BOOLEAN

##### 1.8.3.6.3 Is Required

✅ Yes

##### 1.8.3.6.4 Is Primary Key

❌ No

##### 1.8.3.6.5 Is Unique

❌ No

##### 1.8.3.6.6 Index Type

Index

##### 1.8.3.6.7 Size

0

##### 1.8.3.6.8 Constraints

*No items available*

##### 1.8.3.6.9 Default Value

true

##### 1.8.3.6.10 Is Foreign Key

❌ No

##### 1.8.3.6.11 Precision

0

##### 1.8.3.6.12 Scale

0

#### 1.8.3.7.0 BOOLEAN

##### 1.8.3.7.1 Name

isDeleted

##### 1.8.3.7.2 Type

🔹 BOOLEAN

##### 1.8.3.7.3 Is Required

✅ Yes

##### 1.8.3.7.4 Is Primary Key

❌ No

##### 1.8.3.7.5 Is Unique

❌ No

##### 1.8.3.7.6 Index Type

Index

##### 1.8.3.7.7 Size

0

##### 1.8.3.7.8 Constraints

*No items available*

##### 1.8.3.7.9 Default Value

false

##### 1.8.3.7.10 Is Foreign Key

❌ No

##### 1.8.3.7.11 Precision

0

##### 1.8.3.7.12 Scale

0

#### 1.8.3.8.0 DateTimeOffset

##### 1.8.3.8.1 Name

createdAt

##### 1.8.3.8.2 Type

🔹 DateTimeOffset

##### 1.8.3.8.3 Is Required

✅ Yes

##### 1.8.3.8.4 Is Primary Key

❌ No

##### 1.8.3.8.5 Is Unique

❌ No

##### 1.8.3.8.6 Index Type

Index

##### 1.8.3.8.7 Size

0

##### 1.8.3.8.8 Constraints

*No items available*

##### 1.8.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.8.3.8.10 Is Foreign Key

❌ No

##### 1.8.3.8.11 Precision

0

##### 1.8.3.8.12 Scale

0

#### 1.8.3.9.0 DateTimeOffset

##### 1.8.3.9.1 Name

updatedAt

##### 1.8.3.9.2 Type

🔹 DateTimeOffset

##### 1.8.3.9.3 Is Required

✅ Yes

##### 1.8.3.9.4 Is Primary Key

❌ No

##### 1.8.3.9.5 Is Unique

❌ No

##### 1.8.3.9.6 Index Type

None

##### 1.8.3.9.7 Size

0

##### 1.8.3.9.8 Constraints

*No items available*

##### 1.8.3.9.9 Default Value

CURRENT_TIMESTAMP

##### 1.8.3.9.10 Is Foreign Key

❌ No

##### 1.8.3.9.11 Precision

0

##### 1.8.3.9.12 Scale

0

### 1.8.4.0.0 Primary Keys

- userId

### 1.8.5.0.0 Unique Constraints

#### 1.8.5.1.0 UC_User_Username

##### 1.8.5.1.1 Name

UC_User_Username

##### 1.8.5.1.2 Columns

- username

#### 1.8.5.2.0 UC_User_Email

##### 1.8.5.2.1 Name

UC_User_Email

##### 1.8.5.2.2 Columns

- email

### 1.8.6.0.0 Indexes

- {'name': 'IX_User_Active_NotDeleted', 'columns': ['isActive', 'isDeleted'], 'type': 'BTree'}

# 2.0.0.0.0 Relations

## 2.1.0.0.0 OneToMany

### 2.1.1.0.0 Name

TransformationScriptHasVersions

### 2.1.2.0.0 Id

REL_TS_SV_001

### 2.1.3.0.0 Source Entity

TransformationScript

### 2.1.4.0.0 Target Entity

ScriptVersion

### 2.1.5.0.0 Type

🔹 OneToMany

### 2.1.6.0.0 Source Multiplicity

1

### 2.1.7.0.0 Target Multiplicity

0..*

### 2.1.8.0.0 Cascade Delete

✅ Yes

### 2.1.9.0.0 Is Identifying

✅ Yes

### 2.1.10.0.0 On Delete

Cascade

### 2.1.11.0.0 On Update

Cascade

## 2.2.0.0.0 OneToOne

### 2.2.1.0.0 Name

TransformationScriptActiveVersion

### 2.2.2.0.0 Id

REL_TS_SV_002

### 2.2.3.0.0 Source Entity

TransformationScript

### 2.2.4.0.0 Target Entity

ScriptVersion

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

## 2.3.0.0.0 ManyToMany

### 2.3.1.0.0 Name

ReportUsesTransformationScript

### 2.3.2.0.0 Id

REL_RC_TS_001

### 2.3.3.0.0 Source Entity

ReportConfiguration

### 2.3.4.0.0 Target Entity

TransformationScript

### 2.3.5.0.0 Type

🔹 ManyToMany

### 2.3.6.0.0 Source Multiplicity

0..*

### 2.3.7.0.0 Target Multiplicity

0..*

### 2.3.8.0.0 Cascade Delete

✅ Yes

### 2.3.9.0.0 Is Identifying

❌ No

### 2.3.10.0.0 Join Table

#### 2.3.10.1.0 Name

ReportTransformationScriptAssociation

#### 2.3.10.2.0 Columns

##### 2.3.10.2.1 Guid

###### 2.3.10.2.1.1 Name

reportConfigurationId

###### 2.3.10.2.1.2 Type

🔹 Guid

###### 2.3.10.2.1.3 References

ReportConfiguration.reportConfigurationId

##### 2.3.10.2.2.0 Guid

###### 2.3.10.2.2.1 Name

transformationScriptId

###### 2.3.10.2.2.2 Type

🔹 Guid

###### 2.3.10.2.2.3 References

TransformationScript.transformationScriptId

### 2.3.11.0.0.0 On Delete

Cascade

### 2.3.12.0.0.0 On Update

Cascade

## 2.4.0.0.0.0 OneToMany

### 2.4.1.0.0.0 Name

UserCreatesTransformationScript

### 2.4.2.0.0.0 Id

REL_USER_TS_001

### 2.4.3.0.0.0 Source Entity

User

### 2.4.4.0.0.0 Target Entity

TransformationScript

### 2.4.5.0.0.0 Type

🔹 OneToMany

### 2.4.6.0.0.0 Source Multiplicity

1

### 2.4.7.0.0.0 Target Multiplicity

0..*

### 2.4.8.0.0.0 Cascade Delete

❌ No

### 2.4.9.0.0.0 Is Identifying

❌ No

### 2.4.10.0.0.0 On Delete

NoAction

### 2.4.11.0.0.0 On Update

Cascade

## 2.5.0.0.0.0 OneToMany

### 2.5.1.0.0.0 Name

UserUpdatesTransformationScript

### 2.5.2.0.0.0 Id

REL_USER_TS_002

### 2.5.3.0.0.0 Source Entity

User

### 2.5.4.0.0.0 Target Entity

TransformationScript

### 2.5.5.0.0.0 Type

🔹 OneToMany

### 2.5.6.0.0.0 Source Multiplicity

1

### 2.5.7.0.0.0 Target Multiplicity

0..*

### 2.5.8.0.0.0 Cascade Delete

❌ No

### 2.5.9.0.0.0 Is Identifying

❌ No

### 2.5.10.0.0.0 On Delete

NoAction

### 2.5.11.0.0.0 On Update

Cascade

## 2.6.0.0.0.0 OneToMany

### 2.6.1.0.0.0 Name

UserCreatesScriptVersion

### 2.6.2.0.0.0 Id

REL_USER_SV_001

### 2.6.3.0.0.0 Source Entity

User

### 2.6.4.0.0.0 Target Entity

ScriptVersion

### 2.6.5.0.0.0 Type

🔹 OneToMany

### 2.6.6.0.0.0 Source Multiplicity

1

### 2.6.7.0.0.0 Target Multiplicity

0..*

### 2.6.8.0.0.0 Cascade Delete

❌ No

### 2.6.9.0.0.0 Is Identifying

❌ No

### 2.6.10.0.0.0 On Delete

NoAction

### 2.6.11.0.0.0 On Update

Cascade

## 2.7.0.0.0.0 OneToMany

### 2.7.1.0.0.0 Name

UserPerformsAuditedAction

### 2.7.2.0.0.0 Id

REL_USER_AL_001

### 2.7.3.0.0.0 Source Entity

User

### 2.7.4.0.0.0 Target Entity

AuditLog

### 2.7.5.0.0.0 Type

🔹 OneToMany

### 2.7.6.0.0.0 Source Multiplicity

1

### 2.7.7.0.0.0 Target Multiplicity

0..*

### 2.7.8.0.0.0 Cascade Delete

❌ No

### 2.7.9.0.0.0 Is Identifying

❌ No

### 2.7.10.0.0.0 On Delete

SetNull

### 2.7.11.0.0.0 On Update

Cascade

## 2.8.0.0.0.0 OneToMany

### 2.8.1.0.0.0 Name

JsonSchemaValidatesReport

### 2.8.2.0.0.0 Id

REL_JS_RC_001

### 2.8.3.0.0.0 Source Entity

JsonSchema

### 2.8.4.0.0.0 Target Entity

ReportConfiguration

### 2.8.5.0.0.0 Type

🔹 OneToMany

### 2.8.6.0.0.0 Source Multiplicity

1

### 2.8.7.0.0.0 Target Multiplicity

0..*

### 2.8.8.0.0.0 Cascade Delete

❌ No

### 2.8.9.0.0.0 Is Identifying

❌ No

### 2.8.10.0.0.0 On Delete

SetNull

### 2.8.11.0.0.0 On Update

Cascade

## 2.9.0.0.0.0 OneToMany

### 2.9.1.0.0.0 Name

TransformationScriptHasSecurityViolations

### 2.9.2.0.0.0 Id

REL_TS_SVL_001

### 2.9.3.0.0.0 Source Entity

TransformationScript

### 2.9.4.0.0.0 Target Entity

SecurityViolationLog

### 2.9.5.0.0.0 Type

🔹 OneToMany

### 2.9.6.0.0.0 Source Multiplicity

1

### 2.9.7.0.0.0 Target Multiplicity

0..*

### 2.9.8.0.0.0 Cascade Delete

❌ No

### 2.9.9.0.0.0 Is Identifying

❌ No

### 2.9.10.0.0.0 On Delete

NoAction

### 2.9.11.0.0.0 On Update

Cascade

## 2.10.0.0.0.0 OneToMany

### 2.10.1.0.0.0 Name

ScriptVersionHasSecurityViolations

### 2.10.2.0.0.0 Id

REL_SV_SVL_001

### 2.10.3.0.0.0 Source Entity

ScriptVersion

### 2.10.4.0.0.0 Target Entity

SecurityViolationLog

### 2.10.5.0.0.0 Type

🔹 OneToMany

### 2.10.6.0.0.0 Source Multiplicity

1

### 2.10.7.0.0.0 Target Multiplicity

0..*

### 2.10.8.0.0.0 Cascade Delete

❌ No

### 2.10.9.0.0.0 Is Identifying

❌ No

### 2.10.10.0.0.0 On Delete

NoAction

### 2.10.11.0.0.0 On Update

Cascade

## 2.11.0.0.0.0 OneToMany

### 2.11.1.0.0.0 Name

UserCreatesReportConfiguration

### 2.11.2.0.0.0 Id

REL_USER_RC_001

### 2.11.3.0.0.0 Source Entity

User

### 2.11.4.0.0.0 Target Entity

ReportConfiguration

### 2.11.5.0.0.0 Type

🔹 OneToMany

### 2.11.6.0.0.0 Source Multiplicity

1

### 2.11.7.0.0.0 Target Multiplicity

0..*

### 2.11.8.0.0.0 Cascade Delete

❌ No

### 2.11.9.0.0.0 Is Identifying

❌ No

### 2.11.10.0.0.0 On Delete

NoAction

### 2.11.11.0.0.0 On Update

Cascade

## 2.12.0.0.0.0 OneToMany

### 2.12.1.0.0.0 Name

UserUpdatesReportConfiguration

### 2.12.2.0.0.0 Id

REL_USER_RC_002

### 2.12.3.0.0.0 Source Entity

User

### 2.12.4.0.0.0 Target Entity

ReportConfiguration

### 2.12.5.0.0.0 Type

🔹 OneToMany

### 2.12.6.0.0.0 Source Multiplicity

1

### 2.12.7.0.0.0 Target Multiplicity

0..*

### 2.12.8.0.0.0 Cascade Delete

❌ No

### 2.12.9.0.0.0 Is Identifying

❌ No

### 2.12.10.0.0.0 On Delete

NoAction

### 2.12.11.0.0.0 On Update

Cascade

## 2.13.0.0.0.0 OneToMany

### 2.13.1.0.0.0 Name

UserCreatesJsonSchema

### 2.13.2.0.0.0 Id

REL_USER_JS_001

### 2.13.3.0.0.0 Source Entity

User

### 2.13.4.0.0.0 Target Entity

JsonSchema

### 2.13.5.0.0.0 Type

🔹 OneToMany

### 2.13.6.0.0.0 Source Multiplicity

1

### 2.13.7.0.0.0 Target Multiplicity

0..*

### 2.13.8.0.0.0 Cascade Delete

❌ No

### 2.13.9.0.0.0 Is Identifying

❌ No

### 2.13.10.0.0.0 On Delete

NoAction

### 2.13.11.0.0.0 On Update

Cascade

## 2.14.0.0.0.0 OneToMany

### 2.14.1.0.0.0 Name

UserAssociatesReportAndScript

### 2.14.2.0.0.0 Id

REL_USER_RTSA_001

### 2.14.3.0.0.0 Source Entity

User

### 2.14.4.0.0.0 Target Entity

ReportTransformationScriptAssociation

### 2.14.5.0.0.0 Type

🔹 OneToMany

### 2.14.6.0.0.0 Source Multiplicity

1

### 2.14.7.0.0.0 Target Multiplicity

0..*

### 2.14.8.0.0.0 Cascade Delete

❌ No

### 2.14.9.0.0.0 Is Identifying

❌ No

### 2.14.10.0.0.0 On Delete

NoAction

### 2.14.11.0.0.0 On Update

Cascade

