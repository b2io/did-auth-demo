using System.Text;
using System.Text.Json;

namespace DidAuthDemo.Core.Common;

public static class ByteArrayUtility
{
    public static byte[] ObjectToByteArray(Object obj)
    {
        var objStr = JsonSerializer.Serialize(obj);

        return Encoding.ASCII.GetBytes(objStr);
    }

    public static T ByteArrayToObject<T>(byte[] ba)
    {
        var objStr = Encoding.ASCII.GetString(ba);
        return JsonSerializer.Deserialize<T>(objStr);
    }
}
