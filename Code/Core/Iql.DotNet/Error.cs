using System;
using System.Globalization;

namespace Iql.DotNet
{
    /// <summary>
    ///     Utility class for creating and unwrapping <see cref="Exception" /> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        ///     Formats the specified resource string using <see cref="M:CultureInfo.CurrentCulture" />.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The formatted string.</returns>
        internal static string Format(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        ///     Creates an <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
        {
            return new NotSupportedException(Format(messageFormat, messageArgs));
        }
    }
}