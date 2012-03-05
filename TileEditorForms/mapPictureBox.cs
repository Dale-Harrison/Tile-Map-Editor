using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TileEditorForms
{
    public partial class mapPictureBox : PictureBox
    {
        public int tileLocationX { get; set; }
        public int tileLocationY { get; set; }

        public Point imageLocation { get; set; }
        public string imageURI { get; set; }

    }
}
