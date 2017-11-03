using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.IO;

namespace Personal_Assistant
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine speech = new SpeechRecognitionEngine();
        SpeechSynthesizer jarvis = new SpeechSynthesizer();

        SpeechRecognitionEngine speech0 = new SpeechRecognitionEngine();
        SpeechSynthesizer jarvis0 = new SpeechSynthesizer();
        int count = 1;
        int second = 0;
        List<string> _commands = new List<string>();
        List<string> _speech = new List<string>();
        List<string> _orders = new List<string>();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            notifyIcon1.Icon = this.Icon;
            //
            speech0.RequestRecognizerUpdate();
            speech0.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speech0_SpeechRecognize);
            Choices text = new Choices();
            string[] line = { "listen to me", "hey guy", "yo", "are you there" };
            text.Add(line);
            Grammar words = new Grammar(new GrammarBuilder(text));
            speech0.LoadGrammar(words);
            speech0.SetInputToDefaultAudioDevice();
            if (DateTime.Now.Hour > 0 && DateTime.Now.Hour < 12)
                jarvis0.SpeakAsync("good morning sir");
            else if (DateTime.Now.Hour == 12)
                jarvis0.SpeakAsync("good noon sir");
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour < 5)
                jarvis0.SpeakAsync("good afternoon sir");
            else if (DateTime.Now.Hour >= 21)
                jarvis0.SpeakAsync("good night sir");
            else
                jarvis0.SpeakAsync("hello sir");
            //speech0.RecognizeAsync(RecognizeMode.Multiple);
            //
            speech.RequestRecognizerUpdate();
            speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speech_SpeechRecognize);
            LoadGrammar();
            speech.SetInputToDefaultAudioDevice();
            button1.Focus();
        }

        private void speech0_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text = e.Result.Text;
            string sp = e.Result.Text;
            if (sp == "listen to me" || sp == "hey guy" || sp == "yo")
            {
                if (button1.Enabled)
                {
                    jarvis0.SpeakAsync("listening");
                    button1_Click(null, null);
                }
            }
        }

        private void LoadGrammar()
        {
            Choices text = new Choices();
            //load commands
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\cmds.dll");
            foreach (string line in lines)
            {
                _commands.Add(line);
            }
            //load speech
            string[] sps = File.ReadAllLines(Environment.CurrentDirectory + "\\speech.dll");
            foreach(string sp in sps)
            {
                _speech.Add(sp);
            }
            //load order

            //
            text.Add(lines);
            Grammar words = new Grammar(new GrammarBuilder(text));
            speech.LoadGrammar(words);
        }

        private void Rew()
        {
            button1.Enabled = true;
            button1.Text = "Listen";
            timer1.Stop();
            second = 0;
        }

        private void speech_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text = e.Result.Text;
            string sp = e.Result.Text;
            if (IsInList(sp))
            {
                if (sp == "hello")
                {
                    jarvis.SpeakAsync("Hello sir");
                    Rew();

                }
                if (sp == "yo")
                {
                    jarvis.SpeakAsync("Yo!");
                    Rew();

                }
                if (sp == "who am i")
                {
                    jarvis.SpeakAsync("You are my master sir");
                    Rew();
                }

                if (sp == "open this pc")
                {
                    jarvis.SpeakAsync("Opening this PC");
                    //string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    //Process.Start("explorer", myComputerPath);
                    Process.Start("explorer.exe", "::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
                    Rew();
                }
                if (sp == "open google")
                {
                    jarvis.SpeakAsync("Opening google");
                    Process.Start("https://www.google.com");
                    Rew();
                }
                if (sp == "open facebook")
                {
                    jarvis.SpeakAsync("Opening facebook");
                    Process.Start("https://www.facebook.com");
                    Rew();
                }
                if (sp == "who are you")
                {
                    jarvis.SpeakAsync("i am your personal assistant");
                    Rew();
                }
                if (sp == "what the fuck" || sp == "fuck you")
                {
                    jarvis.SpeakAsync("Fuck you!");
                    Rew();
                }
                if (sp == "nice")
                {
                    jarvis.SpeakAsync("Nothing sir");
                    Rew();
                }
                if (sp == "what time is it" || sp == "what is the time")
                {
                    jarvis.SpeakAsync(DateTime.Now.Hour.ToString("") + DateTime.Now.Minute.ToString(""));
                    Rew();
                }
                if (sp == "switch tab")
                {
                    jarvis.SpeakAsync("yes sir");
                    SendKeys.Send("%{TAB " + count + "}");
                    count += 1;
                    Rew();
                }
                if (sp == "play music")
                {
                    jarvis.SpeakAsync("Sorry, i am still under development, i just can open it now");
                    Process.Start("wmplayer.exe");
                    Rew();
                }
                if (sp == "go home")
                {
                    jarvis.Speak("Goodbye, see you soon!");
                    Application.Exit();
                    Rew();

                }
                if (sp == "switch voice")
                {
                    if (jarvis.Voice.Gender == VoiceGender.Male)
                    {
                        jarvis.SpeakAsync("right away");
                        jarvis.SelectVoiceByHints(VoiceGender.Female);
                        jarvis0.SelectVoiceByHints(VoiceGender.Female);

                    }
                    else
                    {
                        jarvis.SpeakAsync("right away");
                        jarvis.SelectVoiceByHints(VoiceGender.Male);
                        jarvis0.SelectVoiceByHints(VoiceGender.Male);
                    }
                    Rew();
                }
                if (sp == "how are you")
                {
                    jarvis.SpeakAsync("I am always fine sir");
                    Rew();
                }
                if (sp == "how do you do")
                {
                    jarvis.SpeakAsync("how do you do");
                    Rew();
                }
                if (sp == "open moodle")
                {
                    jarvis.SpeakAsync("opening moodle");
                    Process.Start("http://courses.fit.hcmus.edu.vn/login/index.php");
                    Rew();
                }
                if (sp == "what are you doing")
                {
                    jarvis.SpeakAsync("I'm listening to you");
                    Rew();
                }
            }
            else
            {

            }

            button1.Enabled = true;
            button1.Text = "Listen";
            timer1.Stop();
            second = 0;
        }

        private bool IsInList(string s)
        {
            for (int i = 0;i<_commands.Count;i++)
            {
                if (s == _commands[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            speech.RecognizeAsync(RecognizeMode.Single);
            button1.Enabled = false;
            button1.Text = "Listening...";
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second++;
            if (second >= 5)
            {
                Rew();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Personal Assistant Beta version" + "\nCopyleft (ɔ) 2016 \nCoded by Clear All", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (button1.Enabled)
                {
                    jarvis0.SpeakAsync("listening");
                    button1_Click(null, null);
                }
            }
        }

        
    }
}
