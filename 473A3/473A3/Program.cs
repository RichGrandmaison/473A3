using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _473A3
{
    class Program
    {
        static void Main(string[] args)
        {
            Binarize binarize = new Binarize();

            string pearPath = @"C:\workspace\473A3\473A3\473A3\original\pear.png";
            string bananaPath = @"C:\workspace\473A3\473A3\473A3\original\banana.png";
            string orangePath = @"C:\workspace\473A3\473A3\473A3\original\orange.png";
            string titlePath = @"C:\workspace\473A3\473A3\473A3\original\title.png";

            binarize.SetImage(bananaPath);
            binarize.ToBlackAndWhite();
            binarize.image.Save("pearBW.png");


        }
    }
}
