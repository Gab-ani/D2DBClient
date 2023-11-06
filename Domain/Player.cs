using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace D2DBClient.Domain
{
    public class Player
    {

        public string Nickname { get; set; }

        public string Name { get; set; }

        //public Bitmap Portrait { get; set; }

        public List<Hero> HeroPool { get; set; }

    }
}
