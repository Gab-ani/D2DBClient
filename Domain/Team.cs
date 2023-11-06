using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2DBClient.Domain
{
    public class Team
    {
        public string Name { get; set; }

        public Player Carry { get; set; }
        public Player Mid { get; set; }
        public Player Offlane { get; set; }
        public Player Four { get; set; }
        public Player Five { get; set; }

        public Bitmap Icon { get; set; }
    }
}
