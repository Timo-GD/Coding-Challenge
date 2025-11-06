using System.Collections;
using UnityEngine;

namespace CodingChallenge.Items
{
    public class Flashlight : Item
    {
        private Light _light;

        public override void Awake()
        {
            base.Awake();
            _light = GetComponentInChildren<Light>();
        }

        public override IEnumerator Using()
        {
            _light.enabled = _light.enabled ? false : true;
            yield break;
        }
    }
}
