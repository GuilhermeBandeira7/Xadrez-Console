using System;

namespace tabuleiro
{
    internal abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; } //Apenas modificada por subclasses
        public int qteMovimentos { get;protected set; }
        public Tabuleiro tab { get;protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            qteMovimentos = 0;
        }

        public void IncrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public void DecrementarQteMovimentos()
        {
            qteMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat =  MovimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.linha, posicao.coluna];
        }

        /// <summary>
        /// classe para checar os possíveis movimentos de uma peça.
        /// </summary>
        /// <returns>Matriz de booleanos indicando os locais onde a peça pode se deslocar.</returns>
        public abstract bool[,] MovimentosPossiveis();
    }
}
