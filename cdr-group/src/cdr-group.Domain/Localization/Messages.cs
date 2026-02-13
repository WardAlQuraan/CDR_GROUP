namespace cdr_group.Domain.Localization
{
    public static class Messages
    {
        // General
        public const string NotFound = "not_found";
        public const string AlreadyExists = "already_exists";
        public const string UnexpectedError = "unexpected_error";
        public const string ValidationError = "validation_error";
        public const string Unauthorized = "unauthorized";
        public const string Forbidden = "forbidden";

        // Auth
        public const string InvalidCredentials = "invalid_credentials";
        public const string AccountDeactivated = "account_deactivated";
        public const string AccountLocked = "account_locked";
        public const string UsernameExists = "username_exists";
        public const string EmailExists = "email_exists";
        public const string InvalidRefreshToken = "invalid_refresh_token";
        public const string RefreshTokenExpired = "refresh_token_expired";
        public const string RefreshTokenNotFound = "refresh_token_not_found";
        public const string IncorrectCurrentPassword = "incorrect_current_password";
        public const string UserNotFound = "user_not_found";

        // Company
        public const string CompanyCodeExists = "company_code_exists";
        public const string CompanyNotFound = "company_not_found";
        public const string CompanyHasDepartments = "company_has_departments";

        // Department
        public const string DepartmentNotFound = "department_not_found";
        public const string DepartmentCodeExists = "department_code_exists";
        public const string DepartmentHasEmployees = "department_has_employees";
        public const string DepartmentHasSubDepartments = "department_has_sub_departments";
        public const string DepartmentCircularReference = "department_circular_reference";
        public const string DepartmentCannotBeOwnParent = "department_cannot_be_own_parent";
        public const string ParentDepartmentNotFound = "parent_department_not_found";
        public const string ParentDepartmentDifferentCompany = "parent_department_different_company";
        public const string DepartmentRequired = "department_required";

        // Employee
        public const string EmployeeCannotBeOwnManager = "employee_cannot_be_own_manager";
        public const string EmployeeCircularReference = "employee_circular_reference";
        public const string ManagerNotFound = "manager_not_found";
        public const string UserAlreadyLinked = "user_already_linked";
        public const string EmployeeCodeExists = "employee_code_exists";
        public const string PositionNotFound = "position_not_found";
        public const string SalaryBelowMinimum = "salary_below_minimum";
        public const string SalaryAboveMaximum = "salary_above_maximum";

        // Role
        public const string RoleNameExists = "role_name_exists";
        public const string SystemRoleCannotBeModified = "system_role_cannot_be_modified";
        public const string SystemRoleCannotBeDeleted = "system_role_cannot_be_deleted";
        public const string SystemRolePermissionsLocked = "system_role_permissions_locked";

        // Permission
        public const string PermissionNameExists = "permission_name_exists";

        // Position
        public const string PositionCodeExists = "position_code_exists";
        public const string PositionHasEmployees = "position_has_employees";
        public const string MinSalaryGreaterThanMax = "min_salary_greater_than_max";

        // SalaryHistory
        public const string EmployeeNotFoundForSalaryHistory = "employee_not_found_for_salary_history";

        private static readonly Dictionary<string, Dictionary<string, string>> _messages = new()
        {
            ["en"] = new Dictionary<string, string>
            {
                // General
                [NotFound] = "Resource not found.",
                [AlreadyExists] = "Resource already exists.",
                [UnexpectedError] = "An unexpected error occurred.",
                [ValidationError] = "One or more validation errors occurred.",
                [Unauthorized] = "Unauthorized access.",
                [Forbidden] = "Access forbidden.",

                // Auth
                [InvalidCredentials] = "Invalid username or password.",
                [AccountDeactivated] = "User account is deactivated.",
                [AccountLocked] = "User account is locked.",
                [UsernameExists] = "Username already exists.",
                [EmailExists] = "Email already exists.",
                [InvalidRefreshToken] = "Invalid refresh token.",
                [RefreshTokenExpired] = "Refresh token has expired or been revoked.",
                [RefreshTokenNotFound] = "Refresh token not found.",
                [IncorrectCurrentPassword] = "Current password is incorrect.",
                [UserNotFound] = "User not found.",

                // Company
                [CompanyCodeExists] = "Company code already exists.",
                [CompanyNotFound] = "Company not found.",
                [CompanyHasDepartments] = "Cannot delete company with departments. Please reassign departments first.",

                // Department
                [DepartmentNotFound] = "Department not found.",
                [DepartmentCodeExists] = "Department code already exists in this company.",
                [DepartmentHasEmployees] = "Cannot delete department with employees. Please reassign employees first.",
                [DepartmentHasSubDepartments] = "Cannot delete department with sub-departments. Please delete or reassign sub-departments first.",
                [DepartmentCircularReference] = "Cannot assign parent department: circular reference detected.",
                [DepartmentCannotBeOwnParent] = "A department cannot be its own parent.",
                [ParentDepartmentNotFound] = "Parent department not found.",
                [ParentDepartmentDifferentCompany] = "Parent department must belong to the same company.",
                [DepartmentRequired] = "Department is required.",

                // Employee
                [EmployeeCannotBeOwnManager] = "An employee cannot be their own manager.",
                [EmployeeCircularReference] = "Cannot assign manager: circular reference detected.",
                [ManagerNotFound] = "Manager not found.",
                [UserAlreadyLinked] = "User is already linked to another employee.",
                [EmployeeCodeExists] = "Employee code already exists.",
                [PositionNotFound] = "Position not found.",
                [SalaryBelowMinimum] = "The salary of the employee is less than the minimum salary of this position.",
                [SalaryAboveMaximum] = "The salary of the employee is greater than the maximum salary of this position.",

                // Role
                [RoleNameExists] = "Role name already exists.",
                [SystemRoleCannotBeModified] = "System roles cannot be modified.",
                [SystemRoleCannotBeDeleted] = "System roles cannot be deleted.",
                [SystemRolePermissionsLocked] = "System role permissions cannot be modified.",

                // Permission
                [PermissionNameExists] = "Permission name already exists.",

                // Position
                [PositionCodeExists] = "Position code already exists.",
                [PositionHasEmployees] = "Cannot delete position with employees. Please reassign employees first.",
                [MinSalaryGreaterThanMax] = "Minimum salary cannot be greater than maximum salary.",

                // SalaryHistory
                [EmployeeNotFoundForSalaryHistory] = "Employee not found for salary history record.",
            },

            ["ar"] = new Dictionary<string, string>
            {
                // General
                [NotFound] = "المورد غير موجود.",
                [AlreadyExists] = "المورد موجود بالفعل.",
                [UnexpectedError] = "حدث خطأ غير متوقع.",
                [ValidationError] = "حدث خطأ واحد أو أكثر في التحقق.",
                [Unauthorized] = "وصول غير مصرح به.",
                [Forbidden] = "الوصول محظور.",

                // Auth
                [InvalidCredentials] = "اسم المستخدم أو كلمة المرور غير صحيحة.",
                [AccountDeactivated] = "حساب المستخدم معطل.",
                [AccountLocked] = "حساب المستخدم مقفل.",
                [UsernameExists] = "اسم المستخدم موجود بالفعل.",
                [EmailExists] = "البريد الإلكتروني موجود بالفعل.",
                [InvalidRefreshToken] = "رمز التحديث غير صالح.",
                [RefreshTokenExpired] = "رمز التحديث منتهي الصلاحية أو تم إبطاله.",
                [RefreshTokenNotFound] = "رمز التحديث غير موجود.",
                [IncorrectCurrentPassword] = "كلمة المرور الحالية غير صحيحة.",
                [UserNotFound] = "المستخدم غير موجود.",

                // Company
                [CompanyCodeExists] = "رمز الشركة موجود بالفعل.",
                [CompanyNotFound] = "الشركة غير موجودة.",
                [CompanyHasDepartments] = "لا يمكن حذف شركة تحتوي على أقسام. يرجى إعادة تعيين الأقسام أولاً.",

                // Department
                [DepartmentNotFound] = "القسم غير موجود.",
                [DepartmentCodeExists] = "رمز القسم موجود بالفعل في هذه الشركة.",
                [DepartmentHasEmployees] = "لا يمكن حذف قسم يحتوي على موظفين. يرجى إعادة تعيين الموظفين أولاً.",
                [DepartmentHasSubDepartments] = "لا يمكن حذف قسم يحتوي على أقسام فرعية. يرجى حذف أو إعادة تعيين الأقسام الفرعية أولاً.",
                [DepartmentCircularReference] = "لا يمكن تعيين القسم الأب: تم اكتشاف مرجع دائري.",
                [DepartmentCannotBeOwnParent] = "لا يمكن للقسم أن يكون أباً لنفسه.",
                [ParentDepartmentNotFound] = "القسم الأب غير موجود.",
                [ParentDepartmentDifferentCompany] = "يجب أن ينتمي القسم الأب إلى نفس الشركة.",
                [DepartmentRequired] = "القسم مطلوب.",

                // Employee
                [EmployeeCannotBeOwnManager] = "لا يمكن للموظف أن يكون مديراً لنفسه.",
                [EmployeeCircularReference] = "لا يمكن تعيين المدير: تم اكتشاف مرجع دائري.",
                [ManagerNotFound] = "المدير غير موجود.",
                [UserAlreadyLinked] = "المستخدم مرتبط بالفعل بموظف آخر.",
                [EmployeeCodeExists] = "رمز الموظف موجود بالفعل.",
                [PositionNotFound] = "المنصب غير موجود.",
                [SalaryBelowMinimum] = "راتب الموظف أقل من الحد الأدنى لراتب هذا المنصب.",
                [SalaryAboveMaximum] = "راتب الموظف أكبر من الحد الأقصى لراتب هذا المنصب.",

                // Role
                [RoleNameExists] = "اسم الدور موجود بالفعل.",
                [SystemRoleCannotBeModified] = "لا يمكن تعديل أدوار النظام.",
                [SystemRoleCannotBeDeleted] = "لا يمكن حذف أدوار النظام.",
                [SystemRolePermissionsLocked] = "لا يمكن تعديل صلاحيات أدوار النظام.",

                // Permission
                [PermissionNameExists] = "اسم الصلاحية موجود بالفعل.",

                // Position
                [PositionCodeExists] = "رمز المنصب موجود بالفعل.",
                [PositionHasEmployees] = "لا يمكن حذف منصب يحتوي على موظفين. يرجى إعادة تعيين الموظفين أولاً.",
                [MinSalaryGreaterThanMax] = "لا يمكن أن يكون الحد الأدنى للراتب أكبر من الحد الأقصى.",

                // SalaryHistory
                [EmployeeNotFoundForSalaryHistory] = "الموظف غير موجود لسجل تاريخ الراتب.",
            }
        };

        public static string Get(string key, string language = "en")
        {
            var lang = language.StartsWith("ar") ? "ar" : "en";

            if (_messages.TryGetValue(lang, out var messages) && messages.TryGetValue(key, out var message))
                return message;

            if (_messages["en"].TryGetValue(key, out var fallback))
                return fallback;

            return key;
        }
    }
}
