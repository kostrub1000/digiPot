namespace digiPot
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
            Open_COM_ports_button = new Button();
            available_COM_ports_comboBox1 = new ComboBox();
            up_button = new Button();
            down_button = new Button();
            resistance_textBox = new Only_positive_int_less_than_100_allowed_Textbox();
            label1 = new Label();
            set_resistance_button = new Button();
            potentiometer_number_numericUpDown = new NumericUpDown();
            available_COM_ports_comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)potentiometer_number_numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // Open_COM_ports_button
            // 
            Open_COM_ports_button.Location = new Point(62, 54);
            Open_COM_ports_button.Name = "Open_COM_ports_button";
            Open_COM_ports_button.Size = new Size(138, 23);
            Open_COM_ports_button.TabIndex = 0;
            Open_COM_ports_button.Text = "Открыть COM порты";
            Open_COM_ports_button.UseVisualStyleBackColor = true;
            Open_COM_ports_button.Click += Open_COM_ports_button_Click;
            // 
            // available_COM_ports_comboBox1
            // 
            available_COM_ports_comboBox1.FormattingEnabled = true;
            available_COM_ports_comboBox1.Location = new Point(215, 54);
            available_COM_ports_comboBox1.Name = "available_COM_ports_comboBox1";
            available_COM_ports_comboBox1.Size = new Size(121, 23);
            available_COM_ports_comboBox1.TabIndex = 1;
            // 
            // up_button
            // 
            up_button.Enabled = false;
            up_button.Location = new Point(540, 35);
            up_button.Name = "up_button";
            up_button.Size = new Size(177, 23);
            up_button.TabIndex = 2;
            up_button.Text = "Увеличить сопротивление";
            up_button.UseVisualStyleBackColor = true;
            up_button.Click += up_button_Click;
            // 
            // down_button
            // 
            down_button.Enabled = false;
            down_button.Location = new Point(540, 81);
            down_button.Name = "down_button";
            down_button.Size = new Size(177, 23);
            down_button.TabIndex = 3;
            down_button.Text = "Уменьшить сопротивление";
            down_button.UseVisualStyleBackColor = true;
            down_button.Click += down_button_Click;
            // 
            // resistance_textBox
            // 
            resistance_textBox.Location = new Point(566, 122);
            resistance_textBox.Name = "resistance_textBox";
            resistance_textBox.Size = new Size(100, 23);
            resistance_textBox.TabIndex = 4;
            resistance_textBox.Text = "50";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(672, 125);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 5;
            label1.Text = "%";
            // 
            // set_resistance_button
            // 
            set_resistance_button.Enabled = false;
            set_resistance_button.Location = new Point(540, 163);
            set_resistance_button.Name = "set_resistance_button";
            set_resistance_button.Size = new Size(177, 23);
            set_resistance_button.TabIndex = 6;
            set_resistance_button.Text = "Задать сопротивление";
            set_resistance_button.UseVisualStyleBackColor = true;
            set_resistance_button.Click += set_resistance_button_Click;
            // 
            // potentiometer_number_numericUpDown
            // 
            potentiometer_number_numericUpDown.Location = new Point(355, 54);
            potentiometer_number_numericUpDown.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            potentiometer_number_numericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            potentiometer_number_numericUpDown.Name = "potentiometer_number_numericUpDown";
            potentiometer_number_numericUpDown.ReadOnly = true;
            potentiometer_number_numericUpDown.Size = new Size(120, 23);
            potentiometer_number_numericUpDown.TabIndex = 7;
            potentiometer_number_numericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            potentiometer_number_numericUpDown.ValueChanged += potentiometer_number_numericUpDown_ValueChanged;
            // 
            // available_COM_ports_comboBox2
            // 
            available_COM_ports_comboBox2.FormattingEnabled = true;
            available_COM_ports_comboBox2.Location = new Point(215, 94);
            available_COM_ports_comboBox2.Name = "available_COM_ports_comboBox2";
            available_COM_ports_comboBox2.Size = new Size(121, 23);
            available_COM_ports_comboBox2.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 260);
            Controls.Add(available_COM_ports_comboBox2);
            Controls.Add(potentiometer_number_numericUpDown);
            Controls.Add(set_resistance_button);
            Controls.Add(label1);
            Controls.Add(resistance_textBox);
            Controls.Add(down_button);
            Controls.Add(up_button);
            Controls.Add(available_COM_ports_comboBox1);
            Controls.Add(Open_COM_ports_button);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)potentiometer_number_numericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Open_COM_ports_button;
        private ComboBox available_COM_ports_comboBox1;
        private Button up_button;
        private Button down_button;
        private Only_positive_int_less_than_100_allowed_Textbox resistance_textBox;
        private Label label1;
        private Button set_resistance_button;
        private NumericUpDown potentiometer_number_numericUpDown;
        private ComboBox available_COM_ports_comboBox2;
    }
}
