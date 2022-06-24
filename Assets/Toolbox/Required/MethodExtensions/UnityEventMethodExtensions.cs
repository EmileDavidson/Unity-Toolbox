using System.Reflection;
using UnityEngine.Events;

namespace Toolbox.Required
{
    public static class UnityEventMethodExtensions
    {
        
        public static int GetListenerNumber(this UnityEventBase unityEvent)
        {
            var field = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly );
            if (field is null) return 0;
            var invokeCallList = field.GetValue(unityEvent);
            var property = invokeCallList.GetType().GetProperty("Count");
            if (property is null) return 0;
            return (int)property.GetValue(invokeCallList);
        }

    }
}