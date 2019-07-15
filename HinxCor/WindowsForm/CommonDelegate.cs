using System;
using System.Collections.Generic;


public delegate void action();

public delegate void action_s(string msg);
public delegate void action_ss(string str1, string str2);
public delegate void action_sf(string str1, float str2);
public delegate void action_ssf(string str1, string str2, float f);

public delegate void action_a(object o);
public delegate void action_aa(object o1, object o2);

public delegate bool fun_b();
public delegate bool fun_ab(object o);

public delegate object fun_aa(object o);
