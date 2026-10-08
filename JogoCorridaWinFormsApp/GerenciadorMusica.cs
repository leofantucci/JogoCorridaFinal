using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JogoCorridaWinFormsApp
{
    internal class GerenciadorMusica
    {
        private static MediaPlayer bgm = new MediaPlayer();
        private static bool tocando = false;

        public static void TocarFundo(string nomeDoArquivoMp3)
        {
            if (tocando) return; // Se já estiver tocando, não faz nada para não reiniciar

            // Pega o caminho dinâmico de onde o jogo está instalado
            string caminho = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeDoArquivoMp3);

            bgm.Open(new Uri(caminho));

            // VOLUME: Vai de 0.0 (mudo) até 1.0 (100%). Aqui está 40%
            bgm.Volume = 0.3;

            // Faz a música voltar pro começo (Loop infinito) quando acabar
            bgm.MediaEnded += (s, e) =>
            {
                bgm.Position = TimeSpan.Zero;
                bgm.Play();
            };

            bgm.Play();
            tocando = true;
        }

        public static void Parar()
        {
            bgm.Stop();
            tocando = false;
        }
    }
}
