using UnityEngine;
using UserInterface.Elements;

namespace CoreGame.Board
{
    /// <summary>
    /// ScriptableObject containing references to a gameBoard and a gameBuilder producing the first valid state for that board.
    /// </summary>
    [CreateAssetMenu]
    public class LightsOutDefinition : ScriptableObject
    {
        public LOGameBoard gameBoard;
        public LOGameBuilder gameBuilder;
    }
}