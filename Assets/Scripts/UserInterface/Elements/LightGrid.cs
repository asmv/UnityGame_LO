﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreGame.Board;
using CoreGame.Board.Interfaces;
using CoreGame.Management;
using UnityEngine;
using UnityEngine.EventSystems;
using UserInterface.Interfaces;

namespace UserInterface.Elements
{
    public class LightGrid : UIBehaviour, IGameStateDisplay<LOGameState>
    {
        [SerializeField] private Transform m_contentRootTransform;
        [SerializeField] private Transform m_horizontalRowPrefab;
        [SerializeField] private LightButton m_selectableGridItemPrefab;

        public void Display(LOGameState gameState)
        {
            if (m_currentBoardDimensions != null && m_currentBoardDimensions.SequenceEqual(gameState.dimensions))
            {
                Refresh(gameState);
            }
            else
            {
                Clear();
                InstantiateElements(gameState);
                m_currentBoardDimensions = gameState.dimensions;
            }
        }

        private void InstantiateElements(LOGameState gameState)
        {
            if (gameState.dimensions.Length != 2)
            {
                throw new NotSupportedException($"{gameState.dimensions.Length} dimensions are not supported by this layout.");
            }

            for (int i = 0; i < gameState.dimensions[1]; ++i)
            {
                var horizontalRow = Instantiate(m_horizontalRowPrefab, m_contentRootTransform);
                m_horizontalRows.Add(horizontalRow);
                for (int j = 0; j < gameState.dimensions[0]; ++j)
                {
                    var lightButton = Instantiate(m_selectableGridItemPrefab, horizontalRow);
                    var buttonValue = i * gameState.dimensions[0] + j;
                    lightButton.SetContainedValue(buttonValue);
                    lightButton.DisplayActive(gameState.boardState[buttonValue]);
                    lightButton.OnElementSelected += HandleElementSelected;
                    m_childGridItems.Add(lightButton);
                }
            }
        }

        public void Refresh(LOGameState gameState)
        {
            // Leaving this optimization unused in favor of a more cautious implementation (if GameManager skips a step without sending an event, this will still be accurate)
//            foreach (int i in gameState.lastSpacesToggled)
//            {
//                m_childGridItems[i].DisplayActive(gameState.boardState[i]);
//            }
            for (int i = 0; i < gameState.boardState.Length; ++i)
            {
                m_childGridItems[i].DisplayActive(gameState.boardState[i]);
            }
        }
        
        public void Clear()
        {
            for (int i = m_childGridItems.Count-1; i >= 0; --i)
            {
                m_childGridItems[i].OnElementSelected -= HandleElementSelected;
                Destroy(m_childGridItems[i].gameObject);
            }
            m_childGridItems.Clear();

            foreach (Transform horizontalRow in m_horizontalRows)
            {
                Destroy(horizontalRow.gameObject);
            }
            m_horizontalRows.Clear();
        }
        
        private void HandleGameManagerStateChange(GameManagerState gameManagerState)
        {
            if (gameManagerState == GameManagerState.ActivePlay)
            {
                SetAllInteraction(true);
            }

            if (gameManagerState == GameManagerState.ResultsDisplay)
            {
                SetAllInteraction(false);
            }
        }

        private void SetAllInteraction(bool buttonEnableState)
        {
            m_childGridItems.ForEach(button => button.SetInteraction(buttonEnableState));
        }

        private void Awake()
        {
            GameManager.Instance.OnGameManagerStateChanged += HandleGameManagerStateChange;
            GameManager.Instance.OnGameStateChanged += Display;
        }

        private void HandleElementSelected(int element)
        {
            GameManager.Instance.HandleUserInteraction(element);
        }

        private void OnDestroy()
        {
            Clear();
            GameManager.Instance.OnGameManagerStateChanged -= HandleGameManagerStateChange;
            GameManager.Instance.OnGameStateChanged -= Display;
        }

        private int[] m_currentBoardDimensions;
        private List<Transform> m_horizontalRows = new List<Transform>();
        private List<LightButton> m_childGridItems = new List<LightButton>();
    }
}
