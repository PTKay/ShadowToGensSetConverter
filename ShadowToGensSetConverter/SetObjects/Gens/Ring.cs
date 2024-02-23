using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    [XmlRoot("Ring")]
    public class Ring : SetObjectGens
    {
        public bool InitDisp = false;
        public bool IsLightSpeedDashTarget = false;
        public bool IsReset = false;
        public float ResetTime = 0;
        public int TreasureSearchHideType = 0;
    }
}
