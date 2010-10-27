using System;
using Moq;
using Woofy.Core;
using Woofy.Core.Engine;
using Xunit;

namespace Woofy.Tests.ExpressionTests
{
    public class When_visiting
    {
        private readonly ObjectMother factory = new ObjectMother();
        private readonly Context context = new Context { CurrentAddress = new Uri("http://example.com") };
        private readonly IPageParser parser = new PageParser(new AppSettings());
        private readonly VisitExpression visit;
        private const string regex = @"http://example.com/[\d]";

        public When_visiting()
        {
            visit = factory.CreateVisitExpression(parser);
        }

        [Fact]
        public void Should_load_the_start_page_content_if_not_set()
        {
            SetWebClientResponse("hello");
            visit.Invoke(@"[\d]", context).ForceTraversal();
            context.PageContent.ShouldBeEqualTo("hello");
        }

        [Fact]
        public void Should_yield_each_link_until_it_finds_no_more()
        {
            SetWebClientResponse("http://example.com", "...http://example.com/2...");
            SetWebClientResponse("http://example.com/2", "...http://example.com/3...");
            SetWebClientResponse("http://example.com/3", "...http://example.com/4...");
            SetWebClientResponse("http://example.com/4", "...the end...");
            
            var enumerator = visit.Invoke(regex, context).GetEnumerator();
            
            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com"));
            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com/2"));
            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com/3"));
            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com/4"));
            enumerator.MoveNext().ShouldBeFalse();
        }

        [Fact]
        public void Should_only_yield_the_first_link_if_several_are_found_in_the_same_page()
        {
            SetWebClientResponse("http://example.com", "...http://example.com/2...http://example.com/3");
            SetWebClientResponse("http://example.com/2", "...");
            var enumerator = visit.Invoke(regex, context).GetEnumerator();

            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com"));
            enumerator.MoveNext().ShouldBeTrue();
            ((Uri)enumerator.Current).ShouldBeEqualTo(new Uri("http://example.com/2"));
            enumerator.MoveNext().ShouldBeFalse();
        }

        private void SetWebClientResponse(string response)
        {
            factory.WebClient.Setup(x => x.DownloadString(It.IsAny<Uri>())).Returns(response);
        }

        private void SetWebClientResponse(string address, string response)
        {
            factory.WebClient.Setup(x => x.DownloadString(new Uri(address))).Returns(response);
        }
    }
}