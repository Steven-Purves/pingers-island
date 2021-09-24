
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGen))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {

        MapGen map = target as MapGen;

        if (DrawDefaultInspector())
        {
            map.GenerateMap();
            map.SpawnWeather();
        }

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
            map.SpawnWeather();
        }
    }
}
