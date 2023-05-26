using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OLEDWindowsApplication;

public class KeyJsonConverter //: JsonConverter<Bitmap>
{/*
    public override Bitmap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var key = new Bitmap();
        DateTimeOffset.ParseExact(reader.GetString()!,
            "MM/dd/yyyy", CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, Bitmap value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }*/
}