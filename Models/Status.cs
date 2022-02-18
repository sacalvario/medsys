using System;
using System.Collections.Generic;
using System.Windows.Media;

#nullable disable

namespace ECN.Models
{
    public partial class Status
    {
        public Status()
        {
            EcnRevisions = new HashSet<EcnRevision>();
            Ecns = new HashSet<Ecn>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        private SolidColorBrush _StatusColor;
        public SolidColorBrush StatusColor
        {
            get
            {
                if (StatusId == 1)
                {
                    _StatusColor = new SolidColorBrush(Colors.OrangeRed);
                }
                else if (StatusId == 2)
                {
                    _StatusColor = new SolidColorBrush(Colors.Red);
                }
                else if (StatusId == 3)
                {
                    _StatusColor = new SolidColorBrush(Colors.Green);
                }
                else if (StatusId == 4)
                {
                    _StatusColor = new SolidColorBrush(Colors.Green);
                }
                else if (StatusId == 5)
                {
                    _StatusColor = new SolidColorBrush(Colors.Orange);
                }
                return _StatusColor;
            }
        }

        public virtual ICollection<EcnRevision> EcnRevisions { get; set; }
        public virtual ICollection<Ecn> Ecns { get; set; }
    }
}
