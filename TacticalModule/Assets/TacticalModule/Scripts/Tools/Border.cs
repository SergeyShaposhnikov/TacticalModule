using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace TacticalModule.Scripts.Tools
{
    public class Border : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _point1;
        public Vector3 Point1
        {
            get
            {
                return new Vector3(transform.position.x + _point1.x, transform.position.y, transform.position.z + _point1.z);
            }
            set { _point1 = value - transform.position; }
        }

        [SerializeField]
        private Vector3 _point2;
        public Vector3 Point2
        {
            get
            {
                return new Vector3(transform.position.x + _point2.x, transform.position.y, transform.position.z + _point2.z);
            }
            set { _point2 = value - transform.position; }
        }

        //public Vector3 Point2;
        //public Vector3 Point3;
        //public Vector3 Point4;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.Label(Point1, "1");
            Handles.Label(Point2, "2");

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Point1, Point2);
            //Gizmos.DrawLine(Point2, Point3);
            //Gizmos.DrawLine(Point3, Point4);
            //Gizmos.DrawLine(Point4, Point1);
        }
#endif
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Border)), CanEditMultipleObjects]
    public class PositionHandleExampleEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            Border example = (Border)target;
            EditorGUI.BeginChangeCheck();

            if (example.Point1 == Vector3.zero)
                example.Point1 = new Vector3(-1,0,-1);
            if (example.Point2 == Vector3.zero)
                example.Point2 = new Vector3(-1, 0, +1);

            var point1 = Handles.PositionHandle(example.Point1, Quaternion.identity);
            var point2 = Handles.PositionHandle(example.Point2, Quaternion.identity);
            //var point3 = Handles.PositionHandle(example.Point3, Quaternion.identity);
            //var point4 = Handles.PositionHandle(example.Point4, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(example, "Change Look At Target Position");
                example.Point1 = point1;
                example.Point2 = point2;
                //example.Point3 = point3;
                //example.Point4 = point4;
            }
        }
    }
#endif
}
