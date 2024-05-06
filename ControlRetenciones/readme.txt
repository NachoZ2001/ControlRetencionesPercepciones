Resumen:

Primero se deben seleccionar los dos Excel correspondiente y luego la carpeta donde se desea que se genere el REPORTE.
Luego, al hacer click en "Procesar", se ejecuta la funcion Procesar, la cual llama a otras dos funciones:
-Comparar Archivos: esta llama a 8 funciones:
	-CompararArchivosPorCuitFechaImporte: funcion que compara teniendo en cuenta CUIT, Fecha e Importe. Es la primera comparacion y la mas exacta. Buscamos primero los EXACTOS para evitar que marque similares.
	-CompararArchivosPorCuit (tolerancia 0.1): segunda llamada para comparar, en este caso ignoramos la fecha. Marca si el cuit y el importe coinciden con una tolerancia de 0.1 pesos. 
	-CompararArchivosPorCuit (tolerancia 1): tercera llamada, identica a la segunda nada mas que aumenta la tolerancia a 1 peso.
	-CompararArchivosPorCuit (tolerancia 2:) cuarta llamada, tambien identica a la segunda pero con tolerancia de 2 pesos.
	-CompararArchivosPorFechaEImporte(pathfileArchivo1, pathfileArchivo2): esta funcion compara teniendo en cuenta la fecha y el importe, ignorando CUIT. Marca unicamente si coinciden ambos da manera EXACTA.
	-CompararArchivosPorCertificado(pathfileArchivo1, pathfileArchivo2): ultima llamda para comparar, en este caso se hace por numero de certificado e importe, tambien deben coincider de manera EXACTA para que se marque. 
	-MarcarNoCoincidentesEnRojo: esta funcion se llama 2 veces, la misma es para marcar en rojo los que no coincidieron en ninguna de las comparaciones anteriores.

-CrearReporteExcel: esta funcion crea un reporte en Excel de los resultados, devolviendo el total de retenciones en AFIP, el total de retenciones en HOLISTOR, el total de las retenciones registradas en Holistor pero no en AFIP (registros señalizados en rojo en el Excel de Holistor), el total de las retenciones registradas en AFIP pero no en holistor (registros señalizados en rojo en el Excel de AFIP) y por ultimo la diferencia.             

Devuelve como resultado un reporte con lo ya mencionado anteriormente y ambos archivos Excel señalizados, marcando en verde lo que se encuentra registrado en ambas plataformas y señalizando en rojo lo que se encuentra registrado en la plataforma HOLISTOR/AFIP pero no en la otra. 