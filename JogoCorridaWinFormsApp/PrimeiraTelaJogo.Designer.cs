namespace JogoCorridaWinFormsApp
{
    partial class PrimeiraTelaJogo
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
            picLogo = new PictureBox();
            panelPrincipal2 = new Panel();
            picSair = new PictureBox();
            picInicio = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            panelPrincipal2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSair).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInicio).BeginInit();
            SuspendLayout();
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.BackgroundImage = Properties.Resources.LogoStreetCars;
            picLogo.BackgroundImageLayout = ImageLayout.Stretch;
            picLogo.Location = new Point(530, -15);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(395, 168);
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;
            // 
            // panelPrincipal2
            // 
            panelPrincipal2.BackColor = Color.Transparent;
            panelPrincipal2.Controls.Add(picSair);
            panelPrincipal2.Controls.Add(picInicio);
            panelPrincipal2.Controls.Add(picLogo);
            panelPrincipal2.Location = new Point(3, 4);
            panelPrincipal2.Name = "panelPrincipal2";
            panelPrincipal2.Size = new Size(1434, 635);
            panelPrincipal2.TabIndex = 4;
            // 
            // picSair
            // 
            picSair.BackColor = Color.FromArgb(100, 0, 0, 0);
            picSair.BackgroundImage = Properties.Resources.picSair1;
            picSair.BackgroundImageLayout = ImageLayout.Stretch;
            picSair.Location = new Point(615, 339);
            picSair.Name = "picSair";
            picSair.Size = new Size(225, 92);
            picSair.TabIndex = 44;
            picSair.TabStop = false;
            // 
            // picInicio
            // 
            picInicio.BackColor = Color.FromArgb(100, 0, 0, 0);
            picInicio.BackgroundImage = Properties.Resources.picInicio;
            picInicio.BackgroundImageLayout = ImageLayout.Stretch;
            picInicio.Location = new Point(600, 208);
            picInicio.Name = "picInicio";
            picInicio.Size = new Size(257, 97);
            picInicio.TabIndex = 2;
            picInicio.TabStop = false;
            // 
            // PrimeiraTelaJogo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.FundoStreetCars;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1439, 641);
            Controls.Add(panelPrincipal2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PrimeiraTelaJogo";
            Text = "PrimeiraTelaJogo";
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            panelPrincipal2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSair).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInicio).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picLogo;
        private Panel panelPrincipal2;
        private PictureBox picInicio;
        private PictureBox picSair;
    }
}