using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.update
{
    /// <summary>
    /// 数据进程接口
    /// 该接口用于实现数据更新
    /// 用户只需要实现该接口的Prepare方法，将所有步骤准备好，就可以交给系统执行了
    /// </summary>
    public interface IUpdateStepGetter
    {
        /// <summary>
        /// 准备执行进程，得到所有的执行步骤
        /// </summary>
        /// <returns></returns>
        List<IStep> GetSteps();
    }

    public interface IStep
    {
        /// <summary>
        /// 这一次向前走的步数
        /// </summary>
        int ProgressStep { get; }

        ///// <summary>
        ///// 该步骤的level，在执行的时候会按照level从小到大执行，同一个level的Step可以多线程一起执行
        ///// 暂时不支持按照level TODO
        ///// </summary>
        //int Level { get; }

        /// <summary>
        /// 得到这个步骤的描述信息
        /// </summary>
        String StepDesc { get; }

        /// <summary>
        /// 执行进程，返回进程的执行结果
        /// </summary>
        /// <returns></returns>
        String Proceed();
    }
}