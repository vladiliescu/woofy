using System.Windows.Forms;

namespace Woofy.Gui
{
	public static class DataGridViewExtensions
	{
		public static T FirstDataBoundItem<T>(this DataGridViewSelectedRowCollection collection)
		{
			return (T)(collection[0]).DataBoundItem;
		}

		public static T DataBoundItem<T>(this DataGridViewRow row)
		{
			return (T)row.DataBoundItem;
		}
	}
}