using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace C2LP.PDA.APP
{
    static class SystemDll
    {
        [DllImport("Coredll.dll", EntryPoint = "GetTickCount")]
        public static extern int GetTickCount();

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySound(string szSound, IntPtr hMod, int flags);

        [DllImport("CoreDll.DLL", EntryPoint = "sndPlaySound", SetLastError = true)]
        private extern static int WCE_sndPlaySound(string szSound, int flags);

        [DllImport("coredll.dll", EntryPoint = "MessageBeep")]
        public static extern bool MessageBeep(int iType);

        private enum PlaySoundFlags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        public static void PlaySound(string fileName)
        {
            //WCE_PlaySound(fileName, IntPtr.Zero, (int)(PlaySoundFlags.SND_ASYNC | PlaySoundFlags.SND_FILENAME));
            WCE_sndPlaySound(fileName, (int)PlaySoundFlags.SND_ASYNC);
        }
    }
}