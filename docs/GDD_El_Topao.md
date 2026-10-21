# EL TOPAO — Edición Dominicana
## Documento de Diseño de Juego (GDD) v0.1

> **Estado del documento:** borrador vivo. Se actualiza cada vez que tomamos una decisión.
> **Motor:** Unity 6 (6000.5.5f1) · **Render:** HDRP 17.5 · **Lenguaje:** C#

### Cómo leer este documento

Cada decisión lleva una marca:

| Marca | Significado |
|---|---|
| ✅ **CONFIRMADO** | Lo dijiste tú. No se toca sin preguntarte. |
| 🟡 **PROPUESTA** | Es mi sugerencia por defecto. Dime si la cambias. |
| ❓ **ABIERTA** | Falta tu decisión. Está listada en la [sección 22](#22-preguntas-abiertas). |

Los números de movimiento, stamina, tiempos y distancias son **valores iniciales de prueba**. Irán en `ScriptableObject`s para ajustarlos jugando, sin tocar código.

---

## Índice

1. [Visión y pilares](#1-visión-y-pilares)
2. [Público, tono y clasificación](#2-público-tono-y-clasificación)
3. [Decisiones confirmadas hasta ahora](#3-decisiones-confirmadas-hasta-ahora)
4. [El juego original: qué es "el topao"](#4-el-juego-original-qué-es-el-topao)
5. [Reglas de la partida](#5-reglas-de-la-partida)
6. [Personajes](#6-personajes)
7. [Movimiento humano (el corazón del juego)](#7-movimiento-humano-el-corazón-del-juego)
8. [Cámara y sensación en primera persona](#8-cámara-y-sensación-en-primera-persona)
9. [Mecánica de topar](#9-mecánica-de-topar)
10. [Esconderse y sigilo](#10-esconderse-y-sigilo)
11. [Interacción con el entorno](#11-interacción-con-el-entorno)
12. [Mapa: el barrio](#12-mapa-el-barrio)
13. [NPCs y bots](#13-npcs-y-bots)
14. [Multijugador online](#14-multijugador-online)
15. [Modos de juego](#15-modos-de-juego)
16. [Progresión y personalización](#16-progresión-y-personalización)
17. [Interfaz (HUD y menús)](#17-interfaz-hud-y-menús)
18. [Arte y dirección visual (HDRP)](#18-arte-y-dirección-visual-hdrp)
19. [Audio](#19-audio)
20. [Arquitectura técnica](#20-arquitectura-técnica)
21. [Hoja de ruta](#21-hoja-de-ruta)
22. [Preguntas abiertas](#22-preguntas-abiertas)
23. [Riesgos](#23-riesgos)
24. [Glosario](#24-glosario)
25. [Apéndice A: tabla de parámetros](#apéndice-a-tabla-de-parámetros-iniciales)
26. [Apéndice B: lista de "lo que NO hace un niño"](#apéndice-b-lo-que-no-hace-un-niño)

---

## 1. Visión y pilares

### 1.1 Visión (una frase)

> Revivir en primera persona el juego de la calle de tu infancia: correr por el barrio a las 4:30 de la tarde, esconderte donde sabes que nadie te busca, y gritar "¡topao!" cuando alcanzas a tu pana.

### 1.2 Pilares de diseño

Cada decisión del juego se mide contra estos pilares. Si una mecánica los rompe, no entra.

| # | Pilar | Qué significa en la práctica | Qué NO es |
|---|---|---|---|
| 1 | **Cuerpo de niño, no de héroe** ✅ | Corres cansado, tropiezas, no saltas más de lo que un niño salta. | No es parkour de Assassin's Creed, ni de militar, ni de ninja. |
| 2 | **El barrio es el protagonista** ✅ | Cada rincón se siente real y reconocible: el colmado, la banca, el callejón. | No es un mapa genérico con texturas dominicanas. |
| 3 | **Juego de calle, no de combate** ✅ | Se gana con piernas, cabeza y atajos. Nadie lastima a nadie. | Sin armas, sin golpes, sin sangre. |
| 4 | **Tag táctico** ✅ | Leer el mapa, recordar atajos, engañar al que te persigue. | No es solo "corre más rápido". |
| 5 | **Risa y nostalgia** 🟡 | Partidas cortas, caóticas, graciosas. | No es una simulación seria ni un horror. |
| 6 | **Justo con el cansancio** 🟡 | Nadie es invencible: el que persigue también se agota. | No hay una build o habilidad que haga imposible alcanzar a alguien. |

### 1.3 Fantasía del jugador

- **Cuando huyes:** la emoción de saber que alguien viene detrás, el corazón acelerado, la respiración, la decisión de "¿me meto por el callejón o cruzo la calle?".
- **Cuando persigues:** la satisfacción de leer a tu presa, cortarle el camino, usar un atajo que ella no se espera.
- **Cuando eres topao:** la risa, la injusticia fingida ("¡eso no valió!"), el cambio de rol inmediato.

---

## 2. Público, tono y clasificación

> ✅ **Q1 y Q2 respondidas:** público "ambos" y contenido de alcohol/lotería "solo decorativo y sin marcas". La tabla de reemplazos de abajo queda como guía de arte: se usan cajas y bancas genéricas, sin etiquetas legibles.

### 2.1 Público objetivo 🟡

| Aspecto | Propuesta |
|---|---|
| Jugadores | Familias, amigos, nostalgia de adultos dominicanos y de la diáspora. |
| Edad de los personajes | Niños de **8 a 12 años**. |
| Clasificación objetivo | **E (Everyone) / PEGI 3-7**. Sin violencia, sin armas, sin lenguaje fuerte. |
| Tono | Cómico, cálido, nostálgico. Humor de barrio sin ser vulgar. |

### 2.2 Reglas de contenido (para mantener la clasificación) 🟡

1. **Cero armas y cero violencia.** Topar es tocar con la mano. Nadie empuja, golpea ni derriba a otro.
2. **Sin alcohol ni apuestas visibles.** El documento original incluye cajas de cerveza y bancas de lotería. En un juego de niños propongo reemplazarlos o suavizarlos:

   | Elemento original | Reemplazo propuesto |
   |---|---|
   | Cajas de cerveza | Cajas de refrescos, jugos o Malta (la caja amarilla/roja se mantiene visualmente) |
   | Banca de lotería "Banca Darío" | Se mantiene como edificio de barrio, con rótulo de **"Banca Darío — Bancas y Recargas"** (sin mostrar apuestas ni boletos) |
   | Mesa de dominó con ron/cerveza | Mesa de dominó con refrescos |

   ✅ Decisión (Q2): **solo decorativo y sin marcas**. Cajas genéricas sin etiquetas de alcohol; la banca no muestra boletos ni apuestas.
3. **Chat controlado** (ver sección 14.8): sin texto libre ni voz abierta con desconocidos.
4. **Sin compras con dinero real que afecten el juego**, y sin cajas sorpresa (loot boxes).
5. **Lenguaje:** español dominicano natural y expresiones de barrio, sin groserías.

---

## 3. Decisiones confirmadas hasta ahora

| # | Decisión | Estado |
|---|---|---|
| 1 | Motor Unity 6 (6000.5.5f1) con HDRP | ✅ |
| 2 | Ambientación: barrio dominicano fotorrealista, sol de las 4:30 PM | ✅ |
| 3 | **Multijugador online** como núcleo | ✅ |
| 4 | **Primera persona** | ✅ |
| 5 | **Los roles se alternan** (quien es topado pasa a perseguir) | ✅ |
| 6 | Tono: **mezcla de arcade y táctico** | ✅ |
| 7 | **Parkour humano**: nada superhumano ni militar | ✅ |
| 8 | El juego original era **de niños** | ✅ |
| 9 | Pipeline 3D local (Hunyuan3D, TRELLIS, TripoSR, Flux) | ✅ (del documento maestro) |
| 10 | Público: **ambos** (familias/niños y nostalgia de adultos), clasificación baja | ✅ (Q1) |
| 11 | Cerveza y lotería: **solo decorativo y sin marcas** (cajas genéricas, sin etiquetas de alcohol ni apuestas a la vista) | ✅ (Q2) |
| 12 | Victoria: **menos tiempo como topao** | ✅ (Q7) |
| 13 | Tecnología de red: **se decide tras un prototipo de movimiento** (propuesta de partida: NGO + Relay) | ✅ aplazada (Q4) |
| 14 | Número de jugadores por partida | ❓ Q3 (propuesta: 4 a 8) |

---

## 4. El juego original: qué es "el topao"

> ❓ **Q5:** necesito que me confirmes las reglas reales con las que tú jugabas. Cada barrio las juega distinto. Abajo pongo la versión que propongo como base.

El topao (de **topar**: tocar, encontrarse con algo) es un juego de persecución:

1. Uno es "el topao" (el que la lleva). Persigue a los demás.
2. Cuando toca a alguien, esa persona pasa a ser el nuevo topao.
3. Los demás huyen y se esconden por el barrio.

### 4.1 Variantes tradicionales que podemos incluir 🟡

| Variante | Cómo se juega | Prioridad |
|---|---|---|
| **Topao clásico** | Quien la lleva persigue; al tocar, se pasa. Gana quien menos tiempo la llevó. | **Núcleo** |
| **Con base / "libre"** | Hay un lugar (poste, pared, portón) donde estás a salvo un rato. | **Núcleo** |
| **Topao congelado** | A quien topan se queda quieto hasta que otro lo "descongele". | Fase 2 |
| **Escondidas con topao** | Uno cuenta con los ojos cerrados y luego busca. | **Núcleo** (como inicio de ronda) |
| **Topao de equipo** | Dos equipos, uno persigue y otro huye. | Fase 3 |

---

## 5. Reglas de la partida

### 5.1 Estructura general 🟡

```
LOBBY → CARGA → CUENTA ("uno, dos, tres...") → RONDA → FIN DE RONDA → (otra ronda) → RESULTADOS
```

| Parámetro | Valor inicial | Notas |
|---|---|---|
| Jugadores | 4 a 8 (ideal 6) ❓ Q3 | Bots rellenan puestos vacíos ❓ Q9 |
| Rondas por partida | 3 | |
| Duración de ronda | 4 minutos | Se acorta si todos fueron topados al menos una vez |
| Topaos simultáneos | 1 (con 4–6 jugadores) · 2 (con 7–8) | |
| Cuenta inicial | 10 segundos | Ver 5.3 |

### 5.2 Cómo se gana 🟡

**Puntuación principal:** el jugador con **menos tiempo acumulado como topao** gana la partida.

- Cada segundo que eres el topao suma a tu marcador (como en el tag clásico "el que menos la llevó").
- Puntos extra pequeños por: **topar a alguien** (+), **hacer un buen escape** (esquivar por muy poco, +), **terminar la ronda sin ser topao** (+).
- Empates: gana quien tuvo **más topes realizados**.

> 💡 Este sistema evita que el último topao pierda de forma humillante: lo que importa es el tiempo total, no quién es topao al sonar el silbato.

### 5.3 Inicio de ronda: la cuenta 🟡

1. Todos spawnean **dispersos por el barrio** (puntos de spawn fijos y aleatorizados).
2. El topao inicial se queda en un **punto central** con la pantalla en negro (como "cerrar los ojos").
3. Una **cuenta en voz alta** sube ("uno, dos, tres…") y se oye por todo el mapa.
4. Los demás tienen esos 10 segundos para **esconderse o alejarse**.
5. Se oye "¡Ya voy!" y comienza la persecución.

### 5.4 Cambio de rol al ser topado ✅/🟡

Cuando el topao toca a alguien (ver sección 9):

1. El tocado **pasa a ser el nuevo topao**.
2. El anterior topao se vuelve **"libre"** y recibe **inmunidad de 3 segundos** para huir (para que no haya "topé y me topó de vuelta" en loop). 🟡
3. El nuevo topao tiene un **tiempo de espera de 3 segundos** quieto ("contando") antes de poder perseguir. 🟡
4. Se evita el rebote: **no se puede topar de vuelta al mismo jugador durante 10 segundos**. 🟡

### 5.5 Bases ("libre") 🟡

Hay lugares del mapa donde puedes estar a salvo temporalmente (un poste marcado, un portón, una pared pintada).

| Regla | Valor inicial |
|---|---|
| Tiempo máximo a salvo por uso | 6 segundos |
| Espera antes de volver a usar la misma base | 30 segundos |
| Dentro de la base | No puedes ser topado, pero tampoco te mueves ni recuperas estamina rápido |
| Señal visual | Marca en el suelo + sonido cuando alguien está "en base" |
| ¿Máximo de jugadores? | 1 por base |

> ❓ **Q6:** ¿Había bases con ese nombre ("libre", "casa", "paso") en tu barrio? ¿Cómo las llamaban?

### 5.6 Reglas anti-abuso 🟡

- **No esconderse en un sitio sin salida el 100% del tiempo:** si estás quieto más de 20 s en la misma zona, el sonido de tu respiración se escucha más lejos (ver 10).
- **Zonas prohibidas:** salir del mapa jugable lleva a una advertencia y luego a un "vecino" NPC que te devuelve (ver 12.5).
- **Sin atrapamientos:** nadie puede quedar encerrado; hay siempre una salida (sillas y cajas se pueden empujar).
- **Abandono:** si un jugador se desconecta, un bot toma su lugar 🟡.

---

## 6. Personajes

### 6.1 Cuerpo y proporciones 🟡

| Medida | Valor |
|---|---|
| Edad aparente | 10 años |
| Altura | 1.40 m |
| **Altura de los ojos (cámara)** | **1.28 m** |
| Alcance del brazo al frente | 0.55 m desde el pecho |
| Alcance con salto (mano arriba) | ~1.85 m |
| Radio del cuerpo (colisión) | 0.28 m |
| Altura de colisión | 1.35 m (agachado: 0.90 m) |

### 6.2 Personaje del jugador 🟡

El jugador es **un niño o niña del barrio**. Se elige apariencia, no estadísticas. Todos los personajes tienen **las mismas capacidades físicas** (la diferencia es solo estética, para mantener el juego justo).

### 6.3 Variedad de apariencias (arte) 🟡

- Dos tipos de cuerpo (niño / niña), con **3 tonos de piel × peinados × ropa**.
- Ropa de barrio: camiseta de equipo de béisbol (Licey, Águilas, Estrellas… ❓ Q10 sobre licencias), chancletas, shorts, gorra.
- Accesorios cosméticos (ver sección 16).

### 6.4 Voces y reacciones 🟡

Frases cortas en español dominicano, reproducidas según contexto. Ejemplos (a revisar contigo, **no son definitivos**):

| Situación | Frase de ejemplo |
|---|---|
| Topas a alguien | "¡Topao!" |
| Te topan | "¡Ay, no!" / "¡Eso no valió!" |
| Escapas por poco | "¡Uff!" |
| Cansado | Respiración agitada, "Ya no puedo…" |
| Caída leve | "¡Ay!" (sin dolor, estilo cómico) |

---

## 7. Movimiento humano (el corazón del juego)

> ✅ **Requisito de ti:** parkour humano, nada superhumano ni militar. El juego era de niños.

### 7.1 Filosofía de movimiento

El movimiento debe **sentirse como el de un niño real corriendo por su barrio**:

- Hay **peso, esfuerzo e inercia**: arrancar, frenar y girar toman tiempo.
- El cuerpo **se cansa** y eso cambia lo que puedes hacer.
- Los obstáculos tienen **altura real**: lo que un niño de 1.40 m puede pasar es lo que el jugador puede pasar.
- **No hay animaciones de película.** Ningún giro en el aire, ni correr por paredes, ni caídas en voltereta desde alturas.
- Todo lo que hagas tiene **un costo visible** (estamina, tiempo de recuperación, ruido).

### 7.2 Movimientos permitidos (lo que SÍ hay)

| Movimiento | Descripción | Costo |
|---|---|---|
| **Caminar** | Paso normal | Gratis; recupera estamina |
| **Trotar** | Ritmo de juego constante | Casi gratis |
| **Correr** | Carrera normal | Gasta estamina lento |
| **Esprintar** | Máximo esfuerzo, unos segundos | Gasta estamina rápido |
| **Agacharse** | Para pasar bajo cosas y esconderte | Sin costo; muy silencioso |
| **Saltar** | Salto corto y pesado | Pequeño |
| **Salto largo corriendo** | Cruzar un charco, un hueco pequeño | Medio |
| **Saltar un muro bajo (vault)** | Apoyar las manos y pasar al otro lado | Medio, ~0.6 s |
| **Trepar (mantle)** | Subir a una superficie alta con esfuerzo | Alto, ~1.5 s |
| **Gatear / pasar bajo** | Bajo mesas, rejas bajas, tubos | Lento, muy silencioso |
| **Subir escaleras** | A pie, un poco más lento | Ligero |
| **Agarrarse a una reja y trepar** | Dedos en la malla, lento | Alto |
| **Esquivar (paso lateral)** | Un paso rápido de lado para engañar | Pequeño, con enfriamiento |
| **Frenar con derrape corto** | Parar de golpe resbalando un poco (más en piso mojado) | Pierde velocidad |
| **Empujar objetos ligeros** | Sillas plásticas, cajas vacías | Ralentiza |
| **Arrastrar** | Cajas medianas | Muy lento |

### 7.3 Movimientos prohibidos (lo que NO hay) ❌

> Consulta también el [Apéndice B](#apéndice-b-lo-que-no-hace-un-niño).

- Doble salto, salto en el aire, planeo.
- Correr por paredes (wall-run) o rebotar entre paredes.
- Rodar al caer desde alturas ("roll") como protección.
- Saltos de más de ~1.8 m en largo o ~0.45 m en vertical.
- Gancho, cuerda, tirolesa, trepar edificios por fachadas.
- Quitar la inercia: no hay giros instantáneos a toda velocidad.
- Velocidades de adulto atleta (> 6 m/s).
- Caer de azoteas altas sin consecuencias: las alturas peligrosas están **bloqueadas con barandas o muros**.
- Cualquier movimiento "tactical/military": deslizarse en rodillas, apuntar, rodar, cubrirse con arma.

### 7.4 Valores de velocidad 🟡

Basados en datos aproximados de niños de ~10 años (jugando, no atletas):

| Acción | Velocidad |
|---|---|
| Caminar | 1.3 m/s |
| Trotar | 2.6 m/s |
| Correr | 4.0 m/s |
| Esprintar | 5.2 m/s (máx. ~4 s, ver estamina) |
| Agachado | 0.9 m/s |
| Gateando | 0.5 m/s |
| Subiendo escalera | 70% de la velocidad actual |
| Cansado ("jadeo") | tope de 3.0 m/s |
| Empujando objeto | 60% de la velocidad actual |

**Inercia:**

- Aceleración de 0 a correr: ~0.8 s.
- Frenar de correr a 0: ~0.4 s.
- Giro de 180° corriendo: ~0.7 s, perdiendo ~40% de velocidad.

### 7.5 Saltos y obstáculos: alturas reales 🟡

| Obstáculo | Altura | Qué pasa |
|---|---|---|
| Contén / escalón | hasta 0.20 m | Se sube sin acción (con pequeño tropiezo si vas a toda velocidad) |
| Muro bajo / caja / banco | 0.20–0.80 m | **Saltar (vault)**, ~0.6 s |
| Muro mediano / portón | 0.80–1.20 m | **Vault con esfuerzo**, ~1.0 s, requiere carrera previa |
| Muro alto con apoyo | 1.20–1.60 m | **Trepar (mantle)**, ~1.5 s, mucha estamina |
| Más de 1.60 m | — | **No se puede**, salvo con ayuda (silla apilada, caja, escalera) |
| Reja de malla | hasta 2.0 m | Trepar lento (~2.5 s), se oye el metal |
| Hueco horizontal | hasta 1.5 m corriendo | Salto largo (máx. 1.8 m con mucha carrera) |
| Caída segura | hasta 1.2 m | Sin efecto |
| Caída incómoda | 1.2–2.0 m | **Tropiezo** 1 s, mala recuperación, ruido |
| Caída larga | más de 2.0 m | **No existe en el mapa jugable** (barandas o diseño lo impiden) |

> 💡 **Regla de diseño del mapa:** ninguna ruta debe obligar a hacer algo que un niño de 10 años no podría hacer en la vida real.

### 7.6 Estamina y cansancio 🟡

| Valor | Cifra inicial |
|---|---|
| Estamina máxima | 100 |
| Correr | −8 / segundo |
| Esprintar | −20 / segundo |
| Vault | −8 |
| Mantle (trepar) | −20 |
| Trepar reja | −5 / segundo |
| Saltar | −5 |
| Recuperar caminando | +12 / segundo |
| Recuperar trotando | +4 / segundo |
| Recuperar quieto | +18 / segundo |
| Espera antes de recuperar | 1 s |

**Estados de cansancio:**

| Estamina | Estado | Efecto |
|---|---|---|
| 100–40 | Fresco | Normal |
| 40–20 | Agitado | Respiración audible; se oye a 8 m |
| 20–1 | **Jadeo** | Velocidad máxima 3.0 m/s; se oye a 15 m; no puedes esprintar |
| 0 | **Sin aire** | Obligado a caminar/parar ~2 s (se dobla del flato) |

> 💡 El **"flato"** (dolor de costado) es un detalle humano y reconocible: sin aire, el niño se frena aunque no quiera. Esto evita las persecuciones infinitas y obliga a decidir *cuándo* esprintar.

### 7.7 Detalles que dan "humanidad" 🟡

- **Tropiezos:** en piso mojado, desparejo o al cambiar de dirección cansado, hay una pequeña probabilidad de tropezar (se pierde ~0.5 s). No es aleatorio injusto: se advierte con un pequeño resbalón visible antes.
- **La chancleta:** si corres con chancletas y te cansas mucho, puede **salirse una** (cosmético con efecto menor: pierdes 5% de velocidad hasta recogerla). ❓ **Q11:** ¿te parece gracioso o molesto?
- **Manos en las rodillas** cuando estás sin aire.
- **Brazos que se mueven** al correr (visibles en primera persona, ver sección 8).
- **El pecho sube y baja** en el cansancio (movimiento de la cámara).
- **Reacción al topar:** el niño levanta la mano, se detiene un instante, ríe.

### 7.8 Ayuda entre jugadores (cooperación humana) ❓ Q12

Idea 🟡: los niños se ayudan a subir. Un jugador agachado frente a un muro puede **"dar el pie"** (hacer escalera con las manos) a otro para trepar muros de más de 1.6 m.

- ✅ Muy humano y propio de los juegos de calle.
- ⚠️ Complica la red y el balance (¿se puede ayudar al que huye de ti?).
- Mi sugerencia: dejarlo para **Fase 3** y solo en modos por equipos.

---

## 8. Cámara y sensación en primera persona

### 8.1 Principios 🟡

- **Cuerpo completo visible** (manos, brazos, piernas, pies al mirar abajo). Nada de "cámara flotante con armas".
- **Altura de cámara baja** (1.28 m), para que el barrio se vea *grande*.
- **Campo de visión:** 80° por defecto (ajustable 70–100°).

### 8.2 Efectos de cámara

| Efecto | Descripción | Opción de accesibilidad |
|---|---|---|
| Balanceo al caminar/correr | Movimiento suave arriba/abajo y de lado | Sí (intensidad 0–100%) |
| Respiración | Subida y bajada del pecho al cansarse | Sí |
| Inclinación al girar | Ligero ladeo al cambiar de dirección | Sí |
| Impacto al aterrizar | Bajada breve de cámara | Sí |
| Visión de túnel leve | Aparece con muy poca estamina | Sí (se puede desactivar) |
| Ritmo cardíaco | Sonido, **no** efecto de pantalla agresivo | Sí |

> ⚠️ **Mareo (motion sickness):** la primera persona con movimiento rápido marea a mucha gente. Todos los efectos de cámara deben poder bajarse o apagarse desde el menú, y los valores por defecto deben ser **suaves**.

### 8.3 Visibilidad en primera persona (problema conocido)

En primera persona es difícil saber **qué hay a los lados y atrás**. Soluciones 🟡:

- Sonidos 3D muy claros (pasos, respiración, ladridos del perro).
- Indicador discreto de **"alguien viene"** cuando un perseguidor está muy cerca por detrás (un latido de sonido, sin flecha en pantalla).
- Opción de **mirar rápido atrás** con una tecla (giro del cuello, 0.3 s, con costo de velocidad).
- Mapa del barrio memorizable: calles en forma de cuadrícula simple, con puntos de referencia llamativos (el colmado, el edificio, la banca).

### 8.4 Controles propuestos 🟡

| Acción | Teclado y ratón | Mando |
|---|---|---|
| Mover | WASD | Stick izquierdo |
| Mirar | Ratón | Stick derecho |
| Correr / esprintar | Mayús (mantener) | Stick izquierdo presionado / gatillo |
| Agacharse | Ctrl (mantener) | B / círculo |
| Saltar / trepar | Espacio | A / X |
| Topar | Clic izquierdo | Gatillo derecho |
| Interactuar (empujar, abrir) | E | X / cuadrado |
| Mirar atrás | Q (mantener) | Bumper |
| Esquivar | Alt + dirección | Bumper izq. + dirección |
| Emote / frase | G (rueda) | D-pad |

> El salto/trepar es **contextual**: la misma tecla salta, hace vault o trepa según el obstáculo y tu velocidad.

---

## 9. Mecánica de topar

### 9.1 Cómo se topa 🟡

1. Estás cerca de otro jugador (menos de **1.0 m**).
2. Presionas **Topar**: tu personaje **extiende el brazo** (animación de ~0.25 s).
3. Si la mano alcanza a la otra persona, se registra el tope.

| Parámetro | Valor inicial |
|---|---|
| Alcance | 0.9 m desde la cámara |
| Cono de alcance | 70° al frente |
| Tiempo de la animación | 0.25 s |
| Enfriamiento si fallas | 0.8 s (no puedes "spamear") |
| Requiere estar de frente | Sí; topar por la espalda **sí vale** si lo alcanzas |

### 9.2 Qué NO pasa al topar ✅

- No se empuja ni se derriba a nadie. Cero física de golpe.
- No hay daño, ni sangre, ni aturdimiento.
- Es un **toque de mano** con una palmada sonora y la frase "¡Topao!".

### 9.3 Retroalimentación 🟡

- Sonido de palmada + voz.
- Pantalla: breve destello cálido (no agresivo).
- El nuevo topao ve un indicador en el HUD (ver sección 17).
- Los demás jugadores reciben un aviso de audio global ("¡Fulano la lleva!").

### 9.4 Justicia en red (importante)

En línea, el "alcance" debe ser **justo** con latencia alta. Propuesta técnica (ver 14.5): validación en el servidor con **tolerancia retroactiva** (lag compensation) de hasta 150 ms. Si hay duda, se favorece al que **huye** (para no frustrar).

---

## 10. Esconderse y sigilo

La mezcla arcade + táctico se apoya en esconderse y ser descubierto.

### 10.1 Sistema de "ruido" 🟡

Todo lo que haces genera ruido que escucha el topao (jugador real) y los NPCs.

| Acción | Radio de ruido |
|---|---|
| Quieto agachado | 0 m |
| Gatear | 1 m |
| Caminar | 4 m |
| Trotar | 8 m |
| Correr | 14 m |
| Esprintar | 20 m |
| Respiración agitada (cansado) | +8 m |
| Pisar charco / basura / lata | +6 m |
| Trepar reja de malla | 12 m |
| Aterrizar fuerte | 10 m |

### 10.2 Sistema de visión 🟡

- La visión es simplemente la **cámara real**: si el topao te ve, te ve.
- Los escondites reducen la visibilidad parcial (ver abajo).
- **Sombra:** los callejones oscuros ayudan a esconderse. Con la exposición automática de HDRP, el ojo del topao se ajusta tras salir de la luz, y por unos segundos **le cuesta ver en lo oscuro** (esto es un efecto real de HDRP y bien aprovechable).

### 10.3 Escondites 🟡

| Escondite | Cómo funciona | Riesgo |
|---|---|---|
| Detrás de un poste/carro/pared | Quedas fuera de la línea de visión | Bajo |
| Debajo de una mesa o banca | Agachado, parcialmente visible | Medio |
| Entre sillas y cajas apiladas | Visible desde algunos ángulos | Medio |
| Rack de botellones de agua | Escondes parte del cuerpo | Medio |
| Patio trasero (zona de riesgo) | Muy seguro, pero con el perro | Alto |
| Azotea ("plato") | Visión amplia, pero una sola salida | Alto |

> ⚠️ **Seguridad:** por ser un juego de niños, **nada de esconderse dentro de electrodomésticos cerrados** (freezer, nevera, baúl). Se esconden *detrás* o *debajo*, nunca *dentro* de cosas donde un niño real podría quedar atrapado.

### 10.4 Anti-camping 🟡

- No hay escondites donde se pueda quedar 100% invisible indefinidamente.
- La **respiración** se escucha cuando llevas quieto mucho tiempo y la estamina sigue baja.
- Algunos escondites **expulsan** al jugador si el topao lo "ve" varias veces.

---

## 11. Interacción con el entorno

### 11.1 Objetos empujables 🟡

| Objeto | Peso | Efecto |
|---|---|---|
| Silla plástica | Ligero | Se empuja o se tumba en el camino (el que viene detrás se frena/esquiva) |
| Caja de refrescos | Medio | Se empuja; ocupa espacio en un callejón |
| Botellón de agua vacío | Ligero | Rueda, hace ruido |
| Cubo de basura | Medio | Se tumba, ensucia el camino |
| Pelota | Ligero | Se patea; sirve de distracción |

**Regla de justicia:** obstáculos empujados **no hacen daño** y se pueden saltar o rodear. Solo ralentizan 0.3–0.5 s.

### 11.2 Rutas abiertas y cerradas 🟡

| Elemento | Qué se puede hacer |
|---|---|
| Portón de callejón | Se abre/cierra (no se atranca) |
| Reja baja | Se puede trepar |
| Puerta de patio | Abierta solo en ciertas rondas |
| Escalera exterior | Siempre disponible, sube a la azotea |

### 11.3 Distracciones 🟡

| Distracción | Cómo | Resultado |
|---|---|---|
| Lanzar una piedrita/botella a un lado | Con la mano, corto alcance | El topao oye un ruido en esa dirección |
| Patear una lata | Empuja una lata por el piso | Ruido corto |
| Hacer "pssst" | Emote sonoro | Un NPC o jugador gira la cabeza |

> No se tiran objetos **a** las personas, solo **al lado** para hacer ruido. Mantiene el espíritu infantil sin violencia.

### 11.4 Tráfico dinámico ✅ (del documento maestro) / 🟡 detalles

Carros y motores circulan por la calle principal, **lentos y predecibles**. Para un juego de niños:

- Los vehículos **se detienen** si hay un jugador en la calle (no atropellan).
- Pitan, para que sea una **señal de ruido/obstáculo**, no de peligro de muerte.
- Pasan en intervalos regulares: **cruzar entre carros es táctico**, no letal.
- ❓ **Q13:** ¿prefieres que el tráfico sea completamente inofensivo (pasa y pita) o que haya riesgo, por ejemplo un empujón que te frena?

### 11.5 Elementos vivos del barrio 🟡

- La mesa de dominó con adultos que **comentan** si pasas corriendo.
- Un señor que **grita desde el balcón** cuando alguien pasa por su casa.
- Música lejana (bachata, merengue) de un colmado.
- Gallo o gallina en un patio.

---

## 12. Mapa: el barrio

### 12.1 Resumen

Un barrio semi-abierto y denso de **2 a 3 cuadras**, conectadas por callejones y pasadizos. Escala real.

```
 ┌────────────────────────────────────────────────────────────────────────┐
 |                           [ ZONA RESTRINGIDA ]                         |
 |                         Patios Traseros / Callejón                     |
 └───────┬────────────────────────────────────────────────────────┬───────┘
         │                                                        │
 ┌───────┴──────┐  ┌────────────────────────────────────┐  ┌──────┴───────┐
 |  CASA AZUL   |  |      EDIFICIO DE 3 PISOS (BLOCK)   |  | CASA AMARILLA|
 |  (Balcón y   |  |  [Escalera Exterior] [Plato/Azotea]|  | (Galería y   |
 |   Galería)   |  └─────────────────┬──────────────────┘  |  Rejas)      |
 └──────┬───────┘                    │                     └──────┬───────┘
        │                 [Callejón / Pasadizo Secundario]        │
 ═══════╧════════════════════════════╧════════════════════════════╧═══════
 [ ACERA IZQUIERDA ]                                   [ ACERA DERECHA ]
  * [ Poste de Luz ]                                    * [ Reja de Agua ]
  * [ BANCA DARÍO ]                                     * [ Rack Botellones]
  * [ Contén con Basura ]                               * [ Pared Menta/Rosa]
 ─────────────────────────────────────────────────────────────────────────
                          [ CALLE PRINCIPAL DE ASFALTO ]
                      (Tráfico dinámico: Motores y Carros)
 ─────────────────────────────────────────────────────────────────────────
 [ ACERA IZQUIERDA ]                                   [ ACERA DERECHA ]
  * [ Colmado Don Pepe ]                                * [ Entrada a Casa ]
  * [ Freezer y Mesa de Dominó ]                        * [ Perro Viralata ]
 ───────┬────────────────────────────────────────────────────────┬───────
        │                 [ ZONA ABIERTA / ESQUINAS ]            │
```

### 12.2 Dimensiones de referencia 🟡

| Elemento | Medida |
|---|---|
| Área jugable total | ~120 m × 90 m |
| Calle principal | 12 m de ancho |
| Aceras | 3 m, con contén de 0.15 m |
| Callejones | 1.4 a 2.2 m de ancho (estrechos, tensos) |
| Pasadizo secundario | 2.5 m |
| Edificio de 3 niveles | 9–10 m de alto, escalera exterior de cemento |
| Casas de 1–2 niveles | 3.2 a 6.5 m de alto |
| Patios traseros | 10 m de profundidad |

### 12.3 Zonas y su función de juego 🟡

| Zona | Función | Parkour humano |
|---|---|---|
| **Calle principal** | Corredor rápido, expuesto, con tráfico | Correr; cruzar entre carros |
| **Aceras** | Rutas laterales con obstáculos (postes, bancas, basura) | Vault bajo, esquivar |
| **Callejón / pasadizo** | Atajos estrechos y oscuros; ideales para esconderse | Gatear; pasar sin esprint |
| **Edificio de 3 pisos** | Vertical: escalera exterior, azotea | Subir escalera; sin saltos entre azoteas |
| **Casa Azul / Amarilla** | Balcón, galería, rejas | Rejas bajas trepables |
| **Colmado Don Pepe** | Punto de referencia, mesas, sillas, racks | Obstáculos para empujar |
| **Zona abierta / esquinas** | Cruces y visibilidad amplia | Persecución a campo abierto |
| **Zona restringida (patios traseros)** | Zona de riesgo/premio (ver 12.5) | Rejas, muros, el perro |

### 12.4 Verticalidad con límites humanos 🟡

- **Azotea (el "plato"):** accesible por la escalera exterior de cemento. Tiene **baranda o muro de 1.1 m** todo alrededor.
- **No hay saltos entre azoteas** (salvo huecos de menos de 1.0 m, con baranda).
- **Balcones:** se puede pasar de un balcón a otro solo si están a menos de 1.0 m de distancia y con baranda; si no, no.
- **No hay forma de bajar saltando** desde más de 1.2 m: se baja por escalera o hay un obstáculo (contén, caja) que lo impide.

> ❓ **Q14:** ¿Te gustan las azoteas como zona de juego, o prefieres que el juego quede a nivel de calle y primer piso?

### 12.5 Zona restringida 🟡 ❓ Q15

El documento original marca la "ZONA RESTRINGIDA" (patios traseros) pero no define qué es. Mi propuesta:

> **Los patios traseros son "propiedad del vecino bravo".** Entrar es posible pero arriesgado: hay un **perro** que ladra y delata tu posición, y un **señor** que puede gritarte y sacarte.

| Opción | Cómo funcionaría |
|---|---|
| **A. Riesgo/premio** (recomendada) | Puedes entrar. Mucho escondite, pero el perro te delata y hay una sola salida. |
| **B. Prohibida de verdad** | Entrar te expulsa a la calle tras 3 s con un grito del vecino. |
| **C. Zona de bonus** | Hay un objeto a recoger que da puntos pero te hace ruidoso. |

### 12.6 Puntos de referencia (landmarks) 🟡

Para orientarse en primera persona:

1. **Colmado Don Pepe**: rótulo grande, jaulas azules, música.
2. **Banca Darío**: letrero de colores brillantes.
3. **Edificio de block de 3 pisos**: silueta alta, varillas arriba.
4. **Casa Azul / Casa Amarilla / Casa Menta / Casa Rosada**: colores distintos.
5. **Poste con cables**: punto de cruce en la calle principal.

### 12.7 Spawn y bases 🟡

- 8 puntos de spawn dispersos en zonas distintas.
- 4 a 6 bases ("libre"): poste, portón del callejón, pared pintada, esquina del colmado, escalón de la Casa Azul.
- El topao inicial empieza en el **centro** (cruce principal).

### 12.8 Mapas futuros ❓ Q16

Una vez funcione el primer barrio, podrían venir: **barrio del campo / batey**, **playa**, **plaza del pueblo**, **zona colonial**. Por ahora: **un solo mapa**.

---

## 13. NPCs y bots

> ❓ **Q9:** ¿Los bots son para rellenar partidas, o también para jugar solo (modo offline)?

### 13.1 Tipos 🟡

| Tipo | Rol | Interactúa con el juego |
|---|---|---|
| **Bot jugador** | Ocupa un puesto vacío; huye y persigue como un niño | Sí, compite |
| **NPC ambiente** | Gente que vive en el barrio | Solo reacciona |
| **Perro viralata** | Ladra y delata | Revela posición |
| **Vecinos** | Gritan, abren portones, estorban | Reacciona |
| **Conductores** | Manejan lento y pitan | Obstáculo móvil |

### 13.2 Bots que juegan como humanos 🟡

El bot **no** debe ser un robot perfecto. Reglas de diseño:

1. **Percepción limitada:** ve solo en un cono de 100° y a ~35 m; oye con ruido real (misma tabla del 10.1).
2. **Memoria corta:** recuerda "el último lugar donde vi al jugador" durante ~8 s.
3. **Se cansa:** usa la misma estamina que el jugador.
4. **Comete errores:** a veces elige mal el atajo, duda en una esquina, se distrae con el perro.
5. **Tiene "personalidad":**

   | Personalidad | Comportamiento |
   |---|---|
   | **El veloz** | Esprinta mucho; se cansa rápido |
   | **El listo** | Usa atajos y escondites; cuida su estamina |
   | **El despistado** | Se distrae; a veces corre sin rumbo |
   | **El cabezón** | Persigue a un solo jugador hasta lograrlo |

6. **Dificultad ajustable:** Fácil / Normal / Difícil cambia el tamaño de memoria, el tiempo de reacción y la tasa de error, **no** las capacidades físicas (sigue siendo humano).

### 13.3 Arquitectura de IA 🟡

- **Navegación:** NavMesh de Unity con **Off-Mesh Links** para vault, trepar y gatear (los mismos que usa el jugador).
- **Decisiones:** Behaviour Tree o Utility AI con estados:
  `Esconderse → Huir → Buscar → Perseguir → Descansar → Ir a Base`.
- **Sensores:** visión (cono + raycast), oído (eventos de ruido), memoria (última posición conocida).
- **Datos:** parámetros en `ScriptableObject`.

### 13.4 NPC ambiente 🟡

- Rutinas simples (jugar dominó, barrer, hablar en una esquina).
- Reacción al jugador: giran la cabeza, hacen comentarios, a veces cierran un portón.
- Presupuesto: ~15–25 NPC simultáneos en pantalla (instancias con *GPU instancing* y animación simplificada).

### 13.5 El perro viralata 🟡

- Dormita en su zona; si pasas muy cerca o corriendo, **ladra** y todos oyen.
- No muerde ni persigue (juego infantil).
- Puede **seguir** a un jugador un rato (un compañero gracioso) o quedarse ladrando en un sitio.
- ❓ **Q17:** ¿el perro puede ser "amigo" del jugador (le tiras comida y deja de ladrar)?

### 13.6 Sobre el LLM local ❗

Los modelos de Ollama (`qwen2.5-coder`, `deepseek-r1`) son **herramientas de desarrollo** (generar código, scripts de editor, matemáticas), **no** parte del juego en ejecución. Los bots usan IA clásica (NavMesh + Behaviour Tree). Esto es importante para el rendimiento y para que el juego corra en cualquier PC.

---

## 14. Multijugador online

> ✅ Confirmado: multijugador online es el núcleo.
> Esta sección es la que más decisiones técnicas abiertas tiene.

### 14.1 Opciones de red ❓ Q4

| Opción | Pros | Contras |
|---|---|---|
| **Netcode for GameObjects (NGO)** + Unity Relay/Lobby | Oficial, integrado con Unity 6, buena documentación | Menos potente en predicción |
| **Netcode for Entities** | Muy escalable, predicción integrada | Requiere ECS/DOTS: curva de aprendizaje alta |
| **Fish-Net** | Predicción de clientes, rendimiento, gratuito | Comunidad más pequeña |
| **Mirror** | Maduro, mucha comunidad | Menos moderno |
| **Photon Fusion** | Predicción y rollback muy buenos | Dependes de su servicio, pago por CCU |

**Mi recomendación 🟡: Netcode for GameObjects + Unity Relay + Lobby** para empezar (menos riesgo), con una capa de abstracción para poder cambiar después.

### 14.2 Modelo de red 🟡

- **Servidor autoritativo** (un jugador hace de *host* en Fase 1; servidores dedicados en Fase 4).
- **Predicción en el cliente** para el movimiento propio (se siente inmediato).
- **Interpolación** para los demás jugadores.
- **Validación del servidor** para topes, bases, objetos y cambios de rol.

### 14.3 Frecuencia 🟡

| Valor | Cifra |
|---|---|
| Tick del servidor | 30 Hz (suficiente para velocidades humanas) |
| Envío de estado de jugadores | 20–30 Hz |
| Latencia objetivo | < 100 ms jugable; hasta 200 ms tolerable |

> Como los niños corren a velocidades **bajas** (≤ 5.2 m/s), la red perdona mucho más que en un shooter. Es una ventaja real del "parkour humano".

### 14.4 Qué se sincroniza

| Dato | Autoridad |
|---|---|
| Posición, estado de movimiento (corriendo, agachado, trepando) | Cliente predice, servidor valida |
| Estamina | Servidor |
| Quién es el topao, tiempos, puntos | Servidor |
| Topes | Servidor |
| Objetos empujables | Servidor (física simplificada) |
| Estado de portones y bases | Servidor |
| NPC y bots | Servidor (se replican posición y animación) |
| Tráfico | Servidor (o determinista por semilla) |

### 14.5 Detección de topes con latencia 🟡

1. El cliente envía "intento de topar" con un sello de tiempo.
2. El servidor **retrocede** las posiciones de los jugadores (hasta 150 ms) para evaluar.
3. Si el tope vale con la posición pasada, se acepta; si hay duda, se **favorece al que huye**.
4. Se envía el resultado a todos.

### 14.6 Salas y emparejamiento 🟡

| Función | Fase |
|---|---|
| Crear sala con **código** y jugar con amigos | **1** |
| Lista de salas públicas | 2 |
| Emparejamiento automático | 3 |
| Servidores dedicados | 4 |

### 14.7 Reconexión y abandonos 🟡

- Si te desconectas: tienes 30 s para volver; mientras tanto un bot toma tu lugar.
- Si el *host* se va (modelo host-cliente): migración de host **no** en Fase 1 (la partida termina con aviso).

### 14.8 Seguridad infantil y moderación ❗ 🟡

Al ser un juego para niños y online, esto no es opcional:

- **Sin chat de texto libre.** Solo **frases rápidas** preestablecidas y emotes.
- **Sin chat de voz** con desconocidos (opcional solo con amigos/códigos).
- Nombres de jugador **filtrados** (lista de palabras y nombres prefabricados).
- **Sin datos personales** pedidos al jugador.
- Reporte y bloqueo simples.
- ❓ **Q18:** ¿Tienes en mente publicar en Steam, móvil, o solo PC propio? Esto cambia las normas de privacidad (COPPA, GDPR-K) y la clasificación.

### 14.9 Anti-trampa básica 🟡

- Servidor autoritativo para estamina, velocidad y topes.
- Detección de velocidades imposibles ("speed hack").
- Sin información privilegiada enviada al cliente (no se manda la posición de quien no deberías ver).

---

## 15. Modos de juego

| Modo | Descripción | Fase |
|---|---|---|
| **Topao Clásico** | Reglas base (sección 5): gana quien menos la lleva | **1** |
| **Práctica / Recorrido libre** | Mapa sin presión, para aprender el barrio y los atajos | **1** |
| **Contra bots** | Partida rápida contra IA | **2** |
| **Topao con bases** | Más bases, más táctico | 2 |
| **Topao Congelado** | Los topados se congelan hasta ser liberados | 3 |
| **Dos equipos** | Perseguidores vs. corredores | 3 |
| **Eventos temporales** | Barrio de noche, lluvia, apagón ("se fue la luz") | 4 |

> 💡 "Se fue la luz" es un evento muy dominicano y encaja con HDRP: apagón en el barrio → oscuridad, faroles de motor, linternas de celular. Gran idea visual para una fase posterior.

---

## 16. Progresión y personalización

### 16.1 Principios ✅/🟡

- **Cero ventajas de juego** por progresión: todos tienen las mismas capacidades.
- Solo se desbloquean **cosméticos** y **frases/emotes**.
- Sin cajas sorpresa. Sin presión de compra.

### 16.2 Ejemplos de cosméticos 🟡

| Categoría | Ejemplos |
|---|---|
| Ropa | Camisetas de béisbol, de colores, de escuela |
| Calzado | Chancletas, tenis, botas de goma |
| Cabeza | Gorras, cintas, peinados |
| Accesorios | Mochila escolar, bola de goma, trompo |
| Emotes | Bailar un ritmo, reír, burlarse con cariño |
| Frases | Frases de barrio (ver 6.4) |

### 16.3 Cómo se desbloquea ❓ Q19

| Opción | Descripción |
|---|---|
| **A. Por jugar** | XP al terminar partidas |
| **B. Por logros** | "Esquivar a 3 en una ronda", "No ser topado en 2 min" |
| **C. Tienda con moneda del juego** | Ganada jugando |
| **D. Tienda con dinero real** | Cosméticos de pago |

Mi sugerencia 🟡: **A + B + C**. La opción D se decide más adelante y ❓ según la plataforma.

### 16.4 Logros de ejemplo 🟡

- "El Fantasma": termina una ronda sin ser topado.
- "Atajo Maestro": usa 5 atajos distintos en una partida.
- "Conoce el Barrio": toca todas las bases.
- "Pie Ligero": esprinta sin cansarte (con buena gestión de estamina).
- "Pana Fiel": juega 10 partidas con el mismo amigo.

---

## 17. Interfaz (HUD y menús)

### 17.1 Filosofía 🟡

- **HUD mínimo y diegético:** pocos elementos en pantalla; lo demás se *siente* (sonido, cámara).
- Estética **dibujada a mano / infantil**: cartelitos de barrio, letras tipo rótulo de colmado.

### 17.2 HUD durante la partida 🟡

| Elemento | Posición | Muestra |
|---|---|---|
| Tiempo de ronda | Arriba al centro | Reloj |
| Rol actual | Arriba a la izquierda | "Eres el topao" o "Corre" |
| Estamina | Abajo a la izquierda | Barra discreta **o** solo la respiración (opción) |
| Marcador | Arriba a la derecha | Tiempo acumulado como topao |
| Aviso de base | Centro-abajo | "Libre: 6… 5…" |
| Cuenta atrás | Centro | "Uno… dos…" |
| Indicador de ruido | Esquina | Cuánto ruido haces (opcional) |
| Chat rápido | Rueda | Frases y emotes |

### 17.3 Menús 🟡

1. **Menú principal:** Jugar · Practicar · Personaje · Opciones · Salir
2. **Lobby:** lista de jugadores, código de sala, selección de mapa/modo
3. **Resultados:** tabla con tiempos, topes, logros de la partida
4. **Opciones:** gráficos (calidad HDRP), controles, accesibilidad, audio, idioma

### 17.4 Idiomas 🟡 ❓ Q20

- Español (República Dominicana) como idioma principal.
- Inglés como segundo idioma.
- ¿Español neutro adicional?

---

## 18. Arte y dirección visual (HDRP)

### 18.1 Estilo ✅/🟡

Fotorrealista cálido del **documento maestro**, con un matiz para el público infantil:

- **Colores saturados** pero no irreales.
- **Texturas ligeramente estilizadas** (no fotogrametría fría).
- Personajes con **proporciones infantiles realistas**, no caricatura extrema. ❓ **Q21:** ¿personajes más realistas o más estilizados?

### 18.2 Iluminación ✅

| Elemento | Valor |
|---|---|
| Sol | ~100,000 lux, tono cálido (~5000 K), elevación ~30°, 4:30 PM |
| Cielo | Physically Based Sky |
| Niebla volumétrica | Polvo en el aire a contraluz |
| GI | SSGI (rebote de color de casas sobre el cemento) |
| Exposición | Automática (adaptación al entrar/salir de callejones) |
| Decals | Humedad, grietas y charcos en contenes |
| Sombras | Suaves, con *contact shadows* |

> La configuración de iluminación ya está en `ElTopao/Assets/ElTopao/Editor/ElTopaoLightingSetup.cs`.

### 18.3 Arquitectura y props ✅ (con ajustes de la sección 2.2)

- Edificios de block gris crudo con bloques calados, varillas de azotea y escaleras exteriores.
- Casas pintadas de menta, amarillo, azul cielo y rosado, con techos de zinc oxidado.
- Postes de concreto con **cables cruzando**.
- Colmadones con rejas y jaulas azules de botellones.
- Sillas plásticas de colores, cajas, basura junto al contén.

### 18.4 Presupuesto visual (rendimiento) 🟡

Objetivo en una **RTX 5070 (12 GB)**:

| Calidad | Resolución | FPS | Notas |
|---|---|---|---|
| Alta | 1440p | 60 | SSGI + volumétrica + decals activos |
| Media | 1080p | 60 | SSGI reducido, volumétrica baja |
| Baja | 1080p | 60 | Sin SSGI, sin volumétrica; para PC modestas |

> ⚠️ **HDRP es pesado.** Si el público objetivo incluye PC modestas o móvil, hay que reconsiderar URP. ❓ **Q22** (sobre plataformas).

### 18.5 Pipeline de assets 3D ✅ (del documento maestro)

| Tipo | Herramienta | Salida |
|---|---|---|
| Personajes y props principales | Hunyuan3D-2 → rig con Mixamo/AccuRIG | `.fbx` |
| Arquitectura | TRELLIS | `.glb` / `.fbx` |
| Props rápidos | TripoSR / SF3D | `.glb` |
| Texturas PBR | Flux.1 Dev (GGUF Q4) / SDXL (seamless) | `.png` |

**Riesgos del pipeline de IA:**

- Los modelos generados necesitan **limpieza** (retopología, UV, escala real).
- **Personajes infantiles:** Mixamo está pensado para proporciones adultas; la animación retargeteada a un cuerpo de niño puede verse mal. Alternativa: animaciones propias o mocap con niños (con permisos) o animadores.
- Todos los assets deben cumplir **escala real en metros** (un niño mide 1.40, un contén 0.15).

---

## 19. Audio

### 19.1 Ambiente del barrio 🟡

- Motores, pitos, bocinas.
- Música de colmado lejana (bachata, merengue, dembow suave ❓ **Q23**: licencias o composición propia).
- Fichas de dominó.
- Gallos, perros, vecinos conversando.
- Viento, cables eléctricos zumbando.

### 19.2 Sonido del jugador (importante por el sigilo) 🟡

- Pasos distintos según superficie: asfalto, cemento, tierra, charco, zinc, escalera metálica.
- Respiración con 3 intensidades.
- Roce de ropa, chancletas.
- Esfuerzo al trepar.

### 19.3 Voz 🟡

- Voces de **niños** (actores/actrices infantiles con consentimiento y permisos de tutor, o voces sintetizadas con licencia clara). ❓ **Q24**.
- Frases cortas, acento dominicano natural.

### 19.4 Música 🟡

- Dinámica: tranquila al esconderse, más ritmo al ser perseguido.
- Instrumentos de percusión de barrio (güira, tambora) para el ritmo de la persecución.

---

## 20. Arquitectura técnica

### 20.1 Stack ✅

| Componente | Elección |
|---|---|
| Motor | Unity 6 (6000.5.5f1) |
| Render | HDRP 17.5 |
| Red | ❓ Q4 (propuesta NGO + Relay) |
| Lenguaje | C# |
| IA de desarrollo local | Ollama (`qwen2.5-coder:14b/7b`, `deepseek-r1:14b`) |

### 20.2 Estructura de carpetas 🟡

```
Assets/ElTopao/
├── Runtime/              (asmdef: ElTopao.Runtime)
│   ├── Core/             GameState, rondas, reglas, puntuación
│   ├── Player/           Movimiento, estamina, interacción
│   ├── Tagging/          Topar, roles, bases
│   ├── Stealth/          Ruido, visión, escondites
│   ├── AI/               Bots, NPC, perro
│   ├── Network/          Sincronización, salas
│   ├── World/            Zonas (BarrioZone), objetos, portones
│   ├── UI/               HUD, menús
│   └── Audio/
├── Editor/               (asmdef: ElTopao.Editor)
│   ├── ElTopaoLightingSetup.cs   ← ya creado
│   └── Herramientas de mapa/blockout
├── Data/                 ScriptableObjects de parámetros
├── Art/  Prefabs/  Scenes/  Settings/
```

### 20.3 Sistemas principales 🟡

| Sistema | Responsabilidad |
|---|---|
| `MatchManager` | Estado de partida, rondas, ganadores |
| `RoleSystem` | Quién es topao, inmunidades, enfriamientos |
| `PlayerMotor` | Movimiento cinemático (o `CharacterController`) con inercia |
| `MovementStateMachine` | Caminar / correr / vault / mantle / gatear / caer |
| `ObstacleDetector` | Detecta alturas y decide la acción contextual |
| `StaminaSystem` | Estamina y estados de cansancio |
| `TagInteractor` | Alcance, validación y resultado de topes |
| `NoiseSystem` | Emite y escucha ruidos |
| `SafeBase` | Bases ("libre") |
| `BotBrain` | Percepción y decisiones de los bots |
| `NetworkSync` | Replicación y predicción |
| `HudController` | Muestra la información |

### 20.4 Movimiento: decisión técnica ❓ Q25

| Opción | Pros | Contras |
|---|---|---|
| `CharacterController` de Unity | Simple, rápido | Poco control fino; difícil de predecir en red |
| **Motor cinemático propio** | Control total; predicción de red más fácil | Más trabajo |
| Rigidbody | Física natural | Impredecible en red; difícil de afinar |

Recomendación 🟡: **motor cinemático propio**, determinista y apto para predicción en red.

### 20.5 Animación en primera persona 🟡

- Dos modelos: **cuerpo completo** (visto por los demás) y **brazos/cuerpo en primera persona** (visto por ti).
- *Animation Rigging* para IK de manos (agarrar muros, apoyarse en vault).
- Animaciones de vault y mantle con *root motion parcial* y *motion warping*.

### 20.6 Datos y balance 🟡

- Todos los valores del [Apéndice A](#apéndice-a-tabla-de-parámetros-iniciales) viven en `ScriptableObject`s (`MovementConfig`, `StaminaConfig`, `TagConfig`, `MatchConfig`).
- Esto permite ajustar sin recompilar y hacer pruebas A/B.

### 20.7 Pruebas 🟡

- Pruebas unitarias (Edit Mode) de reglas: estamina, roles, puntuación.
- Pruebas de Play Mode de movimiento.
- Un **nivel de pruebas de parkour** con todos los obstáculos a alturas reales (0.2, 0.5, 0.8, 1.2, 1.6, 2.0 m), para validar el cuerpo humano.

---

## 21. Hoja de ruta

| Fase | Objetivo | Entregable |
|---|---|---|
| **0. Diseño** (ahora) | Cerrar este documento y las preguntas abiertas | GDD v1.0 aprobado |
| **1. Prototipo gris** | Mover a un niño en un barrio de cajas grises | Nivel de pruebas de parkour, movimiento + estamina |
| **2. Bucle de topar (offline)** | Topar, roles, bases, rondas, puntuación | Partida local con 1 bot básico |
| **3. Red** | Salas con código, 2–4 jugadores, topes validados | Primer playtest online con amigos |
| **4. Mapa jugable** | Barrio con arte, iluminación HDRP, colisiones reales | Mapa completo |
| **5. Bots, NPC y perro** | IA humana y barrio vivo | Partidas completas con bots |
| **6. Pulido** | Audio, cosméticos, HUD final, optimización | Beta cerrada |
| **7. Lanzamiento** | Servidores, moderación, plataformas | v1.0 |

> 💡 **Regla de oro:** no se construye arte final hasta que el movimiento de un niño "se sienta bien" en cajas grises. Si la persecución no es divertida con cajas, no lo será con HDRP.

---

## 22. Preguntas abiertas

Contesta las que quieras; si no, avanzo con mi propuesta.

### Visión y alcance
- ~~**Q1.**~~ ✅ *Ambos.* ¿Es un juego **para niños** (jugadores niños) o **sobre** la infancia (nostalgia para adultos)? Esto cambia clasificación, chat, tienda y tono.
- ~~**Q2.**~~ ✅ *Solo decorativo y sin marcas.* Cerveza, banca de lotería y dominó con bebidas alcohólicas: ¿los reemplazamos como propongo (2.2)?
- **Q3.** ¿Cuántos jugadores por partida? (propuesta: 4 a 8, ideal 6).

### Reglas
- ~~**Q4.**~~ ⏳ *Se decide después de un prototipo de movimiento.* (Propuesta de partida: Netcode for GameObjects + Unity Relay.)
- **Q5.** ¿Cuáles eran las **reglas reales** del topao en tu barrio?
- **Q6.** ¿Qué decían los niños para estar a salvo ("libre", "casa", "paso", otra)?
- ~~**Q7.**~~ ✅ *Menos tiempo como topao.* ¿Gana quien **menos tiempo** la lleva (mi propuesta), o hay otra condición (ser el último sin ser topado, topar a todos…)?
- **Q8.** ¿Una partida es **una sola ronda larga** o varias rondas cortas?

### NPCs
- **Q9.** ¿Los bots son solo relleno o también un modo **offline** completo?
- **Q17.** ¿El perro puede hacerse amigo del jugador?

### Personajes
- **Q10.** ¿Equipos de béisbol reales (Licey, Águilas…) o inventados (por licencias)?
- **Q11.** ¿Chancleta que se sale al cansarse: gracioso o molesto?
- **Q21.** ¿Personajes realistas o ligeramente estilizados?

### Movimiento
- **Q12.** ¿Cooperación para trepar ("dar el pie")? ¿En qué modos?
- **Q26.** ¿Quieres que el jugador pueda **fallar** un vault (caer, tropezar), o que el vault siempre salga bien si cumple condiciones?
- **Q27.** ¿Todos los niños tienen **las mismas capacidades** (justo) o puede haber leves diferencias (uno más veloz, otro más ágil)?

### Mundo
- **Q13.** ¿Tráfico inofensivo o con algún riesgo (frenar al jugador)?
- **Q14.** ¿Azoteas jugables o solo nivel de calle?
- **Q15.** ¿Qué es la **zona restringida**? (opciones A, B, C en 12.5)
- **Q16.** ¿Habrá más mapas o por ahora uno solo?
- **Q28.** ¿Hora fija (4:30 PM) o ciclo de día, lluvia y apagón desde el principio?

### Progresión y negocio
- **Q18.** ¿Plataforma de lanzamiento? (Steam / móvil / consolas / solo PC propio)
- **Q19.** ¿Cómo se desbloquean los cosméticos? (opciones A–D en 16.3)
- **Q29.** ¿Es un proyecto personal, comercial o ambos?

### Presentación
- **Q20.** ¿Idiomas?
- **Q22.** ¿Qué hardware mínimo apuntamos? (HDRP exige PC decente)
- **Q23.** ¿Música licenciada o compuesta?
- **Q24.** ¿Voces: actores, o IA con licencia?

### Técnico
- **Q25.** ¿Motor de movimiento propio (recomendado) o `CharacterController`?
- **Q30.** ¿Quieres soporte de **mando** desde el principio?
- **Q31.** ¿Quién más trabaja en el proyecto (arte, sonido, programación) o es un desarrollo solo/asistido por IA?

---

## 23. Riesgos

| Riesgo | Probabilidad | Impacto | Mitigación |
|---|---|---|---|
| El movimiento "humano" resulta aburrido | Media | Alto | Prototipo gris temprano; ajustar valores; el cansancio y los atajos crean tensión |
| Mareo en primera persona | Alta | Medio | Efectos de cámara suaves y apagables; FOV ajustable |
| Red + persecución a pie se siente injusta con lag | Media | Alto | Servidor autoritativo, lag compensation, favorecer al que huye |
| HDRP demasiado pesado para el público | Media | Alto | Perfiles de calidad; definir hardware mínimo (Q22) |
| Rigging de niños mal con Mixamo | Alta | Medio | Probar pronto; animación propia o retargeting cuidadoso |
| Assets generados por IA inconsistentes | Alta | Medio | Guía de estilo, limpieza, escala real, revisión manual |
| Seguridad infantil online | Media | Muy alto | Sin chat libre, filtros, normativa COPPA/GDPR-K |
| Alcance demasiado grande | Alta | Alto | Fases; empezar con 1 mapa, 1 modo, sala por código |
| Derechos de música, marcas y equipos | Media | Medio | Contenido inventado o licenciado |
| Cámara en primera persona no deja ver atrás | Alta | Medio | Audio 3D, mirada atrás, indicador sutil |

---

## 24. Glosario

| Término | Significado |
|---|---|
| **Topao / topar** | Tocar a alguien con la mano para pasarle el rol de perseguidor |
| **Libre / base** | Lugar donde estás temporalmente a salvo (nombre ❓ Q6) |
| **Colmado** | Tienda de barrio dominicana |
| **Banca** | Local de la comunidad (en el original, de lotería; ver 2.2) |
| **Contén** | Borde de la acera junto a la calle |
| **Plato / azotea** | Techo plano de un edificio |
| **Callejón** | Pasillo estrecho entre edificios |
| **Zinc** | Lámina metálica de techo |
| **Block** | Bloque de cemento de construcción |
| **Viralata** | Perro callejero |
| **Vault** | Salto sobre un obstáculo apoyando las manos |
| **Mantle** | Subirse a una superficie alta con esfuerzo |
| **Flato** | Dolor de costado al correr cansado |
| **Estamina** | Resistencia física del personaje |
| **HDRP** | High Definition Render Pipeline (render de Unity) |
| **SSGI** | Iluminación global en espacio de pantalla |
| **Off-Mesh Link** | Conexión de NavMesh para saltos y trepadas de la IA |

---

## Apéndice A: tabla de parámetros iniciales

> Todos 🟡 y ajustables. Se guardarán en `ScriptableObject`s.

### Cuerpo
| Parámetro | Valor |
|---|---|
| Altura | 1.40 m |
| Altura de ojos | 1.28 m (agachado 0.85 m) |
| Radio de colisión | 0.28 m |
| Altura de colisión | 1.35 m (agachado 0.90 m) |

### Velocidades (m/s)
| Acción | Valor |
|---|---|
| Caminar | 1.3 |
| Trotar | 2.6 |
| Correr | 4.0 |
| Esprintar | 5.2 |
| Agachado | 0.9 |
| Gateando | 0.5 |
| Jadeo (tope) | 3.0 |
| Empujando | 60 % |

### Aceleración
| Parámetro | Valor |
|---|---|
| 0 → correr | 0.8 s |
| Correr → 0 | 0.4 s |
| Giro 180° corriendo | 0.7 s (−40 % velocidad) |

### Saltos y obstáculos
| Parámetro | Valor |
|---|---|
| Salto vertical parado | 0.30 m |
| Salto vertical corriendo | 0.45 m |
| Salto largo máximo | 1.8 m |
| Vault máximo | 1.2 m |
| Mantle máximo | 1.6 m |
| Reja de malla | hasta 2.0 m |
| Caída segura | 1.2 m |
| Caída con tropiezo | 1.2–2.0 m |

### Estamina
| Parámetro | Valor |
|---|---|
| Máxima | 100 |
| Correr | −8 / s |
| Esprintar | −20 / s |
| Vault | −8 |
| Mantle | −20 |
| Saltar | −5 |
| Recuperar caminando / trotando / quieto | +12 / +4 / +18 por s |
| Retraso de recuperación | 1 s |
| Umbral jadeo | 20 |

### Topar
| Parámetro | Valor |
|---|---|
| Alcance | 0.9 m |
| Cono | 70° |
| Animación | 0.25 s |
| Enfriamiento al fallar | 0.8 s |
| Inmunidad tras ser tocado | 3 s |
| Espera del nuevo topao | 3 s |
| Sin re-tope al mismo jugador | 10 s |

### Partida
| Parámetro | Valor |
|---|---|
| Jugadores | 4–8 |
| Rondas | 3 |
| Duración de ronda | 4 min |
| Cuenta inicial | 10 s |
| Bases: tiempo a salvo / espera | 6 s / 30 s |

### Red
| Parámetro | Valor |
|---|---|
| Tick servidor | 30 Hz |
| Compensación de latencia | hasta 150 ms |
| Reconexión | 30 s |

---

## Apéndice B: lo que NO hace un niño

Lista de control para validar cualquier animación, habilidad o ruta del mapa. **Si algo de aquí aparece, se elimina.**

- [ ] Saltar más de ~0.45 m en vertical o ~1.8 m en largo.
- [ ] Doble salto, planeo, impulso en el aire.
- [ ] Correr por paredes, rebotar entre muros, pared-salto.
- [ ] Trepar fachadas de edificios sin apoyo real.
- [ ] Saltar entre azoteas separadas por más de 1 m.
- [ ] Caer de más de 2 m sin consecuencia (o sin que el mapa lo impida).
- [ ] Rodar al aterrizar como protección.
- [ ] Deslizarse de rodillas, "slide" de acción.
- [ ] Giros instantáneos a toda velocidad.
- [ ] Velocidades de más de ~5.5 m/s.
- [ ] Aguantar un esprint largo (> 4–5 s).
- [ ] Trepar sin cansarse.
- [ ] Cualquier animación o postura **militar o táctica** (cubrirse, apuntar, rodar, cargar arma).
- [ ] Cualquier acción de **violencia** (golpes, empujones, armas, derribos).
- [ ] Esconderse **dentro** de electrodomésticos cerrados (freezer, nevera, baúl).
- [ ] Ruido o contenido adulto (alcohol, apuestas visibles, groserías).
- [ ] Habilidades "poderes" de personaje (invisibilidad, teletransporte, supervelocidad).

---

*Fin del borrador v0.1. Siguiente paso: responder las preguntas abiertas (sección 22) para pasar a la v1.0.*
