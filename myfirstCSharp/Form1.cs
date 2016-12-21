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
            boy.AddRange(new int[] { 1, 8, 10, 12, 13, 14, 16, 18, 19, 20, 25, 39, 45, 46, 47, 48, 49, 51, 54, 55, 56, 57, 58, 61, 62, 63, 64, 65, 68, 70, 75, 78, 80 });
            girl.AddRange(new int[] { 2, 3, 4, 5, 6, 7, 9, 11, 15, 17, 21, 22, 23, 24, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 40, 41, 42, 43, 44, 50, 52, 53, 59, 60, 66, 67, 69, 71, 72, 73, 74, 76, 77, 79, 81, 82 });
            vip.Add(13, "不说了，为涛哥奉上特殊BGM");
            vip.Add(8, "小秦同学今天盘亏手机一部，请大家关照一下");
            vip.Add(10,"感谢下伦带逛，带同学们一直high");
            vip.Add(12, "本码农也出来露个面");

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
            if (girl.Count <= 0)
            {
                MessageBox.Show("匹配全部完成,大会结束");
            }
            else
            {
                if (timer2.Enabled)
                {
                    btnReroll.PerformClick();
                }else
                {
                    if (timer1.Enabled)
                    {
                        timer1.Stop();
                        //MessageBox.Show( boy.Count + "  " + girl.Count);
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

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            panel1.Left = (ActiveForm.Width - panel1.Width) / 2;
            panel1.Top = (ActiveForm.Height - panel1.Height) / 2;
            panel1.Refresh();
        }
    }
}
