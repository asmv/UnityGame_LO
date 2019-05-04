using UnityEngine;

namespace CoreGame.Board
{
    /// <summary>
    /// ScriptableObject that builds the first viable state for the game, given the board and initialization data.
    /// </summary>
    [CreateAssetMenu]
    public class LOGameBuilder : ScriptableObject
    {
        public LOGameState Create(LOGameBoard gameBoard, GameInitData initData)
        {
            LOGameState loGameState = gameBoard.CreateFirstState(initData);
            do
            {
                for (int i = 0; i < initData.randomShuffleFactor; ++i)
                {
                    loGameState = gameBoard.MakeRandomMove(loGameState);
                }
            } while (loGameState.isWon); // if randomization ends up solving the game, re-randomize

            return loGameState;
        }        
    }
}
