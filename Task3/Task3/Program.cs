using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Class to represent each entry in the Symbol Table
class SymbolTableEntry
{
    public string Type { get; set; }       // Data type (int, float, etc.)
    public string VarName { get; set; }    // Variable name
    public string Value { get; set; }      // Assigned value
    public int LineNumber { get; set; }    // Line number of declaration
}

class Program
{
    // Function to check if a string contains a palindrome substring of length ≥ 3
    static bool HasPalindromeSubstring(string text)
    {
        // Outer loop: starting index of substring
        for (int i = 0; i < text.Length; i++)
        {
            // Inner loop: ending index of substring, ensuring length ≥ 3
            for (int j = i + 2; j < text.Length; j++)
            {
                string sub = text.Substring(i, j - i + 1); // Extract substring
                if (IsPalindrome(sub))                     // Check if it's a palindrome
                    return true;
            }
        }
        return false;
    }

    // Function to check if a given string is a palindrome
    static bool IsPalindrome(string str)
    {
        int left = 0;
        int right = str.Length - 1;

        while (left < right)
        {
            if (str[left] != str[right])  // If mismatch, it's not a palindrome
                return false;
            left++;
            right--;
        }

        return true; // All characters matched from both sides
    }

    static void Main()
    {
        List<SymbolTableEntry> symbolTable = new List<SymbolTableEntry>(); // List to store entries
        int lineNumber = 0; // To track line numbers

        Console.WriteLine("Enter variable declarations one per line (e.g., int val33 = 999;)");
        Console.WriteLine("Type 'exit' to stop input and display the symbol table.\n");

        // Infinite loop to accept user input
        while (true)
        {
            Console.Write($"Line {lineNumber + 1}: ");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "exit")
                break; // Exit the loop if user types 'exit'

            lineNumber++;

            // Regex to match a variable declaration
            // Pattern explanation:
            // ^(int|float|char|string)     → Match start of line and type
            // \s+                          → One or more spaces
            // ([a-zA-Z_]\w*)               → Valid variable name (starts with letter or underscore)
            // \s*=\s*                      → Optional spaces around '='
            // (.+);                        → Value followed by semicolon
            Match match = Regex.Match(input, @"^(int|float|char|string)\s+([a-zA-Z_]\w*)\s*=\s*(.+);");

            if (match.Success)
            {
                string type = match.Groups[1].Value;
                string varName = match.Groups[2].Value;
                string value = match.Groups[3].Value.Trim();

                // Check if the variable name contains a palindrome substring
                if (HasPalindromeSubstring(varName))
                {
                    // Add to symbol table if condition is satisfied
                    symbolTable.Add(new SymbolTableEntry
                    {
                        Type = type,
                        VarName = varName,
                        Value = value,
                        LineNumber = lineNumber
                    });
                }
                else
                {
                    Console.WriteLine("➤ Variable name does not contain a palindrome substring of length ≥ 3. Not added.");
                }
            }
            else
            {
                Console.WriteLine("➤ Invalid syntax. Please enter in format: int varName = value;");
            }
        }

        // Displaying the Symbol Table
        Console.WriteLine("\n--- Symbol Table ---");
        Console.WriteLine("{0,-10} | {1,-12} | {2,-10} | {3}", "Type", "VarName", "Value", "Line No");
        Console.WriteLine(new string('-', 50));

        foreach (var entry in symbolTable)
        {
            Console.WriteLine($"{entry.Type,-10} | {entry.VarName,-12} | {entry.Value,-10} | {entry.LineNumber}");
        }
    }
}
