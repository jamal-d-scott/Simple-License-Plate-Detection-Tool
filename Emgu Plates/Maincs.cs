using System;

public class Home
{
    public static void Main(string[] args)
    {
        Process.Start("explorer.exe", @"C:\...");
        /*FileInfo fileInfo = new FileInfo("");
        String imageName = "";
        byte[] imageBytes = new byte[fileInfo.Length];
        using (FileStream fs = fileInfo.OpenRead())
        {
            fs.Read(imageBytes, 0, imageBytes.Length);
        }
        Console.WriteLine("****************************************\n");
        Console.WriteLine("\nProcessing Image: " + imageName);
        ROI roi = new ROI(imageBytes);
        Console.WriteLine("Center:" + roi.Center);
        Console.WriteLine("width:" + roi.Width);
        Console.WriteLine("height:" + roi.Height);
        Console.WriteLine("\n****************************************");*/
        Console.ReadKey();
    }
}
