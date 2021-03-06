﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace Blazorise.Utils
{
    public static class Parsers
    {
        /// <summary>
        /// Internal date format. Compatible with HTML date inputs.
        /// </summary>
        public const string InternalDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Default date format.
        /// </summary>
        public const string ExternalDateFormat = "dd.MM.yyyy";

        /// <summary>
        /// Internal time format. Compatible with HTML time inputs.
        /// </summary>
        public const string InternalTimeFormat = "hh\\:mm\\:ss";

        /// <summary>
        /// Possible date formats.
        /// </summary>
        public static readonly string[] SupportedDateFormats = new string[]
        {
            InternalDateFormat,
            ExternalDateFormat,
            "yyyy-MM-ddTHH:mm",
            CultureInfo.InvariantCulture.DateTimeFormat.LongDatePattern,
            CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
            "o", // a string representing UTC
        };

        /// <summary>
        /// Possible time formats.
        /// </summary>
        public static readonly string[] SupportedTimeFormats = new string[]
        {
            InternalTimeFormat,
            CultureInfo.InvariantCulture.DateTimeFormat.LongTimePattern,
            CultureInfo.InvariantCulture.DateTimeFormat.ShortTimePattern,
        };

        public static bool TryParseDate( string value, out DateTime? result )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                result = null;
                return false;
            }

            if ( DateTime.TryParseExact( value, SupportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt ) )
            {
                result = dt;
                return true;
            }

            if ( DateTimeOffset.TryParse( value, out var dto ) )
            {
                result = dto.DateTime;
                return true;
            }

            result = null;

            return false;
        }

        public static bool TryParseDate<TValue>( string value, out TValue result )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                result = default;
                return false;
            }

            if ( DateTime.TryParseExact( value, SupportedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt ) )
            {
                result = (TValue)(object)dt;
                return true;
            }

            if ( DateTimeOffset.TryParse( value, out var dto ) )
            {
                result = (TValue)(object)dto.DateTime;
                return true;
            }

            result = default;

            return false;
        }

        public static bool TryParseTime<TValue>( string value, out TValue result )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                result = default;
                return false;
            }

            if ( TimeSpan.TryParseExact( value, SupportedTimeFormats, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time ) )
            {
                result = (TValue)(object)time;
                return true;
            }

            if ( DateTime.TryParseExact( value, SupportedTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt ) )
            {
                result = (TValue)(object)dt;
                return true;
            }

            result = default;

            return false;
        }
    }
}
