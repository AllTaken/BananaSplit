using BananaSplit.Extensions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BananaSplit
{
    public partial class SettingsForm : Form
    {
        public Settings Settings { get; set; }

        private const string SampleOriginalP1Text = "Example";
        private const string SampleOriginalP2Text = "Filename";
        private const string SampleOriginalText = SampleOriginalP1Text + SampleOriginalP2Text;
        private const string SampleExtensionText = ".mkv";
        private const string SampleIncrementText = "S02E0";

        public SettingsForm(Settings settings)
        {
            InitializeComponent();

            PopulateCombos();

            AddEventHandlers();

            Settings = settings;            

            ApplySettings();

            UpdateExample();
        }

        private void ApplySettings()
        {
            BlackFrameDurationInput.Value = (decimal)Settings.BlackFrameDuration;
            BlackFrameThresholdInput.Value = (decimal)Settings.BlackFrameThreshold;
            BlackFramePixelThresholdInput.Value = (decimal)Settings.BlackFramePixelThreshold;
            ReferenceFrameOffsetInput.Value = (decimal)Settings.ReferenceFrameOffset;
            ShowLogCheckbox.Checked = Settings.ShowLog;
            DeleteOriginalCheckbox.Checked = Settings.DeleteOriginal;
            FFMPEGArgumentsInput.Text = Settings.FmpegArguments;
            ProcessTypeComboBox.SelectedItem = Settings.ProcessType.GetDisplayName();
            FindTextBox.Text = Settings.RenameFindText;
            RenameTypeComboBox.SelectedItem = Settings.RenameType.GetDisplayName();
            RenameOriginalCheckBox.Checked = Settings.RenameOriginal;
            MultiplierInput.Value = Settings.IncrementMultiplier;
            StartIndexInput.Value = Settings.StartIndex;
            PaddingInput.Value = Settings.Padding;
            NewTextTextBox.Text = Settings.RenameNewText;
        }

        private void AddEventHandlers()
        {
            RenameTypeComboBox.SelectedIndexChanged += RenameTypeComboBox_SelectedIndexChanged;

            SaveButton.Click += SaveButton_Click;
            FormClosing += SettingsForm_FormClosing;

            FindTextBox.TextChanged += FindTextBox_TextChanged;
            NewTextTextBox.TextChanged += NewTextTextBox_TextChanged;
        }

        private void PopulateCombos()
        {
            ProcessTypeComboBox.Items.Clear();
            ProcessTypeComboBox.Items.AddRange(typeof(ProcessingType).GetDisplayNames().ToArray());

            RenameTypeComboBox.Items.Clear();
            RenameTypeComboBox.Items.AddRange(typeof(RenameType).GetDisplayNames().ToArray());
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            Settings.BlackFrameDuration = (double)BlackFrameDurationInput.Value;
            Settings.BlackFrameThreshold = (double)BlackFrameThresholdInput.Value;
            Settings.BlackFramePixelThreshold = (double)BlackFramePixelThresholdInput.Value;
            Settings.ReferenceFrameOffset = (double)ReferenceFrameOffsetInput.Value;
            Settings.ShowLog = ShowLogCheckbox.Checked;
            Settings.DeleteOriginal = DeleteOriginalCheckbox.Checked;
            Settings.FmpegArguments = FFMPEGArgumentsInput.Text;
            Settings.RenameFindText = FindTextBox.Text;
            Settings.RenameNewText = NewTextTextBox.Text;
            Settings.RenameOriginal = RenameOriginalCheckBox.Checked;
            Settings.IncrementMultiplier = (int)MultiplierInput.Value;
            Settings.StartIndex = (int)StartIndexInput.Value;
            Settings.Padding = (int)PaddingInput.Value;

            foreach (ProcessingType type in (ProcessingType[])Enum.GetValues(typeof(ProcessingType)))
            {
                if (type.GetDisplayName() == (string)ProcessTypeComboBox.SelectedItem)
                {
                    Settings.ProcessType = type;
                }
            }

            foreach (RenameType type in (RenameType[])Enum.GetValues(typeof(RenameType)))
            {
                if (type.GetDisplayName() == (string)RenameTypeComboBox.SelectedItem)
                {
                    Settings.RenameType = type;
                }
            }

            Settings.Save();
            Hide();
        }


        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void ToggleRenameType(bool enableFindText, bool enableNewText, string text)
        {
            FindTextBox.Enabled = enableFindText;
            NewTextTextBox.Enabled = enableNewText;
            RenameLabel.Text = text;
        }

        private void RenameTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = GetRenameType();
            switch (type)
            {
                case RenameType.Prefix:
                    ToggleRenameType(false, true, "Prefix");
                    break;
                case RenameType.Suffix:
                    ToggleRenameType(false, true, "Suffix");
                    break;
                case RenameType.AppendAfter:
                    ToggleRenameType(true, true, "Append After");
                    break;
                case RenameType.Replace:
                    ToggleRenameType(true, true, "Replace");
                    break;
                case RenameType.Increment:
                    ToggleRenameType(false, false, "Increment");
                    break;
            }

            UpdateExample();
        }

        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateExample();
        }

        private void NewTextTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateExample();
        }

        private RenameType GetRenameType()
        {
            return Enum.Parse<RenameType>((RenameTypeComboBox.SelectedItem as string).Replace(" ", ""));
        }

        private void UpdateExample()
        {
            var type = GetRenameType();
            switch (type)
            {
                case RenameType.Prefix:
                    ResultLabel.Text = NewTextTextBox.Text + SampleOriginalText + SampleExtensionText;
                    break;
                case RenameType.Suffix:
                    ResultLabel.Text = SampleOriginalText + NewTextTextBox.Text + SampleExtensionText;
                    break;
                case RenameType.AppendAfter:
                    OriginalLabel.Text = SampleOriginalP1Text + FindTextBox.Text + SampleOriginalP2Text + SampleExtensionText;
                    ResultLabel.Text = SampleOriginalP1Text + FindTextBox.Text + NewTextTextBox.Text + SampleOriginalP2Text + SampleExtensionText;
                    break;
                case RenameType.Replace:
                    OriginalLabel.Text = SampleOriginalP1Text + FindTextBox.Text + SampleOriginalP2Text + SampleExtensionText;
                    ResultLabel.Text = OriginalLabel.Text;
                    if (!string.IsNullOrEmpty(FindTextBox.Text))
                    {
                        ResultLabel.Text = OriginalLabel.Text.Replace(FindTextBox.Text, NewTextTextBox.Text);
                    }
                    break;
                case RenameType.Increment:
                    OriginalLabel.Text = SampleOriginalP1Text + SampleIncrementText + "2" + SampleOriginalP2Text + SampleExtensionText;
                    ResultLabel.Text = SampleOriginalP1Text + SampleIncrementText + "3" + SampleOriginalP2Text + SampleExtensionText;
                    break;
            }

            switch (type)
            {
                case RenameType.Prefix:
                case RenameType.Suffix:
                case RenameType.AppendAfter:
                case RenameType.Replace:
                    if (ResultLabel.Text.Contains("{i}"))
                    {
                        OriginalLabel.Text = OriginalLabel.Text.Replace("{i}", "1");
                        ResultLabel.Text = ResultLabel.Text.Replace("{i}", "1");
                    }
                    else
                    {
                        OriginalLabel.Text = OriginalLabel.Text.Replace(".mkv", "-1.mkv");
                        ResultLabel.Text = ResultLabel.Text.Replace(".mkv", "-2.mkv");
                    }
                    break;
            }
        }
    }
}
