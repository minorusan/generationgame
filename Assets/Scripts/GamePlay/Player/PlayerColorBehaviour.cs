using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerColorBehaviour : MonoBehaviour
    {
        private Material _instance;

        public Color activeColor
        {
            get
            {
                return settings.activeColor;
            }
        }
        public PlayerColorSettings settings;

        private void Awake()
        {

            ApplyActiveColor();
        }

        public void ChangeColor()
        {
            settings.activeColor = settings.colors[Random.Range(0, settings.colors.Length)];
            ApplyActiveColor();
        }

        private void ApplyActiveColor()
        {
            _instance = FindObjectOfType<TrailRenderer>().material;
            var material = settings.playerMaterial;
            material.SetColor("_EmissionColor", settings.activeColor);
            material.SetColor("_Color", settings.activeColor);
            _instance.SetColor("_EmissionColor", settings.activeColor);
            _instance.SetColor("_Color", settings.activeColor);

        }
    }
}