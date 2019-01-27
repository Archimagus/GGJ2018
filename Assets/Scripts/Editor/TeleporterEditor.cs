using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Teleporter)), CanEditMultipleObjects]
public class FreeMoveHandleExampleEditor : Editor
{
	protected virtual void OnSceneGUI()
	{
		var teleporter = (Teleporter)target;

		//float size = HandleUtility.GetHandleSize(teleporter.Destination) * 0.5f;
		//Vector3 snap = Vector3.one * 0.5f;

		EditorGUI.BeginChangeCheck();
		Handles.color = teleporter.EditorHandleColor;
		Vector3 newTargetPosition = Handles.FreeMoveHandle(teleporter.Destination, Quaternion.identity, 0.5f, Vector3.zero, Handles.SphereHandleCap);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(teleporter, "MoveTeleporterDestination");
			teleporter.Destination = newTargetPosition;
		}
	}
}