Una importante empresa de robótica en Buenos Aires, Sky.Net, necesita un nuevo software donde simular sus múltiples entidades mecánicas para optimizar su inventario y tener un mejor control de sus capacidades y su estado.

Sky.Net tiene varios robots a su disposición, que llamamos operadores y, que por ahora vienen en 3 tipos principales. Estos operadores son drásticamente diferentes unos de otros: Drones voladores de varias hélices bajo el acrónimo “UAV”, unidades cuadrúpedas “K9”, y entidades semi-humanoides de carga “M8”.

La empresa trabaja desde un cuartel general que tiene visibilidad del estado de sus operadores mecánicos y puede asignar órdenes directamente. Todos los operadores tienen un identificador único*, una batería**, un estado general, un valor de carga máxima en kilos*** y una velocidad óptima de movimiento**** en kilómetros/hora y una localización actual*****. Pueden haber más datos esenciales sin listar.

*Somos libres de usar el sistema que creamos más conveniente para esto.
**El tipo de batería es diferente para todos los tipos de operador y se calcula en mAh (miliAmperios por hora, donde 1000mAh quiere decir 1 hora de uso). En orden según su tipo tienen una batería de 4000 mAh, 6500 mAh, 12250 mAh.
***La carga máxima se divide por tipo en 5kg - 40 kg - 250 kg 
****Se estima que una entidad robótica en viaje se mueve constantemente o por promedio a esta velocidad.
*****Puede ser un string de momento.

Aparte, todos los operadores son capaces de ciertas acciones en común.
1) Moverse una cantidad de kilómetros hacia otra localización, consumiendo correspondientemente la batería. Nos mencionaron que por cada 10% utilizados de carga, los operadores se mueven un 5% más lento.
2) Transferir una carga de batería de un operador a otro, considerando que deben estar en la misma localización.
3) Transferir una carga física de una entidad a otra, usando sentido común.
4) Volver al cuartel y transferir toda la carga física.
5) Volver al cuartel y cargar batería.

Del cuartel, podemos realizar varias operaciones en particular:
1) Listar el estado de todos los operadores.
2) Listar el estado de todos los operadores que estén en cierta localización.
3) Hacer un total recall (llamado y retorno) general a todos los operadores.
4) Seleccionar un operador en específico y:
  a) Enviarlo a una localización en especial.
  b) indicar retorno a cuartel
  c) cambiar estado a STANDBY - una entidad en STANDBY no puede ser utilizada por comandos generales.
5) Agregar o remover operadores de la reserva.

No es obligación crear un menú de opciones para acceder a la funcionalidad del programa, pero por motivos de testing, quizá sea adecuado.
