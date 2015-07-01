using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Graphics.UI.MouseControllers.Providers
{
    internal static class MouseControllerProvider
    {
        private static List<MouseController<Viewport>> _cachedMouseControllers;

        internal static MouseController<Viewport> GetMouseControllerByAction(eMouseAction action)
        {
            if(_cachedMouseControllers == null)
                LoadMouseControllers();

            return _cachedMouseControllers.First(c => c.MouseAction == action);
        }

        private static void LoadMouseControllers()
        {
            _cachedMouseControllers = new List<MouseController<Viewport>>();

            foreach (var type in TypeProvider.LoadEspecificyTypesFromAssembly(Assembly.GetExecutingAssembly(), typeof(MouseController<Viewport>)))
            {
                if (type.IsAbstract) continue;

                var instanceCreated = Activator.CreateInstance(type) as MouseController<Viewport>;

                if (instanceCreated != null)
                    _cachedMouseControllers.Add(instanceCreated);
            }
        }
    }
}
