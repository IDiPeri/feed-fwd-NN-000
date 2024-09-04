
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Simple_FFNN
{
    internal class NeuralNetwork
    {
        // We'll initialize the weights with a random value from -20 to +20
        public const double InitialWeightRange = 20;

        // Value for the bias unit
        public const double BiasValue = 1.0;

        public const double ActivationFunctionCurvature = 0.2;

        public const double TRUE = 0.5;
        public const double FALSE = -0.5;

        // Keep this many weights buffers to know the before, current and next time
        public const int TimeBufferSize = 3;

        /// <summary>
        /// Simple constructor to create a custom sized NN
        /// </summary>
        /// <param name="inputsCount"></param>
        /// <param name="hiddenLayerCount"></param>
        /// <param name="nodesPerHiddenLayer"></param>
        /// <param name="outputCount"></param>
        public NeuralNetwork(int inputsCount, int hiddenLayerCount, int nodesPerHiddenLayer, int outputCount)
        {
            // Sanity checking on inputs

            if (inputsCount < 1) { throw new Exception("There must be at least 1 input node"); }
            if (outputCount < 1) { throw new Exception("There must be at least 1 output node"); }
            if (hiddenLayerCount < 0) { throw new Exception("The hidden layer count must be non-negative"); }
            if ((hiddenLayerCount > 0) && (nodesPerHiddenLayer < 1))
            {
                throw new Exception("There must be at least 1 hidden node if there's at least 1 hidden layer");
            }

            // Create the input, hidden and output layers

            Layers = new List<NNLayer>();

            NNLayer inputLayer = new NNLayer(inputsCount);
            Layers.Add(inputLayer);

            for(int i = 0; i < hiddenLayerCount; i++)
            {
                Layers.Add(new NNLayer(nodesPerHiddenLayer));
            }

            NNLayer outputLayer = new NNLayer(outputCount);
            Layers.Add(outputLayer);

            // Create all of the weight matrices inbetween each layer
            Weights = new List<Matrix<double>[]>();
            for (int i = 1; i < Layers.Count; i++)
            {
                int previousOutputCount = Layers[i-1].Outputs.Count;
                int currentInputCount = Layers[i].SumOfInputs.Count;

                // Weight matrix should be previousOutputCount (rows) x currentInputCount (count)
                Matrix<double>[] weights = new Matrix<double>[TimeBufferSize];
                for(int t=0; t < TimeBufferSize; t++)
                {
                    weights[t] = DenseMatrix.OfArray(new double[previousOutputCount, currentInputCount]);
                }

                // Initialize the weights
                for (int r = 0; r < previousOutputCount; r++)
                {
                    for (int c = 0; c < currentInputCount; c++)
                    {
                        for (int t = 0; t < TimeBufferSize; t++)
                        {
                            if (t == 0)
                            {
                                weights[t][r, c] = ((m_Random.NextDouble() * 2.0) - 1.0) * InitialWeightRange;
                            }
                            else
                            {
                                weights[t][r, c] = weights[0][r, c];
                            }
                        }
                    }
                }

                Weights.Add(weights);
            }
        }

        public int CurrentBufferIndex { get; private set; } = 0;

        private int PreviousBufferIndex
        {
            get { return (CurrentBufferIndex + (TimeBufferSize-1)) % TimeBufferSize; }
        }

        private int NextBufferIndex
        {
            get { return (CurrentBufferIndex + 1) % TimeBufferSize; }
        }

        private Random m_Random = new Random();

        public List<NNLayer> Layers { get; private set; }
        public List<Matrix<double>[]> Weights { get; private set; }

        public void SetInputs(Vector<double> inputs)
        {
            if (inputs.Count != Layers[0].SumOfInputs.Count)
            {
                throw new Exception("Wrong number of inputs");
            }

            // ///////////////////////////////////////////////////////////////////
            //!FIX: add this to NNLayer instead? since it's a special propagation pattern

            // Copy the inputs directly into the sum of inputs for the first layer
            for(int i = 0; i< inputs.Count; i++)
            {
                Layers[0].SumOfInputs[i] = inputs[i];
            }

            // Copy the sum of inputs directly into the outputs for the first layer
            for (int i = 0; i < Layers[0].SumOfInputs.Count; i++)
            {
                Layers[0].Outputs[i] = Layers[0].SumOfInputs[i];
            }
            // Make sure the last output element is always 1.0 (for the bias)
            Layers[0].Outputs[Layers[0].Outputs.Count - 1] = NeuralNetwork.BiasValue;
            // ///////////////////////////////////////////////////////////////////
        }

        public void PropagateInputsFwd()
        {
            // Assume SetInputs() has been called and the first layer's output vector is set properly

            for (int i = 1; i < Layers.Count; i++)
            {
                var previousOutput = Layers[i - 1].Outputs;
                var weights = Weights[i - 1][CurrentBufferIndex];

                // !FIX: should we NOT make this public and add SetSumOfInputs for sanity check about dimensions?
                Layers[i].SumOfInputs = previousOutput * weights;

                // Put the sum of inputs through the activation function
                Layers[i].CalculateOutputs();
            }
        }

        public void SetTargetOutputs(Vector<double> targetOutputs)
        {
            var lastLayer = Layers[Layers.Count - 1];
            lastLayer.Delta = targetOutputs - lastLayer.GetOutputsWithoutBias();
            //!FIX: calculate error signal
        }

        public Vector<double> GetOutputsWithoutBias()
        {
            var lastLayer = Layers[Layers.Count - 1];
            return lastLayer.GetOutputsWithoutBias();
        }
    }
}
