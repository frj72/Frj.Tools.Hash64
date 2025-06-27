namespace Frj.Tools.Hash64.Tests;

public class TestHash64Helper
{
    public record struct StructRecord(int Value, double AnotherValue);
    [Fact]
    public async Task TestString()
    {
        var res = Hash64Helper.Hash("hello");
        await Verify(res);
    }
    
    [Fact]
    public async Task TestStrings() => await Verify(Hash64Helper.Hash("hello", "world"));

    [Fact]
    public async Task TestInt() => await Verify(Hash64Helper.Hash(42));

    [Fact]
    public async Task TestByte() => await Verify(Hash64Helper.Hash((byte)42));

    [Fact]
    public async Task TestSByte() => await Verify(Hash64Helper.Hash((sbyte)42));
    
    
    [Fact]
    public async Task TestLong() => await Verify(Hash64Helper.Hash(42L));
    
    [Fact]
    public async Task TestShort() => await Verify(Hash64Helper.Hash((short)42));
    
    [Fact]
    public async Task TestUShort() => await Verify(Hash64Helper.Hash((ushort)42));
    
    [Fact]
    public async Task TestUInt() => await Verify(Hash64Helper.Hash((uint)42));
    
    [Fact]
    public async Task TestULong() => await Verify(Hash64Helper.Hash((ulong)42));
    
    [Fact]
    public async Task TestDouble() => await Verify(Hash64Helper.Hash(42.0));
    
    [Fact]
    public async Task TestFloat() => await Verify(Hash64Helper.Hash(42f));
    
    [Fact]
    public async Task TestDecimal() => await Verify(Hash64Helper.Hash(42m));
    
    [Fact]
    public async Task TestBoolTrue() => await Verify(Hash64Helper.Hash(true));
    
    [Fact]
    public async Task TestBoolFalse() => await Verify(Hash64Helper.Hash(false));
    
    [Fact]
    public async Task TestChar() => await Verify(Hash64Helper.Hash('A'));
    
    [Fact]
    public async Task TestDateTime()
    {
        var dateTime = new DateTime(2021, 1, 1);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        await Verify(Hash64Helper.Hash(dateTime));
    }
    
    [Fact]
    public async Task TestDateTimeOffset()
    {
        var dateTime = new DateTime(2021, 1, 1);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        var dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
        await Verify(Hash64Helper.Hash(dateTimeOffset));
    }

    [Fact]
    public async Task TestTimeSpan() => await Verify(Hash64Helper.Hash(new TimeSpan(1, 2, 3)));
    
    [Fact]
    public async Task TestByteArray() => await Verify(Hash64Helper.Hash(new byte[] { 1, 2, 3 }));
    
    [Fact]
    public async Task TestGuid() => await Verify(Hash64Helper.Hash(Guid.Parse("00000000-0000-0000-0000-000000000000")));
    
    [Fact]
    public async Task TestNull() => await Verify(Hash64Helper.Hash((string?)null));

    [Fact]
    public async Task TestMixed()
    {
        int i = 42;
        var dateTime = new DateTime(2021, 1, 1);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        var dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
        var res = Hash64Helper.Hash(i, 42L, 42.0, 42.0M, true, false, 'a', dateTime, dateTimeOffset, new TimeSpan(1, 2, 3),
            new byte[] {1, 2, 3}, Guid.Parse("00000000-0000-0000-0000-000000000000"), (string?)null);
        await Verify(res);
    }

    [Fact]
    public async Task TestStruct()
    {
        var res = Hash64Helper.Hash(new StructRecord(42, 3.14));
        await Verify(res);
    }

    [Fact]
    public void TestDateTimeVsObj()
    {
        var dateTime = new DateTime(2021, 1, 1);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        //var dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
        var hash64 = new Hash64();
        hash64.Append(dateTime);
        Assert.Equal(Hash64Helper.Hash(dateTime), hash64.GetCurrentHashAsUInt64());
    }
    
    [Fact]
    public void TestDateOffsetTimeVsObj()
    {
        var dateTime = new DateTime(2021, 1, 1);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        var dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
        var hash64 = new Hash64();
        hash64.Append(dateTimeOffset);
        Assert.Equal(Hash64Helper.Hash(dateTimeOffset), hash64.GetCurrentHashAsUInt64());
    }

    [Fact]
    public void TestBoolTrueVsObj()
    {
        var hash64 = new Hash64();
        hash64.Append(true);
        Assert.Equal(Hash64Helper.Hash(true), hash64.GetCurrentHashAsUInt64());
    }
    
    [Fact]
    public void TestBoolFalseVsObj()
    {
        var hash64 = new Hash64();
        hash64.Append(false);
        Assert.Equal(Hash64Helper.Hash(false), hash64.GetCurrentHashAsUInt64());
    }
    
    [Fact]
    public async Task TestHashToLong()
    {
        var res = Hash64Helper.HashToLong("hello", "world");
        await Verify(res);
       
    }

}
