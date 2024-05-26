using System.Collections.Generic;

namespace Game.Scripts.Game.Enemy
{
    public sealed class Registry<T>
    {
        private readonly List<T> _entities = new();

        public int RegisteredCount => _entities.Count;

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }
    }
}