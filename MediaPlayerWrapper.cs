using System;
using System.Runtime.InteropServices;

namespace TizenMediaPlayer
{
    public class MediaPlayer : IDisposable
    {
        private IntPtr _handle;

        [DllImport("tizen_media_player", EntryPoint = "mp_create")]
        private static extern IntPtr mp_create();

        [DllImport("tizen_media_player", EntryPoint = "mp_destroy")]
        private static extern void mp_destroy(IntPtr player);

        [DllImport("tizen_media_player", EntryPoint = "mp_set_url")]
        private static extern void mp_set_url(IntPtr player, string url);

        [DllImport("tizen_media_player", EntryPoint = "mp_play")]
        private static extern void mp_play(IntPtr player);

        [DllImport("tizen_media_player", EntryPoint = "mp_pause")]
        private static extern void mp_pause(IntPtr player);

        [DllImport("tizen_media_player", EntryPoint = "mp_seek")]
        private static extern void mp_seek(IntPtr player, int positionMs);

        public MediaPlayer()
        {
            _handle = mp_create();
        }

        public void SetUrl(string url) => mp_set_url(_handle, url);
        public void Play() => mp_play(_handle);
        public void Pause() => mp_pause(_handle);
        public void Seek(int positionMs) => mp_seek(_handle, positionMs);

        public void Dispose()
        {
            if (_handle != IntPtr.Zero)
            {
                mp_destroy(_handle);
                _handle = IntPtr.Zero;
            }
			GC.SuppressFinalize(this);
        }
		
		~MediaPlayer()
		{
				Dispose();
		}
    }
}
