using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Common
{
    public interface IMovable
    {
        IAtomicVariable<Vector3> Direction { get; }
    }
}