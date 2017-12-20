using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
namespace myfirstCSharp
{
    public partial class Form1 : Form
    {
        List<int> boy = new List<int>();
        List<int> girl = new List<int>();
        Dictionary<int, string> vip = new Dictionary<int, string>();
        System.Media.SoundPlayer sp;
        
        public Form1()
        {
            InitializeComponent();            
        }
        private void delete()
        {
            if (boy.Count > 0)
            {
                boy.Remove(int.Parse(p1.Text));
                girl.Remove(int.Parse(p2.Text));
            }
            else
            {
                girl.Remove(int.Parse(p1.Text));
                girl.Remove(int.Parse(p2.Text));
            }
            remaining.Text = "剩余人数：" + (boy.Count + girl.Count);
        }

        private void changeBgm(UnmanagedMemoryStream bgm)
        {
            sp.Stop();
            sp.Stream = bgm;
            sp.PlayLooping();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (remaining.Text == "")
            {
                remaining.Text = "剩余人数:" + total.Text;
                total.Enabled = false;
                int half = int.Parse(total.Text) / 2;
                for (int i = 0; i < half; i++)
                {
                    girl.Add(i + 1);
                    boy.Add(half + i + 1);
                }
                btnStart.PerformClick();
            }else
            {
                if (girl.Count <= 0)
                {
                    MessageBox.Show("匹配全部完成,大会结束");
                }
                else
                {
                    if (timer2.Enabled)
                    {
                        btnReroll.PerformClick();
                    }
                    else
                    {
                        if (timer1.Enabled)
                        {
                            timer1.Stop();
                            if (vip.ContainsKey(int.Parse(p1.Text)))
                            {
                                changeBgm(Properties.Resources.bgm);
                                info.Text = vip[int.Parse(p1.Text)];
                                //btnReroll.Visible = true;
                            }
                            else
                                changeBgm(Properties.Resources.bgmWaiting);
                            delete();
                        }
                        else
                        {
                            info.Text = "";
                            changeBgm(Properties.Resources.bgmRolling);
                            btnReroll.Visible = false;
                            timer1.Start();
                        }
                    }
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int index1, index2;
            if (boy.Count > 0)
            {
                index1 = rnd.Next(boy.Count);
                index2 = rnd.Next(girl.Count);

                p1.Text = (boy[index1]).ToString();
                p2.Text = (girl[index2]).ToString();
            }
            else
            {
                index1 = rnd.Next(girl.Count);
                index2 = rnd.Next(girl.Count);

                p1.Text = (girl[index1]).ToString();
                p2.Text = (girl[girl.Count - 1 - index1]).ToString();
            }       
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            sp = new System.Media.SoundPlayer(global::myfirstCSharp.Properties.Resources.bgmWaiting);
            sp.PlayLooping();
        }

        private void btnReroll_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled)
            {
                timer2.Stop();
                delete();
            }
            else
            {
                girl.Add(int.Parse(p2.Text));
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int index = rnd.Next(girl.Count);
            p2.Text = girl[index].ToString();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Location  = new System .Drawing.Point((this.Width - panel1.Width) / 2,(this.Height - panel1.Height) / 2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
