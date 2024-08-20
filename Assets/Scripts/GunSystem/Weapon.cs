using UnityEngine;
using UnityEditor;

namespace Game.GunSystem
{
    [CreateAssetMenu(menuName = "Game/Gun", fileName = "Weapon", order = 3)]
    public class Waeapon : ScriptableObject
    {
        // Name of the waeapon
        public string w_name = null;
        // An easy description of the weapon
        public string w_description = null;
        // Tipology of the weapon
        public GunType w_type = GunType.Firearms;

        //Different types for every typology
        public BladedType w_bladedType;
        public FirearmsType w_firearmsType;
        public ExplosiveType w_explosiveType;
        public EnergyType w_energyType;
        public ProjectileType w_projectuleType;
        public SiegeType w_siegeType;
        public ModernSpecialWeaponType w_modernSpecialWeaponType;

        // Damage of the gun, it depends of the typology of the weapon, for example:
        // For the fireamrs is damage per bullet, for bladed like sword it's damage per penetratation?
        public float damage;
        // The cool down is
        public float coolDown;
        // Reload times for every projectile weapon, and firearm
        public float reloadTime;
        // The range depends of the typology of the weapon, for example:
        // For the firearms the range per bullet, for blanded like swors is the max distance that the enemy/objective
        // needs to be near to get damage
        public float range;
        // The weigh of the Weapon, it changes the waling and running speed
        public float weight;
        // The durabilty of a weapon, it means that the weapon will need or change totally of weapon for swords or axes, or needs maintenance
        // for firearms or mines
        public float durability;
        // the radius of the explosion, it's necesary for explosion physics, and damage. This feature is only for explosives
        public float blastRadius;
        // Added for explosives, the time before the explosive explodes, for bombs and IED
        public float fuseTime;
        // Added for energy weapons, the energy that the weapon will consume in a minute
        public float energyConsumption;
        // Added for energy weapons, the time to charge to full the energy weapon
        public float chargeTime;
        // Added for projectiles, the speed of the projectile
        public float projectileVelocity;
        // Cadence is the quantity of bullets in a second, 
        public float cadence;
    }

    [CustomEditor(typeof(Waeapon))]
    public class WaeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Waeapon weapon = (Waeapon)target;

            // General weapon properties
            EditorGUILayout.LabelField("Weapon Name", weapon.w_name);
            weapon.w_name = EditorGUILayout.TextField("Name", weapon.w_name);

            EditorGUILayout.LabelField("Weapon Description", weapon.w_description);
            weapon.w_description = EditorGUILayout.TextField("Description", weapon.w_description);

            EditorGUILayout.LabelField("Weapon Type", EditorStyles.boldLabel);
            weapon.w_type = (GunType)EditorGUILayout.EnumPopup("Gun Type", weapon.w_type);

            // Display fields based on weapon type
            switch (weapon.w_type)
            {
                case GunType.Bladed:
                    weapon.w_bladedType = (BladedType)EditorGUILayout.EnumPopup("Bladed Type", weapon.w_bladedType);
                    break;
                case GunType.Firearms:
                    weapon.w_firearmsType = (FirearmsType)EditorGUILayout.EnumPopup("Firearms Type", weapon.w_firearmsType);
                    break;
                case GunType.Explosive:
                    weapon.w_explosiveType = (ExplosiveType)EditorGUILayout.EnumPopup("Explosive Type", weapon.w_explosiveType);
                    break;
                case GunType.Energy:
                    weapon.w_energyType = (EnergyType)EditorGUILayout.EnumPopup("Energy Type", weapon.w_energyType);
                    break;
                case GunType.Projectile:
                    weapon.w_projectuleType = (ProjectileType)EditorGUILayout.EnumPopup("Projectile Type", weapon.w_projectuleType);
                    break;
                case GunType.Siege:
                    weapon.w_siegeType = (SiegeType)EditorGUILayout.EnumPopup("Siege Type", weapon.w_siegeType);
                    break;
                case GunType.ModernSpecialWeapon:
                    weapon.w_modernSpecialWeaponType = (ModernSpecialWeaponType)EditorGUILayout.EnumPopup("Modern Special Weapon Type", weapon.w_modernSpecialWeaponType);
                    break;
            }

            // Common fields for all weapons
            weapon.damage = EditorGUILayout.FloatField("Damage", weapon.damage);
            weapon.coolDown = EditorGUILayout.FloatField("Cool Down", weapon.coolDown);
            weapon.reloadTime = EditorGUILayout.FloatField("Reload Time", weapon.reloadTime);
            weapon.range = EditorGUILayout.FloatField("Range", weapon.range);
            weapon.weight = EditorGUILayout.FloatField("Weight", weapon.weight);
            weapon.durability = EditorGUILayout.FloatField("Durability", weapon.durability);

            // Conditional fields based on weapon type
            if (weapon.w_type == GunType.Explosive)
            {
                weapon.blastRadius = EditorGUILayout.FloatField("Blast Radius", weapon.blastRadius);
                weapon.fuseTime = EditorGUILayout.FloatField("Fuse Time", weapon.fuseTime);
            }

            if (weapon.w_type == GunType.Energy)
            {
                weapon.energyConsumption = EditorGUILayout.FloatField("Energy Consumption", weapon.energyConsumption);
                weapon.chargeTime = EditorGUILayout.FloatField("Charge Time", weapon.chargeTime);
            }

            if (weapon.w_type == GunType.Projectile)
            {
                weapon.projectileVelocity = EditorGUILayout.FloatField("Projectile Velocity", weapon.projectileVelocity);
            }

            if (weapon.w_type == GunType.Firearms)
            {
                weapon.cadence = EditorGUILayout.FloatField("Cadence", weapon.cadence);
            }

            // Save any changes made to the inspector
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
