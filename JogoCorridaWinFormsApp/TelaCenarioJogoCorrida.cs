using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    
    public partial class TelaCenarioJogoCorrida : Form
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

        List<PictureBox> listaCenarios = new List<PictureBox>();
        int indiceSelecionado = 0;
        bool telaTravada = false;

        System.Windows.Forms.Timer timerRoleta = new System.Windows.Forms.Timer();
        System.Diagnostics.Stopwatch cronometroRoleta = new System.Diagnostics.Stopwatch();
        bool bordaAcesa = true;
        System.Media.SoundPlayer somRoleta = new System.Media.SoundPlayer(Properties.Resources.roleta_normal);
        System.Media.SoundPlayer somClick = new System.Media.SoundPlayer(Properties.Resources.som_click);


        private readonly bool isMultiplayer;


        public TelaCenarioJogoCorrida(bool modoMultiplayer)
        {
            InitializeComponent();

            isMultiplayer = modoMultiplayer;

            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;

            timerRoleta.Tick += TimerRoleta_Tick;
            this.Load += TelaCenarioJogoCorrida_Load;

        }


        private void TelaCenarioJogoCorrida_Load(object sender, EventArgs e)
        {
            CarregarMiniaturasCenarios();

            somRoleta.LoadAsync();
            somClick.LoadAsync();
            if (picSair != null) picSair.Click += PicSair_Click;

            if (panelPrincipal4 != null)
            {
                foreach (Control controle in panelPrincipal4.Controls)
                {
                    if (controle is not PictureBox pic)
                        continue;

                    bool ehCenario = pic.Tag is TipoCenario;
                    bool ehAleatorio = pic.Name == "picAleatorio";

                    if (!ehCenario && !ehAleatorio)
                        continue;

                    pic.BorderStyle = BorderStyle.None;
                    pic.Paint += Cenario_Paint;

                    listaCenarios.Add(pic);

                    if (ehAleatorio)
                        pic.Click += PicAleatorio_Click;
                    else
                        pic.Click += Cenario_Click;
                }
                listaCenarios = listaCenarios.OrderBy(p => p.Top).ThenBy(p => p.Left).ToList();
            }
            
        }

        private void Cenario_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (listaCenarios.Count > 0 && pic == listaCenarios[indiceSelecionado] && bordaAcesa)
            {
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle, Color.Yellow, 4, ButtonBorderStyle.Solid,
                    Color.Yellow, 4, ButtonBorderStyle.Solid, 
                    Color.Yellow, 4, ButtonBorderStyle.Solid, 
                    Color.Yellow, 4, ButtonBorderStyle.Solid);
            }
        }

        private void AtualizarBordaVisual()
        {
            if (listaCenarios.Count == 0) return;
            foreach (var pic in listaCenarios) pic.Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal4 != null)
            {
                panelPrincipal4.Dock = DockStyle.None;
                panelPrincipal4.Anchor = AnchorStyles.None;
                panelPrincipal4.Location = new Point((this.ClientSize.Width - panelPrincipal4.Width) / 2, (this.ClientSize.Height - panelPrincipal4.Height) / 2);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (telaTravada || listaCenarios.Count == 0) return true;

            if (keyData == Keys.Right || keyData == Keys.D || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaCenarios.Count) indiceSelecionado = 0;
                AtualizarBordaVisual(); return true;
            }
            else if (keyData == Keys.Left || keyData == Keys.A)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaCenarios.Count - 1;
                AtualizarBordaVisual(); return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaCenarios[indiceSelecionado]); return true;
            }
            else if (keyData == Keys.Escape)
            {
                VoltarParaMenu(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Cenario_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;
            PictureBox clicado = sender as PictureBox;
            indiceSelecionado = listaCenarios.IndexOf(clicado);
            somClick.Play();
            AtualizarBordaVisual();
            ConfirmarSelecao(clicado);
        }

        private void PicAleatorio_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;
            ExecutarSorteio();
        }

        private void ConfirmarSelecao(PictureBox selecionado)
        {
            if (selecionado.Name == "picAleatorio") ExecutarSorteio();
            else IniciarCorridaFinal(selecionado);
        }

        private void ExecutarSorteio()
        {
            telaTravada = true;
            somRoleta.Play();
            cronometroRoleta.Restart();
            timerRoleta.Interval = 40;
            timerRoleta.Start();
        }

        private void TimerRoleta_Tick(object sender, EventArgs e)
        {
            do
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaCenarios.Count) indiceSelecionado = 0;
            } while (listaCenarios[indiceSelecionado].Name == "picAleatorio");

            AtualizarBordaVisual();
            long tempoPassado = cronometroRoleta.ElapsedMilliseconds;

            if (tempoPassado > 6500) timerRoleta.Interval = 100;
            if (tempoPassado > 7800) timerRoleta.Interval = 300;
            if (tempoPassado > 9300) timerRoleta.Interval = 450;

            if (tempoPassado >= 9500)
            {
                timerRoleta.Stop();
                cronometroRoleta.Stop();
                IniciarCorridaFinal(listaCenarios[indiceSelecionado]);
            }
        }

        private void IniciarCorridaFinal(PictureBox cenarioEscolhido)
        {
            telaTravada = true;
            int piscadas = 0;
            System.Windows.Forms.Timer timerPiscar = new System.Windows.Forms.Timer();
            timerPiscar.Interval = 150;
            timerPiscar.Tick += (s, args) =>
            {
                bordaAcesa = !bordaAcesa;
                AtualizarBordaVisual();
                piscadas++;
                if (piscadas >= 6)
                {
                    timerPiscar.Stop();
                    bordaAcesa = true;
                    AtualizarBordaVisual();
                }
            };
            timerPiscar.Start();

            System.Windows.Forms.Timer timerEspera = new System.Windows.Forms.Timer();
            timerEspera.Interval = 2500;
            timerEspera.Tick += (s, args) =>
            {
                timerEspera.Stop();

                TipoCenario cenarioSelecionado = (TipoCenario)cenarioEscolhido.Tag;

                TelaNivelJogoCorrida telaNiveis = new TelaNivelJogoCorrida(isMultiplayer, cenarioSelecionado);
                TrocarDeTela(telaNiveis);
            };
            timerEspera.Start();
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

        private void CarregarMiniaturasCenarios()
        {
            picCenario1.Tag = TipoCenario.Areas_Rochosas;
            picCenario2.Tag = TipoCenario.Alem_Do_Mundo;
            picCenario3.Tag = TipoCenario.Terras_Desconhecidas;
            picCenario4.Tag = TipoCenario.Floresta_Feliz;
        }
    }
}
