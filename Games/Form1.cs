using System.Net;
namespace Games
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string word = "";
        List<Label> labels = new List<Label>();
        int mount = 0;
        enum BodyPart
        {
            Head,
            Left_Eye,
            Right_Eye,
            Mouth,
            Left_Arm,
            Right_Arm,
            Body,
            Left_Leg,
            Right_Leg,
        }
        void DrawBody(BodyPart BP)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue,2);
            if (BP == BodyPart.Head)
                g.DrawEllipse(p, 40, 50, 40, 40);
            else if (BP == BodyPart.Left_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 50, 60, 5, 5);
            }
            else if (BP == BodyPart.Right_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 60, 60, 5, 5);
            }
            else if (BP == BodyPart.Mouth)
                g.DrawArc(p, 50, 60, 20, 20, 45, 90);
            else if (BP == BodyPart.Body)
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            else if (BP == BodyPart.Left_Arm)
                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            else if (BP == BodyPart.Right_Arm)
                g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            else if (BP == BodyPart.Left_Leg)
                g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            else if (BP == BodyPart.Right_Leg)
                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
        }
        void DrowHangPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
            DrawBody(BodyPart.Head);
            DrawBody(BodyPart.Left_Eye);
            DrawBody(BodyPart.Right_Eye);
            DrawBody(BodyPart.Mouth);
            DrawBody(BodyPart.Body);
            DrawBody(BodyPart.Left_Arm);
            DrawBody(BodyPart.Right_Arm);
            DrawBody(BodyPart.Left_Leg);
            DrawBody(BodyPart.Right_Leg);
            // getRandWord();
            makeLabels();
        }

        void makeLabels()
        {
            word = getRandWord();
            char[] chars = word.ToCharArray();
            int btwn = 445 / chars.Length -1;
            for(int i=0; i<chars.Length -1; i++)
            {
                labels.Add(new Label());
                labels[i].Location=new Point( (i * btwn) +10 , 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }
            label1.Text = "word length : " + (chars.Length - 1).ToString();
        }
        string getRandWord()
        {
            WebClient wc = new WebClient();
            string wordList = wc.DownloadString("https://gist.githubusercontent.com/hugsy/8910dc78d208e40de42deb29e62df913/raw/eec99c5597a73f6a9240cab26965a8609fa0f6ea/english-adjectives.txt");
            string[] words = wordList.Split('\n');
            Random ran = new Random();
            return words[ran.Next(0, words.Length -1)];
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            DrowHangPost();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if(!char.IsLetter(letter))
            {
                MessageBox.Show("You can only send a letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            if(word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                for(int i = 0; i < letters.Length -1; i++)
                {
                    if(letters[i] == letter)
                        labels[i].Text = letter.ToString();
        
                }
                foreach (Label l in labels)
                {
                    if (l.Text == "_") return;
                    MessageBox.Show("You are the winner", "Winner");
                    resetGame();
                }
            }
            else
            {
                MessageBox.Show("The letter you sent isn't in the word", "Sorry");
                label2.Text += " " + letter.ToString() + ",";
                DrawBody((BodyPart)mount);
                mount++;
                if (mount == 9)
                {
                    MessageBox.Show("You lost the word was " + word, "Lostest");
                    resetGame();
                }
            }
        }
        void resetGame()
        {
            Graphics g= panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            getRandWord();
            makeLabels();
            DrowHangPost();
            label2.Text = "Messed: ";
            textBox1.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text==word)
            {
                MessageBox.Show("You have win", "Congra");
                resetGame();
            }
            else
            {
                MessageBox.Show("The word is Wrong", "Falid");
                DrawBody((BodyPart)mount);
                mount++;
                if (mount == 9)
                {
                    MessageBox.Show("You lost the word was " + word, "Lostest");
                    resetGame();
                }
            }
        }
    }
}