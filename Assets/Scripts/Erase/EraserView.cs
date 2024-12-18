using UnityEngine;

namespace Erase
{
    public class EraserView: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ToggleView(bool state)
        {
            _spriteRenderer.enabled = state;
        }
    }
}