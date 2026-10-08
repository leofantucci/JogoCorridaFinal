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
            picEstatisticas = new PictureBox();
            picInicio = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            panelPrincipal2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSair).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEstatisticas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInicio).BeginInit();
            SuspendLayout();
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.BackgroundImage = Properties.Resources.LogoStreetCars;
            picLogo.BackgroundImageLayout = ImageLayout.Stretch;
            picLogo.Location = new Point(606, -19);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(451, 213);
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;
            // 
            // panelPrincipal2
            // 
            panelPrincipal2.BackColor = Color.Transparent;
            panelPrincipal2.Controls.Add(picSair);
            panelPrincipal2.Controls.Add(picEstatisticas);
            panelPrincipal2.Controls.Add(picInicio);
            panelPrincipal2.Controls.Add(picLogo);
            panelPrincipal2.Location = new Point(3, 5);
            panelPrincipal2.Margin = new Padding(3, 4, 3, 4);
            panelPrincipal2.Name = "panelPrincipal2";
            panelPrincipal2.Size = new Size(1639, 804);
            panelPrincipal2.TabIndex = 4;
            // 
            // picSair
            // 
            picSair.BackColor = Color.FromArgb(100, 0, 0, 0);
            picSair.BackgroundImage = Properties.Resources.picSair1;
            picSair.BackgroundImageLayout = ImageLayout.Stretch;
            picSair.Location = new Point(710, 619);
            picSair.Margin = new Padding(3, 4, 3, 4);
            picSair.Name = "picSair";
            picSair.Size = new Size(257, 116);
            picSair.TabIndex = 44;
            picSair.TabStop = false;
            // 
            // picEstatisticas
            // 
            picEstatisticas.BackColor = Color.FromArgb(100, 0, 0, 0);
            picEstatisticas.BackgroundImage = Properties.Resources.picEstatisticas;
            picEstatisticas.BackgroundImageLayout = ImageLayout.Stretch;
            picEstatisticas.Location = new Point(637, 424);
            picEstatisticas.Margin = new Padding(3, 4, 3, 4);
            picEstatisticas.Name = "picEstatisticas";
            picEstatisticas.Size = new Size(400, 157);
            picEstatisticas.TabIndex = 3;
            picEstatisticas.TabStop = false;
            // 
            // picInicio
            // 
            picInicio.BackColor = Color.FromArgb(100, 0, 0, 0);
            picInicio.BackgroundImage = Properties.Resources.picInicio;
            picInicio.BackgroundImageLayout = ImageLayout.Stretch;
            picInicio.Location = new Point(686, 263);
            picInicio.Margin = new Padding(3, 4, 3, 4);
            picInicio.Name = "picInicio";
            picInicio.Size = new Size(294, 123);
            picInicio.TabIndex = 2;
            picInicio.TabStop = false;
            // 
            // PrimeiraTelaJogo
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.FundoStreetCars;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1645, 812);
            Controls.Add(panelPrincipal2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "PrimeiraTelaJogo";
            Text = "PrimeiraTelaJogo";
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            panelPrincipal2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSair).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEstatisticas).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInicio).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picLogo;
        private Panel panelPrincipal2;
        private PictureBox picEstatisticas;
        private PictureBox picInicio;
        private PictureBox picSair;
    }
}