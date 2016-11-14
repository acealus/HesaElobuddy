﻿namespace NechritoRiven.Event.OrbwalkingModes
{
    #region

    using Core;
    using EloBuddy.SDK;
    using System.Linq;

    #endregion

    internal class LaneclearMode : NechritoRiven.Core.Core
    {
        #region Public Methods and Operators

        public static void Laneclear()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions( EntityManager.UnitTeam.Enemy, Player.Position, Player.AttackRange + 380).ToList();

            if (minions == null || (MenuConfig.LaneEnemy && Player.CountEnemiesInRange(1500) > 0))// || Player.IsWindingUp)
            {
                return;
            }

            if (minions.Count <= 2)
            {
                return;
            }

            foreach (var m in minions)
            {
                if (m.IsUnderEnemyturret())
                {
                    return;
                }
                if (Spells.E.IsReady() && MenuConfig.LaneE)
                {
                    BackgroundData.CastE(m);
                }
                if (MenuConfig.LaneQFast && m.Health < Spells.Q.GetSpellDamage(m) && Spells.Q.IsReady())
                {
                    BackgroundData.CastQ(m);
                }
               else if (Spells.W.IsReady()
                    && MenuConfig.LaneW 
                    //&& !Player.IsWindingUp
                    && !(m.Health > Spells.W.GetSpellDamage(m))
                    && BackgroundData.InRange(m))
                {
                    BackgroundData.CastW(m);
                }
            }
        }

        #endregion
    }
}
