rmdir /S /Q release
mkdir release
util\ilmerge bin\Castle.Core.dll bin\Castle.DynamicProxy2.dll bin\FluentNHibernate.dll bin\Iesi.Collections.dll bin\log4net.dll bin\NHibernate.dll bin\NHibernate.Linq.dll bin\SimpleCore.dll bin\SimpleLib.dll bin\SimpleServer.dll /out:release\Simple.dll