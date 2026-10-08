using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace JogoCorridaWinFormsApp
{
    
    public partial class TelaEscolhaJogoCorrida : Form
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

        List<PictureBox> listaBotoes = new List<PictureBox>();
        int indiceSelecionado = 0;

        System.Media.SoundPlayer somClick = new System.Media.SoundPlayer(Properties.Resources.som_click);

        public TelaEscolhaJogoCorrida()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.Load += TelaEscolhaJogoCorrida_Load;
        }

        private void TelaEscolhaJogoCorrida_Load(object sender, EventArgs e)
        {
            somClick.LoadAsync();

            if (panelPrincipal5 != null)
            {
                foreach (Control controle in panelPrincipal5.Controls)
                {
                    if (controle is PictureBox pic)
                    {
                        if (pic.Name == "picLogo" || pic.Name == "picModoDeJogo" || pic.Name == "picSingle" || pic.Name == "picMulti" || (pic.Image == null && pic.BackgroundImage == null))
                        {
                            continue;
                        }

                        pic.BorderStyle = BorderStyle.None;
                        pic.Paint += Botao_Paint;
                        pic.Click += Botao_Click;
                        listaBotoes.Add(pic);
                    }
                }
                listaBotoes = listaBotoes.OrderBy(p => p.Left).ToList();
            }
            AtualizarBordaVisual();
        }

        private void Botao_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (listaBotoes.Count > 0 && pic == listaBotoes[indiceSelecionado])
            {
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle, Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid, Color.DarkRed, 4, ButtonBorderStyle.Solid, Color.DarkRed, 4, ButtonBorderStyle.Solid);
            }
        }

        private void AtualizarBordaVisual()
        {
            if (listaBotoes.Count == 0) return;
            foreach (var pic in listaBotoes) pic.Invalidate();
        }
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal5 != null)
            {
                panelPrincipal5.Dock = DockStyle.None;
                panelPrincipal5.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal5.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal5.Height) / 2;
                panelPrincipal5.Location = new Point(centroX, centroY);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (listaBotoes.Count == 0) return base.ProcessCmdKey(ref msg, keyData);

            if (keyData == Keys.Right || keyData == Keys.D || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaBotoes.Count) indiceSelecionado = 0;
                AtualizarBordaVisual(); return true;
            }
            else if (keyData == Keys.Left || keyData == Keys.A)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaBotoes.Count - 1;
                AtualizarBordaVisual(); return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaBotoes[indiceSelecionado]); return true;
            }
            else if (keyData == Keys.Escape)
            {
                VoltarParaMenu(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Botao_Click(object sender, EventArgs e)
        {
            PictureBox clicado = sender as PictureBox;
            if (clicado.Name == "picSingle") ConfirmarSelecao(panelPrincipal5.Controls["picSingleplayer"] as PictureBox);
            else if (clicado.Name == "picMulti") ConfirmarSelecao(panelPrincipal5.Controls["picMultiplayer"] as PictureBox);
            else
            {
                indiceSelecionado = listaBotoes.IndexOf(clicado);
                AtualizarBordaVisual();
                ConfirmarSelecao(clicado);
                somClick.Play();

            }
        }
        private void ConfirmarSelecao(PictureBox selecionado)
        {
            if (selecionado.Name == "picSingmsg")
            {
                TelaInicioJogoCorrida telaSelecao = new TelaInicioJogoCorrida(false);
                TrocarDeTela(telaSelecao);
            }
            else if (selecionado.Name == "picMultimsg")
            {
                TelaInicioJogoCorrida telaSelecao = new TelaInicioJogoCorrida(true);
                TrocarDeTela(telaSelecao);
            }
            else if (selecionado.Name == "picSair")
            {
                VoltarParaMenu();
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
