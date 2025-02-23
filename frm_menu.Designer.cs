namespace NT_Movie_2025
{
    partial class frm_menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_adduser = new System.Windows.Forms.Button();
            this.btn_addmovie = new System.Windows.Forms.Button();
            this.btn_addtype = new System.Windows.Forms.Button();
            this.btn_addmember = new System.Windows.Forms.Button();
            this.btn_rent = new System.Windows.Forms.Button();
            this.btn_return = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_adduser
            // 
            this.btn_adduser.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_adduser.Location = new System.Drawing.Point(51, 43);
            this.btn_adduser.Name = "btn_adduser";
            this.btn_adduser.Size = new System.Drawing.Size(216, 112);
            this.btn_adduser.TabIndex = 0;
            this.btn_adduser.Text = "ຜູ້ໃຊ້";
            this.btn_adduser.UseVisualStyleBackColor = true;
            this.btn_adduser.Click += new System.EventHandler(this.btn_adduser_Click);
            // 
            // btn_addmovie
            // 
            this.btn_addmovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addmovie.Location = new System.Drawing.Point(299, 43);
            this.btn_addmovie.Name = "btn_addmovie";
            this.btn_addmovie.Size = new System.Drawing.Size(230, 112);
            this.btn_addmovie.TabIndex = 1;
            this.btn_addmovie.Text = "ໜັງ";
            this.btn_addmovie.UseVisualStyleBackColor = true;
            this.btn_addmovie.Click += new System.EventHandler(this.btn_addmovie_Click);
            // 
            // btn_addtype
            // 
            this.btn_addtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addtype.Location = new System.Drawing.Point(556, 43);
            this.btn_addtype.Name = "btn_addtype";
            this.btn_addtype.Size = new System.Drawing.Size(207, 112);
            this.btn_addtype.TabIndex = 2;
            this.btn_addtype.Text = "ປະເພດໜັງ";
            this.btn_addtype.UseVisualStyleBackColor = true;
            this.btn_addtype.Click += new System.EventHandler(this.btn_addtype_Click);
            // 
            // btn_addmember
            // 
            this.btn_addmember.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addmember.Location = new System.Drawing.Point(51, 276);
            this.btn_addmember.Name = "btn_addmember";
            this.btn_addmember.Size = new System.Drawing.Size(216, 102);
            this.btn_addmember.TabIndex = 3;
            this.btn_addmember.Text = "ສະມາຊິກ";
            this.btn_addmember.UseVisualStyleBackColor = true;
            this.btn_addmember.Click += new System.EventHandler(this.btn_addmember_Click);
            // 
            // btn_rent
            // 
            this.btn_rent.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rent.Location = new System.Drawing.Point(299, 276);
            this.btn_rent.Name = "btn_rent";
            this.btn_rent.Size = new System.Drawing.Size(230, 102);
            this.btn_rent.TabIndex = 4;
            this.btn_rent.Text = "ເຊົ່າໜັງ";
            this.btn_rent.UseVisualStyleBackColor = true;
            this.btn_rent.Click += new System.EventHandler(this.btn_rent_Click);
            // 
            // btn_return
            // 
            this.btn_return.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_return.Location = new System.Drawing.Point(556, 276);
            this.btn_return.Name = "btn_return";
            this.btn_return.Size = new System.Drawing.Size(207, 102);
            this.btn_return.TabIndex = 5;
            this.btn_return.Text = "ປິດໂປຣແກຣມ";
            this.btn_return.UseVisualStyleBackColor = true;
            this.btn_return.Click += new System.EventHandler(this.btn_return_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Chartreuse;
            this.button1.Font = new System.Drawing.Font("Phetsarath OT", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(337, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 39);
            this.button1.TabIndex = 27;
            this.button1.Text = "back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_return);
            this.Controls.Add(this.btn_rent);
            this.Controls.Add(this.btn_addmember);
            this.Controls.Add(this.btn_addtype);
            this.Controls.Add(this.btn_addmovie);
            this.Controls.Add(this.btn_adduser);
            this.Name = "frm_menu";
            this.Text = "frm_menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_adduser;
        private System.Windows.Forms.Button btn_addmovie;
        private System.Windows.Forms.Button btn_addtype;
        private System.Windows.Forms.Button btn_addmember;
        private System.Windows.Forms.Button btn_rent;
        private System.Windows.Forms.Button btn_return;
        private System.Windows.Forms.Button button1;
    }
}