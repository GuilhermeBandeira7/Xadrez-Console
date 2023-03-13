using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    /// <summary>
    /// Interação do usuário com as posições de um tabuleiro de xadrez
    /// </summary>
    internal class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }
       
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        /// <summary>
        /// Converte uma posição da matriz para uma posição de xadrez
        /// </summary>
        /// <returns></returns>
        public Posicao ToPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
