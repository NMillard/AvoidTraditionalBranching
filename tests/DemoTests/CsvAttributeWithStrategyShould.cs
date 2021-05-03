using System;
using Core.StrategyPattern.WithStrategy;
using Xunit;

namespace DemoTests {
    public class CsvAttributeWithStrategyShould {
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