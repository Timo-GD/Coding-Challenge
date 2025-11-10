using System.Collections;
using UnityEngine;

namespace CodingChallenge.Items
{
    public class AmmoClip : Item
    {
        private bool _isEmpty;

        public override IEnumerator Using()
        {
            if (transform.parent.GetComponentInChildren<Gun>() == null || _isEmpty)
                yield break;

            transform.parent.GetComponentInChildren<Gun>().Reload();
            _hand.DropItem(true);
            _isEmpty = true;

            yield return new WaitForSeconds(5f);
            Reload();
        }

        /// <summary>
        /// Reloads the magazine;
        /// </summary>
        private void Reload()
        {
            _isEmpty = false;
        }

    }
}
