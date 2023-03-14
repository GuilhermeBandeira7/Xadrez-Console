using System;
using tabuleiro;


namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get;private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;

            ColocarPecas();
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);    
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));

            ColocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));
            //ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));

        }

        /// <summary>
        /// Varre a matriz de peças e retorna as peças em jogo de denominada cor.
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }               
            }
            return aux;
        }

        /// <summary>
        /// Abstração do movimento de uma peça no xadrez.
        /// </summary>
        /// <param name="origem">Local atual da peça na matriz do tabuleiro</param>
        /// <param name="destino">Local destino da peça na matriz do tabuleiro</param>
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tab.RetirarPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        /// <summary>
        /// Realiza uma jogada.
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (EstaEmXeque(jogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque.");
            }

            if (EstaEmXeque(Adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            AdversarioEmXequeMate();

            turno++;
            mudaJogador();
        }

        private void AdversarioEmXequeMate()
        {
            if (testeXequemate(Adversaria(jogadorAtual)))
            {
                terminada = true;
            }
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);   
            }
            tab.ColocarPeca(p, origem);

        }

        /// <summary>
        /// Verifica o input da peça escolhida para tratar possíveis erros.
        /// </summary>
        /// <param name="pos"></param>
        /// <exception cref="TabuleiroException"></exception>
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não exise peça na posição de origem escolhida");
            }

            if(jogadorAtual != tab.Peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida. ");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida.");
            }
        }

        /// <summary>
        /// Identifica as peças adversárias.
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

            /// <summary>
        /// Identifica o Rei adversário.
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        /// <summary>
        /// Testa se o Rei está em cheque.
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        /// <exception cref="TabuleiroException"></exception>
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);  
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro.");
            }

            foreach(Peca x  in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }               
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }
    }
}
