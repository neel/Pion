using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;

namespace Pion.Infrastructure.Common
{
    public static class ServiceLocator
    {
        static IKernel _standardKernel;

        public static void Dispose()
        {
            _standardKernel.Dispose();
        }

        public static void Initialize(INinjectModule initializedModule)
        {
            _standardKernel = new StandardKernel(initializedModule);
        }

        public static T Resolve<T>()
        {
            return _standardKernel.Get<T>();
        }
    }
}
