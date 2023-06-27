using System.Text;

namespace Formatters
{
    public static class TimeFormatter
    {
        private static readonly string _minutesSecondsSeparator = ":";
        private static readonly string _secondsMillisecondsSeparator = ".";
        private static readonly int _secondsInMinutes = 60;
        private static readonly int _mulForVisibleMilliseconds = 100;
        private static readonly int _mulForDividingNumber = 10;

        public static string GetTimeFormat(float allSeconds)
        {
            int minutes = (int)allSeconds / _secondsInMinutes;
            int seconds = (int)allSeconds % _secondsInMinutes;
            int milliseconds = (int)((allSeconds - (int)allSeconds) * _mulForVisibleMilliseconds);
            var builder = new StringBuilder();
            AppendCorrectNumber(minutes, builder);
            builder.Append(_minutesSecondsSeparator);
            AppendCorrectNumber(seconds, builder);
            builder.Append(_secondsMillisecondsSeparator);
            AppendCorrectNumber(milliseconds, builder);
            return builder.ToString();
        }

        private static void AppendCorrectNumber(int number, StringBuilder builder)
        {
            builder.Append(number / _mulForDividingNumber);
            builder.Append(number % _mulForDividingNumber);
        }
    }
}
