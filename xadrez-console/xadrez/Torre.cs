using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro Tab, Cor cor) : base(Tab, cor)
        {

        }

        public override string ToString()
        {
            return "T";
        }

    }
}