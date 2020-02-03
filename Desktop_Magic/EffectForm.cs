using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace DM
{
    public partial class EffectForm : Form
    {
        List<CFlake> pics = new List<CFlake>();
        Random rnd = new Random();
        Random rd = new Random();
        int speed = 10;
        int number = 10;
        int amount = 10;
        int Size = 50;
        string path = "";
        int type = 1;
        Graphics g;
        int screenHeight;
        int screenWidth;
        int size;
        Point p = new Point(0, 0);
        int x;
        int y;
        RegistryKey r1;
        public EffectForm()
        {
            InitializeComponent();
            speed = CGlob.Speed;
            number = CGlob.Number;
            amount = CGlob.Amount;
            type = CGlob.Type;
            Size = CGlob.Size;
            path = CGlob.Path;
            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();
            pictureBox5.Hide();
            timer1.Interval = 17;   
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }

        private void InitPictureBox()
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if ((int)r1.GetValue("DMT") == 3)
            {
                if (File.Exists(CGlob.Path))
                {
                    pictureBox4.Image = Image.FromFile(CGlob.Path);

                }
                else
                {
                    CGlob.Type = 1;                   
                    MessageBox.Show("Can not find your picture!!?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }     
            }
            r1.Close();
            r1.Dispose();
            for (int i = 0; i < amount; i++)
            {
                CFlake pic = new CFlake();
                pics.Add(pic);
                CreateFlake(pic);
            }
        }
        private void CreateFlake(CFlake pic)
        {
            path = CGlob.Path;
            Size = CGlob.Size;
            type = CGlob.Type;
            screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            if (type == 1)
            {
                pic.Image = pictureBox1.Image;
                size = rnd.Next(36) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 2)
            {
                pic.Image = pictureBox2.Image;
                size = rnd.Next(8) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 3)
            {
                pictureBox4.Image = Image.FromFile(CGlob.Path);
                pic.Image = pictureBox4.Image;
                size = rnd.Next(CGlob.Size) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 4)
            {
                pic.Image = pictureBox5.Image;
                size = rnd.Next(25) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else
            {
                
                int o = rd.Next(1, 5);
                if (o == 1)
                {
                    pic.Image = pictureBox5.Image;
                    size = rnd.Next(25) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else if (o == 2)
                {
                    pic.Image = pictureBox1.Image;
                    size = rnd.Next(36) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else if (o == 3)
                {
                    pic.Image = pictureBox2.Image;
                    size = rnd.Next(8) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else
                {
                    if (File.Exists(CGlob.Path))
                    {
                        pictureBox4.Image = Image.FromFile(CGlob.Path);
                        pic.Image = pictureBox4.Image;
                        size = rnd.Next(CGlob.Size) + 5;
                        pic.Size = new System.Drawing.Size(size, size);
                    }
                    else
                    {
                        int o1 = rd.Next(1, 5);
                        if (o1 == 1)
                        {
                            pic.Image = pictureBox5.Image;
                            size = rnd.Next(25) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                        else if (o1 == 2)
                        {
                            pic.Image = pictureBox1.Image;
                            size = rnd.Next(36) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                        else if (o1 == 3)
                        {
                            pic.Image = pictureBox2.Image;
                            size = rnd.Next(8) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                    }
                }
               
                
            }
            pic.Position = new System.Drawing.Point(rnd.Next(screenWidth), rnd.Next(-15, -10));
            int sign = rnd.Next(2) == 0 ? -1 : 1;
            pic.Direction = rnd.Next(10) * sign;
            pic.Speed = rnd.Next(1);

        }
        //
        private void RecreateFlake(CFlake pic)
        {
            Size = CGlob.Size;
            type = CGlob.Type;
            screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            if (type == 1)
            {
                size = rnd.Next(36) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 2)
            {
                size = rnd.Next(8) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 3)
            {
                size = rnd.Next(CGlob.Size) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else if (type == 4)
            {
                size = rnd.Next(25) + 5;
                pic.Size = new System.Drawing.Size(size, size);
            }
            else
            {
                int o = rd.Next(1, 5);
                if (o == 1)
                {
                    pic.Image = pictureBox5.Image;
                    size = rnd.Next(25) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else if (o == 2)
                {
                    pic.Image = pictureBox1.Image;
                    size = rnd.Next(36) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else if (o==3)
                {
                    pic.Image = pictureBox2.Image;
                    size = rnd.Next(8) + 5;
                    pic.Size = new System.Drawing.Size(size, size);
                }
                else
                {
                    if (File.Exists(CGlob.Path) && pictureBox4.Image != null)
                    {
                        pic.Image = pictureBox4.Image;
                        size = rnd.Next(CGlob.Size) + 5;
                        pic.Size = new System.Drawing.Size(size, size);
                    }
                    else
                    {
                        int o1 = rd.Next(1, 5);
                        if (o1 == 1)
                        {
                            pic.Image = pictureBox5.Image;
                            size = rnd.Next(25) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                        else if (o1 == 2)
                        {
                            pic.Image = pictureBox1.Image;
                            size = rnd.Next(36) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                        else if (o1 == 3)
                        {
                            pic.Image = pictureBox2.Image;
                            size = rnd.Next(8) + 5;
                            pic.Size = new System.Drawing.Size(size, size);
                        }
                    }
                }
                
            }
            pic.Position = new System.Drawing.Point(rnd.Next(screenWidth), rnd.Next(-15, -10));
            int sign = rnd.Next(2) == 0 ? -1 : 1;
            pic.Direction = rnd.Next(10) * sign;
            pic.Speed = rnd.Next(1);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            InitPictureBox();
        }
        public void Rescale()
        {
            if (pics.Count != CGlob.Amount || type != CGlob.Type || path != CGlob.Path)
            {  
                g = this.CreateGraphics();
                for (int i = 0; i < pics.Count; i++)
                {
                    g.FillRectangle(SystemBrushes.Control, pics[i].Bounds); 
                }
                pics.Clear();
                g.Dispose();
                for (int i=0;i<CGlob.Amount;i++)
                {
                    CFlake pic = new CFlake();
                    pics.Add(pic);
                    CreateFlake(pic);
                }
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Rescale();
            speed = CGlob.Speed;
            number = CGlob.Number;
            type = CGlob.Type;
            g = this.CreateGraphics();
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            for (int i = 0; i < pics.Count; i++)
            {             
                g.FillRectangle(SystemBrushes.Control, pics[i].Bounds);
                if (pics[i].Top > screenHeight || pics[i].Right < 0 || pics[i].Left > screenWidth)
                {
                    RecreateFlake(pics[i]);
                    continue;
                }
                if (type == 1)
                {
                    if (speed > 0)
                    {
                        pics[i].Image = pictureBox1.Image;
                    }
                    else if (speed < 0)
                    {
                        pics[i].Image = pictureBox3.Image;
                    }
                    if (speed == 0)
                    {
                        if (pics[i].Direction > 0)
                            pics[i].Image = pictureBox1.Image;
                        else
                            pics[i].Image = pictureBox3.Image;
                    }
                }
                pics[i].Speed = number;
                x = pics[i].Position.X;
                y = pics[i].Position.Y;

                x = x + pics[i].Direction + speed;
                y = y + pics[i].Speed;
                //new point
                p.X = x;
                p.Y = y;
                pics[i].Position = p;
                if (pics[i].Image != null)
                    g.DrawImage(pics[i].Image, pics[i].Bounds);
            }
            g.Dispose();
        } 
    }
}
