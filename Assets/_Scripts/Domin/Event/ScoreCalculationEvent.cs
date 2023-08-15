public static class ScoreCalculationEvent
{
    public delegate void scoreCalculationEevntHandler(int data);

    private static event scoreCalculationEevntHandler _scoreCalculationEvent;

    /// 订阅事件
   public static void Register(scoreCalculationEevntHandler handler)
    {
        _scoreCalculationEvent += handler;
    }


    ///注销事件
   public static void Unregister(scoreCalculationEevntHandler handler)
    {
        _scoreCalculationEvent -= handler;
    }


    ///发布事件
    public static void Trigger(int data)
    {
        _scoreCalculationEvent?.Invoke(data);
    }
}