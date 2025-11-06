namespace ReportingSystem.Core.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Represents a user role, defining a set of permissions.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets the unique identifier for the role.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Represents the Administrator role with full system access.
        /// </summary>
        public static Role Administrator => new(1, "Administrator");

        /// <summary>
        /// Represents the Viewer role with read-only access to reports.
        /// </summary>
        public static Role Viewer => new(2, "Viewer");

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="id">The role identifier.</param>
        /// <param name="name">The role name.</param>
        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Private constructor for EF Core
        private Role() { }

        /// <summary>
        /// Gets a list of all predefined system roles.
        /// </summary>
        /// <returns>An enumerable collection of roles.</returns>
        public static IEnumerable<Role> List()
        {
            return new[] { Administrator, Viewer };
        }

        /// <summary>
        /// Gets a role from its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>The matching Role instance.</returns>
        /// <exception cref="ArgumentException">Thrown if the role name is not valid.</exception>
        public static Role FromName(string name)
        {
            var role = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (role == null)
            {
                throw new ArgumentException($"Possible values for Role: {string.Join(",", List().Select(s => s.Name))}");
            }

            return role;
        }

        /// <summary>
        /// Gets a role from its identifier.
        /// </summary>
        /// <param name="id">The identifier of the role.</param>
        /// <returns>The matching Role instance.</returns>
        /// <exception cref="ArgumentException">Thrown if the role id is not valid.</exception>
        public static Role From(int id)
        {
            var role = List().SingleOrDefault(s => s.Id == id);

            if (role == null)
            {
                throw new ArgumentException($"Possible values for Role: {string.Join(",", List().Select(s => s.Name))}");
            }

            return role;
        }
    }
}