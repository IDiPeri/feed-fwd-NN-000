using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinearAlgebraTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Matrix<double> A = DenseMatrix.OfArray(new double[,] {
                    {1,1,1,1},
                    {1,2,3,4},
                    {4,3,2,1}});
            Vector<double>[] nullspace = A.Kernel();

            // verify: the following should be approximately (0,0,0)
            var v = (A * (2 * nullspace[0] - 3 * nullspace[1]));

            int i;
            i = 0; ;
        }
    }
}
