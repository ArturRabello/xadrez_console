using System;
using System.Collections.Generic;
using tabuleiro;


namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas= new HashSet<Peca>();

            colocarPecas();
        }


        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            turno++;
            mudaJogador();

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


        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos)== null)
            {
                throw new TabuleiroException("não existe peca na posicao de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peca de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peca de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tab.peca(origem).podeMoverPara(destino)){
                throw new TabuleiroException("Posicao de destino invalida");
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas){
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = null;
            foreach(Peca x in pecas)
            {
                aux.Add(x);
            }
             aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovapeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca , new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovapeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovapeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovapeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovapeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovapeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovapeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
