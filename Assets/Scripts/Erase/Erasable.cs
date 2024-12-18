using UnityEngine;

namespace Erase
{
    public struct BoundsWorldCoord
    {
        public Vector2 boundLeftDown;
        public Vector2 boundRightUp;
    }
    [RequireComponent(typeof(SpriteRenderer))]
    public class Erasable : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BoundsWorldCoord _bounds;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var tex = GenerateTexture(_spriteRenderer.sprite.texture);
            _spriteRenderer.sprite = Sprite.Create(tex,_spriteRenderer.sprite.rect,new Vector2(0.5f,0.5f));
            CalculateBounds(_spriteRenderer.sprite.bounds);
        }
        
        private Texture2D GenerateTexture(Texture2D src)
        {
            var tex = new Texture2D(src.width, src.height);
            var pixelData = src.GetPixels();
            tex.SetPixels(pixelData);
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.Apply();
            return tex;
        }

        private void CalculateBounds(Bounds bounds)
        {
            _bounds.boundRightUp.x = transform.position.x + bounds.size.x * transform.localScale.x / 2;
            _bounds.boundRightUp.y = transform.position.y + bounds.size.y * transform.localScale.y / 2;
            _bounds.boundLeftDown.x = transform.position.x - bounds.size.x * transform.localScale.x / 2;
            _bounds.boundLeftDown.y = transform.position.y - bounds.size.y * transform.localScale.y / 2;
        }
        
        public bool CheckIfInBounds(Vector2 pos1, Vector2 pos2)
        {
            if (pos1.x > _bounds.boundLeftDown.x && pos1.y > _bounds.boundLeftDown.y &&
                pos1.x < _bounds.boundRightUp.x && pos1.y < _bounds.boundRightUp.y)
                return true;
            
            if (pos2.x > _bounds.boundLeftDown.x && pos2.y > _bounds.boundLeftDown.y &&
                pos2.x < _bounds.boundRightUp.x && pos2.y < _bounds.boundRightUp.y)
                return true;
            
            return false;
        }
        
        public Vector2 ClampInErasableBounds(Vector2 pos)
        {
            pos.x = Mathf.Clamp(pos.x, _bounds.boundLeftDown.x, _bounds.boundRightUp.x);
            pos.y = Mathf.Clamp(pos.y, _bounds.boundLeftDown.y, _bounds.boundRightUp.y);
            return pos;
        }

        public void RegenerateCollider()
        {
            Destroy(GetComponent<Collider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public BoundsWorldCoord Bounds => _bounds;
    }
}
