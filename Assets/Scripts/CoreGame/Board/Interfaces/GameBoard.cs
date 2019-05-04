using System.Collections.Generic;
using Utility;

namespace CoreGame.Board.Interfaces
{
    /// <summary>
    /// Class that contains the logic for modifying the GameState with moves.
    /// </summary>
    public abstract class GameBoard<TGameState, TMoveDataType> where TGameState : IGameState
    {
        /// <summary>
        /// Returns a boolean value indicating whether or not the game state marks the end of the game.
        /// </summary>
        /// <param name="gameState">The game state to assess.</param>
        /// <returns><c>true</c> if the state marks the game's end, <c>false</c> otherwise.</returns>
        public abstract bool IsWon(TGameState gameState);

        /// <summary>
        /// Returns a new <see cref="TGameState"/> representing the state of the game after the given <paramref name="move"/> is performed.
        /// </summary>
        /// <param name="gameState">A <see cref="TGameState"/> representing the state of the game prior to making the move.</param>
        /// <param name="move">A <see cref="TMoveDataType"/> representing a valid move for the given <see cref="GameBoard{TGameState,TMoveDataType}"/>.</param>
        /// <returns>A new <see cref="TGameState"/> representing the state of the game after the move is made and <c>null</c> if the move was invalid.</returns>
        public abstract TGameState MakeMove(TGameState gameState, TMoveDataType move);

        /// <summary>
        /// Gets a list of valid moves to perform on the provided <paramref name="gameState"/>.
        /// </summary>
        /// <param name="gameState">The state of the game from which to determine available moves.</param>
        /// <returns>A <see cref="List{T}"/> of the valid moves for the given state. Note that the amount of returned moves may be 0.</returns>
        public abstract List<TMoveDataType> GetValidMoves(TGameState gameState);

        /// <summary>
        /// Checks if the given move is a valid one for the state.
        /// </summary>
        /// <param name="gameState">The <see cref="TGameState"/> against which to compare the provided <paramref name="move"/>.</param>
        /// <param name="move">The move to check validity for.</param>
        /// <returns><c>true</c> if the move is valid for the given board, <c>false</c> otherwise.</returns>
        public virtual bool IsValidMove(TGameState gameState, TMoveDataType move)
        {
            return GetValidMoves(gameState).Contains(move);
        }

        /// <summary>
        /// Chooses a random move and returns the state resulting from taking that move.
        /// </summary>
        /// <param name="gameState">A <see cref="TGameState"/> representing the state of the game prior to making the move.</param>
        /// <returns>A new <see cref="TGameState"/> representing the state of the game after the move is made and <c>null</c> if there are no available moves.</returns>
        public virtual TGameState MakeRandomMove(TGameState gameState)
        {
            var validMoves = GetValidMoves(gameState);
            if (validMoves.Count == 0) return null;
            return MakeMove(gameState, validMoves.Choice());
        }
    }
}
