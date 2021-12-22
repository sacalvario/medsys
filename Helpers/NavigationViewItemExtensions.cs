
using System;
using ECN.ViewModels;

namespace ModernWpf.Controls
{
    public static class NavigationViewItemExtensions
    {

        public static Type SetTargetPageType(this NavigationViewItem navigationViewItem)
        {
            switch (navigationViewItem.Tag.ToString())
            {
                case "History":
                    return typeof(HistoryViewModel);

                case "Ecn":
                    return typeof(EcnViewModel);

                case "Records":
                    return typeof(EcnRecordsViewModel);

                default:
                    return null;
            }
        }
    }
}
