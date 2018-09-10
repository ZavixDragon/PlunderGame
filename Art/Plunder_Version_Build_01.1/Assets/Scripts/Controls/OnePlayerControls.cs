using System;
using System.Linq;
using Assets.Scripts.BowlX;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controls
{
    public class OnePlayerControls : MonoBehaviour
    {
        public SoundEffectControl ChooseBowl;

        public void Redo()
        {
            if (GameResources.Queue.NextAvailableTime > DateTimeOffset.Now)
                return;

            var pick = GameResources.Game.LastPick;
            GameResources.Game.Undo();
            GameResources.Game.Pick(pick);
        }

        public void Update()
        {
            if (GameResources.Queue.NextAvailableTime > DateTimeOffset.Now)
                return;

            if (Input.mousePresent)
                ResolveInput(Input.mousePosition, () => Input.GetMouseButtonUp(0));
            else
                ResolveInput(Input.GetTouch(0).position, () => Input.GetTouch(0).phase == TouchPhase.Ended);
        }

        private void ResolveInput(Vector3 position, Func<bool> isUsing)
        {
            BowlScript bowl;
            if (GameResources.Game.IsGameOver)
            {
                if (isUsing())
                    SceneManager.LoadScene(0);
            }
            else if (TryGetSelectedBowlBehaviour(position, out bowl))
            {
                if (isUsing())
                    MakeMove(bowl);
                else
                    PreviewMove(bowl);
            }
            else if (GameResources.Game.IsPreviewing)
                GameResources.Game.StopPreviewing();
        }

        private bool TryGetSelectedBowlBehaviour(Vector3 position, out BowlScript selectedBowl)
        {
            var bowl = Physics.RaycastAll(GameResources.Cameras.ActiveCamera.ScreenPointToRay(position), 20)
                .Select(x => x.transform.gameObject.GetComponent<BowlScript>())
                .FirstOrDefault(x => x != null);
            if (bowl != null && (bowl.Body.Owner != GameResources.Game.PlayerToAct || bowl.Body.Position == Position.Score))
                bowl = null;
            selectedBowl = bowl;
            return bowl != null;
        }

        private void MakeMove(BowlScript bowl)
        {
            try
            {
                GameResources.Game.Pick(bowl.Body.Position);
                ChooseBowl.Play();
            }
            catch (Exception ex)
            {
                GameResources.ErrorMessage = ex.Message;
            }
        }

        private void PreviewMove(BowlScript bowl)
        {
            try
            {
                if (!GameResources.Game.IsPreviewing || bowl.ID != GameResources.Game.PreviewPickID)
                    GameResources.Game.Preview(bowl.Body.Position);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
}
