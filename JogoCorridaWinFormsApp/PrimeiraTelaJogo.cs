using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    public partial class PrimeiraTelaJogo : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // Ativa o WS_EX_COMPOSITED
                return handleParam;
            }
        }

        List<PictureBox> listaBotoes = new List<PictureBox>();
        int indiceSelecionado = 0;

        System.Media.SoundPlayer somClick = new System.Media.SoundPlayer(Properties.Resources.som_click);

        public PrimeiraTelaJogo()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += PrimeiraTelaJogo_Load;
        }

        private void PrimeiraTelaJogo_Load(object sender, EventArgs e)
        {
            GerenciadorMusica.TocarFundo("fundoJogo.mp3");
            somClick.LoadAsync();
            if (panelPrincipal2 != null)
            {
                foreach (Control controle in panelPrincipal2.Controls)
                {
                    if (controle is PictureBox pic)
                    {
                        if (pic.Name == "picLogo" || (pic.Image == null && pic.BackgroundImage == null))
                        {
                            continue;
                        }

                        pic.BorderStyle = BorderStyle.None;
                        pic.Paint += Botao_Paint;
                        pic.Click += Botao_Click;
                        listaBotoes.Add(pic);
                    }
                }
                listaBotoes = listaBotoes.OrderBy(p => p.Top).ToList();
            }
            AtualizarBordaVisual();
        }

        private void Botao_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;

            if (listaBotoes.Count > 0 && pic == listaBotoes[indiceSelecionado])
            {
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid);
            }
        }

        private void AtualizarBordaVisual()
        {
            if (listaBotoes.Count == 0) return;

            foreach (var pic in listaBotoes)
            {
                pic.Invalidate();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal2 != null)
            {
                panelPrincipal2.Dock = DockStyle.None;
                panelPrincipal2.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal2.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal2.Height) / 2;
                panelPrincipal2.Location = new Point(centroX, centroY);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (listaBotoes.Count == 0) return base.ProcessCmdKey(ref msg, keyData);

            if (keyData == Keys.Down || keyData == Keys.S || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaBotoes.Count) indiceSelecionado = 0;
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Up || keyData == Keys.W)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaBotoes.Count - 1;
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaBotoes[indiceSelecionado]);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Botao_Click(object sender, EventArgs e)
        {
            PictureBox clicado = sender as PictureBox;
            indiceSelecionado = listaBotoes.IndexOf(clicado);
            AtualizarBordaVisual();
            ConfirmarSelecao(clicado);
            somClick.Play();
        }

        private void ConfirmarSelecao(PictureBox selecionado)
        {
            if (selecionado.Name == "picInicio")
            {
                TelaEscolhaJogoCorrida telaSelecao = new TelaEscolhaJogoCorrida();
                TrocarDeTela(telaSelecao);
            }
            else if (selecionado.Name == "picEstatisticas")
            {
                TelaEstatisticaJogoCorrida telaSelecao = new TelaEstatisticaJogoCorrida();
                TrocarDeTela(telaSelecao);
            }
            else if (selecionado.Name == "picSair")
            {
                Application.Exit();
            }
        }

        private void PicSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // SISTEMA DE TRANSIÇÃO SUAVE (Tela preta de arcade)
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