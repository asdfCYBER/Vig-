using System;
using System.Threading.Tasks;
using Game.Context;
using Game.Mod;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Utils;

namespace Vigнет
{
    public class Vigнет : AbstractMod
    {
        public override CachedLocalizedString Title => "Vigнет";

        public override CachedLocalizedString Description
            => "Disables vignette post processing effect";

        public override async Task OnContextChanged(IControllers controller)
        {
            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (obj.name != "Game") continue; // there's probably a better way to find a root component

                // Get the object controlling the processing of the post
                GameObject postObj = obj.transform.Find("Game Board Holder/Post Processing").gameObject;
                PostProcessingControl postControl = postObj.GetComponent<PostProcessingControl>();

                // Get the Vignette object and disable it
                foreach (VolumeComponent volumeProfileComponent in postControl.Volume.profile.components)
                {
                    try
                    {
                        Vignette vignette = (Vignette)volumeProfileComponent;
                        vignette.active = false;
                        Debug.Log("[Vigнет] Disabled vignette effect");
                    }
                    catch (InvalidCastException) { }
                }
            }

            await Task.Yield();
        }
    }
}
