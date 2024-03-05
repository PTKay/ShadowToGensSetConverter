using ShadowToGensSetConverter.Helpers;
using ShadowToGensSetConverter.Mapper.StageMappers;
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
            { "000A_Metal-Box", (shadowSet) => new ObjectPhysics("IronBox") },
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
            { "001B_Roadblock", (shadowSet) => new ObjectPhysics("csc_obj_barricadeB")},
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
            { "0014_Goal-Ring", (shadowSet) => {
                return new GoalRing();
            } }
        };

        public static List<SetObjectGens> MapToGens(string stageName, SetObjectShadow setObject)
        {
            SetObjectGens setObjectGens = null;
            
            switch(setObject.Name)
            {
                case "2589_Destructable1":
                    // If it's a destructable, we use the stage specific mapper
                    setObjectGens = StageMapper.GetSpecificStageMapper(stageName)?.MapDestructableToGens(setObject as Destructable1);
                    break;
                case "258A_Effect1":
                    // If it's an effect, we use the stage specific mapper
                    setObjectGens = StageMapper.GetSpecificStageMapper(stageName)?.MapEffectToGens(setObject as Effect1);
                    break;
                case "2588_Decoration1":
                    // If it's a decoration, we use the stage specific mapper
                    setObjectGens = StageMapper.GetSpecificStageMapper(stageName)?.MapDecorationToGens(setObject as Decoration1);
                    break;
                default:
                    Func<SetObjectShadow, SetObjectGens> setObjectGensGetter = MappingDictionary.GetValueOrDefault(setObject.Name);
                    setObjectGens = setObjectGensGetter?.Invoke(setObject);
                    break;
            }


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
