using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Graphics.UI.MouseControllers.Providers
{
    internal static class MouseControllerProvider
    {
        private static List<MouseController<ViewportControl>> _cachedMouseControllers;

        internal static MouseController<ViewportControl> GetMouseControllerByAction(eMouseAction action)
        {
            if(_cachedMouseControllers == null)
                LoadMouseControllers();

            return _cachedMouseControllers.First(c => c.MouseAction == action);
        }

        private static void LoadMouseControllers()
        {
            _cachedMouseControllers = new List<MouseController<ViewportControl>>();

            foreach (var type in TypeProvider.LoadEspecificyTypesFromAssembly(Assembly.GetExecutingAssembly(), typeof(MouseController<ViewportControl>)))
            {
                if (type.IsAbstract) continue;

                var instanceCreated = Activator.CreateInstance(type) as MouseController<ViewportControl>;

                if (instanceCreated != null)
                    _cachedMouseControllers.Add(instanceCreated);
            }
        }
    }
}
