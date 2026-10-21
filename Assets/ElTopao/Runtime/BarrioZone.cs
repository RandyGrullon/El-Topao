using UnityEngine;

namespace ElTopao
{
    public enum BarrioZoneType { Calle, Acera, Callejon, Azotea, ZonaRestringida, Colmado }

    /// <summary>Marca una región del mapa (trigger) para que la IA y las reglas de juego la consulten.</summary>
    [RequireComponent(typeof(BoxCollider))]
    public class BarrioZone : MonoBehaviour
    {
        public BarrioZoneType type = BarrioZoneType.Calle;

        public bool IsRestricted => type == BarrioZoneType.ZonaRestringida;

        void Reset()
        {
            var box = GetComponent<BoxCollider>();
            box.isTrigger = true;
        }
    }
}
