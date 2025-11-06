namespace ReportingSystem.Core.Domain.Entities
{
    /// <summary>
    /// Represents a JSON Schema definition used for validation.
    /// </summary>
    public class JsonSchema
    {
        /// <summary>
        /// Gets the unique identifier for the JSON Schema.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the user-defined name for the schema.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content of the JSON Schema as a string.
        /// </summary>
        public string SchemaContent { get; set; }
        
        /// <summary>
        /// Gets the date and time when the schema was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        // Private constructor for EF Core
        private JsonSchema() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSchema"/> class.
        /// </summary>
        /// <param name="name">The name of the schema.</param>
        /// <param name="schemaContent">The JSON Schema content.</param>
        public JsonSchema(string name, string schemaContent)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Schema name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(schemaContent))
                throw new ArgumentException("Schema content cannot be empty.", nameof(schemaContent));
                
            Id = Guid.NewGuid();
            Name = name;
            SchemaContent = schemaContent;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}