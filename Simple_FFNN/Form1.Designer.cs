namespace Simple_FFNN
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonInitializeNN = new Button();
            training_pictureBox_TrainingPattern = new PictureBox();
            label1 = new Label();
            groupBox_TrainingPattern = new GroupBox();
            groupBox1 = new GroupBox();
            radioButton_CircleInSquare = new RadioButton();
            radioButton_LessThan = new RadioButton();
            radioButton_XOR = new RadioButton();
            tabControl_WorkFlow = new TabControl();
            tabPage_Setup = new TabPage();
            setup_button_Next = new Button();
            setup_textBox_HiddenLayers = new TextBox();
            label2 = new Label();
            setup_textBox_NodesPerHiddenLayer = new TextBox();
            tabPage_Training = new TabPage();
            training_button_Next = new Button();
            buttonTestInvFn = new Button();
            groupBox2 = new GroupBox();
            radioButton_TestInvFn_True = new RadioButton();
            radioButton_TestInvFn_False = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)training_pictureBox_TrainingPattern).BeginInit();
            groupBox_TrainingPattern.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl_WorkFlow.SuspendLayout();
            tabPage_Setup.SuspendLayout();
            tabPage_Training.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonInitializeNN
            // 
            buttonInitializeNN.Location = new Point(494, 151);
            buttonInitializeNN.Name = "buttonInitializeNN";
            buttonInitializeNN.Size = new Size(91, 44);
            buttonInitializeNN.TabIndex = 1;
            buttonInitializeNN.Text = "Test...";
            buttonInitializeNN.UseVisualStyleBackColor = true;
            buttonInitializeNN.Click += buttonInitializeNN_Click;
            // 
            // training_pictureBox_TrainingPattern
            // 
            training_pictureBox_TrainingPattern.Location = new Point(201, 26);
            training_pictureBox_TrainingPattern.Name = "training_pictureBox_TrainingPattern";
            training_pictureBox_TrainingPattern.Size = new Size(128, 128);
            training_pictureBox_TrainingPattern.TabIndex = 2;
            training_pictureBox_TrainingPattern.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 64);
            label1.Name = "label1";
            label1.Size = new Size(167, 20);
            label1.TabIndex = 3;
            label1.Text = "Nodes per hidden layer:";
            // 
            // groupBox_TrainingPattern
            // 
            groupBox_TrainingPattern.Controls.Add(groupBox2);
            groupBox_TrainingPattern.Controls.Add(buttonTestInvFn);
            groupBox_TrainingPattern.Controls.Add(groupBox1);
            groupBox_TrainingPattern.Controls.Add(training_pictureBox_TrainingPattern);
            groupBox_TrainingPattern.Location = new Point(17, 17);
            groupBox_TrainingPattern.Name = "groupBox_TrainingPattern";
            groupBox_TrainingPattern.Size = new Size(360, 230);
            groupBox_TrainingPattern.TabIndex = 4;
            groupBox_TrainingPattern.TabStop = false;
            groupBox_TrainingPattern.Text = "Training Pattern";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton_CircleInSquare);
            groupBox1.Controls.Add(radioButton_LessThan);
            groupBox1.Controls.Add(radioButton_XOR);
            groupBox1.Location = new Point(15, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(163, 109);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // radioButton_CircleInSquare
            // 
            radioButton_CircleInSquare.AutoSize = true;
            radioButton_CircleInSquare.Location = new Point(6, 77);
            radioButton_CircleInSquare.Name = "radioButton_CircleInSquare";
            radioButton_CircleInSquare.Size = new Size(133, 24);
            radioButton_CircleInSquare.TabIndex = 6;
            radioButton_CircleInSquare.TabStop = true;
            radioButton_CircleInSquare.Text = "Circle in Square";
            radioButton_CircleInSquare.UseVisualStyleBackColor = true;
            radioButton_CircleInSquare.CheckedChanged += radioButton_CircleInSquare_CheckedChanged;
            // 
            // radioButton_LessThan
            // 
            radioButton_LessThan.AutoSize = true;
            radioButton_LessThan.Checked = true;
            radioButton_LessThan.Location = new Point(6, 17);
            radioButton_LessThan.Name = "radioButton_LessThan";
            radioButton_LessThan.Size = new Size(93, 24);
            radioButton_LessThan.TabIndex = 4;
            radioButton_LessThan.TabStop = true;
            radioButton_LessThan.Text = "Less Than";
            radioButton_LessThan.UseVisualStyleBackColor = true;
            radioButton_LessThan.CheckedChanged += radioButton_LessThan_CheckedChanged;
            // 
            // radioButton_XOR
            // 
            radioButton_XOR.AutoSize = true;
            radioButton_XOR.Location = new Point(6, 47);
            radioButton_XOR.Name = "radioButton_XOR";
            radioButton_XOR.Size = new Size(59, 24);
            radioButton_XOR.TabIndex = 5;
            radioButton_XOR.TabStop = true;
            radioButton_XOR.Text = "XOR";
            radioButton_XOR.UseVisualStyleBackColor = true;
            radioButton_XOR.CheckedChanged += radioButton_XOR_CheckedChanged;
            // 
            // tabControl_WorkFlow
            // 
            tabControl_WorkFlow.Controls.Add(tabPage_Setup);
            tabControl_WorkFlow.Controls.Add(tabPage_Training);
            tabControl_WorkFlow.Location = new Point(12, 12);
            tabControl_WorkFlow.Name = "tabControl_WorkFlow";
            tabControl_WorkFlow.SelectedIndex = 0;
            tabControl_WorkFlow.Size = new Size(753, 483);
            tabControl_WorkFlow.TabIndex = 5;
            // 
            // tabPage_Setup
            // 
            tabPage_Setup.Controls.Add(setup_button_Next);
            tabPage_Setup.Controls.Add(setup_textBox_HiddenLayers);
            tabPage_Setup.Controls.Add(label2);
            tabPage_Setup.Controls.Add(setup_textBox_NodesPerHiddenLayer);
            tabPage_Setup.Controls.Add(label1);
            tabPage_Setup.Location = new Point(4, 29);
            tabPage_Setup.Name = "tabPage_Setup";
            tabPage_Setup.Padding = new Padding(3);
            tabPage_Setup.Size = new Size(745, 450);
            tabPage_Setup.TabIndex = 0;
            tabPage_Setup.Text = "Setup";
            tabPage_Setup.UseVisualStyleBackColor = true;
            // 
            // setup_button_Next
            // 
            setup_button_Next.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            setup_button_Next.Location = new Point(648, 400);
            setup_button_Next.Name = "setup_button_Next";
            setup_button_Next.Size = new Size(91, 44);
            setup_button_Next.TabIndex = 7;
            setup_button_Next.Text = "Next...";
            setup_button_Next.UseVisualStyleBackColor = true;
            setup_button_Next.Click += setup_button_Next_Click;
            // 
            // setup_textBox_HiddenLayers
            // 
            setup_textBox_HiddenLayers.Location = new Point(234, 21);
            setup_textBox_HiddenLayers.Name = "setup_textBox_HiddenLayers";
            setup_textBox_HiddenLayers.Size = new Size(57, 27);
            setup_textBox_HiddenLayers.TabIndex = 6;
            setup_textBox_HiddenLayers.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 24);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 5;
            label2.Text = "Hidden Layers:";
            // 
            // setup_textBox_NodesPerHiddenLayer
            // 
            setup_textBox_NodesPerHiddenLayer.Location = new Point(234, 61);
            setup_textBox_NodesPerHiddenLayer.Name = "setup_textBox_NodesPerHiddenLayer";
            setup_textBox_NodesPerHiddenLayer.Size = new Size(57, 27);
            setup_textBox_NodesPerHiddenLayer.TabIndex = 4;
            setup_textBox_NodesPerHiddenLayer.Text = "9";
            // 
            // tabPage_Training
            // 
            tabPage_Training.Controls.Add(training_button_Next);
            tabPage_Training.Controls.Add(groupBox_TrainingPattern);
            tabPage_Training.Controls.Add(buttonInitializeNN);
            tabPage_Training.Location = new Point(4, 29);
            tabPage_Training.Name = "tabPage_Training";
            tabPage_Training.Padding = new Padding(3);
            tabPage_Training.Size = new Size(745, 450);
            tabPage_Training.TabIndex = 1;
            tabPage_Training.Text = "Training";
            tabPage_Training.UseVisualStyleBackColor = true;
            // 
            // training_button_Next
            // 
            training_button_Next.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            training_button_Next.Location = new Point(648, 400);
            training_button_Next.Name = "training_button_Next";
            training_button_Next.Size = new Size(91, 44);
            training_button_Next.TabIndex = 8;
            training_button_Next.Text = "Next...";
            training_button_Next.UseVisualStyleBackColor = true;
            // 
            // buttonTestInvFn
            // 
            buttonTestInvFn.Location = new Point(6, 170);
            buttonTestInvFn.Name = "buttonTestInvFn";
            buttonTestInvFn.Size = new Size(172, 44);
            buttonTestInvFn.TabIndex = 9;
            buttonTestInvFn.Text = "Test Inverse Fn";
            buttonTestInvFn.UseVisualStyleBackColor = true;
            buttonTestInvFn.Click += buttonTestInvFn_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton_TestInvFn_False);
            groupBox2.Controls.Add(radioButton_TestInvFn_True);
            groupBox2.Location = new Point(201, 160);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(143, 56);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            // 
            // radioButton_TestInvFn_True
            // 
            radioButton_TestInvFn_True.AutoSize = true;
            radioButton_TestInvFn_True.Checked = true;
            radioButton_TestInvFn_True.Location = new Point(6, 20);
            radioButton_TestInvFn_True.Name = "radioButton_TestInvFn_True";
            radioButton_TestInvFn_True.Size = new Size(58, 24);
            radioButton_TestInvFn_True.TabIndex = 5;
            radioButton_TestInvFn_True.Text = "True";
            radioButton_TestInvFn_True.UseVisualStyleBackColor = true;
            // 
            // radioButton_TestInvFn_False
            // 
            radioButton_TestInvFn_False.AutoSize = true;
            radioButton_TestInvFn_False.Location = new Point(70, 20);
            radioButton_TestInvFn_False.Name = "radioButton_TestInvFn_False";
            radioButton_TestInvFn_False.Size = new Size(62, 24);
            radioButton_TestInvFn_False.TabIndex = 6;
            radioButton_TestInvFn_False.Text = "False";
            radioButton_TestInvFn_False.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1110, 542);
            Controls.Add(tabControl_WorkFlow);
            Name = "Form1";
            Text = "Simple Feed Forward Neural Network Training and Testing";
            ((System.ComponentModel.ISupportInitialize)training_pictureBox_TrainingPattern).EndInit();
            groupBox_TrainingPattern.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl_WorkFlow.ResumeLayout(false);
            tabPage_Setup.ResumeLayout(false);
            tabPage_Setup.PerformLayout();
            tabPage_Training.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button buttonInitializeNN;
        private PictureBox training_pictureBox_TrainingPattern;
        private Label label1;
        private GroupBox groupBox_TrainingPattern;
        private TabControl tabControl_WorkFlow;
        private TabPage tabPage_Setup;
        private TabPage tabPage_Training;
        private TextBox setup_textBox_HiddenLayers;
        private Label label2;
        private TextBox setup_textBox_NodesPerHiddenLayer;
        private Button setup_button_Next;
        private Button training_button_Next;
        private GroupBox groupBox1;
        private RadioButton radioButton_CircleInSquare;
        private RadioButton radioButton_LessThan;
        private RadioButton radioButton_XOR;
        private GroupBox groupBox2;
        private RadioButton radioButton_TestInvFn_False;
        private RadioButton radioButton_TestInvFn_True;
        private Button buttonTestInvFn;
    }
}
