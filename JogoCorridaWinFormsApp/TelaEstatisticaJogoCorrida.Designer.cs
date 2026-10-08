namespace JogoCorridaWinFormsApp
{
    partial class TelaEstatisticaJogoCorrida
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
            panelPrincipal3 = new Panel();
            panel1 = new Panel();
            picSair = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            picLogo = new PictureBox();
            panelPrincipal3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSair).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelPrincipal3
            // 
            panelPrincipal3.BackColor = Color.Transparent;
            panelPrincipal3.Controls.Add(panel1);
            panelPrincipal3.Controls.Add(picLogo);
            panelPrincipal3.Location = new Point(14, 15);
            panelPrincipal3.Margin = new Padding(3, 4, 3, 4);
            panelPrincipal3.Name = "panelPrincipal3";
            panelPrincipal3.Size = new Size(1490, 765);
            panelPrincipal3.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(picSair);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(9, 10);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1490, 765);
            panel1.TabIndex = 3;
            // 
            // picSair
            // 
            picSair.BackgroundImage = Properties.Resources.picSair1;
            picSair.BackgroundImageLayout = ImageLayout.Stretch;
            picSair.Location = new Point(1323, 15);
            picSair.Margin = new Padding(3, 4, 3, 4);
            picSair.Name = "picSair";
            picSair.Size = new Size(143, 68);
            picSair.TabIndex = 44;
            picSair.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.LogoStreetCars;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(521, -19);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(451, 213);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.EstatisticaImagem1;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(160, 109);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1184, 750);
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.BackgroundImage = Properties.Resources.LogoStreetCars;
            picLogo.BackgroundImageLayout = ImageLayout.Stretch;
            picLogo.Location = new Point(521, -19);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(451, 213);
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // TelaEstatisticaJogoCorrida
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.FundoStreetCars;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1518, 795);
            Controls.Add(panelPrincipal3);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "TelaEstatisticaJogoCorrida";
            Text = "TelaEstatisticaJogoCorrida";
            panelPrincipal3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSair).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPrincipal3;
        private PictureBox picLogo;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox picSair;
    }
}