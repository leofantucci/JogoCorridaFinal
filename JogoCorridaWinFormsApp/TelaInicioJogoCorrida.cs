using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Obrigatório para ordenar a navegação do teclado
using System.Windows.Forms;


namespace JogoCorridaWinFormsApp
{
    
    public partial class TelaInicioJogoCorrida : Form
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

        List<PictureBox> listaPersonagens = new List<PictureBox>();
        int indiceSelecionado = 0;
        bool telaTravada = false;

        System.Windows.Forms.Timer timerRoleta = new System.Windows.Forms.Timer();
        
        int passosRestantes = 0;

        System.Diagnostics.Stopwatch cronometroRoleta = new System.Diagnostics.Stopwatch();
        PictureBox personagemVencedor = null;

        bool bordaAcesa = true;

        System.Media.SoundPlayer somRoleta = new System.Media.SoundPlayer(Properties.Resources.roleta_normal);
        System.Media.SoundPlayer somClick = new System.Media.SoundPlayer(Properties.Resources.som_click);

        public bool isMultiplayer = false;
        public int jogadorAtual = 1;
        PictureBox personagemP1 = null;

        public TelaInicioJogoCorrida(bool modoMultiplayer = false)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;

            isMultiplayer = modoMultiplayer;
            timerRoleta.Tick += TimerRoleta_Tick;
            this.Load += TelaInicioJogoCorrida_Load;
        }

        private void TelaInicioJogoCorrida_Load(object sender, EventArgs e)
        {
            somRoleta.LoadAsync();
            somClick.LoadAsync();

            if (picSair != null)
            {
                picSair.Click += PicSair_Click;
            }

            if (panelPrincipal != null)
            {
                foreach (Control controle in panelPrincipal.Controls)
                {
                    if (controle is PictureBox pic)
                    {
                        if (pic.Name == "picLogo" || pic.Name == "picSair" || pic.Name == "picTitulo" || (pic.Image == null && pic.BackgroundImage == null))
                        {
                            continue;
                        }

                        pic.BorderStyle = BorderStyle.None;
                        pic.Paint += Personagem_Paint;

                        listaPersonagens.Add(pic);

                        if (pic.Name == "picAleatorio")
                            pic.Click += PicAleatorio_Click;
                        else
                            pic.Click += Personagem_Click;
                    }
                }
                listaPersonagens = listaPersonagens.OrderBy(p => p.Top).ThenBy(p => p.Left).ToList();
            }

            if (listaPersonagens.Count > 0)
            {
                AtualizarBordaVisual();
            }
        }

        private void Personagem_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;

            // Configurações da fonte e cores da "Etiqueta" no canto
            Font fonteTag = new Font("Impact", 12, FontStyle.Regular);
            SolidBrush fundoVermelho = new SolidBrush(Color.Red);
            SolidBrush fundoAzul = new SolidBrush(Color.DeepSkyBlue);
            SolidBrush corTexto = new SolidBrush(Color.White);

            // 1. Borda e Etiqueta de quem está navegando agora
            if (listaPersonagens.Count > 0 && pic == listaPersonagens[indiceSelecionado] && bordaAcesa)
            {
                Color corBorda = (jogadorAtual == 1) ? Color.DarkRed : Color.DeepSkyBlue;
                SolidBrush fundoFiltro = (jogadorAtual == 1) ? fundoVermelho : fundoAzul;
                string textoJogador = (jogadorAtual == 1) ? "P1" : "P2";

                // Desenha a borda
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                    corBorda, 4, ButtonBorderStyle.Solid, corBorda, 4, ButtonBorderStyle.Solid,
                    corBorda, 4, ButtonBorderStyle.Solid, corBorda, 4, ButtonBorderStyle.Solid);

                // Desenha a caixinha com o texto P1 ou P2 no canto superior esquerdo
                e.Graphics.FillRectangle(fundoFiltro, 0, 0, 26, 22);
                e.Graphics.DrawString(textoJogador, fonteTag, corTexto, 2, 0);
            }

            // 2. Mantém a borda e a etiqueta "P1" fixas no personagem que o P1 já escolheu
            if (isMultiplayer && jogadorAtual == 2 && pic == personagemP1)
            {
                // Borda vermelha fixa
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid, Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid, Color.DarkRed, 4, ButtonBorderStyle.Solid);

                // Caixinha vermelha com "P1" fixa
                e.Graphics.FillRectangle(fundoVermelho, 0, 0, 26, 22);
                e.Graphics.DrawString("P1", fonteTag, corTexto, 2, 0);
            }
        }

        private void AtualizarBordaVisual()
        {
            if (listaPersonagens.Count == 0) return;

            foreach (var pic in listaPersonagens)
            {
                pic.Invalidate();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal != null)
            {
                panelPrincipal.Dock = DockStyle.None;
                panelPrincipal.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal.Height) / 2;
                panelPrincipal.Location = new Point(centroX, centroY);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (telaTravada || listaPersonagens.Count == 0) return true;

            if (keyData == Keys.Right || keyData == Keys.D || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaPersonagens.Count) indiceSelecionado = 0; // Se chegou no fim, volta pro começo
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Left || keyData == Keys.A)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaPersonagens.Count - 1; // Se chegou no começo, vai pro fim
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaPersonagens[indiceSelecionado]);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                // Permite usar o botão ESC do teclado para voltar ao Menu
                VoltarParaMenu();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Personagem_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;

            PictureBox clicado = sender as PictureBox;

            // Impede o P2 de clicar no personagem do P1
            if (isMultiplayer && jogadorAtual == 2 && clicado == personagemP1) return;

            indiceSelecionado = listaPersonagens.IndexOf(clicado);
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
            if (selecionado.Name == "picAleatorio")
            {
                ExecutarSorteio();
            }
            // Impede confirmação caso o P2 tente selecionar o personagem do P1
            else if (isMultiplayer && jogadorAtual == 2 && selecionado == personagemP1)
            {
                return;
            }
            else
            {
                // Chama IniciarTransicao para piscar a borda antes de ir pra corrida
                IniciarTransicao(selecionado);
            }
        }

        private void ExecutarSorteio()
        {
            telaTravada = true;

            // Sorteia o vencedor secretamente antes de girar
            Random rnd = new Random();
            do
            {
                int rndIndex = rnd.Next(0, listaPersonagens.Count);
                personagemVencedor = listaPersonagens[rndIndex];
            }
            while (personagemVencedor.Name == "picAleatorio" || (isMultiplayer && jogadorAtual == 2 && personagemVencedor == personagemP1));

            somRoleta.Play();

            cronometroRoleta.Restart();
            timerRoleta.Interval = 40; //velocidade de incio rapida
            timerRoleta.Start();
        }

        private void TimerRoleta_Tick(object sender, EventArgs e)
        {
            passosRestantes--;

            do
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaPersonagens.Count) indiceSelecionado = 0;
            } while (listaPersonagens[indiceSelecionado].Name == "picAleatorio" || (isMultiplayer && jogadorAtual == 2 && listaPersonagens[indiceSelecionado] == personagemP1));

            AtualizarBordaVisual();

            long tempoPassado = cronometroRoleta.ElapsedMilliseconds;

            int distancia = 0;
            int tempIndice = indiceSelecionado;

            while (listaPersonagens[tempIndice] != personagemVencedor)
            {
                tempIndice++;
                if (tempIndice >= listaPersonagens.Count) tempIndice = 0;
                if (listaPersonagens[tempIndice].Name == "picAleatorio" || (isMultiplayer && jogadorAtual == 2 && listaPersonagens[tempIndice] == personagemP1)) continue;
                distancia++;
            }

            if (tempoPassado > 7500)
            {
                if (distancia > 10) timerRoleta.Interval = 40; // Continua voando para tirar o atraso
                else if (distancia > 8) timerRoleta.Interval = 80;  // Faltam 10 casas: começa a frear
                else if (distancia > 6) timerRoleta.Interval = 190;
                else if (distancia > 4) timerRoleta.Interval = 320;
                else if (distancia > 2) timerRoleta.Interval = 550;
                else if (distancia > 0) timerRoleta.Interval = 800; // Faltam 2 casas: quase parando
                else if (distancia == 0)
                {
                    // Chegou exatamente no vencedor perto dos 11 segundos
                    timerRoleta.Stop();
                    cronometroRoleta.Stop();
                    IniciarTransicao(listaPersonagens[indiceSelecionado]);
                }
            }
        }

        private void IniciarTransicao(PictureBox personagemEscolhido)
        {
            telaTravada = true;
            //SOM DE CONFIRMAÇÃO
            // new System.Media.SoundPlayer(@"C:\caminho\som_escolhido.wav").Play();

            int piscadas = 0;
            System.Windows.Forms.Timer timerPiscar = new System.Windows.Forms.Timer();
            timerPiscar.Interval = 150; // A cada 150 milissegundos ela pisca
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

                if (isMultiplayer && jogadorAtual == 1)
                {
                    personagemP1 = personagemEscolhido;
                    jogadorAtual = 2;

                    // Empurra a borda do P2 pra não nascer em cima do P1
                    do
                    {
                        indiceSelecionado++;
                        if (indiceSelecionado >= listaPersonagens.Count) indiceSelecionado = 0;
                    }
                    while (listaPersonagens[indiceSelecionado].Name == "picAleatorio" || listaPersonagens[indiceSelecionado] == personagemP1);

                    telaTravada = false;
                    bordaAcesa = true;
                    AtualizarBordaVisual();
                }
                else
                {
                    // Se for Singleplayer ou o P2 já escolheu, vai pro Cenário!
                    TelaCenarioJogoCorrida jogo = new TelaCenarioJogoCorrida( isMultiplayer);
                    TrocarDeTela(jogo);
                    
                }
            };
            timerEspera.Start();
        }

        private void IniciarCorrida(PictureBox personagemEscolhido)
        {
            TelaCenarioJogoCorrida jogo = new TelaCenarioJogoCorrida(isMultiplayer);
            TrocarDeTela(jogo);
        }

        private void PicSair_Click(object sender, EventArgs e)
        {
            if (telaTravada) return; // Evita que o jogador saia no meio da roleta girando
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