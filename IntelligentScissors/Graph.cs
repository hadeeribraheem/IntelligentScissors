using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
namespace IntelligentScissors
{   /// <summary>
    /// inintiate Graph construction
    /// weight Calculation
    /// </summary>
    public struct Pixel // pixel position in the image
    {
        public int Xposition;
        public int Yposition;
    }
    public class Node
    {
        public Pixel Pixel { get; set; }
        public Pixel Parent { get; set; }
        public double Cost { get; set; }  //cost is the distance between current node and parent
        public Node(int x, int y)
        {
            Pixel pixel = new Pixel();
            pixel.Xposition = x;
            pixel.Yposition = y;
            this.Pixel = pixel;

            Pixel parent = new Pixel();
            parent.Xposition = -1;
            parent.Yposition = -1;
            this.Parent = parent;

            Cost = int.MaxValue;
        }
    }

    public static class Graph
    {
        public static int IMG_Width { get; set; }          
        public static int IMG_Height { get; set; } 
        public static RGBPixel[,] IMG_Matrix { get; set; } // Original image
        /* 2D Array of image pixels as Nodes (Pixel, Parent, Cost, TOparent_Length) to be used in DijkstraSSSP */
        public static Node[,] IMG_Nodes { get; set; }
        // Construct a weighted graph for an image
        public static void ConstructGraph(RGBPixel[,] original_ImgMatrix) // Analysis: O(V^2)
        {
            IMG_Width = ImageOperations.GetWidth(original_ImgMatrix);
            IMG_Height = ImageOperations.GetHeight(original_ImgMatrix);
            Graph.IMG_Matrix = original_ImgMatrix;
            IMG_Nodes = new Node[IMG_Width, IMG_Height];     
        }
        // Edge Weights Generation between two Pixels
        // We can set the edge-weight between P1 and P2 as Wp1,Wp2 = 1/G
        // So regions with Low G have high weight, and regions with high G have low weight
        public static double GenerateWeight(Pixel pixel1, Pixel pixel2) 
        {
            double weight;
                
            // x axis
            if (pixel1.Xposition > pixel2.Xposition) // pixel2 is left to pixel 1
                weight = ImageOperations.CalculatePixelEnergies(pixel2.Xposition, pixel2.Yposition, IMG_Matrix).X;

            else if (pixel1.Xposition < pixel2.Xposition) // pixel2 is right to pixel1
                weight = ImageOperations.CalculatePixelEnergies(pixel1.Xposition, pixel1.Yposition, IMG_Matrix).X;
            
            //y axis
            else if (pixel1.Yposition < pixel2.Yposition) // pixel2 is down to pixel 1
                weight = ImageOperations.CalculatePixelEnergies(pixel1.Xposition, pixel1.Yposition, IMG_Matrix).Y;

            else // pixel2 is top to pixel 1
                weight = ImageOperations.CalculatePixelEnergies(pixel2.Xposition, pixel2.Yposition, IMG_Matrix).Y;
            
            return 1.0 / weight;
        }
    }
}
