﻿using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class BackgroundChanger : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _sprites;

        private void Awake()
        {
            _image.sprite = _sprites.GetRandom();
        }
    }
}