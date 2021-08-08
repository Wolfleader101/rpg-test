using UnityEngine;

namespace ScriptableObjects.Enemies
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public abstract class BaseEnemy: ScriptableObject
    {
        
        [SerializeField] private string enemyName;
        public string Name => enemyName;
        

        [SerializeField] private float maxHealth = 100;
        public float MaxHealth => maxHealth;


        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

    }
}