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

        private void buttonInitializeNN_Click(object sender, EventArgs e)
        {
            m_NeuralNetwork = new NeuralNetwork(inputsCount:2, hiddenLayerCount:1, nodesPerHiddenLayer:9, outputCount:1);

            Vector<double> inputs = DenseVector.OfArray(new double[2]);
            inputs[0] = 0.25;
            inputs[1] = 0.15;

            m_NeuralNetwork.SetInputs(inputs);
            m_NeuralNetwork.PropagateInputsFwd();

            //var lastLayer = m_NeuralNetwork.Layers[m_NeuralNetwork.Layers.Count - 1];
            //var outputs = lastLayer.Outputs;
            var outputs = m_NeuralNetwork.GetOutputs();
        }

        private NeuralNetwork m_NeuralNetwork;
    }
}
