using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erase
{
    public struct DragData
    {
        public Vector2 currentMousePosition;
        public Vector2 previousMousePosition;
        public Queue<Vector2> dragPoints;
        public Vector2 prevPoint;
    }
    public class Eraser : MonoBehaviour
    {
        [SerializeField] private int eraserSize;
        [SerializeField] private List<Erasable> _erasables;
        private EraserView eraserView;
        private Camera _camera;
        private DragData _dragData;
        
        void Start()
        {
            _camera = Camera.main;
            eraserView = GetComponentInChildren<EraserView>();
            eraserView.ToggleView(false);
            _dragData.dragPoints = new Queue<Vector2>();
        }

        

        void Update()
        {
            CheckForDrag();
            if(_dragData.dragPoints.Count > 0)
                MoveEraserAndFindErasable();
        }

        private void CheckForDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
                eraserView.ToggleView(true);
                _dragData.dragPoints.Enqueue(_camera.ScreenToWorldPoint(Input.mousePosition));
                _dragData.prevPoint = transform.position;
            } 
            else
            if (Input.GetMouseButtonUp(0))
            {
                eraserView.ToggleView(false);
                _dragData.dragPoints.Clear();
                foreach (var e in _erasables)
                {
                    e.RegenerateCollider();
                }
            }
            else
            if (Input.GetMouseButton(0))
            {
                _dragData.previousMousePosition = _dragData.currentMousePosition;
                 _dragData.currentMousePosition = Input.mousePosition;
                if (_dragData.previousMousePosition != _dragData.currentMousePosition)
                    _dragData.dragPoints.Enqueue(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        private void MoveEraserAndFindErasable()
        {
            transform.position = Vector2.Lerp(transform.position, _dragData.dragPoints.Peek(), 0.9f); 
            FindErasableBetweenPoints(_dragData.prevPoint,transform.position);
            
            if (Vector2.Distance(_dragData.dragPoints.Peek(), transform.position) < 0.1f)
            {
                _dragData.prevPoint = _dragData.dragPoints.Dequeue();
            }
            else
            {
                _dragData.prevPoint = transform.position;
            }
        }

        private void FindErasableBetweenPoints(Vector2 prevPos, Vector2 currentPos)
        {
            foreach (var e in _erasables)
            {
                if (e.CheckIfInBounds(prevPos, currentPos))
                {
                    Erase(prevPos,currentPos,e);
                }
            }
        }
        private void Erase(Vector2 startPoint,Vector2 endPoint,Erasable erasable)
        {
            startPoint = erasable.ClampInErasableBounds(startPoint);
            endPoint = erasable.ClampInErasableBounds(endPoint);

            var sprite = erasable.SpriteRenderer.sprite;
            var erasablePos = erasable.transform.position;
            var erasableScale = erasable.transform.localScale;
            
            var deltaPosStart = startPoint - (Vector2) erasablePos;
            var deltaPosEnd = endPoint - (Vector2) erasablePos;
            
            var rectPointStart =
                CalculateTextureCoord(sprite, deltaPosStart, erasableScale);
            var rectPointEnd =
                CalculateTextureCoord(sprite, deltaPosEnd, erasableScale);
            
            ErasePixels(sprite.texture, rectPointStart,rectPointEnd);
        }

        private Vector2 CalculateTextureCoord(Sprite sprite,Vector2 deltaPos,Vector3 scale)
        {
            var rectSize = sprite.rect.size;
            deltaPos.x /= sprite.bounds.size.x*scale.x;
            deltaPos.y /= sprite.bounds.size.y*scale.y;
            var rectPoint = new Vector2(rectSize.x * deltaPos.x + rectSize.x / 2,
                rectSize.y * deltaPos.y + rectSize.y / 2);
            return rectPoint;
        }

        private void ErasePixels(Texture2D tex, Vector2 start, Vector2 end)
        {
            TextureHelpers.LineNoApply(tex, Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y),
                Mathf.RoundToInt(end.x), Mathf.RoundToInt(end.y), eraserSize, Color.clear);
            tex.Apply();
        }
    }
}
