using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Moq;
using Xunit;

namespace Application.Tests {
    public class DispatcherShould {

        [Fact]
        public async Task InvokeHandler() {
            // Arrange
            var updateEmailHandler = new Mock<ICommandHandlerAsync<UpdateEmailCommand>>();
            
            var handlers = new Dictionary<Type, IEnumerable<object>> {
                { typeof(ICommandHandlerAsync<UpdateEmailCommand>), new object[]{ updateEmailHandler.Object } }
            };
            
            var sut = new CommandDispatcher(handlers);
            var updateEmailCommand = new UpdateEmailCommand("somemail", "newmail");
            
            // Act
            await sut.DispatchAsync(updateEmailCommand);
            
            // Assert
            updateEmailHandler.Verify(handler => handler.HandleAsync(updateEmailCommand), Times.Once);
        }
    }
}