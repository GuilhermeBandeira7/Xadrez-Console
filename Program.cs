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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.WriteLine("Digite a posição de origem:");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine("Digite a posição de destino:");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(Exception ex) { Console.WriteLine(ex.Message); Console.ReadLine(); }  
                }
                Console.Clear() ;   
                Tela.ImprimirPartida(partida);
            }
            catch(TabuleiroException ex) 
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}