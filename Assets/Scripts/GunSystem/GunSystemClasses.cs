using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GunSystem
{
    public enum GunType
    {
        Bladed,
        Firearms,
        Explosive,
        Energy,
        Projectile,
        Siege,
        ModernSpecialWeapon,
        Utility
    }
    public enum BladedType
    {
        Sword,
        Knives,
        Axe,
        Spear,
        Halberd,
        Mace,
        Morningstar,
        Dagger
    }
    public enum FirearmsType
    {
        Pistol,
        Rifle,
        Shotgun,
        SMG,
        LMG,
        Sinper
    }
    public enum ExplosiveType
    {
        Bomb,
        Grenade,
        Missile,
        LandMine,
        SeaMine,
        IED
    }
    public enum EnergyType
    {
        Laser,
        Plasma,
        Electromagnetic
    }
    public enum ProjectileType
    {
        Bows,
        Slingshot,
        ThrowingWeapon
    }
    public enum SiegeType
    {
        Catapult,
        Ballistae,
        Cannon
    }
    public enum ModernSpecialWeaponType
    {
        NonLethal,
        Tactical
    }
}
