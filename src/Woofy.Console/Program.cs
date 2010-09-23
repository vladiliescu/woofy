using System;
using Woofy.Core.Engine;

namespace Woofy.Console
{
	class Program
	{
		/*
		 * start_at http://...
		 * do_while visit_next(regex)
		 *		download regex
		 */
		static void Main(string[] args)
		{
			var statements = new IStatement[] { 
        	    new StartAt { Url = "http://xkcd.com/5/" }, 
                new DoWhile
                {
                    Condition = new [] { new VisitNext { Regex = @"<a\shref=""(?<content>/[\d]*/)""\saccesskey=""p"">" } },
                    Body = new[] { new Download { Regex = @"<img\ssrc=""(?<content>http://imgs.xkcd.com/comics/[\w()-]*\.(gif|jpg|jpeg|png))" }}
                }
            };

			var context = new Context();
			foreach (var statement in statements)
			{
				statement.Run(context);
			}
		}
	}
}
