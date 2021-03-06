﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace IndependentUtils.Configuration.Attributes
{
    public class AutogeneratePropertyAttribute : Attribute
    {
        /// <summary>
        /// Qualifies that the property is an option.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        private class OptionAttribute : Attribute
        {
        }

        private string _addName = "Add";

        private readonly string _name;

        public string Name => _name;

        public string AddItemName
        {
            get
            {
                return _addName;
            }
            set
            {
                _addName = value;
            }
        }

        [Option]
        public object DefaultValue { get; set; }

        [Option]
        public ConfigurationPropertyOptions Options { get; set; }

        [Option]
        public bool IsDefaultCollection { get; set; }

        [Option]
        public bool IsRequired { get; set; }

        public IEnumerable<Tuple<string, object>> GetAllValues()
        {
            return typeof(AutogeneratePropertyAttribute).GetProperties()
                .Where(t => t.GetCustomAttribute<OptionAttribute>() != null)
                .Select(t => Tuple.Create(t.Name, t.GetValue(this)))
                .Where(t => !IsDefaultValue(t.Item2));
        }

        public AutogeneratePropertyAttribute(string name) => _name = name;

        private static bool IsDefaultValue(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            return obj.Equals(GetDefaultValueOf(obj.GetType()));
        }

        private static object GetDefaultValueOf(Type elementType)
        {
            if (elementType.IsValueType)
            {
                return Activator.CreateInstance(elementType);
            }
            return null;
        }
    }
}
