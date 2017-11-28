using PaintShop.Services.Implementation;
using PaintShop.Services.Repository;
using System;
using Unity;

namespace PaintShop
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize unity dependency injector
            IColorMixing colorMixing = unityInitializer();            

            if (args?.Length == 0)
                Console.WriteLine("Command line argument is missing");
            else
            {
                try
                {                    
                    string finalColor = colorMixing.GetCustomerColors(@"" + args[0]);
                    Console.WriteLine("Output " + finalColor);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }
        }

        #region Private Method
        private static IColorMixing unityInitializer()
        {
            var container = new UnityContainer();
            container.RegisterType<IReadFile, FileManager>();
            container.RegisterType<IColorMixing, ColorMixing>();

            return container.Resolve<IColorMixing>();
        }
        #endregion
    }   
}
