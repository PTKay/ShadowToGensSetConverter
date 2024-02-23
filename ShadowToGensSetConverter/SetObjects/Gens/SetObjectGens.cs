using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ShadowToGensSetConverter.SetObjects.Common;
using ShadowToGensSetConverter.SetObjects.Gens.Elements;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public abstract class SetObjectGens : SetObject
    {
        public int SetObjectID = 0;
        public float GroundOffset = 0;
        public int Range = 500;
        public bool IsCastShadow = true;

        public MultiSetParam MultiSetParam = null;
    }
}
