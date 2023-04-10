using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace LTH.ColorMatch.UI
{
    public class MoveUI : MonoBehaviour
    {
        [SerializeField] private RectTransform moveUI;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private Vector2 currentPos;
        [SerializeField] private Vector2 originPos;
        [SerializeField] private float moveSpeed;
        private bool _isMoving;

        public bool isComplete;
        
        // private void Start()
        // {
        // }

        private void OnEnable()
        {
            moveUI.anchoredPosition = originPos;
            currentPos = moveUI.anchoredPosition; // 시작 위치 초기화
        }

        public void StartMove()
        {
            if (!_isMoving) // 이동 중이 아닐 때만 실행
            {
                StopAllCoroutines();
                StartCoroutine(MoveCoroutine(currentPos, targetPos)); // 코루틴 시작
            }
        }

        public void Return()
        {
            if (!_isMoving) // 이동 중이 아닐 때만 실행
            {
                StopAllCoroutines();
                StartCoroutine(MoveCoroutine(currentPos,originPos)); // 코루틴 시작
            }
        }

        private IEnumerator MoveCoroutine(Vector2 start,Vector2 target)
        {
            _isMoving = true; // 이동 중으로 변경
            isComplete = false;
            float elapsedTime = 0f; // 경과 시간 초기화

            while (elapsedTime < moveSpeed) // 이동 속도에 따라 이동 시간 계산
            {
                elapsedTime += Time.deltaTime; // 경과 시간 누적
                moveUI.anchoredPosition = Vector2.Lerp(start, target, elapsedTime / moveSpeed); // 선형 보간으로 위치 조정
                yield return null; // 한 프레임 대기
            }
            
            moveUI.anchoredPosition = target; // 목표 위치에 정확히 도달하도록 위치 조정
            currentPos = target;
            _isMoving = false; // 이동 중 해제
            isComplete = true;
        }
    }
}
