using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class BlueGuyController : ToyController
{
    private const float BLUEGUY_FULL_POWER = 2000;
    private const float BLUEGUY_MAX_VELOCITY = 35;
    private const float BLUEGUY_DECAY_LIFETIME = 20;
    private readonly float BLUEGUY_WIND_DOWN_RATE = 10;

    protected override float DECAY_LIFETIME
    {
        get
        {
            return BLUEGUY_DECAY_LIFETIME;
        }
    }

    protected override float MAX_VELOCITY
    {
        get
        {
            return BLUEGUY_MAX_VELOCITY;
        }
    }

    protected override float FULL_POWER
    {
        get
        {
            return BLUEGUY_FULL_POWER;
        }
    }

    protected override float WIND_DOWN_RATE
    {
        get
        {
            return BLUEGUY_WIND_DOWN_RATE;
        }
    }
}

