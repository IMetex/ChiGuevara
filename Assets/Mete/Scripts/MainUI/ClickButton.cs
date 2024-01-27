using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mete.Scripts
{
    public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _ımage;
        [SerializeField] private Sprite _default, _pressed;
        [SerializeField] private AudioClip _compressClip, _uncompressClip;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _ımage.sprite = _pressed;
            _source.PlayOneShot(_compressClip);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _ımage.sprite = _default;
            _source.PlayOneShot(_uncompressClip);
        }
    }
}