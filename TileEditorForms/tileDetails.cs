using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEditorForms
{
    public class tileDetails
    {
        public int imageX { get; set; }
        public int imageY { get; set; }
        public string imageName { get; set; }
        public int mapLocationX { get; set; }
        public int mapLocationY { get; set; }

        public tileDetails(int InX, int InY, string InImageName, int InMapLocationX, int InMapLocationY)
        {
            imageX = InX;
            imageY = InY;
            imageName = InImageName;
            mapLocationX = InMapLocationX;
            mapLocationY = InMapLocationY;
        }
    }
}
