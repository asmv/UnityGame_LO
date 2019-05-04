using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface.Interfaces
{
    public abstract class SelectableGridItem<TContainedValue> : Selectable
    {   
        public event Action<TContainedValue> OnElementSelected;
        
        public abstract void DisplayActive(bool activeState);

        public virtual void SetInteraction(bool activeInteraction)
        {
            interactable = activeInteraction;
        }

        public virtual void SetContainedValue(TContainedValue value)
        {
            m_containedValue = value;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            OnElementSelected?.Invoke(m_containedValue);
        }

        private TContainedValue m_containedValue;
    }
}