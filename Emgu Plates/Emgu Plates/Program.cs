using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Drawing;
using System.IO;

namespace Emgu_Plates
{
    class ROI
    {
        protected byte[] imageData;
        protected Point ROI_CENTER;
        protected int ROI_HEIGHT;
        protected int ROI_WIDTH;

        public Point Center
        {
            get{return ROI_CENTER;}
            set{ROI_CENTER = value;}
        }
        public int Height
        {
            get{return ROI_HEIGHT;}
            set{ROI_HEIGHT = value;}
        }
        public int Width
        {
            get{return ROI_WIDTH;}
            set{ROI_WIDTH = value;}
        }
        
        public ROI(byte[] input)
        {
            imageData = input;
            Mat image = new Mat();
            //Reads an image from a buffer in memory
            CvInvoke.Imdecode(imageData, Emgu.CV.CvEnum.LoadImageType.AnyColor, image);
            //Makes a list object to hold multiple detected plates
            List<Rectangle> plates = new List<Rectangle>();
            //Uses the Haar cascade classifier to detect objects
            using(CascadeClassifier plateObject = new CascadeClassifier(Directory.GetCurrentDirectory()+@"\haar.xml"))
            {
                using(Mat grayScale = new Mat())
                {
                    //Converts the image to grayscale 
                    CvInvoke.CvtColor(image, grayScale, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                    //Calls the cascade classifier to detect object properties
                    Rectangle[] platesDetected = plateObject.DetectMultiScale(grayScale, 1.1, 10, new Size(20, 20));
                    //Add's the range of detected plates to the list
                    plates.AddRange(platesDetected);
                }
            }
            foreach(Rectangle plate in plates)
            {
                //Draws a rectangle around each range that was added  (detects multiple plates in one image)
                CvInvoke.Rectangle(image, plate, new Bgr(Color.Red).MCvScalar, 2);
                ROI_CENTER = new Point(plate.Left + plate.Width / 2, plate.Top + plate.Height / 2);
                decimal percentX = Decimal.Divide(plate.Left + plate.Width / 2,image.Width);
                decimal percentY = Decimal.Divide(plate.Top + plate.Height / 2,image.Height);
                //Console.WriteLine("ROI Location as Percent of full image: <"+decimal.Truncate(percentX*100)+"%,"+decimal.Truncate(percentY*100)+"%>");
                ROI_HEIGHT = plate.Width;
                ROI_WIDTH = plate.Height;
            }
            ImageViewer.Show(image);
        }
    }
}
