using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Interfaces;

namespace UserInterface.Elements
{
    [RequireComponent(typeof(Image))]
    public class LightButton : SelectableGridItem<int>
    {
        [SerializeField] private Sprite ActiveImage;
        [SerializeField] private Sprite InactiveImage;
        
        public override void DisplayActive(bool activeState)
        {
            gameObject.GetComponent<Image>().sprite = activeState ? ActiveImage : InactiveImage;
        }
    }
}
