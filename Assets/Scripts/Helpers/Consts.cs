
public struct Tags
{
    public const string TAG_SEED = "Seed";
    public const string TAG_THORN = "Thorn";
    public const string TAG_BOMB = "Bomb";
    public const string TAG_BG = "Background";
    public const string TAG_COIN = "Coin";
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
}
public struct AnimatorVariables
{
    public const string AST_DISAPPEAR = "Disappear";
    public const string AST_RESET = "Reset";
    public const string P_BACKTOFLY = "BackToFly";
    public const string P_DAMAGE = "Damage";
}
public enum GameState
{
    Initialization, // Level yükleniyor
    Investment,     // Kuþ koþuyor, kapýlardan geçiyor (Scroll aktif)
    Reward,         // Kart seçimi (Scroll durdu, UI açýk)
    Combat,         // Sýra tabanlý savaþ (Scroll durdu, Player/Enemy etkileþimi)
    LevelEnd        // Level bitti (Win/Lose)
}
public struct SceneName
{
    public const string SN_PLAY = "Play";
    public const string SN_MENU = "Menu";
    public const string SN_CHOOSE_CHARACTER = "ChooseCharacter";

}



