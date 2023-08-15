using UnityEngine;

namespace Domin.Enitiy
{
    public class Hole
    {
        public int Index;
        public Transform Transform;

        public Hole(int index, Transform transform)
        {
            Index = index;
            Transform = transform;
        }
    }
}