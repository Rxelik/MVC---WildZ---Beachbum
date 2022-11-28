using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBehavior
{
    public class PlayerPlayedCardEventArgs { }
    public class PlayerDrawCardEventArgs { }
    public class EnemyPlayedCardEventArgs { }
    public class EnemyDrawedCardEventArgs { }


    public event EventHandler<PlayerPlayedCardEventArgs> PlayerUsedCard = (sender, e) => { };
    public event EventHandler<PlayerDrawCardEventArgs>   PlayerDrawCard = (sender, e) => { };
    public event EventHandler<EnemyPlayedCardEventArgs>  EnemyUsedCard = (sender, e) => { };
    public event EventHandler<EnemyDrawedCardEventArgs>  EnemyDrawCard = (sender, e) => { };

}
