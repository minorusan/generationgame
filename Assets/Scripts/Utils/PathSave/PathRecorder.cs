using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.ObjectPooling;
using GamePlay.Player;

namespace Utils.PathSave
{
    public class PathRecorder : MonoBehaviour
    {
        private List<Vector2> _currentPath;
        private Color _currentColor;
        private bool _recordingPath;
        private Transform _player;
        private PlayerColorBehaviour _color;
        private LevelMaps _currentMap;
        public float pathUpdateTime;
        public CurvedLineRenderer pathPrefab;
        public SavedPath pathSaver;
        public bool records = true;
        // Use this for initialization
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _currentMap = pathSaver.maps.FirstOrDefault(m => m.levelName == SceneManager.GetActiveScene().name);
            _currentMap.levelName = SceneManager.GetActiveScene().name;
            _color = FindObjectOfType<PlayerColorBehaviour>();
            DrawAllMaps(_currentMap.maps, true);
        }

        private void Update()
        {
            if (!_recordingPath && _player.gameObject.activeInHierarchy)
            {
                _recordingPath = true;
                StartCoroutine(UpdatePath());
            }
        }

        public void StopRecording()
        {
            records = false;
        }

        private IEnumerator UpdatePath()
        {
            _currentPath = new List<Vector2>(1000);
            _currentColor = _color.activeColor;
            while (_player.gameObject.activeInHierarchy)
            {
                _currentPath.Add(_player.position);
                yield return new WaitForSeconds(pathUpdateTime);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SerializePath()
        {
            var allMaps = _currentMap.maps == null ? new List<GenerationPath>() : _currentMap.maps.ToList();
            allMaps.Add(new GenerationPath { path = _currentPath.ToArray(), color = _currentColor });
            _currentMap.maps = allMaps.ToArray();

            var allLevelMaps = pathSaver.maps.ToList();
            allLevelMaps.Remove(pathSaver.maps.FirstOrDefault(m => m.levelName == SceneManager.GetActiveScene().name));
            allLevelMaps.Add(_currentMap);
            pathSaver.maps = allLevelMaps.ToArray();
            _recordingPath = false;
            DrawAllMaps(_currentMap.maps);
        }

        private void DrawAllMaps(GenerationPath[] paths, bool drawAll = false)
        {
            if (!records)
            {
                return;
            }
            if (drawAll)
            {
                var existing = FindObjectsOfType<CurvedLineRenderer>();
                for (int i = 0; i < existing.Length; i++)
                {
                    Destroy(existing[i].gameObject);
                }
                if (paths == null)
                {
                    return;
                }
                for (int i = 0; i < paths.Length; i++)
                {
                    var newRenderer = Instantiate(pathPrefab, transform) as CurvedLineRenderer;
                    newRenderer.GetComponent<LineRenderer>().endColor = paths[i].color;
                    newRenderer.GetComponent<LineRenderer>().startColor = Color.clear;
                    StartCoroutine(DrawPathPerFrames(paths[i].path, newRenderer));
                }
            }
            else
            {
                var newRenderer = Instantiate(pathPrefab, transform) as CurvedLineRenderer;
                var color = paths[paths.Length - 1].color;
                color.a *= 0.5f;
                newRenderer.GetComponent<LineRenderer>().endColor = color;
                newRenderer.GetComponent<LineRenderer>().startColor = Color.clear;

                StartCoroutine(DrawPathPerFrames(paths[paths.Length - 1].path, newRenderer));
            }

        }

        private IEnumerator DrawPathPerFrames(Vector2[] paths, CurvedLineRenderer newRenderer)
        {
            for (int j = 0; j < paths.Length; j++)
            {
                var point = new Vector3(paths[j].x, paths[j].y, _player.transform.position.z);
                newRenderer.AddPoint(point);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}