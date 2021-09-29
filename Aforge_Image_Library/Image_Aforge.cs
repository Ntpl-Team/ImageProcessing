  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using AForge;
using AForge.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Aforge_Image_Library
{
    class Image_Aforge
    {       
        
        /// <summary>
        /// DeskewRight
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int value</returns>
        public int DeskewRight(Bitmap image)
        {
            if (image != null)
            {
               // AForge.Imaging.Image.Clone(image);
                DocumentSkewChecker check = new DocumentSkewChecker();
                check.MaxSkewToDetect = 15; //max skew angle
                double angle = check.GetSkewAngle(image); // get image skew angle

                if (Math.Abs(angle) < 15) //if image deskew 15 degree then rotate
                {
                    RotateBilinear rotate = new RotateBilinear(angle, true);
                    Bitmap imagerotate = rotate.Apply(image);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// DEskew left
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int</returns>
        public int DeskewLeft(Bitmap image)
        {
            if (image != null)
            {
                DocumentSkewChecker check = new DocumentSkewChecker();
                check.MaxSkewToDetect = 15;
                double angle = check.GetSkewAngle(image); //get image skew angle

                if (Math.Abs(angle) < 15) //check skew angle then rotate
                {
                    RotateBilinear rotate = new RotateBilinear(-angle, true);
                    Bitmap imageRotate = rotate.Apply(image);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// rotate 90
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int</returns>
        public int RotateClockWise90(Bitmap image)
        {
            if (image != null)
            {
                RotateBilinear rotate = new RotateBilinear(90); //rotate 90 degree clockwise
                Bitmap rotate90 = rotate.Apply(image);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// rotate 90
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int</returns>
        public int RotateCounter90(Bitmap image)
        {
            if (image != null)
            {
                RotateBilinear rotate = new RotateBilinear(-90); //rotate 90 degree anticlockwise
                Bitmap rotate90 = rotate.Apply(image);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Crop image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int</returns>
        public int AutoCrop(Bitmap image)
        {
            if (image != null)
            {
                Rectangle rect = new Rectangle(50, 80, 120, 160); 
                if (rect.Width <= image.Width && rect.Height <= image.Height) //check crop area 
                {
                    Crop crop = new Crop(rect);
                    Bitmap CropImage = crop.Apply(image);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="dest_width">width</param>
        /// <param name="dest_height">height</param>
        /// <returns>int</returns>
        public int Resize(Bitmap image,int dest_width, int dest_height)
        {
            if (image != null)
            {             
                ResizeBilinear resize = new ResizeBilinear(dest_width,dest_height);
                Bitmap RisizeImage = resize.Apply(image);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// despeckle
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>int</returns>
        public int Despeckle(Bitmap image)
        {
            if (image != null)
            {
                Median filter = new Median();
                Bitmap DespeckleImage = filter.Apply(image);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// compression
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="dest_width">width</param>
        /// <param name="dest_height">height</param>
        /// <returns>int</returns>
        public int ImageCompression(Bitmap image,int dest_width, int dest_height)
        {
            if (image != null)
            {
                ResizeBilinear resizeimage = new ResizeBilinear(dest_width, dest_height); //resize with given value
                Bitmap resizeImage = resizeimage.Apply(image);

                //check quality of compressed image
                Graphics graphics = Graphics.FromImage(resizeImage);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                // save compressed image in tiff image format
                resizeImage.Save("img1.tiff", ImageFormat.Tiff);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// AreaCrop
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="rect">rectangle</param>
        /// <returns>int</returns>
        public int AreaCrop(Bitmap image,Rectangle rect)
        {
            if (image != null)
            {
                Crop areacrop = new Crop(rect);
                Bitmap CropImage = areacrop.Apply(image);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

    

