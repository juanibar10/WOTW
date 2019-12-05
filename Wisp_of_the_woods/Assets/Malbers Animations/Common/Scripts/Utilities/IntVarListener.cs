using MalbersAnimations.Scriptables;
using MalbersAnimations.Events;
using UnityEngine;

namespace MalbersAnimations
{
    public class IntVarListener : MonoBehaviour
    {
        public IntVar value;
        public IntEvent Raise = new IntEvent();

        void OnEnable()
        {
            value?.OnValueChanged.AddListener(InvokeInt);
            Raise.Invoke(value ?? 0);
        }

        void OnDisable()
        {
            value?.OnValueChanged.RemoveListener(InvokeInt);
        }

        public virtual void InvokeInt(int value)
        {
            Raise.Invoke(value);
        }
    }
}