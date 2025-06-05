using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BananaSplit;
public class StatusBarManager()
{
    public MainForm MainForm { get; set; }

    public void SetStatusBarProgressBarValue(int value, int maximum)
    {
        MainForm.StatusBar.Invoke(
            new MethodInvoker(
                delegate ()
                {
                    MainForm.StatusBarProgressBar.Minimum = 0;
                    MainForm.StatusBarProgressBar.Maximum = maximum;

                    if (value >= maximum)
                        value = maximum - 1;

                    MainForm.StatusBarProgressBar.Value = value;
                }
            )
        );
    }

    public void ClearStatusBarProgressBarValue()
    {
        MainForm.StatusBar.Invoke(
            new MethodInvoker(
                delegate ()
                {
                    MainForm.StatusBarProgressBar.Minimum = 0;
                    MainForm.StatusBarProgressBar.Maximum = 1;

                    MainForm.StatusBarProgressBar.Value = 0;
                }
            )
        );
    }

    public void SetStatusBarLabelValue(string value)
    {
        MainForm.StatusBar.Invoke(
            new MethodInvoker(
                delegate ()
                {
                    MainForm.StatusBarLabel.Text = value;
                }
            )
        );
    }
}
