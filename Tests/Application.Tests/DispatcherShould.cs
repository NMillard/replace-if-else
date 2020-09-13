using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Medium.ReplacingIfElse.Application.CommandHandlers.Commands;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Medium.ReplacingIfElse.Application.Commands.Inputs;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Moq;
using Xunit;

namespace Application.Tests {
    public class DispatcherShould {

        [Fact]
        public async Task InvokeHandler() {
            // Arrange
            var updateEmailHandler = new Mock<ICommandHandlerAsync<ChangeEmailCommand>>();

            var handlers = new Dictionary<Type, IEnumerable<object>> {
                { typeof(ICommandHandlerAsync<ChangeEmailCommand>), new object[] { updateEmailHandler.Object } }
            };

            var sut = new CommandDispatcher(handlers);
            var updateEmailCommand = new ChangeEmailCommand("somemail", "newmail");

            // Act
            await sut.DispatchAsync(updateEmailCommand);

            // Assert
            updateEmailHandler.Verify(handler => handler.HandleAsync(updateEmailCommand), Times.Once);
        }

        [Fact]
        public async Task ThrowWhenDispatchingCommandWithNoHandlers() {
            var emptyHandlers = new Dictionary<Type, IEnumerable<object>>();

            var sut = new CommandDispatcher(emptyHandlers);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await sut.DispatchAsync(new ChangeEmailCommand("old", "new"))
            );
        }
        
        [Fact]
        public async Task NotInvokeHandlerWhenOfInvalidType() {
            // Arrange
            var updateEmailHandler = new Mock<ICommandHandlerAsync<ChangeEmailCommand>>();

            var handlers = new Dictionary<Type, IEnumerable<object>> {
                { typeof(ICommandHandlerAsync<ChangeEmailCommand>), new[] { new { Invalid = "Handler"} } }
            };
            
            var updateEmailCommand = new ChangeEmailCommand("somemail", "newmail");
            var sut = new CommandDispatcher(handlers);

            // Act
            await sut.DispatchAsync(updateEmailCommand);

            // Assert
            updateEmailHandler.Verify(handler => handler.HandleAsync(updateEmailCommand), Times.Never);
        }
    }
}