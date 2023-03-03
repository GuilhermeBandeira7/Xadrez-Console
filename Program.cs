using tabuleiro;
using xadrez;
using xadrez_console.xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 1));

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(2, 1));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(2, 2));

                Tela.ImprimirTabuleiro(tab);

                //PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);
                //Console.WriteLine(posicaoXadrez);
            }
            catch(TabuleiroException ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}