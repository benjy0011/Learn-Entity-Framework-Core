using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace efCore.API.Data.ValueConverters;

public class DateTimeToChar8Converter : ValueConverter<DateTime, string>
{
    // 'ctor' = constructor
    public DateTimeToChar8Converter() : base( // parse in two function to ValueConverter parent
        dateTime => dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
        stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture)
        )
    {
        
    }
}
