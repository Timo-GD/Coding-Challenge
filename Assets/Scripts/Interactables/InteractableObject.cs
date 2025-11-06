using UnityEngine;

namespace CodingChallenge.Interactable
{
    public class InteractableObject : MonoBehaviour
    {
        public virtual bool Use()
        {
            return true;
        }
    }
}
