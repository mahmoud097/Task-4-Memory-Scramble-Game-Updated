namespace Task_4_Memory_Scramble_Game
{
    public partial class Form1 : Form
    {
        //initiate List numbers
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        string firstChoice;
        string secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        int totalTime = 60;
        int countDownTime;
        bool gameOver = false;

        public Form1()          // call method load pics
        {
            InitializeComponent();
            LoadPictures();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            lblTimtLeft.Text = "Time Left: " + countDownTime;
            if (countDownTime < 1)
            {
                GameOver("Time Up , You Lose!!!");

                foreach (PictureBox x in pictures)
                {
                    if (x.Tag != null)
                    {
                        x.Image = Image.FromFile("pics/" + (string)x.Tag + ".png");
                    }
                }
            }
        }
             // Event handler to restart the game
             
        private void RestartGameEvent(object sender, EventArgs e)
        {
            RestartGame();
        }

        // Method to load the pictures into the PictureBox controls
        
        private void LoadPictures()
        {
           int leftpos = 20;
           int toppos = 20;
           int rows = 0;
           
            // Create and configure PictureBox controls
            
           for (int i = 0; i < 12; i++)
           {
                PictureBox newPic = new PictureBox();
                newPic.Height = 50;
                newPic.Width = 50;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);

                 if (rows < 3)
                {
                    rows++;
                    newPic.Left = leftpos;
                    newPic.Top = toppos;
                    this.Controls.Add(newPic);
                    leftpos = leftpos + 60;
                }
                if (rows == 3)
                {
                    leftpos = 20;
                    toppos += 60;
                    rows = 0;
                }
            }
                 // Start a new game
            RestartGame();
  
        }
              // Event handler for picture box click
              
        private void NewPic_Click(object? sender, EventArgs e)
        {
            if (gameOver)
            {
                // dont register a click if the game is over
                
                return;
            }
                // Handle the first choice
                
            if (firstChoice == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Image == null)
                {
                    picA.Image = Image.FromFile("pics/" + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                }
            }

            // Handle the second choice
            
            else if (secondChoice == null)
            {
                picB = sender as PictureBox;

                if (picB.Tag != null && picB.Image == null)
                {
                    picB.Image = Image.FromFile("pics/" + (string)picB.Tag + ".png");
                    secondChoice = (string)picB.Tag;
                }
            }
            else
            { 
            
            // Check the pictures if both choices have been made
            
                CheckPictures(picA, picB);
            }
        }

              
        private void RestartGame()    // Method to restart the game
        {
             // Randomize the list of numbers

             
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            // assign the random list to the original
            numbers = randomList;
            for (int i = 0; i < pictures.Count; i++)
            {

            // Assign the random numbers to the PictureBox controls

            
                pictures[i].Image = null;
                pictures[i].Tag = numbers[i].ToString();

                
            }    // Reset the game variables
            
            tries = 0;
            lblStatus.Text = "Mismatched: " + tries + " times.";
            lblTimtLeft.Text = "Time Left: " + totalTime;
            gameOver = false;
            GameTimer.Start();
            countDownTime = totalTime;
            

        }
          // Method to check the two chosen pictures
        private void CheckPictures(PictureBox A, PictureBox B)
        {
           
           if (firstChoice == secondChoice)   // If the pictures match, set their tags to null
            {
                A.Tag = null;
                B.Tag = null;
            }
            else
            {
                tries++;
                lblStatus.Text = "Mismatched " + tries + " times.";  // If they do not match, increment the mismatch counter
            }
             
            firstChoice = null;       // Reset the choices
            secondChoice = null;
            
            foreach (PictureBox pics in pictures.ToList())    // Hide the pictures that do not match
            {
                if (pics.Tag != null)
                {
                    pics.Image = null;
                }
            }
            // now lets check if all of the items have been solved
            if (pictures.All(o => o.Tag == pictures[0].Tag))    // Check if all pairs have been matched
            {
                GameOver("Great Work, You Win!!!!");
            }
            
          
        }
        private void GameOver(string msg) // Method to handle the game over scenario
        {
            GameTimer.Stop();
            gameOver = true;
            MessageBox.Show(msg + " Click Restart to Play Again.", "Moo Says: ");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblTimtLeft_Click(object sender, EventArgs e)
        {

        }
    }
}


