using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Data;
using Exceptions;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="LevelsDataSO")]
    public class LevelsDataSO : ScriptableObject
    {
        public int Count { get { return _levels.Count; } }
        [SerializeField] private string FileName = "levels";
        [SerializeField] private int _maxLevels = 10;
        private List<LevelData> _levels;

        void Awake()
        {
            LoadLevels();
        }

        public LevelData GetLevelData(int index)
        {
            if(_levels is null)
            {
                LoadLevels();
            }
            if(index < 0 || index >= _levels.Count)
            {
                throw new DataException(index, _levels.Count);
            }
            return _levels[index];
        }

        public void SaveLevels()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FileName);
            string json = JsonUtility.ToJson(new LevelsDTO(){ Levels=this._levels });
            using(var writer = new StreamWriter(File.OpenWrite(filePath)))
            {
                writer.Write(json);
            }
        }

        public IEnumerator<LevelData> GetEnumerator()
        {
            foreach(var level in _levels)
            {
                yield return level;
            }
        }

        private void LoadLevels()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FileName);
            string json = "";
            if(File.Exists(filePath))
            {
                using(var reader = new StreamReader(File.OpenRead(filePath)))
                {
                    json = reader.ReadToEnd();
                }
            }
            else
            {
                json = Resources.Load<TextAsset>(FileName).text;
            }
            var levels = JsonUtility.FromJson<LevelsDTO>(json);
            if(levels == null)
            {
                throw new DataException(filePath);
            }
            _levels = levels.Levels;
            if(_levels.Count > _maxLevels)
            {
                _levels.RemoveRange(_maxLevels, _maxLevels - _levels.Count);
            }
        }
    }
}
