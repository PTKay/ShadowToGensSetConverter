using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Light
{
    public class SonicLightXml
    {
        public string LightType = "Omni";
        public Position Position = new Position();
        public Color Color = new Color() { };
        public OmniLightRange OmniLightRange = new OmniLightRange();
    }
}
