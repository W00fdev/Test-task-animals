using Assets.CodeBase.Logic.UI;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    // Немного нарушает SRP для упрощения
    public class Watcher : MonoBehaviour
    {
        [SerializeField] private AggroViewUI _aggroViewUI;
        [SerializeField] private AggroCheck _aggroCheck;

        private Transform _target;
        private AggroType _aggroType;

        private void Start()
        {
            // Подписываемся на события AggroCheck
            _aggroCheck.AggroTypeChanged = SwitchAggroType;
            _aggroCheck.TargetFound = SetTarget;
        }

        private void Update()
        {
            // При любом агрессивном поведении

            if (_aggroType == AggroType.IDLE || _target == null)
                return;

            Vector3 look = (_target.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, look) != 0f)
            {
                // Смотрим на игрока без поворота по осям xz
                transform.rotation = Quaternion.LookRotation(look, Vector3.up);
                transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));
            }
        }

        public void SwitchAggroType(AggroType aggroType)
        {
            if (_aggroType == aggroType)
                return;

            _aggroViewUI.SwitchSprite(aggroType);
            _aggroType = aggroType;
        }

        public void SetTarget(Transform target) 
            => _target = target;
    }

}
