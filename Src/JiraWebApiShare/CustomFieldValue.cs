using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Value of a custom field.
    /// </summary>
    public class CustomFieldValue
    {
        /// <summary>
        /// Value of the custom field value.
        /// </summary>
        public object? Value { get; private set; }

        /// <summary>
        /// Type of the custom field value Value.
        /// </summary>
        public CustomFieldType Type { get; private set; }

        #region constructors

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="type">Type of the parameter to initialize with.</param>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(CustomFieldType type, object? value = null)
        {
            this.Value = value;
            this.Type = type;
        }


        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(string value)
        {
            this.Value = value;
            this.Type = CustomFieldType.String;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(DateTime? value)
        {
            this.Value = value;
            this.Type = CustomFieldType.DateTime;
        }
        
        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(double? value)
        {
            this.Value = value;
            this.Type = CustomFieldType.Double;
        }
        
        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(Project value)
        {
            this.Value = value;
            this.Type = CustomFieldType.Project;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(IssueVersion value)
        {
            this.Value = value;
            this.Type = CustomFieldType.Version;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(User value)
        {
            this.Value = value;
            this.Type = CustomFieldType.User;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(Group value)
        {
            this.Value = value;
            this.Type = CustomFieldType.Group;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(IEnumerable<string> value)
        {
            this.Value = value == null ? null : value.ToArray();
            this.Type = CustomFieldType.StringArray;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(IEnumerable<Group> value)
        {
            this.Value = value == null ? null : value.ToArray();
            this.Type = CustomFieldType.GroupArray;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(IEnumerable<User> value)
        {
            this.Value = value == null ? null : value.ToArray();
            this.Type = CustomFieldType.UserArray;
        }

        /// <summary>
        /// Initializes a new instance of the CustomFieldValue class.
        /// </summary>
        /// <param name="value">Parameter to initialize with.</param>
        public CustomFieldValue(IEnumerable<IssueVersion> value)
        {
            this.Value = value == null ? null : value.ToArray();
            this.Type = CustomFieldType.VersionArray;
        }

        #endregion

        #region implicit casts

        /// <summary>
        /// Case string value to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(string value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case string array to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(string[] value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case double value to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(double? value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case DateTime value to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(DateTime? value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case Project to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(Project value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case Group to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(Group value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case Group array to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(Group[] value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case User to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(User value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case User array to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(User[] value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case Version to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(IssueVersion value)
        {
            return new CustomFieldValue(value);
        }

        /// <summary>
        /// Case Version array to CustomFieldValue.
        /// </summary>
        /// <param name="value">Value to cast from.</param>
        /// <returns>CustomFieldValue to cast to.</returns>
        public static implicit operator CustomFieldValue(IssueVersion[] value)
        {
            return new CustomFieldValue(value);
        }
        
        #endregion

        #region explicit casts

        /// <summary>
        /// Get string value from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator string(CustomFieldValue value)
        {
            return (string)value.Value!;
        }

        /// <summary>
        /// Get double value from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator double?(CustomFieldValue value)
        {
            return (double?)value.Value;
        }

        /// <summary>
        /// Get DateTime value from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator DateTime?(CustomFieldValue value)
        {
            return (DateTime?)value.Value;
        }

        /// <summary>
        /// Get Project from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator Project(CustomFieldValue value)
        {
            return (Project)value.Value!;
        }

        /// <summary>
        /// Get Version from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator IssueVersion(CustomFieldValue value)
        {
            return (IssueVersion)value.Value!;
        }
        
        /// <summary>
        /// Get Version array from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator IssueVersion[](CustomFieldValue value)
        {
            return (IssueVersion[])value.Value!;
        }
        
        /// <summary>
        /// Get User from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator User(CustomFieldValue value)
        {
            return (User)value.Value!;
        }

        /// <summary>
        /// Get User array from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator User[](CustomFieldValue value)
        {
            return (User[])value.Value!;
        }

        /// <summary>
        /// Get Group from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator Group(CustomFieldValue value)
        {
            return (Group)value.Value!;
        }

        /// <summary>
        /// Get Group array from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator Group[](CustomFieldValue value)
        {
            return (Group[])value.Value!;
        }

        /// <summary>
        /// Get string array from custom field.
        /// </summary>
        /// <param name="value">Istance of CustomFieldValue to get value from.</param>
        /// <returns>Casted Value of CustomFieldValue.</returns>
        public static explicit operator string[](CustomFieldValue value)
        {
            //if (value.Type == CustomFieldType.OptionArray)
            //{
            //    IEnumerable<CustomFieldOption> customFieldOptions = (CustomFieldOption[])value.Value;
            //    return customFieldOptions.Select(c => c.Value).ToArray();
            //}
            return (string[])value.Value!;
        }

        #endregion

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is not CustomFieldValue element)
            {
                return false;
            }
            return this.Type == element.Type && this.Value == element.Value;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Compare equal operator.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the id of the first element is equal to the id of the second element; otherwise, false.</returns>
        public static bool operator ==(CustomFieldValue element1, CustomFieldValue element2)
        {
            if (ReferenceEquals(element1, element2))
            {
                return true;
            }
            if (((object)element1 == null) || ((object)element2 == null))
            {
                return false;
            }
            return element1.Equals(element2);
        }

        /// <summary>
        /// Compare equal operator.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        public static bool operator ==(CustomFieldValue element, string name)
        {
            return (object)element != null && element.Type == CustomFieldType.String && ((string)element.Value!) == name;
        }

        /// <summary>
        ///  Compare not equal operator.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
        public static bool operator !=(CustomFieldValue element1, CustomFieldValue element2)
        {
            return !(element1 == element2);
        }

        /// <summary>
        /// Compare not equal operator.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        public static bool operator !=(CustomFieldValue element, string name)
        {
            return !(element == name);
        }

        /// <summary>
        /// Compare less than operator to allow LINQ compare.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the first operand is less than to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator <(CustomFieldValue element1, CustomFieldValue element2)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare less than operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is less than to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator <(CustomFieldValue element, string name)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare less than or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the first operand is less than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator <=(CustomFieldValue element1, CustomFieldValue element2)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare less than or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is less than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator <=(CustomFieldValue element, string name)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare greater than operator to allow LINQ compare.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator >(CustomFieldValue element1, CustomFieldValue element2)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare greater than operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator >(CustomFieldValue element, string name)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare greater or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator >=(CustomFieldValue element1, CustomFieldValue element2)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }

        /// <summary>
        /// Compare greater or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static bool operator >=(CustomFieldValue element, string name)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison.");
        }
    }
}
