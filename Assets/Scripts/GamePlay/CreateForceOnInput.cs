using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class CreateForceOnInput : MonoBehaviour
    {
        public static event Action released;

        private LineRenderer _renderer;
        private bool _getsForce;
        private Rigidbody2D _player;

        public float forceMultiplier;
        public float textureSize;

        private void Awake()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            _renderer = GetComponentInChildren<LineRenderer>();
        }

        public void Reset()
        {
            _getsForce = false;
            _player.velocity = Vector2.zero;
        }

        private void OnMouseDown()
        {
            _getsForce = true;
            _renderer.enabled = true;
            var position = GetWorldPositionOnPlane(Input.mousePosition, _renderer.transform.position.z);
            _renderer.SetPosition(1, position);
            _renderer.startColor = _player.GetComponentInChildren<TrailRenderer>().material.color;
        }

        private void OnMouseUp()
        {
            if (!_player.gameObject.activeInHierarchy)
            {
                Reset();
            }
            if (_getsForce)
            {
                _getsForce = false;
                _player.velocity = Vector2.zero;
                var distance = Vector2.Distance(_renderer.GetPosition(0), _renderer.GetPosition(1));
                var direction = _renderer.GetPosition(0) - _renderer.GetPosition(1);
                _player.AddForce(direction.normalized * (distance * forceMultiplier), ForceMode2D.Impulse);
                _renderer.enabled = false;
                if (released != null)
                {
                    released();
                }
            }
        }

        private void LateUpdate()
        {
            if (_getsForce)
            {
                var position = GetWorldPositionOnPlane(Input.mousePosition, _renderer.transform.position.z);
                _renderer.SetPosition(0, position);
                var distance = Vector2.Distance(_renderer.GetPosition(0), _renderer.GetPosition(1));
                _renderer.material.SetInt("_RepeatCount", Mathf.RoundToInt(distance * textureSize));
            }

            if (!_player.gameObject.activeInHierarchy)
            {
                Reset();
            }
        }

        public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }
    }
}
