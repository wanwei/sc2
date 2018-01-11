using com.wer.sc.utils.param;

namespace com.wer.sc.strategy
{
    public interface IStrategyConfig
    {
        string ClassName { get; }

        string Name { get; }

        string Description { get; }

        IParameters Parameters { get; }

        string ErrorInfo { get; }

        bool IsError { get; }
    }
}