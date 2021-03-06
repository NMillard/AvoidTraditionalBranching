using System;
using Core.StrategyPattern.NoStrategy;
using Xunit;

namespace DemoTests {
    public class CsvAttributeNoStrategy {
        [Fact]
        public void GenerateCsvLine() {
            var user = new User("nmillard") {
                Created = DateTimeOffset.Parse("2021/10/31"),
                CurrentBalance = 1_000,
            };

            string result = user.ToCsv();
            
            Assert.Equal("\"nmillard\",2021-10-31,1.000", result);
        }
    }
}