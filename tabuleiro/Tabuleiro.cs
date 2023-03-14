using System;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            this.pecas = new Peca[linhas, colunas];
        }

        /// <summary>
        /// Retorna uma peça dentro da matriz
        /// </summary>
        /// <param name="linha">Linha da peça</param>
        /// <param name="coluna">Coluna da peça</param>
        /// <returns></returns>
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        /// <summary>
        /// Retorna uma peça dentro de um tabuleiro
        /// </summary>
        /// <param name="posicao">Posição linha e coluna da peça dentro da matriz</param>
        /// <returns></returns>
        public Peca peca(Posicao posicao)
        {
            return pecas[posicao.linha, posicao.coluna];
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return peca(posicao) != null;   
        }

        public void ColocarPeca(Peca peca, Posicao posicao) 
        {
            if(ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição.");
            }
            pecas[posicao.linha, posicao.coluna] = peca;
            peca.posicao = posicao;
        }

        /// <summary>
        /// Valida uma posição
        /// </summary>
        /// <param name="posicao">Posição para ser validada no tabuleiro</param>
        /// <returns></returns>
        public bool posicaoValida(Posicao posicao)
        {
            if (posicao.linha < 0 || posicao.linha >= linhas || posicao.coluna < 0 || posicao.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if(!posicaoValida(posicao)) {
                throw new TabuleiroException("Posição Inválida!");
            }
        }

        public Peca? RetirarPeca(Posicao posicao)
        {
            if(peca(posicao) == null)
            {
                return null;
            }
            Peca aux = peca(posicao);
            aux.posicao = null;
            pecas[posicao.linha, posicao.coluna] = null;
            return aux;
        }
       
    }
}
