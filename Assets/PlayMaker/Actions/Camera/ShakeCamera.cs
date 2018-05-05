// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Camera)]
    public class ShakeCamera : FsmStateAction
    {
        public FsmFloat shakeAmount;
        public FsmFloat shakeDuration;

        public override void OnEnter()
        {
            Utils.CameraShake.ShakeWithForce(shakeAmount.Value, shakeDuration.Value);
        }
    }
}