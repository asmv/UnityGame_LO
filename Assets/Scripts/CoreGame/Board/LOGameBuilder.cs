using UnityEngine;

namespace CoreGame.Board
{
    [CreateAssetMenu]
    public class LOGameBuilder : ScriptableObject
    {
        public LOGameState Create(LOGameBoard gameBoard, GameInitData initData)
        {
            LOGameState loGameState = gameBoard.CreateFirstState(initData);
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
