using JogoCorrida;
using System.IO;
using System.Media;

namespace JogoCorridaWinFormsApp
{
    public partial class FormJogoCorrida : Form
    {
        private Jogo jogo;

        private readonly bool isMultiplayer;
        private readonly TipoCenario cenarioSelecionado;
        private readonly int nivel;

        private readonly List<PictureBox> pictureBoxes = [];

        private DateTime tempoUltimaMovimentacao;
        private DateTime tempoUltimaPontuacao;
        private DateTime tempoInicioJogo;
        private DateTime horaMostrouExplosao;

        private bool explosaoAtiva = false;
        private bool jogoFinalizado = false;
        private bool fase2Ativada = false;

        private bool jogador1Ativo = true;
        private bool jogador2Ativo = false;

        private bool UpPressed = false;
        private bool DownPressed = false;
        private bool LeftPressed = false;
        private bool RightPressed = false;

        private bool Up2Pressed = false;
        private bool Down2Pressed = false;
        private bool Left2Pressed = false;
        private bool Right2Pressed = false;

        private Label lblPontuacao;
        private Label lblPontuacao2;
        private Label lblFase;
        private Label lblTempo;
        private Label lblControles;

        private Panel painelFim;
        private Label lblFimPontuacao;
        private Label lblFimPontuacao2;
        private Label lblFimRecorde;

        public FormJogoCorrida(bool modoMultiplayer, TipoCenario cenario, int nivel)
        {
            this.isMultiplayer = modoMultiplayer;
            this.cenarioSelecionado = cenario;
            this.nivel = nivel;

            jogador2Ativo = modoMultiplayer;

            DoubleBuffered = true;

            InitializeComponent();

            KeyPreview = true;

            WindowState = FormWindowState.Maximized;

            int largura = Screen.PrimaryScreen.Bounds.Width;
            int altura = Screen.PrimaryScreen.Bounds.Height;

            jogo = new Jogo
            {
                YMaximo = altura,
                InicioXRua = 350,
                FimXRua = 1650,
                PoderAtivo = false,
                PoderX = 0,
                PoderY = -999,
                Poder2Ativo = false,
                Poder2X = 0,
                Poder2Y = -999
            };

            int dificuldade;

            if (nivel == 1)
            {
                jogo.Velocidade = 400;
                dificuldade = 1;
            }
            else if (nivel == 2)
            {
                jogo.Velocidade = 50;
                dificuldade = 2;
            }
            else
            {
                jogo.Velocidade = 1;
                dificuldade = 3;
            }

            jogo.InicioYRua = 10;
            jogo.FimYRua = jogo.YMaximo - 222;

            jogo.IniciaJogo(dificuldade, isMultiplayer);

            picCarro.BackColor = Color.Transparent;
            picCarro.BackgroundImage = Properties.Resources.Novo_Projeto;
            picCarro.BackgroundImageLayout = ImageLayout.Stretch;
            picCarro.Size = new Size(60, 150);
            picCarro.Visible = true;

            picCarro2.BackColor = Color.Transparent;
            picCarro2.BackgroundImage = CriarImagemCarro2();
            picCarro2.BackgroundImageLayout = ImageLayout.Stretch;
            picCarro2.Size = new Size(60, 150);
            picCarro2.Visible = isMultiplayer;

            picPoder.BackColor = Color.Transparent;
            picPoder.BackgroundImage = Properties.Resources.poder;
            picPoder.BackgroundImageLayout = ImageLayout.Stretch;
            picPoder.Size = new Size(30, 60);
            picPoder.Visible = false;

            picPoder2.BackColor = Color.Transparent;
            picPoder2.BackgroundImage = Properties.Resources.poder;
            picPoder2.BackgroundImageLayout = ImageLayout.Stretch;
            picPoder2.Size = new Size(30, 60);
            picPoder2.Visible = false;

            picExplosao.BackColor = Color.Transparent;
            picExplosao.BackgroundImage = Properties.Resources.explosao;
            picExplosao.BackgroundImageLayout = ImageLayout.Stretch;
            picExplosao.Visible = false;

            CarregarImagemCenario();

            CriarHUD();
            CriarObstaculosVisuais();

            tempoUltimaMovimentacao = DateTime.Now;
            tempoUltimaPontuacao = DateTime.Now;
            tempoInicioJogo = DateTime.Now;

            timerJogo.Interval = 30;
            timerJogo.Enabled = true;

            Focus();
        }

        private Image CriarImagemCarro2()
        {
            Bitmap carro2 = new Bitmap(60, 150);

            using (Graphics g = Graphics.FromImage(carro2))
            {
                g.DrawImage(
                    Properties.Resources.picCarro,
                    new Rectangle(0, 0, 60, 150),
                    new Rectangle(97, 14, 166, 331),
                    GraphicsUnit.Pixel
                );
            }

            return carro2;
        }

        private void CriarHUD()
        {
            lblPontuacao = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 22, FontStyle.Bold),
                BackColor = Color.FromArgb(180, Color.Black),
                ForeColor = Color.White,
                Text = "P1: 0 PTS",
                Location = new Point(20, 20)
            };

            lblPontuacao2 = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 22, FontStyle.Bold),
                BackColor = Color.FromArgb(180, Color.Black),
                ForeColor = Color.White,
                Text = "P2: 0 PTS",
                Location = new Point(20, 60)
            };

            lblPontuacao2.Visible = isMultiplayer;

            lblFase = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(180, Color.Black),
                ForeColor = Color.White,
                Text = "FASE 1",
                Location = isMultiplayer ? new Point(20, 100) : new Point(20, 60)
            };

            lblTempo = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(180, Color.Black),
                ForeColor = Color.White,
                Text = "TEMPO: 00:00",
                Location = new Point(Screen.PrimaryScreen.Bounds.Width - 260, 20)
            };

            lblControles = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(160, Color.Black),
                ForeColor = Color.White,
                Text = isMultiplayer
                    ? "P1: WASD + C   |   P2: SETAS + M"
                    : "P1: WASD + C"
            };

            lblControles.Location = new Point(20, Screen.PrimaryScreen.Bounds.Height - 60);

            Controls.Add(lblPontuacao);
            Controls.Add(lblPontuacao2);
            Controls.Add(lblFase);
            Controls.Add(lblTempo);
            Controls.Add(lblControles);

            lblPontuacao.BringToFront();
            lblPontuacao2.BringToFront();
            lblFase.BringToFront();
            lblTempo.BringToFront();
            lblControles.BringToFront();
        }

        private void CriarObstaculosVisuais()
        {
            foreach (var pictureBox in pictureBoxes)
            {
                Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            pictureBoxes.Clear();

            foreach (var ob in jogo.Obstaculos)
            {
                var picOb = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Size = new Size(ob.Largura, ob.Altura),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                AtualizarImagemObstaculo(picOb, ob);

                pictureBoxes.Add(picOb);
                Controls.Add(picOb);

                picOb.BringToFront();
            }

            picCarro.BringToFront();
            picCarro2.BringToFront();
            picPoder.BringToFront();
            picPoder2.BringToFront();
            picExplosao.BringToFront();

            lblPontuacao.BringToFront();
            lblPontuacao2.BringToFront();
            lblFase.BringToFront();
            lblTempo.BringToFront();
            lblControles.BringToFront();
        }

        private void AtualizarImagemObstaculo(PictureBox pictureBox, Elemento obstaculo)
        {
            if (jogo.GetTipoObstaculo(obstaculo) == TipoObstaculo.Cone)
            {
                pictureBox.BackgroundImage = Properties.Resources.cone;
            }
            else
            {
                pictureBox.BackgroundImage = Properties.Resources.carro_batido;
            }

            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void CarregarImagemCenario()
        {
            Image imagemEscolhida;

            if (cenarioSelecionado == TipoCenario.Areas_Rochosas)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario1multgif
                    : Properties.Resources.cenario1gif;
            }
            else if (cenarioSelecionado == TipoCenario.Alem_Do_Mundo)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario2multgif
                    : Properties.Resources.cenario2gif;
            }
            else if (cenarioSelecionado == TipoCenario.Terras_Desconhecidas)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario3multgif
                    : Properties.Resources.cenario3gif;
            }
            else
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario4multgif
                    : Properties.Resources.cenario4gif;
            }

            this.BackgroundImage = imagemEscolhida;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private string CaminhoAudio(string nome)
        {
            return Path.Combine(
                AppContext.BaseDirectory,
                "Audios",
                nome
            );
        }

        private void TocarSomExplosao()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer(
                    CaminhoAudio("explosao.wav")
                );

                sp.Play();
            }
            catch
            {
            }
        }

        private void FormJogoCorrida_KeyDown(object sender, KeyEventArgs e)
        {
            if (jogoFinalizado)
                return;

            if (!jogador1Ativo)
                return;

            if (e.KeyCode == Keys.A)
                LeftPressed = true;

            if (e.KeyCode == Keys.D)
                RightPressed = true;

            if (e.KeyCode == Keys.W)
                UpPressed = true;

            if (e.KeyCode == Keys.S)
                DownPressed = true;

            if (e.KeyCode == Keys.C)
            {
                jogo.LancarPoder();
                e.SuppressKeyPress = true;
            }

            if (isMultiplayer && jogador2Ativo)
            {
                if (e.KeyCode == Keys.Left)
                    Left2Pressed = true;

                if (e.KeyCode == Keys.Right)
                    Right2Pressed = true;

                if (e.KeyCode == Keys.Up)
                    Up2Pressed = true;

                if (e.KeyCode == Keys.Down)
                    Down2Pressed = true;

                if (e.KeyCode == Keys.M)
                {
                    jogo.LancarPoder2();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void FormJogoCorrida_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                LeftPressed = false;

            if (e.KeyCode == Keys.D)
                RightPressed = false;

            if (e.KeyCode == Keys.W)
                UpPressed = false;

            if (e.KeyCode == Keys.S)
                DownPressed = false;

            if (e.KeyCode == Keys.Left)
                Left2Pressed = false;

            if (e.KeyCode == Keys.Right)
                Right2Pressed = false;

            if (e.KeyCode == Keys.Up)
                Up2Pressed = false;

            if (e.KeyCode == Keys.Down)
                Down2Pressed = false;
        }

        private void FormJogoCorrida_Load(object sender, EventArgs e)
        {
            Focus();
        }

        private void timerJogo_Tick(object sender, EventArgs e)
        {
            if (jogoFinalizado)
                return;

            AtualizarPontuacao();

            jogo.AtualizarFase();

            if (jogo.Fase == 2 && !fase2Ativada)
            {
                fase2Ativada = true;

                jogo.InicioXRua = 300;
                jogo.FimXRua = 1550;

                jogo.LimparObstaculos();

                int dificuldade = nivel == 1 ? 1 :
                                  nivel == 2 ? 2 : 3;

                int quantidade = dificuldade switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 15,
                    _ => 5
                };

                jogo.Obstaculos = jogo.FabricaObstaculos(
                    quantidade,
                    300,
                    1000
                );

                AtualizarPictureBoxes();
            }

            AtualizarHUD();

            MovimentarCarroP1();
            MovimentarCarroP2();

            jogo.MovimentaPoder();

            AtualizarPoderes();

            if (jogo.ChecarColisaoPoder())
            {
                AtualizarPictureBoxes();
                MostrarExplosao(1);
            }

            if ((DateTime.Now - tempoUltimaMovimentacao).TotalMilliseconds >= jogo.Velocidade)
            {
                tempoUltimaMovimentacao = DateTime.Now;

                jogo.MovimentaObstaculos();
                AtualizarPictureBoxes();
            }

            if (VerificarColisoes())
                return;

            AtualizarExplosao();
        }

        private void AtualizarPontuacao()
        {
            if ((DateTime.Now - tempoUltimaPontuacao).TotalSeconds >= 2)
            {
                if (jogador1Ativo)
                    jogo.Pontuacao++;

                if (isMultiplayer && jogador2Ativo)
                    jogo.Pontuacao2++;

                tempoUltimaPontuacao = DateTime.Now;
            }
        }

        private void AtualizarHUD()
        {
            TimeSpan tempoDecorrido = DateTime.Now - tempoInicioJogo;

            lblPontuacao.Text = $"P1: {jogo.Pontuacao} PTS";
            lblPontuacao2.Text = $"P2: {jogo.Pontuacao2} PTS";
            lblFase.Text = $"FASE {jogo.Fase}";
            lblTempo.Text = $"TEMPO: {tempoDecorrido.Minutes:D2}:{tempoDecorrido.Seconds:D2}";
        }

        private void MovimentarCarroP1()
        {
            if (!jogador1Ativo)
                return;

            int velocidade = 20;

            if (LeftPressed && jogo.Carro.PosicaoX > jogo.InicioXRua)
                jogo.Carro.PosicaoX -= velocidade;

            if (RightPressed &&
                jogo.Carro.PosicaoX < jogo.FimXRua - jogo.Carro.Largura)
                jogo.Carro.PosicaoX += velocidade;

            if (UpPressed && jogo.Carro.PosicaoY > jogo.InicioYRua)
                jogo.Carro.PosicaoY -= velocidade;

            if (DownPressed &&
                jogo.Carro.PosicaoY < jogo.FimYRua)
                jogo.Carro.PosicaoY += velocidade;

            picCarro.Location = new Point(
                jogo.Carro.PosicaoX,
                jogo.Carro.PosicaoY
            );
        }

        private void MovimentarCarroP2()
        {
            if (!isMultiplayer || !jogador2Ativo)
                return;

            int velocidade = 20;

            if (Left2Pressed && jogo.Carro2.PosicaoX > jogo.InicioXRua)
                jogo.Carro2.PosicaoX -= velocidade;

            if (Right2Pressed &&
                jogo.Carro2.PosicaoX < jogo.FimXRua - jogo.Carro2.Largura)
                jogo.Carro2.PosicaoX += velocidade;

            if (Up2Pressed && jogo.Carro2.PosicaoY > jogo.InicioYRua)
                jogo.Carro2.PosicaoY -= velocidade;

            if (Down2Pressed &&
                jogo.Carro2.PosicaoY < jogo.FimYRua)
                jogo.Carro2.PosicaoY += velocidade;

            picCarro2.Location = new Point(
                jogo.Carro2.PosicaoX,
                jogo.Carro2.PosicaoY
            );
        }

        private void AtualizarPoderes()
        {
            if (jogo.PoderAtivo)
            {
                picPoder.Location = new Point(
                    jogo.PoderX,
                    jogo.PoderY
                );

                picPoder.Visible = true;
            }
            else
            {
                picPoder.Visible = false;
            }

            if (jogo.Poder2Ativo)
            {
                picPoder2.Location = new Point(
                    jogo.Poder2X,
                    jogo.Poder2Y
                );

                picPoder2.Visible = true;
            }
            else
            {
                picPoder2.Visible = false;
            }
        }

        private void AtualizarPictureBoxes()
        {
            CriarObstaculosVisuais();

            for (int i = 0; i < jogo.Obstaculos.Count; i++)
            {
                var ob = jogo.Obstaculos[i];

                if (i >= pictureBoxes.Count)
                    continue;

                pictureBoxes[i].Location = new Point(
                    ob.PosicaoX,
                    ob.PosicaoY
                );
            }
        }

        private bool VerificarColisoes()
        {
            if (jogador1Ativo && jogo.ChecarColisaoCarro())
            {
                jogador1Ativo = false;
                picCarro.Visible = false;
                jogo.PoderAtivo = false;
                jogo.PoderY = -999;
                picPoder.Visible = false;
                MostrarExplosao(0);
            }

            if (isMultiplayer && jogador2Ativo && jogo.ChecarColisaoCarro2())
            {
                jogador2Ativo = false;
                picCarro2.Visible = false;
                jogo.Poder2Ativo = false;
                jogo.Poder2Y = -999;
                picPoder2.Visible = false;
                MostrarExplosao(0);
            }

            if (!jogador1Ativo && !jogador2Ativo)
            {
                GameOver();
                return true;
            }

            return false;
        }

        private void AtualizarExplosao()
        {
            if (!explosaoAtiva)
                return;

            if (DateTime.Now - horaMostrouExplosao >= TimeSpan.FromSeconds(1))
            {
                picExplosao.Visible = false;
                explosaoAtiva = false;
            }
        }

        private void GameOver()
        {
            if (jogoFinalizado)
                return;

            MostrarExplosao(0);

            jogoFinalizado = true;

            timerJogo.Enabled = false;

            SalvarRecorde();

            MostrarTelaFim();
        }

        private void MostrarExplosao(int tipo)
        {
            if (tipo == 0)
            {
                picExplosao.Location = new Point(
                    jogo.BatidaX - 50,
                    jogo.BatidaY - 30
                );
            }
            else
            {
                picExplosao.Location = new Point(
                    jogo.PoderObstaculoX - 20,
                    jogo.PoderObstaculoY - 30
                );
            }

            picExplosao.Size = new Size(160, 160);
            picExplosao.Visible = true;
            picExplosao.BringToFront();

            horaMostrouExplosao = DateTime.Now;
            explosaoAtiva = true;

            TocarSomExplosao();
        }

        private string CaminhoRecorde()
        {
            return Path.Combine(
                AppContext.BaseDirectory,
                "recorde.txt"
            );
        }

        private int LerRecorde()
        {
            try
            {
                string caminho = CaminhoRecorde();

                if (!File.Exists(caminho))
                    return 0;

                string texto = File.ReadAllText(caminho);

                if (int.TryParse(texto, out int recorde))
                    return recorde;
            }
            catch
            {
            }

            return 0;
        }

        private int SalvarRecorde()
        {
            int recorde = LerRecorde();

            int pontos = jogo.Pontuacao;

            if (isMultiplayer && jogo.Pontuacao2 > pontos)
                pontos = jogo.Pontuacao2;

            if (pontos > recorde)
            {
                recorde = pontos;

                try
                {
                    File.WriteAllText(
                        CaminhoRecorde(),
                        recorde.ToString()
                    );
                }
                catch
                {
                }
            }

            jogo.MelhorPontuacao = recorde;

            return recorde;
        }

        private void MostrarTelaFim()
        {
            painelFim = new Panel
            {
                Size = new Size(500, 400),
                BackColor = Color.FromArgb(230, Color.Black)
            };

            painelFim.Left = (ClientSize.Width - painelFim.Width) / 2;
            painelFim.Top = (ClientSize.Height - painelFim.Height) / 2;

            int topo = 30;

            if (isMultiplayer)
            {
                lblFimPontuacao = new Label
                {
                    AutoSize = false,
                    Width = 500,
                    Height = 50,
                    Text = $"P1: {jogo.Pontuacao} PTS",
                    ForeColor = Color.White,
                    Font = new Font("Arial", 24, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Top = topo,
                    Left = 0
                };

                painelFim.Controls.Add(lblFimPontuacao);

                topo += 55;

                lblFimPontuacao2 = new Label
                {
                    AutoSize = false,
                    Width = 500,
                    Height = 50,
                    Text = $"P2: {jogo.Pontuacao2} PTS",
                    ForeColor = Color.White,
                    Font = new Font("Arial", 24, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Top = topo,
                    Left = 0
                };

                painelFim.Controls.Add(lblFimPontuacao2);

                topo += 55;
            }
            else
            {
                lblFimPontuacao = new Label
                {
                    AutoSize = false,
                    Width = 500,
                    Height = 50,
                    Text = $"PONTUAÇÃO: {jogo.Pontuacao}",
                    ForeColor = Color.White,
                    Font = new Font("Arial", 24, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Top = topo,
                    Left = 0
                };

                painelFim.Controls.Add(lblFimPontuacao);

                topo += 55;
            }

            lblFimRecorde = new Label
            {
                AutoSize = false,
                Width = 500,
                Height = 50,
                Text = $"RECORDE: {jogo.MelhorPontuacao}",
                ForeColor = Color.White,
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Top = topo,
                Left = 0
            };

            Button btnReiniciar = new Button
            {
                Text = "REINICIAR",
                Width = 180,
                Height = 60,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Left = 50,
                Top = topo + 60
            };

            Button btnMenu = new Button
            {
                Text = "VOLTAR AO MENU",
                Width = 180,
                Height = 60,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Left = 270,
                Top = topo + 60
            };

            btnReiniciar.Click += BtnReiniciar_Click;
            btnMenu.Click += BtnMenu_Click;

            painelFim.Controls.Add(lblFimRecorde);
            painelFim.Controls.Add(btnReiniciar);
            painelFim.Controls.Add(btnMenu);

            Controls.Add(painelFim);

            painelFim.BringToFront();
        }

        private void BtnReiniciar_Click(object? sender, EventArgs e)
        {
            painelFim?.Dispose();

            jogoFinalizado = false;
            explosaoAtiva = false;
            fase2Ativada = false;

            jogador1Ativo = true;
            jogador2Ativo = isMultiplayer;

            tempoUltimaMovimentacao = DateTime.Now;
            tempoUltimaPontuacao = DateTime.Now;
            tempoInicioJogo = DateTime.Now;

            picExplosao.Visible = false;
            picPoder.Visible = false;
            picPoder2.Visible = false;
            picCarro.Visible = true;
            picCarro2.Visible = isMultiplayer;

            jogo.InicioXRua = 350;
            jogo.FimXRua = 1650;

            jogo.IniciaJogo(
                nivel == 1 ? 1 :
                nivel == 2 ? 2 : 3,
                isMultiplayer
            );

            CriarObstaculosVisuais();

            AtualizarHUD();

            timerJogo.Enabled = true;

            Focus();
        }

        private void BtnMenu_Click(object? sender, EventArgs e)
        {
            timerJogo.Enabled = false;

            PrimeiraTelaJogo menu = new PrimeiraTelaJogo();

            Hide();

            menu.ShowDialog();

            Close();
        }
    }
}