using System;
using System.Collections;
using UnityEngine;

namespace Ui.Windows.Extra
{
    public class MainMenuShipPreview : MonoBehaviour
    {
        [Header("Positions")]
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _menuPosition;
        [SerializeField] private Transform _endPosition;

        [Header("Ship")]
        [SerializeField] private float _moveTime;
        [SerializeField] private AnimationCurve _moveCurve;
        [SerializeField] private Transform _ship;

        private Coroutine _moveRoutine;
        private Action _callback;

        public void Enter(Action callback = null)
        {
            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _callback = callback;
            _moveRoutine = StartCoroutine(Move(_startPosition, _menuPosition, false));
        }
        
        public void Exit(Action callback = null)
        {
            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _callback = callback;
            _moveRoutine = StartCoroutine(Move(_menuPosition, _endPosition, false));
        }

        private IEnumerator Move(Transform from, Transform to, bool inverseCurve)
        {
            float timer = 0f;
            while (timer < _moveTime)
            {
                timer += Time.deltaTime;
                float t = timer / _moveTime;
                if (inverseCurve) t = 1 - t;

                t = _moveCurve.Evaluate(t);
                
                _ship.position = Vector3.Lerp(from.position, to.position, t);
                yield return null;
            }

            _ship.position = to.position;
            _moveRoutine = null;
            _callback?.Invoke();
        }
    }
}