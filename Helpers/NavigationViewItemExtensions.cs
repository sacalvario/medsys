
using System;
using ECN.ViewModels;

namespace ModernWpf.Controls
{
    public static class NavigationViewItemExtensions
    {

        public static Type SetTargetPageType(this NavigationViewItem navigationViewItem)
        {
            return navigationViewItem != null
                ? navigationViewItem.Tag.ToString() switch
                {
                    "History" => typeof(HistoryViewModel),
                    "Ecn" => typeof(EcnViewModel),
                    "Records" => typeof(EcnRecordsViewModel),
                    "Checklist" => typeof(ChecklistViewModel),
                    "Approved" => typeof(ApprovedViewModel),
                    "NumberPartHistory" => typeof(NumberPartHistoryViewModel),
                    "Dashboard" => typeof(DashboardViewModel),
                    _ => null,
                }
                : null;
        }
    }
}
