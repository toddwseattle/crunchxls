using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CrunchBaseXLS
{
    public partial class SearchingCrunchBaseProgressForm : Form
    {
        public SearchingCrunchBaseProgressForm()
        {
            InitializeComponent();
        }

        private void SearchingCrunchBaseProgressForm_Load(object sender, EventArgs e)
        {

        }
        private int totalfound=0;
        public int TotalFound
        {
            get { return totalfound; }
            set
            {
                totalfound = value;
                MatchCountLabel.Text = value.ToString("0,0");
                progressBar1.Maximum=value;
                progressBar1.Minimum=0;

            }
        
        }
        private int retrieved=0;
        public int Retireved
        {
            get { return retrieved; }
            set
            {
                retrieved = value;
                RetrievedCountLabel.Text = value.ToString("0,0");
                progressBar1.Value = value;
            }
        }
        private bool canceledhit=false;
        public bool Canceled { get { return canceledhit; } set { canceledhit = value; } }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Canceled = true;
        }

        private TimeSpan et=new TimeSpan(0);

        public TimeSpan EstimatedTime
        {
            get { return et; }
            set
            {
                et = value;
                EstimatedTimeLabel.Text = "Estimated Time: " + et.ToString(@"dd\.hh\:mm\:ss");
            }
        }

    }
}
