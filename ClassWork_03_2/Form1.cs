using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassWork_03_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox3.Text = "result.txt";
        }

        public List<int> workData = new List<int>();
        public Random rnd = new Random(DateTime.Now.Millisecond);

        public Mutex mutex_1_2 = null;
        public Mutex mutex_2_3 = null;

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc1, this);
            ThreadPool.QueueUserWorkItem(ThreadProc2, this);
            ThreadPool.QueueUserWorkItem(ThreadProc3, this);
        }

        private void ThreadProc1(object param)
        {
            Form1 form = param as Form1;
            form.mutex_1_2 = new Mutex(true);
            for (int i = 0; i < 20; i++)
            {
                form.workData.Add(rnd.Next(1, 101));
            }
            form.Invoke(new Action(() =>
            {
                form.textBox1.Text = "";
                foreach (int a in form.workData)
                {
                    form.textBox1.Text += a.ToString() + " ";
                }
            }));
            form.mutex_1_2.ReleaseMutex();
        }

        private void ThreadProc2(object param)
        {
            Form1 form = param as Form1;
            form.mutex_2_3 = new Mutex(true);
            
            while (form.mutex_1_2 is null) { Thread.Sleep(0); }
            form.mutex_1_2.WaitOne();

            form.workData.Reverse();
            form.Invoke(new Action(() => 
            {
                form.textBox2.Text = "";
                foreach (int a in form.workData)
                {
                    form.textBox2.Text += a.ToString() + " ";
                }
            }));

            form.mutex_2_3.ReleaseMutex();
        }

        public delegate string GetFileName();
        public string _GetFileName()
        {
            return textBox3.Text;
        }

        private void ThreadProc3(object param)
        {
            Form1 form = param as Form1;

            while(form.mutex_2_3 is null) { Thread.Sleep(0); }
            form.mutex_2_3.WaitOne();

            string fileName;
            string fileBody = "";
            
            fileName = (string)form.Invoke(new GetFileName(_GetFileName));

            foreach (int a in form.workData)
            {
                fileBody += a.ToString() + "\n";
            }

            File.WriteAllText(fileName, fileBody);

            MessageBox.Show("Work is done");
        }
    }
}