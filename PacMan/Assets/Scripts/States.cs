using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp476A3
{
    public enum PlayerState
    {
        NORMAL, SPEEDUP, STOP, WAITING
    };
    public enum PlayerDirection
    {
        UP, DOWN, LEFT, RIGHT
    };
    public enum GameState
    {
        WAITING, PLAYING, GAMEOVER
    };
    public enum GhostState
    {
        WANDER, LERPING, NAVIGATING, WAITING
    };
}
