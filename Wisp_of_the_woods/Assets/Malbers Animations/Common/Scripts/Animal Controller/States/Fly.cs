using MalbersAnimations.Scriptables;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MalbersAnimations.Controller
{
    public class Fly : State
    {
        public enum FlyInput { Toggle, Press, None}

        [Header("Fly Parameters")]
        [Range(0, 90),Tooltip("Bank amount used when turning")]
        public float Bank = 30;
        [Range(0, 90), Tooltip("Limit to go Up and Down")]
        public float Ylimit = 80;
        [Space, Tooltip("Type of Fly Input for Activating Flying. \nToggle: Press the Input Down to Start Flying. Press when Flying to Stop Flying.\nPress: As long as the Input is Pressed the Animal will keep Flying")]
        public FlyInput flyInput = FlyInput.Toggle;

        [Tooltip("When the Animal is close to the Ground it will automatically Land")]
        public BoolReference canLand = new BoolReference( true);
        [Tooltip("Ray Length multiplier to check for ground and automatically land (increases or decreases the MainPivot Lenght for the Fall Ray")]
        public FloatReference LandMultiplier = new FloatReference(1f);
        [Tooltip("When Entering the Fly State... The animal will keep the Velocity from the last State if this value is greater than zero")]
        public FloatReference InertiaTime = new FloatReference(1);
        
        
        [Header("Avoid Water"),Tooltip("Avoids Water when Flying")]
        public bool AvoidWater = false;
        [Tooltip("Radius of the spherecast for Finding Water")]
        public float WRadius = 0.1f;
        [Tooltip("Distance for spherecast Ray for Finding Water")]
        public float WDistance = 0.5f;

        private Collider[] WaterCollider;
        private int WaterLayer;

        //[Tooltip("Uses the Rotator on the Animal to Apply Rotations. If the Animations Rotates  the Animal. Disable this")]
        //public BoolReference UsePitchRotation = new BoolReference(true);


        [Header("Gliding")]
        public BoolReference GlideOnly = new BoolReference(false);
        public BoolReference AutoGlide = new BoolReference(true);
        [MinMaxRange(0, 10)]
        public RangedFloat GlideChance = new RangedFloat(0.8f, 4);
        [MinMaxRange(0, 10)]
        public RangedFloat FlapChange = new RangedFloat(0.5f, 4);

        protected bool isGliding = false;
        protected float FlyStyleTime = 1;


        protected float currentTime = 1;
        RaycastHit[] LandHit = new RaycastHit[1];

        [Header("Down Acceleration")]
        public FloatReference GravityDrag = new FloatReference(0);
        public FloatReference DownAcceleration = new FloatReference(1);
        private float acceleration = 0;

        protected Vector3 verticalInertia;
      
        public override void StatebyInput()
        {
            if (InputValue && !ActiveState)  //Enable fly if is not already active
            {
                if (flyInput == FlyInput.Toggle)
                {
                    InputValue = false; //Reset the Input to false if is set to toggle
                }

                Activate();
            }
            else if (InputValue && ActiveState && (flyInput == FlyInput.Toggle))
            {
                AllowExit();
                InputValue = false; 
            }
        }

        public override bool TryActivate()
        {
            return InputValue;
        }


        public override void StartState()
        {
            LandHit = new RaycastHit[1];
            currentTime = Time.time;
            FlyStyleTime = GlideChance.RandomValue;
            WaterCollider = new Collider[1];
            WaterLayer = LayerMask.GetMask("Water");
        } 

        public override void AnimationStateEnter()
        {
            if (InMainTagHash)
            {
                verticalInertia = MalbersTools.CleanUpVector(animal.DeltaPos, animal.Forward, animal.Right);
                acceleration = 0;
                animal.LastState = this; //IMPORTANT for Modes that changes the Last state enter

                animal.InertiaPositionSpeed = Vector3.ProjectOnPlane(animal.DeltaPos, animal.Up); //Keep the Speed from the take off
                animal.currentSpeedModifier.Vertical = 2;

                if (GlideOnly)
                {
                    animal.UseSprintState = false;
                    animal.Speed_Change_Lock(true);
                }
            }
        }

        public override void OnStateMove(float deltatime)
        {
            if (InMainTagHash) //While is flying
            { 
                if (General.FreeMovement)
                    animal.FreeMovementRotator(Ylimit, Bank, deltatime);

                if (GlideOnly && ActiveState)
                {
                    if (animal.UpDownSmooth > 0) animal.UpDownSmooth = 0;
                  
                }
                else if (AutoGlide) AutoGliding();
             

                if (AvoidWater)
                {
                    var WaterPos = transform.position + animal.AdditivePosition;
                    var Dist = WDistance * animal.ScaleFactor;
                    RaycastHit hit;
                    Color findWater = Color.gray;
                   
                    
                    if (Physics.Raycast(WaterPos, animal.GravityDirection, out hit, Dist, WaterLayer))
                    {
                        findWater = Color.cyan;

                        if (animal.MovementAxis.y < 0) animal.MovementAxis.y = 0;

                        if (hit.distance < Dist*0.75f)
                        {
                            animal.AdditivePosition += animal.GravityDirection * -(Dist * 0.75f - hit.distance);
                        }

                    }
                    if (debug) Debug.DrawRay(WaterPos, animal.GravityDirection * Dist, findWater);
                    
                    return;
                }

                if (InertiaTime > 0) animal.AddInertia(ref verticalInertia, deltatime * InertiaTime);
                GravityAceleration(deltatime);
            }
        }


        void GravityAceleration(float deltaTime)
        {
            var Gravity = animal.GravityDirection;
            //Add more speed when going Down
            float downAcceleration = DownAcceleration * animal.ScaleFactor;

            if (animal.UpDownSmooth < -0.001f)
            {
                animal.AdditivePosition += (Gravity * downAcceleration * deltaTime * acceleration);      //Add Gravity if is in use
                acceleration += downAcceleration * deltaTime;

                acceleration = Mathf.Lerp(acceleration, acceleration + downAcceleration, deltaTime);
            }
            else
            {
                acceleration = Mathf.MoveTowards(acceleration, 0, deltaTime * 2);                  //Deacelerate slowly all the acceleration you earned..
            }

            animal.AdditivePosition += animal.GravityDirection * acceleration * deltaTime;
            animal.AdditivePosition += transform.forward * acceleration * deltaTime;

            if (GravityDrag > 0)
            {
                animal.AdditivePosition += animal.GravityDirection * (GravityDrag * animal.ScaleFactor / 2) * deltaTime;
            }
        }

        void AutoGliding()
        {
            if (Time.time - FlyStyleTime >= currentTime)
            {
                currentTime = Time.time;
                isGliding = !isGliding;

                FlyStyleTime = isGliding ? GlideChance.RandomValue : FlapChange.RandomValue;

                animal.currentSpeedModifier.Vertical = isGliding ? 2 : Random.Range(1f, 1.5f);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            verticalInertia = Vector3.zero;
            animal.FreeMovement = false;
            acceleration = 0;
            isGliding = false;
            animal.Speed_Change_Lock(false);
        }


        public override void TryExitState(float DeltaTime)
        {
            if (canLand)
            {
                var MainPivot = animal.Main_Pivot_Point + animal.AdditivePosition;

                float LandDistance = animal.Height * LandMultiplier * animal.ScaleFactor;

                if (debug) Debug.DrawRay(MainPivot, animal.GravityDirection * LandDistance, Color.yellow);

                bool Hit = Physics.RaycastNonAlloc(MainPivot, animal.GravityDirection, LandHit, LandDistance, animal.GroundLayer, QueryTriggerInteraction.Ignore) > 0;

                if (Hit)
                {
                    if (LandHit[0].distance < LandDistance) animal.Grounded = true; //Means the animal is touching the ground

                    AllowExit();  //Let the other states to awake (Ground)
                    return;
                }
            }

            switch (flyInput)
            {
                case FlyInput.Toggle:
                    if (ActiveState && InputValue)
                    {
                        AllowExit();
                        InputValue = false;
                    }
                    break;
                case FlyInput.Press:
                    if (ActiveState && !InputValue)
                    {
                        AllowExit();
                    }
                    break;
                case FlyInput.None:
                    break;
                default:
                    break;
            }
        }


#if UNITY_EDITOR
        public override void Reset()
        {
            ID = Resources.Load<StateID>("StatesID/Fly");
            Input = "Fly";

            General = new AnimalModifier()
            {
                RootMotion = true,
                Grounded = false,
                Sprint = true,
                OrientToGround = false,
                CustomRotation = false,
                IgnoreLowerStates = true,
                Gravity = false,
                modify = (modifier)(-1),
                AdditivePosition = true,
                Colliders = true,
                //KeepInertia = true
            };
        }


        //public override void DebugState()
        //{
        //    if (Application.isPlaying)
        //    {
        //        var scale = animal.ScaleFactor;
        //        var WaterPos = transform.position + animal.AdditivePosition + (animal.GravityDirection * WDistance);

        //        Gizmos.color = IsInWater ? Color.blue : Color.gray;
        //        Gizmos.DrawSphere(WaterPos, WRadius * scale);
        //        Gizmos.DrawWireSphere(WaterPos, WRadius * scale);
        //    }
        //}

#endif
    }
}
