using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WIndow_Switcher
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            listView1.GridLines = true;
            listView1.View = View.Details;
            process_List();
            
            
            
        }
        

        void process_List()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                var memory = (theprocess.WorkingSet64 / 1024) / 1024;
                string check = theprocess.ProcessName;





                ListViewItem item = new ListViewItem(new[] {check,theprocess.Id.ToString(),
                                                                    memory.ToString() +" mb"
                                                                    });
				                                     	
				                                     	
				                                     
                string winCheck = theprocess.MainWindowTitle;
                if (!String.IsNullOrEmpty(winCheck))
                {
                    listView1.Items.Add(item);
                }
                else
                {
                    //do nothing
                }
                

            }
            
           

        }

        void FadeAway(double Speed, int WaitSpeed )
        {
            for (double x = 1; x > 0; x = x - Speed)
            {
                this.Opacity = x;
                System.Threading.Thread.Sleep(WaitSpeed);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName(proc_text.Text);
            if (pname.Length == 0)
            {
                this.Close();
            }
            ShowInTaskbar = false;

            foreach (Process p in Process.GetProcessesByName(proc_text.Text))
            {
                SwitchToThisWindow(p.MainWindowHandle, true);
  
            }
        }

        private void StartBTN_Click(object sender, EventArgs e)
        {

            Process[] pname = Process.GetProcessesByName(proc_text.Text);
            if (pname.Length == 0)
            {
                MessageBox.Show(this,"Not a valid process","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                
            else{

            timer1.Start();
            FadeAway(0.10,30);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count == 0)
            {
                //nothing
            }
            else
            {
                proc_text.Text = listView1.SelectedItems[0].Text;
                
            }

        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            process_List();
        }

        private void proc_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;

                if (!proc_text.AcceptsReturn)
                {

                    StartBTN.PerformClick();
                }
            }
        }

        

       

        

        
    }
}
