# El Topao — contexto para Claude

Lee esto primero. Es el traspaso de la sesión anterior (2026-10-19 a 2026-10-21).

## Qué es el proyecto
"El Topao (Edición Dominicana)": tag online en **primera persona** en un barrio dominicano. Unity 6 (6000.5.5f1), HDRP 17.5, C#. Hardware del usuario: RTX 5070 12 GB, 32 GB RAM. Stack local de IA: Ollama (qwen2.5-coder, deepseek-r1) para ayudar a desarrollar, **no** forma parte del juego en ejecución.

El diseño completo está en `docs/GDD_El_Topao.md` (v0.1). Las marcas del documento: ✅ confirmado, 🟡 propuesta mía, ❓ abierta.

## Decisiones ya confirmadas por el usuario
- Multijugador online como núcleo.
- Primera persona.
- Los roles se alternan: quien es topado pasa a perseguir.
- Tono: mezcla de arcade y táctico.
- **Parkour humano**: cuerpo de niño de ~10 años. Nada superhumano ni militar. El juego original era de niños.
- Público: ambos (familias/niños y nostalgia de adultos), clasificación baja.
- Alcohol y lotería: solo decorativo y sin marcas (cajas genéricas, sin etiquetas legibles ni apuestas a la vista).
- Victoria: gana quien acumula menos tiempo como topao.
- Red: aún sin decidir. Se decide después de un prototipo de movimiento (propuesta de partida: Netcode for GameObjects + Unity Relay).

## Cómo trabajar con el usuario
- Responde en español.
- El usuario pidió explícitamente que **le preguntes cualquier duda**. No asumas en decisiones de diseño: pregunta, de a pocas preguntas por bloque.
- No escribas más código hasta cerrar el diseño. Antes de programar, repasa los pendientes de abajo.
- Cada respuesta del usuario se anota en el GDD (tacha la pregunta en la sección 22 y actualiza la sección que afecte).

## Pendientes: preguntas abiertas del GDD (sección 22)
Retomar por bloques, en este orden:

1. **Reglas** — Q3 jugadores por partida (propuesta 4 a 8) · Q5 reglas reales del topao en su barrio · Q6 cómo llamaban a la base ("libre", "casa", "paso") · Q8 una ronda larga o varias cortas.
2. **Movimiento y personajes** — Q11 chancleta que se sale al cansarse · Q12 "dar el pie" para trepar, y en qué modos · Q21 personajes realistas o estilizados · Q26 si un vault puede fallar · Q27 si todos tienen las mismas capacidades · Q10 equipos de béisbol reales o inventados.
3. **Mundo** — Q13 tráfico inofensivo o con algún riesgo · Q14 azoteas jugables o solo calle · Q15 qué es la zona restringida (opciones A, B o C en la sección 12.5) · Q16 más mapas · Q28 hora fija o ciclo de día, lluvia y apagón.
4. **NPCs** — Q9 bots solo de relleno o también modo offline · Q17 si el perro puede hacerse amigo del jugador.
5. **Progresión y negocio** — Q18 plataforma (Steam, móvil, consolas) · Q19 cómo se desbloquean los cosméticos · Q29 proyecto personal o comercial.
6. **Presentación** — Q20 idiomas · Q22 hardware mínimo (HDRP es pesado; si incluye PC modestas o móvil, considerar URP) · Q23 música licenciada o compuesta · Q24 voces: actores o IA con licencia.
7. **Técnico** — Q25 motor de movimiento (propuesta: cinemático propio) · Q30 soporte de mando · Q31 quién más trabaja en el proyecto.

## Qué sigue después de cerrar las preguntas
1. Pasar el GDD a v1.0.
2. Crear el proyecto Unity (HDRP, Unity 6000.5.5f1) y un **nivel de pruebas de parkour** en cajas grises con obstáculos a 0.2, 0.5, 0.8, 1.2, 1.6 y 2.0 m.
3. Movimiento con estamina y inercia (valores en el Apéndice A del GDD, guardados en ScriptableObjects).
4. Decidir la red con ese prototipo en mano.
5. Bucle de topar offline, luego red, luego el mapa con arte (ver hoja de ruta, sección 21).

## Código en el repo (esqueleto de Unity)
La raíz del repo es la raíz del proyecto Unity (Assets/, Packages/, ProjectSettings/). No hay archivos `.meta` ni escena: Unity los genera al abrir el proyecto.
- `Packages/manifest.json` y `ProjectSettings/ProjectVersion.txt` (6000.5.5f1), con HDRP 17.5.0.
- Dos asmdef: `ElTopao.Runtime` y `ElTopao.Editor`.
- `Assets/ElTopao/Runtime/BarrioZone.cs`: componente marcador de zonas (calle, acera, callejón, azotea, zona restringida, colmado).
- `Assets/ElTopao/Editor/ElTopaoLightingSetup.cs`: menú **El Topao > Iluminación** que configura el sol de las 4:30 PM (~100,000 lux), Physically Based Sky, niebla volumétrica, SSGI, exposición automática y ACES. **Nunca se compiló contra Unity**: al abrir el proyecto, revisar primero los errores de compilación. Los tipos de HDRP se verificaron leyendo el paquete instalado.
- Falta: el blockout del barrio (generador de mapa por código). Se pausó hasta cerrar el diseño.
- Unity está instalado en `C:/Program Files/Unity/Hub/Editor/6000.5.5f1`.
