﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalMobilAdminApp
{
    public partial class DashboardForm : Form
    {
        
        public DashboardForm()
        {
            InitializeComponent();
        }


        private void DashboardForm_Load(object sender, EventArgs e)
        {
            button2.Click += new EventHandler(button2_Click);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MobilForm mobilForm = new MobilForm();
            mobilForm.Show();
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

    }
}
