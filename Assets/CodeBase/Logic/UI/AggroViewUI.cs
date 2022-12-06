using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.Logic.UI
{
    public class AggroViewUI : MonoBehaviour
    {
        [SerializeField] private Image _aggroImage;

        [Space]
        [Header("IDLE/AGGRESSIVE/ATTACK")]
        [SerializeField] private Sprite[] _aggroTypeSprites;

        public void SwitchSprite(AggroType aggroType) 
            => _aggroImage.sprite = _aggroTypeSprites[(int)aggroType];
    }
}
