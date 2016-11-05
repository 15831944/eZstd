using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eZstd.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         height = 300;

        }

        private float height;


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Matrix s = new Matrix();
            s.Scale(2,3);
            s.Scale(2, 3);

            var gr = e.Graphics;
            //
            Matrix m = gr.Transform;
            // Create string to draw.
            String drawString = "Sample Text";
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(height / 2, height / 2);
            e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);

            //
            gr.DrawLine(new Pen(Color.Red), new PointF(0, 00), new PointF(height, height));

            gr.TranslateTransform(0, height);
            gr.ScaleTransform(1, -1);
            gr.DrawLine(new Pen(Color.Blue), new PointF(00, 00), new PointF(height, height));

            gr.FillRectangle(new SolidBrush(Color.Yellow), 0,0,100,200 );

            // Draw string to screen.
            e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);


        }
    }
}
