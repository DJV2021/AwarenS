/************************************************************************************

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

#define DEBUG_LOCOMOTION_PANEL

using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

namespace Plugins.Oculus
{
    public class TeleportToNode : MonoBehaviour
    {
        private LocomotionController _lc;
        private LocomotionTeleport TeleportController => _lc.GetComponent<LocomotionTeleport>();

        public void Start()
        {
            _lc = FindObjectOfType<LocomotionController>();

            // This is just a quick hack-in, need a prefab-based way of setting this up easily.
            var eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.LogError("Need EventSystem");
            }
            
            // SetupTwoStickTeleport();
            SetupNodeTeleport();

            // SAMPLE-ONLY HACK:
            // Due to restrictions on how Unity project settings work, we just hackily set up default
            // to ignore the water layer here. In your own project, you should set up your collision
            // layers properly through the Unity editor.
            Physics.IgnoreLayerCollision(0, 4);
        }

        [Conditional("DEBUG_LOCOMOTION_PANEL")]
        private static void Log(string msg)
        {
            Debug.Log(msg);
        }

        /// <summary>
        /// This method will ensure only one specific type TActivate in a given group of components derived from the same TCategory type is enabled.
        /// This is used by the sample support code to select between different targeting, input, aim, and other handlers.
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <typeparam name="TActivate"></typeparam>
        /// <param name="target"></param>
        private static void ActivateCategory<TCategory, TActivate>(GameObject target) where TCategory : MonoBehaviour where TActivate : MonoBehaviour
        {
            var components = target.GetComponents<TCategory>();
            Log("Activate " + typeof(TActivate) + " derived from " + typeof(TCategory) + "[" + components.Length + "]");
            foreach (var t in components)
            {
                var c = (MonoBehaviour)t;
                var active = c.GetType() == typeof(TActivate);
                Log(c.GetType() + " is " + typeof(TActivate) + " = " + active);
                if (c.enabled != active)
                {
                    c.enabled = active;
                }
            }
        }

        /// <summary>
        /// This generic method is used for activating a specific set of components in the LocomotionController. This is just one way 
        /// to achieve the goal of enabling one component of each category (input, aim, target, orientation and transition) that
        /// the teleport system requires.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TAim"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TOrientation"></typeparam>
        /// <typeparam name="TTransition"></typeparam>
        private void ActivateHandlers<TInput, TAim, TTarget, TOrientation, TTransition>()
            where TInput : TeleportInputHandler
            where TAim : TeleportAimHandler
            where TTarget : TeleportTargetHandler
            where TOrientation : TeleportOrientationHandler
            where TTransition : TeleportTransition
        {
            ActivateInput<TInput>();
            ActivateAim<TAim>();
            ActivateTarget<TTarget>();
            ActivateOrientation<TOrientation>();
            ActivateTransition<TTransition>();
        }

        private void ActivateInput<TActivate>() where TActivate : TeleportInputHandler
        {
            ActivateCategory<TeleportInputHandler, TActivate>();
        }

        private void ActivateAim<TActivate>() where TActivate : TeleportAimHandler
        {
            ActivateCategory<TeleportAimHandler, TActivate>();
        }

        private void ActivateTarget<TActivate>() where TActivate : TeleportTargetHandler
        {
            ActivateCategory<TeleportTargetHandler, TActivate>();
        }

        private void ActivateOrientation<TActivate>() where TActivate : TeleportOrientationHandler
        {
            ActivateCategory<TeleportOrientationHandler, TActivate>();
        }

        private void ActivateTransition<TActivate>() where TActivate : TeleportTransition
        {
            ActivateCategory<TeleportTransition, TActivate>();
        }

        private void ActivateCategory<TCategory, TActivate>() where TCategory : MonoBehaviour where TActivate : MonoBehaviour
        {
            ActivateCategory<TCategory, TActivate>(_lc.gameObject);
        }

        private void SetupNonCap()
        {
            var input = TeleportController.GetComponent<TeleportInputHandlerTouch>();
            input.InputMode = TeleportInputHandlerTouch.InputModes.SeparateButtonsForAimAndTeleport;
            input.AimButton = OVRInput.RawButton.A;
            input.TeleportButton = OVRInput.RawButton.A;
        }

        private void SetupTeleportDefaults()
        {
            TeleportController.enabled = true;
            //lc.PlayerController.SnapRotation = true;
            _lc.PlayerController.RotationEitherThumbstick = false;
            //lc.PlayerController.FixedSpeedSteps = 0;
            TeleportController.EnableMovement(false, false, false, false);
            TeleportController.EnableRotation(false, false, false, false);

            var input = TeleportController.GetComponent<TeleportInputHandlerTouch>();
            input.InputMode = TeleportInputHandlerTouch.InputModes.CapacitiveButtonForAimAndTeleport;
            input.AimButton = OVRInput.RawButton.A;
            input.TeleportButton = OVRInput.RawButton.A;
            input.CapacitiveAimAndTeleportButton = TeleportInputHandlerTouch.AimCapTouchButtons.A;
            input.FastTeleport = false;

            var hmd = TeleportController.GetComponent<TeleportInputHandlerHMD>();
            hmd.AimButton = OVRInput.RawButton.A;
            hmd.TeleportButton = OVRInput.RawButton.A;

            var orient = TeleportController.GetComponent<TeleportOrientationHandlerThumbstick>();
            orient.Thumbstick = OVRInput.Controller.LTouch;
        }

        // Teleport between node with A buttons. Display laser to node. Allow snap turns.
        private void SetupNodeTeleport()
        {
            SetupTeleportDefaults();
            SetupNonCap();
            //lc.PlayerController.SnapRotation = true;
            //lc.PlayerController.FixedSpeedSteps = 1;
            _lc.PlayerController.RotationEitherThumbstick = true;
            TeleportController.EnableRotation(true, false, false, true);
            ActivateHandlers<TeleportInputHandlerTouch, TeleportAimHandlerLaser, TeleportTargetHandlerNode, TeleportOrientationHandlerThumbstick, TeleportTransitionBlink>();
            var input = TeleportController.GetComponent<TeleportInputHandlerTouch>();
            input.AimingController = OVRInput.Controller.RTouch;
            //var input = TeleportController.GetComponent<TeleportAimHandlerLaser>();
            //input.AimingController = OVRInput.Controller.RTouch;
        }
    }
}
