using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class AnimationDispatcher : MonoBehaviour
    {
        private readonly Dictionary<string, List<Action>> _actionsDictionary = new();

        public void SubscribeOnEvent(string key, Action action)
        {
            if (!_actionsDictionary.ContainsKey(key))
            {
                _actionsDictionary[key] = new List<Action>();
            }

            _actionsDictionary[key].Add(action);
        }

        public void UnsubscribeOnEvent(string key, Action action)
        {
            if (_actionsDictionary.TryGetValue(key, out var actionsList))
            {
                actionsList.Remove(action);
            }
        }
        
        public void ReceiveString(string actionKey)
        {
            if (!_actionsDictionary.TryGetValue(actionKey, out var actionsList)) return;

            for (var index = 0; index < actionsList.Count; index++)
            {
                var action = actionsList[index];
                action?.Invoke();
            }
        }
    }
}