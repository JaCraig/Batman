mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core\bin\Release lib
..\.nuget\nuget pack Batman.Core.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.MVC\bin\Release lib
..\.nuget\nuget pack Batman.MVC.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Bootstrapper.SimpleInjector\bin\Release lib
..\.nuget\nuget pack Batman.Core.Bootstrapper.SimpleInjector.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Bootstrapper.TinyIoC\bin\Release lib
..\.nuget\nuget pack Batman.Core.Bootstrapper.TinyIoC.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Communication.RazorFormatter\bin\Release lib
..\.nuget\nuget pack Batman.Core.Communication.RazorFormatter.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Communication.SMTP\bin\Release lib
..\.nuget\nuget pack Batman.Core.Communication.SMTP.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.FileSystem.Local\bin\Release lib
..\.nuget\nuget pack Batman.Core.FileSystem.Default.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Logging.NLog\bin\Release lib
..\.nuget\nuget pack Batman.Core.Logging.NLog.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.Core.Profiling.MiniProfiler\bin\Release lib
..\.nuget\nuget pack Batman.Core.Profiling.MiniProfiler.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.MVC.Assets.Less\bin\Release lib
..\.nuget\nuget pack Batman.MVC.Assets.Less.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.MVC.Assets.WebGrease\bin\Release lib
..\.nuget\nuget pack Batman.MVC.Assets.WebGrease.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.MVC.Assets.Yui\bin\Release lib
..\.nuget\nuget pack Batman.MVC.Assets.Yui.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Batman.MVC.Serialization.ServiceStack\bin\Release lib
..\.nuget\nuget pack Batman.MVC.Serialization.ServiceStack.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

copy ..\Glimpse.Batman\bin\Release lib
..\.nuget\nuget pack Glimpse.Batman.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content

mkdir lib
mkdir tools
mkdir content

xcopy ..\Batman.MVC.Default\Content\Templates .\content\Templates /C /D /E /I /F /R /Y /Z
copy ..\Batman.MVC.Default\readme.txt readme.txt
..\.nuget\nuget pack Batman.MVC.Default.nuspec

rd /s/q lib
rd /s/q tools
rd /s/q content
del readme.txt

FOR %%A IN (*.nupkg) DO ..\.nuget\nuget push %%A