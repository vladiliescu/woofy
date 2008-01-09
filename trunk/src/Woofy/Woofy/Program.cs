using System;

using Woofy.Controllers;

namespace Woofy
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            ComicsPresenter comicsController = new ComicsPresenter();
            comicsController.RunApplication();
        }
    }
}
