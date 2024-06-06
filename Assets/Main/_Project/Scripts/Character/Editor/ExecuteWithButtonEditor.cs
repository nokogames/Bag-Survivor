#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ExecuteWithButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var monoBehaviour = target as MonoBehaviour;
        var methods = monoBehaviour.GetType().GetMethods();
        foreach (var method in methods)
        {
            var executeAttribute = (ExecuteWithButtonAttribute)method.GetCustomAttributes(typeof(ExecuteWithButtonAttribute), true).FirstOrDefault();
            if (executeAttribute != null)
            {
                if (GUILayout.Button(executeAttribute.buttonLabel))
                {
                    method.Invoke(monoBehaviour, null);
                }
            }
        }
    }
}
#endif
