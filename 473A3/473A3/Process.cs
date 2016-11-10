using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _473A3
{
    public class Process
    {
        public enum Type { FillOpposites, FillImmediateNeighbors };
        public int a, b, c, d, e, f, g, h;
        bool isZS1;
        bool Modified = false;
        int count = 0;

        public void SetNeighbors(Bitmap image, int i, int j)
        {
            a = b = c = d = e = f = g = h = 255;
            int rows = image.Height;
            int columns = image.Width;

            a = (i == 0 || j == 0) ? 255 : (int)image.GetPixel(i - 1, j - 1).R; ; //top left
            b = (j == 0) ? 255 : (int)image.GetPixel(i, j - 1).R; ; //top middle
            c = (j == 0 || i >= columns - 1) ? 255 : (int)image.GetPixel(i + 1, j - 1).R; ; //top right
            d = (i == 0) ? 255 : (int)image.GetPixel(i - 1, j).R; ; //middle left
            e = (i >= columns - 1) ? 255 : (int)image.GetPixel(i + 1, j).R; ; //middle right
            f = (j >= rows - 1 || i == 0) ? 255 : (int)image.GetPixel(i - 1, j + 1).R; ; //bottom left
            g = (j >= rows - 1) ? 255 : (int)image.GetPixel(i, j + 1).R; ; //bottom middle
            h = (j >= rows - 1 || i >= columns - 1) ? 255 : (int)image.GetPixel(i + 1, j + 1).R; ; //bottom right
        }


        public Bitmap FillOpposites(Bitmap image)
        {
            Modified = false;
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if ((int)image.GetPixel(i, j).R == 255)
                    {
                        if (a + h == 0 || b + g == 0 || d + e == 0 || c + f == 0)
                        {
                            copy.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            Modified = true;
                        }
                    }
                }
            }
            return copy;
        }

        public Bitmap FillImmidiateNeighbors(Bitmap image)
        {
            Modified = false;
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if ((int)image.GetPixel(i, j).R == 255)
                    {
                        if (d + b + g + e <= 255) //if 3 neighbors are black
                        {
                            copy.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            Modified = true;
                        }
                    }
                }
            }
            return copy;
        }

        public Bitmap Skeletonize(Bitmap original)
        {
            Bitmap copy = new Bitmap(original);

            do
            {
                copy = ThinZS(copy);
            } while (Modified);

            for (int i = 0; i < copy.Width; i++)
            {
                for (int j = 0; j < copy.Height; j++)
                {
                    if (copy.GetPixel(i, j).R == 0)
                    {
                        original.SetPixel(i,j,Color.Aqua);
                    }
                }
            }

            return original;
        }

        public Bitmap ThinZS(Bitmap original)
        {
            Modified = false;
            Bitmap copy = new Bitmap(original);
            isZS1 = true;
            for (int i = 1; i < original.Width; i++)
            {
                for (int j = 1; j < original.Height; j++)
                {
                    if (original.GetPixel(i, j).R == 0)
                    {
                        SetNeighbors(original, i, j);
                        ZhangSuen(copy, i, j, original.Height, original.Width);
                    }
                }
            }
            isZS1 = false;
            Bitmap copy2 = new Bitmap(copy);
            for (int i = 1; i < copy.Width; i++)
            {
                for (int j = 1; j < copy.Height; j++)
                {
                    if (copy.GetPixel(i, j).R == 0)
                    {
                        SetNeighbors(copy, i, j);
                        ZhangSuen(copy2, i, j, original.Height, original.Width);
                    }
                }
            }
            return copy2;
        }

        void ZhangSuen(Bitmap copy, int i, int j, int rows, int columns)
        {
            int A, B;
            bool edgeCase = (i == 0) || (j == 0) || (i == rows - 1) || (j == columns - 1);

            if (!edgeCase)
            {
                A = 0;
                B = 2040 - (a + b + c + d + e + f + g + h);
                if (b == 255 && c == 0)
                    A++;
                if (c == 255 && e == 0)
                    A++;
                if (e == 255 && h == 0)
                    A++;
                if (h == 255 && g == 0)
                    A++;
                if (g == 255 && f == 0)
                    A++;
                if (f == 255 && d == 0)
                    A++;
                if (d == 255 && a == 0)
                    A++;
                if (a == 255 && b == 0)
                    A++;

                if (isZS1)
                {
                    if ((2 * 255 <= B) && (B <= 6 * 255) && (A == 1) && ((b + e + g) >= 255) && ((e + g + d) >= 255))
                    {
                        copy.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        Modified = true;
                    }
                }
                else
                {
                    if ((2 * 255 <= B) && (B <= 6 * 255) && (A == 1) && ((b + e + d) >= 255) && ((b + g + d) >= 255))
                    {
                        copy.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        Modified = true;
                    }
                }
            }
        }//end ZhangSuen

        public Bitmap LoopProcess(Bitmap image, Type type, int times)
        {
            Bitmap copy = new Bitmap(image);
            do
            {
                System.Console.WriteLine(count++);
                if (type == Type.FillImmediateNeighbors)
                {
                    copy = FillImmidiateNeighbors(copy);
                }
                else if (type == Type.FillOpposites)
                {
                    copy = FillOpposites(copy);
                }
                times--;
            } while (times > 0);
            return copy;
        }

        public Bitmap GetContour(Bitmap image)
        {
            Bitmap copy = new Bitmap(image);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    SetNeighbors(image, i, j);
                    if ((int)image.GetPixel(i, j).R == 0)
                    {
                        if (a + b + c + d + e + f + g + h == 0)
                        {
                            copy.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
            }
            return copy;
        }

    }
}
