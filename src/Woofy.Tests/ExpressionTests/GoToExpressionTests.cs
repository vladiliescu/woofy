using System;
using Moq;
using Woofy.Core.Engine;
using Woofy.Core.Engine.Expressions;
using Woofy.Flows.ApplicationLog;
using Xunit;

namespace Woofy.Tests.ExpressionTests
{
    public class GoToExpressionTests
    {
        private readonly ObjectMother factory = new ObjectMother();

        [Fact]
        public void Should_parse_correctly_urls_ending_with_dots()
        {
            var appLog = new Mock<IAppLog>();
            appLog
                .Setup(x => x.Send(It.IsAny<AppLogEntryAdded>()))
                .Callback<AppLogEntryAdded>(entry => entry.Message.ShouldBeEqualTo("http://example.com/so.many.dots..."));
            var goTo = new GoToExpression(appLog.Object, factory.WebClient.Object, factory.ApplicationController.Object);

            goTo.Invoke("http://example.com/so.many.dots...", new Context("", "", null));
        }
    }
}