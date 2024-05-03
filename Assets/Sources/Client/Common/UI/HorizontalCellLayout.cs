using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CardGame.Layouts
{
    [ExecuteInEditMode]
    public class HorizontalCellLayout : MonoBehaviour // TODO : Make Controller and View (not time for this)
    {
        public int CellCount => transform.childCount;
        public Vector3 ContentPivot => (Vector3)Rect.center + _pivotOffset;
        public Rect Rect
        {
            get
            {
                var startPoint = transform.position;
                startPoint.x -= _layoutSize.x / 2f;;
                startPoint.y -= _layoutSize.y / 2f;
                return new Rect(startPoint, _layoutSize);
            }
        }

        [SerializeField] private Vector3 _pivotOffset;
        [SerializeField] private Vector2 _layoutSize;
        [SerializeField] private Vector3 _lookAtRotateAxies = Vector3.forward;
        [SerializeField] private AnimationCurve _layoutCurve;
        [SerializeField] [Range(-10f,10f)] private float _padding = 0f;
        [SerializeField] [Range(0f,10f)] private float _animateSpeed = 1;
        [SerializeField] private int _curveResolution = 25;
        [SerializeField] private bool _lookAtNormal;

        private Coroutine _animateProcess;
        
        public void Rebuild(bool force = false)
        {
            if (CellCount == 0)
            {
                return;
            }
            
            var cells = GetComponentsInChildren<BoxCollider>();
            var maxRange = cells.Sum(x => x.bounds.size.x) + (_padding * cells.Length);
            var maxTimeRange = (maxRange / Rect.width);
            var layoutRect = Rect;
            var paddingTime = _padding/ layoutRect.width;
            var lastTimePosition = (0.5f + paddingTime/2f) - (maxTimeRange / 2f); // start offset to center
            var targets = new Dictionary<Transform, float>();
            for (var i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                var timePosition = cell.size.x / layoutRect.width;
                targets.Add(cell.transform, lastTimePosition + (timePosition / 2f));
                lastTimePosition += timePosition + paddingTime;
            }
            
            if (_animateProcess != null)
            {
                StopCoroutine(_animateProcess);
                _animateProcess = null;
            }
            _animateProcess = StartCoroutine(Animate(targets, force));
        }

        private IEnumerator Animate(IDictionary<Transform, float> targets, bool force)
        {
            if (force)
            {
                foreach (var target in targets)
                {
                    Place(target.Key, target.Value);
                }
                _animateProcess = null;
                yield break;
            }
            
            while (targets.Count > 0)
            {
                Action postRemove = default;
                foreach (var target in targets)
                {
                    var currPos = GetCurveTime(target.Key.position.x);
                    var time = Mathf.MoveTowards(currPos, target.Value, _animateSpeed * Time.deltaTime);
                    Place(target.Key, time);
                    if (Math.Abs(time - currPos) <= 0.00001f)
                    {
                        postRemove += () => targets.Remove(target.Key);
                    }
                }
                postRemove?.Invoke();
                yield return null;
            }
            _animateProcess = null;
        }
        
        public void InsertAtLast(Transform cell, bool update = true)
        {
            cell.SetParent(transform);
            cell.SetSiblingIndex(CellCount);
            if (update)
            {
                Rebuild();
            }
        }

        public void InsertAt(Transform cell, int sublingIndex, bool update = true)
        {
            InsertAtLast(cell, false);
            var cellCount = CellCount;
            if (sublingIndex < 0 || sublingIndex >= cellCount)
            {
                sublingIndex = cellCount;
            }
            
            cell.SetSiblingIndex(sublingIndex);
            if (update)
            {
                Rebuild();  
            }
        }

        public Vector3 GetPointAtCurve(float time01)
        {
            time01 = Mathf.Clamp01(time01);
            var point = ContentPivot;
            var halfSizeX = _layoutSize.x / 2f;
            point.x = Mathf.Lerp(point.x - halfSizeX ,point.x + halfSizeX, time01);
            point.y += _layoutCurve.Evaluate(time01);
            point.z += -time01;
            return point;
        }
        
        public Vector2 GetNormalAtCurve(float time01)
        {
            var step = 1f / _curveResolution;
            var timeNext = time01 + (time01 + step >= 1 ? -step : step);
            var point1 = GetPointAtCurve(Mathf.Min(time01, timeNext));
            var point0 = GetPointAtCurve(Mathf.Max(time01, timeNext));
            return Vector2.Perpendicular(point0 - point1);
        }
        
        public float GetCurveTime(float xPosition)
        {
            var point = ContentPivot;
            var halfSizeX = _layoutSize.x / 2f;
            return Mathf.Clamp01(Mathf.InverseLerp(point.x - halfSizeX ,point.x + halfSizeX, xPosition));
        }
        
        private void Place(Transform cell, float time01)
        {
            time01 = Mathf.Clamp01(time01);
            cell.position = GetPointAtCurve(time01);
            if (_lookAtNormal)
            {
                cell.rotation = Quaternion.LookRotation(_lookAtRotateAxies, GetNormalAtCurve(time01));
            }
            else
            {
                cell.rotation = Quaternion.identity;
            }
        }
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var faceColor = Color.white;
            faceColor.a = 0.5f;
            Handles.DrawSolidRectangleWithOutline(Rect, faceColor, Color.white);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ContentPivot, 0.5f);
        }
        #endif
    }
}
