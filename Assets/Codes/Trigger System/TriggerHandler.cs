using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class TriggerHandler : MonoBehaviour
    {
        [Header("Trigger Event")]
        [TypeFilter(typeof(ITriggerStrategy))]
        [SerializeField] SerializableType _triggerEvent;
    }
}