using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
   public static class StringCalculator
   {
      /* Some general notes:
      
         I have assumed that the input is small enough that calling string.Split() on the input will not cause problems.
         Another way of solving this would be to read through the string as a stream and avoid the memory allocations.

         My solution (with linq) involves iterating through the collection more than once, if performance is thought to be an issue,
         another solution would be to iterate through the collection once, with a loop, and keep a list of negative numbers as well as the current total.
      */

      /// <summary>
      /// Sums a list of non-negative numbers < 1000 delimited by ',' or '\n'
      /// Custom delimiters may be declared in the first line of the string using the form '\\*\n' 
      /// Where * is a list of single characters to be used as delimiters when parsing the string.
      /// Any numbers > 1000 in the input are ignored in the final total.
      /// </summary>
      /// <param name="input">A string of delimited non-negative numbers</param>
      /// <returns>The sum of the valid integers in the input string</returns>
      public static int Add(string input)
      {
         if (string.IsNullOrEmpty(input))
         {
            return 0;
         }

         char[] delimiters = GetDelimiters(input);

         if (delimiters != standardDelimiters)
         {
            input = RemoveFirstLine(input);
         }

         IEnumerable<int> tokens = Tokenize(input, delimiters);

         RejectNegativeNumbers(tokens);

         return tokens.Where(x => x <= maxNumber).Sum();
      }

      private static void RejectNegativeNumbers(IEnumerable<int> tokens)
      {
         if (tokens.Any(i => i < 0))
         {
            throw new FormatException($"Negatives not allowed: {string.Join(',', tokens.Where(i => i < 0))}");
         }
      }

      private static IEnumerable<int> Tokenize(string input, char[] delimiters)
      {
         try
         {
            return input.Split(delimiters).Select(i => int.Parse(i));
         }
         catch (InvalidCastException)
         {
            throw new FormatException("The input string contains invalid characters.");
         }
      }

      private static string RemoveFirstLine(string input)
      {
         int i = input.IndexOf('\n');
         return input.Substring(i + 1);
      }

      private static char[] GetDelimiters(string input)
      {       
         var match = Regex.Match(input, customDelimiterPattern);

         return match.Success ? match.Groups[0].Value.ToCharArray() : standardDelimiters;
      }

      private const int maxNumber = 1000;
      private const string customDelimiterPattern = @"^//(.*)\n";
      private static readonly char[] standardDelimiters = { ',', '\n' };
   }
}
