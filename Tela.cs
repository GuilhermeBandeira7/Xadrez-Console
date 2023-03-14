using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a  b  c  d  e  f  g  h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a  b  c  d  e  f  g  h");
            Console.BackgroundColor = fundoOriginal;
        }

        /// <summary>
        /// Identifica a cor da peça que será posicionada
        /// </summary>
        /// <param name="peca">Peça que será adicionada no tabuleiro</param>
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        /// <summary>
        /// Lê a posição de xadrez digitada pelo usuário.
        /// </summary>
        /// <returns>Uma posição de xadrez.</returns>
        internal static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0]; // ch recebe o primeiro caracter 
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);

        }

        internal static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.Write("Peças Capturadas: \n");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();

            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor= ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> pecas)
        {
            Console.Write("[");
            foreach(Peca x in pecas)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
    }
}
