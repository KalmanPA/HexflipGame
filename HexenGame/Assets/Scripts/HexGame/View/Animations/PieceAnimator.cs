using Hex.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.View.Animations
{
    public class PieceAnimator : MonoBehaviour
    {
        private Coroutine _moveCoroutine;
        private float tValue = 0f;

        private PieceView _view;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Quaternion _flippiedRotation;
        private void Start()
        {
            _view = GetComponent<PieceView>();

            _view.PieceModel.Flipped += PieceModel_Flipped;

            _startPosition = transform.position;

            //_startRotation = transform.rotation;
        }

        private void PieceModel_Flipped(object sender, System.EventArgs e)
        {
            if (sender is not Piece piece) return;
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            tValue = Mathf.Clamp01(tValue);

            _startRotation = piece.CurrentColor == CurrentColor.Black ? Quaternion.identity : Quaternion.Euler(180, 0, 0);
            _flippiedRotation = piece.CurrentColor == CurrentColor.Black ? Quaternion.Euler(180, 0, 0) : Quaternion.identity;

            if (tValue > 0 && tValue < 1f)
            {
                tValue = 1 - tValue;
            }
            else
            {
                tValue = 0;
            }

            _moveCoroutine = StartCoroutine(FlipPiece());
        }

        IEnumerator FlipPiece()
        {
            //Rotate values

            

            //Quaternion targetRotation = _startRotation * Quaternion.Euler(180f, 0f, 0f);

            float speed = 2f;

            //float timer = 0f;

            //Move values



            while (tValue < 1f)
            {
                tValue += Time.deltaTime * speed;
                transform.rotation = Quaternion.Slerp(_startRotation, _flippiedRotation, tValue);

                Vector3 newPos = _startPosition;
                float tY = Mathf.Sin(tValue * Mathf.PI);
                newPos.y = Mathf.Lerp(_startPosition.y, _startPosition.y + 1f, tY);
                transform.position = newPos;

                yield return null;
            }


            //Quaternion targetRotation = _startRotation * Quaternion.Euler(180f, 0f, 0f);

            //while (tValue < 1f)
            //{


            //    tValue += Time.deltaTime;

            //    transform.rotation = Quaternion.Slerp(_startRotation, targetRotation, tValue);

            //    Vector3 newPos = _startPosition;
            //    float tY = Mathf.Sin(tValue * Mathf.PI);
            //    newPos.y = Mathf.Lerp(_startPosition.y, _startPosition.y + 1f, tY);
            //    transform.position = newPos;
            //    yield return null;
            //}
            //transform.SetPositionAndRotation(_startPosition, _flippiedRotation);
            //(_startRotation, _flippiedRotation) = (_flippiedRotation, _startRotation);

        }
    }
}
