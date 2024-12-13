﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FunDraw.Types
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string ID { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Email { get; set; }
    }
}
namespace FunDraw
{
    public partial class HoSoNgChoi : Form
    {
        private Size formOriginalSize;
        private Rectangle circle;
        private Rectangle click_pic;
        private Rectangle player;
        private Rectangle id;
        private Rectangle tham_gia;
        private Rectangle email;
        private Rectangle password;
        private Rectangle forgot_pass;
        public HoSoNgChoi()
        {
            InitializeComponent();
            this.Resize += HoSoNgChoi_Resize;
            formOriginalSize = this.Size;
            circle = new Rectangle(guna2CirclePictureBox1.Location, guna2CirclePictureBox1.Size);
            click_pic = new Rectangle(Click_pic.Location, Click_pic.Size);
            player = new Rectangle(lbPlayer.Location, lbPlayer.Size);
            id = new Rectangle(label2.Location, label2.Size);
            tham_gia = new Rectangle(label3.Location, label3.Size);
            email = new Rectangle(label4.Location, label4.Size);
            forgot_pass = new Rectangle(lbChangePassword.Location, lbChangePassword.Size);
        }
        private Types.UserProfile userProfile;
        public HoSoNgChoi(Types.UserProfile profile = null)
        {
            InitializeComponent();
            this.Resize += HoSoNgChoi_Resize;
            formOriginalSize = this.Size;
            circle = new Rectangle(guna2CirclePictureBox1.Location, guna2CirclePictureBox1.Size);
            click_pic = new Rectangle(Click_pic.Location, Click_pic.Size);
            player = new Rectangle(lbPlayer.Location, lbPlayer.Size);
            id = new Rectangle(label2.Location, label2.Size);
            tham_gia = new Rectangle(label3.Location, label3.Size);
            email = new Rectangle(label4.Location, label4.Size);
            forgot_pass = new Rectangle(lbChangePassword.Location, lbChangePassword.Size);
            userProfile = profile;
            userProfile = profile ?? new Types.UserProfile
            {
                Username = "Người chơi mặc định",
                ID = "0000",
                JoinedDate = DateTime.Now,
                Email = "default@example.com"
            };
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            lbPlayer.Text = $"Tên người chơi: {userProfile.Username}";
            lbID.Text = $"ID: {userProfile.ID}";
            lbJoin.Text = $"Ngày tham gia: {userProfile.JoinedDate:yyyy-MM-dd}";
            lbEmail.Text = $"Email: {userProfile.Email}";
        }
        private void resize_control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void HoSoNgChoi_Resize(object sender, EventArgs e)
        {
            resize_control(guna2CirclePictureBox1, circle);
            resize_control(Click_pic, click_pic);
            resize_control(lbPlayer, player);
            resize_control(label2, id);
            resize_control(label3, tham_gia);
            resize_control(label4, email);
            resize_control(lbChangePassword, forgot_pass);
        }

        private void HoSoNgChoi_Load(object sender, EventArgs e)
        {

        }

        private void lbChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.ShowDialog();
        }

    }
}
