using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface.Interfaces
{
    public abstract class SelectableGridItem<TContainedValue> : Selectable
    {   
        public event Action<TContainedValue> OnElementSelected;
        
        public abstract void DisplayActive(bool activeState);

        /// <summary>
        /// Sets the interactivity of the element to the given value.
        /// </summary>
        /// <param name="activeInteraction"><c>false</c> if player interaction should be disabled, <c>true</c> otherwise.</param>
        public virtual void SetInteraction(bool activeInteraction)
        {
            m_isEnabled = activeInteraction;
        }

        /// <summary>
        /// Sets the internal value of this selectable item to the given value.
        /// </summary>
        /// <param name="value">The value to store within this selectable item.</param>
        public virtual void SetContainedValue(TContainedValue value)
        {
            m_containedValue = value;
        }

        /// <summary>
        /// Function reacting to mousePointerUp commands for selecting items.
        /// </summary>
        /// <param name="eventData">Pointer event data. Unused for this function.</param>
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (m_isEnabled)
            {
                OnElementSelected?.Invoke(m_containedValue);
            }
        }

        private bool m_isEnabled = true;
        private TContainedValue m_containedValue;
    }
}