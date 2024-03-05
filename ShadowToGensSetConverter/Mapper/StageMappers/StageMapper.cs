using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Mapper.StageMappers
{
    public abstract class StageMapper
    {
        public static StageMapper GetSpecificStageMapper(string stageName)
        {
            switch (stageName)
            {
                case "stg0202":
                    return new Stage202Mapper();
                case "stg0100":
                    return new Stage100Mapper();
                default:
                    return null;
            }
        }

        public abstract SetObjectGens MapDestructableToGens(Destructable1 shadowObject);
        public abstract SetObjectGens MapEffectToGens(Effect1 shadowObject);
        public abstract SetObjectGens MapDecorationToGens(Decoration1 shadowObject);
    }
}
