using ShadowToGensSetConverter.Helpers;
using ShadowToGensSetConverter.SetObjects;
using ShadowToGensSetConverter.SetObjects.Common;
using ShadowToGensSetConverter.SetObjects.Gens;
using ShadowToGensSetConverter.SetObjects.Gens.Elements;
using ShadowToGensSetConverter.SetObjects.Shadow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Mapper
{
    public class ShadowToGensSetObjectMapper
    {

        private static Dictionary<string, Func<SetObjectShadow, SetObjectGens>> MappingDictionary = new Dictionary<string, Func<SetObjectShadow, SetObjectGens>>()
        {
            { "0001_Spring", (shadowSet) => {
                SetObjects.Shadow.Spring spring = shadowSet as SetObjects.Shadow.Spring;

                SetObjects.Gens.Spring springGens = new SetObjects.Gens.Spring();
                springGens.FirstSpeed = spring.Strength * 11;
                springGens.OutOfControl = spring.NoControlTime;

                return springGens;
            } },

            { "0002_Long-Spring", (shadowSet) => {
                shadowSet.Rotation.y += 180; // Rotate Y
                shadowSet.Position.z += 15f;
                SetObjects.Shadow.LongSpring spring = shadowSet as SetObjects.Shadow.LongSpring;

                SetObjects.Gens.WideSpring springGens = new SetObjects.Gens.WideSpring();
                springGens.FirstSpeed = spring.Strength * 12;
                springGens.OutOfControl = spring.NoControlTime;

                return springGens;
            } },

            { "0004_Dash-Ramp", (shadowSet) => {
                if (shadowSet.Rotation.y != 0) {
                    shadowSet.Rotation.y -= 180;
                }
                DashRamp dashRamp = shadowSet as DashRamp;

                JumpBoard3D jumpBoard = new JumpBoard3D();
                jumpBoard.SetImpulse(dashRamp.Strength * 11.15f);
                jumpBoard.OutOfControl = dashRamp.NoControlTime;

                return jumpBoard;
            } },

            { "0003_Dash-Panel", (shadowSet) => {
                shadowSet.Rotation.y -= 180; // Rotate Y
                SetObjects.Shadow.DashPanel dashPanel = shadowSet as SetObjects.Shadow.DashPanel;

                SetObjects.Gens.DashPanel gensDashPanel = new SetObjects.Gens.DashPanel();

                gensDashPanel.Speed = dashPanel.Strength * 17;
                gensDashPanel.OutOfControl = dashPanel.NoControlTime;

                return gensDashPanel;
            } },
            { "0009_Wood-Box", (shadowSet) => new ObjectPhysics("cte_obj_woodboxA") },
            { "000B_Unbreakable-Box", (shadowSet) => new ObjectPhysics("IronBox") },
            { "000C_Weapon-Box", (shadowSet) => new ObjectPhysics("cte_obj_woodboxA") },
            { "0010_Rings", (shadowSet) => {
                Rings rings = shadowSet as Rings;
                Ring ring = new Ring();

                if (rings.NumberOfRings > 1)
                {
                    MultiSetParam param = new MultiSetParam();
                    Position currentPosition = rings.Position;
                    for (int i = 1; i < rings.NumberOfRings; ++i)
                    {
                        Element elem = new Element();
                        elem.Index = i;
                        elem.Rotation = rings.Rotation;

                        elem.Position = VectorOperations.CalculateNextPosition(currentPosition, rings.Rotation, -(rings.LengthRadius * i / rings.NumberOfRings));;

                        param.Elements.Add(elem);
                    }

                    ring.MultiSetParam = param;
                }

                return ring;
            } },
            { "0022_Red-Fruit", (shadowSet) => {
                shadowSet.Position.y += 10f;
                return new Bomb();
            } },
            { "0037_Cage", (shadowSet) => new ObjectPhysics("IronBox") },
            { "003A_Special-Weapon-Box", (shadowSet) => new ObjectPhysics("cte_obj_woodboxA") },
            { "0064_GUN-Soldier", (shadowSet) => new EnemyGunHunter3D() },
            { "0065_GUN-Beetle", (shadowSet) => new EnemyGunBeetle3D() },
            { "008D_Black-Arms-Warrior-(Soldier)", (shadowSet) => new EnemyBiter3D() },
            { "008E_Black-Arms-Hawk", (shadowSet) => {
                EnemyTaker3D taker = new EnemyTaker3D();

                taker.PositionListMove.Add(shadowSet.Position);

                return taker;
            } },
            { "2588_Decoration1", (shadowSet) => {
                Decoration1 decoration = shadowSet as Decoration1;
                switch(decoration.DecorationType)
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
            } },
            { "2589_Destructable1", (shadowSet) => {
                Destructable1 destructable = shadowSet as Destructable1;
                switch(destructable.DestructableType)
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

                        if (Math.Round(shadowSet.Rotation.x) == 45 && Math.Round(shadowSet.Rotation.y) == 45 && Math.Round(shadowSet.Rotation.z)== -30) {
                            shadowSet.Rotation = new Rotation()
                            {
                                x = -20,
                                y = -24,
                                z = -48
                            };
                        }
                        else if (Math.Round(shadowSet.Rotation.x) == -45 && Math.Round(shadowSet.Rotation.y) == -135 && Math.Round(shadowSet.Rotation.z) == -30) {
                            shadowSet.Rotation = new Rotation()
                            {
                                x = 20,
                                y = -66,
                                z = -48
                            };
                        } else
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
            } },
            { "258A_Effect1", (shadowSet) => {
                Effect1 effect = shadowSet as Effect1;
                SetParticle toReturn = null;

                switch(effect.EfectType)
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
            } },
            { "0014_Goal-Ring", (shadowSet) => {
                return new GoalRing();
            } }
        };

        public static List<SetObjectGens> MapToGens(SetObjectShadow setObject)
        {

            Func<SetObjectShadow, SetObjectGens> setObjectGensGetter = MappingDictionary.GetValueOrDefault(setObject.Name);

            if (setObjectGensGetter == null)
            {
                return null;
            }

            SetObjectGens setObjectGens = setObjectGensGetter.Invoke(setObject);
            if (setObjectGens == null)
            {
                return null;
            }

            setObjectGens.Position = setObject.Position;
            setObjectGens.Rotation = setObject.Rotation;
            ConvertToQuaternion(setObjectGens);

            if (setObjectGens.GetType() == typeof(ObjectPhysics))
            {
                ObjectPhysics obj = setObjectGens as ObjectPhysics;
                if (obj.Particle != null && (obj.Type.Contains("taxi") || obj.Type.Contains("car")))
                {
                    // Logic specific to cars, so that particle shows up in the hood
                    float offsetX = 15f;
                    obj.Particle.Position = VectorOperations.CalculateNextPosition(setObject.Position, setObject.Rotation, offsetX);
                    obj.Particle.Rotation = new Rotation() { x = setObject.Rotation.x, y = setObject.Rotation.y, z = setObject.Rotation.z };
                    ConvertToQuaternion(obj.Particle);

                    return [setObjectGens, obj.Particle];
                }
            }

            return [setObjectGens];
        }

        private static void ConvertToQuaternion(SetObjectGens setObjectGens)
        {
            setObjectGens.Rotation = VectorOperations.ToQuaternion(setObjectGens.Rotation);
            if (setObjectGens.MultiSetParam != null)
            {
                foreach (var multiSetElement in setObjectGens.MultiSetParam.Elements)
                {
                    multiSetElement.Rotation = VectorOperations.ToQuaternion(multiSetElement.Rotation);
                }
            }
        }
    }
}
