﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TilemapEditor
{
    class Tile
    {
        //Tile map coordinates
        public int x = 0;
        public int y = 0;

        //Tile size
        public int width = 16;
        public int height = 16;

        //Tilemap to sample from
        public Image tileMap;

        //Default constructor
        public Tile()
        {

        }

        //Calculates and returns rect (read only)
        public Rectangle Rect
        {
            get
            {
                //return rect within tilemap
                return new Rectangle(x * width, y * height, width, height);
            }
        }

        //Image property handles extracting tile image from tilemap and setting back
        public Image Image
        {
            //Gets image as correct portion of tilemap
            get
            {
                Bitmap bmp = new Bitmap(width, height);

                if (tileMap != null)
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(
                            tileMap,
                            new Rectangle(0, 0, width, height),
                            Rect,
                            GraphicsUnit.Pixel);
                    }
                }

                return bmp;
            }
            //Write image portion back into tilemap
            set
            {
                if (tileMap != null)
                {
                    Image img = value;

                    using (Graphics g = Graphics.FromImage(tileMap))
                    {
                        //Set clipping to only this tile
                        g.SetClip(Rect);

                        //Clear area to be overwritted by new image
                        g.Clear(Color.Empty);

                        //Write tile image into tilemap
                        g.DrawImage(
                            img,
                            Rect,
                            new Rectangle(0, 0, width, height),
                            GraphicsUnit.Pixel);
                    }
                }
            }
        }
    }
}
