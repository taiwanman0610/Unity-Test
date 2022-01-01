using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ui
{
    public class ResizePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        //Drag handler members/methods and RectTransform members/methods 
        //referenced from DragHandle.cs and Unity Documentation

        private RectTransform RectTransform;
        private Rect rect;
        private Vector2 lastPosition;
        private bool leftClick = false, rightClick = false, topClick = false, botClick = false;
        private float extraX = 0, extraY = 0;
        private float rightBound, leftBound, topBound, botBound;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            rect = RectTransform.rect;
            RectTransform boundary = (RectTransform)RectTransform.parent;
            Rect boundRect = boundary.rect;
            rightBound = boundary.position.x + boundRect.width / 2;
            leftBound = boundary.position.x - boundRect.width / 2;
            topBound = boundary.position.y + boundRect.height / 2;
            botBound = boundary.position.y - boundRect.height / 2;
        }

        public void CheckEdge(Vector2 pressPosition)
        {
            rect = RectTransform.rect;
            lastPosition = pressPosition;
            if (rect.xMax - (pressPosition.x - RectTransform.transform.position.x) <= 10)
            {
                rightClick = true;
                RectTransform.position = new Vector3(RectTransform.position.x - RectTransform.sizeDelta.x/2, 
                                                     RectTransform.position.y, 
                                                     RectTransform.position.z);
                RectTransform.pivot = new Vector2(0, RectTransform.pivot.y);
            }
            else if ((pressPosition.x - RectTransform.transform.position.x) - rect.xMin <= 10)
            {
                leftClick = true;
                RectTransform.position = new Vector3(RectTransform.position.x + RectTransform.sizeDelta.x / 2, 
                                                     RectTransform.position.y, 
                                                     RectTransform.position.z);
                RectTransform.pivot = new Vector2(1, RectTransform.pivot.y);
            }
            if (rect.yMax - (pressPosition.y - RectTransform.transform.position.y) <= 10)
            {
                topClick = true;
                RectTransform.position = new Vector3(RectTransform.position.x, 
                                                     RectTransform.position.y - RectTransform.sizeDelta.y / 2, 
                                                     RectTransform.position.z);
                RectTransform.pivot = new Vector2(RectTransform.pivot.x, 0);
            }
            else if ((pressPosition.y - RectTransform.transform.position.y) - rect.yMin <= 10)
            {
                botClick = true;
                RectTransform.position = new Vector3(RectTransform.position.x, 
                                                     RectTransform.position.y + RectTransform.sizeDelta.y / 2, 
                                                     RectTransform.position.z);
                RectTransform.pivot = new Vector2(RectTransform.pivot.x, 1);
            }
        }

        public void Resize(Vector2 mousePosition)
        {
            Vector2 offset = lastPosition - mousePosition;
            if (rightClick)
            {
                if (extraX < 0)
                {
                    extraX -= offset.x;
                    if (extraX >= 0)
                    {
                        offset.x = -extraX;
                        extraX = 0;
                    }
                }
                if (extraX == 0)
                {
                    if (mousePosition.x > rightBound)
                    {
                        RectTransform.sizeDelta = new Vector2(rightBound - RectTransform.position.x, RectTransform.sizeDelta.y);
                    }
                    else
                    {
                        RectTransform.sizeDelta = new Vector2(mousePosition.x - RectTransform.position.x,
                                                          RectTransform.sizeDelta.y);
                    }
                }
            }
            else if (leftClick)
            {
                if (extraX < 0)
                {
                    extraX += offset.x;
                    if (extraX >= 0)
                    {
                        offset.x = extraX;
                        extraX = 0;
                    }
                }
                if (extraX == 0)
                {
                    if (mousePosition.x < leftBound)
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.position.x - leftBound, RectTransform.sizeDelta.y);
                    }
                    else
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.position.x - mousePosition.x,
                                                          RectTransform.sizeDelta.y);
                    }
                }
            }
            if (topClick)
            {
                if (extraY < 0)
                {
                    extraY -= offset.y;
                    if (extraY >= 0)
                    {
                        offset.y = -extraY;
                        extraY = 0;
                    }
                }
                if (extraY == 0) {
                    if (mousePosition.y > topBound)
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, topBound - RectTransform.position.y);
                    }
                    else
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x,
                                                              mousePosition.y - RectTransform.position.y);
                    }
                }
            }
            else if (botClick)
            {
                if (extraY < 0)
                {
                    extraY += offset.y;
                    if (extraY >= 0)
                    {
                        offset.y = extraY;
                        extraY = 0;
                    }
                }
                if (extraY == 0)
                {
                    if (mousePosition.y < botBound)
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, RectTransform.position.y - botBound);
                    }
                    else
                    {
                        RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x,
                                                              RectTransform.position.y - mousePosition.y);
                    }
                }
            }
            if (RectTransform.sizeDelta.x < 60)
            {
                extraX -= (60 - RectTransform.sizeDelta.x);
                RectTransform.sizeDelta = new Vector2(60, RectTransform.sizeDelta.y);
            }
            if (RectTransform.sizeDelta.y < 90)
            {
                extraY -= (90 - RectTransform.sizeDelta.y);
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, 90);
            }
            lastPosition = mousePosition;
        }

        public void EndResize()
        {
            if (rightClick)
            {
                rightClick = false;
                RectTransform.position = new Vector3(RectTransform.position.x + RectTransform.sizeDelta.x / 2, 
                                                     RectTransform.position.y, 
                                                     RectTransform.position.z);
            }
            else if (leftClick)
            {
                leftClick = false;
                RectTransform.position = new Vector3(RectTransform.position.x - RectTransform.sizeDelta.x / 2,
                                                     RectTransform.position.y,
                                                     RectTransform.position.z);
            }
            if (topClick)
            {
                topClick = false;
                RectTransform.position = new Vector3(RectTransform.position.x,
                                                     RectTransform.position.y + RectTransform.sizeDelta.y / 2,
                                                     RectTransform.position.z);
            }
            else if (botClick)
            {
                botClick = false;
                RectTransform.position = new Vector3(RectTransform.position.x,
                                                     RectTransform.position.y - RectTransform.sizeDelta.y / 2,
                                                     RectTransform.position.z);
            }
            RectTransform.pivot = new Vector2(0.5f,0.5f);
            extraX = 0;
            extraY = 0;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CheckEdge(eventData.pressPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Resize(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndResize();
        }
    }
}
