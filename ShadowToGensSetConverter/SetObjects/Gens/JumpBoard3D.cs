using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class JumpBoard3D : SetObjectGens
    {
        public float ImpulseSpeedOnBoost = 43;
        public float ImpulseSpeedOnNormal = 43;
        public bool IsTo3D = false;
        public bool LookBack = false;
        public float OutOfControl = 1;
        public bool RigidBody = true;
        public int SizeType = 1;

        public void SetImpulse(float impulse)
        {
            ImpulseSpeedOnNormal = impulse;
            ImpulseSpeedOnBoost = impulse;
        }
    }
}
