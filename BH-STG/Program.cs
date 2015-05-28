/*
 * Component: Program
 * Version: 1.0.1
 * Created: Febraury 24th, 2014
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
 * AUTO-GENERATED
*/

using System;

namespace BH_STG
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
}
