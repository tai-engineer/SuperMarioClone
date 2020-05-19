using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : Singleton<EventsManager>
{
    #region Musroom Events
    public delegate void MushroomCollisionEvent(Mushroom.PowerUpType type);
    #endregion
}
