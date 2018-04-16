/*
 * Solution Created by RGBSchemes
 * Code can be found at http://www.rgbschemes.com/blog/oculus-touch-and-finger-stuff-part-1/ and http://www.rgbschemes.com/blog/oculus-touch-and-finger-stuff-part-2/
 * Bone structure used to detect finger tips in Wrist_Menu Scene when selecting panels, and Wrist_Inventory Scene when pressing inventory buttons, and provides more realistic hand features to other scenes
 */

using UnityEngine;
using Oculus.Avatar;
using Oculus.Platform;
using Oculus.Platform.Models;
using System.Collections;

public class PlatformManager : MonoBehaviour {
    public OvrAvatar Avatar;
    void Awake() {
        Oculus.Platform.Core.Initialize();
        Oculus.Platform.Users.GetLoggedInUser().OnComplete(UserLoggedInCallback);
        Oculus.Platform.Request.RunCallbacks();
    }

    private void UserLoggedInCallback(Message<User> message) {
        if (!message.IsError) {
            Avatar.oculusUserID = message.Data.ID;
        }
    }
}