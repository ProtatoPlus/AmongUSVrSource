using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestMod
{
    internal class gui : MonoBehaviour
    {
        GUIContent content;
        void OnGUI()
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), content);
        }
    }
}
