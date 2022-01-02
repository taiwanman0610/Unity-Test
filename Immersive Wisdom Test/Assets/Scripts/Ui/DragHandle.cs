using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    public class DragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform m_RectTransform;
        private RectTransform RectTransform => m_RectTransform ? m_RectTransform : m_RectTransform = GetComponent<RectTransform>();
        
        private Vector2 StartMousePosition { get; set; }
        private Vector2 StartTransformPosition { get; set; }
        private int? DraggingPointerId { get; set; }
        private bool IsDragging => DraggingPointerId.HasValue;
        private bool edgeClick = false;

        private void OnValidate()
        {
            RectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        private void Awake()
        {
            OnValidate();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //checks if header click is on edge on window. If click is on edge, execute window resizing instead of moving
            Rect rect = m_RectTransform.rect;
            if ((rect.yMax - (eventData.pressPosition.y - RectTransform.transform.position.y) <= 10) ||
                (rect.xMax - (eventData.pressPosition.x - RectTransform.transform.position.x) <= 10) ||
                ((eventData.pressPosition.x - RectTransform.transform.position.x) - rect.xMin <= 10))
            {
                edgeClick = true;
                GetComponentInParent<ResizePanel>().CheckEdge(eventData.pressPosition);
                return;
            }
            if (IsDragging)
            {
                return;
            }
            
            DraggingPointerId = eventData.pointerId;
            StartMousePosition = eventData.position;
            StartTransformPosition = RectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (edgeClick)
            {
                GetComponentInParent<ResizePanel>().Resize(eventData.position);
                return;
            }
            if (!IsDragging)
            {
                return;
            }
            
            RectTransform parent = (RectTransform)RectTransform.parent;
            Rect parentRect = parent.rect;
            Rect rect = RectTransform.rect;
            
            Vector2 offset = eventData.position - StartMousePosition;
            Vector2 anchoredPosition = StartTransformPosition + offset;
            
            for (int i = 0; i < 2; i++)
            {
                float halfSize = 0.5f * (parentRect.size[i] - rect.size[i]);
                anchoredPosition[i] = Mathf.Clamp(anchoredPosition[i], -halfSize, halfSize);
            }
            
            RectTransform.anchoredPosition = anchoredPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (edgeClick)
            {
                GetComponentInParent<ResizePanel>().EndResize();
                edgeClick = false;
                return;
            }
            if (!IsDragging || DraggingPointerId.GetValueOrDefault() == eventData.pointerId)
            {
                return;
            }

            DraggingPointerId = null;
        }
    }
}
