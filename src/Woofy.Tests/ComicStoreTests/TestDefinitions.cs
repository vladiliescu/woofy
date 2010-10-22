using System;
using Woofy.Core.Engine;

namespace Woofy.Tests.ComicStoreTests
{
    public class _AlphaTestDefinition : Definition
    {
        public override string Comic
        {
            get { return "alpha"; }
        }

        public override string StartAt
        {
            get { return ""; }
        }
      
        protected override void RunImpl(Context context)
        {
            throw new NotImplementedException();
        }
    }

    public class _BetaTestDefinition : Definition
    {
        public override string Comic
        {
            get { return "beta"; }
        }

        public override string StartAt
        {
            get { return ""; }
        }

        protected override void RunImpl(Context context)
        {
            throw new NotImplementedException();
        }
    }
}