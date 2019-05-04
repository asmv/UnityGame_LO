using System.Collections.Generic;

namespace CoreGame.Board.Interfaces
{
    /// <summary>
    /// Class that contains the logic for modifying the GameState with moves.
    /// </summary>
    public abstract class GameBoard<TGameState, TMoveDataType> where TGameState : IGameState
    {
        public abstract bool IsWon(TGameState gameState);

        public abstract TGameState MakeMove(TGameState gameState, TMoveDataType move);

        public abstract List<TMoveDataType> GetValidMoves(TGameState gameState, TMoveDataType move);

        public abstract bool IsValidMove(TGameState gameState, TMoveDataType move);
    }
}
