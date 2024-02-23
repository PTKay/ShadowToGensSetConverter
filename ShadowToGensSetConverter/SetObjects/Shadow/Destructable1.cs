using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.SetObjects.Shadow
{
    public class Destructable1 : SetObjectShadow
    {
        public int DestructableType;

        public override void ReadMiscSettings(BinaryReader reader)
        {
            DestructableType = reader.ReadInt32();
        }
    }
}
