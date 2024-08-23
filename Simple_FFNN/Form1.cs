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

        private void buttonInitializeNN_Click(object sender, EventArgs e)
        {
            m_NeuralNetwork = new NeuralNetwork(inputsCount:2, hiddenLayerCount:1, nodesPerHiddenLayer:9, outputCount:1);

            Vector<double> inputs = DenseVector.OfArray(new double[2]);
            inputs[0] = 0.25;
            inputs[1] = 0.15;

            m_NeuralNetwork.SetInputs(inputs);
            m_NeuralNetwork.PropagateInputsFwd();

            var outputs = m_NeuralNetwork.GetOutputs();
        }

        private NeuralNetwork m_NeuralNetwork;
    }
}
