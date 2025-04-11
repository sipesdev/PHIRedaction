using System.Text.RegularExpressions;

namespace PHIRedaction.Services
{
    public class RedactionService
    {
        // TODO: Move all of this into separate classes
        private static readonly string _redactionPattern = "^(Patient Name: |Date of Birth: |Social Security Number: |Address: |Phone Number: |Email: |Medical Record Number: ).*$";
        private static readonly string _redactionReplacement = "[REDACTED]";

        // Compiled regex for all the performance gains
        private static readonly Regex _redactionRegex = new (pattern: _redactionPattern, options: RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

        /// <summary>
        /// Redacts sensitive information from the given text
        /// </summary>
        /// <param name="text">Text to be redacted</param>
        /// <returns>Redacted text in the form of a string</returns>
        public static string RedactText(string text)
        {
            var redactedText = text;

            if (_redactionRegex.IsMatch(text))
            {
                redactedText = _redactionRegex.Replace(text, _redactionReplacement);
            }

            return redactedText;
        }
    }
}