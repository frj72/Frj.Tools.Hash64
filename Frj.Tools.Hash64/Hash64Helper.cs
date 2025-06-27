using System;

namespace Frj.Tools.Hash64;

public static class Hash64Helper
{ 
    public static ulong Hash(params object?[] objects)
    {
        var hash = new Hash64();
        hash.Append(objects);
        return hash.GetCurrentHashAsUInt64();
    }

    public static long HashToLong(params object?[] objects)
    {
        var hash = new Hash64();
        hash.Append(objects);
        return hash.GetCurrentHashAsInt64();
    }

    public static ulong Hash<T>(T val) where T : struct
    {
        var hash = new Hash64();
        switch (val)
        {
            case bool b:
                hash.Append(b);
                break;
            case DateTime dt:
                hash.Append(dt);
                break;
            case DateTimeOffset dto:
                hash.Append(dto);
                break;
            default:
                hash.Append<T>(val);
                break;
        }
        return hash.GetCurrentHashAsUInt64();
    }
    
}
