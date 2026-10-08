using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            VoltarParaMenu();
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
