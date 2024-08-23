namespace Simple_FFNN
{
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Double;

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
