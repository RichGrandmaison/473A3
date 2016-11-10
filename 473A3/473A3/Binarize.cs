using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _473A3
{
    class Binarize
    {
        public Bitmap image;
        int counter = 0;
        int grayScaleTreshhold;
        bool reverseScale = false;

        public void SetImage(string imagePath)
        {
            Image rawImage = Image.FromFile(imagePath);
            image = new Bitmap(rawImage);
        }

        public void SetTreshhold(int i)
        {
            grayScaleTreshhold = i;
        }

        public void ReverseScale(bool b)
        {
            reverseScale = b;
        }

        //First, average out the RGB of the pixel, and set R = B = G = average(R,B,G)
        //Second, set a threshold to decide if a pixel will be black or white
        public void ToBlackAndWhite()
        {
            int blackOrWhite;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    int gray = (int)(r * 0.2989 + g * 0.5870 + b * 0.1140);
                    if (reverseScale)
                    {
                        blackOrWhite = (gray > grayScaleTreshhold) ? 255 : 0;
                    } else
                    {
                        blackOrWhite = (gray > grayScaleTreshhold) ? 0 : 255;
                    }

                    image.SetPixel(i, j, Color.FromArgb(blackOrWhite, blackOrWhite, blackOrWhite));
                }
            }
        }

    }
}
