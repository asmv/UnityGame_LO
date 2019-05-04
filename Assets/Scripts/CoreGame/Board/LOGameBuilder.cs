using UnityEngine;
using Utility;

namespace CoreGame.Board
{
    public class LOGameBuilder
    {
        public LOGameState CreateEmptyState(int[] dimensions)
        {
            return new LOGameState(dimensions);
        }

        public LOGameState CreateSolvableGameState(LOGameBoard gameBoard, int[] dimensions)
        {
            LOGameState loGameState = CreateEmptyState(dimensions);
            do
            {
                for (int i = 0; i < m_randomScrambleMoves; ++i)
                {
                    loGameState = gameBoard.MakeRandomMove(loGameState);
                }
            } while (loGameState.isWon); // if randomization ends up solving the game, re-randomize

            return loGameState;
        }

        [SerializeField] private static int m_randomScrambleMoves = 3;
        
    }
}
