using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace SurfStat.Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
                {
                    x.Service<Monitor>(s =>
                        {
                            s.ConstructUsing(name => new Monitor());
                            s.WhenStarted(m => m.Start());
                            s.WhenStopped(m => m.Stop());
                        });
                    x.RunAsLocalSystem();

                    x.SetDescription("Monitors the internet connection for a SurfBeam modem");
                    x.SetDisplayName("SurfStat Monitor");
                    x.SetServiceName("SurfStat Monitor");
                });
        }
    }
}
