using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace TileEditorForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<mapPictureBox> mapItems;
        List<mapPictureBox> sourceItems;
        List<tileDetails> tileDetails;
        mapPictureBox pctBrush;

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            mapItems = new List<mapPictureBox>();
            sourceItems = new List<mapPictureBox>();
            tileDetails = new List<tileDetails>();
            Point nextLocation = new Point(0, 0);
            pctBrush = new mapPictureBox();
            pctBrush.Location = new Point(472, 12);
            this.Controls.Add(pctBrush);
                
            pnlImages.Height = Convert.ToInt32(Math.Floor(Convert.ToDouble(pnlImages.Height / 48))*48);

            for (int i = 0; nextLocation.Y < pnlImages.Bottom; i++)
            {
                mapPictureBox temp = new mapPictureBox();
                temp.Image = Image.FromFile("blankSquare.bmp");
                temp.Width = 48;
                temp.Height = 48;
                temp.tileLocationX = x;
                temp.tileLocationY = y;
                temp.Location = nextLocation;
                temp.imageURI = "blankSquare.bmp";
                temp.MouseEnter += new EventHandler(temp_MouseEnter);
                temp.MouseLeave += new EventHandler(temp_MouseLeave);
                temp.Click += new EventHandler(temp_Click);
                mapItems.Add(temp);
                pnlImages.Controls.Add(mapItems[i]);

                if (nextLocation.X + 48 > pnlImages.Width)
                {
                    nextLocation.X = 0;
                    nextLocation.Y += 48;
                    y += 1;
                    x = 0;
                }
                else
                {
                    nextLocation.X += 48;
                    x += 1;
                }
                
            }

        }

        void temp_Click(object sender, EventArgs e)
        {
            mapPictureBox getSender = (mapPictureBox)sender;
            getSender.Image = pctBrush.Image;
            getSender.imageLocation = pctBrush.imageLocation;
            getSender.imageURI = pctBrush.imageURI;
        }

        void temp_MouseLeave(object sender, EventArgs e)
        {
            mapPictureBox tile = (mapPictureBox)sender;
            tile.BorderStyle = BorderStyle.None;
            tile.Update();
        }

        void temp_MouseEnter(object sender, EventArgs e)
        {
            mapPictureBox tile = (mapPictureBox)sender;
            tile.BorderStyle = BorderStyle.FixedSingle;
            tile.Update();

        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg";
            dlg.ShowDialog();

            Image tempImage = Image.FromFile(dlg.FileName);

            Point nextLocation = new Point(0, 0);
            Size size = new Size(48,48);
            Rectangle rect = new Rectangle(nextLocation,size);


            for (int i = 0; nextLocation.Y < tempImage.Size.Height; i++)
            {
                mapPictureBox temp = new mapPictureBox();
                temp.Image = cropImage(tempImage, rect);
                temp.Width = 48;
                temp.Height = 48;
                temp.Location = nextLocation;
                temp.imageURI = dlg.FileName;
                temp.MouseEnter += new EventHandler(temp_MouseEnter);
                temp.MouseLeave += new EventHandler(temp_MouseLeave);
                temp.MouseClick += new MouseEventHandler(sourceBox_MouseClick);
                sourceItems.Add(temp);
                srcLocation.Controls.Add(sourceItems[i]);

                if (nextLocation.X + 48 > srcLocation.Width)
                {
                    nextLocation.X = 0;
                    nextLocation.Y += 48;
                    
                }
                else
                {
                    nextLocation.X += 48;
                }

                rect = new Rectangle(nextLocation, size);

            }
            
            
        }

        void sourceBox_MouseClick(object sender, MouseEventArgs e)
        {

            mapPictureBox getSender = (mapPictureBox)sender;
            pctBrush.Image = getSender.Image;
            pctBrush.imageURI = getSender.imageURI;
            

        }

        private Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.ShowDialog();

            using (XmlWriter writer = XmlWriter.Create(saveFile.FileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Tiles");

                foreach(mapPictureBox box in mapItems){
           
                    writer.WriteStartElement("Tile");
                    writer.WriteElementString("X", box.tileLocationX.ToString());
                    writer.WriteElementString("Y", box.tileLocationX.ToString());
                    writer.WriteElementString("TextureFile", box.imageURI);
                    writer.WriteElementString("TextureFileX", box.imageLocation.X.ToString());
                    writer.WriteElementString("TextureFileY", box.imageLocation.Y.ToString());
                    

                    writer.WriteEndElement();
                    
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            
        }

 
    }
}
