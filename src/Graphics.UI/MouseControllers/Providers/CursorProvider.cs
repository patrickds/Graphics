using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers.Providers
{
    internal static class CursorProvider
    {
        private const string RESOURCES_PATH = "Graphics.UI.Resources.";
        private const string PANT_CURSOR = "pan.cur";

        public static Dictionary<eMouseAction, Cursor> Cursors { get; set; }

        static CursorProvider()
        {
            BuildCursors();
        }

        private static void BuildCursors()
        {
            Cursors = new Dictionary<eMouseAction, Cursor>();
            Cursors.Add(eMouseAction.Pan, BuildCursor(PANT_CURSOR));
        }

        private static Cursor BuildCursor(string cursor)
        {
            var cursorPath = RESOURCES_PATH + cursor;
            var cursorStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(cursorPath);
            return new Cursor(cursorStream);
        }
    }
}