using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace DidAuthDemo.App.Common;

public static class Utility
{
    public static byte[] ObjectToByteArray(Object obj)
    {
        var objStr = JsonSerializer.Serialize(obj);

        return Encoding.UTF8.GetBytes(objStr);
    }

    public static T ByteArrayToObject<T>(byte[] ba)
    {
        var objStr = Encoding.UTF8.GetString(ba);
        return JsonSerializer.Deserialize<T>(objStr);
    }
}
