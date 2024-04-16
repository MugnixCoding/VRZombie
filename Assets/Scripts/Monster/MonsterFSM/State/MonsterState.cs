using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState : BaseState<Monster.MonsterState>
{
    protected MonsterFSMContext context;
    public MonsterState(MonsterFSMContext _context)
    {
        context = _context;
    }
}
