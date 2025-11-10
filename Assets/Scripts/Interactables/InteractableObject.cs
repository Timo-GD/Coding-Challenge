using UnityEngine;

namespace CodingChallenge.Interactable
{
    public class InteractableObject : MonoBehaviour
    {
        /// <summary>
        /// The general Use function that all InteractableObjects inherit;
        /// </summary>
        /// <returns></returns>
        public virtual bool Use()
        {
            return true;
        }
    }
}
