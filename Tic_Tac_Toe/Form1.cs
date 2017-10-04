using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (player1 != null && player1 != String.Empty)
                label1.Text = player1;
            if (player2 != null && player2 != String.Empty)
                label3.Text = player2;

            // setPlayerDefaultsToolStripMenuItem.PerformClick();
        }

        #region MenuStrip
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;

            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }
                catch { }
            }
        }

        private void setPlayerDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // label1.Text = "Alexander";
            label3.Text = "Computer";
        }

        private void resetWinCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x_win_count.Text = "0";
            draw_count.Text = "0";
            o_win_count.Text = "0";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"Tic Tac Toe\" game (AI isn't implemented).\nBy Alexander Usov", "Tic Tac Toe About");
        }
        #endregion

        #region Previewing
        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            }
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
                b.Text = "";
        }
        #endregion

        bool turn = true; // true = X turn, false = O turn
        bool against_computer = false;
        byte turn_count = 0;
        static string player1, player2;

        public static void setPlayerNames(string n1, string n2)
        {
            player1 = n1;
            player2 = n2;
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((label1.Text == "Player 1") || (label3.Text == "Player 2"))
            {
                MessageBox.Show("You must specify the players' names before you can start!\nType Computer (for Player 2) to play against the computer.");
            }
            else
            {
                Button b = (Button)sender;
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";

                turn = !turn;
                b.Enabled = false;
                turn_count++;

                checkForWinner();
            }

            // check to see if playing against computer and if it's O's turn
            if ((!turn) && (against_computer))
                computer_make_move();
        }

        private void checkForWinner()
        {
            bool there_is_a_winner = false;

            #region IF-ELSE cascade
            // horizontal checks
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            // vertical checks
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                there_is_a_winner = true;

            // diagonal checks
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                there_is_a_winner = true;
            #endregion

            if (there_is_a_winner)
            {
                disableButtons();

                string winner = "";
                if (turn)
                {
                    if (player2 != null && player2 != String.Empty)
                        winner = player2;
                    else
                        winner = "Player 2";
                    o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    if (player1 != null && player1 != String.Empty)
                        winner = player1;
                    else
                        winner = "Player 1";
                    x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                }

                MessageBox.Show(winner + " Wins!", "Yay!");
            }
            else
            {
                if (turn_count == 9)
                {
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                    MessageBox.Show("Draw!", "Bummer!");
                }
            }
        }

        private void computer_make_move()
        {
            // ToDO AI
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }

        private void label3_TextChanged(object sender, EventArgs e)
        {
            if (label3.Text.ToUpper() == "COMPUTER")
                against_computer = true;
            else
                against_computer = false;
        }
    }
}
