using System;
using UnityEngine;

namespace Project.Architecture
{
        public abstract class BaseMonoBehaviour : MonoBehaviour
        {
            protected virtual void OnEditorValidate()
            {

            }

#if UNITY_EDITOR
            [Obsolete("UseItem OnEditorValidate instead")]
            private void OnValidate()
            {
                if (UnityEditor.EditorApplication.isPlaying)
                    return;

                OnEditorValidate();
            }
#endif
        }

}