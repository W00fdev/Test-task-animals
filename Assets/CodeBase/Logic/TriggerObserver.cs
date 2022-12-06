using System;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public Action<Collider> TriggerEnter;
        public Action<Collider> TriggerExit;

        // Физические слои настроены только на игрока
        private void OnTriggerEnter(Collider collider)
            => TriggerEnter?.Invoke(collider);

        private void OnTriggerExit(Collider collider)
            => TriggerExit?.Invoke(collider);
    }

}
