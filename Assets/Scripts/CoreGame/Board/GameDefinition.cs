using CoreGame.Board.Interfaces;
using UnityEngine;
using UserInterface.Interfaces;

namespace CoreGame.Board
{
    public abstract class GameDefinition<TGameState, TMoveDataType> : ScriptableObject where TGameState : IGameState
    {
        [SerializeField] public IGameStateDisplay<TGameState> gameStateDisplay;
        [SerializeField] public GameBoard<TGameState, TMoveDataType> boardDefiniton;
    }
}