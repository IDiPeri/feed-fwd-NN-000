namespace Simple_FFNN
{
    using System;
    using System.Drawing.Imaging;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Double;

    public partial class Form1 : Form
    {
        public enum WorkflowTabs
        {
            Setup = 0,
            Training = 1,
        }

        public enum TrainingPattern
        {
            LessThan,
            XOR,
            CircleInSquare
        }

        public Form1()
        {
            InitializeComponent();

            SelectTab(0);
            DisplaySelectedTrainingImage();
        }

        const double TRAINING_CIRCLE_RADIUS = 0.35;
        const double THRESHOLD_FLOATINGPOINT_LOGIC = 0.1;

        #region Data Members
        private NeuralNetwork m_NeuralNetwork;
        private Random m_Random = new Random();
        #endregion

        private void SelectTab(WorkflowTabs tabIndex)
        {
            tabControl_WorkFlow.SelectedIndex = (int)tabIndex;
        }

        private Bitmap GenerateImage(int width, int height,
                                     Func<double, double, double> normalizedFunction)
        {
            Bitmap outputImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            //!FIX: dirt simple slow process to get started
            for (int y_pixel = 0; y_pixel < height; y_pixel++)
            {
                double y_coord = ((double)y_pixel / (double)height) - 0.5;
                for (int x_pixel = 0; x_pixel < width; x_pixel++)
                {
                    double x_coord = ((double)x_pixel / (double)width) - 0.5;
                    double fnOutput = normalizedFunction(x_coord, y_coord);

                    Color pixelColor = Color.Gray;
                    if (Math.Abs(fnOutput - NeuralNetwork.TRUE) < THRESHOLD_FLOATINGPOINT_LOGIC)
                    {
                        pixelColor = Color.Green;
                    }
                    else if (Math.Abs(fnOutput - NeuralNetwork.FALSE) < THRESHOLD_FLOATINGPOINT_LOGIC)
                    {
                        pixelColor = Color.Red;
                    }

                    outputImage.SetPixel(x_pixel, y_pixel, pixelColor);
                }
            }

            return outputImage;
        }

        #region Trainng functions

        private void Coordinate_TrainingPattern_LessThan(bool outputIsPositive,
                                                         out double x, out double y)
        {
            x = m_Random.NextDouble();

            if (outputIsPositive)
            {
                y = m_Random.NextDouble() * (x - double.Epsilon);
            }
            else
            {
                y = m_Random.NextDouble() * ((1.0 - x) - double.Epsilon);
                y = y + x;
            }

            x = x - 0.5;
            y = y - 0.5;
        }

        private void Coordinate_TrainingPattern_XOR(bool outputIsPositive,
                                                    out double x, out double y)
        {
            // Pick a random boolean value
            bool x_logic = (m_Random.NextDouble() > 0.5);
            // Adjust other boolean value so the XOR output matches outputIsPositive
            bool y_logic = x_logic;
            if (outputIsPositive)
            {
                y_logic = !x_logic;
            }

            x = m_Random.NextDouble() * 0.5;
            if (!x_logic)
            {
                x -= 0.5;
            }

            y = m_Random.NextDouble() * 0.5;
            if (!y_logic)
            {
                y -= 0.5;
            }
        }

        private void Coordinate_TrainingPattern_CircleInSquare(bool outputIsPositive,
                                                               out double x, out double y)
        {
            double theta = m_Random.NextDouble() * Math.PI * 2;

            double radius;
            if (outputIsPositive)
            {
                radius = m_Random.NextDouble() * TRAINING_CIRCLE_RADIUS;
            }
            else
            {
                //!FIX: need to limit random sample to only within the world limits
                //double distanceToEdge

                //!FIX: for now just hack it with the circumscribed circle around the world limits
                radius = m_Random.NextDouble() * (Math.Sqrt(2.0) / 2.0 - TRAINING_CIRCLE_RADIUS);
                radius += TRAINING_CIRCLE_RADIUS + double.Epsilon;
            }

            x = Math.Cos(theta) * radius;
            y = Math.Sin(theta) * radius;

            //!FIX: keep in limits since we're hacking the range above
            if (x < -0.5) { x = -0.5 + double.Epsilon; }
            if (x > 0.5) { x = 0.5 - double.Epsilon; }
            if (y < -0.5) { y = -0.5 + double.Epsilon; }
            if (y > 0.5) { y = 0.5 - double.Epsilon; }
        }

        private double TrainingPattern_LessThan(double x, double y)
        {
            if (x < y)
            {
                return NeuralNetwork.TRUE;
            }
            else
            {
                return NeuralNetwork.FALSE;
            }
        }

        private double TrainingPattern_XOR(double x, double y)
        {
            // Negative values are FALSE
            // Non-Negative values are TRUE

            bool X_logical = true;
            if (x < 0)
            {
                X_logical = false;
            }

            bool Y_logical = true;
            if (y < 0)
            {
                Y_logical = false;
            }

            if (X_logical ^ Y_logical)
            {
                return NeuralNetwork.TRUE;
            }
            else
            {
                return NeuralNetwork.FALSE;
            }
        }

        private double TrainingPattern_CircleInSquare(double x, double y)
        {
            double distance = Math.Sqrt(x * x + y * y);
            if (distance <= TRAINING_CIRCLE_RADIUS)
            {
                return NeuralNetwork.TRUE;
            }
            else
            {
                return NeuralNetwork.FALSE;
            }
        }

        #endregion

        #region Setup Tab

        private void setup_button_Next_Click(object sender, EventArgs e)
        {
            int hiddenLayerCount = int.Parse(setup_textBox_HiddenLayers.Text);
            int nodesPerHiddenLayer = int.Parse(setup_textBox_NodesPerHiddenLayer.Text);
            m_NeuralNetwork = new NeuralNetwork(inputsCount: 2,
                                                hiddenLayerCount: hiddenLayerCount,
                                                nodesPerHiddenLayer: nodesPerHiddenLayer,
                                                outputCount: 1);

            SelectTab(WorkflowTabs.Training);
        }

        #endregion

        #region Training Tab
        public delegate void Inv2DFunc(bool outputIsPositive,
                                        out double x, out double y);

        private Inv2DFunc DeterminSelectedTrainingInverseFunction()
        {
            Inv2DFunc inverseFunction = null;

            TrainingPattern selectedPattern = DeterminSelectedTrainingPattern();
            switch (selectedPattern)
            {
                case TrainingPattern.LessThan:
                    inverseFunction = Coordinate_TrainingPattern_LessThan;
                    break;

                case TrainingPattern.XOR:
                    inverseFunction = Coordinate_TrainingPattern_XOR;
                    break;

                case TrainingPattern.CircleInSquare:
                    inverseFunction = Coordinate_TrainingPattern_CircleInSquare;
                    break;

                default:
                    throw new Exception("Unknown training pattern");
            }

            return inverseFunction;
        }

        private Func<double, double, double> DeterminSelectedTrainingFunction()
        {
            Func<double, double, double> normalizedFunction = null;

            TrainingPattern selectedPattern = DeterminSelectedTrainingPattern();
            switch (selectedPattern)
            {
                case TrainingPattern.LessThan:
                    normalizedFunction = TrainingPattern_LessThan;
                    break;

                case TrainingPattern.XOR:
                    normalizedFunction = TrainingPattern_XOR;
                    break;

                case TrainingPattern.CircleInSquare:
                    normalizedFunction = TrainingPattern_CircleInSquare;
                    break;

                default:
                    throw new Exception("Unknown training pattern");
            }

            return normalizedFunction;
        }

        private TrainingPattern DeterminSelectedTrainingPattern()
        {
            TrainingPattern selectedPattern;
            if (radioButton_LessThan.Checked)
            {
                selectedPattern = TrainingPattern.LessThan;
            }
            else if (radioButton_XOR.Checked)
            {
                selectedPattern = TrainingPattern.XOR;
            }
            else if (radioButton_CircleInSquare.Checked)
            {
                selectedPattern = TrainingPattern.CircleInSquare;
            }
            else
            {
                selectedPattern = TrainingPattern.LessThan;
            }
            return selectedPattern;
        }

        private void DisplaySelectedTrainingImage()
        {
            TrainingPattern selectedPattern = DeterminSelectedTrainingPattern();

            Bitmap trainingImage;
            int imageWidth = 128;
            int imageHeight = 128;
            switch (selectedPattern)
            {
                case TrainingPattern.LessThan:
                    trainingImage = GenerateImage(imageWidth, imageHeight, TrainingPattern_LessThan);
                    break;

                case TrainingPattern.XOR:
                    trainingImage = GenerateImage(imageWidth, imageHeight, TrainingPattern_XOR);
                    break;

                case TrainingPattern.CircleInSquare:
                    trainingImage = GenerateImage(imageWidth, imageHeight, TrainingPattern_CircleInSquare);
                    break;

                default:
                    throw new Exception("Unknown training pattern");
            }

            if (training_pictureBox_TrainingPattern.Image != null)
            {
                var oldImage = training_pictureBox_TrainingPattern.Image;
                training_pictureBox_TrainingPattern.Image = null;
                oldImage.Dispose();
            }
            training_pictureBox_TrainingPattern.Image = trainingImage;
        }

        private void radioButton_LessThan_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySelectedTrainingImage();
        }

        private void radioButton_XOR_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySelectedTrainingImage();
        }

        private void radioButton_CircleInSquare_CheckedChanged(object sender, EventArgs e)
        {
            DisplaySelectedTrainingImage();
        }

        private void buttonTestInvFn_Click(object sender, EventArgs e)
        {
            TrainingPattern selectedPattern = DeterminSelectedTrainingPattern();

            Bitmap trainingImage = training_pictureBox_TrainingPattern.Image as Bitmap;
            int imageWidth = 128;
            int imageHeight = 128;
            bool outputIsPositive = false;
            Color pixelColor = Color.Black;
            double expectedOutput = NeuralNetwork.FALSE;
            if (radioButton_TestInvFn_True.Checked)
            {
                outputIsPositive = true;
                pixelColor = Color.White;
                expectedOutput = NeuralNetwork.TRUE;
            }

            double x_coord;
            double y_coord;
            int x_pixel;
            int y_pixel;

            for (int i = 0; i < 100; i++)
            {
                switch (selectedPattern)
                {
                    case TrainingPattern.LessThan:
                        Coordinate_TrainingPattern_LessThan(outputIsPositive, out x_coord, out y_coord);
                        x_pixel = (int)((x_coord + 0.5) * (imageWidth - 1) + 0.5);
                        y_pixel = (int)((y_coord + 0.5) * (imageHeight - 1) + 0.5);
                        trainingImage.SetPixel(x_pixel, y_pixel, pixelColor);
                        double fnOutput = TrainingPattern_LessThan(x_coord, y_coord);
                        //!FIX: check fnOutput against expectedOutput
                        break;

                    case TrainingPattern.XOR:
                        Coordinate_TrainingPattern_XOR(outputIsPositive, out x_coord, out y_coord);
                        x_pixel = (int)((x_coord + 0.5) * (imageWidth - 1) + 0.5);
                        y_pixel = (int)((y_coord + 0.5) * (imageHeight - 1) + 0.5);
                        trainingImage.SetPixel(x_pixel, y_pixel, pixelColor);
                        break;

                    case TrainingPattern.CircleInSquare:
                        Coordinate_TrainingPattern_CircleInSquare(outputIsPositive, out x_coord, out y_coord);
                        x_pixel = (int)((x_coord + 0.5) * (imageWidth - 1) + 0.5);
                        y_pixel = (int)((y_coord + 0.5) * (imageHeight - 1) + 0.5);
                        trainingImage.SetPixel(x_pixel, y_pixel, pixelColor); ;
                        break;

                    default:
                        throw new Exception("Unknown training pattern");
                }
            }

            training_pictureBox_TrainingPattern.Refresh();
        }

        private void buttonTest000_Click(object sender, EventArgs e)
        {
            /* !FIX: remove this
            m_NeuralNetwork = new NeuralNetwork(inputsCount: 2, hiddenLayerCount: 1, nodesPerHiddenLayer: 9, outputCount: 1);

            Vector<double> inputs = DenseVector.OfArray(new double[2]);
            inputs[0] = 0.25;
            inputs[1] = 0.15;

            m_NeuralNetwork.SetInputs(inputs);
            m_NeuralNetwork.PropagateInputsFwd();

            var outputs = m_NeuralNetwork.GetOutputs();
            */
        }

        private double CurrentNNFunction(double x, double y)
        {
            Vector<double> inputs = DenseVector.OfArray(new double[2]);
            inputs[0] = x;
            inputs[1] = y;

            m_NeuralNetwork.SetInputs(inputs);
            m_NeuralNetwork.PropagateInputsFwd();

            var outputs = m_NeuralNetwork.GetOutputsWithoutBias();
            return outputs[0];
        }

        private void buttonDisplayCurrentNN_Click(object sender, EventArgs e)
        {
            Bitmap currentNNImage;
            int imageWidth = 128;
            int imageHeight = 128;
            currentNNImage = GenerateImage(imageWidth, imageHeight, CurrentNNFunction);

            if (training_pictureBox_CurrentNN.Image != null)
            {
                var oldImage = training_pictureBox_CurrentNN.Image;
                training_pictureBox_CurrentNN.Image = null;
                oldImage.Dispose();
            }
            training_pictureBox_CurrentNN.Image = currentNNImage;
        }

        private void training_button_Next_Click(object sender, EventArgs e)
        {

        }

        private void buttonTrain1DataPoint_Click(object sender, EventArgs e)
        {
            // ///////////////////////////////////////////////////////////////////////////////////////////
            // Come up with a random x,y coordinate which matches the selected output option True or False
            Inv2DFunc inverseFunction = DeterminSelectedTrainingInverseFunction();

            bool outputIsPositive = false;
            double expectedOutput = NeuralNetwork.FALSE;
            if (radioButton_TestInvFn_True.Checked)
            {
                outputIsPositive = true;
                expectedOutput = NeuralNetwork.TRUE;
            }

            inverseFunction(outputIsPositive, out double x, out double y);
            Vector<double> inputs = DenseVector.OfArray(new double[2]);
            inputs[0] = x;
            inputs[1] = y;

            // Propagate the inputs forward through the network
            m_NeuralNetwork.SetInputs(inputs);
            m_NeuralNetwork.PropagateInputsFwd();

            var outputs = m_NeuralNetwork.GetOutputsWithoutBias();

            // ///////////////////////////////////////////////////////////////////////////////////////////
            // Calculate the error signal
            Vector<double> targetOutputs = DenseVector.OfArray(new double[1]);
            targetOutputs[0] = expectedOutput;
            m_NeuralNetwork.SetTargetOutputs(targetOutputs);
            m_NeuralNetwork.PropagateErrorSignalBackwards();
        }

        #endregion
    }
}
