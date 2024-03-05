using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Mapper.StageMappers
{
    class Stage100Mapper : StageMapper
    {
        public override SetObjectGens MapDecorationToGens(Decoration1 shadowObject)
        {
            return null;
        }

        public override SetObjectGens MapDestructableToGens(Destructable1 shadowObject)
        {
            switch (shadowObject.DestructableType)
            {
                case 3:
                    shadowObject.Rotation.y += 90; // Rotate Y
                    return new ObjectPhysics("nyc_obj_bj_bench");
                case 7: return new ObjectPhysics("cte_obj_trashbox");
                case 9:
                    shadowObject.Rotation.y += 180; // Rotate Y
                    return new ObjectPhysics("cte_obj_paperbox");
                case 10: return new ObjectPhysics("csc_obj_barricadeA");
                case 16: return new ObjectPhysics("cte_obj_telbox");
                case 17:
                    shadowObject.Position.y += 7f;
                    return new ObjectPhysics("Bpc_obj_taxi").WithParticle(new SetParticle("ef_st_csc_yh1_bg_fire_a1") { EffecScale = 0.65f });
                case 18:
                    shadowObject.Position.y += 6f;
                    return new ObjectPhysics("Bpc_obj_redcar").WithParticle(new SetParticle("ef_st_csc_yh1_bg_fire_a1") { EffecScale = 0.65f });
                case 19:
                    shadowObject.Rotation.y += 180; // Rotate Y
                    shadowObject.Position.y -= 50f;
                    return new ObjectPhysics("cte_obj_signal")
                    {
                        IsCastShadow = false
                    };
                case 21: return new ObjectPhysics("csc_obj_roadconeB");
                case 22: return new ObjectPhysics("cte_obj_tree");
            }

            return null;
        }

        public override SetObjectGens MapEffectToGens(Effect1 effect)
        {
            SetParticle toReturn = null;

            switch (effect.EfectType)
            {
                case 30: toReturn = new SetParticle("ef_st_csc_yh1_bg_fire_a1"); break;
                case 91: toReturn = new SetParticle("ef_st_csc_yh1_bg_fire_a2"); break;
                case 92: toReturn = new SetParticle("ef_st_csc_yh1_bg_fire_b1"); break;
                case 94: toReturn = new SetParticle("ef_st_csc_yh1_bg_smoke_a1"); break;
            }

            if (toReturn != null)
            {
                toReturn.EffectScaleX = effect.ScaleX;
                toReturn.EffectScaleY = effect.ScaleY;
                toReturn.EffectScaleZ = effect.ScaleZ;
                toReturn.EffecScale = 1;
            }

            return toReturn;
        }
    }
}
