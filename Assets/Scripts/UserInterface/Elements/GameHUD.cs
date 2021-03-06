﻿using System;
using CoreGame.Board;
using CoreGame.Board.Interfaces;
using CoreGame.Management;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UserInterface.Interfaces;
using Utility.ScriptableObjects;

namespace UserInterface.Elements
{
    public class GameHUD : UIBehaviour, IGameInfoDisplay
    {
        [SerializeField] private Text m_movesText;
        [SerializeField] private Text m_timerText;
        [SerializeField] private Text m_resetButtonText;
        [SerializeField] private IntReference m_movesMade;
        [SerializeField] private Text m_winText;
        
        /// <summary>
        /// Formats and sets the text attribute of the moves UI element.
        /// </summary>
        /// <param name="moves"></param>
        public void DisplayMoves(int moves)
        {
            m_movesText.text = $"Moves: {moves}";
        }

        /// <summary>
        /// Resets the HUD to its default values.
        /// </summary>
        public void ResetHUD()
        {
            m_movesText.text = "Moves: 0";
            m_timerText.text = "00:00";
            m_resetButtonText.text = "Reset";
            m_startTime = DateTime.Now;
            m_movesMade.value = 0;
            m_winText.gameObject.SetActive(false);
        }

        private void HandleGameManagerStateChange(GameManagerState gameManagerState)
        {
            if (gameManagerState == GameManagerState.ActivePlay)
            {
                ResetHUD();
                m_doUpdateTimer = true;
            }

            if (gameManagerState == GameManagerState.ResultsDisplay)
            {
                m_resetButtonText.text = "Play Again";
                m_doUpdateTimer = false;
                m_winText.gameObject.SetActive(true);
            }
        }

        private void HandleGameStateChange(IGameState gameState)
        {
            DisplayMoves(m_movesMade.value);
        }
        
        private void Start()
        {
            ResetHUD();
            GameManager.Instance.OnGameManagerStateChanged += HandleGameManagerStateChange;
            GameManager.Instance.OnGameStateChanged += HandleGameStateChange;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameManagerStateChanged -= HandleGameManagerStateChange;
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChange;
        }

        private void Update()
        {
            if (m_doUpdateTimer)
            {
                m_timerText.text = (DateTime.Now - m_startTime).ToString(@"mm\:ss");
            }
        }


        private DateTime m_startTime;
        private bool m_doUpdateTimer = true;
    }
}
