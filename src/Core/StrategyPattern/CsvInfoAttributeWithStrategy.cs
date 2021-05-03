using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.StrategyPattern.WithStrategy {

    public class User {
        private readonly Username username;

        public User(Username username) {
            this.username = username;
        }

        [CsvInfo(typeof(EscapedText))]
        public string Username => username.Value;
        
        [CsvInfo(typeof(ShortDate))]
        public DateTimeOffset Created { get; set; }

        [CsvInfo(typeof(ThousandSeparator))]
        public decimal CurrentBalance { get; set; }
        
        public string ToCsv() {
            Dictionary<PropertyInfo, CsvInfoAttribute> properties = GetType()
                .GetProperties()
                .ToDictionary(
                    prop => prop,
                    info => info.GetCustomAttribute<CsvInfoAttribute>());

            string csvRow = string.Join(",", properties.Select(p => p.Value.Formatter.Format(p.Key.GetValue(this))));
            return csvRow;
        }
    }
    

    [AttributeUsage(AttributeTargets.Property)]
    public class CsvInfoAttribute : Attribute {
        public CsvInfoAttribute(Type formatter) {
            formatter ??= typeof(NoFormatting);
            if (!formatter.IsAssignableFrom(typeof(IValueFormatter)))
                throw new ArgumentException($"Must implement {nameof(IValueFormatter)}", nameof(formatter));

            bool noDefaultConstructor = formatter.GetConstructor(Type.EmptyTypes) is null;
            if (noDefaultConstructor)
                throw new ArgumentException("Must have a default constructor", nameof(formatter));
            
            Formatter = (IValueFormatter) Activator.CreateInstance(formatter);
        }

        public IValueFormatter Formatter { get; }
    }

    public interface IValueFormatter {
        string Format(object value);
    }
    
    public class NoFormatting : IValueFormatter {
        public string Format(object value) => $"{value}";
    }

    public class EscapedText : IValueFormatter {
        public string Format(object value) => $"\"{value}\"";
    }

    public class ShortDate : IValueFormatter {
        public string Format(object value) => $"{value:yyyy-MM-dd}";
    }
    
    public class ThousandSeparator : IValueFormatter {
        public string Format(object value) => $"{value:N0}";
    }
}