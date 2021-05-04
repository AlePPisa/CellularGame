using UnityEngine;


    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class ScriptableCell : ScriptableObject
    {
        public GameObject prefab;
        public int id;
    }
