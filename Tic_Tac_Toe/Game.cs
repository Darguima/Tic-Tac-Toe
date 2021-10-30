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
	public partial class Game : Form
	{

		Boolean isCrossFirstToPlay = true;
		Boolean isCrossPlaying = true;
		Boolean hasGameAlreadyStarted = false;

		int crossScore = 0;
		int circleScore = 0;

		// -1 = circle; 0 - void; 1 = cross
		int[][] game = new int[3][] {
			new int[3] {0, 0, 0},
			new int[3] {0, 0, 0},
			new int[3] {0, 0, 0}
		};

		private void RestartGame()
        {
			image_r0_c0.Image = null;
			image_r0_c1.Image = null;
			image_r0_c2.Image = null;
			image_r1_c0.Image = null;
			image_r1_c1.Image = null;
			image_r1_c2.Image = null;
			image_r2_c0.Image = null;
			image_r2_c1.Image = null;
			image_r2_c2.Image = null;

			game = new int[3][] {
				new int[3] {0, 0, 0},
				new int[3] {0, 0, 0},
				new int[3] {0, 0, 0}
			};

			hasGameAlreadyStarted = false;
			isCrossPlaying = isCrossFirstToPlay ? true : false;
		}

		private bool HasSomeoneWinTheGame () {
			return
				game[0][0] == game[0][1] && game[0][1] == game[0][2] && game[0][0] != 0 ||
				game[1][0] == game[1][1] && game[1][1] == game[1][2] && game[1][0] != 0 ||
				game[2][0] == game[2][1] && game[2][1] == game[2][2] && game[2][0] != 0 ||

				game[0][0] == game[1][0] && game[1][0] == game[2][0] && game[0][0] != 0 ||
				game[0][1] == game[1][1] && game[1][1] == game[2][1] && game[0][1] != 0 ||
				game[0][2] == game[1][2] && game[1][2] == game[2][2] && game[0][2] != 0 ||

				game[0][0] == game[1][1] && game[1][1] == game[2][2] && game[1][1] != 0 ||
				game[0][2] == game[1][1] && game[1][1] == game[2][0] && game[1][1] != 0;
		}

		private bool IsTheGameTied () {
			return
				!game[0].Contains(0) &&
				!game[1].Contains(0) &&
				!game[2].Contains(0)
				;
		}

		private async void CongratulateTheWinner (int winner) // -1 = circle; 0 - void; 1 = cross
		{

			image_r0_c0.Visible = false;
			image_r0_c1.Visible = false;
			image_r0_c2.Visible = false;
			image_r1_c0.Visible = false;
			image_r1_c1.Visible = false;
			image_r1_c2.Visible = false;
			image_r2_c0.Visible = false;
			image_r2_c1.Visible = false;
			image_r2_c2.Visible = false;

			if (winner == 0)
            {
				lb_CongratulateTheWinner.Text = "Tied Game";
				picture_CongratulateTheWinner.Image = Properties.Resources.gametTied;
			}

			else
            {
				lb_CongratulateTheWinner.Text = "Winner";
				picture_CongratulateTheWinner.Image = winner == -1
					? Properties.Resources.circle_image
					: Properties.Resources.cross_image;
			}

			lb_CongratulateTheWinner.Visible = true;
			picture_CongratulateTheWinner.Visible = true;
			await Task.Delay(1500);

			lb_CongratulateTheWinner.Visible = false;
			picture_CongratulateTheWinner.Visible = false;
			picture_CongratulateTheWinner.Image = null;

			image_r0_c0.Visible = true;
			image_r0_c1.Visible = true;
			image_r0_c2.Visible = true;
			image_r1_c0.Visible = true;
			image_r1_c1.Visible = true;
			image_r1_c2.Visible = true;
			image_r2_c0.Visible = true;
			image_r2_c1.Visible = true;
			image_r2_c2.Visible = true;
		}

		public Game()
		{
			InitializeComponent();
		}

		private void image_startPlayerCross_Click(object sender, EventArgs e)
		{
			isCrossFirstToPlay = true;
			if (!hasGameAlreadyStarted) isCrossPlaying = true;

			image_startPlayerCross.BackColor = SystemColors.ControlDarkDark;
			image_startPlayerCross.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			image_startPlayerCircle.BackColor = SystemColors.ControlDark;
			image_startPlayerCircle.BorderStyle = System.Windows.Forms.BorderStyle.None;
		}

		private void image_startPlayerCircle_Click(object sender, EventArgs e)
		{
			isCrossFirstToPlay = false;
			if (!hasGameAlreadyStarted) isCrossPlaying = false;

			image_startPlayerCircle.BackColor = SystemColors.ControlDarkDark;
			image_startPlayerCircle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			image_startPlayerCross.BackColor = SystemColors.ControlDark;
			image_startPlayerCross.BorderStyle = System.Windows.Forms.BorderStyle.None;
		}

		private void bt_resetScore_Click(object sender, EventArgs e)
		{
			crossScore = 0;
			circleScore = 0;
			lb_scoreCross.Text = crossScore.ToString();
			lb_scoreCircle.Text = circleScore.ToString();
		}

		private void bt_restartGame_Click(object sender, EventArgs e)
		{
			RestartGame();
		}

		private void Image_Click(object sender, EventArgs e)
		{
			hasGameAlreadyStarted = true;

			PictureBox pictureBox = (PictureBox)sender;

			if (pictureBox.Image != null) return;

			pictureBox.Image = isCrossPlaying
				? Properties.Resources.cross_image
				: Properties.Resources.circle_image;

			
			// name exemple: image_r1_c1
			int row = Int32.Parse(pictureBox.Name[7].ToString()); 
			int columnn = Int32.Parse(pictureBox.Name[10].ToString());

			game[row][columnn] = isCrossPlaying ? 1 : -1;

			// -1 = circle; 0 - nobody; 1 = cross
			int winner = 0;
			if (HasSomeoneWinTheGame())
            {
				winner = isCrossPlaying ? 1 : -1;
			} 

			if (winner == 0) // nobody win, continue the game
            {
				isCrossPlaying = !isCrossPlaying;

				if (IsTheGameTied())
                {
					RestartGame();
					CongratulateTheWinner(0);
				}

				return;
			}

			else if (winner == -1) circleScore++;

			else if (winner == 1) crossScore++;

			CongratulateTheWinner(winner);

			lb_scoreCross.Text = crossScore.ToString();
			lb_scoreCircle.Text = circleScore.ToString();
			RestartGame();

		}

        private void linkLb_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			try
            {
				linkLb_github.LinkVisited = true;
				System.Diagnostics.Process.Start("https://github.com/Darguima/Mini-Projects");
			} catch (Exception)
			{
				MessageBox.Show("Access \"https://github.com/Darguima/Mini-Projects\"");
			}
        }
    }
}
