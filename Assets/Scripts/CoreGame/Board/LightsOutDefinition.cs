using UnityEngine;
using UserInterface.Elements;

namespace CoreGame.Board
{
    [CreateAssetMenu]
    public class LightsOutDefinition : GameDefinition<LOGameState, int>
    {
        public LOGameBoard gameBoard;
        public LOGameBuilder gameBuilder;
    }
}