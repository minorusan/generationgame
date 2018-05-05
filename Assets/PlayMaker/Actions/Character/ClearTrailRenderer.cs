// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Character)]
    public class ClearTrailRenderer : FsmStateAction
    {
        public FsmGameObject trailOwner;

        public override void OnEnter()
        {
            var owner = trailOwner.Value;
            owner.GetComponentInChildren<TrailRenderer>().Clear();
            Finish();
        }
    }
}