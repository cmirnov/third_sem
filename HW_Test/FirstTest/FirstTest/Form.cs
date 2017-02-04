using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstTest
{
    public partial class FirstTest : Form
    {
        private List<Point> listOfPoints;
        public FirstTest(List<Point> points)
        {
            InitializeComponent();
            listOfPoints = points;
        }

        private void Go_Click(object sender, EventArgs e)
        {
            DrawIt();
        }


        private void DrawIt()
        {
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                this.Graph.Series["Time"].Points.AddXY(listOfPoints[i].X, listOfPoints[i].Y);
            }
        }
    }
}
