namespace JogoCorridaWinFormsApp
{
    partial class FormJogoCorrida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerJogo = new System.Windows.Forms.Timer(components);
            picCarro = new PictureBox();
            picCarro2 = new PictureBox();
            picExplosao = new PictureBox();
            picPoder = new PictureBox();
            picPoder2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picCarro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCarro2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picExplosao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPoder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPoder2).BeginInit();
            SuspendLayout();
            // 
            // timerJogo
            // 
            timerJogo.Tick += timerJogo_Tick;
            // 
            // picCarro
            // 
            picCarro.BackColor = Color.Transparent;
            picCarro.Location = new Point(182, 315);
            picCarro.Name = "picCarro";
            picCarro.Size = new Size(60, 150);
            picCarro.TabIndex = 0;
            picCarro.TabStop = false;
            // 
            // picCarro2
            // 
            picCarro2.BackColor = Color.Transparent;
            picCarro2.Location = new Point(270, 315);
            picCarro2.Name = "picCarro2";
            picCarro2.Size = new Size(60, 150);
            picCarro2.TabIndex = 1;
            picCarro2.TabStop = false;
            // 
            // picExplosao
            // 
            picExplosao.BackColor = Color.Transparent;
            picExplosao.Location = new Point(543, 12);
            picExplosao.Name = "picExplosao";
            picExplosao.Size = new Size(250, 200);
            picExplosao.TabIndex = 2;
            picExplosao.TabStop = false;
            picExplosao.Visible = false;
            // 
            // picPoder
            // 
            picPoder.Location = new Point(0, 0);
            picPoder.Name = "picPoder";
            picPoder.Size = new Size(100, 50);
            picPoder.TabIndex = 3;
            picPoder.TabStop = false;
            // 
            // picPoder2
            // 
            picPoder2.Location = new Point(0, 0);
            picPoder2.Name = "picPoder2";
            picPoder2.Size = new Size(100, 50);
            picPoder2.TabIndex = 4;
            picPoder2.TabStop = false;
            // 
            // FormJogoCorrida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 561);
            Controls.Add(picPoder2);
            Controls.Add(picPoder);
            Controls.Add(picExplosao);
            Controls.Add(picCarro2);
            Controls.Add(picCarro);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormJogoCorrida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Jogo Corrida - IFSP";
            Load += FormJogoCorrida_Load;
            KeyDown += FormJogoCorrida_KeyDown;
            KeyUp += FormJogoCorrida_KeyUp;
            ((System.ComponentModel.ISupportInitialize)picCarro).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCarro2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExplosao).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPoder).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPoder2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerJogo;
        private PictureBox picCarro;
        private PictureBox picCarro2;
        private PictureBox picExplosao;
        private PictureBox picPoder;
        private PictureBox picPoder2;
    }
}