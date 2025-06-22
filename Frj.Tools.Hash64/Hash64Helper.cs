using System;

namespace Frj.Tools.Hash64;

public static class Hash64Helper
{

    public static ulong Hash(params object[] objects)
    {
        var hash = new Hash64();
        hash.Append(objects);
        return hash.GetCurrentHashAsUInt64();
    }
}
