using UnityEngine;

namespace CardGame.Cameras
{
    public class CameraSceneObject : MonoBehaviour
    {
        public Camera Main => _main;
        [SerializeField] private Camera _main;
    }
}