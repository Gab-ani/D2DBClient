using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2DBClient.Domain
{
    public class Hero
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Bitmap Icon { get; set; }

        public Hero(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Hero()
        {

        }

    }
}
