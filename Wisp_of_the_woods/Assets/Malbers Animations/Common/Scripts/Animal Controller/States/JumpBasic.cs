using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MalbersAnimations.Events;
using MalbersAnimations.Utilities;
using MalbersAnimations.Scriptables;

namespace MalbersAnimations.Controller
{
    public class JumpBasic : State
    {
        [Header("Jump Parameters")]
        [Tooltip("Can the Animal be Rotated while Jumping?")]
        public BoolReference AirControl = new BoolReference(true);
        /// <summary>If the Jump input is pressed, the Animal will keep going Up while the Jump Animation is Playing</summary>
        [Tooltip("If the Jump input is pressed, the Animal will keep going Up while the Jump Animation is Playing")]
        public BoolReference JumpPressed = new BoolReference(false);
        [Tooltip("Smooth Value for Changing Speed Movement on the Air")]
        public FloatReference AirSmooth = new FloatReference(5);
        public FloatReference AirRotation = new FloatReference(10);
        public FloatReference Height = new FloatReference(10);
        public FloatReference Forward = new FloatReference(5);
        private float JumpPressHeight_Value = 1;

        [Range(0, 1)]
        public float ExitTime = 0.5f;
        
        protected MSpeed JumpSpeed;
        private bool CanJumpAgain;

        public override void StatebyInput()
        {
            if (InputValue && CanJumpAgain)
            {
                Activate();   
                CanJumpAgain = false;
            }
        }

        public override void Activate()
        {
            base.Activate();
            IgnoreLowerStates = true;                   //Make sure while you are on Jump State above the list cannot check for Trying to activate State below him
            animal.currentSpeedModifier.animator = 1;

            IsPersistent = true;                 //IMPORTANT!!!!! DO NOT ELIMINATE!!!!! 
            JumpStart();
        }

        private void JumpStart()
        {
            JumpPressHeight_Value = 1;
            IsPersistent = true;

            JumpSpeed = new MSpeed(animal.CurrentSpeedModifier) //Inherit the Vertical and the Lerps
            {
                name = "Jump Basic Speed",
                position = animal.HorizontalSpeed, //Inherit the Horizontal Speed you have from the last state
                animator = 1,
                rotation = AirRotation
            };

            animal.UpdateDirectionSpeed = AirControl;

            animal.SetCustomSpeed(JumpSpeed);       //Set the Current Speed to the Jump Speed Modifier
        }

        public override void OnStateMove(float deltaTime)
        {
            if (InMainTagHash)
            {
                if (JumpPressed)
                {
                    JumpPressHeight_Value = Mathf.Lerp(JumpPressHeight_Value, InputValue ? 1 : 0, deltaTime * AirSmooth);
                }

                Vector3 ExtraJumpHeight = (animal.UpVector * Height);
                animal.AdditivePosition += ExtraJumpHeight * deltaTime * JumpPressHeight_Value;


                if (Forward > CurrentSpeed)
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed, Forward, deltaTime * AirSmooth);

            }
        }

        public override void TryExitState(float DeltaTime)
        {
            if (animal.AnimState.normalizedTime >= ExitTime)
            {
                IgnoreLowerStates = false;
                IsPersistent = false;
            }
        }

        public override void ResetState()
        {
            CanJumpAgain = true;
            JumpPressHeight_Value = 1;
        }

        public override void ExitState()
        {
            base.ExitState();
            CanJumpAgain = true;
            JumpPressHeight_Value = 1;
            animal.UpdateDirectionSpeed = true; //Reset the Rotate Direction
        }

#if UNITY_EDITOR
        public override void Reset()
        {
            ID = MalbersTools.GetInstance<StateID>("Jump");
            Input = "Jump";

            SleepFromState = new List<StateID>() { MalbersTools.GetInstance<StateID>("Fall"), MalbersTools.GetInstance<StateID>("Fly") };
            SleepFromMode = new List<ModeID>() { MalbersTools.GetInstance<ModeID>("Action"), MalbersTools.GetInstance<ModeID>("Attack1") };


            General = new AnimalModifier()
            {
                RootMotion = false,
                Grounded = false,
                Sprint = false,
                OrientToGround = false,
                CustomRotation = false,
                IgnoreLowerStates = true, //IMPORTANT!
                Persistent = false,
                AdditivePosition = true,
                Colliders = true,
                Gravity = true,
                modify = (modifier)(-1),
            };

            ExitFrame = false;
        }
#endif
    }
}
