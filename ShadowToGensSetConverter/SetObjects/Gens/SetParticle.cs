using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class SetParticle : SetObjectGens
    {
        public float BootType = 0;
        public float CullingRadius = 10;
        public float EffColScaleA = 1;
        public float EffColScaleB = 1;
        public float EffColScaleG = 1;
        public float EffColScaleR = 1;
        public float EffecScale = 2;
        public string EffectName;
        public float EffectScaleX = 1;
        public float EffectScaleY = 1;
        public float EffectScaleZ = 1;
        public float EmissionPosType = 0;
        public bool NoStopEvent = true;
        public string SeName = "4002_ring";
        public bool UseSE = false;
        public float effectScaleType = 0;

        public SetParticle(string particleName)
        {
            IsCastShadow = false;
            EffectName = particleName;
        }

        public SetParticle() { }
    }
}
