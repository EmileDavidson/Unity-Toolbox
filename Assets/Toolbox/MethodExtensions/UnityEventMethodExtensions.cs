using System.Reflection;
using UnityEngine.Events;

namespace Toolbox.MethodExtensions
{
    public static class UnityEventMethodExtensions
    {
        /// <summary>
        /// Gets the number of subscribed listeners. this excludes the persistent listeners (from unity inspector for instance)
        /// </summary>
        /// <param name="unityEvent"></param>
        /// <returns></returns>
        public static int GetListenerCount(this UnityEventBase unityEvent)
        {
            var field = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly );
            if (field is null) return 0;
            var invokeCallList = field.GetValue(unityEvent);
            var property = invokeCallList.GetType().GetProperty("Count");
            if (property is null) return 0;
            return (int)property.GetValue(invokeCallList);
        }

        /// <summary>
        /// Gets the total amount of listeners by adding GetListenerCount() & GetPersistentEventCount()
        /// </summary>
        /// <param name="unityEvent"></param>
        /// <returns></returns>
        public static int GetTotalListenerCount(this UnityEventBase unityEvent)
        {
            return unityEvent.GetListenerCount() + unityEvent.GetPersistentEventCount();
        }

    }
}