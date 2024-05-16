using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasyREST
{
    /// <summary>
    /// Helper class containing generic helper methods
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Outputs message to debiug console if in debug mode
        /// </summary>
        /// <param name="message"></param>
        public static void DebugMessage(string message)
        {
            #if DEBUG
                Debug.WriteLine(message);
            #endif
        }



    }
}
