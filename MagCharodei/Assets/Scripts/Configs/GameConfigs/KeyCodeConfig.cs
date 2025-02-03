using UnityEngine;

namespace Configs.GameConfigs
{
    [CreateAssetMenu(fileName = "KeyCodeConfig", menuName = "Configs/Game Configs/KeyCode Config")]
    public class KeyCodeConfig : ScriptableObject
    {
        [SerializeField] private KeyCode m_Up = KeyCode.UpArrow;
        public KeyCode Up => m_Up;
        
        [SerializeField] private KeyCode m_Down = KeyCode.DownArrow;
        public KeyCode Down => m_Down;
        
        [SerializeField] private KeyCode m_Left = KeyCode.LeftArrow;
        public KeyCode Left => m_Left;
        
        [SerializeField] private KeyCode m_Right = KeyCode.RightArrow;
        public KeyCode Right => m_Right;
        
        [SerializeField] private KeyCode m_PreviousSpell = KeyCode.Q;
        public KeyCode PreviousSpell => m_PreviousSpell;
        
        [SerializeField] private KeyCode m_NextSpell = KeyCode.W;
        public KeyCode NextSpell => m_NextSpell;
        
        [SerializeField] private KeyCode m_CastSpell = KeyCode.X;
        public KeyCode CastSpell => m_CastSpell;
    }
}