using System;
using Xunit;

namespace StringCalculator.Tests
{
   public class StringCalculatorTests
   {
      [Fact]
      public void Add_EmptyString_Returns0()
      {
         // arrange
         var input = string.Empty;

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(0, result);
      }

      [Theory]
      [InlineData("0")]
      [InlineData("1")]
      [InlineData("2")]
      [InlineData("5")]
      [InlineData("10")]
      [InlineData("151")]
      [InlineData("1000")]
      public void Add_SingleNumber_ReturnsNumber(string input)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(int.Parse(input), result);
      }

      [Theory]
      [InlineData("1,2", 3)]
      [InlineData("1,2,3", 6)]
      [InlineData("1,2,3,4", 10)]
      [InlineData("1,2,3,4,5", 15)]
      public void Add_CommaDelimitedNumbers_ReturnsSum(string input, int expected)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(expected, result);
      }

      [Theory]
      [InlineData("1\n2", 3)]
      [InlineData("1\n2\n3", 6)]
      [InlineData("1\n2\n3\n4", 10)]
      [InlineData("1\n2\n3\n4\n5", 15)]
      public void Add_NewlineDelimitedNumbers_ReturnsSum(string input, int expected)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(expected, result);
      }

      [Theory]
      [InlineData("1,\n")]
      [InlineData("1\n,")]
      [InlineData("1\n\n")]
      public void Add_ConsecutiveDelimiters_ThrowsFormatException(string input)
      {
         // arrange

         // act
         Action act = () => StringCalculator.Add(input);

         // assert
         Assert.Throws<FormatException>(act);
      }

      [Theory]
      [InlineData("//;\n1;2;3", 6)]
      [InlineData("//j\n1j2j3", 6)]
      [InlineData("//@\n1@2@3", 6)]
      public void Add_CustomDelimiter_ReturnsSum(string input, int expected)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(expected, result);
      }

      [Theory]
      [InlineData("1,2,j")]
      [InlineData("1,2,@")]
      public void Add_InvalidCharacters_ThrowsFormatException(string input)
      {
         // arrange

         // act
         Action act = () => StringCalculator.Add(input);

         // assert
         Assert.Throws<FormatException>(act);
      }

      [Theory]
      [InlineData("-1", "-1")]
      [InlineData("1,-2,-3", "-2,-3")]
      [InlineData("-1,999,-3", "-1,-3")]
      public void Add_NegativeNumbers_ThrowsFormatException(string input, string invalidNumbers)
      {
         // arrange

         // act
         Action act = () => StringCalculator.Add(input);

         // assert
         var exception = Assert.Throws<FormatException>(act);
         Assert.Contains(invalidNumbers, exception.Message);
      }

      [Theory]
      [InlineData("1,1000", 1001)]
      [InlineData("1,1001", 1)]
      [InlineData("1,2,3,1001", 6)]
      public void Add_LargeNumbers_IgnoresLargeNumbers(string input, int expected)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(expected, result);
      }

      [Theory]
      [InlineData("//;j@\n1;2j3", 6)]
      [InlineData("//j;@\n1@2j3", 6)]
      [InlineData("//@j;\n1@2;3", 6)]
      public void Add_MultipleCustomDelimiters_ReturnsSum(string input, int expected)
      {
         // arrange

         // act
         var result = StringCalculator.Add(input);

         // assert
         Assert.Equal(expected, result);
      }
   }
}
