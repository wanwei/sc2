该项目是专门给测试工程提供帮助的工程，主要功能有三点：
1.提供测试需要用到的数据，如tick数据，k线数据等
2.管理和装载测试用例，下面会有一个测试用例的管理规范
3.提供一些测试的帮助类，如AssertUtils

测试用例管理规范
每一个测试工程都各自管理自己的测试用例，按照一定规则放到项目的TestCase目录下，在编译的时候统一拷贝到运行环境里，这样系统测试的时候可以通过TestCaseLoader和AssertUtils自动装载测试用例。具体规则如下：
1.所有测试代码和要测试的类的代码必须在同一个命名空间，如要测试的类是com.wer.sc.data.KLineData，那么测试代码也要在这个命名空间，如com.wer.sc.data.TestKLineData，测试需要用到的用例则放在TestCase\com\wer\sc\data\目录下。
2.项目编译的时候在生成事件里加上如下代码
mkdir $(TargetDir)TestCase\
xcopy $(ProjectDir)TestCase\* $(TargetDir)TestCase\ /s /e /y


具体细节可以参考项目com.wer.sc.mockdata.test