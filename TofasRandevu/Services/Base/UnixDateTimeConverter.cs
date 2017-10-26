using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TofasRandevu.Services.Base
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime UnixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime )
            {
                DateTime date = ((DateTime)value).ToUniversalTime();
                long totalMilliseconds = (long)(date - UnixStartTime).TotalMilliseconds;
                if (totalMilliseconds > 0)
                {
                    writer.WriteValue(totalMilliseconds);
                }
                else
                {
                    int? nullInt = null;
                    writer.WriteValue(nullInt);
                }
            }
            else
            {
                throw new ArgumentException("value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.Integer)
            {
                throw new Exception("Invalid token. Expected integer");
            }

            double totalSeconds = 0;

            try
            {
                totalSeconds = Convert.ToDouble(reader.Value, CultureInfo.CurrentCulture);
            }
            catch
            {
                throw new Exception("Invalid double value.");
            }
            var dof = new DateTimeOffset(UnixStartTime.AddSeconds(totalSeconds / 1000d), new TimeSpan(0));
            var dtGmt = dof.ToLocalTime().DateTime;
            return dtGmt;
        }
    }
}