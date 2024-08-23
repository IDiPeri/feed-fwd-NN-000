using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Simple_FFNN
{
    internal class NNLayer
    {
        public NNLayer(int nodeCount)
        {
            SumOfInputs = DenseVector.OfArray(new double[nodeCount]);
            Delta = DenseVector.OfArray(new double[nodeCount]);

            // Reserve the last element for the bias unit (i.e. always 1.0)
            Outputs = DenseVector.OfArray(new double[nodeCount + 1]);
            Outputs[nodeCount] = NeuralNetwork.BiasValue;
        }

        public Vector<double> SumOfInputs { get; set; }
        public Vector<double> Outputs { get; private set; }
        public Vector<double> Delta { get; private set; }

        public void CalculateOutputs()
        {
            for (int i = 0; i < SumOfInputs.Count; i++)
            {
                Outputs[i] = ActivationFunction(SumOfInputs[i]);
            }

            // Make sure the last output element is always 1.0 (for the bias)
            Outputs[Outputs.Count - 1] = NeuralNetwork.BiasValue;
        }

        private double ActivationFunction(double input)
        {
            // Hyperbolic tangent
            var v = Math.Exp(-NeuralNetwork.ActivationFunctionCurvature * input);
            var sigmoid = (1 - v) / (1 + v);
            return sigmoid;
        }
    }
}
