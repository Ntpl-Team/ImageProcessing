using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Imaging;
using System.Drawing;

namespace Project_ZoomImage
{
    public class Image
    {
        public ComplexImage zoomIn(ComplexImage image)
        {

            double zoomFactor = 0.015;
            Bitmap filter = new Bitmap(image.ToString());
            int width = (int)(filter.Width * zoomFactor);
            int height = (int)(filter.Height * zoomFactor);
            return image;
        }


        public ComplexImage zoomOut(ComplexImage image)
        {

            double zoomFactor = 0.015;
            Bitmap filter = new Bitmap(image.ToString());
            int width = (int)(filter.Width / zoomFactor);
            int height = (int)(filter.Height / zoomFactor);
            return image;
        }
    }
}
