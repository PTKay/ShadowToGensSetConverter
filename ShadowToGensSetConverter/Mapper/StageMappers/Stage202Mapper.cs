using ShadowToGensSetConverter.SetObjects.Common;
using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Mapper.StageMappers
{
    class Stage202Mapper : StageMapper
    {

        public override SetObjectGens MapDestructableToGens(Destructable1 shadowSet)
        {
            switch (shadowSet.DestructableType)
            {
                case 0: return new ObjectPhysics("csc_obj_barricadeA");
                case 1:
                    shadowSet.Position.y += 6f;
                    return new ObjectPhysics("Bpc_obj_redcar").WithParticle(new SetParticle("ef_st_csc_yh1_bg_fire_a1") { EffecScale = 0.65f });
                case 2:
                    shadowSet.Position.y += 7f;
                    return new ObjectPhysics("Bpc_obj_taxi").WithParticle(new SetParticle("ef_st_csc_yh1_bg_fire_a1") { EffecScale = 0.65f });
                case 7: return new ObjectPhysics("csc_obj_roadconeB");
                case 13:
                    shadowSet.Position.y -= 5f;

                    if (Math.Round(shadowSet.Rotation.x) == 45 && Math.Round(shadowSet.Rotation.y) == 45 && Math.Round(shadowSet.Rotation.z) == -30)
                    {
                        shadowSet.Rotation = new Rotation()
                        {
                            x = -20,
                            y = -24,
                            z = -48
                        };
                    }
                    else if (Math.Round(shadowSet.Rotation.x) == -45 && Math.Round(shadowSet.Rotation.y) == -135 && Math.Round(shadowSet.Rotation.z) == -30)
                    {
                        shadowSet.Rotation = new Rotation()
                        {
                            x = 20,
                            y = -66,
                            z = -48
                        };
                    }
                    else
                    {
                        shadowSet.Rotation.y -= 90; // Rotate Y
                        var a = shadowSet.Rotation.x;
                        shadowSet.Rotation.x = shadowSet.Rotation.z;
                        shadowSet.Rotation.z = -a;
                    }

                    return new ObjectPhysics("sph_obj_roketglassB");
                case 19: return new ObjectPhysics("sph_obj_drumA");
                case 20:
                    shadowSet.Position.y += 12f;
                    shadowSet.Rotation.y += 180; // Rotate Y
                    shadowSet.Rotation.z *= -1;
                    return new ObjectPhysics("sph_obj_guardrailA");
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

        public override SetObjectGens MapDecorationToGens(Decoration1 shadowSet)
        {
            switch (shadowSet.DecorationType)
            {
                case 0:
                    shadowSet.Rotation.y += 180; // Rotate Y
                    shadowSet.Position.y -= 50f;
                    return new ObjectPhysics("cte_obj_signal")
                    {
                        IsCastShadow = false
                    };
            }

            return null;
        }
    }
}
