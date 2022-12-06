using System.Collections;
using UnityEngine;
using System;

namespace Assets.CodeBase.Logic
{
    public class AggroCheck : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _aggroTime;

        // События для Watcher, чтобы не рушить Dependency Inversion.
        public Action<AggroType> AggroTypeChanged;
        public Action<Transform> TargetFound;
        
        private AggroType _aggroType = AggroType.IDLE;
        private Coroutine _aggroCoroutine;

        private void Awake()
        {
            _triggerObserver.TriggerEnter = EnterAggroZone;
            _triggerObserver.TriggerExit = ExitAggroZone;
        }

        private void EnterAggroZone(Collider collider)
        {
            if (_aggroCoroutine == null && _aggroType == AggroType.IDLE)
            {
                _aggroCoroutine = StartCoroutine(AggroToAttackCoroutine());
                _aggroType = AggroType.AGGRESSIVE;

                TargetFound?.Invoke(collider.transform);
                AggroTypeChanged?.Invoke(_aggroType);
            }
        }

        private void ExitAggroZone(Collider obj)
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }

            _aggroType = AggroType.IDLE;

            TargetFound?.Invoke(null);
            AggroTypeChanged?.Invoke(_aggroType);
        }

        // Время злости
        IEnumerator AggroToAttackCoroutine()
        {
            yield return new WaitForSeconds(_aggroTime);

            _aggroType = AggroType.ATTACK;
            AggroTypeChanged?.Invoke(_aggroType);
            _aggroCoroutine = null;
        }
    }

}
