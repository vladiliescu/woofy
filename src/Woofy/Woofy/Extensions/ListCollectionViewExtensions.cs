using System.Windows.Data;

namespace Woofy
{
    public static class ListCollectionViewExtensions
    {
        public static T GetItemAt<T>(this ListCollectionView listCollectionView, int index)
        {
            return (T)listCollectionView.GetItemAt(index);
        }
    }
}