using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        int timePlayed = 0;

        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "a", "a", "B", "B", "c", "c", "d", "d",
            "A", "A", "C", "C", "D", "D", "e", "e",
            "E", "E", "f","f"
        };

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
            timer.Text = timePlayed + " seconds";
            timer2.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.SaddleBrown)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.SaddleBrown;
                    timerTooSlow.Start();
                    return;
                }

                timerTooSlow.Stop();
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.SaddleBrown;

                if (!CheckForWinner())
                {
                    if (firstClicked.Text == secondClicked.Text)
                    {
                        SystemSounds.Exclamation.Play();
                        firstClicked = null;
                        secondClicked = null;
                        return;
                    }
                    SystemSounds.Asterisk.Play();
                    timer1.Start();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            SystemSounds.Beep.Play();
            firstClicked = null;
            secondClicked = null;
        }

        private bool CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return false;
                }
            }
            return true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (CheckForWinner())
            {
                timer2.Stop();
                SystemSounds.Exclamation.Play();
                MessageBox.Show("You matched all the icons!", "Congratulations");
                Close();
            }
            else
            {
                timePlayed += 1;
                timer.Text = timePlayed + " seconds";
            }
        }

        private void timerTooSlow_Tick(object sender, EventArgs e)
        {
            timerTooSlow.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            firstClicked = null;
        }
    }
}