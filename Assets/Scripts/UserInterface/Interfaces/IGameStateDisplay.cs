using CoreGame.Board.Interfaces;

namespace UserInterface.Interfaces
{
    public interface IGameStateDisplay<TGameState> where TGameState : IGameState
    {
        void Display(TGameState gameState);
    }
}