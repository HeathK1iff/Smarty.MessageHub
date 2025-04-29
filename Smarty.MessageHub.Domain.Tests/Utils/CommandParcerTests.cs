using Smarty.MessageHub.Domain.Utils;
using Xunit;

namespace Smarty.MessageHub.Domain.Tests.Utils;

public class CommandParcerTests
{
    [Fact]
    public void TryToParce_PutEmptyValue_ReturnFalse()
    {
        var parcer = new CommandParcer();
        
        var actual = parcer.TryToParce(string.Empty, out var comamnd);

        Assert.False(actual);
    }
    
    [Fact]
    public void TryToParce_PutSomeTextValue_ReturnFalse()
    {
        var parcer = new CommandParcer();
        
        var actual = parcer.TryToParce("test", out var comamnd);

        Assert.False(actual);
    }

    [Theory]
    [InlineData("#test")]
    [InlineData("#test ")]
    [InlineData(" #test")]
    [InlineData(" #test ")]
    public void TryToParce_PutTestCommandWithoutParams_ReturnTrueWithoutParams(string commandText)
    {
        var parcer = new CommandParcer();
        
        var actual = parcer.TryToParce(commandText, out var comamnd);

        Assert.True(actual);
        Assert.Equal("test", comamnd.CommandName);
    }

    [Theory]
    [InlineData("#test x", new string[] { "x" })]
    [InlineData("#test x y", new string[] { "x", "y" })]
    [InlineData("#test x y z", new string[] { "x", "y", "z" })]
    public void TryToParce_PutTestCommandWithParams_ReturnTrueWithParams(string commandText, string[] expectedParams)
    {
        var parcer = new CommandParcer();
        
        var actual = parcer.TryToParce(commandText, out var comamnd);

        Assert.True(actual);
        Assert.Equal("test", comamnd.CommandName);
        Assert.Equal(expectedParams, comamnd.CommandParams);
    }

}

