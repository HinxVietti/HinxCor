using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HinxCor.Unity.UI
{

public struct UserNavigation : IEquatable<UserNavigation>
{
    /*
     * This looks like it's not flags, but it is flags,
     * the reason is that Automatic is considered horizontal
     * and verical mode combined
     */
    [Flags]
    public enum Mode
    {
        None = 0, // No UserNavigation
        Horizontal = 1, // Automatic horizontal UserNavigation
        Vertical = 2, // Automatic vertical UserNavigation
        Automatic = 3, // Automatic UserNavigation in both dimensions
        Explicit = 4, // Explicitly specified only
    }

    // Which method of UserNavigation will be used.
    [FormerlySerializedAs("mode")]
    [SerializeField]
    private Mode m_Mode;

    // Game object selected when the joystick moves up. Used when UserNavigation is set to "Explicit".
    [FormerlySerializedAs("selectOnUp")]
    [SerializeField]
    private UserSelectable m_SelectOnUp;

    // Game object selected when the joystick moves down. Used when UserNavigation is set to "Explicit".
    [FormerlySerializedAs("selectOnDown")]
    [SerializeField]
    private UserSelectable m_SelectOnDown;

    // Game object selected when the joystick moves left. Used when UserNavigation is set to "Explicit".
    [FormerlySerializedAs("selectOnLeft")]
    [SerializeField]
    private UserSelectable m_SelectOnLeft;

    // Game object selected when the joystick moves right. Used when UserNavigation is set to "Explicit".
    [FormerlySerializedAs("selectOnRight")]
    [SerializeField]
    private UserSelectable m_SelectOnRight;

    public Mode mode { get { return m_Mode; } set { m_Mode = value; } }
    public UserSelectable selectOnUp { get { return m_SelectOnUp; } set { m_SelectOnUp = value; } }
    public UserSelectable selectOnDown { get { return m_SelectOnDown; } set { m_SelectOnDown = value; } }
    public UserSelectable selectOnLeft { get { return m_SelectOnLeft; } set { m_SelectOnLeft = value; } }
    public UserSelectable selectOnRight { get { return m_SelectOnRight; } set { m_SelectOnRight = value; } }

    static public UserNavigation defaultUserNavigation
    {
        get
        {
            var defaultNav = new UserNavigation();
            defaultNav.m_Mode = Mode.Automatic;
            return defaultNav;
        }
    }

    public bool Equals(UserNavigation other)
    {
        return mode == other.mode &&
            selectOnUp == other.selectOnUp &&
            selectOnDown == other.selectOnDown &&
            selectOnLeft == other.selectOnLeft &&
            selectOnRight == other.selectOnRight;
    }
}
}