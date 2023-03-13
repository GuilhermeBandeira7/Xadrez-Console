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
                        Tela.ImprimirTabuleiro(partida.tab);
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.turno);
                        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                        Console.WriteLine();
                        Console.WriteLine("Digite a posição de origem(ex:a1):");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine("Digite a posição de destino(ex:b2):");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(Exception ex) { Console.WriteLine(ex.Message); Console.ReadLine(); }  
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