using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace.Enemy
{
    [CreateAssetMenu(menuName = "TestSina", fileName = "NewSina")]
    public class Test : ScriptableObject
    {
        [SerializeField] List<Dialog> dialogs = new List<Dialog>();
    }


    [Serializable]
    public class Dialog
    {
        public string message;
    }
}