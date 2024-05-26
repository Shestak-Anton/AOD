using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Spawn
{
    public sealed class PrefabProvider : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabs;

        public T GetPrefab<T>()
        {
            foreach (var prefab in _prefabs)
            {
                if (prefab.TryGetComponent<T>(out var component))
                {
                    return component;
                }
            }

            throw new Exception($"Please, define component {typeof(T)}.");
        }

    }
}