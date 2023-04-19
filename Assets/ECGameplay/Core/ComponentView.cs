using UnityEngine;

namespace ECGameplay
{
    public class ComponentView : MonoBehaviour
    {
        public string Type;
        public object Component { get; set; }
    }
}