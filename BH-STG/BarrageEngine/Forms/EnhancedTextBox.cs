using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BH_STG.BarrageEngine.Forms
{
    public partial class EnhancedTextBox : Form
    {
        public EnhancedTextBox()
        {
            InitializeComponent();
        }
        
        public void setText(string txt)
        {
            text_txt.Text = txt;
        }

        public void setTitle(string txt)
        {
            this.Text = txt;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Changelog_Load(object sender, EventArgs e)
        {
            this.Text = "Changelog";
        }
    }
}
