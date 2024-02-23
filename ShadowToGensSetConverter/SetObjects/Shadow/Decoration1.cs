using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    public class Decoration1 : SetObjectShadow
    {
        public int DecorationType;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            DecorationType = reader.ReadInt32();
        }
    }
}
