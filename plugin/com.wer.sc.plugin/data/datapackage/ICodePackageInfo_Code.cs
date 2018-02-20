namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 描述一支股票或期货的时间信息
    /// </summary>
    public interface ICodePackageInfo_Code
    {
        string Code { get; set; }

        int StartDate { get; set; }

        int EndDate { get; set; }
    }
}