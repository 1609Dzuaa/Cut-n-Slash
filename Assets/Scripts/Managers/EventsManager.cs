using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class EventsManager : BaseSingleton<EventsManager>
{
    //You can assign values of any type to variables of type object
    //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types
    private Dictionary<EEvents, Action<object>> _dictEvents = new();
    //Thêm sẵn các Action tương ứng với Event trong EnumEvents tại đây
    private readonly Action<object> EnemiesOnReceiveDamage;
    private readonly Action<object> ArrowOnReceiveInfor;
    private readonly Action<object> PopupTextOnReceiveInfor;

    //Làm việc với Event thì nên phân biệt với nhau bằng key là object
    //Tránh cùng 1 lúc nó Notify tất cả Func đã đky event đó
    //thay vì chỉ Notify những Func cần

    protected override void Awake()
    {
        base.Awake();
        AddEventsToDictionary();
        DontDestroyOnLoad(gameObject);
    }

    public void AddEventsToDictionary()
    {
        _dictEvents.Add(EEvents.EnemiesOnReceiveDamage, EnemiesOnReceiveDamage);
        _dictEvents.Add(EEvents.ArrowOnReceiveInfor, ArrowOnReceiveInfor);
        _dictEvents.Add(EEvents.PopupTextOnReceiveInfor, PopupTextOnReceiveInfor);
        //Val là cái event, còn thg nào quan tâm cái event đó thì gọi hàm dưới
    }

    public void SubcribeToAnEvent(EEvents eventType, Action<object> function)
    {
        //if (!_dictEvents.ContainsKey(eventType))
            //_dictEvents.Add(eventType, function);
        _dictEvents[eventType] += function;
    }

    public void UnSubcribeToAnEvent(EEvents eventType, Action<object> function)
    {
        _dictEvents[eventType] -= function;
    }

    public void NotifyObservers(EEvents eventType, object eventArgsType)
    {
        //Gọi thằng đã sub cái eventType với tham số eventArgsType
        //(tránh bị gọi tất cả func đã đki cùng 1 lúc)
        _dictEvents[eventType]?.Invoke(eventArgsType);
    }
}