using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Drawing.Imaging;

namespace ImageWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String abd;

       

        //crop from picturebox
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        Pen cropPen = new Pen(Color.Yellow, 2);
        Bitmap cropBitmap;
        Bitmap bm_dest;
        Bitmap bm_source;
        int i = 1;
        FolderBrowserDialog s_fold = new FolderBrowserDialog();
        FolderBrowserDialog d_fold = new FolderBrowserDialog();

        string[] files;
        int pheight;
        int pwidth;


        private void Browser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string[] folders = Directory.GetFiles(fbd.SelectedPath);
                textBox1.Text = fbd.SelectedPath;
                abd = fbd.SelectedPath;

                foreach (string folder in folders)
                {
                    listBox1.Items.Add(Path.GetFileName(folder));
                }
            }

            //crop from picturebox

            pictureBox1.Width = pwidth;
            pictureBox1.Height = pheight;
            Type pboxType = pictureBox1.GetType();
            PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
            Rectangle rectangle = (Rectangle)irProperty.GetValue(pictureBox1, null);

            pictureBox1.Height = rectangle.Height;
            pictureBox1.Width = rectangle.Width; 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = abd + "\\" + listBox1.SelectedItem.ToString();
            pictureBox1.Image = new Bitmap(path.ToString());
            System.Drawing.Image img = System.Drawing.Image.FromFile(@"E:\Picture\download.jfif");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Dispose();

            //crop from picturebox

            pictureBox1.Size = new Size(pictureBox1.Width, Height - (Height * 5 / 100));
            pheight = pictureBox1.Height;
            pwidth = pictureBox1.Width;
        }

        private void button5_Click(object sender, EventArgs e)
        {
              //Rotate
            Bitmap b1 = new Bitmap(@"E:\Picture\download.jfif");

            pictureBox1.Image = rotateImage(b1, 60f);
         }


        //Rotate

        private Bitmap rotateImage(Bitmap b, float angle)
        {

            int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));

            //create a new empty bitmap to hold rotated image

            Bitmap returnBitmap = new Bitmap(maxside, maxside);

            //make a graphics object from the empty bitmap

            Graphics g = Graphics.FromImage(returnBitmap);

            //move rotation point to center of image

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);

            //rotate

            g.RotateTransform(angle);

            //move image back

            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);

            //draw passed in image onto graphics object

            g.DrawImage(b, new Point(0, 0));

            return returnBitmap;
        }

      private void button3_Click(object sender, EventArgs e)
        {
            //crop from picturebox

            Cursor = Cursors.Default;
            if (cropWidth < 1)
            {
                return;
            }
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);  
            Bitmap OriginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            Bitmap img = new Bitmap(cropWidth, cropHeight);
            Graphics g = Graphics.FromImage(img);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
            pictureBox1.Image = img;
            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
            //pictureBox1Location();
            //btnCrop.Enabled = false;         
        }


        // Zoom
          private Size Multiplier;

        public void ZoomIn()
        {
            Multiplier = new Size(2,2);

            Image MyImage = pictureBox1.Image;

            Bitmap MyBitMap = new Bitmap(MyImage, Convert.ToInt32(MyImage.Width * Multiplier.Width),
                Convert.ToInt32(MyImage.Height * Multiplier.Height));

            Graphics Graphic = Graphics.FromImage(MyBitMap);

            Graphic.InterpolationMode = InterpolationMode.High ;

            pictureBox1.Image = MyBitMap;

        }

        public void ZoomOut()
        {
            Multiplier = new Size(2, 2);

            Image MyImage = pictureBox1.Image;

            Bitmap MyBitMap = new Bitmap(MyImage, Convert.ToInt32(MyImage.Width / Multiplier.Width),
                Convert.ToInt32(MyImage.Height / Multiplier.Height));

            Graphics Graphic = Graphics.FromImage(MyBitMap);

            Graphic.InterpolationMode = InterpolationMode.High ;

            pictureBox1.Image = MyBitMap;

        }
        private void button1_Click(object sender, EventArgs e)
        {
          ZoomIn();
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {}

       


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //crop from picturebox

              if (e.Button == MouseButtons.Left)
            {
                cropX = e.X;
                cropY = e.Y;
                cropPen.DashStyle = DashStyle.Solid;
                Cursor = Cursors.Cross;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //crop from picturebox

            if (e.Button == MouseButtons.Left)
            {
                if (pictureBox1.Image == null)
                    return;

                pictureBox1.Refresh();
                cropWidth = e.X - cropX;
                cropHeight = e.Y - cropY;
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //crop from picturebox

            try
            {
                Cursor = Cursors.Default;
                try
                {

                    if (cropWidth < 1)
                        return;

                    Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                    Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);

                    cropBitmap = new Bitmap(cropWidth, cropHeight);
                    Graphics g = Graphics.FromImage(cropBitmap);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel);
                    cropBitmap.Save("d:\\abc.jpg");
                }
                catch { }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           ZoomOut();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
