using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    public partial class TelaNivelJogoCorrida : Form
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
        List<PictureBox> listaNiveis = new List<PictureBox>();
        int indiceSelecionado = 0;
        bool telaTravada = false;

        System.Media.SoundPlayer somClick = new System.Media.SoundPlayer(Properties.Resources.som_click);

        private readonly bool isMultiplayer;
        private readonly TipoCenario cenarioSelecionado;
        public TelaNivelJogoCorrida(bool modoMultiplayer, TipoCenario cenario)
        {
            InitializeComponent();
            this.isMultiplayer = modoMultiplayer;
            this.cenarioSelecionado = cenario;
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += TelaNivelJogoCorrida_Load;
        }

        private void TelaNivelJogoCorrida_Load(object sender, EventArgs e)
        {
            somClick.LoadAsync();
            if (picSair != null) picSair.Click += PicSair_Click;

            if (panelPrincipal5 != null)
            {
                foreach (Control controle in panelPrincipal5.Controls)
                {
                    if (controle is not PictureBox pic) continue;

                    if (pic.Name == "picLogo" || pic.Name == "picSair" || pic.Name == "picDificuldade" || (pic.Image == null && pic.BackgroundImage == null))
                        continue;

                    pic.BorderStyle = BorderStyle.None;
                    pic.Paint += Nivel_Paint;
                    pic.Click += Nivel_Click;

                    listaNiveis.Add(pic);
                }

                listaNiveis = listaNiveis.OrderBy(p => p.Left).ThenBy(p => p.Top).ToList();
            }

            AtualizarBordaVisual();
        }

        private void Nivel_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (listaNiveis.Count > 0 && pic == listaNiveis[indiceSelecionado])
            {
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                    Color.Yellow, 4, ButtonBorderStyle.Solid,
                    Color.Yellow, 4, ButtonBorderStyle.Solid,
                    Color.Yellow, 4, ButtonBorderStyle.Solid,
                    Color.Yellow, 4, ButtonBorderStyle.Solid);
            }
        }
        private void AtualizarBordaVisual()
        {
            if (listaNiveis.Count == 0) return;
            foreach (var pic in listaNiveis) pic.Invalidate();
        }
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal5 != null)
            {
                panelPrincipal5.Dock = DockStyle.None;
                panelPrincipal5.Anchor = AnchorStyles.None;
                panelPrincipal5.Location = new Point((this.ClientSize.Width - panelPrincipal5.Width) / 2, (this.ClientSize.Height - panelPrincipal5.Height) / 2);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (telaTravada || listaNiveis.Count == 0) return true;

            if (keyData == Keys.Right || keyData == Keys.D || keyData == Keys.Down || keyData == Keys.S || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaNiveis.Count) indiceSelecionado = 0;
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Left || keyData == Keys.A || keyData == Keys.Up || keyData == Keys.W)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaNiveis.Count - 1;
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaNiveis[indiceSelecionado]);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                VoltarParaMenu();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Nivel_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;

            PictureBox clicado = sender as PictureBox;
            indiceSelecionado = listaNiveis.IndexOf(clicado);
            AtualizarBordaVisual();
            ConfirmarSelecao(clicado);
        }
        private void ConfirmarSelecao(PictureBox selecionado)
        {
            telaTravada = true;
            somClick.Play();

            // Lógica para descobrir qual nível foi clicado pela Tag ou pelo Nome
            int nivelEscolhido = 1; // 1 = Fácil, 2 = Médio, 3 = Difícil
            if (selecionado.Name.Contains("Medio") || selecionado.Name.Contains("2")) nivelEscolhido = 2;
            if (selecionado.Name.Contains("Dificil") || selecionado.Name.Contains("3")) nivelEscolhido = 3;

            // Envia tudo para o jogo final
            FormJogoCorrida jogoFinal = new FormJogoCorrida(isMultiplayer, cenarioSelecionado, nivelEscolhido);
            TrocarDeTela(jogoFinal);
        }
        private void PicSair_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;
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
