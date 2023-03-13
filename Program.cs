using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.WriteLine("Digite a posição de origem(ex:a1):");
                    Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();

                    Console.WriteLine("Digite a posição de destino(ex:b2):");
                    Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();

                    partida.executaMovimento(origem, destino);
                }

                Tela.ImprimirTabuleiro(partida.tab);

                //PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);
                //Console.WriteLine(posicaoXadrez);
            }
            catch(TabuleiroException ex) 
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}