Este programa sirve para comparar las retenciones percepciones de AFIP con el Excel extraido de la plataforma de contabilidad. Comparaciones que realiza:
1- LLamada para comparar por cuit, importe y fecha (mayor exactitud).
2- Segunda llamada para que compare de manera exacta sin tener en cuenta la fecha
3- Tercera llamada para que compare con una tolerancia de 1 peso en el importe
4- Cuarta llamada para que compare con una tolerancia de 2 pesos en el importe
5- Quinta llamada para comparar por certificado e importe 
6- Sexta llamada para comparar por fecha e importe exactos sin tener en cuenta CUIT        

De manera que primero va a marcar con mayor exactitud, teniendo en cuenta la mayor cantidad de parametros, señalizando si ya fue comparado o no para evitar comparar el mismo registro
en las demas llamadas. Tambien exporta un Excel de reporte, indicando "Retenciones AFIP", "Retenciones que no están en AFIP pero sí registradas", "Retenciones Contabilidad" y "Retenciones 
que no están en Contabilidad pero sí en AFIP" y la diferencia entre estas. 
