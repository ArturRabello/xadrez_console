using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro Tab, Cor cor) : base(Tab, cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }

    }
}
