using UnityEngine;

namespace ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "new Vector3", menuName = "active time battle/Vector3", order = 0)]
    public class Vector3_SO : ScriptableObject
    {
        public Vector3 vector3;
    }
}