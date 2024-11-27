﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using FunDraw;
namespace FunDraw
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lbForgetPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
        }

        private async void btLogin_Click(object sender, EventArgs e)
        {
            await Session.Login(tbUsername.Text, tbPassword.Text);

        }

        private void lbRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }
    }
}
