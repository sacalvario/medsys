
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

                case "Checklist":
                    return typeof(ChecklistViewModel);

                case "Approved":
                    return typeof(ApprovedViewModel);

                case "NumberPartHistory":
                    return typeof(NumberPartHistoryViewModel);

                default:
                    return null;
            }
        }
    }
}
