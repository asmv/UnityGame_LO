using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.ScriptableObjects
{
    /// <summary>
    /// Utility ScriptableObject storing a reference to an integer.
    /// </summary>
    [CreateAssetMenu]
    public class IntReference : ScriptableObject
    {
        public int value;
    }
}

