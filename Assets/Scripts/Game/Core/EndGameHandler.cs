using DG.Tweening;
using PFramework;
using UnityEngine;
using System.Collections;
namespace Game
{
    public class EndGameHandler : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _winDelay;
        [SerializeField] float _loseDelay;

        // Tween _tween;

        #region MonoBehaviour

        void Start()
        {
            Messenger.AddListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);

            // _tween?.Kill();
        }

        #endregion

        void GameEvent_GameEnd(bool isWin)
        {
            if (isWin)
            {
                DataMain.LevelIndex++;

                DataMain.LevelMax = Mathf.Max(DataMain.LevelMax, DataMain.LevelIndex);
                DataMain.LevelMax = Mathf.Min(DataMain.LevelMax, GameHelper.TotalLevel - 1);

                Taptic.Taptic.Success();

                AudioManager.Play(AudioFactory.SfxSuccess);
            }
            else
            {
                Taptic.Taptic.Vibrate();
            }

            StartCoroutine(ShowEndGamePopup(isWin));
        }

        IEnumerator ShowEndGamePopup(bool isWin)
        {
            float duration = isWin ? _winDelay : _loseDelay;
            GameObject objPopup = isWin ? PrefabFactory.PopupWin : PrefabFactory.PopupLose;

            // _tween = DOVirtual.DelayedCall(isWin ? _winDelay : _loseDelay, () =>
            // {
            // AdsShowHandler.Instance.Show(() =>
            // {

            GameObject chest = FindObjectOfType<ChestScript>().gameObject;

            Vector3 gap = chest.transform.position + new Vector3(15.14f, 4.59f, 0.51f);

            GameObject plane = PrefabManager.Instance.SpawnPlane(gap);

            plane.transform.DOMove(new Vector3(chest.transform.position.x, plane.transform.position.y, plane.transform.position.z), 2f).OnComplete(
                () =>
                {
                    Destroy(chest);
                    plane.transform.DOMove(new Vector3(plane.transform.position.y - 50f, plane.transform.position.y, plane.transform.position.z), 2f);
                }
            );

            // plane.transform.DOMove(new Vector3(plane.transform.position.y - 5f, plane.transform.position.y, plane.transform.position.z), 2f);

            yield return new WaitForSeconds(4f);

            PopupHelper.Create(objPopup);
            // });
            // });
        }
    }
}