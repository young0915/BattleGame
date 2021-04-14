public enum EmClickState
{
    Ok,
    Cancel,
}

public enum EmCanvasLayer
{
    Layer1,
    Layer2,
    Overlay
}

// CUIAction 사용하는 타입.
public enum EmUIActionType
{
    GameOver=1,
    Victory,
    NicknameNotallowed =4,
    lackInventory,
    lackMoney,
    HeroLack,
    PossessionProduct,
    NoSeatsAvailable,
    Unreinforceable,
    None = 0
}

public enum EmCameraType
{
    Start,
    Lobby,
    Battle
}

public enum EmCellState
{
    NotComplete,
   Complete
}

public enum EmMonPos
{
    One,
    Two,
    Three
}

public enum EmDBType
{
    Monster=1,
    Hero,
    Tower,
    Item,
    Inven,
    None = 0
}

public enum EmObjectPoolType
{
    Bullet,
}

public enum EmItem
{
    Hero =1,
    Tower=2,
    Etc=3,
    None=0
}

public enum EmHeroType
{
    Striker=1,                                           // 돌격형.
    Penetration,                                    // 침투형.
    Recovery,                                       //  회복형.
    None =0                                       //  없음.
}

public enum EmTowerType
{
    Archer=1,
    Barracks,
    Cannon,
    Mage,
    None=0
}


public enum EmCharacterAction
{
    Idle=0,
    Attack,
    Skill,
    GetHit,
    Victory,
    Walk,
    Die
}

public enum EmInven
{
    Exist = 1,
    None =0
}

