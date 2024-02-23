using ShadowToGensSetConverter.SetObjects.Common;
using ShadowToGensSetConverter.SetObjects.Gens.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShadowToGensSetConverter.SetObjects.Gens
{
    public class ObjectPhysics : SetObjectGens
    {
        public float AddRange = 0;
        public float CullingRange = 15;
        public Position DebrisTarget = new Position();
        public float GroundOffset = 0;
        public bool IsDynamic = false;
        public bool IsReset = false;
        public string Type;
        public WrappedObjectId WrappedObjectID = new WrappedObjectId();

        [XmlIgnore]
        public SetParticle Particle;

        public ObjectPhysics(string Type)
        {
            this.Type = Type;
        }

        public ObjectPhysics() { }

        public ObjectPhysics WithParticle(SetParticle particle) { 
            Particle = particle;
            return this;
        }
    }
}
