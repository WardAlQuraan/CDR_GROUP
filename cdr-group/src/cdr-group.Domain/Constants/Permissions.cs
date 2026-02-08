namespace cdr_group.Domain.Constants
{
    public static class Permissions
    {
        public static class Users
        {
            public const string Read = "users.read";
            public const string Create = "users.create";
            public const string Update = "users.update";
            public const string Delete = "users.delete";
            public const string Activate = "users.activate";
        }

        public static class Roles
        {
            public const string Read = "roles.read";
            public const string Manage = "roles.manage";
        }

        public static class Employees
        {
            public const string Read = "employees.read";
            public const string Create = "employees.create";
            public const string Update = "employees.update";
            public const string Delete = "employees.delete";
            public const string AssignManager = "employees.assign-manager";
            public const string LinkToUser = "employees.link-to-user";
        }

        public static class Departments
        {
            public const string Read = "departments.read";
            public const string Create = "departments.create";
            public const string Update = "departments.update";
            public const string Delete = "departments.delete";
            public const string AssignManager = "departments.assign-manager";
        }

        public static class Positions
        {
            public const string Read = "positions.read";
            public const string Create = "positions.create";
            public const string Update = "positions.update";
            public const string Delete = "positions.delete";
        }

        public static class Files
        {
            public const string Read = "files.read";
            public const string Upload = "files.upload";
            public const string Update = "files.update";
            public const string Delete = "files.delete";
        }

        public static class Companies
        {
            public const string Read = "companies.read";
            public const string Create = "companies.create";
            public const string Update = "companies.update";
            public const string Delete = "companies.delete";
        }

        public static class Events
        {
            public const string Read = "events.read";
            public const string Create = "events.create";
            public const string Update = "events.update";
            public const string Delete = "events.delete";
        }

        public static class ContactUs
        {
            public const string Read = "contactus.read";
            public const string Update = "contactus.update";
            public const string Delete = "contactus.delete";
        }
    }
}
