using System.Collections.Concurrent;
using Assets.Scripts.Temp.EnigmaDragons;

namespace Assets.Scripts.Temp
{
    public static class Access
    {
        private static readonly ConcurrentDictionary<string, Data> _data = new ConcurrentDictionary<string, Data>();
        public static T Data<T>(string id) where T : Data => (T)_data[id];
        public static void Add(Data data) => _data[data.ID] = data;
        public static void Remove(Data data) => _data.TryRemove(data.ID, out data);
        public static bool DataExists(string id) => _data.ContainsKey(id);

        private static readonly ConcurrentDictionary<string, BodyData> _bodyData = new ConcurrentDictionary<string, BodyData>();
        public static T BodyData<T>(string id) where T : BodyData => (T)_bodyData[id];
        public static void Add(BodyData bodyData) => _bodyData[bodyData.ID] = bodyData;
        public static void Remove(BodyData bodyData) => _bodyData.TryRemove(bodyData.ID, out bodyData);
        public static bool BodyDataExists(string id) => _bodyData.ContainsKey(id);

        private static readonly ConcurrentDictionary<string, Script> _scripts = new ConcurrentDictionary<string, Script>();
        public static T Script<T>(string id) where T : Script => (T) _scripts[id];
        public static void Add(Script script) => _scripts[script.ID] = script;
        public static void Remove(Script script) => _scripts.TryRemove(script.ID, out script);
        public static bool ScriptExists(string id) => _scripts.ContainsKey(id);
    }
}
