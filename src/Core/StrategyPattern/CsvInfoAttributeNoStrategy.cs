using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.StrategyPattern.NoStrategy {

    public class User {
        private readonly Username username;

        public User(Username username) {
            this.username = username;
        }

        [CsvInfo(Formatting.EscapedText)]
        public string Username => username.Value;
        
        [CsvInfo(Formatting.ShortDate)]
        public DateTimeOffset Created { get; set; }

        [CsvInfo(Formatting.ThousandSeparator)]
        public decimal CurrentBalance { get; set; }
        
        public string ToCsv() {
            Dictionary<PropertyInfo, CsvInfoAttribute> properties = GetType()
                .GetProperties()
                .ToDictionary(
                    prop => prop,
                    info => info.GetCustomAttribute<CsvInfoAttribute>());

            string csvRow = string.Join(",", properties.Select(p => p.Value.Format(p.Key.GetValue(this))));
            return csvRow;
        }
    }
    
    
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvInfoAttribute : Attribute {
        private readonly Formatting formatting;

        public CsvInfoAttribute(Formatting formatting = Formatting.NoFormatting) {
            this.formatting = formatting;
        }

        public string Format(object context) => formatting switch {
            Formatting.EscapedText when context is string value => $"\"{value}\"",
            Formatting.ShortDate when context is DateTimeOffset value => $"{value:yyyy-MM-dd}",
            Formatting.ThousandSeparator when context is decimal value => $"{value:N0}",
            Formatting.NoFormatting => context?.ToString(),
            _ => context?.ToString() ?? ""
        };
    }
    
    public enum Formatting {
        NoFormatting,
        EscapedText,
        ShortDate,
        ThousandSeparator,
    }
}
















