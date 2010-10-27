using System;
using Moq;
using Woofy.Core.Engine;
using Xunit;

namespace Woofy.Tests.ExpressionTests
{
    public class When_visiting
    {
        private readonly ObjectMother factory = new ObjectMother();
        private readonly Context context = new Context { CurrentAddress = new Uri("http://example.com") };
        private readonly VisitExpression visit;

        public When_visiting()
        {
            visit = factory.CreateVisitExpression();
        }

        [Fact]
        public void Should_load_the_start_page_content_if_not_set()
        {
            factory.WebClient.Setup(x => x.DownloadString(It.IsAny<Uri>())).Returns("hello");
            visit.Invoke("[a-z]", context).ForceTraversal();
            context.PageContent.ShouldBeEqualTo("hello");
        }

        [Fact]
        public void Should_yield_each_page_until_it_doesnt_find_any()
        {
            Assert.True(false);
        }

        [Fact]
        public void Should_only_yield_the_first_page_if_several_are_found()
        {
            Assert.True(false);
        }
    }
}