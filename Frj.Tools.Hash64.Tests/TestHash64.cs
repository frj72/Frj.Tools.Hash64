namespace Frj.Tools.Hash64.Tests;

public class TestHash64
{
    public record struct StructRecord(int Value, double AnotherValue);

    [Fact]
    public async Task TestString()
    {
        var hash64 = new Hash64();
        hash64.Append("hello");
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestInt()
    {
        int val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestByte()
    {
        var hash64 = new Hash64();
        hash64.Append((byte)42);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestSByte()
    {
        sbyte val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestLong()
    {
        var hash64 = new Hash64();
        hash64.Append(42L);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestShort()
    {
        short val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestUShort()
    {
        ushort val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestUInt()
    {
        uint val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestULong()
    {
        ulong val = 42;
        var hash64 = new Hash64();
        hash64.Append(val);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestDouble()
    {
        var hash64 = new Hash64();
        hash64.Append(42.0);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestFloat()
    {
        var hash64 = new Hash64();
        hash64.Append(42.0f);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestDecimal()
    {
        var hash64 = new Hash64();
        hash64.Append(42.0M);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestBool()
    {
        var hash64 = new Hash64();
        hash64.Append(true);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestChar()
    {
        var hash64 = new Hash64();
        hash64.Append('a');
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestDateTime()
    {
        var hash64 = new Hash64();
        hash64.Append(new DateTime(2021, 1, 1));
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestDateTimeOffset()
    {
        var hash64 = new Hash64();
        hash64.Append(new DateTimeOffset(new DateTime(2021, 1, 1), TimeSpan.Zero));
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestTimeSpan()
    {
        var hash64 = new Hash64();
        hash64.Append(new TimeSpan(1, 2, 3));
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestByteArray()
    {
        var hash64 = new Hash64();
        hash64.Append([1, 2, 3]);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestGuid()
    {
        var hash64 = new Hash64();
        hash64.Append(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestNull()
    {
        var hash64 = new Hash64();
        hash64.AppendNull();
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestMixed()
    {
        int i = 42;
        var hash64 = new Hash64();
        hash64.Append("hello");
        hash64.Append(i);
        hash64.Append(42L);
        hash64.Append(42.0);
        hash64.Append(42.0M);
        hash64.Append(true);
        hash64.Append('a');
        hash64.Append(new DateTime(2021, 1, 1));
        hash64.Append(new DateTimeOffset(new DateTime(2021, 1, 1), TimeSpan.Zero));
        hash64.Append(new TimeSpan(1, 2, 3));
        hash64.Append([1, 2, 3]);
        hash64.Append(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        hash64.AppendNull();
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }
    
    [Fact]
    public async Task TestObjectArray()
    {
        var hash64 = new Hash64();
        hash64.Append("hello", 42, 42L, 42.0, 42.0M, true, 'a', new DateTime(2021, 1, 1), new DateTimeOffset(new DateTime(2021, 1, 1), TimeSpan.Zero), new TimeSpan(1, 2, 3), Guid.Parse("00000000-0000-0000-0000-000000000000"), null as int?);
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestStruct()
    {
        var hash64 = new Hash64();
        hash64.Append<StructRecord>(new StructRecord(42, 42.0));
        var res = hash64.GetCurrentHashAsUInt64();
        await Verify(res);
    }

    [Fact]
    public async Task TestHasAsInt64()
    {
        var hash64 = new Hash64();
        hash64.Append("hello", "world");
        var resAsLong = hash64.GetCurrentHashAsInt64();
        await Verify(resAsLong);
    }

    [Fact]
    public void TestReset()
    {
        var hash64 = new Hash64();
        hash64.Append("hello", "world");
        var resAsLong = hash64.GetCurrentHashAsInt64();
        hash64.Reset();
        hash64.Append("hello", "world");
        var secondResAsLong = hash64.GetCurrentHashAsInt64();
        Assert.Equal(resAsLong, secondResAsLong);
    }
}
