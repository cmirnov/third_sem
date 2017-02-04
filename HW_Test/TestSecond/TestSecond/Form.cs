using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSecond
{
    public partial class SecondTest : Form
    {
        private List<Point> listOfPointsMid;
        private List<Point> listOfPointsAver;
        public SecondTest(List<Point> pointsMid, List<Point> pointsAver)
        {
            InitializeComponent();
            listOfPointsMid = pointsMid;
            listOfPointsAver = pointsAver;
        }

        private void Go_Click(object sender, EventArgs e)
        {
            DrawIt();
        }

        private void DrawIt()
        {
            for (int i = 0; i < listOfPointsMid.Count; i++)
            {
                this.Graph.Series["Time_Mid"].Points.AddXY(listOfPointsMid[i].X, listOfPointsMid[i].Y);
            }
            for (int i = 0; i < listOfPointsAver.Count; i++)
            {
                this.Graph.Series["Time_Aver"].Points.AddXY(listOfPointsAver[i].X, listOfPointsAver[i].Y);
            }
        }
    }
}
