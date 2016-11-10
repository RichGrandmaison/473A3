using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace _473A3
{
    class Program
    {
        static void Main(string[] args)
        {
            Binarize binarize = new Binarize();
            Process process = new Process();

            //change paths to work on your machine!!
            string pearPath = @"F:\workspace\473A3\473A3\473A3\original\pear.png";
            string bananaPath = @"F:\workspace\473A3\473A3\473A3\original\banana.png";
            string orangePath = @"F:\workspace\473A3\473A3\473A3\original\orange.png";
            string titlePath = @"F:\workspace\473A3\473A3\473A3\original\title.png";

            binarize.SetTreshhold(80);
            binarize.SetImage(pearPath);
            binarize.ToBlackAndWhite();
            Bitmap pearProcessed = process.LoopProcess(binarize.image, Process.Type.FillImmediateNeighbors, 20);
            pearProcessed = process.LoopProcess(binarize.image, Process.Type.FillOpposites, 20);
            pearProcessed.Save("pearBWpostProcess.png");

            binarize.SetTreshhold(100);
            binarize.SetImage(bananaPath);
            binarize.ToBlackAndWhite();
            Bitmap bananaProcessed = process.LoopProcess(binarize.image, Process.Type.FillImmediateNeighbors, 20);
            bananaProcessed = process.LoopProcess(binarize.image, Process.Type.FillOpposites, 20);
            bananaProcessed.Save("bananaBWpostProcess.png");


            binarize.SetTreshhold(45);
            binarize.SetImage(orangePath);
            binarize.ToBlackAndWhite();
            Bitmap orangeProcessed = process.LoopProcess(binarize.image, Process.Type.FillImmediateNeighbors, 20);
            orangeProcessed = process.LoopProcess(binarize.image, Process.Type.FillOpposites, 20);
            orangeProcessed.Save("orangeBWpostProcess.png");

            binarize.SetTreshhold(128);
            binarize.SetImage(titlePath);
            binarize.ReverseScale(true);
            binarize.ToBlackAndWhite();
            Bitmap titleProcessed = process.LoopProcess(binarize.image, Process.Type.FillImmediateNeighbors, 20);
            titleProcessed = process.LoopProcess(binarize.image, Process.Type.FillOpposites, 20);
            titleProcessed.Save("titleBWpostProcess.png");

            //change paths to work on your machine!!
            string processedPearPath = @"F:\workspace\473A3\473A3\473A3\bin\Debug\pearBWpostProcess.png";
            string processedBananaPath = @"F:\workspace\473A3\473A3\473A3\bin\Debug\bananaBWpostProcess.png";
            string processedOrangePath = @"F:\workspace\473A3\473A3\473A3\bin\Debug\orangeBWpostProcess.png";
            string processedTitlePath = @"F:\workspace\473A3\473A3\473A3\bin\Debug\titleBWpostProcess.png";
            
            Image rawImage = Image.FromFile(processedTitlePath);
            Bitmap image = new Bitmap(rawImage);
            Bitmap titleSkeleton = process.Skeletonize(image);
            titleSkeleton.Save("titleSkeleton.png");

            rawImage = Image.FromFile(processedPearPath);
            image = new Bitmap(rawImage);
            Bitmap pearSkeleton = process.Skeletonize(image);
            pearSkeleton.Save("pearSkeleton.png");

            rawImage = Image.FromFile(processedOrangePath);
            image = new Bitmap(rawImage);
            Bitmap orangeSkeleton = process.Skeletonize(image);
            orangeSkeleton.Save("orangeSkeleton.png");
           
            rawImage = Image.FromFile(processedBananaPath);
            image = new Bitmap(rawImage);
            Bitmap bananaSkeleton = process.Skeletonize(image);
            bananaSkeleton.Save("bananaSkeleton.png");

        }
    }
}
