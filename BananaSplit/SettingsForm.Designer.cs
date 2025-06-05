namespace BananaSplit
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            BlackFramePixelThresholdInput = new System.Windows.Forms.NumericUpDown();
            bplabel1 = new System.Windows.Forms.Label();
            ShowLogCheckbox = new System.Windows.Forms.CheckBox();
            ReferenceFrameOffsetLabel = new System.Windows.Forms.Label();
            ReferenceFrameOffsetInput = new System.Windows.Forms.NumericUpDown();
            DeleteOriginalCheckbox = new System.Windows.Forms.CheckBox();
            ProcessTypeLabel = new System.Windows.Forms.Label();
            ProcessTypeComboBox = new System.Windows.Forms.ComboBox();
            BlackFrameDurationInput = new System.Windows.Forms.NumericUpDown();
            BlackFrameThresholdInput = new System.Windows.Forms.NumericUpDown();
            BlackFrameThresholdLabel = new System.Windows.Forms.Label();
            BlackFrameDurationLabel = new System.Windows.Forms.Label();
            FFMPEGArgumentsGroupBox = new System.Windows.Forms.GroupBox();
            FFMPEGArgumentsInput = new System.Windows.Forms.TextBox();
            FFMPEGArgumentsLegend = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            MultiplierInput = new System.Windows.Forms.NumericUpDown();
            label8 = new System.Windows.Forms.Label();
            PaddingInput = new System.Windows.Forms.NumericUpDown();
            label7 = new System.Windows.Forms.Label();
            StartIndexInput = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            RenameOriginalCheckBox = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            RenameTypeComboBox = new System.Windows.Forms.ComboBox();
            RenameLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            textBox1 = new System.Windows.Forms.TextBox();
            FindTextBox = new System.Windows.Forms.TextBox();
            panel1 = new System.Windows.Forms.Panel();
            ResultLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            OriginalLabel = new System.Windows.Forms.Label();
            NewTextTextBox = new System.Windows.Forms.TextBox();
            SaveButton = new System.Windows.Forms.Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BlackFramePixelThresholdInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReferenceFrameOffsetInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BlackFrameDurationInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BlackFrameThresholdInput).BeginInit();
            FFMPEGArgumentsGroupBox.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MultiplierInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PaddingInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartIndexInput).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(13, 15);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(456, 476);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(BlackFramePixelThresholdInput);
            tabPage1.Controls.Add(bplabel1);
            tabPage1.Controls.Add(ShowLogCheckbox);
            tabPage1.Controls.Add(ReferenceFrameOffsetLabel);
            tabPage1.Controls.Add(ReferenceFrameOffsetInput);
            tabPage1.Controls.Add(DeleteOriginalCheckbox);
            tabPage1.Controls.Add(ProcessTypeLabel);
            tabPage1.Controls.Add(ProcessTypeComboBox);
            tabPage1.Controls.Add(BlackFrameDurationInput);
            tabPage1.Controls.Add(BlackFrameThresholdInput);
            tabPage1.Controls.Add(BlackFrameThresholdLabel);
            tabPage1.Controls.Add(BlackFrameDurationLabel);
            tabPage1.Controls.Add(FFMPEGArgumentsGroupBox);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(448, 448);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // BlackFramePixelThresholdInput
            // 
            BlackFramePixelThresholdInput.DecimalPlaces = 2;
            BlackFramePixelThresholdInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            BlackFramePixelThresholdInput.Location = new System.Drawing.Point(6, 58);
            BlackFramePixelThresholdInput.Name = "BlackFramePixelThresholdInput";
            BlackFramePixelThresholdInput.Size = new System.Drawing.Size(130, 23);
            BlackFramePixelThresholdInput.TabIndex = 12;
            BlackFramePixelThresholdInput.Tag = "BlackFramePixelThreshold";
            BlackFramePixelThresholdInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bplabel1
            // 
            bplabel1.AutoSize = true;
            bplabel1.Location = new System.Drawing.Point(145, 60);
            bplabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            bplabel1.Name = "bplabel1";
            bplabel1.Size = new System.Drawing.Size(118, 15);
            bplabel1.TabIndex = 11;
            bplabel1.Text = "Black Pixel Threshold";
            // 
            // ShowLogCheckbox
            // 
            ShowLogCheckbox.AutoSize = true;
            ShowLogCheckbox.Location = new System.Drawing.Point(6, 134);
            ShowLogCheckbox.Margin = new System.Windows.Forms.Padding(2);
            ShowLogCheckbox.Name = "ShowLogCheckbox";
            ShowLogCheckbox.Size = new System.Drawing.Size(78, 19);
            ShowLogCheckbox.TabIndex = 7;
            ShowLogCheckbox.Text = "Show Log";
            ShowLogCheckbox.UseVisualStyleBackColor = true;
            // 
            // ReferenceFrameOffsetLabel
            // 
            ReferenceFrameOffsetLabel.AutoSize = true;
            ReferenceFrameOffsetLabel.Location = new System.Drawing.Point(145, 85);
            ReferenceFrameOffsetLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ReferenceFrameOffsetLabel.Name = "ReferenceFrameOffsetLabel";
            ReferenceFrameOffsetLabel.Size = new System.Drawing.Size(146, 15);
            ReferenceFrameOffsetLabel.TabIndex = 3;
            ReferenceFrameOffsetLabel.Text = "Reference Frame Offset (s)";
            // 
            // ReferenceFrameOffsetInput
            // 
            ReferenceFrameOffsetInput.DecimalPlaces = 1;
            ReferenceFrameOffsetInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            ReferenceFrameOffsetInput.Location = new System.Drawing.Point(6, 84);
            ReferenceFrameOffsetInput.Name = "ReferenceFrameOffsetInput";
            ReferenceFrameOffsetInput.Size = new System.Drawing.Size(130, 23);
            ReferenceFrameOffsetInput.TabIndex = 5;
            ReferenceFrameOffsetInput.Tag = "Settings.BlackFrameDuration";
            ReferenceFrameOffsetInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DeleteOriginalCheckbox
            // 
            DeleteOriginalCheckbox.AutoSize = true;
            DeleteOriginalCheckbox.Location = new System.Drawing.Point(6, 156);
            DeleteOriginalCheckbox.Name = "DeleteOriginalCheckbox";
            DeleteOriginalCheckbox.Size = new System.Drawing.Size(155, 19);
            DeleteOriginalCheckbox.TabIndex = 8;
            DeleteOriginalCheckbox.Text = "Delete/Replace Originals";
            DeleteOriginalCheckbox.UseVisualStyleBackColor = true;
            // 
            // ProcessTypeLabel
            // 
            ProcessTypeLabel.AutoSize = true;
            ProcessTypeLabel.Location = new System.Drawing.Point(145, 111);
            ProcessTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ProcessTypeLabel.Name = "ProcessTypeLabel";
            ProcessTypeLabel.Size = new System.Drawing.Size(109, 15);
            ProcessTypeLabel.TabIndex = 3;
            ProcessTypeLabel.Text = "Processing Method";
            // 
            // ProcessTypeComboBox
            // 
            ProcessTypeComboBox.FormattingEnabled = true;
            ProcessTypeComboBox.Location = new System.Drawing.Point(6, 110);
            ProcessTypeComboBox.Name = "ProcessTypeComboBox";
            ProcessTypeComboBox.Size = new System.Drawing.Size(132, 23);
            ProcessTypeComboBox.TabIndex = 6;
            ProcessTypeComboBox.Tag = "Settings.ProcessType";
            // 
            // BlackFrameDurationInput
            // 
            BlackFrameDurationInput.DecimalPlaces = 2;
            BlackFrameDurationInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            BlackFrameDurationInput.Location = new System.Drawing.Point(6, 8);
            BlackFrameDurationInput.Name = "BlackFrameDurationInput";
            BlackFrameDurationInput.Size = new System.Drawing.Size(130, 23);
            BlackFrameDurationInput.TabIndex = 5;
            BlackFrameDurationInput.Tag = "Settings.BlackFrameDuration";
            BlackFrameDurationInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BlackFrameThresholdInput
            // 
            BlackFrameThresholdInput.DecimalPlaces = 2;
            BlackFrameThresholdInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            BlackFrameThresholdInput.Location = new System.Drawing.Point(6, 33);
            BlackFrameThresholdInput.Name = "BlackFrameThresholdInput";
            BlackFrameThresholdInput.Size = new System.Drawing.Size(130, 23);
            BlackFrameThresholdInput.TabIndex = 4;
            BlackFrameThresholdInput.Tag = "BlackFrameThreshold";
            BlackFrameThresholdInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BlackFrameThresholdLabel
            // 
            BlackFrameThresholdLabel.AutoSize = true;
            BlackFrameThresholdLabel.Location = new System.Drawing.Point(145, 34);
            BlackFrameThresholdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            BlackFrameThresholdLabel.Name = "BlackFrameThresholdLabel";
            BlackFrameThresholdLabel.Size = new System.Drawing.Size(126, 15);
            BlackFrameThresholdLabel.TabIndex = 3;
            BlackFrameThresholdLabel.Text = "Black Frame Threshold";
            // 
            // BlackFrameDurationLabel
            // 
            BlackFrameDurationLabel.AutoSize = true;
            BlackFrameDurationLabel.Location = new System.Drawing.Point(145, 8);
            BlackFrameDurationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            BlackFrameDurationLabel.Name = "BlackFrameDurationLabel";
            BlackFrameDurationLabel.Size = new System.Drawing.Size(136, 15);
            BlackFrameDurationLabel.TabIndex = 1;
            BlackFrameDurationLabel.Text = "Black Frame Duration (s)";
            // 
            // FFMPEGArgumentsGroupBox
            // 
            FFMPEGArgumentsGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            FFMPEGArgumentsGroupBox.Controls.Add(FFMPEGArgumentsInput);
            FFMPEGArgumentsGroupBox.Controls.Add(FFMPEGArgumentsLegend);
            FFMPEGArgumentsGroupBox.Location = new System.Drawing.Point(6, 180);
            FFMPEGArgumentsGroupBox.Name = "FFMPEGArgumentsGroupBox";
            FFMPEGArgumentsGroupBox.Size = new System.Drawing.Size(431, 263);
            FFMPEGArgumentsGroupBox.TabIndex = 10;
            FFMPEGArgumentsGroupBox.TabStop = false;
            FFMPEGArgumentsGroupBox.Text = "FFMPEG Arguments";
            // 
            // FFMPEGArgumentsInput
            // 
            FFMPEGArgumentsInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            FFMPEGArgumentsInput.Location = new System.Drawing.Point(6, 22);
            FFMPEGArgumentsInput.Multiline = true;
            FFMPEGArgumentsInput.Name = "FFMPEGArgumentsInput";
            FFMPEGArgumentsInput.Size = new System.Drawing.Size(334, 237);
            FFMPEGArgumentsInput.TabIndex = 9;
            // 
            // FFMPEGArgumentsLegend
            // 
            FFMPEGArgumentsLegend.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            FFMPEGArgumentsLegend.AutoSize = true;
            FFMPEGArgumentsLegend.Location = new System.Drawing.Point(344, 24);
            FFMPEGArgumentsLegend.Name = "FFMPEGArgumentsLegend";
            FFMPEGArgumentsLegend.Size = new System.Drawing.Size(74, 90);
            FFMPEGArgumentsLegend.TabIndex = 11;
            FFMPEGArgumentsLegend.Text = "Legend:\r\n{source}\r\n{destination}\r\n{start}\r\n{end}\r\n{duration}";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(MultiplierInput);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(PaddingInput);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(StartIndexInput);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(RenameOriginalCheckBox);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(RenameTypeComboBox);
            tabPage2.Controls.Add(RenameLabel);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(panel2);
            tabPage2.Controls.Add(FindTextBox);
            tabPage2.Controls.Add(panel1);
            tabPage2.Controls.Add(NewTextTextBox);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(2);
            tabPage2.Size = new System.Drawing.Size(448, 448);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Rename";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // MultiplierInput
            // 
            MultiplierInput.Location = new System.Drawing.Point(142, 124);
            MultiplierInput.Margin = new System.Windows.Forms.Padding(2);
            MultiplierInput.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            MultiplierInput.Name = "MultiplierInput";
            MultiplierInput.Size = new System.Drawing.Size(66, 23);
            MultiplierInput.TabIndex = 25;
            MultiplierInput.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(9, 126);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(115, 15);
            label8.TabIndex = 24;
            label8.Text = "Increment Multiplier";
            // 
            // PaddingInput
            // 
            PaddingInput.Location = new System.Drawing.Point(143, 183);
            PaddingInput.Margin = new System.Windows.Forms.Padding(2);
            PaddingInput.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            PaddingInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PaddingInput.Name = "PaddingInput";
            PaddingInput.Size = new System.Drawing.Size(66, 23);
            PaddingInput.TabIndex = 23;
            PaddingInput.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(10, 184);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(51, 15);
            label7.TabIndex = 22;
            label7.Text = "Padding";
            // 
            // StartIndexInput
            // 
            StartIndexInput.Location = new System.Drawing.Point(143, 154);
            StartIndexInput.Margin = new System.Windows.Forms.Padding(2);
            StartIndexInput.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            StartIndexInput.Name = "StartIndexInput";
            StartIndexInput.Size = new System.Drawing.Size(66, 23);
            StartIndexInput.TabIndex = 21;
            StartIndexInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(10, 154);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(63, 15);
            label6.TabIndex = 20;
            label6.Text = "Start Index";
            // 
            // RenameOriginalCheckBox
            // 
            RenameOriginalCheckBox.Location = new System.Drawing.Point(10, 102);
            RenameOriginalCheckBox.Margin = new System.Windows.Forms.Padding(2);
            RenameOriginalCheckBox.Name = "RenameOriginalCheckBox";
            RenameOriginalCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RenameOriginalCheckBox.Size = new System.Drawing.Size(147, 18);
            RenameOriginalCheckBox.TabIndex = 19;
            RenameOriginalCheckBox.Text = "Rename Original";
            RenameOriginalCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            RenameOriginalCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(9, 77);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 15);
            label1.TabIndex = 17;
            label1.Text = "Type";
            // 
            // RenameTypeComboBox
            // 
            RenameTypeComboBox.FormattingEnabled = true;
            RenameTypeComboBox.Location = new System.Drawing.Point(143, 76);
            RenameTypeComboBox.Margin = new System.Windows.Forms.Padding(2);
            RenameTypeComboBox.Name = "RenameTypeComboBox";
            RenameTypeComboBox.Size = new System.Drawing.Size(91, 23);
            RenameTypeComboBox.TabIndex = 16;
            // 
            // RenameLabel
            // 
            RenameLabel.AutoSize = true;
            RenameLabel.Location = new System.Drawing.Point(9, 45);
            RenameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            RenameLabel.Name = "RenameLabel";
            RenameLabel.Size = new System.Drawing.Size(48, 15);
            RenameLabel.TabIndex = 15;
            RenameLabel.Text = "Replace";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(9, 12);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(30, 15);
            label4.TabIndex = 14;
            label4.Text = "Find";
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.Control;
            panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel2.Controls.Add(textBox1);
            panel2.Location = new System.Drawing.Point(9, 214);
            panel2.Margin = new System.Windows.Forms.Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(426, 142);
            panel2.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Location = new System.Drawing.Point(0, 0);
            textBox1.Margin = new System.Windows.Forms.Padding(2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox1.Size = new System.Drawing.Size(424, 140);
            textBox1.TabIndex = 26;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FindTextBox
            // 
            FindTextBox.Location = new System.Drawing.Point(143, 10);
            FindTextBox.Margin = new System.Windows.Forms.Padding(2);
            FindTextBox.Name = "FindTextBox";
            FindTextBox.Size = new System.Drawing.Size(292, 23);
            FindTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(ResultLabel);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(OriginalLabel);
            panel1.Location = new System.Drawing.Point(9, 376);
            panel1.Margin = new System.Windows.Forms.Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(426, 70);
            panel1.TabIndex = 11;
            // 
            // ResultLabel
            // 
            ResultLabel.AutoSize = true;
            ResultLabel.Location = new System.Drawing.Point(84, 44);
            ResultLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ResultLabel.Name = "ResultLabel";
            ResultLabel.Size = new System.Drawing.Size(126, 15);
            ResultLabel.TabIndex = 3;
            ResultLabel.Text = "ExampleFilename.mkv";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 44);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(36, 15);
            label3.TabIndex = 2;
            label3.Text = "After:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 10);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 15);
            label2.TabIndex = 1;
            label2.Text = "Before:";
            // 
            // OriginalLabel
            // 
            OriginalLabel.AutoSize = true;
            OriginalLabel.Location = new System.Drawing.Point(84, 10);
            OriginalLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            OriginalLabel.Name = "OriginalLabel";
            OriginalLabel.Size = new System.Drawing.Size(127, 15);
            OriginalLabel.TabIndex = 0;
            OriginalLabel.Text = "ExampleFilename.mp4";
            // 
            // NewTextTextBox
            // 
            NewTextTextBox.Location = new System.Drawing.Point(143, 44);
            NewTextTextBox.Margin = new System.Windows.Forms.Padding(2);
            NewTextTextBox.Name = "NewTextTextBox";
            NewTextTextBox.Size = new System.Drawing.Size(292, 23);
            NewTextTextBox.TabIndex = 2;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            SaveButton.Location = new System.Drawing.Point(395, 490);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(70, 24);
            SaveButton.TabIndex = 10;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AcceptButton = SaveButton;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(472, 522);
            Controls.Add(SaveButton);
            Controls.Add(tabControl1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            ShowIcon = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Text = "Settings";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BlackFramePixelThresholdInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReferenceFrameOffsetInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)BlackFrameDurationInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)BlackFrameThresholdInput).EndInit();
            FFMPEGArgumentsGroupBox.ResumeLayout(false);
            FFMPEGArgumentsGroupBox.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MultiplierInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)PaddingInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartIndexInput).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label BlackFrameThresholdLabel;
        private System.Windows.Forms.Label BlackFrameDurationLabel;
        private System.Windows.Forms.NumericUpDown BlackFrameDurationInput;
        private System.Windows.Forms.NumericUpDown BlackFrameThresholdInput;
        private System.Windows.Forms.Label ProcessTypeLabel;
        private System.Windows.Forms.ComboBox ProcessTypeComboBox;
        private System.Windows.Forms.Label FFMPEGArgumentsLegend;
        private System.Windows.Forms.TextBox FFMPEGArgumentsInput;
        private System.Windows.Forms.Label ReferenceFrameOffsetLabel;
        private System.Windows.Forms.NumericUpDown ReferenceFrameOffsetInput;
        private System.Windows.Forms.CheckBox DeleteOriginalCheckbox;
        private System.Windows.Forms.GroupBox FFMPEGArgumentsGroupBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox ShowLogCheckbox;
        private System.Windows.Forms.NumericUpDown BlackFramePixelThresholdInput;
        private System.Windows.Forms.Label bplabel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox FindTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label OriginalLabel;
        private System.Windows.Forms.TextBox NewTextTextBox;
        private System.Windows.Forms.Label RenameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox RenameTypeComboBox;
        private System.Windows.Forms.CheckBox RenameOriginalCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown StartIndexInput;
        private System.Windows.Forms.NumericUpDown PaddingInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown MultiplierInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
    }
}