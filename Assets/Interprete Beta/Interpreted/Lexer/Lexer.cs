namespace Interpreted;
using System;
public class Lexer
{
        private static readonly List<Token> ListTokens = new List<Token>();
        public static List<Token> LexicalAnalysis(string input)
        {
            string[] linesOfInput = input.Split('\n');
            for (int i = 0; i < linesOfInput.Length; i++)
            {
                LexicalAnalysis(linesOfInput[i], i + 1);
            }
            return ListTokens;
        }
        public static void LexicalAnalysis(string input, int line)
        {
            int column = 0;
            while (column < input.Length)
            {
                bool matched = false;
                if (input[column] == ' ' || input[column] == '\t' || input[column] == '\v' || input[column] == '\f' || input[column] == 'r')
                {
                    column++;
                    continue;
                }
                if (input[column] == '/' && column + 1 < input.Length)
                {
                    if (input[column + 1] == '/') return;
                }

                foreach (var token in Token.TokenStringDictionary)
                {
                    var match = token.Value.Match(input.Substring(column));
                    if (match.Success)
                    {
                        ListTokens.Add(new Token(token.Key, match.Groups[0].Value, line, column + 1));
                        column += match.Length;
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    throw new Exception($"Unexpected at position in Line {line}:Column {column + 1}");
                }
            }
        }
}
