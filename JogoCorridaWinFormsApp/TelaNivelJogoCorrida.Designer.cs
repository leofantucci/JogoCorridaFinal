namespace JogoCorridaWinFormsApp
{
    partial class TelaNivelJogoCorrida
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
            panelPrincipal5 = new Panel();
            picSair = new PictureBox();
            picDificil = new PictureBox();
            picMedio = new PictureBox();
            picFacil = new PictureBox();
            picDificuldade = new PictureBox();
            picLogo = new PictureBox();
            panelPrincipal5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSair).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDificil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMedio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picFacil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDificuldade).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelPrincipal5
            // 
            panelPrincipal5.BackColor = Color.Transparent;
            panelPrincipal5.Controls.Add(picSair);
            panelPrincipal5.Controls.Add(picDificil);
            panelPrincipal5.Controls.Add(picMedio);
            panelPrincipal5.Controls.Add(picFacil);
            panelPrincipal5.Controls.Add(picDificuldade);
            panelPrincipal5.Controls.Add(picLogo);
            panelPrincipal5.Location = new Point(2, 12);
            panelPrincipal5.Name = "panelPrincipal5";
            panelPrincipal5.Size = new Size(1413, 589);
            panelPrincipal5.TabIndex = 0;
            // 
            // picSair
            // 
            picSair.BackgroundImage = Properties.Resources.picSair1;
            picSair.BackgroundImageLayout = ImageLayout.Stretch;
            picSair.Location = new Point(1085, 56);
            picSair.Margin = new Padding(3, 4, 3, 4);
            picSair.Name = "picSair";
            picSair.Size = new Size(143, 68);
            picSair.TabIndex = 45;
            picSair.TabStop = false;
            // 
            // picDificil
            // 
            picDificil.BackgroundImage = Properties.Resources.picDificil;
            picDificil.BackgroundImageLayout = ImageLayout.Stretch;
            picDificil.Location = new Point(835, 401);
            picDificil.Name = "picDificil";
            picDificil.Size = new Size(198, 132);
            picDificil.TabIndex = 6;
            picDificil.TabStop = false;
            // 
            // picMedio
            // 
            picMedio.BackgroundImage = Properties.Resources.picMedio;
            picMedio.BackgroundImageLayout = ImageLayout.Stretch;
            picMedio.Location = new Point(635, 401);
            picMedio.Name = "picMedio";
            picMedio.Size = new Size(198, 132);
            picMedio.TabIndex = 5;
            picMedio.TabStop = false;
            // 
            // picFacil
            // 
            picFacil.BackgroundImage = Properties.Resources.picFacil1;
            picFacil.BackgroundImageLayout = ImageLayout.Stretch;
            picFacil.Location = new Point(435, 401);
            picFacil.Name = "picFacil";
            picFacil.Size = new Size(198, 132);
            picFacil.TabIndex = 4;
            picFacil.TabStop = false;
            // 
            // picDificuldade
            // 
            picDificuldade.BackgroundImage = Properties.Resources.picDificuldade;
            picDificuldade.BackgroundImageLayout = ImageLayout.Stretch;
            picDificuldade.Location = new Point(555, 201);
            picDificuldade.Name = "picDificuldade";
            picDificuldade.Size = new Size(372, 156);
            picDificuldade.TabIndex = 3;
            picDificuldade.TabStop = false;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.BackgroundImage = Properties.Resources.LogoStreetCars;
            picLogo.BackgroundImageLayout = ImageLayout.Stretch;
            picLogo.Location = new Point(505, 4);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(467, 230);
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // TelaNivelJogoCorrida
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.FundoStreetCars;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1427, 613);
            Controls.Add(panelPrincipal5);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TelaNivelJogoCorrida";
            Text = "TelaNivelJogoCorrida";
            panelPrincipal5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSair).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDificil).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMedio).EndInit();
            ((System.ComponentModel.ISupportInitialize)picFacil).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDificuldade).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPrincipal5;
        private PictureBox picLogo;
        private PictureBox picDificuldade;
        private PictureBox picDificil;
        private PictureBox picMedio;
        private PictureBox picFacil;
        private PictureBox picSair;
    }
}