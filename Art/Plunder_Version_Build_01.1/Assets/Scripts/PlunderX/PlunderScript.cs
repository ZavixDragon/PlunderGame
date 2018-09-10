using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.PlunderX
{
    public class PlunderScript : Script
    {
        public LightingScript LightingScript;
        public WorldScript WorldScript;

        public PlunderBody Body { get; set; }

        public PlunderScript Spawn(PlunderBody body)
        {
            var plunder = Instantiate(this);
            plunder.Body = body;
            plunder.ID = body.ID;
            plunder.LightingScript = LightingScript.Spawn(body);
            plunder.WorldScript = WorldScript.Spawn(body);
            plunder.WorldScript.transform.SetParent(plunder.transform);
            plunder.LightingScript.transform.SetParent(plunder.WorldScript.transform);
            return plunder;
        }
    }
}
