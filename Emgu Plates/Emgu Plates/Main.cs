using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using Emgu_Plates;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Drawing;
public class Home
{
    [STAThread]
    public static void Main(string[] args)
    {
        string option = "";
        while (option != "quit")
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            FileInfo fileInfo = null;

            fileSelector.Title = "Browse Files";
            fileSelector.InitialDirectory = @"c:\Users\" + Environment.UserName + "\\Desktop";
            fileSelector.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                fileInfo = new FileInfo(fileSelector.FileName);
            }

            if (fileInfo != null)
            {
                String imageName = fileInfo.Name;
                byte[] imageBytes = new byte[fileInfo.Length];
                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(imageBytes, 0, imageBytes.Length);
                }
                Console.WriteLine("****************************************\n");
                Console.WriteLine("Processing Image: " + imageName);
                ROI roi = new ROI(imageBytes);
                if (roi.Width == 0 && roi.Height == 0)
                {
                    Console.WriteLine("ROI could not detect a visible plate object. Attempting a rotation\n");
                    
                    
                }
                else
                {
                    Console.WriteLine("Center:" + roi.Center);
                    Console.WriteLine("Width:" + roi.Width);
                    Console.WriteLine("Height:" + roi.Height);
                    Console.WriteLine("Detected ROI Dimentions:" + roi.Width + "x" + roi.Height);
                    Console.WriteLine("Detected ROI Area:" + roi.Width * roi.Height + "\n");
                }
            }
            else
            {
                Console.WriteLine("Do you whish to exit? type 'quit', 'no' to continue:");
                option = Console.ReadLine();
                option = option.ToLower();
                switch (option)
                {
                    case ("quit"):
                        Console.WriteLine("Exiting the program. Press any key to continue..");
                        break;
                    case ("no"):
                        break;
                    default:
                        Console.WriteLine("Command not recognized");
                        break;
                }
            }
        }
        Console.ReadKey();
    }
}
