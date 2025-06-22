using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Text;

namespace Frj.Tools.Hash64;

public class Hash64
{
    private static readonly byte[] NullBytes =
        [0x1a, 0x9a, 0x76, 0x30, 0xe5, 0x64, 0x11, 0xef, 0x8d, 0xf9, 0xfa, 0xa2, 0x25, 0x38, 0x81, 0x42];

    private readonly XxHash64 _xxHash64 = new();

    public void Append(bool? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(byte? value) => _xxHash64.Append(value is null ? NullBytes : [value.Value]);

    public void Append(sbyte? value) => _xxHash64.Append(value is null ? NullBytes : [(byte)value.Value]);

    public void Append(char? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(short? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(ushort? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(int? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(uint? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(long? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(ulong? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(float? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(double? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value));

    public void Append(decimal? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }
        foreach (var bit in decimal.GetBits(value.Value)) 
            _xxHash64.Append(BitConverter.GetBytes(bit));
    }
    public void Append(DateTime? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value.Ticks));

    public void Append(string? value) => _xxHash64.Append(value is null ? NullBytes : Encoding.UTF8.GetBytes(value));

    public void Append(byte[]? value) => _xxHash64.Append(value ?? NullBytes);

    public void Append(Guid? value) => _xxHash64.Append(value is null ? NullBytes : value.Value.ToByteArray());

    public void AppendNull() => _xxHash64.Append(NullBytes);

    public void Append(DateTimeOffset? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value.Ticks));

    public void Append(TimeSpan? value) => _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value.Ticks));

    public void Append<T>(T? value) where T : struct
    {
        if (value is null)
        {
            AppendNull();
            return;
        }
        var size = Marshal.SizeOf<T>();
        var ptr = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.StructureToPtr(value, ptr, false);
            var bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            _xxHash64.Append(bytes);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

        

    public void Append(params object?[] objects)
    {
        foreach (var o in objects)
        {
            switch (o)
            {
                case bool b:
                    Append(b);
                    break;

                case byte by:
                    Append(by);
                    break;

                case sbyte sb:
                    Append(sb);
                    break;

                case char c:
                    Append(c);
                    break;

                case short s:
                    Append(s);
                    break;

                case ushort us:
                    Append(us);
                    break;

                case int i:
                    Append(i);
                    break;

                case uint ui:
                    Append(ui);
                    break;

                case long l:
                    Append(l);
                    break;

                case ulong ul:
                    Append(ul);
                    break;

                case float f:
                    Append(f);
                    break;

                case double d:
                    Append(d);
                    break;

                case decimal dc:
                    Append(dc);
                    break;

                case DateTime dt:
                    Append(dt);
                    break;

                case DateTimeOffset dto:
                    Append(dto);
                    break;

                case TimeSpan ts:
                    Append(ts);
                    break;

                case string str:
                    Append(str);
                    break;

                case byte[] ba:
                    Append(ba);
                    break;

                case Guid g:
                    Append(g);
                    break;
                    
                case object[] oa:
                    Append(oa);
                    break;
                    

                case null:
                    AppendNull();
                    break;
                    
                default:
                    throw new NotSupportedException($"Type {o?.GetType().FullName} is not supported by {nameof(Hash64)}.");
            }
        }
    }
    public ulong GetCurrentHashAsUInt64()
    {
        return _xxHash64.GetCurrentHashAsUInt64();
    }
        
}