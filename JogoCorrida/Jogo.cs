using System.Drawing;

namespace JogoCorrida
{
    public enum TipoObstaculo
    {
        Cone,
        CarroBatido
    }

    public class Jogo
    {
        public Elemento Carro { get; set; }
        public Elemento Carro2 { get; set; }
        public List<Elemento> Obstaculos { get; set; }

        public int Velocidade { get; set; }
        public int Pontuacao { get; set; }
        public int Pontuacao2 { get; set; }
        public int Tempo { get; set; }
        public int MelhorPontuacao { get; set; }
        public int ColisoesPermitidas { get; set; }

        public int YMaximo { get; set; }
        public int InicioXRua { get; set; }
        public int FimXRua { get; set; }
        public int InicioYRua { get; set; }
        public int FimYRua { get; set; }

        public int BatidaX { get; set; }
        public int BatidaY { get; set; }

        public int ObstaculoBatidaX { get; set; }
        public int ObstaculoBatidaY { get; set; }

        public int PoderX { get; set; }
        public int PoderY { get; set; }
        public bool PoderAtivo { get; set; }

        public int Poder2X { get; set; }
        public int Poder2Y { get; set; }
        public bool Poder2Ativo { get; set; }

        public int PoderObstaculoX { get; set; }
        public int PoderObstaculoY { get; set; }

        public int Fase { get; set; }

        private readonly Dictionary<Elemento, TipoObstaculo> tiposObstaculos = new();
        private readonly Random rnd = new();

        public void IniciaJogo(int key, bool isMultiplayer = false)
        {
            Carro = new Elemento
            {
                Tipo = TipoElemento.Carro,
                PosicaoX = PosicionaObjeto(1),
                PosicaoY = YMaximo - 222,
                Largura = 60,
                Altura = 150
            };

            Pontuacao = 0;
            Pontuacao2 = 0;
            Tempo = 0;
            Fase = 1;

            PoderAtivo = false;
            PoderX = Carro.PosicaoX;
            PoderY = -999;

            PoderObstaculoX = -999;
            PoderObstaculoY = -999;

            Poder2Ativo = false;
            Poder2X = 0;
            Poder2Y = -999;

            if (isMultiplayer)
            {
                Carro.PosicaoX -= 90;

                Carro2 = new Elemento
                {
                    Tipo = TipoElemento.Carro,
                    PosicaoX = PosicionaObjeto(1) + 90,
                    PosicaoY = YMaximo - 222,
                    Largura = 60,
                    Altura = 150
                };

                if (Carro2.PosicaoX + Carro2.Largura > FimXRua)
                    Carro2.PosicaoX = FimXRua - Carro2.Largura;

                Poder2X = Carro2.PosicaoX;
            }

            int qtdObj = key switch
            {
                1 => 5,
                2 => 10,
                3 => 15,
                _ => 5
            };

            Obstaculos = FabricaObstaculos(qtdObj, 300, 1000);
        }

        public List<Elemento> FabricaObstaculos(int qtd, int dmin, int dmax)
        {
            var obstaculos = new List<Elemento>();

            int ultimoY = 0;

            for (int i = 0; i < qtd; i++)
            {
                int distancia = rnd.Next(dmin, dmax + 1);

                int y;

                if (i == 0)
                {
                    y = 0;
                }
                else
                {
                    y = ultimoY - distancia;
                }

                var ob = new Elemento
                {
                    Tipo = TipoElemento.Obstaculo,
                    PosicaoX = PosicionaObjeto(9),
                    Largura = 60,
                    Altura = 50,
                    PosicaoY = y
                };

                TipoObstaculo tipo;

                if (rnd.Next(2) == 0)
                    tipo = TipoObstaculo.Cone;
                else
                    tipo = TipoObstaculo.CarroBatido;

                tiposObstaculos[ob] = tipo;

                obstaculos.Add(ob);

                ultimoY = y;
            }

            return obstaculos;
        }

        public TipoObstaculo GetTipoObstaculo(Elemento obstaculo)
        {
            if (tiposObstaculos.TryGetValue(obstaculo, out var tipo))
                return tipo;

            return TipoObstaculo.Cone;
        }

        public void LimparObstaculos()
        {
            if (Obstaculos != null)
            {
                foreach (var ob in Obstaculos)
                    tiposObstaculos.Remove(ob);

                Obstaculos.Clear();
            }
        }

        public int PosicionaObjeto(int key)
        {
            if (key == 1)
            {
                return InicioXRua + ((FimXRua - InicioXRua) / 2);
            }

            if (key == 9)
            {
                int larguraObjeto = 60;

                int minimo = InicioXRua;
                int maximo = FimXRua - larguraObjeto;

                if (maximo <= minimo)
                    return minimo;

                return rnd.Next(minimo, maximo + 1);
            }

            return InicioXRua + 10;
        }

        public bool ChecarColisaoCarro()
        {
            return ChecarColisaoCarro(Carro);
        }

        public bool ChecarColisaoCarro2()
        {
            if (Carro2 == null)
                return false;

            return ChecarColisaoCarro(Carro2);
        }

        private bool ChecarColisaoCarro(Elemento carroEscolhido)
        {
            Rectangle carro = new Rectangle(
                carroEscolhido.PosicaoX,
                carroEscolhido.PosicaoY,
                carroEscolhido.Largura,
                carroEscolhido.Altura
            );

            foreach (var ob in Obstaculos)
            {
                Rectangle obstaculo = new Rectangle(
                    ob.PosicaoX,
                    ob.PosicaoY,
                    ob.Largura,
                    ob.Altura
                );

                if (carro.IntersectsWith(obstaculo))
                {
                    ObstaculoBatidaX = ob.PosicaoX;
                    ObstaculoBatidaY = ob.PosicaoY;

                    BatidaX = carroEscolhido.PosicaoX;
                    BatidaY = carroEscolhido.PosicaoY;

                    return true;
                }
            }

            return false;
        }

        public bool ChecarColisaoPoder()
        {
            bool acertou = false;

            for (int i = Obstaculos.Count - 1; i >= 0; i--)
            {
                var ob = Obstaculos[i];

                Rectangle obstaculo = new Rectangle(
                    ob.PosicaoX,
                    ob.PosicaoY,
                    ob.Largura,
                    ob.Altura
                );

                if (PoderAtivo)
                {
                    Rectangle poder = new Rectangle(
                        PoderX,
                        PoderY,
                        30,
                        60
                    );

                    if (poder.IntersectsWith(obstaculo))
                    {
                        PoderObstaculoX = ob.PosicaoX;
                        PoderObstaculoY = ob.PosicaoY;

                        tiposObstaculos.Remove(ob);
                        Obstaculos.RemoveAt(i);

                        PoderAtivo = false;
                        PoderY = -999;

                        acertou = true;
                        continue;
                    }
                }

                if (Poder2Ativo)
                {
                    Rectangle poder2 = new Rectangle(
                        Poder2X,
                        Poder2Y,
                        30,
                        60
                    );

                    if (poder2.IntersectsWith(obstaculo))
                    {
                        PoderObstaculoX = ob.PosicaoX;
                        PoderObstaculoY = ob.PosicaoY;

                        tiposObstaculos.Remove(ob);
                        Obstaculos.RemoveAt(i);

                        Poder2Ativo = false;
                        Poder2Y = -999;

                        acertou = true;
                    }
                }
            }

            return acertou;
        }

        public void MovimentaObstaculos()
        {
            foreach (var ob in Obstaculos)
            {
                ob.PosicaoY += 25;

                if (ob.PosicaoY > YMaximo)
                {
                    ob.PosicaoY = 0;
                    ob.PosicaoX = PosicionaObjeto(9);
                }
            }
        }

        public void MovimentaPoder()
        {
            if (PoderAtivo)
            {
                PoderY -= 25;

                if (PoderY < -100)
                {
                    PoderY = -999;
                    PoderAtivo = false;
                }
            }

            if (Poder2Ativo)
            {
                Poder2Y -= 25;

                if (Poder2Y < -100)
                {
                    Poder2Y = -999;
                    Poder2Ativo = false;
                }
            }
        }

        public void LancarPoder()
        {
            if (PoderAtivo)
                return;

            PoderX = Carro.PosicaoX + (Carro.Largura / 2) - 15;
            PoderY = Carro.PosicaoY - 60;
            PoderAtivo = true;
        }

        public void LancarPoder2()
        {
            if (Carro2 == null)
                return;

            if (Poder2Ativo)
                return;

            Poder2X = Carro2.PosicaoX + (Carro2.Largura / 2) - 15;
            Poder2Y = Carro2.PosicaoY - 60;
            Poder2Ativo = true;
        }

        public void AtualizarFase()
        {
            int pontos = Pontuacao > Pontuacao2 ? Pontuacao : Pontuacao2;

            if (pontos >= 10)
                Fase = 2;
            else
                Fase = 1;
        }
    }

}
