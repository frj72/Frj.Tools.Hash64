using System.Buffers;
using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.HighPerformance.Buffers;

namespace Frj.Tools.Hash64;

public class Hash64
{
    private static readonly byte[] NullBytes =
        [0x1a, 0x9a, 0x76, 0x30, 0xe5, 0x64, 0x11, 0xef, 0x8d, 0xf9, 0xfa, 0xa2, 0x25, 0x38, 0x81, 0x42];

    private readonly XxHash64 _xxHash64 = new();

    public void Append(bool? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[1];
        buffer[0] = value.Value ? (byte)1 : (byte)0;
        _xxHash64.Append(buffer);
    }

    public void Append(byte? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[1];
        buffer[0] = value.Value;
        _xxHash64.Append(buffer);
    }

    public void Append(sbyte? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[1];
        buffer[0] = (byte)value.Value;
        _xxHash64.Append(buffer);
    }

    public void Append(char? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(char)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(short? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(short)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(ushort? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(ushort)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(int? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(uint? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(uint)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(long? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(long)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(ulong? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(ulong)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(float? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(float)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }

    public void Append(double? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(double)];
        BitConverter.TryWriteBytes(buffer, value.Value);
        _xxHash64.Append(buffer);
    }


    public void Append(decimal? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }
        using var decimalBuffer = MemoryOwner<byte>.Allocate(16);
        var bits = decimal.GetBits(value.Value);
        for (var i = 0; i < 4; i++)
            BitConverter.TryWriteBytes(decimalBuffer.Span.Slice(i * 4, 4), bits[i]);
        _xxHash64.Append(decimalBuffer.Span);
    }

    public void Append(DateTime? value) =>
        _xxHash64.Append(value is null ? NullBytes : BitConverter.GetBytes(value.Value.Ticks));

    public void Append(string? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        var maxByteCount = Encoding.UTF8.GetMaxByteCount(value.Length);

        switch (maxByteCount)
        {
            case <= 1024:
            {
                Span<byte> buffer = stackalloc byte[maxByteCount];
                var len = Encoding.UTF8.GetBytes(value.AsSpan(), buffer);
                _xxHash64.Append(buffer[..len]);
                break;
            }
            default:
            {
                using var owner = MemoryPool<byte>.Shared.Rent(maxByteCount);
                var span = owner.Memory.Span;
                var len = Encoding.UTF8.GetBytes(value.AsSpan(), span);
                _xxHash64.Append(span[..len]);
                break;
            }
        }
    }

    public void Append(byte[]? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        _xxHash64.Append(value);
    }

    public void Append(Guid? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[16];
        value.Value.TryWriteBytes(buffer);
        _xxHash64.Append(buffer);
    }

    public void AppendNull()
    {
        _xxHash64.Append(NullBytes);
    }

    public void Append(DateTimeOffset? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(long)];
        BitConverter.TryWriteBytes(buffer, value.Value.Ticks);
        _xxHash64.Append(buffer);
    }

    public void Append(TimeSpan? value)
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        Span<byte> buffer = stackalloc byte[sizeof(long)];
        BitConverter.TryWriteBytes(buffer, value.Value.Ticks);
        _xxHash64.Append(buffer);
    }


    public void Append<T>(T? value) where T : struct
    {
        if (value is null)
        {
            AppendNull();
            return;
        }

        var tVal = value.Value;
        var buffer = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref tVal, 1));
        _xxHash64.Append(buffer);
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
                    throw new NotSupportedException(
                        $"Type {o?.GetType().FullName} is not supported by {nameof(Hash64)}.");
            }
        }
    }

    public void Reset()
    {
        _xxHash64.Reset();
    }

    public ulong GetCurrentHashAsUInt64() => _xxHash64.GetCurrentHashAsUInt64();

    public long GetCurrentHashAsInt64() => BitConverter.ToInt64(_xxHash64.GetCurrentHash(), 0);
}
