using Assets.Scripts.Code.Common;
using Assets.Scripts.Code.CoreGame;
using Assets.Scripts.Code.UI;
using Assets.Scripts.Temp;
using Assets.Scripts.Temp.EnigmaDragons;
using Assets.Scripts.Temp.StoneX;
using UnityEngine;

namespace Assets.Scripts.BowlX
{
    public class BowlScript : Script
    {
        public TextMesh Text;
        public MeshRenderer Renderer;
        public Bowl Body { get; set; }

        public BowlScript Spawn(Bowl body, Transform parent) 
        {
            var bowl = Instantiate(this, parent);
            bowl.ID = body.ID;
            bowl.Body = body;
            body.StoneIDs.ForEach(x =>
            {
                var target = bowl.transform.position;
                target.x -= body.Position == Position.Score ? 0 : 7.6f;
                target.y += body.Position == Position.Score ? 6 - 1 : 3;
                target.z += body.Position == Position.Score ? -0.5f : 0;
                if (!Access.BodyDataExists(x))
                    new StoneBodyData(x, target, Quaternion.identity);
                if (GameResources.IsContinue)
                {
                    GameResources
                        .Stones[Mathf.Abs(x.GetHashCode() % GameResources.Stones.Count)]
                        .Spawn(new StoneBody(x), bowl.transform);
                }
                else
                    GameResources.Queue.SpawnStone(x, body.ID, body.Owner, body.Position);
            });
            return bowl;
        }

        public void Update()
        {
            if (GameResources.Game.IsPreviewing)
            {
                if (GameResources.Game.IsPreviewCapturing
                        && GameResources.Game.Bowls[GameResources.Game.OpposingPlayer][5 - (int) GameResources.Game.PreviewLastBowl.Position].ID == ID)
                    SetDisplay("Captured", GameResources.BowlCapturedMaterial);
                else if (GameResources.Game.PreviewLastBowl.Position == Position.Score && ID == GameResources.Game.PreviewLastBowl.ID)
                    SetDisplay("Extra Turn", GameResources.BowlExtraTurnMaterial);
                else if (GameResources.Game.PreviewLastBowl.ID == ID)
                    SetDisplay(BlankIfZero(Body.PreviewStoneIDs.Count), GameResources.LastBowlMaterial);
                else if (GameResources.Game.PreviewPickID == ID)
                    SetDisplay(BlankIfZero(Body.PreviewStoneIDs.Count), GameResources.BowlHoverMaterial);
                else if (Body.PreviewStoneIDs.Count > Body.StoneIDs.Count)
                    SetDisplay(BlankIfZero(Body.PreviewStoneIDs.Count), GameResources.BowlAddedMaterial);
                else
                    SetDisplay(BlankIfZero(Body.PreviewStoneIDs.Count), GameResources.BowlIdleMaterial);
            }
            else
                SetDisplay(BlankIfZero(Body.StoneIDs.Count), GameResources.BowlIdleMaterial);
        }

        private void SetDisplay(string text, Material material)
        {
            Text.text = text;
            Renderer.material = material;
        }

        private string BlankIfZero(int num)
        {
            return num == 0 ? "" : num.ToString();
        }
    }
}
