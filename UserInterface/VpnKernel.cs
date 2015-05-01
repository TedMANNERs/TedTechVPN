using System;
using Ninject;

namespace TedTechVpn.UserInterface
{
    public class VpnKernel : IDisposable
    {
        private static VpnKernel _instance;

        private VpnKernel()
        {
            Kernel = new StandardKernel();
            Kernel.Load<VpnModule>();
        }

        public static VpnKernel Instance
        {
            get { return _instance ?? (_instance = new VpnKernel()); }
        }

        public IKernel Kernel { get; private set; }

        public static T Get<T>()
        {
            return Instance.Kernel.Get<T>();
        }

        public static void ClearInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Kernel != null)
                {
                    Kernel.Dispose();
                    Kernel = null;
                }
            }
        }
    }
}