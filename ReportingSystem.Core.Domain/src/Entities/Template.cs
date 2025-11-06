namespace ReportingSystem.Core.Domain.Entities
{
    /// <summary>
    /// Represents a Handlebars template used for rendering HTML or PDF reports.
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Gets the unique identifier for the template.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the name of the template, typically the original filename.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content of the Handlebars template.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the template was last modified.
        /// </summary>
        public DateTimeOffset LastModifiedDate { get; set; }

        // Private constructor for EF Core
        private Template() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Template"/> class.
        /// </summary>
        /// <param name="name">The name of the template.</param>
        /// <param name="content">The content of the template.</param>
        public Template(string name, string content)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Template name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Template content cannot be empty.", nameof(content));
                
            Id = Guid.NewGuid();
            Name = name;
            Content = content;
            LastModifiedDate = DateTimeOffset.UtcNow;
        }
    }
}