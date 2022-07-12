using _04_Scripts._05_Enemies_Bosses.Enemy;
using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemiesCore), true)]
public class FieldOfViewEditor : Editor { 
    private void OnSceneGUI(){
        EnemiesCore fov = (EnemiesCore)target;
        Handles.color = Color.red;
            
        var position = fov.transform.position;
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, fov.radius);

        var eulerAngles = fov.transform.eulerAngles;
        Vector3 viewAngle01 = DirectionFromAngle(eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(eulerAngles.y, fov.angle / 2);

        Handles.color = Color.white;
        Handles.DrawLine(position, position + viewAngle01 * fov.radius);
        Handles.DrawLine(position, position + viewAngle02 * fov.radius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees){
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

