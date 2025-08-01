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
            case char c:
                hash.Append(c);
                break;
            case uint ui:
                hash.Append(ui);
                break;
            case short s:
                hash.Append(s);
                break;
            case ulong ul:
                hash.Append(ul);
                break;
            case float f:
                hash.Append(f);
                break;
            case int i:
                hash.Append(i);
                break;
            case TimeSpan ts:
                hash.Append(ts);
                break;
            case ushort us:
                hash.Append(us);
                break;
            case Guid g:
                hash.Append(g);
                break;
            case long l:
                hash.Append(l);
                break;
            case byte b:
                hash.Append(b);
                break;
            case double d:
                hash.Append(d);
                break;
            case decimal dc:
                hash.Append(dc);
                break;
            case sbyte sb:
                hash.Append(sb);
                break;
            default:
                hash.Append<T>(val);
                break;
        }
        return hash.GetCurrentHashAsUInt64();
    }
    
    
}
