using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Woofy.Tests
{
    public interface IEventSubscriber
    {
        void Handler(object sender, EventArgs e);
    }
}
