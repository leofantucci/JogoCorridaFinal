using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace JogoCorridaWinFormsApp
{
    
    public partial class TelaEstatisticaJogoCorrida : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // Ativa o WS_EX_COMPOSITED (Double Buffer do Windows)
                return handleParam;
            }
        }

        public TelaEstatisticaJogoCorrida()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            if (picSair != null)
            {
                picSair.Click += PicSair_Click;
            }

            CarregarEstatisticas();
        }

        private void CarregarEstatisticas()
        {
            try
            {
                string caminho = System.IO.Path.Combine(AppContext.BaseDirectory, "recorde.txt");
                int recorde = 0;
                if (System.IO.File.Exists(caminho))
                {
                    string texto = System.IO.File.ReadAllText(caminho).Trim();
                    int.TryParse(texto, out recorde);
                }

                Label lblRecorde = new Label
                {
                    AutoSize = true,
                    Font = new Font("Arial", 48, FontStyle.Bold),
                    ForeColor = Color.Gold,
                    BackColor = Color.Transparent,
                    Text = recorde.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                if (panelPrincipal3 != null)
                {
                    lblRecorde.Location = new Point((panelPrincipal3.Width - lblRecorde.PreferredWidth) / 2, 400);
                    panelPrincipal3.Controls.Add(lblRecorde);
                    lblRecorde.BringToFront();
                }
                else
                {
                    lblRecorde.Location = new Point((this.ClientSize.Width - lblRecorde.PreferredWidth) / 2, this.ClientSize.Height / 2);
                    this.Controls.Add(lblRecorde);
                    lblRecorde.BringToFront();
                }
            }
            catch { }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal3 != null)
            {
                panelPrincipal3.Dock = DockStyle.None;
                panelPrincipal3.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal3.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal3.Height) / 2;
                panelPrincipal3.Location = new Point(centroX, centroY);
            }


        }
        private void PicSair_Click(object sender, EventArgs e)
        {
            string senha = Interaction.InputBox("Digite a senha para fechar o jogo:", "Bloqueado", "");
            if (senha == "1234")
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Senha incorreta!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VoltarParaMenu()
        {
            PrimeiraTelaJogo menu = new PrimeiraTelaJogo();
            TrocarDeTela(menu);
        }

        private void TrocarDeTela(Form proximaTela)
        {
            proximaTela.Show(); // Abre a tela nova por cima

            // Espera 100 milissegundos antes de esconder a tela velha
            System.Windows.Forms.Timer timerTransicao = new System.Windows.Forms.Timer();
            timerTransicao.Interval = 100;
            timerTransicao.Tick += (s, args) =>
            {
                this.Hide(); // Esconde a tela antiga silenciosamente por baixo
                timerTransicao.Stop();
                timerTransicao.Dispose();
            };
            timerTransicao.Start();
        }
    }
}
