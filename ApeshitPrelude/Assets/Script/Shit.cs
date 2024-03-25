using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    // Start is called before the first frame update
    public class Shit : XRGrabInteractable
{
        protected override void Awake()
        {
            base.Awake();
      
        }

        private void FixedUpdate()
        {

        }

        private void CheckForCollision()
        {

        }

        private void Stop()
        {

        }

        public void Release(float pullValue)
        {

        }

        private void SetPhysics(bool usePhysics)
        {

        }

        private void MaskAndFire(float power)
        {

        }

        private IEnumerator RotateWithVelocity()
        {
            yield return null;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
    {

        // Make sure to do this
        base.OnSelectEntering(args);
    }


    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Make sure to do this
        base.OnSelectExited(args);

    }
}

