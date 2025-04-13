using Smarty.TelegramGate.Domain.Entities;
using Smarty.TelegramGate.Domain.Utils;
using Xunit;
using Message = Smarty.TelegramGate.Domain.Entities.Message;

namespace Smarty.TelegramGate.Domain.Tests.Utils;

public class MessageUtilsTests
{
    [Fact]
    public void TryGetSessionId_GetSessionId_AsExpected()
    {
        Guid expectedId = Guid.NewGuid();
        var messageChain = new ResponseMessage(
            new AuthenticatedMessage(
                new Message()
                {
                    Body = "test"
                })
                {
                    SessionId = expectedId
                }
            )
            {
                Message = "Test"
            };
        
        bool result = MessageUtils.TryGetSessionId(messageChain, out var actualId); 
        
        Assert.True(result);
        Assert.Equal(expectedId, actualId );
    } 

    [Fact]
    public void TryGetSessionId_TryToGetSessionIdFromNotValid_AsExpected()
    {
        Guid expectedId = Guid.NewGuid();
        var messageChain = new ResponseMessage(
                new Message()
                {
                    Body = "test"
                })
            {
                Message = "Test"
            };
        
        bool result = MessageUtils.TryGetSessionId(messageChain, out var actualId); 
        
        Assert.False(result);
    } 

    [Fact]
    public void TryExtractMessage_GetLastMessage_ReturnTrueAndCorrectMessageInChain()
    {
        var expectedMessage = new Message()
        {
            Body = "test"
        };

        var messageChain = new ResponseMessage(
            new AuthenticatedMessage(expectedMessage)
                {
                    SessionId = Guid.NewGuid()
                }
            )
            {
                Message = "Test"
            };
        
        bool result = MessageUtils.TryExtractMessage<Message>(messageChain, out var actualMessage); 
        
        Assert.True(result);
        Assert.Equal(expectedMessage, actualMessage);
    } 

}
