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
            SuspendLayout();
            // 
            // buttonInitializeNN
            // 
            buttonInitializeNN.Location = new Point(12, 12);
            buttonInitializeNN.Name = "buttonInitializeNN";
            buttonInitializeNN.Size = new Size(91, 44);
            buttonInitializeNN.TabIndex = 1;
            buttonInitializeNN.Text = "Initialize";
            buttonInitializeNN.UseVisualStyleBackColor = true;
            buttonInitializeNN.Click += buttonInitializeNN_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonInitializeNN);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Button buttonInitializeNN;
    }
}
