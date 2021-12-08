using UnityEngine;
namespace Kamaii
{
    public enum ItemID
    {
        COIN
    }
    public class Items : MonoBehaviour
    {
        public Sprite[] m_ItemSprites;
        public int[] m_ItemStacks;
        public string[] m_ItemNames;
        public string[] m_ItemDescriptions;
        public static Sprite[] ItemSprites { get; private set; }
        public static int[] ItemStacks { get; private set; }
        public static string[] ItemNames { get; private set; }
        public static string[] ItemDescriptions { get; private set; }
        public void Awake()
        {
            ItemSprites = m_ItemSprites;
            ItemStacks = m_ItemStacks;
            ItemNames = m_ItemNames;
            ItemDescriptions = m_ItemDescriptions;
        }
        public static Sprite GetSprite(ItemID id)
        {
            return ItemSprites[(int)id];
        }
        public static int GetMaxStack(ItemID id)
        {
            return ItemStacks[(int)id];
        }
    }
}