// ActivePack Library
// Copyright (C) BigBlit Assets Michal Kalinowski
// http://bigblit.fun
//

using UnityEngine;

namespace BigBlit.ActivePack
{
    /// <summary>
    /// Keyboard controller for all pressable objects.
    /// </summary>
    [BehaviourInfo("Keyboard controller for all pressable objects\n" +
    "Use it if you want to press/unpress IPressable behaviour with a keyboard.")]
    public class KeyboardPressController : PressControllerBase
    {
        #region FIELDS AND PROPERTIES

        [SerializeField] KeyCode keyCode = KeyCode.None;
        #endregion

        #region UNITY EVENTS
        private void Update() {
            if (Input.GetKeyDown(keyCode)) {
                foreach (var target in Targets) {
                    target.Press();
                }
            }  else if(Input.GetKeyUp(keyCode)) {
                foreach (var target in Targets) {
                    target.Normal();
                }
            }
        }

        #endregion
    }
}
