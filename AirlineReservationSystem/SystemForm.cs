/*
 * Project: Assignment1 - Airline Reservation System
 * Revision History:
 * 	Jay Jang, 2017.9.18: Created
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJangAssignment1
{
    public partial class SystemForm : Form
    {
        const int NUMBER_OF_ROWS = 5;
        const int NUMBER_OF_SEATS = 3;
        const int NUMBER_OF_WAITINGLIST = 10;
        int seatRow;
        int seatCol;
        string[,] seatsArray = new string[NUMBER_OF_ROWS, NUMBER_OF_SEATS];
        string[] waitingArray = new string[NUMBER_OF_WAITINGLIST];
        public SystemForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Book a seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text;
            try
            {
                if (userName == null || userName.Length < 1)
                {
                    throw new Exception("Please enter your name at least" +
                        " 1 letter.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Name: " + ex.Message);
                return;
            }

            if (lbRow.SelectedItem == null)
            {
                MessageBox.Show("Please select a row of seat.");
                return;
            }
            else
            {
                seatRow = int.Parse(lbRow.SelectedItem.ToString());
            }

            if (lbCol.SelectedItem == null)
            {
                MessageBox.Show("Please select a col of seat.");
                return;
            }
            else
            {
                seatCol = int.Parse(lbCol.SelectedItem.ToString());
            }

            //Seat full check
            bool seatFull = true;
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_SEATS; j++)
                {
                    string checkSeat = seatsArray[i, j];
                    if (checkSeat == null || checkSeat == "")
                    {
                        seatFull = false;
                        break;
                    }
                }
            }

            if (seatFull == true)
            {
                bool waitingFull = true;
                for (int i = 0; i < NUMBER_OF_WAITINGLIST; i++)
                {
                    string checkWaiting = waitingArray[i];
                    if (checkWaiting == null || checkWaiting == "")
                    {
                        waitingFull = false;
                        break;
                    }
                }
                if (waitingFull == true)
                {
                    MessageBox.Show("Waiting List is Full");
                    return;
                }
                else
                {
                    for (int i = 0; i < NUMBER_OF_WAITINGLIST; i++)
                    {

                        if (waitingArray[i] == null || waitingArray[i] == "")
                        {
                            waitingArray[i] = userName;
                            break;
                        }
                    }
                    MessageBox.Show("Successfully added to waiting list");
                    return;
                }
            }
            else
            {
                if (seatsArray[seatRow, seatCol] == null ||
                    seatsArray[seatRow, seatCol] == "")
                {
                    seatsArray[seatRow, seatCol] = userName;
                    MessageBox.Show("Seat[" + seatRow + ", " +
                        seatCol + "] is now booked");
                }
                else
                {
                    MessageBox.Show("Seat is occupied, choose another");
                    return;
                }
            }

        }

        /// <summary>
        /// Cancel the seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lbRow.SelectedItem == null)
            {
                MessageBox.Show("Please select a row of seat.");
                return;
            }
            else
            {
                seatRow = int.Parse(lbRow.SelectedItem.ToString());
            }

            if (lbCol.SelectedItem == null)
            {
                MessageBox.Show("Please select a col of seat.");
                return;
            }
            else
            {
                seatCol = int.Parse(lbCol.SelectedItem.ToString());
            }
            //Cancel a seat
            if (seatsArray[seatRow, seatCol] == "" ||
                seatsArray[seatRow, seatCol] == null)
            {
                MessageBox.Show("Cannot Cancel an empty seat");
                return;

            }
            else
            {
                seatsArray[seatRow, seatCol] = "";
                MessageBox.Show("Seat successfully cancelled");

                if (waitingArray[0] == "" || waitingArray[0] == null)
                {
                    return;
                }
                else
                {
                    seatsArray[seatRow, seatCol] = waitingArray[0];
                    for (int i = 0; i < NUMBER_OF_WAITINGLIST - 1; i++)
                    {
                        waitingArray[i] = waitingArray[i + 1];
                    }
                    waitingArray[NUMBER_OF_WAITINGLIST - 1] = "";
                    MessageBox.Show("Moved the 1st person from waiting" +
                        " list to cancelled seat");
                    return;
                }
            }
        }

        /// <summary>
        /// Add to Waiting List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToWatingList_Click(object sender, EventArgs e)
        {
            //First check seat available
            bool seatFull = true;
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_SEATS; j++)
                {
                    string checkSeat = seatsArray[i, j];
                    if (checkSeat == null || checkSeat == "")
                    {
                        seatFull = false;
                        break;
                    }
                }
            }

            if (seatFull == true)
            {
                bool waitingFull = true;
                for (int i = 0; i < NUMBER_OF_WAITINGLIST; i++)
                {
                    string checkWaiting = waitingArray[i];
                    if (checkWaiting == null || checkWaiting == "")
                    {
                        waitingFull = false;
                        break;
                    }
                }
                if (waitingFull == true)
                {
                    MessageBox.Show("Waiting List is Full");
                    return;
                }
                else
                {
                    for (int i = 0; i < NUMBER_OF_WAITINGLIST; i++)
                    {

                        if (waitingArray[i] == null || waitingArray[i] == "")
                        {
                            waitingArray[i] = txtName.Text;
                            break;
                        }
                    }
                    MessageBox.Show("Successfully added to waiting list");
                    return;
                }
            }
            else
            {
                MessageBox.Show("There are seats available, use BOOK to book");
                return;
            }

        }

        /// <summary>
        /// Check the seat status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (lbRow.SelectedItem == null)
            {
                MessageBox.Show("Please select a row of seat.");
                return;
            }
            else
            {
                seatRow = int.Parse(lbRow.SelectedItem.ToString());
            }

            if (lbCol.SelectedItem == null)
            {
                MessageBox.Show("Please select a col of seat.");
                return;
            }
            else
            {
                seatCol = int.Parse(lbCol.SelectedItem.ToString());
            }

            if (seatsArray[seatRow, seatCol] == null ||
                seatsArray[seatRow, seatCol] == "")
            {
                txtStatus.Text = "Free";
            }
            else
            {
                txtStatus.Text = "Occupied";
            }
        }

        /// <summary>
        /// Fill all seats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFillAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_SEATS; j++)
                {
                    seatsArray[i, j] = "Jay";
                }
            }
        }

        /// <summary>
        /// Show Seat List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < NUMBER_OF_SEATS; j++)
                {
                    result += "  [" + i + ", " + j + "] " + "-- " +
                        seatsArray[i, j] + "\n";
                }
            }
            rtbSeats.Text = result;
        }

        /// <summary>
        /// Show Waiting List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < NUMBER_OF_WAITINGLIST; i++)
            {
                result += "  [" + i + "] " + "-- " + waitingArray[i] + "\n";

            }
            rtbWatingList.Text = result;
        }
    }
}
