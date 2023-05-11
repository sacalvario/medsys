using System;
using System.Collections.Generic;
using System.Windows.Media;

#nullable disable

namespace ECN.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        private SolidColorBrush _StatusColor;
        public SolidColorBrush StatusColor
        {
            get
            {
                if (IdEstado == 1)
                {
                    _StatusColor = new SolidColorBrush(Color.FromRgb(251, 100, 45));
                }
                else if (IdEstado == 2)
                {
                    _StatusColor = new SolidColorBrush(Colors.Orange);
                }
                else if (IdEstado == 3)
                {
                    _StatusColor = new SolidColorBrush(Color.FromRgb(0, 172, 0));
                }
                return _StatusColor;
            }
        }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
